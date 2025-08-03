using UnityEngine;

public class FrameBehave_DynamicRevive : FrameBehave
{
    [SerializeField] private int reviveFrame = 0;
    protected override void OnFrameRefresh(int frameIndex, int frameDelta, int loopDir)
    {
        if (frameIndex == reviveFrame)
        {
            PlayerManager.Instance.FlashInput();
            GameManager.Instance.RestartLevel();
        }
    }
}
