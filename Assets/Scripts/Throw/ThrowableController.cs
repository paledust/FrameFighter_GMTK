using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [Header("Throwable Settings")]
    [SerializeField] private GameObject[] throwables;
    [SerializeField] private Transform throwPointRoot;
    [SerializeField] private Transform aimPoint;
    [SerializeField] private float throwFreq = 5f;

    private Transform[] throwPoints;
    private float flowTimer = 0f;

    void OnEnable()
    {
        throwPoints = new Transform[throwPointRoot.childCount];
        for (int i = 0; i < throwPointRoot.childCount; i++)
        {
            throwPoints[i] = throwPointRoot.GetChild(i);
        }
    }

    void Update()
    {
        flowTimer += Time.deltaTime * throwFreq;
        if (flowTimer > 1f)
        {
            flowTimer = 0f;
            var spawnPoint = throwPoints[Random.Range(0, throwPoints.Length)];
            var obj = Instantiate(throwables[Random.Range(0, throwables.Length)], spawnPoint.position, spawnPoint.rotation).GetComponent<ThrowingObject>();
            obj.Init(spawnPoint, aimPoint);
        }
    }
}
