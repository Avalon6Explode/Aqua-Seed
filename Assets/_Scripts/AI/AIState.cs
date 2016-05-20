using UnityEngine;

public abstract class AIState : MonoBehaviour {

	public enum StateType {

		IDLE,
		NORMAL,
		AGGRESSIVE
	
	}


	[SerializeField]
	protected StateType stateType;


	protected bool isInUse;


	public StateType State { get { return stateType; } }
	public bool IsInUse { get { return isInUse; } }


	public AIState() {

		isInUse = false;

	}

	public void Activate() {

		isInUse = true;
	
	}

	public void Deactivate() {

		isInUse = false;
	
	}

	public abstract void Behave();
}
