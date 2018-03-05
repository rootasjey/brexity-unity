using System.Collections;
using UnityEngine;
using UnityEngine.Video;

namespace Assets.Scripts.Persistent {
    public class Cinematic : MonoBehaviour {
        /* PRIVATE PROPS */
        private VideoPlayer _videoPlayer;
        private AudioSource _audioSource;
        //private VideoClip _videoToPlay;

        private GUIContent _hintSkipText;
        private GUIStyle _fontStyle;
        private Rect _hintRectPosition;

        /* PUBLIC PROPS */
        public static Cinematic Instance { get; set; }

        private string Name { get { return "Cinematic"; } }

        public virtual void Awake() {
            if (Instance == null) {
                Instance = this;

            } else if (Instance != this) {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);

            Instance.name = Name;
        }

        private void Update() {
            if (!_videoPlayer || !_videoPlayer.isPlaying)
                return;

            if (Input.GetKeyDown(KeyCode.Space) || 
                Input.GetKeyDown(KeyCode.Return)) {

                StopIntro();
            }
        }

        private void OnGUI() {
            if (!_videoPlayer || !_videoPlayer.isPlaying || 
                _videoPlayer.time >= 5f) {

                return;
            }

            GUI.Label(_hintRectPosition, _hintSkipText, _fontStyle);
        }

        private void DeactivateSceneElements() {
            // deactivate hud, gameplay world
            HUD.Instance.gameObject.SetActive(false);
        }

        private void ActivateSceneElements() {
            // activate hud, gameplay world
            HUD.Instance.gameObject.SetActive(true);
        }

        public void PlayVideo(VideoClip videoToPlay) {
            Application.runInBackground = true;

            DeactivateSceneElements();
            StartCoroutine(PlayIntro(videoToPlay));
            //StartCoroutine(HideSkipTextAfterSecs());
        }

        private IEnumerator PlayIntro(VideoClip videoToPlay) {
            var camera = GameObject.Find("Main Camera");

            if (camera.GetComponent<VideoPlayer>()) {
                var player = camera.GetComponent<VideoPlayer>();
                if (player) Destroy(player);
            }

            if (camera.GetComponent<AudioSource>()) {
                var audio = camera.GetComponent<AudioSource>();
                if (audio) Destroy(audio);
            }

            _videoPlayer = camera.AddComponent<VideoPlayer>();

            _audioSource = camera.AddComponent<AudioSource>();
            _audioSource.playOnAwake = false;

            _videoPlayer.playOnAwake = false;
            _videoPlayer.source = VideoSource.VideoClip;
            _videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

            _videoPlayer.EnableAudioTrack(0, true);
            _videoPlayer.SetTargetAudioSource(0, _audioSource);

            //Set video To Play then prepare Audio to prevent Buffering
            _videoPlayer.clip = videoToPlay;
            _videoPlayer.Prepare();

            //Wait until video is prepared
            while (!_videoPlayer.isPrepared) {
                yield return null;
            }

            _videoPlayer.Play();
            _audioSource.Play();

            while (_videoPlayer.isPlaying) {
                //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
                yield return null;
            }

            StopIntro();
        }

        private void InitializeFontStyle() {
            _fontStyle = new GUIStyle {
                font = Fonts.GetFont("ScreenMatrix"),
                fontSize = 24,
                normal = new GUIStyleState() { textColor = Color.white }
            };

            _hintSkipText = new GUIContent("PRESS SPACE KEY TO SKIP...");

            _hintRectPosition = new Rect(10, Screen.height - 50, 200, 100);
        }

        private void StopIntro() {
            _videoPlayer.Stop();

            ActivateSceneElements();

            Destroy(_videoPlayer);
            Destroy(_audioSource);

            HUD.Instance.gameObject.SetActive(true);
        }

        private IEnumerator HideSkipTextAfterSecs() {
            yield return new WaitForSeconds(5f);
        }
    }
}
