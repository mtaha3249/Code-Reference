using Core;
using GameDesign;
using System.Collections.Generic;
using Tetris;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Base class of player
    /// This is use for User and AI
    /// </summary>
    public abstract class Player : MonoBehaviour
    {
        protected Spawner _spawner;

        protected List<TileBehaviour> _tileBehaviour = new List<TileBehaviour>();
        protected List<Collider2D> _placedTile = new List<Collider2D>();
        protected TileBehaviour _currentTile;
        protected int _tileFallIndex;
        protected Camera _camera;
        [SerializeField, Header("Moving Camera")]
        protected Transform _movingCamera;
        [SerializeField, Tooltip("X means minimum size to start moving"), Range(1, 20)]
        private float _cameraMin = 10;
        private Vector3 _currentPosition;
        protected Bounds _bounds;
        private Vector2 _cameraFromRange, _boundRange;

        [SerializeField, Header("Game End")]
        protected SO_Event _levelFail;
        [SerializeField]
        protected SO_Event _levelComplete;

        /// <summary>
        /// all tiles player have
        /// </summary>
        public List<TileBehaviour> Tiles
        {
            get => _tileBehaviour;
        }

        public List<Collider2D> PlacedTile
        {
            get => _placedTile;
        }

        /// <summary>
        /// Current moving tile
        /// </summary>
        public TileBehaviour CurrentTile
        {
            get => _currentTile;
            set => _currentTile = value;
        }

        protected virtual void Awake()
        {
            _spawner = GetComponent<Spawner>();
            _spawner.Initialize(this);
            _camera = Camera.main;

            _cameraFromRange = new Vector2(0, GameData.Instance.GetCurrentLevel().Win / 2);
            _boundRange = new Vector2(0, GameData.Instance.GetCurrentLevel().Win);
        }

        /// <summary>
        /// Update Camera height
        /// </summary>
        /// <param name="bound"></param>
        public void AdjustCamera(Bounds bound)
        {
            if (_movingCamera == null)
                return;

            _bounds = bound;
            _currentPosition = _movingCamera.localPosition;
            if (bound.size.y >= _cameraMin)
            {
                _currentPosition.y = MathsFunctions.Remap(bound.size.y, _boundRange, _cameraFromRange);
            }
            else
            {
                _currentPosition.y = 0;
            }
        }

        /// <summary>
        /// Late update for camera movement
        /// </summary>
        protected virtual void LateUpdate()
        {
            if (_movingCamera == null)
                return;

            // move camera base on tower size
            _movingCamera.localPosition = Vector3.Lerp(_movingCamera.localPosition, _currentPosition, Time.deltaTime);
        }

        /// <summary>
        /// Callback when tile fall in the water
        /// </summary>
        public abstract void OnTileFall(params object[] args);

        /// <summary>
        /// Update position
        /// Calls once and update position base on the AI
        /// </summary>
        public abstract void UpdatePosition();

        /// <summary>
        /// Check level complete condition
        /// </summary>
        /// <param name="bound">bound to check</param>
        public abstract void CheckComplete(Bounds bound);
    }
}