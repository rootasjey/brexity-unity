using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenTitle : MonoBehaviour {
    //Raw Image to Show Video Images [Assign from the Editor]
    //public RawImage image;

    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;
    public Font fontScreenMatrix;

    private GameObject _screenTitle;

    private bool _isIntroSkipped = false;
    private GUIContent _hintSkipText;
    private GUIStyle _fontStyle;

    private int _cursorMenuIndex = 0;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audioSource;

    private void Start() {
        _screenTitle = GameObject.Find("Menu");
        _screenTitle.SetActive(false);

        InitFontStyle();

        Application.runInBackground = true;

        StartCoroutine(PlayIntro());
        StartCoroutine(HideSkipTextAfterSecs());
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
        //Add VideoPlayer to the GameObject
        videoPlayer = gameObject.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = gameObject.AddComponent<AudioSource>();

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

        _screenTitle.SetActive(true);
    }

    private IEnumerator HideSkipTextAfterSecs() {
        yield return new WaitForSeconds(5f);
        _isIntroSkipped = true;
    }

    public void StartGame() {
        SceneManager.LoadScene("Stage2_town");
    }

    public void ExitGame() {
        ExitGame();
    }

    //public void ShowMainMenu() {
    //    _mainMenu.SetActive(true);
    //    _optionsMenu.SetActive(false);
    //}

    //public void ShowSettingsMenu() {
    //    _mainMenu.SetActive(false);
    //    _optionsMenu.SetActive(true);
    //}

    //public void ShowCredits() {

    //}
}
