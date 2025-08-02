using UnityEngine;

public class FrameBehave_DynamicAttackBox : MonoBehaviour
{
    [SerializeField] private int firstFrame = 0;
    [SerializeField] private DynamicBox CW_dynamicBox;
    [SerializeField] private DynamicBox CCW_dynamicBox;
    [SerializeField] private FrameController frameController;

    void OnEnable()
    {
        frameController.OnFrameRefresh += OnFrameRefresh;
    }
    void OnDisable()
    {
        frameController.OnFrameRefresh -= OnFrameRefresh;
    }
    void OnFrameRefresh(int frameIndex, int frameDelta)
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