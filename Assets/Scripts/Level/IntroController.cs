using System.Collections;
using UnityEngine;

public class IntroController : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private GameObject tutorialUI;
    [SerializeField] private GameObject wheel;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject swapPlayer;
    void OnEnable()
    {
        EventHandler.E_OnLoop += OnLoop;
    }
    void OnDisable()
    {
        EventHandler.E_OnLoop -= OnLoop;
    }
    void OnLoop()
    {
        this.enabled = false;
        PlayerManager.Instance.FlashInput();
        player.SetActive(false);
        tutorialUI.SetActive(false);
        wheel.SetActive(false);
        swapPlayer.SetActive(true);
        StartCoroutine(coroutineDelaySwitchScene(0.5f));
    }
    IEnumerator coroutineDelaySwitchScene(float delay)
    {
        yield return new WaitForSeconds(delay);
        GameManager.Instance.SwitchingScene(nextSceneName);
    }
}
