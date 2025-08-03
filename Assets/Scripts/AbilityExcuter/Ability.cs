using System;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public string abilityName;
    public string abilityID;
    public Vector2Int highLightFrame;
    public Color abilityColor;
    public GameObject abilityPrefab;
}
public enum AbilityState
{
    Detached,   //挂起
    Pending,    //载入
    Working,    //生效中
    Complete,   //结束
}
public abstract class Ability : MonoBehaviour
{
    [SerializeField] protected string abilityID;
    protected AbilityController parent = null; //buff作用者
    protected AbilityState buffState = AbilityState.Detached;//buff生命状态

    public string m_abilityID => abilityID;
    public bool IsPending => buffState == AbilityState.Pending;
    public bool IsDone => buffState == AbilityState.Complete;

    protected Action onBuffStart;
    protected Action onBuffComplete;
    protected Action onBuffRemoved;


    public virtual void Initialize(AbilityController parent)
    {
        this.parent = parent;
    }
    //修改buff的状态
    public void ChangeBuffState(AbilityState newState)
    {
        if (buffState == newState) return;
        buffState = newState;
        switch (buffState)
        {
            case AbilityState.Working:
                onBuffStart?.Invoke();
                AbilityBegin();
                break;
            case AbilityState.Complete:
                onBuffComplete?.Invoke();
                AbilityComplete();
                break;
            case AbilityState.Detached:
                onBuffRemoved?.Invoke();
                AbilityRemove();
                break;
            default:
                break;
        }
    }
    public virtual void RefreshAbility() { } //buff刷新后
    public virtual void UpdateAbility() { } //buff生效中

    protected virtual void AbilityBegin() { } //buff生效后
    protected virtual void AbilityComplete() { } //buff销毁前
    protected virtual void AbilityRemove() { } //buff销毁后

    public Ability OnBuffStart(Action startCallback)
    {
        onBuffStart = startCallback;
        return this;
    }
    public Ability OnBuffComplete(Action completeCallback)
    {
        onBuffComplete = completeCallback;
        return this;
    }
    public Ability OnBuffRemoved(Action destroyCallback)
    {
        onBuffRemoved = destroyCallback;
        return this;
    }
}