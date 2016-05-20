using System;
using UnityEngine;

[RequireComponent(typeof (Animator))]
[RequireComponent(typeof (CircleCollider2D))]
public class AIStateController : MonoBehaviour {

	[Serializable]
	public class MultiAIState {

		public AIState aiState;
	
	}

	[SerializeField]
	Enemy enemy;

	[SerializeField]
	float delayStartingNormalState;

	[SerializeField]
	MultiAIState[] states;

	
	bool isSeenPlayer;
	bool isPickState;


	AIState.StateType[] referStateArray;
	AIState.StateType currentUseState;
	AIState.StateType previousUseState;


	public AIState.StateType CurrentUseState { get { return currentUseState; } }
	public AIState.StateType PrviousUseState { get { return previousUseState; } }


	public AIStateController() {

		states = new MultiAIState[3];
	
	}

	void Initialize() {

		isSeenPlayer = false;
		isPickState = false;

		referStateArray = new AIState.StateType[] { 

			AIState.StateType.IDLE,
			AIState.StateType.NORMAL, 
			AIState.StateType.AGGRESSIVE
		
		};

		currentUseState = AIState.StateType.IDLE;
		previousUseState = AIState.StateType.IDLE;

	}

	void Awake() {

		Initialize();	
	
	}

	void Start() {

		Use(AIState.StateType.IDLE);

	}

	void Update() {

		ChangeStateControl();

	}

	void OnTriggerEnter2D(Collider2D col) {

		isSeenPlayer = col.gameObject.tag == "Player";

	}

	void Use(AIState.StateType state) {
		
		var selectedState = GetState(state);
		
		if (selectedState) {
			
			DeactivateAllState();
			UpdateState(state);
			selectedState.Activate();
		
		}

	}

	void DeactivateAllState() {
		
		for (int i = 0; i < states.Length; i++) {

			var selectedState = states[i].aiState;

			if (selectedState) {
				selectedState.Deactivate();
			}

		}

	}

	void UpdateState(AIState.StateType newState) {
		
		var oldState = currentUseState;
		currentUseState = newState;

		if (oldState != currentUseState) {

			previousUseState = oldState;
		
		}

	}

	AIState GetState(AIState.StateType state) {
		
		AIState selectedState = null;

		for (int i = 0; i < states.Length; i++) {

			selectedState = states[i].aiState;

			if (selectedState && selectedState.State == state) {

				break;
			
			}
		}

		return selectedState;

	}

	AIState.StateType GetRandomStateType() {
		
		var result = AIState.StateType.IDLE;
		var randomNum = UnityEngine.Random.Range(0, states.Length);
		var selectedState = states[randomNum].aiState;

		if (selectedState) {

			result = selectedState.State;
		
		}

		return result;
	
	}

	AIState.StateType GetRandomStateTypeExcept(AIState.StateType exceptState) {

		var result = GetRandomStateType();
		var resultIndex = 0;

		if (result == exceptState) {

			var selectedStateIndex = Array.IndexOf(referStateArray, result);

			if (selectedStateIndex + 1 > referStateArray.Length - 1) {

				var appoximateError = UnityEngine.Random.Range(-1, 2);

				if (appoximateError > 0) {
					
					resultIndex = selectedStateIndex - 1;
				
				}
				else {

					resultIndex = 0;
				
				}

			}

			result = referStateArray[resultIndex];

		}

		return result;
	}


	void ChangeStateControl() {

		if (enemy.IsInHurt || isSeenPlayer) {

			Use(AIState.StateType.AGGRESSIVE);
		
		}
		else {

			if (!isPickState) {

				var pickState = GetRandomStateTypeExcept(AIState.StateType.AGGRESSIVE);

				if (pickState == AIState.StateType.IDLE) {

					Invoke("ForceToNormalState", delayStartingNormalState);

				}

				Use(pickState);
				isPickState = true;

			}
		}

	}

	void ChangeStateTo(AIState.StateType state) {
		
		Use(state);
	
	}

	void ForceToNormalState() {
	
		Use(AIState.StateType.NORMAL);
	
	}
}
