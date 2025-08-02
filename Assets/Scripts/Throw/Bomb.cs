using UnityEngine;

public class Bomb : ThrowingObject
{
    [Header("Bomb Settings")]
    [SerializeField] private float flyingTime = 3f;
    [SerializeField] private DefendBox defendBox;
    [SerializeField] private int rotateRound = 2;
    [SerializeField] private SpriteRenderer bombSprite;

    [Header("Explosion Settings")]
    [SerializeField] private GameObject explosion;

    private bool canExplode = true;
    private float flyingTimer = 0;
    private Vector2 aimPos;
    private Vector2 startPos;

    protected override void OnInitialized(Transform spawnPoint, Transform aimPoint)
    {
        base.OnInitialized(spawnPoint, aimPoint);
        m_rigid.linearVelocity = Vector2.zero;
        startPos = spawnPoint.position;
        aimPos = (Vector2)aimPoint.position + Vector2.right * Random.Range(-1f, 1f);
        defendBox.Init(OnDefend);
    }

    void OnDestroy()
    {
        if (canExplode)
            Destroy(Instantiate(explosion, transform.position, Quaternion.identity), 0.1f);
    }
    void Update()
    {
        if (flyingTimer < flyingTime)
        {
            flyingTimer += Time.deltaTime;

            float t = flyingTimer / flyingTime;
            t = Mathf.Clamp01(t);

            float maxHeight = 4.0f;

            // 计算水平方向的位置
            Vector2 horizontalPosition = Vector2.Lerp(startPos, aimPos, t);

            // 计算垂直方向的高度（抛物线公式）
            float y = maxHeight * 4 * (t - t * t);

            float targetY = Mathf.Lerp(startPos.y, aimPos.y, t);
            var result = new Vector2(horizontalPosition.x, targetY + y);

            // 更新位置
            m_rigid.MovePosition(result);

            float scale = Mathf.Sin(t * Mathf.PI) * 0.25f + 1.25f;
            bombSprite.transform.localScale = new Vector3(scale, scale, scale);
            bombSprite.transform.rotation = Quaternion.Euler(0, 0, flyingTimer * rotateRound * 360);

            if (flyingTimer >= flyingTime)
            {
                defendBox.gameObject.SetActive(true);
            }
        }
    }
    void OnDefend(AttackBox attackBox)
    {
        if(attackBox.tag == Service.PlayerTag)
        {
            canExplode = false;
            EventHandler.Call_OnParried(this);
            Destroy(gameObject);
        }
    }
}
