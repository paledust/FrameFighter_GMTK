using System.Collections;
using UnityEngine;

public class PlayerCharacter : CharacterBase
{
    [SerializeField] private SpriteRenderer loopSprite;
    [SerializeField] private SpriteRenderer damageRender;
    [SerializeField] private Sprite[] damageSprite;
    [SerializeField] private float stunTime = 0.1f;
    private CoroutineExcuter damageAnimator;
    private bool stuned;
    void Start()
    {
        stuned = false;
        damageAnimator = new CoroutineExcuter(this);
    }

    protected override bool IsDamagable() => !stuned;
    protected override void OnTakeDamage()=>damageAnimator.Excute(coroutineTakeDamage());

    IEnumerator coroutineTakeDamage()
    {
        stuned = true;
        damageRender.sprite = damageSprite[Random.Range(0, damageSprite.Length)];
        damageRender.enabled = true;
        loopSprite.enabled = false;
        yield return new WaitForSeconds(stunTime);
        damageRender.enabled = false;
        loopSprite.enabled = true;
        stuned = false;
    }
}
