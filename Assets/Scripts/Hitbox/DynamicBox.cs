using System.Collections;
using UnityEngine;

public class DynamicBox : MonoBehaviour
{
    [SerializeField] private float duration = 0.2f;
    private CoroutineExcuter counter;
    void Awake()
    {
        counter = new CoroutineExcuter(this);
    }
    public void ActivateBox()
    {
        gameObject.SetActive(true);
        counter.Excute(coroutineCountDeactivate());
    }
    IEnumerator coroutineCountDeactivate()
    {
        yield return new WaitForSeconds(duration);
        gameObject.SetActive(false);
    }
}
