using Core;
using GameDesign;
using Save;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Main game controller
    /// </summary>
    public class GameController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _userPrefab, _aiPrefab;
        [SerializeField]
        private SO_Event _hasAI;
        [SerializeField]
        private SO_Event _informationScreen;
        private Player _currentPlayer, _currentAI;

        private void Awake()
        {
            Time.timeScale = 1;

            _hasAI.Raise(GameData.Instance.GetCurrentLevel().HasAI);
            _currentPlayer = Instantiate(_userPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();
            if (GameData.Instance.GetCurrentLevel().HasAI)
                _currentAI = Instantiate(_aiPrefab, Vector3.zero, Quaternion.identity).GetComponent<Player>();

            _currentPlayer?.UpdatePosition();
            _currentAI?.UpdatePosition();

            if (!SaveProgress.Instance._data.isFirstTime)
            {
                _informationScreen.Raise(true);
                SaveProgress.Instance._data.isFirstTime = true;
                SaveProgress.Instance.Save();
            }
        }

        /// <summary>
        /// Level fail
        /// </summary>
        public void LevelFail()
        {
            Time.timeScale = 0;
        }

        /// <summary>
        /// Level Complete
        /// </summary>
        public void LevelComplete()
        {
            Time.timeScale = 0;
            SaveProgress.Instance._data.level += 1;
            SaveProgress.Instance.Save();
        }
    }
}