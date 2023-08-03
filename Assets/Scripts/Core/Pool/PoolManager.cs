using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Pool
{
    /// <summary>
    /// Handle all the pooling of the items given
    /// </summary>
    public class PoolManager : MonoBehaviour
    {
        [SerializeField]
        GameObject[] _itemToSpawned;
        [SerializeField]
        int _initialSize = 2;
        [SerializeField]
        private bool _isExpandable;

        /// <summary>
        /// Dictionary for fast finding of the list of the item type
        /// </summary>
        Dictionary<string, List<GameObject>> _allSpawned = new Dictionary<string, List<GameObject>>();

        static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = Instantiate(Resources.Load<GameObject>("Pool Manager"), Vector3.zero, Quaternion.identity);
                    _instance = go.GetComponent<PoolManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        private void Awake()
        {
            Init();
            SceneManager.activeSceneChanged += OnActiveSceneChanged;
        }

        private void OnDestroy()
        {
            SceneManager.activeSceneChanged -= OnActiveSceneChanged;
        }

        private void OnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            DespawnAll();
        }

        /// <summary>
        /// Initialize Once
        /// </summary>
        void Init()
        {
            for (int x = 0; x < _itemToSpawned.Length; x++)
            {
                List<GameObject> _spawnedList = new List<GameObject>();
                for (int i = 0; i < _initialSize; i++)
                {
                    GameObject _spawned = Instantiate(_itemToSpawned[x], Vector3.zero, Quaternion.identity);
                    _spawned.transform.parent = transform;
                    _spawned.SetActive(false);
                    _spawnedList.Add(_spawned);
                }
                _allSpawned.Add(_itemToSpawned[x].name, _spawnedList);
            }
        }

        /// <summary>
        /// Spawn given gameobject
        /// </summary>
        /// <param name="itemToSpawn">object to spawn</param>
        /// <param name="position">position to be</param>
        /// <param name="rotation">rotaion to be</param>
        /// <returns>spawned gameobject to be</returns>
        public GameObject Spawn(GameObject itemToSpawn, Vector3 position, Quaternion rotation)
        {
            List<GameObject> _spawnedList = _allSpawned[itemToSpawn.name];
            for (int i = 0; i < _spawnedList.Count; i++)
            {
                if (!_spawnedList[i].activeInHierarchy)
                {
                    _spawnedList[i].transform.position = position;
                    _spawnedList[i].transform.rotation = rotation;
                    _spawnedList[i].SetActive(true);
                    return _spawnedList[i];
                }
            }
            if (_isExpandable)
            {
                GameObject _spawned = Instantiate(itemToSpawn, position, rotation);
                _spawned.transform.parent = _spawnedList[0].transform.parent;
                _spawned.SetActive(true);
                _spawnedList.Add(_spawned);
                _allSpawned[itemToSpawn.name] = _spawnedList;
                return _spawned;
            }

            return null;
        }

        /// <summary>
        /// Despawn given object
        /// </summary>
        /// <param name="toDespawn">object to despawn</param>
        /// <param name="OnDone">Action done when despawned</param>
        /// <param name="delay">Delay to spawn</param>
        public void Despawn(GameObject toDespawn, Action OnDone = null, float delay = 0)
        {
            StartCoroutine(DespawnWithDelay(toDespawn, delay, OnDone));
        }

        /// <summary>
        /// Routine to wait and despawn
        /// </summary>
        /// <param name="toDespawn"></param>
        /// <param name="delay"></param>
        /// <param name="OnDone"></param>
        /// <returns></returns>
        IEnumerator DespawnWithDelay(GameObject toDespawn, float delay, Action OnDone = null)
        {
            yield return new WaitForSeconds(delay);
            toDespawn.SetActive(false);
            OnDone?.Invoke();
        }

        /// <summary>
        /// Despawn all objects
        /// </summary>
        public void DespawnAll()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                Despawn(transform.GetChild(i).gameObject);
            }
        }
    }
}
