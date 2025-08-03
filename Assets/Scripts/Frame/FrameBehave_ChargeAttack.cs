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
    protected override void OnFrameRefresh(int frameIndex, int frameDelta, int loopDir)
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
                    EndCharge();
                }
                break;
            case ChargeState.Charged:
                if (loopDir > 0)
                {
                    frameIndex += frameController.FrameRate;
                }
                else if (loopDir < 0)
                {
                    frameIndex -= frameController.FrameRate;
                }

                if (frameDelta * delta < 0)
                {
                    EventHandler.Call_OnCancelCharge(abilityName);
                    EndCharge();
                    break;
                }
                if (frameDelta * delta > 0)
                {
                    if (delta > 0 && frameIndex >= chargeEndFrame)
                    {
                        EventHandler.Call_OnTriggerAbility(abilityName);
                        EndCharge();
                        break;
                    }
                    else if (delta < 0 && frameIndex <= chargeEndFrame)
                    {
                        EventHandler.Call_OnTriggerAbility(abilityName);
                        EndCharge();
                        break;
                    }
                }
                break;
        }
    }
    void EndCharge()
    {
        chargeState = ChargeState.None;
        chargeTimer = 0f;
        p_charge.Stop();
    }
}
