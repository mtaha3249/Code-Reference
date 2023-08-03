using Helper;
using UnityEngine;

namespace UI
{

    /// <summary>
    /// Button to make scene switch
    /// </summary>
    public class Btn_SceneSwitch : Btn_Base
    {
        /// <summary>
        /// Scene Switch Type
        /// </summary>
        [System.Serializable]
        private enum SwitchType
        {
            NewScene = 0,
            Reload = 1
        }

        [SerializeField]
        private SwitchType _switchType;
        [SerializeField, Tooltip("Scene to load Index. Applicable if switchType is NewScene")]
        private int _sceneIndex = 0;
        [SerializeField, Tooltip("Toogle timescale, if 0 then 1 and vice versa")]
        private bool _toggleTimeScale = false;

        public override void OnClick()
        {
            switch (_switchType)
            {
                case SwitchType.NewScene:
                    SwitchScene.LoadScene(_sceneIndex);
                    break;
                case SwitchType.Reload:
                    SwitchScene.LoadCurrentScene();
                    break;
            }
            if (_toggleTimeScale)
                Time.timeScale = Time.timeScale == 0 ? 1 : 0;
        }
    }
}