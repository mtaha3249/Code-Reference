using UnityEngine;
using Helper;
using GameDesign;
using Core;
using Pool;
using Gameplay;

namespace Tetris
{
    /// <summary>
    /// Main class of tile
    /// This class handle all kind of movement and intialize tile
    /// </summary>
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class TileBehaviour : MonoBehaviour
    {
        [SerializeField]
        private TileData _data;
        [SerializeField]
        private SO_Event _tileFall;
        [Header("VFX"), SerializeField]
        private GameObject _hitParticle;
        [SerializeField]
        private GameObject _acidParticle;

        Rigidbody2D _body2D;
        Collider2D _collider2D;
        bool _isFalling = true;
        Vector3 _currentRotation;
        Spawner _spawner;

        Vector2 _xClampPosition;

        public TileData TileData
        {
            get => _data;
        }

        public bool IsFalling
        {
            get => _isFalling;
        }

        /// <summary>
        /// Validate all components needed for this script
        /// </summary>
        void ValidateComponent()
        {
            if (_body2D == null)
                _body2D = GetComponent<Rigidbody2D>();
            if (_collider2D == null)
                _collider2D = GetComponent<Collider2D>();
        }

        /// <summary>
        /// Initialize behaviour
        /// </summary>
        public void InitializeBehaviour(Spawner spawner)
        {
            ValidateComponent();
            ResetTile();
            _currentRotation = transform.eulerAngles;
            _xClampPosition = new Vector2(transform.position.x - 1.5f, transform.position.x + 1.5f);
            _data.Gravity = GameData.Instance.GetCurrentLevel().Gravity;
            this._spawner = spawner;
            ApplyVelocity();
        }

        /// <summary>
        /// Apply velocity to the body
        /// </summary>
        private void ApplyVelocity()
        {
            if (_isFalling)
            {
                _body2D.velocity = Vector3.down * _data.Gravity;
            }
        }

        /// <summary>
        /// Rotate tile base on Input
        /// </summary>
        public void Rotate()
        {
            _currentRotation += Vector3.forward * 90;
            if (_isFalling)
                transform.Rotate(GameData.Instance._tileRotateSpeed, _currentRotation);
        }

        /// <summary>
        /// Modify gravity base on gesture
        /// </summary>
        /// <param name="isDownGesture">true will increase gravity, false return to original state</param>
        public void ModifyGravity(bool isDownGesture)
        {
            _data.Gravity = isDownGesture ? GameData.Instance.GetCurrentLevel().MaxGravity : GameData.Instance.GetCurrentLevel().Gravity;
            ApplyVelocity();
        }

        /// <summary>
        /// Pan tile to right or left
        /// </summary>
        /// <param name="isRight">is Right side pan?</param>
        public void Pan(bool isRight)
        {
            Vector3 newPosition = transform.position + (isRight ? Vector3.right * 0.5f : Vector3.left * 0.5f);
            if (newPosition.x >= _xClampPosition.y)
            {
                newPosition.x = _xClampPosition.y;
            }
            else if (newPosition.x <= _xClampPosition.x)
            {
                newPosition.x = _xClampPosition.x;
            }
            transform.position = newPosition;
        }

        /// <summary>
        /// Reset the data of this tile
        /// </summary>
        private void ResetTile()
        {
            _isFalling = true;
            _body2D.mass = 1;
            _body2D.gravityScale = 0;
            _body2D.drag = 0f;
            _body2D.angularDrag = 0f;
        }

        /// <summary>
        /// Get current collider Bound
        /// </summary>
        /// <returns></returns>
        public Bounds GetBounds()
        {
            return _collider2D.bounds;
        }

        /// <summary>
        /// Spawn particle
        /// </summary>
        /// <param name="position">position to spawn</param>
        /// <param name="isHit">is hit particle? or acid?</param>
        void SpawnParticle(Vector3 position, bool isHit)
        {
            // spawn base on isHit?
            // then despawn after 2 seconds
            PoolManager.Instance.Despawn(PoolManager.Instance.Spawn(isHit ? _hitParticle : _acidParticle, position, Quaternion.identity), null, 2);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if ((collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Tile")) && _isFalling)
            {
                // reset properties
                _isFalling = false;
                _body2D.mass = 1000;
                _body2D.gravityScale = 1;
                _body2D.drag = 1f;
                _body2D.angularDrag = 1f;
                // spawn particle | Dust Particle
                SpawnParticle(collision.ClosestPoint(transform.position), true);
                // adding in the list so bound will be modified
                _spawner.ModifyPlaced(_collider2D, true);
                // spawn next tile
                _spawner.SpawnTetrisTile();
                _spawner.TilePlaced.Raise(_spawner.Player);
            }
            if (collision.gameObject.CompareTag("Water"))
            {
                // spawn particle | Acid Particle
                SpawnParticle(collision.ClosestPoint(transform.position), false);
                // Despawn the object so can be used by the pool again
                PoolManager.Instance.Despawn(gameObject);
                // Raise event so the counter can be increased
                _tileFall.Raise(_spawner.Player);
                // removing in the list so bound will be modified
                _spawner.RemoveTile(this);
                _spawner.ModifyPlaced(_collider2D, false);
            }
        }
    }
}