using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Helper
{
    /// <summary>
    /// Static function for loading scene management
    /// </summary>
    public class SwitchScene
    {
        /// <summary>
        /// Load Current scene
        /// </summary>
        public static void LoadCurrentScene()
        {
            LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        /// <summary>
        /// Load scene by index
        /// </summary>
        /// <param name="index">scene index</param>
        public static void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        }

        /// <summary>
        /// Load scene by name
        /// </summary>
        /// <param name="name">scene name</param>
        public static void LoadScene(string name)
        {
            SceneManager.LoadScene(name);
        }

        /// <summary>
        /// Load scene async
        /// </summary>
        /// <param name="index">scene index</param>
        /// <param name="OnProgressChanged"></param>
        /// <param name="OnCompleted"></param>
        public static void LoadSceneAsync(int index, Action<float> OnProgressChanged, Action OnCompleted)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(index);
            async.allowSceneActivation = false;
            CoroutineManager.Instance.RunCoroutine(LoadProgressRoutine(async, OnProgressChanged, OnCompleted));
        }

        /// <summary>
        /// Load scene async
        /// </summary>
        /// <param name="name">scene name</param>
        /// <param name="OnProgressChanged"></param>
        /// <param name="OnCompleted"></param>
        public static void LoadSceneAsync(string name, Action<float> OnProgressChanged, Action OnCompleted)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = false;
            CoroutineManager.Instance.RunCoroutine(LoadProgressRoutine(async, OnProgressChanged, OnCompleted));
        }

        /// <summary>
        /// Run coroutine for async progress
        /// </summary>
        /// <param name="async"></param>
        /// <param name="OnProgressChanged"></param>
        /// <param name="OnCompleted"></param>
        /// <returns></returns>
        static IEnumerator LoadProgressRoutine(AsyncOperation async, Action<float> OnProgressChanged, Action OnCompleted)
        {
            while (!async.isDone)
            {
                OnProgressChanged?.Invoke(async.progress);
                if (async.progress >= 0.9f)
                {
                    OnProgressChanged?.Invoke(1.0f);
                    OnCompleted?.Invoke();
                    async.allowSceneActivation = true;
                }
                yield return null;
            }
        }
    }
}