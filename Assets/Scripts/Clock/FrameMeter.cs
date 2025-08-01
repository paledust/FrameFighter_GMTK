using UnityEngine;
using DG.Tweening;

public class FrameMeter : MonoBehaviour
{
    [SerializeField] private SpriteRenderer meterRender;
    [SerializeField] private Color activeColor = Color.cyan;
    public void ActivateMeter()
    {
        meterRender.color = activeColor;
        transform.transform.DOKill();
        transform.transform.DOPunchScale(Vector3.one * 0.1f, 0.15f);
    }
    public void DeactivateMeter()
    {
        meterRender.color = Color.black;
    }
}
