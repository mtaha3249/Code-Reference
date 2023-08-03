using Core;
using UnityEngine;

namespace Gameplay
{
    public class PlayBackgroundMusic : MonoBehaviour
    {
        [SerializeField]
        private string _audioName;

        private void Awake()
        {
            AudioManager.Instance.PlayLoopAudio(_audioName);
        }
    }
}