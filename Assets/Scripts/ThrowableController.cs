using UnityEngine;

public class ThrowableController : MonoBehaviour
{
    [SerializeField] private GameObject[] throwables;
    [SerializeField] private Transform throwPointRoot;
    [SerializeField] private float throwFreq = 5f;
    [Header("VFX")]
    [SerializeField] private ParticleSystem p_blink;
    [SerializeField] private ParticleSystem[] p_parrieds;
    private Transform[] throwPoints;
    private float flowTimer = 0f;
    void OnEnable()
    {
        throwPoints = new Transform[throwPointRoot.childCount];
        for (int i = 0; i < throwPointRoot.childCount; i++)
        {
            throwPoints[i] = throwPointRoot.GetChild(i);
        }

        EventHandler.E_OnParried += OnParried;
    }
    void OnDisable()
    {
        EventHandler.E_OnParried -= OnParried;
    }
    void OnParried(ThrowingObject throwingObject)
    {
        p_blink.transform.position = throwingObject.transform.position;
        p_blink.Play();

        var p_parried = p_parrieds[throwingObject.particleIndex];
        p_parried.transform.position = throwingObject.transform.position;
        p_parried.transform.rotation = throwingObject.transform.rotation;
        p_parried.Play();
    }
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
