using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Utility script for automatic aligning of attached object
    /// </summary>
    [ExecuteInEditMode]
    public class AutoPosition : MonoBehaviour
    {
        public enum PositionType
        {
            TopCenter = 0,
            BottomCenter = 1,
            Bottom
        }

        [SerializeField]
        private PositionType _positionType = PositionType.BottomCenter;

        private void Awake()
        {
            switch (_positionType)
            {
                case PositionType.TopCenter:
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height, 0));
                    break;
                case PositionType.BottomCenter:
                    transform.position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0));
                    break;
                case PositionType.Bottom:
                    Vector3 currentPosition = transform.position;
                    currentPosition.y = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, 0, 0)).y;
                    transform.position = currentPosition;
                    break;
            }
        }

#if UNITY_EDITOR
        private void Update()
        {
            Awake();
        }
#endif
    }
}