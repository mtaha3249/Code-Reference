using Core;
using System.Collections;
using UnityEngine;

namespace Helper
{
    public class CameraShake : MonoBehaviour, ISOCallback
    {
        [SerializeField]
        private float _shakeTime;
        [SerializeField]
        private float _shakeSpeed;
        [SerializeField]
        private float _shakeFrequency;

        private float _cachedShakeTime;
        Vector3 _targetShakePosition;

        public void OnEventRaisedCallback(params object[] param)
        {
            _cachedShakeTime = _shakeTime;
            CoroutineManager.Instance.RunCoroutine(ShakeCamera());
        }

        /// <summary>
        /// Shake camera routine
        /// </summary>
        /// <returns></returns>
        IEnumerator ShakeCamera()
        {
            while (_cachedShakeTime >= 0 && Time.timeScale != 0)
            {
                _targetShakePosition = transform.localPosition + (Random.insideUnitSphere * _shakeFrequency);
                _targetShakePosition.z = 0;
                transform.localPosition = Vector3.Lerp(transform.localPosition, _targetShakePosition, _shakeSpeed * Time.deltaTime);
                _cachedShakeTime -= Time.deltaTime;
                yield return null;
            }

            transform.localPosition = Vector3.zero;
        }
    }
}