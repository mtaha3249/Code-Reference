using Helper;
using System.Collections;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Background cloud mover
    /// Can do other impotant stuff to handle multiple background
    /// </summary>
    public class Background : MonoBehaviour
    {
        [SerializeField]
        private Transform _clouds;
        [SerializeField]
        private float _cloudMoveSpeed = 1;
        [SerializeField]
        private Vector2 _cloudRange = new Vector2(23, -33);

        Vector3 _startPosition, _endPosition;
        float pingPong = 0;

        private void Start()
        {
            _startPosition = _clouds.localPosition;
            _endPosition = _clouds.localPosition;

            _startPosition.x = _cloudRange.x;
            _endPosition.x = _cloudRange.y;

            CoroutineManager.Instance.RunCoroutine(CloudRoutine());
        }

        /// <summary>
        /// Coroutine to move background as it is more optimal than Update
        /// </summary>
        /// <returns></returns>
        IEnumerator CloudRoutine()
        {
            while (true && _clouds)
            {
                pingPong = Mathf.PingPong(Time.time * _cloudMoveSpeed, 1);
                _clouds.localPosition = Vector3.Lerp(_startPosition, _endPosition, pingPong);
                yield return null;
            }
        }
    }
}