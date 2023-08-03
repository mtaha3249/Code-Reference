using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

namespace InputSystem
{
    /// <summary>
    /// Manger hnadle swipe and tap input for the game
    /// </summary>
    public class InputManager : MonoBehaviour
    {
        /// <summary>
        /// Swipe Threshold
        /// </summary>
        [SerializeField, Tooltip("Swipe threshold")]
        private float _threshold = 20f;
        private float _tapInputTime = 0.2f;
        float _currentTapInputTime = 0;

        Vector3 _initialPosition, _endPosition;
        bool _isPressing, _isSwiped, _overUI;

        /// <summary>
        /// Auto null when destroyed no need to unsubscribe
        /// </summary>
        public Action OnSwipeUp, OnSwipeDown, OnSwipeRight, OnSwipeLeft, OnTap;

        static InputManager _instance;
        public static InputManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = new GameObject();
                    go.name = "Input Manager";
                    _instance = go.AddComponent<InputManager>();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        /// <summary>
        /// Vertical Delta change
        /// </summary>
        float VerticalDelta
        {
            get
            {
                return Mathf.Abs(_endPosition.y - _initialPosition.y);
            }
        }

        /// <summary>
        /// Horizontal Delta change
        /// </summary>
        float HorizontalDelta
        {
            get
            {
                return Mathf.Abs(_endPosition.x - _initialPosition.x);
            }
        }

        private void Update()
        {
#if UNITY_EDITOR
            SwipeOnEditor();
#else
            SwipeOnDevice();
#endif
        }

        void SwipeOnDevice()
        {
            foreach (Touch touch in Input.touches)
            {
                // is tapped on UI
                if (EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    _overUI = true;
                }
                if (touch.phase == TouchPhase.Ended && _overUI)
                {
                    // means over UI so return here
                    _overUI = false;
                    _initialPosition = Vector3.zero;
                    _endPosition = Vector3.zero;
                    _isPressing = false;
                    _isSwiped = false;
                    _currentTapInputTime = 0;
                    return;
                }
                else if (touch.phase == TouchPhase.Began && !_isPressing)
                {
                    _initialPosition = touch.position;
                    _endPosition = touch.position;
                    _isPressing = true;
                    _isSwiped = false;
                    _currentTapInputTime = 0;
                }
                else if (touch.phase == TouchPhase.Moved && _isPressing)
                {
                    _endPosition = touch.position;
                    DetectSwipe();
                }
                else if (touch.phase == TouchPhase.Ended)
                {
                    _endPosition = touch.position;
                    DetectSwipe();
                    _isPressing = false;
                    if (_currentTapInputTime <= _tapInputTime)
                        DetectTap();
                }
                _currentTapInputTime += Time.deltaTime;
            }
        }

        /// <summary>
        /// Editor Swipe detection
        /// </summary>
        void SwipeOnEditor()
        {
            // is tapped on UI
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (!_isPressing && Input.GetMouseButtonDown(0))
            {
                _initialPosition = Input.mousePosition;
                _endPosition = Input.mousePosition;
                _isPressing = true;
                _isSwiped = false;
                _currentTapInputTime = 0;
            }

            if (_isPressing)
            {
                _endPosition = Input.mousePosition;
                DetectSwipe();
            }

            if (Input.GetMouseButtonUp(0))
            {
                _endPosition = Input.mousePosition;
                DetectSwipe();
                _isPressing = false;
                if (_currentTapInputTime <= _tapInputTime)
                    DetectTap();
            }
            _currentTapInputTime += Time.deltaTime;
        }

        /// <summary>
        /// Detect Swipe
        /// Core Logic for very simple swipe
        /// </summary>
        void DetectSwipe()
        {
            if (VerticalDelta > _threshold && VerticalDelta > HorizontalDelta)
            {
                // Vertical swipe
                if (_endPosition.y - _initialPosition.y < 0)
                {
                    OnSwipeDown?.Invoke();
                    _isSwiped = true;
                }
                else if (_endPosition.y - _initialPosition.y > 0)
                {
                    OnSwipeUp?.Invoke();
                    _isSwiped = true;
                }
                _initialPosition = _endPosition;
            }
            else if (HorizontalDelta > _threshold && VerticalDelta < HorizontalDelta)
            {
                // Horizontal Swipe
                if (_endPosition.x - _initialPosition.x < 0)
                {
                    OnSwipeLeft?.Invoke();
                    _isSwiped = true;
                }
                else if (_endPosition.x - _initialPosition.x > 0)
                {
                    OnSwipeRight?.Invoke();
                    _isSwiped = true;
                }
                _initialPosition = _endPosition;
            }
        }

        /// <summary>
        /// Detect tap
        /// </summary>
        void DetectTap()
        {
            // check if user earlier did swiped then don't detect tap
            if (!_isPressing && !_isSwiped)
            {
                OnTap?.Invoke();
            }
        }

        private void OnDestroy()
        {
            OnSwipeUp = OnSwipeDown = OnSwipeRight = OnSwipeLeft = OnTap = null;
        }
    }
}