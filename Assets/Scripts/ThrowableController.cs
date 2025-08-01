using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] private GameObject[] throwables;
    [SerializeField] private Transform[] throwPoints;
    [SerializeField] private float throwFreq = 5f;
    private float flowTimer = 0f;
    void Update()
    {
        flowTimer += Time.deltaTime * throwFreq;
        if (flowTimer > 1f)
        {
            flowTimer = 0f;
            var spawnPoint = throwPoints[Random.Range(0, throwPoints.Length)];
            var obj = Instantiate(throwables[Random.Range(0, throwables.Length)], spawnPoint.position, spawnPoint.rotation);
        }
    }
}
