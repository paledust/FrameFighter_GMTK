using UnityEngine;

public class PlayerSkill : MonoBehaviour
{
    [SerializeField] private int skillCharge = 5;
    [SerializeField] private ParticleSystem chargeParticle;
    private float chargeCounter = 0;
    void Start()
    {
        chargeCounter = 0;
    }
    void OnEnable()
    {
        EventHandler.E_OnParried += OnParried;
        EventHandler.E_OnTriggerSkill += OnSkill;
    }
    void OnDisable()
    {
        EventHandler.E_OnParried -= OnParried;
        EventHandler.E_OnTriggerSkill -= OnSkill;
    }
    void OnParried(ThrowingObject throwingObject)
    {
        if (chargeCounter < skillCharge)
        {
            chargeCounter++;
            if (chargeCounter >= skillCharge)
            {
                chargeParticle.Play();
            }
        }
    }
    void OnSkill()
    {
        Debug.LogWarning("Skill Triggered!");
        chargeParticle.Stop();
        chargeCounter = 0;
    }
}
