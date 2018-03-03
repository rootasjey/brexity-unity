using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.Helpers {
    public static class Cinematic {

        private static VideoClip _currentVideoPlaying;

        public static VideoClip CurrentVideoPlaying {
            get { return _currentVideoPlaying; }
        }

        private static GameObject _currentCamera;
        public static GameObject CurrentCamera {
            get {
                return _currentCamera;
            }
        }

        public static void PlayVideo(VideoClip videoToPlay) {
            Application.runInBackground = true;

            //StartCoroutine(PlayIntro());
            //StartCoroutine(HideSkipTextAfterSecs());
        }
    }
}
