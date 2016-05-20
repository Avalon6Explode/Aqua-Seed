using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	[SerializeField]
	[Range(0, 10)]
	protected int maxAudioSource;

	[SerializeField]
	[Range(0.0f, 1.0f)]
	protected float soundVolume;

	[SerializeField]
	protected ulong startDelay;

	[SerializeField]
	protected AudioClip[] sounds;

	[SerializeField]
	protected bool isPlayOnAwake;

	[SerializeField]
	protected bool isLoopSound;


	protected int currentSoundIndex;
	protected AudioSource[] audioSource;


	public AudioClip[] Sounds { get { return sounds; } }
	

	public AudioPlayer() {
		sounds = new AudioClip[1];
	}

	protected void Awake() {
		InitAudioSource();
	}

	protected void InitAudioSource() {
		
		for (int i = 0; i < maxAudioSource; i++) {
			gameObject.AddComponent<AudioSource>();
		}		

		audioSource = GetComponents<AudioSource>();
		
		for (int i = 0; i < audioSource.Length; i++) {
			audioSource[i].clip = sounds[0];
			audioSource[i].loop = isLoopSound;
			audioSource[i].playOnAwake = isPlayOnAwake;
			audioSource[i].volume = soundVolume;
		}
	}

	public virtual void Play() {
		
		var selectedSource = GetAvailableSource();

		if (selectedSource) {
			selectedSource.Play(startDelay);
		}
	}

	public void PlayOnce() {

		var selectedSource = GetAvailableSource();

		if (selectedSource) {
			selectedSource.PlayOneShot(selectedSource.clip, soundVolume);
		}
	}

	public void PlayFirstSound() {
		SetSound(0);
		ForcePlay();
	}

	public void PlayLastSound() {
		SetSound(sounds.Length - 1);
		ForcePlay();
	}

	public void PlayNext() {
		var nextSound = currentSoundIndex + 1;
		nextSound = (nextSound > sounds.Length - 1) ? 0 : nextSound;
		SetSound(nextSound);
		ForcePlay();
	}

	public void PlayPrevious() {
		var previousSound = currentSoundIndex - 1;
		previousSound = (previousSound < 0) ? sounds.Length - 1 : previousSound;
		SetSound(previousSound);
		ForcePlay();
	}

	public void ForcePlay() {
		Stop();
		Play();
	}

	public void Stop() {
		for (int i = 0; i < maxAudioSource; i++) {
			var selectedSource = audioSource[i];
			selectedSource.Stop();
		}
	}

	public void SetSound(int index) {

		var selectedSoundIndex = (index > sounds.Length) ? sounds.Length : index;
		selectedSoundIndex = (selectedSoundIndex < 0) ? 0 : selectedSoundIndex;

		for (int i = 0; i < audioSource.Length; i++) {
			var selectedSource = audioSource[i];
			selectedSource.clip = sounds[selectedSoundIndex];
		}

		currentSoundIndex = selectedSoundIndex;
	}

	public void SetStartDelay(ulong value) {
		startDelay = value;
	}

	public AudioSource GetAvailableSource() {
		
		AudioSource selectedSource = null;

		for (int i = 0; i < audioSource.Length; i++) {
			if (!audioSource[i].isPlaying) {
				selectedSource = audioSource[i];
				break;
			}
		}

		return selectedSource;
	}
}
