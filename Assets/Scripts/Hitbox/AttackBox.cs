using System;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private Action<HitBox> onHit;
    private Action<DefendBox> onBlocked;
    private Action<AttackBox> onParried;

    public void Init(Action<HitBox> _onHit, Action<DefendBox> _onBlocked, Action<AttackBox> _onParried)
    {
        onHit = _onHit;
        onBlocked = _onBlocked;
        onParried = _onParried;
    }
    public void OnParried(AttackBox attackBox)
    {
        onParried?.Invoke(attackBox);
    }
    public void OnHit(HitBox hitBox)
    {
        onHit?.Invoke(hitBox);
    }
    public void OnBlocked(DefendBox defendBox)
    {
        onBlocked?.Invoke(defendBox);
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var attackbox = collision.GetComponent<AttackBox>();
        if (attackbox!=null)
        {
            attackbox.OnParried(this);
        }
    }
}
