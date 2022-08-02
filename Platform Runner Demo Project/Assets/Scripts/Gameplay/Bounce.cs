using UnityEngine;

public class Bounce : MonoBehaviour
{
    public float force = 10f;
    public float stunTime = 0.5f;
    private Vector3 hitDirection;

    private void OnCollisionStay(Collision collision)
    {
        foreach (ContactPoint contact in collision.contacts)
        {
            Debug.DrawRay(contact.point, contact.normal, Color.white);
            if (collision.transform.CompareTag("Player"))
            {
                hitDirection = contact.normal;
                collision.gameObject.GetComponent<PlayerController>().HitPlayer(-hitDirection * force, stunTime);
                return;
            }

            if (collision.transform.CompareTag("AI"))
            {
                hitDirection = contact.normal;
                collision.gameObject.GetComponent<AIController>().Hit(-hitDirection * force, stunTime);
                return;
            }
        }
    }
    
}
