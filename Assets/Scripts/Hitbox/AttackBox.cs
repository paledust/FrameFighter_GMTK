using System;
using UnityEngine;

public class AttackBox : MonoBehaviour
{
    private Action onHit;
    private Action onBlocked;

    public void Init(Action _onHit, Action _onBlocked)
    {
        onHit = _onHit;
        onBlocked = _onBlocked;
    }
    public void OnHit()
    {
        onHit?.Invoke();
        onBlocked?.Invoke();
    }
    public void OnBlocked()
    {
        Debug.LogWarning("Blocked");

        onBlocked?.Invoke();
    }
}
