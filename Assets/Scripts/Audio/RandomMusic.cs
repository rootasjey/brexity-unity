using UnityEngine;

public class RandomMusic : MonoBehaviour {
    public AudioClip[] musics;

    private AudioSource _audioSource;
    private AudioClip _backgroundMusic;

	// Use this for initialization
	void Start () {
        _audioSource = gameObject.GetComponent<AudioSource>();

        int index = Random.Range(0, musics.Length);

        _backgroundMusic = musics[index];
        _audioSource.clip = _backgroundMusic;
        _audioSource.Play();
	}
}
