using UnityEngine;

public class BulletAudioPlayer : MonoBehaviour {

	[SerializeField]
	AudioClip soundEffect;
	
	[SerializeField]
	[Range(0.0f, 1.0f)]
	protected float soundVolume;

	[SerializeField]
	int maxAudioSource;

	[SerializeField]
	protected bool isLoopSound;


	AudioSource[] audioSource;


	void Awake() {
		InitAudioSource();
	}

	void InitAudioSource() {
		
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

	public void PlaySoundEffect() {

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
