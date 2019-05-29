using UnityEngine;


public class GameController : MonoBehaviour {

	[SerializeField]
	GameObject player;


	Health health;
	bool isGameOver;
	

	public bool IsGameOver { get { return isGameOver; } }


	void Awake() {

		health = player.GetComponent<Health>();

	}

	void Update() {

		if (health.Current <= 0) {

			SetGameOver(true);

		}

	}

	public void SetGameOver(bool value) {

		isGameOver = value;

	}
}
