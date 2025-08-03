using TMPro;
using UnityEngine;

public class FrameBehave_Tutorial : FrameBehave
{
    [SerializeField] private TextMeshProUGUI tmpUI;
    [SerializeField] private string tutorialText;
    [SerializeField] private int frameIndex;

    protected override void OnFrameRefresh(int frameIndex, int frameDelta, int loopDir)
    {
        if (this.frameIndex == frameIndex)
        {
            this.enabled = false;
            tmpUI.text = tutorialText;
        }
    }
}
