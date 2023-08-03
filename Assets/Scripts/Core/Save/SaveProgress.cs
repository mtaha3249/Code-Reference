using System;
using UnityEngine;

namespace Save
{
    /// <summary>
    /// Actual data to save in the progress
    /// We can add bool, float, array, list anything that can be serializeable by Unity JSON Utility
    /// To add dictonary and any other kind of thing we should use 3rd party json parser like (Newtonsoft)
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public bool isFirstTime = false;
        public int level = 0;
    }

    /// <summary>
    /// Save and load progress of the game
    /// </summary>
    public class SaveProgress : MonoBehaviour
    {
        const string _prefsKey = "game_progress";
        public SaveData _data;
        static SaveProgress _instance;

        public static SaveProgress Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject _go = new GameObject();
                    _go.name = "Save Progress";
                    _instance = _go.AddComponent<SaveProgress>();
                    DontDestroyOnLoad(_go);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            _data = Load();
        }

        /// <summary>
        /// Load Game progress from prefs
        /// </summary>
        /// <returns></returns>
        SaveData Load()
        {
            if(PlayerPrefs.HasKey(_prefsKey))
            {
                string _progress = PlayerPrefs.GetString(_prefsKey);
                Debug.Log("Game Progress Loaded: " + _progress);
                return JsonUtility.FromJson<SaveData>(_progress);
            }
            else
            {
                Debug.Log("New Game Progress Created");
                SaveData _data = new SaveData();
                SaveWithData(_data);
                return _data;
            }
        }

        /// <summary>
        /// Save all modified game progress
        /// </summary>
        public void Save()
        {
            SaveWithData(_data);
        }

        /// <summary>
        /// Save game progress of given data
        /// </summary>
        /// <param name="_data"></param>
        void SaveWithData(SaveData _data)
        {
            string _progress = JsonUtility.ToJson(_data, true);
            PlayerPrefs.SetString(_prefsKey, _progress);
            PlayerPrefs.Save();
        }
    }
}