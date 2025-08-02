using UnityEngine;

public class ThrowingObject : MonoBehaviour
{
    public int particleIndex;
    [SerializeField] protected float lifeTime = 5;
    [SerializeField] protected float flySpeed = 12;
    [SerializeField] protected AttackBox attackBox;

    protected Rigidbody2D m_rigid;
    protected bool isInitialized = false;

    public void Init(Transform spawnPoint, Transform aimPoint)
    {
        m_rigid = GetComponent<Rigidbody2D>();

        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        m_rigid.linearVelocity = transform.up * flySpeed;
        Destroy(gameObject, lifeTime);

        attackBox.Init(OnHit, OnBlocked, OnParried);
        OnInitialized(spawnPoint, aimPoint);
        isInitialized = true;
    }
    protected virtual void OnInitialized(Transform spawnPoint, Transform aimPoint){}
    void Start()
    {
        if (!isInitialized)
        {
            m_rigid = GetComponent<Rigidbody2D>();
            m_rigid.linearVelocity = transform.up * flySpeed;
            Destroy(gameObject, lifeTime);

            attackBox.Init(OnHit, OnBlocked, OnParried);
        }
    }
    protected void OnParried(AttackBox attackBox)
    {
        if (attackBox.tag == Service.PlayerTag)
        {
            EventHandler.Call_OnParried(this);
            Destroy(gameObject);
        }
    }
    protected void OnBlocked(DefendBox defendBox)
    {
        if (attackBox.tag == Service.PlayerTag)
        {
            EventHandler.Call_OnParried(this);
            Destroy(gameObject);
        }
    }
    protected void OnHit(HitBox hitBox)
    {
        if (hitBox.tag == Service.PlayerTag)
        {
            Destroy(gameObject);
        }
    }
}
