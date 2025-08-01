using UnityEngine;

public class HitBox : MonoBehaviour
{
    private Character self;
    void Awake()
    {
        self = GetComponentInParent<Character>();
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Service.ThrowableTag))
        {
            self.TakeDamge();
            collision.GetComponent<AttackBox>().OnHit();
        }
    }
}
