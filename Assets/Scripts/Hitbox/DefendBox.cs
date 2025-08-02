using System;
using UnityEngine;

public class DefendBox : MonoBehaviour
{
    private Action<AttackBox> onBlocked;
    public void Init(Action<AttackBox> _onBlocked)
    {
        onBlocked = _onBlocked;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var attackbox = collision.GetComponent<AttackBox>();
        if (attackbox!=null)
        {
            onBlocked?.Invoke(attackbox);
            attackbox.OnBlocked(this);
        }
    }
}