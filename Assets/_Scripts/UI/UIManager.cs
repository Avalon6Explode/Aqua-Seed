using UnityEngine;

public class UIManager : MonoBehaviour {

	public void Restart() {

		UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);

	}

	public void Quit() {

		Application.Quit();

	}
}
