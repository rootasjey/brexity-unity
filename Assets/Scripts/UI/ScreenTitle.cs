using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class ScreenTitle : MonoBehaviour {
    private GameObject _mainMenu;
    private GameObject _optionsMenu;

    private GameObject _screenTitle;

    //Raw Image to Show Video Images [Assign from the Editor]
    //public RawImage image;

    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;

    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    //Audio
    private AudioSource audioSource;

    private void Start() {
        //_mainMenu = GameObject.Find("MainMenu");
        //_optionsMenu = GameObject.Find("OptionsMenu");

        //_optionsMenu.SetActive(false);

        _screenTitle = GameObject.Find("Menu");
        _screenTitle.SetActive(false);

        Application.runInBackground = true;
        StartCoroutine(PlayIntro());
    }

    private void Update() {
        if (Input.anyKeyDown) {
            StopIntro();
        }
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
            Debug.Log("Preparing Video");
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

    private void StopIntro() {
        videoPlayer.Stop();
        _screenTitle.SetActive(true);
    }

    public void StartGame() {
        SceneManager.LoadScene("Stage2_town");
    }

    public void ShowMainMenu() {
        _mainMenu.SetActive(true);
        _optionsMenu.SetActive(false);
    }

    public void ShowOptionsMenu() {
        _mainMenu.SetActive(false);
        _optionsMenu.SetActive(true);
    }

    public void ShowCredits() {

    }
}
