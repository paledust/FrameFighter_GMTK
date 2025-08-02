using UnityEngine;

public class ThrowableVFXController : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] private ParticleSystem p_blink;
    [SerializeField] private ParticleSystem[] p_parrieds;
    void OnEnable()
    {
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
}
