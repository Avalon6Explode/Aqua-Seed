using UnityEngine;

public class UIGameOverController : MonoBehaviour {

	Canvas ui;
	Health playerHealth;


	void Initialize() {

		ui = GetComponent<Canvas>();
		playerHealth = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().Player.GetComponent<Health>();

	}

	void Start() {

		Initialize();
		SetEnableUI(false);

	}

	void Update() {

		if (playerHealth && playerHealth.Current <= 0) {

			SetEnableUI(true);

		}

	}

	void SetEnableUI(bool value) {

		if (ui) {

			ui.enabled = value;

		}

	}

}
