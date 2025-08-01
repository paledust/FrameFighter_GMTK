using UnityEngine;
using DG.Tweening;

public class Dragable_Clock : Basic_Clickable
{
    [SerializeField] private Transform clockTrans;
    [SerializeField, ShowOnly] private float angle;
    [SerializeField] private FrameMeter[] meters;

    private int snapIndex = 0;
    private bool isSnap = true;
    private bool isZero = true;

    void Start()
    {
        isSnap = true;
        isZero = true;
        angle = 0f;
        snapIndex = 0;
        meters[snapIndex].ActivateMeter();
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

    public override void ControllingUpdate(PlayerController player)
    {
        Vector2 cursorPos = player.GetCursorWorldPoint(0);
        Vector2 diff = cursorPos - (Vector2)clockTrans.position;
        angle = Vector2.SignedAngle(diff, Vector2.up);
        if (angle < 0) angle += 360;

        int roundIndex = Mathf.RoundToInt(angle / 15f);
        transform.rotation = Quaternion.Euler(0, 0, -Vector2.SignedAngle(diff, Vector2.up));
        if (Mathf.Abs(angle - roundIndex * 15f) <= 5f)
        {
            if (!isSnap)
            {
                if (roundIndex != snapIndex)
                {
                    if (roundIndex != 0 && isZero)
                        isZero = false;
                    if (roundIndex == 0 && !isZero)
                        isZero = true;
                    meters[snapIndex].DeactivateMeter();
                    snapIndex = roundIndex% meters.Length;
                    meters[snapIndex].ActivateMeter();
                    EventHandler.Call_OnRefreshFrame(snapIndex);
                }
                isSnap = true;
            }
        }
        else
        {
            isSnap = false;
        }
    }
}
