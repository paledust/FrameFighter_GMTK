using UnityEngine;

public class Ability_Heal : Ability
{
    [SerializeField] private float duration = 1.5f;
    private float abilityTimer = 0;
    public override void UpdateAbility()
    {
        abilityTimer += Time.deltaTime;
        if (abilityTimer >= duration)
        {
            ChangeBuffState(AbilityState.Complete);
        }
    }
    protected override void AbilityRemove()
    {
        Destroy(gameObject);
    }
}
