using System;
using UnityEngine;

public class FrameBehave_ChargeAttack : FrameBehave
{
    public enum ChargeState
    {
        None,
        Charging,
        Charged
    }
    [SerializeField] private int chargeInitFrame = 4;
    [SerializeField] private int chargeEndFrame = 8;
    [SerializeField] private float chargeSpeed = 4f;
    [SerializeField] private string abilityName = "ChargeAttack";
    [Header("VFX")]
    [SerializeField] private ParticleSystem p_charge;

    private ChargeState chargeState = ChargeState.None;
    private int delta;
    private float chargeTimer;

    void Start()
    {
        delta = chargeEndFrame - chargeInitFrame;
    }
    void Update()
    {
        if (chargeState == ChargeState.Charging)
        {
            chargeTimer += Time.deltaTime * chargeSpeed;
            if (chargeTimer >= 1f)
            {
                p_charge.Play();
                EventHandler.Call_OnChargedAbility(abilityName);
                chargeState = ChargeState.Charged;
                chargeTimer = 0f;
            }
        }
    }
    protected override void OnFrameRefresh(int frameIndex, int frameDelta)
    {
        switch (chargeState)
        {
            case ChargeState.None:
                if (frameIndex == chargeInitFrame)
                {
                    chargeState = ChargeState.Charging;
                    chargeTimer = 0f;
                }
                break;
            case ChargeState.Charging:
                if (frameIndex != chargeInitFrame)
                {
                    CancelCharge();
                }
                break;
            case ChargeState.Charged:
                if (frameIndex >= chargeEndFrame && frameDelta * delta > 0)
                {
                    EventHandler.Call_OnTriggerAbility(abilityName);
                    CancelCharge();
                }
                if (frameDelta * delta < 0)
                {
                    CancelCharge();
                }
                break;
        }
    }
    void CancelCharge()
    {
        chargeState = ChargeState.None;
        chargeTimer = 0f;
        p_charge.Stop();
    }
}
