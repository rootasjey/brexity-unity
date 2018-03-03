using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class IntroPlayer : MonoBehaviour {
    public GameObject ScreenAfterIntro;
    public GameObject MusicAfterIntro;

    //Raw Image to Show Video Images [Assign from the Editor]
    //public RawImage image;

    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;
    public Font fontScreenMatrix;

    private bool _isIntroSkipped = false;
    private GUIContent _hintSkipText;
    private GUIStyle _fontStyle;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    // Use this for initialization
    void Start () {
        Debug.Log(SceneManager.GetActiveScene().name);
        if (videoToPlay == null /*&& !SceneManager.GetActiveScene().name.Equals("Stage2_sewer")*/) {
            StopIntro();
            return;
        }

        PersistentHUD.instance.gameObject.SetActive(false);
        InitializeFontStyle();

        Application.runInBackground = true;

        StartCoroutine(PlayIntro());
        StartCoroutine(HideSkipTextAfterSecs());
    }

    // Update is called once per frame
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            StopIntro();
        }
    }

    private void OnGUI() {
        if (_isIntroSkipped) return;

        GUI.Label(new Rect(10, Screen.height - 50, 200, 100), _hintSkipText, _fontStyle);
    }

    IEnumerator PlayIntro() {
        var camera = GameObject.Find("Main Camera");

        //Add VideoPlayer to the GameObject
        videoPlayer = camera.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = camera.AddComponent<AudioSource>();

        //Disable Play on Awake for both Video and Audio
        videoPlayer.playOnAwake = false;
        audioSource.playOnAwake = false;

        //We want to play from video clip not from url
        videoPlayer.source = VideoSource.VideoClip;

        //Set Audio Output to AudioSource
        videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;

        //Assign the Audio from Video to AudioSource to be played
        videoPlayer.EnableAudioTrack(0, true);
        videoPlayer.SetTargetAudioSource(0, audioSource);

        //Set video To Play then prepare Audio to prevent Buffering
        videoPlayer.clip = videoToPlay;
        videoPlayer.Prepare();

        //Wait until video is prepared
        while (!videoPlayer.isPrepared) {
            //Debug.Log("Preparing Video");
            yield return null;
        }

        //Debug.Log("Done Preparing Video");

        //Assign the Texture from Video to RawImage to be displayed
        //image.texture = videoPlayer.texture;

        //Play Video
        videoPlayer.Play();

        //Play Sound
        audioSource.Play();

        //Debug.Log("Playing Video");
        while (videoPlayer.isPlaying) {
            //Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
            yield return null;
        }

        StopIntro();
    }

    private void InitializeFontStyle() {
        _fontStyle = new GUIStyle {
            font = fontScreenMatrix,
            fontSize = 24,
            normal = new GUIStyleState() { textColor = Color.white }
        };

        _hintSkipText = new GUIContent("PRESS SPACE KEY TO SKIP...");
    }

    private void StopIntro() {
        _isIntroSkipped = true;

        if (videoPlayer) videoPlayer.Stop();

        PersistentHUD.instance.gameObject.SetActive(true);

        if (ScreenAfterIntro != null) {
            ScreenAfterIntro.SetActive(true);
        }

        if (MusicAfterIntro != null) {
            MusicAfterIntro.SetActive(true);
        }
    }

    private IEnumerator HideSkipTextAfterSecs() {
        yield return new WaitForSeconds(5f);
        _isIntroSkipped = true;
    }
}
