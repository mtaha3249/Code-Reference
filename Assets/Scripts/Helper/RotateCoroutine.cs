using System.Collections;
using UnityEngine;

namespace Helper
{
    /// <summary>
    /// Extension function for the rotating object
    /// </summary>
    public static class RotateCoroutine
    {
        /// <summary>
        /// Rotate by given speed
        /// </summary>
        /// <param name="t"></param>
        /// <param name="speed"></param>
        public static void Rotate(this Transform t, float speed, Vector3 target)
        {
            CoroutineManager.Instance.StartCoroutine(RotateRoutine(t, speed, target));
        }

        /// <summary>
        /// Rotate by given speed to the given target
        /// </summary>
        /// <param name="transform"></param>
        /// <param name="speed"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        static IEnumerator RotateRoutine(Transform transform, float speed, Vector3 target)
        {
            Quaternion from = transform.rotation;
            Quaternion to = Quaternion.Euler(target);
            float t = 0f;
            while (t <= 1f)
            {
                // a unique edge case, when game replayed
                if (transform)
                    transform.rotation = Quaternion.Slerp(from, to, t);
                else
                    CoroutineManager.Instance.StopCoroutine();

                t += Time.deltaTime / speed;
                yield return null;
            }
            transform.rotation = to;
        }
    }
}