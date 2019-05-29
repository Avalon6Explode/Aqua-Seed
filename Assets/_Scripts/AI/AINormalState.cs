using UnityEngine;

public class AINormalState : AIState {

	enum ExploreType {

		HORIZONTAL,
		VERTICAL

	}

	enum MoveState {

		NONE,
		UP,
		DOWN,
		LEFT,
		RIGHT
	}

	[SerializeField]
	Animator anim;

	[SerializeField]
	Rigidbody2D rigid;

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float delayToStartMovingAgain;

	[SerializeField]
	float hitWallCheckLength;

	[SerializeField]
	LayerMask hitWallCheckLayer;


	ExploreType exploreType;
	
	bool isWalking;
	bool isFacingRight;
	bool reInitPickDirection;

	Ray2D rayUp;
	Ray2D rayDown;
	Ray2D rayLeft;
	Ray2D rayRight;

	RaycastHit2D hitUp;
	RaycastHit2D hitDown;
	RaycastHit2D hitLeft;
	RaycastHit2D hitRight;

	Vector3 moveDirection;
	MoveState currentMoveState;


	public AINormalState() : base() {
		
		stateType = AIState.StateType.NORMAL;
	
	}

	void Initialize() {

		moveDirection = Vector3.zero;
		currentMoveState = MoveState.NONE;

		exploreType = (UnityEngine.Random.Range(-1, 2) > 0) ? ExploreType.HORIZONTAL : ExploreType.VERTICAL; 

		isWalking = false;
		isFacingRight = false;
		reInitPickDirection = false;

		rayUp = new Ray2D();
		rayDown = new Ray2D();
		rayLeft = new Ray2D();
		rayRight = new Ray2D();

		rayUp.direction = Vector3.up;
		rayDown.direction = Vector3.down;
		rayLeft.direction = Vector3.left;
		rayRight.direction = Vector3.right;

	}

	void Awake() {

		Initialize();

	}

	void FixedUpdate() {

		if (isInUse) {

			MoveBehave();

		}

	}

	void Update() {

		if (isInUse) {

			Behave();
		
		}

	}

	public override void Behave() {

		BeginMoving();
		PickDirection();
		ChangeMoveDirectionControl();
		UpdateAnimationState();
		AnimationControl();

	}

	void MoveBehave() {

		MovementControl();
		UpdateRayOrigin();
		UpdateRaycastHit();

	}

	void PickDirection() {

		if (reInitPickDirection) {

			switch (exploreType) {

				case ExploreType.HORIZONTAL :
				
					if (currentMoveState != MoveState.LEFT || currentMoveState != MoveState.RIGHT) {

						if (UnityEngine.Random.Range(-1, 2) < 0) {

							SetMoveState(MoveState.LEFT);

						}
						else {

							SetMoveState(MoveState.RIGHT);

						}

						reInitPickDirection = false;

					}

					break;

				case ExploreType.VERTICAL :

					if (currentMoveState != MoveState.UP || currentMoveState != MoveState.DOWN) {

						if (UnityEngine.Random.Range(-1, 2) > 0) {

							SetMoveState(MoveState.UP);

						}
						else {

							SetMoveState(MoveState.DOWN);

						}

						reInitPickDirection = false;

					}
			
					break;
					
			}
		}
	}

	void BeginMoving() {

		if (currentMoveState == MoveState.NONE) {

			currentMoveState = GetRandomMoveState();
			reInitPickDirection = true;
		
		}

	}

	void MovementControl() {

		switch (currentMoveState) {

			case MoveState.NONE :

				moveDirection = Vector3.zero;
			
				break;

			case MoveState.UP :

				moveDirection = Vector3.up;
			
				break;

			case MoveState.DOWN :

				moveDirection = Vector3.down;

				break;

			case MoveState.LEFT :

				moveDirection = Vector3.left;
			
				break;

			case MoveState.RIGHT :

				moveDirection = Vector3.right;
			
				break;

		}

		rigid.AddForce(moveDirection * moveSpeed, ForceMode2D.Force);
	
	}


	void ChangeMoveDirectionControl() {

		switch (exploreType) {

			case ExploreType.HORIZONTAL :
				
				if (hitLeft) {

					SetMoveState(MoveState.RIGHT);

				}
				
				if (hitRight) {

					SetMoveState(MoveState.LEFT);

				}

				break;

			case ExploreType.VERTICAL :
				
				if (hitUp) {

					SetMoveState(MoveState.DOWN);

				}
				
				if (hitDown) {

					SetMoveState(MoveState.UP);

				}

				break;

		}
	
	}

	void SetMoveState(MoveState state) {

		currentMoveState = state;
	
	}

	MoveState GetRandomMoveState() {

		var randomNum = Random.Range(0, 5);
		var moveState = MoveState.NONE;

		switch (randomNum) {

			case 0 :

				moveState = MoveState.NONE;
				
				break;

			case 1 :

				moveState = MoveState.UP;

				break;

			case 2 :

				moveState = MoveState.DOWN;

				break;

			case 3 :

				moveState = MoveState.LEFT;

				break;

			case 4 :

				moveState = MoveState.RIGHT;

				break;

		}

		return moveState;

	}

	void UpdateAnimationState() {

		isWalking = (currentMoveState != MoveState.NONE);
		isFacingRight = (currentMoveState == MoveState.RIGHT);

	}

	void UpdateRayOrigin() {

		rayUp.origin = transform.position;
		rayDown.origin = transform.position;
		rayLeft.origin = transform.position;
		rayRight.origin = transform.position;

	}

	void UpdateRaycastHit() {

		hitUp = Physics2D.Raycast(rayUp.origin, rayUp.direction, hitWallCheckLength, hitWallCheckLayer);
		hitDown = Physics2D.Raycast(rayDown.origin, rayDown.direction, hitWallCheckLength, hitWallCheckLayer);
		hitLeft = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, hitWallCheckLength, hitWallCheckLayer);
		hitRight = Physics2D.Raycast(rayRight.origin, rayRight.direction, hitWallCheckLength, hitWallCheckLayer);
	
	}

	void AnimationControl() {

		anim.SetBool("Walking", isWalking);
		anim.SetBool("FacingRight", isFacingRight);

	}
	
}
