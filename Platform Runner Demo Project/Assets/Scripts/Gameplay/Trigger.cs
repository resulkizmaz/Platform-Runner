using UnityEngine;
using UnityEngine.Events;

public class Trigger : MonoBehaviour
{
    [SerializeField] private bool killZone;
    [SerializeField] private UnityEvent OnTriggerEvent;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnTriggerEvent.Invoke();

        if(other.CompareTag("AI") && killZone)
            other.GetComponent<AIController>().RestartAI();
    }
}
