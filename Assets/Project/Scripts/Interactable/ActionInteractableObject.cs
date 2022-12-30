using System;

namespace Project
{
    public abstract class ActionInteractableObject<T> : InteractableObject<T>
    {
        private Action<T> _onExit;
        private Action<T> _onInteracted;

        public void Setup(Action<T> onInteracted, Action<T> onExit)
        {
            _onInteracted = onInteracted;
            _onExit = onExit;
        }

        protected override void OnInteract(T component)
        {
            _onInteracted?.Invoke(component);
        }

        protected override void OnExit(T component)
        {
            _onExit?.Invoke(component);
        }
    }
}