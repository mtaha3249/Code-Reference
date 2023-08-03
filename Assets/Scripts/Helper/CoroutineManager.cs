using System.Collections;
using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Manager which handle all kind of external coroutine calls
    /// </summary>
    public class CoroutineManager : MonoBehaviour
    {
        /// <summary>
        /// Singelton Instance
        /// </summary>
        public static CoroutineManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    _instance = go.AddComponent<CoroutineManager>();
                    go.name = "Coroutine_Manager (Auto Created)";
                    DontDestroyOnLoad(go);
                }

                return _instance;
            }
        }

        static CoroutineManager _instance;

        private void Awake()
        {
            StopAllCoroutines();
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
        }

        /// <summary>
        /// Run Given Coroutine
        /// </summary>
        /// <param name="routine"></param>
        public void RunCoroutine(IEnumerator routine)
        {
            StartCoroutine(routine);
        }

        /// <summary>
        /// Stop All Coroutines
        /// </summary>
        public void StopCoroutine()
        {
            StopAllCoroutines();
        }
    }
}