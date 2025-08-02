using UnityEngine;

public class FrameBehave_DynamicAttackBox : MonoBehaviour
{
    [SerializeField] private int TargetFrame = 9;
    [SerializeField] private int DeltaDir = 1;
    [SerializeField] private DynamicBox dynamicBox;
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
        if (frameIndex == TargetFrame && frameDelta * DeltaDir > 0)
        {
            dynamicBox.ActivateBox();
        }
    }
}