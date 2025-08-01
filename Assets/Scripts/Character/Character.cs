using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private float health = 5;
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
    public void TakeDamge()
    {
        if (stuned) return;
        damageAnimator.Excute(coroutineTakeDamage());
        health--;
    }
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
