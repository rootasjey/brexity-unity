using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class ScreenTitleOrchestrator : MonoBehaviour {
    //Raw Image to Show Video Images [Assign from the Editor]
    //public RawImage image;

    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;
    public Font fontScreenMatrix;
    
    private GameObject _backgroundMusic;

    private bool _isIntroSkipped = false;
    private GUIContent _hintSkipText;
    private GUIStyle _fontStyle;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private GameObject _mainMenu, _settingsMenu;

    private void Start() {
        InitializeComponents();
        InitFontStyle();

        Application.runInBackground = true;

        StartCoroutine(PlayIntro());
        StartCoroutine(HideSkipTextAfterSecs());
    }

    private void InitializeComponents() {
        _mainMenu = GameObject.Find("MainMenu");
        _mainMenu.SetActive(false);

        _backgroundMusic = GameObject.Find("BackgroundMusic");
        _backgroundMusic.SetActive(false);

        _settingsMenu = GameObject.Find("SettingsMenu");
        _settingsMenu.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
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

        //Debug.Log("Done Playing Video");

        StopIntro();
    }

    private void InitFontStyle() {
        _fontStyle = new GUIStyle {
            font = fontScreenMatrix,
            fontSize = 24,
            normal = new GUIStyleState() { textColor = Color.white }
        };

        _hintSkipText = new GUIContent("PRESS SPACE KEY TO SKIP...");
    }

    private void StopIntro() {
        _isIntroSkipped = true;

        videoPlayer.Stop();

        _mainMenu.SetActive(true);
        _backgroundMusic.SetActive(true);
    }

    private IEnumerator HideSkipTextAfterSecs() {
        yield return new WaitForSeconds(5f);
        _isIntroSkipped = true;
    }

    public GameObject GetMainMenu() {
        return _mainMenu;
    }

    public GameObject GetSettingsMenu() {
        return _settingsMenu;
    }
}
