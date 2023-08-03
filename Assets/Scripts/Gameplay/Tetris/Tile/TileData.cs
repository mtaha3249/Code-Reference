using UnityEngine;

namespace Tetris
{
    /// <summary>
    /// Only a data container for the tile
    /// </summary>
    [System.Serializable]
    public class TileData
    {
        [SerializeField]
        private Sprite _sprite;

        private float _gravity;

        public float Gravity
        {
            get => _gravity;
            set => _gravity = value;
        }

        public Sprite Sprite
        {
            get => _sprite;
        }
    }
}