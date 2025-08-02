using UnityEngine;

public class Enemy : CharacterBase
{
    public enum EnemyState
    {
        Idle,
        Move,
        Dead
    }
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private Animator animator;
    [SerializeField] private EnemyState currentState = EnemyState.Idle;

    private Rigidbody2D m_rigid;

    private static readonly int DEAD_TRIGGER = Animator.StringToHash("dead");
    private static readonly int MOVE_BOOL = Animator.StringToHash("ismove");

    void Start()
    {
        m_rigid = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentState = EnemyState.Move;
        animator.SetBool(MOVE_BOOL, true);
    }
    protected override void OnDie()
    {
        animator.SetTrigger(DEAD_TRIGGER);
        m_rigid.simulated = false;
        Destroy(gameObject, 2f);
    }
    void FixedUpdate()
    {
        if (currentState == EnemyState.Move)
        {
            m_rigid.MovePosition(m_rigid.position + Vector2.right * moveSpeed * Time.fixedDeltaTime);
        }
    }
}
