using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float flySpeed = 12;
    [SerializeField] private AttackBox attackBox;
    private Rigidbody2D m_rigid;
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.linearVelocity = transform.up * flySpeed;
        Destroy(gameObject, lifeTime);

        attackBox.Init(OnHit, OnBlocked);
    }
    void OnBlocked()
    {
        Destroy(gameObject);
    }
    void OnHit()
    {

    }
}
