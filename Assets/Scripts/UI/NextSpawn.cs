using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    /// <summary>
    /// Handle the callback for the next tile spawn and show the image earlier in the game
    /// </summary>
    public class NextSpawn : MonoBehaviour, ISOCallback
    {
        [SerializeField]
        private Image _image;

        public void OnEventRaisedCallback(params object[] param)
        {
            _image.sprite = (Sprite)param[0];
            _image.SetNativeSize();
        }
    }
}