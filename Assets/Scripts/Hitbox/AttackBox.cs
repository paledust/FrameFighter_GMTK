using System;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private Action onHit;
    private Action onBlocked;
    private Action onParried;

    public void Init(Action _onHit, Action _onBlocked, Action _onParried)
    {
        onHit = _onHit;
        onBlocked = _onBlocked;
        onParried = _onParried;
    }
    public void OnParried()
    {
        onParried?.Invoke();
    }
    public void OnHit()
    {
        onHit?.Invoke();
    }
    public void OnBlocked()
    {
        onBlocked?.Invoke();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Service.ThrowableTag))
        {
            collision.GetComponent<AttackBox>()?.OnParried();
        }
    }
}
