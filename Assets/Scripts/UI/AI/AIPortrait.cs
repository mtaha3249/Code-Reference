using Core;
using UnityEngine;

namespace UI
{
    /// <summary>
    /// Enable or disable AI portrait
    /// </summary>
    public class AIPortrait : MonoBehaviour, ISOCallback
    {
        public void OnEventRaisedCallback(params object[] param)
        {
            bool _hasAI = (bool)param[0];
            gameObject.SetActive(_hasAI);
        }
    }
}