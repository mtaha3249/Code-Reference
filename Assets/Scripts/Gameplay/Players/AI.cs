using GameDesign;
using Gameplay;
using System.Collections;
using UnityEngine;

namespace AI
{
    /// <summary>
    /// Very AI system doing random decision
    /// </summary>
    public class AI : Player
    {
        /// <summary>
        /// AI decision enum for making code more clean instead of bools
        /// </summary>
        public enum AIDecision
        {
            Pan = 0,
            Rotate = 1,
            Fall = 2,
        }

        [SerializeField]
        private AIDecision _decision;
        [SerializeField, Header("Camera"), Tooltip("Apply given texture to the camera")]
        private Camera _myCamera;
        [SerializeField]
        private RenderTexture _camTexture;

        protected override void Awake()
        {
            base.Awake();
            _spawner.OnSpawned += OnSpawned;
            _myCamera.targetTexture = _camTexture;
        }

        private void OnDestroy()
        {
            _spawner.OnSpawned -= OnSpawned;
        }

        /// <summary>
        /// Called when tile is spawned
        /// </summary>
        void OnSpawned()
        {
            // run bheaviour
            StartCoroutine(RunAIBehaviour());
        }

        /// <summary>
        /// AI Behaviour
        /// </summary>
        /// <returns></returns>
        IEnumerator RunAIBehaviour()
        {
            while (CurrentTile.IsFalling)
            {
                yield return new WaitForSeconds(Random.Range(1, 6));
                // random toss for rotate or pan
                _decision = (AIDecision)Random.Range(0, 3);
                // random toss to number of rotate or pan
                int numberOfSwipes = Random.Range(1, 3);
                // run for all swipes
                for (int x = 0; x <= numberOfSwipes; x++)
                {
                    switch (_decision)
                    {
                        case AIDecision.Pan:
                            // random toss to make right or left
                            bool panRight = Random.Range(0, 2) == 0 ? false : true;
                            if (panRight)
                            {
                                CurrentTile?.Pan(true);
                            }
                            else
                            {
                                CurrentTile?.Pan(false);
                            }
                            break;
                        case AIDecision.Rotate:
                            CurrentTile?.Rotate();
                            break;
                        case AIDecision.Fall:
                            bool canFall = Random.Range(0, 2) == 0 ? false : true;
                            CurrentTile.ModifyGravity(canFall);
                            break;
                    }
                }
                yield return null;
            }
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
                    // means player win and AI failed
                    _levelComplete.Raise();
                }
            }
        }

        /// <summary>
        /// Update AI position if using AI
        /// </summary>
        public override void UpdatePosition()
        {
            transform.position = Vector3.right * 65;
        }

        /// <summary>
        /// Level complete condition for AI which means player loose
        /// </summary>
        /// <param name="bound">bound to check</param>
        public override void CheckComplete(Bounds bound)
        {
            if (bound.size.y >= GameData.Instance.GetCurrentLevel().Win)
            {
                _levelFail.Raise();
            }
        }
    }
}