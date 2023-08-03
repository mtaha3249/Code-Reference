using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Conatiner for the event raise
    /// </summary>
    [CreateAssetMenu(fileName = "Event-TYPE", menuName = "Tetris/Create Game Event", order = 1)]
    public class SO_Event : ScriptableObject
    {
        [SerializeField, TextArea(6, 10)]
        private string _comment ="Raised when ??";
        List<SO_Listener> _allListeners = new List<SO_Listener>();

        /// <summary>
        /// Add listener for callbacks
        /// </summary>
        /// <param name="listener"></param>
        public void AddListener(SO_Listener listener)
        {
            _allListeners.Add(listener);
        }

        /// <summary>
        /// Remove listeners for callbacks
        /// </summary>
        /// <param name="listener"></param>
        public void RemoveListener(SO_Listener listener)
        {
            _allListeners.Remove(listener);
        }

        #region Raise Overload Methods
        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="args"></param>
        public void Raise(params object[] args)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(args);
        }

        /// <summary>
        /// Raise Event
        /// </summary>
        public void Raise()
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(null);
        }


        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="val"></param>
        public void Raise(bool val)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(val);
        }

        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="val"></param>
        public void Raise(int val)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(val);
        }

        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="val"></param>
        public void Raise(float val)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(val);
        }

        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="val"></param>
        public void Raise(string val)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(val);
        }

        /// <summary>
        /// Raise Event by given argument
        /// </summary>
        /// <param name="val"></param>
        public void Raise(Object val)
        {
            for (int i = _allListeners.Count - 1; i >= 0; i--)
                _allListeners[i].OnEventRaised(val);
        }
        #endregion
    }
}