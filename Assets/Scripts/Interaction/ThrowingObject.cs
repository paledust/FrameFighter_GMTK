using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    [SerializeField] private float lifeTime = 5;
    [SerializeField] private float flySpeed = 12;
    private Rigidbody2D m_rigid;
    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        m_rigid.linearVelocity = transform.up * flySpeed;
    }
}
