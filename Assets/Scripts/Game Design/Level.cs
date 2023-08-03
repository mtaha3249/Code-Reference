using UnityEngine;

namespace GameDesign
{
    /// <summary>
    /// Each Level information container
    /// </summary>
    [CreateAssetMenu(fileName = "Level-NO", menuName = "Tetris/Game Design/Create Level", order = 1)]
    public class Level : ScriptableObject
    {
        [SerializeField]
        private bool _useAI;
        [SerializeField]
        private int _win = 40;
        [SerializeField]
        private int _loose = 5;
        [SerializeField, Range(0, 10)]
        private float _gravity = 9.8f;
        [SerializeField, Range(0, 20)]
        private float _maxGravity = 9.8f;
        [SerializeField]
        private GameObject[] _tetris;

        public bool HasAI
        {
            get => _useAI;
        }

        public int Win
        {
            get => _win;
        }

        public int Loose
        {
            get => _loose;
        }

        public float Gravity
        {
            get => _gravity;
        }

        public float MaxGravity
        {
            get => _maxGravity;
        }

        public GameObject[] Tetris
        {
            get => _tetris;
        }
    }
}