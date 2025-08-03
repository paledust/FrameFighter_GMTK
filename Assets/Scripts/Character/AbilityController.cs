using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    public string abilityName;
    public Vector2Int highLightFrame;
    public Color abilityColor;
}
public class AbilityController : MonoBehaviour
{
    [SerializeField] private List<AbilityData> abilities;
    [SerializeField] private Dragable_Clock clock;
    void OnEnable()
    {
        EventHandler.E_OnTriggerAbility += OnTriggerAbility;
        EventHandler.E_OnChargedAbility += OnChargedAbility;
    }
    void OnDisable()
    {
        EventHandler.E_OnTriggerAbility -= OnTriggerAbility;
        EventHandler.E_OnChargedAbility -= OnChargedAbility;
    }
    void OnChargedAbility(string abilityName)
    {
        Debug.Log($"Ability Charged: {abilityName}");
        var ability = abilities.Find(a => a.abilityName == abilityName);
        for (int i = ability.highLightFrame.x; i <= ability.highLightFrame.y; i++)
        {
            clock.HighLightFrame(i, ability.abilityColor);
        }
    }
    void OnTriggerAbility(string abilityName)
    {
        Debug.Log($"Ability triggered: {abilityName}");
        var ability = abilities.Find(a => a.abilityName == abilityName);
    }
}
