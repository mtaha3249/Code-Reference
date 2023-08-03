using Core;
using GameDesign;
using Gameplay;
using Helper;
using Pool;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tetris
{
    /// <summary>
    /// Spawn All tetris and show next tetris
    /// </summary>
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private SO_Event _nextSpawn;
        [SerializeField]
        private SO_Event _tilePlaced;
        [SerializeField]
        private SO_Event _meterUpdate;

        int _spawnIndex;
        GameObject _itemToSpawn;
        Camera _camera;
        Player _player;
        Bounds _bound = new Bounds();

        public Action OnSpawned;

        /// <summary>
        /// Player component
        /// </summary>
        public Player Player
        {
            get => _player;
        }

        /// <summary>
        /// Is Player?
        /// </summary>
        public bool IsPlayer
        {
            get
            {
                return _player as User;
            }
        }

        /// <summary>
        /// Tile placed event
        /// </summary>
        public SO_Event TilePlaced
        {
            get => _tilePlaced;
        }

        /// <summary>
        /// Initialize tetris spawner
        /// </summary>
        public void Initialize(Player player)
        {
            this._player = player;
            _camera = Camera.main;
            FindTile();
            CoroutineManager.Instance.RunCoroutine(UpdateBounds());
        }

        /// <summary>
        /// Spawn Tetris tile and find next
        /// </summary>
        public void SpawnTetrisTile()
        {
            GameObject spawned = PoolManager.Instance.Spawn(_itemToSpawn, GetSpawnPosition(), Quaternion.identity);
            TileBehaviour _spawnedTile = spawned.GetComponent<TileBehaviour>();
            _spawnedTile.InitializeBehaviour(this);
            _player.Tiles.Add(_spawnedTile);
            _player.CurrentTile = _spawnedTile;
            OnSpawned?.Invoke();
            FindTile();
        }

        /// <summary>
        /// Find Tile to spawn
        /// </summary>
        void FindTile()
        {
            _spawnIndex = Random.Range(0, GameData.Instance.GetCurrentLevel().Tetris.Length);
            _itemToSpawn = GameData.Instance.GetCurrentLevel().Tetris[_spawnIndex];
            // Raise event to notify UI
            _nextSpawn.Raise(_itemToSpawn.GetComponent<TileBehaviour>().TileData.Sprite);
        }

        /// <summary>
        /// Find Spawn Position
        /// </summary>
        /// <returns></returns>
        Vector3 GetSpawnPosition()
        {
            if (_camera == null)
                _camera = Camera.main;
            Vector3 currentPosition = transform.position;
            currentPosition.y = _camera.ScreenToWorldPoint(new Vector3(0, Screen.height + 100, 0)).y;
            return currentPosition;
        }

        /// <summary>
        /// Remove tile from the player
        /// </summary>
        /// <param name="_tile"></param>
        public void RemoveTile(TileBehaviour tile)
        {
            _player.Tiles.Remove(tile);
            _player.Tiles.TrimExcess();
        }

        /// <summary>
        /// Add or Remove from the list for the placed element
        /// </summary>
        /// <param name="collider">collide to add or remove</param>
        /// <param name="canAdd">add or remove</param>
        public void ModifyPlaced(Collider2D collider, bool canAdd)
        {
            if (canAdd)
                _player.PlacedTile.Add(collider);
            else
                _player.PlacedTile.Remove(collider);
        }

        /// <summary>
        /// Update bounds base on tiles player have
        /// doing coroutine to optimize the cost of call instead of update/doing every frame
        /// </summary>
        IEnumerator UpdateBounds()
        {
            WaitForSeconds _wait = new WaitForSeconds(0.15f);
            while (_player)
            {
                _bound.size = Vector3.zero;
                if (_player.PlacedTile.Count > 0)
                    _bound.center = _player.PlacedTile[0].bounds.center;
                for (int i = 0; i < _player.PlacedTile.Count; i++)
                {
                    _bound.Encapsulate(_player.PlacedTile[i].bounds);
                }
                yield return _wait;
                _player.CheckComplete(_bound);
                _player.AdjustCamera(_bound);
                _meterUpdate?.Raise(GameData.Instance.GetCurrentLevel().Win, _bound.size.y);
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(_bound.center, _bound.size);
        }
    }
}