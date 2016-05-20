using UnityEngine;

public class AIAgressiveState : AIState {

	[SerializeField]
	Animator anim;


	public AIAgressiveState() : base() {
		stateType = AIState.StateType.AGGRESSIVE;	
	}

	void Update() {
		if (isInUse) {
			Behave();
			AnimationControl();
		}
	}

	public override void Behave() {
		print("Aggressive state in used.");
	}

	void AnimationControl() {

	}
}
