using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Spawn Art Background
    /// </summary>
    public class SpawnBackground : MonoBehaviour
    {
        private void Awake()
        {
            if (GameManager.Instance._background == null)
                GameManager.Instance.SpawnBackground();
        }
    }
}