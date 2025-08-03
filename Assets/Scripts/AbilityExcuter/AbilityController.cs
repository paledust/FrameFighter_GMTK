using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    [SerializeField] private List<AbilityData> abilities;
    [SerializeField] private Dragable_Clock clock;
    private Dictionary<string, Ability> activeAbilities;
    private Action<Ability> onAbilityCreated;

    void OnEnable()
    {
        EventHandler.E_OnTriggerAbility += OnTriggerAbility;
        EventHandler.E_OnChargedAbility += OnChargedAbility;
        EventHandler.E_OnCancelCharge += OnCancelCharge;
    }
    void OnDisable()
    {
        EventHandler.E_OnTriggerAbility -= OnTriggerAbility;
        EventHandler.E_OnChargedAbility -= OnChargedAbility;
        EventHandler.E_OnCancelCharge -= OnCancelCharge;
    }
    void Start()
    {
        activeAbilities = new Dictionary<string, Ability>();
    }
    void Update()
    {
        if (activeAbilities == null) return;
        foreach (var buff in activeAbilities.Values.ToList())
        {
            if (buff.IsPending)
                buff.ChangeBuffState(AbilityState.Working);
            if (buff.IsDone)
                HandleAbilityComplete(buff);
            else
            {
                buff.UpdateAbility();
                if (buff.IsDone)
                    HandleAbilityComplete(buff);
            }
        }
    }
    void OnChargedAbility(string abilityName)
    {
        var ability = abilities.Find(a => a.abilityName == abilityName);
        for (int i = ability.highLightFrame.x; i <= ability.highLightFrame.y; i++)
        {
            clock.HighLightFrame(i, ability.abilityColor);
        }
    }
    void OnCancelCharge(string abilityName)
    {
        var ability = abilities.Find(a => a.abilityName == abilityName);
        for (int i = ability.highLightFrame.x; i <= ability.highLightFrame.y; i++)
        {
            clock.DeactivateFrame(i);
        }
    }
    void OnTriggerAbility(string abilityName)
    {
        Debug.Log($"Ability triggered: {abilityName}");
        var abilityData = abilities.Find(a => a.abilityName == abilityName);
        var ability = Instantiate(abilityData.abilityPrefab, transform.position, Quaternion.identity).GetComponent<Ability>();

        //添加技能
        ability.Initialize(this);
        //Todo：通过Buff Manager创建Buff，并调用Buff Awake
        if (activeAbilities == null)
            activeAbilities = new Dictionary<string, Ability>();

        if (activeAbilities.ContainsKey(ability.m_abilityID))
        {
            ability.RefreshAbility();
            //Todo:同类型buff，触发刷新buff事件
        }
        else
        {
            activeAbilities.Add(ability.m_abilityID, ability);
            ability.ChangeBuffState(AbilityState.Pending);
        }
        onAbilityCreated?.Invoke(ability);
    }
    public void CleanUpAllAbility()
    {
        foreach (var ability in activeAbilities.Values.ToList())
        {
            HandleAbilityRemove(ability);
        }
        activeAbilities.Clear();
    }
    public void RemoveAbility(string id)
    {
        if (activeAbilities.ContainsKey(id))
        {
            HandleAbilityRemove(activeAbilities[id]);
        }
    }
    void HandleAbilityRemove(Ability ability)
    {
        activeAbilities.Remove(ability.m_abilityID);
        ability.ChangeBuffState(AbilityState.Detached);
    }
    void HandleAbilityComplete(Ability ability)
    {
        activeAbilities.Remove(ability.m_abilityID);
        ability.ChangeBuffState(AbilityState.Detached);
    }
}
