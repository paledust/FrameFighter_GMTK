using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class GameController : MonoBehaviour
{
    [Header("Dead timeline")]
    [SerializeField] private PlayableDirector director;
    [SerializeField] private PlayableDirector gameDirector;
    [SerializeField] private float startDelay = 1.5f;
    [SerializeField] private GameObject[] throwControllers;
    void Start()
    {
        StartCoroutine(coroutineStart());
    }
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
    IEnumerator coroutineStart()
    {
        yield return new WaitForSeconds(startDelay);
        foreach (var controller in throwControllers)
        {
            controller.SetActive(true);
        }
        gameDirector.Play();
    }
}
