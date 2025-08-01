using UnityEngine;

public class FrameController : MonoBehaviour
{
    [SerializeField] private Animator characterAnimator;
    [SerializeField] private int frameRate = 24;
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
        // Assuming the animator has a parameter named "FrameIndex"
        characterAnimator.Play(LoopState, 0, (frameIndex+0f)/24f);
    }
}
