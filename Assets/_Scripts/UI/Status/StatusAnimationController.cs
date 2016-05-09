using UnityEngine;

public class StatusAnimationController : MonoBehaviour {

	enum StatusType {
		NONE,
		HEALTH,
		STAMINA,
		ENERGY
	}

	//Debug Only
	[SerializeField]
	GameObject player;

	[SerializeField]
	StatusType type;


	Animation anim;
	AnimationState state;
	
	Health health;
	Stamina stamina;
	Energy energy;


	public StatusAnimationController() {
		health = null;
		stamina = null;
		energy = null;
	}

	void Awake() {
		anim = GetComponent<Animation>();
		foreach (AnimationState state in anim) {
			this.state = state;
		}
		this.state.speed = 0.0f;
		anim.Play();

		switch (type) {
			case StatusType.HEALTH :
				health = player.gameObject.GetComponent<Health>();
			break;

			case StatusType.STAMINA :
				stamina = player.gameObject.GetComponent<Stamina>();
			break;

			case StatusType.ENERGY :
				energy = player.gameObject.GetComponent<Energy>();
			break;
		}
	}

	void Update() {
		switch (type) {
			case StatusType.HEALTH :
				state.time = health.Current * 0.01f;
			break;

			case StatusType.STAMINA :
				state.time = stamina.Current * 0.01f;
			break;

			case StatusType.ENERGY :
				state.time = energy.Current * 0.01f;
			break;
		}
	}
}
