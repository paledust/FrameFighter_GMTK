using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Sprite HeartSprite;
    [SerializeField] private Sprite DamageSprite;
    [SerializeField] private Image[] heartIcons;
    void OnEnable()
    {
        EventHandler.E_OnPlayerHealthChange += OnPlayerHealthChange;
    }
    void OnDisable()
    {
        EventHandler.E_OnPlayerHealthChange -= OnPlayerHealthChange;
    }
    void OnPlayerHealthChange(int health)
    {
        for(int i=0; i < heartIcons.Length; i++)
        {
            if(i <= health-1)
                heartIcons[i].sprite = HeartSprite;
            else
                heartIcons[i].sprite = DamageSprite;
        }
    }
}
