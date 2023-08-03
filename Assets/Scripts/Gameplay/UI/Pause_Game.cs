using Core;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Callback handler when pause game event is raised
    /// </summary>
    public class Pause_Game : MonoBehaviour, ISOCallback
    {
        [SerializeField, TextArea(6, 10)]
        private string _comment = "Set Timescale to 0/1, and enable/disable given screen";
        /// <summary>
        /// pause screen panel
        /// </summary>
        [SerializeField]
        private GameObject _screen;

        /// <summary>
        /// Callback from SO
        /// </summary>
        /// <param name="param"></param>
        public void OnEventRaisedCallback(params object[] param)
        {
            bool _state = (bool)param[0];
            _screen.SetActive(_state);
            Time.timeScale = _state == true ? 0 : 1;
        }
    }
}