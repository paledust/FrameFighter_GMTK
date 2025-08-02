using UnityEngine;

public class HitBox : MonoBehaviour
{
    private CharacterBase self;
    void Awake()
    {
        self = GetComponentInParent<CharacterBase>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        var attackbox = collision.GetComponent<AttackBox>();
        if (attackbox!=null)
        {
            self.TakeDamge();
            attackbox.OnHit();
        }
    }
}
