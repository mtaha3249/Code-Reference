using Core;
using GameDesign;
using InputSystem;
using UnityEditor;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// This is the user class, doing action base on user input
    /// </summary>
    public class User : Player
    {
        [Header("Game Events")]
        [SerializeField]
        private SO_Event _countdownStartEvent;

        [Header("Highlighter")]
        [SerializeField]
        private Transform _highlighter;
        [SerializeField]
        private float _highlighterYFollowSpeed = 10;
        float _initialHighlighterOffset = 0;
        Vector3 _highlighterPosition;

        [Header("Sound")]
        [SerializeField]
        private string _placedSound;
        [SerializeField]
        private string _destroyed;
        [SerializeField]
        private string _rotated;

        protected override void Awake()
        {
            base.Awake();
            _countdownStartEvent.Raise();
            InputManager.Instance.OnSwipeRight += OnSwipeRight;
            InputManager.Instance.OnSwipeLeft += OnSwipeLeft;
            InputManager.Instance.OnSwipeDown += OnSwipeDown;
            InputManager.Instance.OnSwipeUp += OnSwipeUp;
            InputManager.Instance.OnTap += OnTap;
            _highlighter.localScale = Vector3.one - Vector3.right;
            _initialHighlighterOffset = _highlighter.position.y;
        }

        void OnSwipeRight()
        {
            CurrentTile?.Pan(true);
        }

        void OnSwipeLeft()
        {
            CurrentTile?.Pan(false);
        }

        void OnSwipeDown()
        {
            CurrentTile?.ModifyGravity(true);
        }

        void OnSwipeUp()
        {
            CurrentTile?.ModifyGravity(false);
        }

        void OnTap()
        {
            CurrentTile?.Rotate();
            // audio when rotated
            AudioManager.Instance.PlayOneShotAudio(_rotated);
        }

        /// <summary>
        /// On Tile fall successfull
        /// </summary>
        /// <param name="args"></param>
        public override void OnTileFall(params object[] args)
        {
            if ((Player)args[0] == this)
            {
                _tileFallIndex++;
                if (_tileFallIndex >= GameData.Instance.GetCurrentLevel().Loose)
                {
                    _levelFail.Raise();
                }

                // give heavy haptic for wrong
                HapticManager.Instance.Haptic(HapticManager.FeedbackImpact.High);
                // play audio
                AudioManager.Instance.PlayOneShotAudio(_destroyed);

                //// give very light haptic
                //HapticManager.Instance.Haptic(HapticManager.FeedbackImpact.Light);
                //// play audio
                //AudioManager.Instance.PlayOneShotAudio(_placedSound);
            }
        }

        public void OnPlaced(params object[] args)
        {
            if ((Player)args[0] == this)
            {
                // give very light haptic
                HapticManager.Instance.Haptic(HapticManager.FeedbackImpact.Light);
                // play audio
                AudioManager.Instance.PlayOneShotAudio(_placedSound);
            }
        }

        /// <summary>
        /// Update position of user base on AI or single player
        /// </summary>
        public override void UpdatePosition()
        {
            transform.position = Vector3.zero;
        }

        /// <summary>
        /// Level complete condition for user
        /// </summary>
        /// <param name="bound">bound to check</param>
        public override void CheckComplete(Bounds bound)
        {
            if (bound.size.y >= GameData.Instance.GetCurrentLevel().Win)
            {
                _levelComplete.Raise();
            }
        }

        protected override void LateUpdate()
        {
            base.LateUpdate();
            UpdateHighlighter();
        }

        /// <summary>
        /// Update Highlighter position
        /// </summary>
        void UpdateHighlighter()
        {
            if (!CurrentTile)
                return;

            _highlighter.localScale = (Vector3.right * CurrentTile.GetBounds().size.x) + Vector3.up;
            _highlighterPosition = _bounds.center == Vector3.zero ? (Vector3.right * CurrentTile.transform.position.x) : (Vector3.right * CurrentTile.transform.position.x);

            _highlighterPosition.y = _bounds.center == Vector3.zero ? Mathf.Lerp(_highlighter.position.y, _initialHighlighterOffset, _highlighterYFollowSpeed * Time.deltaTime) : Mathf.Lerp(_highlighter.position.y, _bounds.center.y + (_bounds.size.y / 2), _highlighterYFollowSpeed * Time.deltaTime);

            _highlighter.position = _highlighterPosition;
        }
    }
}