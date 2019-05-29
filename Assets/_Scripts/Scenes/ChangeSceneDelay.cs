using UnityEngine;

public class ChangeSceneDelay : MonoBehaviour {

	[SerializeField]
	int sceneIndex;

	[SerializeField]
	float delay;


	void Update() {

		Invoke("ChangeScene", delay);

	}

	void ChangeScene() {

		UnityEngine.SceneManagement.SceneManager.LoadScene(sceneIndex);

	}

}
