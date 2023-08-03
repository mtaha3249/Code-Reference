using Core;
using UnityEditor;
using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Editor extension for adding listener and removing listener from the Hierarchy of the selected gameobject
    /// </summary>
    public static class SO_Listener_Modifier
    {
        /// <summary>
        /// Add Listener tool item
        /// </summary>
        [MenuItem("GameObject/Tetris/Add SO Listener", false, 0)]
        static void AddListener()
        {
            GameObject[] _list = Selection.gameObjects;

            for (int i = _list.Length - 1; i >= 0; i--)
            {
                Selection.activeGameObject = _list[i];
                Selection.activeGameObject.AddComponent<SO_Listener>();
            }

            if (_list.Length > 1)
                Selection.activeGameObject = null;
        }

        /// <summary>
        /// Remove Listener tool item
        /// </summary>
        [MenuItem("GameObject/Tetris/Remove All SO Listener", false, 0)]
        static void RemoveAllListener()
        {
            GameObject[] _list = Selection.gameObjects;
            for (int i = _list.Length - 1; i >= 0; i--)
            {
                Selection.activeGameObject = _list[i];
                SO_Listener[] _allListeners = Selection.activeGameObject.GetComponents<SO_Listener>();
                for (int j = _allListeners.Length - 1; j >= 0; j--)
                {
                    UnityEngine.Object.DestroyImmediate(_allListeners[j]);
                }
            }

            if (_list.Length > 1)
                Selection.activeGameObject = null;
        }
    }
}