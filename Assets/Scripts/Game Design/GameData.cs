using Save;
using System.Collections.Generic;
using UnityEngine;

namespace GameDesign
{
    /// <summary>
    /// Data Container for the game
    /// </summary>
    [CreateAssetMenu(fileName = "Game Data", menuName = "Tetris/Game Design/Create Game Data", order = 1)]
    public class GameData : ScriptableObject
    {
        [Range(0, 3)]
        public float _tileRotateSpeed = 0.25f;
        [SerializeField]
        private List<Level> _allLevels;

        static GameData _instance;

        public static GameData Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<GameData>("Game Data");
                }
                return _instance;
            }
        }

        /// <summary>
        /// Return current level information
        /// </summary>
        /// <returns></returns>
        public Level GetCurrentLevel()
        {
            return _allLevels[SaveProgress.Instance._data.level % _allLevels.Count];
        }
    }
}