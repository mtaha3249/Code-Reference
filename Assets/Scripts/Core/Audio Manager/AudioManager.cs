using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    /// <summary>
    /// Audio Information for type
    /// </summary>
    [System.Serializable]
    public struct AudioInformation
    {
        public AudioClip _audio;
        public AudioSource _source;
        [Range(0, 1)]
        public float _volume;
    }

    /// <summary>
    /// Input Information
    /// </summary>
    [System.Serializable]
    public struct AudioInputInformation
    {
        public string _name;
        public AudioInformation _information;
    }

    /// <summary>
    /// Manager handles all the audio system
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        static AudioManager _instance;

        public static AudioManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject go = GameObject.Instantiate(Resources.Load<AudioManager>("Audio Manager").gameObject);
                    _instance = go.GetComponent<AudioManager>();
                    _instance.Init();
                    DontDestroyOnLoad(go);
                }
                return _instance;
            }
        }

        public AudioInputInformation[] _inputInformation;

        Dictionary<string, AudioInformation> _audios = new Dictionary<string, AudioInformation>();

        void Init()
        {
            for (int i = 0; i < _inputInformation.Length; i++)
            {
                _audios.Add(_inputInformation[i]._name, _inputInformation[i]._information);
            }
        }

        /// <summary>
        /// Play one shot audio
        /// </summary>
        /// <param name="_name">audio name</param>
        public void PlayOneShotAudio(string _name)
        {
            if (_name == "")
                return;

            _audios[_name]._source.volume = _audios[_name]._volume;
            _audios[_name]._source.PlayOneShot(_audios[_name]._audio);
        }

        /// <summary>
        /// Play loop audio
        /// </summary>
        /// <param name="_name"></param>
        public void PlayLoopAudio(string _name)
        {
            if (_name == "")
                return;

            if (_audios[_name]._source.isPlaying)
                _audios[_name]._source.Play();
        }
    }
}