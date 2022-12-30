using UnityEngine;

namespace Project
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractableObject <T>: MonoBehaviour
    {
        private void Start()
        {
            GetComponent<Collider>().isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out T component))
            {
                OnInteract(component);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out T component))
            {
                OnExit(component);
            }
        }

        protected abstract void OnInteract(T component);
        
        protected abstract void OnExit(T component);
    }
}