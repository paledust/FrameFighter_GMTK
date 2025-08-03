using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [Header("Dead timeline")]
    [SerializeField] private PlayableDirector director;

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
        director.Play();
    }
}
