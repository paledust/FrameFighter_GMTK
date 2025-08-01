using UnityEngine;

public class DefendBox : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Service.ThrowableTag))
        {
            collision.GetComponent<AttackBox>()?.OnBlocked();
        }
    }
}