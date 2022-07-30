using UnityEngine;

public class Damaging : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.CompareTag("Player") || collision.transform.CompareTag("AI"))
        {
            Damageable damageableObject = collision.gameObject.GetComponent<Damageable>();
            if (damageableObject != null)
                damageableObject.TakeDamage(damageAmount);
        }

    }


}
