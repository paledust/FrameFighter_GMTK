using UnityEngine;

public class Ability_Defence : Ability
{
    [SerializeField] private float duration = 5f;
    private float abilityTimer = 0;
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
        abilityTimer -= 1;
    }
    protected override void AbilityRemove()
    {
        Destroy(gameObject);
    }
}
