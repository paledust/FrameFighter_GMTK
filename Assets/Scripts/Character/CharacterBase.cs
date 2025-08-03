using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    [SerializeField] protected int health = 5;
    [SerializeField] protected GameObject hitboxRoot;

    public void TakeDamge()
    {
        if (IsDamagable())
        {
            health--;
            OnTakeDamage();

            if (health <= 0)
            {
                health = 0;
                hitboxRoot.SetActive(false);
                OnDie();
            }
        }
    }
    protected virtual bool IsDamagable() { return true; }
    protected virtual void OnDie()
    {

    }
    protected virtual void OnTakeDamage()
    {
        // Override this method to handle damage effects
    }
}
