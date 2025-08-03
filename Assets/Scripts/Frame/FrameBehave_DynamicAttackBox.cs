using UnityEngine;

public class FrameBehave_DynamicAttackBox : FrameBehave
{
    [SerializeField] private int firstFrame = 0;
    [SerializeField] private DynamicBox CW_dynamicBox;
    [SerializeField] private DynamicBox CCW_dynamicBox;

    protected override void OnFrameRefresh(int frameIndex, int frameDelta, int loopDir)
    {
        if (frameIndex == frameController.GetNextFrame(firstFrame) && frameDelta > 0)
        {
            CW_dynamicBox.ActivateBox();
        }
        else if (frameIndex == firstFrame && frameDelta < 0)
        {
            CCW_dynamicBox.ActivateBox();
        }
    }
}