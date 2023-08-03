using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Modify Level Complete and Fail screen title
    /// </summary>
    public class LevelEndTitle : MonoBehaviour, ISOCallback
    {
        [SerializeField]
        private Image _title;
        [SerializeField]
        private Sprite _aiTitle, _simpleTitle;

        public void OnEventRaisedCallback(params object[] param)
        {
            bool _hasAI = (bool)param[0];
            _title.sprite = _hasAI ? _aiTitle : _simpleTitle;
        }
    }
}