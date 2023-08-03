using UnityEngine;
using UnityEngine.Events;

namespace Core
{
    /// <summary>
    /// Monobehavioue handle the callback of the given event when raised
    /// </summary>
    public class SO_Listener : MonoBehaviour
    {
        [SerializeField]
        protected SO_Event _event;

        [SerializeField]
        private UnityEvent<object[]> _callbacks;

        private void OnEnable()
        {
            // register me
            _event.AddListener(this);
        }

        private void OnDisable()
        {
            // unregister me
            _event.RemoveListener(this);
        }

        /// <summary>
        /// Callback when event is raised
        /// </summary>
        /// <param name="args"></param>
        public void OnEventRaised(params object[] args)
        {
            _callbacks.Invoke(args);
        }
    }
}