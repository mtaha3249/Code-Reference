using Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Meter : MonoBehaviour, ISOCallback
    {
        [SerializeField]
        private Vector2 _range;
        [SerializeField]
        private Image _meterPin;
        [SerializeField]
        private Color _badColor, _goodColor, _bestColor;

        [Header("Texts")]
        [SerializeField]
        private TextMeshProUGUI _max;

        float _currentPosition;
        Vector3 _newPosition;

        private void Awake()
        {
            _newPosition = _meterPin.transform.localPosition;
        }

        public void OnEventRaisedCallback(params object[] param)
        {
            _max.text = string.Format("{0} M", (int)param[0]);
            _currentPosition = (float)param[1];
            _currentPosition = MathsFunctions.Remap(_currentPosition, new Vector2(0, (int)param[0]), _range);
            ModifyPinVisualColor((float)param[1], (int)param[0]);
        }

        void ModifyPinVisualColor(float currentHeight, float maxHeight)
        {
            float _intermediateNumber = maxHeight / 3;
            if (currentHeight >= 0 && currentHeight < _intermediateNumber)
            {
                // bad
                _meterPin.color = _badColor;
            }
            else if (currentHeight >= _intermediateNumber && currentHeight <= (_intermediateNumber * 2))
            {
                // good
                _meterPin.color = _goodColor;
            }
            else if (currentHeight > (_intermediateNumber * 2) && currentHeight <= maxHeight)
            {
                // best
                _meterPin.color = _bestColor;
            }
        }

        private void LateUpdate()
        {
            _newPosition.y = Mathf.Lerp(_newPosition.y, _currentPosition, Time.deltaTime);
            _meterPin.transform.localPosition = _newPosition;
        }
    }
}