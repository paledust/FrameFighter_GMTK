using UnityEngine;

public class Ability_Heal : Ability
{
    [SerializeField] private float duration = 1.5f;
    private float abilityTimer = 0;
    protected override void AbilityBegin()
    {
        parent.m_playerCharacter.HealCharacter();
    }
    public override void UpdateAbility()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= duration)
        {
            ChangeBuffState(AbilityState.Complete);
        }
    }
    public override void RefreshAbility()
    {
        parent.m_playerCharacter.HealCharacter();
        abilityTimer = 0;
    }
    protected override void AbilityRemove()
    {
        Destroy(gameObject);
    }
}
