using UnityEditor;
using UnityEngine;

namespace Save
{
    /// <summary>
    /// Editor extension for reset all saved game data
    /// </summary>
    public static class ResetSaveData
    {
        [MenuItem("Tetris/Reset Save Data %&r", priority = 0)]
        static void ResetSave()
        {
            PlayerPrefs.DeleteAll();
            EditorUtility.DisplayDialog("Cleared Save Data.","All saved progress has been deleted.", "Ok");
        }
    }
}