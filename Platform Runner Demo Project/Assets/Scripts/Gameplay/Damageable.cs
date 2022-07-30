using UnityEngine;
using UnityEngine.Events;

public class Damageable : MonoBehaviour
{
    [SerializeField] private int healthAmount;
    [Space(10)]
    [SerializeField] private UnityEvent OnDeadEvent;


    private int _health;

    private void Start()
    {
        _health = healthAmount;
    }

    public void TakeDamage(int damageAmount)
    {
        _health -= damageAmount;
        if (_health <= 0)
        {
            PlayerController _pc = GetComponent<PlayerController>();
            if (_pc != null)
                _pc.CanMove = true;
            OnDeadEvent.Invoke();
        }
    }


}
