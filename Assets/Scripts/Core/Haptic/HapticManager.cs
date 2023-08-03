using UnityEngine;

namespace Core
{
    /// <summary>
    /// Following this guy
    /// https://www.youtube.com/watch?v=Pz99rp_hDQs
    /// </summary>
    public class HapticManager
    {
        static HapticManager _instance;

        public static HapticManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new HapticManager();
                    _instance.Init();
                }
                return _instance;
            }
        }

        /// <summary>
        /// Basic feedback impacts
        /// </summary>
        public enum FeedbackImpact
        {
            Light = 50,
            Medium = 100,
            High = 250
        }

        AndroidJavaClass _unityPlayer;
        AndroidJavaObject _currentActivity;
        AndroidJavaObject _hapticObject;

        bool IsAndroid
        {
            get
            {
#if UNITY_ANDROID && !UNITY_EDITOR
                return true;
#else
                return false;
#endif
            }
        }


        void Init()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            _unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
            _currentActivity = _unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
            _hapticObject = _currentActivity.Call<AndroidJavaObject>("getSystemService", "vibrator");
#endif
        }

        /// <summary>
        /// Play haptic feedback base on given impact
        /// Currently only support Android
        /// </summary>
        /// <param name="impact">impact</param>
        public void Haptic(FeedbackImpact impact)
        {
            if (IsAndroid)
            {
                _hapticObject.Call("vibrate", (long)impact);
            }
            else
                Handheld.Vibrate();
        }

        /// <summary>
        /// Cancel the haptic
        /// </summary>
        public void Cancel()
        {
            if (IsAndroid)
                _hapticObject.Call("cancel");
        }
    }
}