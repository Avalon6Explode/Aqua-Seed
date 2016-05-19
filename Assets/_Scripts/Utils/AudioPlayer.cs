using UnityEngine;

public class AudioPlayer : MonoBehaviour {

	[SerializeField]
	protected AudioClip soundEffect;
	
	[SerializeField]
	[Range(0.0f, 1.0f)]
	protected float soundVolume;

	[SerializeField]
	protected int maxAudioSource;

	[SerializeField]
	protected bool isLoopSound;


	protected AudioSource[] audioSource;


	protected void Awake() {
		InitAudioSource();
	}

	protected void InitAudioSource() {
		
		for (int i = 0; i < maxAudioSource; i++) {
			gameObject.AddComponent<AudioSource>();
		}		

		audioSource = GetComponents<AudioSource>();
		
		for (int i = 0; i < audioSource.Length; i++) {
			audioSource[i].clip = soundEffect;
			audioSource[i].loop = isLoopSound;
			audioSource[i].playOnAwake = false;
		}
	}

	public virtual void Play() {

		AudioSource selectedSource = null;

		for (int i = 0; i < audioSource.Length; i++) {
			if (!audioSource[i].isPlaying) {
				selectedSource = audioSource[i];
				break;
			}
		}

		if (selectedSource) {
			selectedSource.PlayOneShot(selectedSource.clip, soundVolume);
		}
	}
}
