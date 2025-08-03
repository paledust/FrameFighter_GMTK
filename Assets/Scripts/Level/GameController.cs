using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject circle;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject frameController;
    [Header("Dead")]
    [SerializeField] private GameObject deadplayer;
    [SerializeField] private GameObject deadCircle;
    [SerializeField] private GameObject deadFrameController;

    void OnEnable()
    {
        EventHandler.E_OnPlayerDie += OnPlayerDie;
    }
    void OnDisable()
    {
        EventHandler.E_OnPlayerDie -= OnPlayerDie;
    }
    void OnPlayerDie()
    {
        circle.SetActive(false);
        player.SetActive(false);
        frameController.SetActive(false);
        deadplayer.SetActive(true);
        deadCircle.SetActive(true);
        deadFrameController.SetActive(true);
    }
}
