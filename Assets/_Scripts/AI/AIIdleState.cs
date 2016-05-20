using UnityEngine;

public class AIIdleState : AIState {

	[SerializeField]
	Animator anim;


	public AIIdleState() : base() {

		stateType = AIState.StateType.IDLE;
	
	}

	void Update() {

		if (isInUse) {
			
			Behave();
		
		}

	}

	public override void Behave() {
		AnimationControl();
	}

	void AnimationControl() {

		anim.SetBool("Walking", false);
		anim.SetBool("Biting", false);
		anim.SetBool("Attack", false);
	
	}
}
