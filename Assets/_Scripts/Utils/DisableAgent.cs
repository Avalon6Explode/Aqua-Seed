using UnityEngine;

public class DisableAgent : MonoBehaviour {

	[SerializeField]
	float disableDelay;


	float timeToDisable;
	bool isInitDisable;
	bool isStartDisable;


	public DisableAgent() {
		isStartDisable = false;
		isInitDisable = false;
		timeToDisable = 0.0f;
	}

	void Update() {
		if (!isInitDisable) {
			if (isStartDisable) {
				timeToDisable = Time.time + disableDelay;
				isInitDisable = true;
			}
		}
		else {
			if (isStartDisable && Time.time > timeToDisable) {
				gameObject.SetActive(false);
				isStartDisable = false;
				isInitDisable = false;
			}
		}
	}

	public void StartDisable() {
		isStartDisable = true;
	}
}
