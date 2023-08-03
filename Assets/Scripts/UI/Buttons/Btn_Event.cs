using Core;
using UnityEngine;

namespace UI
{
    public class Btn_Event : Btn_Base
    {
        [SerializeField, TextArea(6, 10)]
        private string _comment = "Raise given event with given bool value";
        /// <summary>
        /// event
        /// </summary>
        [SerializeField]
        private SO_Event _event;
        [SerializeField]
        private bool _value;
        public override void OnClick()
        {
            _event.Raise(_value);
        }
    }
}