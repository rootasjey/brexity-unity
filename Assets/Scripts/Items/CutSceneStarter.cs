using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class CutSceneStarter : Interactable {
    //Video To Play [Assign from the Editor]
    public VideoClip videoToPlay;
    public VideoClip videoToPlay1;
    public int questStep;
    public bool incrementQuestStep;

    private bool _isIntroSkipped = false;
    private GUIContent _hintSkipText;
    private GUIStyle _fontStyle;

    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private bool _isVideoPlaying { get; set; }

    private Stage2Orchestrator _orchestrator { get; set; }
    public PlayerStats localPlayerStats;

    public override void Interact() {
        _orchestrator = GameObject.Find("Orchestrator")
           .GetComponent<Stage2Orchestrator>();

        if (_orchestrator.StageQuest.Quest.CurrentStep != questStep) {
            return;
        }

        PersistentHUD.instance.gameObject.SetActive(false);

        if (incrementQuestStep) {
            PersistentQuest.instance.ResetTimer();
            _orchestrator.StageQuest.Quest.CompleteNext();
            videoToPlay = videoToPlay1;
        }

        if (_orchestrator.StageQuest.Quest.CurrentStep == 0)
        {
            questStep++;
            _orchestrator.StageQuest.Quest.CompleteNext();
            incrementQuestStep = false;
        }

        _isVideoPlaying = true;

        InitializeFontStyle();

        _orchestrator.Audio.SetActive(false);

        Application.runInBackground = true;

        StartCoroutine(PlayIntro());
        StartCoroutine(HideSkipTextAfterSecs());
    }

    public override void Update() {
        base.Update();
        localPlayerStats = playerStats;
        if (_isVideoPlaying) {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
                StopIntro();
            }
        }
    }

    IEnumerator PlayIntro() {
        var camera = GameObject.Find("Main Camera");

        //Add VideoPlayer to the GameObject
        videoPlayer = camera.GetComponent<VideoPlayer>() ?? camera.AddComponent<VideoPlayer>();
        //videoPlayer = camera.AddComponent<VideoPlayer>();

        //Add AudioSource
        audioSource = camera.GetComponent<AudioSource>() ?? camera.AddComponent<AudioSource>();
        //audioSource = camera.AddComponent<AudioSource>();

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

        _orchestrator.GamePlay.SetActive(false);

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
            font = customFont,
            fontSize = 24,
            normal = new GUIStyleState() { textColor = Color.white }
        };

        _hintSkipText = new GUIContent("PRESS SPACE KEY TO SKIP...");
    }

    private void StopIntro() {
        _isIntroSkipped = true;
        _isVideoPlaying = false;

        PersistentHUD.instance.gameObject.SetActive(true);

        videoPlayer.Stop();

        _orchestrator.GamePlay.SetActive(true);
        _orchestrator.Audio.SetActive(false);
    }

    private IEnumerator HideSkipTextAfterSecs() {
        yield return new WaitForSeconds(5f);
        _isIntroSkipped = true;
    }
}
