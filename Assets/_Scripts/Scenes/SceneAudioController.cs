using UnityEngine;

public class SceneAudioController : MonoBehaviour {

	[SerializeField]
	bool firstSong;

	[SerializeField]
	bool nextSong;

	[SerializeField]
	bool previousSong;

	[SerializeField]
	bool lastSong;


	AudioPlayer audioPlayer;

	public AudioPlayer AudioPlayer { get { return audioPlayer; } }


	void Awake() {
		audioPlayer = GetComponent<AudioPlayer>();
	}

	void Start() {
		audioPlayer.Play();
	}

	void Update() {
		PlaySoundControl();
	}

	void PlaySoundControl() {
		if (firstSong) {
			audioPlayer.PlayFirstSound();
			firstSong = false;
		}
		else if (nextSong) {
			audioPlayer.PlayNext();
			nextSong = false;
		}
		else if (previousSong) {
			audioPlayer.PlayPrevious();
			previousSong = false;
		}
		
		if (lastSong) {
			audioPlayer.PlayLastSound();
			lastSong = false;
		}
	}
}
