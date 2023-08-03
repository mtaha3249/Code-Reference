using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Base class of button add listener automatic
    /// </summary>
    [RequireComponent(typeof(Button))]
    public abstract class Btn_Base : MonoBehaviour
    {
        /// <summary>
        /// Auto Add listener
        /// </summary>
        [SerializeField]
        protected bool _autoAddListener = true;
        [SerializeField]
        protected string _audioName = "Button Click";
        /// <summary>
        /// button component reference
        /// </summary>
        protected Button _btn;

        protected virtual void Awake()
        {
            _btn = GetComponent<Button>();
            if (_autoAddListener)
            {
                _btn.onClick.AddListener(() =>
                {
                    AudioManager.Instance.PlayOneShotAudio(_audioName);
                });
                _btn.onClick.AddListener(OnClick);
            }
        }

        /// <summary>
        /// What happens when button is clicked
        /// </summary>
        public abstract void OnClick();
    }
}