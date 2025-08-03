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
    protected override void AbilityRemove()
    {
        Destroy(gameObject);
    }
}
