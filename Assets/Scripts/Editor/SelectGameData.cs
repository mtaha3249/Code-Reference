using System.IO;
using UnityEditor;
using UnityEngine;

namespace GameDesign
{
    /// <summary>
    /// Editor extension function
    /// Create OR Select game data
    /// </summary>
    public static class SelectGameData
    {
        [MenuItem("Tetris/Select Game Data %&g", priority = 2)]
        static void Select()
        {
            if(Resources.Load<GameData>("Game Data"))
            {
                Selection.activeObject = Resources.Load<GameData>("Game Data");
            }
            else
            {
                GameData _gameData = ScriptableObject.CreateInstance<GameData>();
                string _path = "Assets/Resources/Game Data.asset";
                if (!Directory.Exists(Application.dataPath +"/Resources/"))
                {
                    Directory.CreateDirectory(Application.dataPath + "/Resources");
                }
                AssetDatabase.CreateAsset(_gameData, _path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                EditorUtility.FocusProjectWindow();
                Selection.activeObject = _gameData;
            }
        }
    }
}