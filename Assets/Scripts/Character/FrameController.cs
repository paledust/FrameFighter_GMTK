using System;
using UnityEngine;

public class FrameController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private int frameRate = 24;
    public event Action<int, int> OnFrameRefresh;

    private int lastFrameIndex = 0;

    private const string LoopState = "loop";

    void OnEnable()
    {
        EventHandler.E_OnRefreshFrame += RefreshFrame;
    }
    void OnDisable()
    {
        EventHandler.E_OnRefreshFrame -= RefreshFrame;
    }
    void Start()
    {
        characterAnimator.speed = 0;
    }
    void RefreshFrame(int frameIndex)
    {
        int PostFrameDelta = frameIndex + frameRate - lastFrameIndex;
        int PreFrameDelta = frameIndex - frameRate - lastFrameIndex;
        int FrameDelta = frameIndex - lastFrameIndex;
        if(Mathf.Abs(PostFrameDelta) < Mathf.Abs(FrameDelta))
        {
            FrameDelta = PostFrameDelta;
        }
        else if(Mathf.Abs(PreFrameDelta) < Mathf.Abs(FrameDelta))
        {
            FrameDelta = PreFrameDelta;
        }

        lastFrameIndex = frameIndex;
        OnFrameRefresh?.Invoke(frameIndex, FrameDelta);
        characterAnimator.Play(LoopState, 0, (frameIndex + 0f) / (frameRate + 0f));
    }
    public int GetNextFrame(int currentFrame) => (currentFrame + 1)%frameRate;
}
