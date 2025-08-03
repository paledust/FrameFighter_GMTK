using System.Collections;
using UnityEngine;
using DG.Tweening;

public class PlayerCharacter : CharacterBase
{
    [SerializeField] private SpriteRenderer loopSprite;
    [SerializeField] private Sprite[] damageSprite;
    [SerializeField] private float stunTime = 0.1f;
    private CoroutineExcuter damageAnimator;
    private bool stuned;
    private int MaxHealth = 3;

    void Start()
    {
        stuned = false;
        MaxHealth = health;
        damageAnimator = new CoroutineExcuter(this);
    }

    protected override bool IsDamagable() => !stuned;
    protected override void OnTakeDamage()
    {
        EventHandler.Call_OnPlayerHealthChange(health);
        damageAnimator.Excute(coroutineTakeDamage());
    }
    protected override void OnDie()
    {
        EventHandler.Call_OnPlayerDie();
    }
    public void HealCharacter()
    {
        health++;
        health = Mathf.Min(health, MaxHealth);
        EventHandler.Call_OnPlayerHealthChange(health);
    }

    IEnumerator coroutineTakeDamage()
    {
        stuned = true;
        loopSprite.transform.DOKill();
        loopSprite.transform.DOShakePosition(stunTime, 0.5f, 20);
        yield return new WaitForSeconds(stunTime);
        stuned = false;
    }
}
