using UnityEngine;
using DG.Tweening;

public class Dragable_Clock : Basic_Clickable
{
    [SerializeField] private Transform clockTrans;
    [SerializeField, ShowOnly] private float angle;
    [SerializeField] private FrameMeter[] meters;
    private int snapIndex = 0;
    private bool isSnap = true;
    [SerializeField] private bool isZero = true;
    private float stepAngle = 16;
    private float accumulatedAngle = 0f;
    private Vector2 lastDir = Vector2.up;

    void Start()
    {
        isSnap = true;
        isZero = true;
        angle = 0f;
        snapIndex = 0;
        meters[snapIndex].ActivateMeter();
        stepAngle = 360f / meters.Length;
    }

    public override void OnClick(PlayerController player, Vector3 hitPos)
    {
        player.HoldInteractable(this);
        clockTrans.DOKill();
        clockTrans.DOPunchScale(Vector3.one * 0.1f, 0.15f);
    }

    public override void OnRelease(PlayerController player)
    {
        player.ReleaseCurrentHolding();
    }

    public void OnScroll(PlayerController player, float scrollDelta)
    {
        angle = snapIndex * stepAngle;
        if (scrollDelta > 0)
        {
            angle += stepAngle;
        }
        else if (scrollDelta < 0)
        {
            angle -= stepAngle;
        }

        AngleClamp();

        int roundIndex = Mathf.RoundToInt(angle / stepAngle);
        transform.rotation = Quaternion.Euler(0, 0, -angle);

        accumulatedAngle += Vector2.SignedAngle(transform.up, lastDir);
        lastDir = transform.up;

        Snap(roundIndex);
    }
    public override void ControllingUpdate(PlayerController player)
    {
        Vector2 cursorPos = player.GetCursorWorldPoint(0);
        Vector2 diff = cursorPos - (Vector2)clockTrans.position;
        angle = Vector2.SignedAngle(diff, Vector2.up);

        AngleClamp();

        int roundIndex = Mathf.RoundToInt(angle / stepAngle);
        transform.rotation = Quaternion.Euler(0, 0, -angle);

        accumulatedAngle += Vector2.SignedAngle(transform.up, lastDir);
        lastDir = transform.up;

        if (Mathf.Abs(angle - roundIndex * stepAngle) <= 5f)
        {
            if (!isSnap)
            {
                Snap(roundIndex);
                isSnap = true;
            }
        }
        else
        {
            isSnap = false;
        }
    }
    public void HighLightFrame(int frameIndex, Color highlightColor)
    {
        frameIndex = frameIndex % meters.Length;
        meters[frameIndex].HighLightMeter(highlightColor);
    }
    public void DeactivateFrame(int frameIndex)
    {
        frameIndex = frameIndex % meters.Length;
        meters[frameIndex].DeactivateMeter();
    }
    void AngleClamp()
    {
        if (angle < 0) angle += 360;
        if (angle >= 360) angle -= 360;
    }
    void Snap(int roundIndex)
    {
        if (roundIndex != snapIndex)
        {
            if (roundIndex % meters.Length != 0 && isZero)
                isZero = false;
            if (roundIndex % meters.Length == 0 && !isZero)
            {
                if (Mathf.Abs(accumulatedAngle) > 360 - stepAngle)
                {
                    EventHandler.Call_OnLoop();
                    lastDir = Vector2.up;
                    accumulatedAngle = Vector2.SignedAngle(transform.up, lastDir);
                    lastDir = transform.up;
                }
                isZero = true;
            }
            meters[snapIndex].DeactivateMeter();
            snapIndex = roundIndex % meters.Length;
            meters[snapIndex].ActivateMeter();
            EventHandler.Call_OnRefreshFrame(snapIndex);
        }        
    }
}
