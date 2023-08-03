using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Main GameManager class
    /// Should be use only to communicate among scene or some importan functions
    /// </summary>
    public class GameManager
    {
        private static GameManager _instance;

        public static GameManager Instance
        {
            get
            {
                if(_instance==null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        public Background _background;

        /// <summary>
        /// Spawn game background
        /// </summary>
        public void SpawnBackground()
        {
            _background = GameObject.Instantiate(Resources.Load<GameObject>("Art - 2D Game Kit"), Vector3.zero, Quaternion.identity).GetComponent<Background>();
            _background.name = "Art - 2D Game Kit";
            GameObject.DontDestroyOnLoad(_background.gameObject);
        }
    }
}