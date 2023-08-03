using Core;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Gameplay
{
    /// <summary>
    /// Countdown sequence comes in the start of the game
    /// </summary>
    public class CountdownSequence : MonoBehaviour
    {
        [SerializeField]
        private SO_Event _countEnd;
        [SerializeField]
        private TextMeshProUGUI _text;

        WaitForSeconds _primaryDelay;
        WaitForSeconds _secondaryDelay;

        /// <summary>
        /// Main call for start countdown handle by SO-Listener
        /// </summary>
        public void StartCountdown()
        {
            _primaryDelay = new WaitForSeconds(1.5f);
            _secondaryDelay = new WaitForSeconds(0.2f);
            StartCoroutine(Countdown());
        }

        /// <summary>
        /// Countdown sequence
        /// wait
        /// show text
        /// wait
        /// show text
        /// </summary>
        /// <returns></returns>
        IEnumerator Countdown()
        {
            yield return _secondaryDelay;
            _text.text = "" + 3;
            _text.gameObject.SetActive(true);
            yield return _primaryDelay;
            _text.gameObject.SetActive(false);
            yield return _secondaryDelay;
            // now show 2
            _text.text = "" + 2;
            _text.gameObject.SetActive(true);
            yield return _primaryDelay;
            _text.gameObject.SetActive(false);
            yield return _secondaryDelay;
            // now show 1
            _text.text = "" + 1;
            _text.gameObject.SetActive(true);
            yield return _primaryDelay;
            _text.gameObject.SetActive(false);
            yield return _secondaryDelay;
            // event raised when countdown sequence end
            _countEnd.Raise();
        }
    }
}