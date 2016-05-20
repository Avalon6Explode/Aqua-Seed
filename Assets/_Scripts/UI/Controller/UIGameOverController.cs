using UnityEngine;
using UnityEngine.UI;

public class UIGameOverController : MonoBehaviour {

	[SerializeField]
	Text txtTitle;


	Canvas ui;
	Health playerHealth;

	bool isShow;
	bool enableSwith;


	void Initialize() {

		ui = GetComponent<Canvas>();
		playerHealth = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().Player.GetComponent<Health>();

	}

	void Start() {

		Initialize();
		SetEnableUI(false);

	}

	void Update() {

		enableSwith = Input.GetKeyDown(KeyCode.Escape);

		if (playerHealth.Current <= 0) {

				txtTitle.text = "GameOver";
				isShow = true;

		}
		else {
			
			if (enableSwith) {

				isShow = !isShow;

			}

			txtTitle.text = "Aqua Seed";

		}

		SetEnableUI(isShow);

	}

	void SetEnableUI(bool value) {

		if (ui) {

			ui.enabled = value;

		}

	}

}
