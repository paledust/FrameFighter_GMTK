using UnityEngine;

public abstract class FrameBehave : MonoBehaviour
{
    [SerializeField] protected FrameController frameController;
    void OnEnable()
    {
        frameController.OnFrameRefresh += OnFrameRefresh;
    }
    void OnDisable()
    {
        frameController.OnFrameRefresh -= OnFrameRefresh;
    }
    protected abstract void OnFrameRefresh(int frameIndex, int frameDelta);
}
