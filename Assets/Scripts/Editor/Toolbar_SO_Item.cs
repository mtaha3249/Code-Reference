using Core;
using UnityEditor;
using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Editor extension for creating event scriptable object or creating gameobject with listener
    /// </summary>
    public static class Toolbar_SO_Item
    {
        [MenuItem("Tetris/Create SO Event %#e", priority = 1)]
        static void CreateEvent()
        {
            SO_Event _soEvent = ScriptableObject.CreateInstance<SO_Event>();
            string _path = "Assets/SO_Event.asset";
            AssetDatabase.CreateAsset(_soEvent, _path);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = _soEvent;
        }

        [MenuItem("Tetris/Create SO Listener %#l", priority = 1)]
        static void CreateListener()
        {
            GameObject _go = new GameObject();
            _go.AddComponent<SO_Listener>();
            _go.name = "SO_Listener_New";
            Selection.activeObject = _go;
        }
    }
}