using UnityEngine;

public class MuteSound : MonoBehaviour {

	[SerializeField]
	AudioPlayer[] sounds;


	public void Mute() {

		for (int i = 0; i < sounds.Length; i++) {

			sounds[i].Mute();

		}

	}

	public void UnMute() {

		for (int i = 0; i < sounds.Length; i++) {

			sounds[i].UnMute();

		}

	}
}
