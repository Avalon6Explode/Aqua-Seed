using UnityEngine;

public class AIAgressiveState : AIState {

	enum AttackType {

		NONE,
		BITE,
		CLAW

	}


	[SerializeField]
	Enemy enemy;

	[SerializeField]
	Animator anim;

	[SerializeField]
	Rigidbody2D rigid;

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float attackRate;

	[SerializeField]
	float hitWallCheckLength;

	[SerializeField]
	LayerMask hitWallCheckLayer;
	
	[SerializeField]
	float hitPlayerCheckLength;

	[SerializeField]
	LayerMask hitPlayerCheckLayer;


	bool isWalking;
	bool isFacingRight;

	bool isAttackByBite;
	bool isAttackByClaw;

	float nextAttack;

	GameObject player;

	Ray2D rayTarget;
	RaycastHit2D hitTarget;

	AttackType currentAttackType;
	Vector3 moveTargetPos;

	Ray2D rayUp;
	Ray2D rayDown;
	Ray2D rayLeft;
	Ray2D rayRight;

	RaycastHit2D hitUp;
	RaycastHit2D hitDown;
	RaycastHit2D hitLeft;
	RaycastHit2D hitRight;


	public AIAgressiveState() : base() {

		stateType = AIState.StateType.AGGRESSIVE;	
	
	}

	void Initialize() {

		isWalking = false;
		isFacingRight = false;
		isAttackByBite = false;
		isAttackByClaw = false;

		rayTarget = new Ray2D();
		rayTarget.direction = Vector3.zero;

		rayUp = new Ray2D();
		rayDown = new Ray2D();
		rayLeft = new Ray2D();
		rayRight = new Ray2D();

		rayUp.direction = Vector3.up;
		rayDown.direction = Vector3.down;
		rayLeft.direction = Vector3.left;
		rayRight.direction = Vector3.right;

		player = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().Player;

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

		UpdateMoveTargetPosition();
		UpdateAttackType();
		AnimationControl();
	
	}

	void MoveBehave() {

		UpdateRayOrigin();
		UpdateRayTargetDirection();
		UpdateRaycastHit();
		MovementControl();
		AttackTargetControl();

	}

	void MovementControl() {

		if (hitTarget && player) {

			if (hitTarget.transform.gameObject.tag == "Player") {

				moveTargetPos.Normalize();
				rigid.AddForce(moveTargetPos * moveSpeed);

			}
			else {

				if (player.transform.position.x > transform.position.x) {

					rigid.AddForce(Vector3.right * moveSpeed, ForceMode2D.Force);

				}
				else if (player.transform.position.x < transform.position.x) {

					rigid.AddForce(Vector3.left * moveSpeed, ForceMode2D.Force);

				}

				if (hitRight && !hitUp && player.transform.position.y >= transform.position.y) {

					rigid.AddForce(Vector3.left * moveSpeed * 2.0f, ForceMode2D.Force);
					rigid.AddForce(Vector3.up * moveSpeed * 2.0f, ForceMode2D.Force);

				}

				if (hitRight && !hitDown && player.transform.position.y <= transform.position.y) {

					rigid.AddForce(Vector3.left * moveSpeed * 2.0f, ForceMode2D.Force);
					rigid.AddForce(Vector3.down * moveSpeed * 2.0f, ForceMode2D.Force);

				}


				if (hitLeft && !hitUp && player.transform.position.y <= transform.position.y) {

					rigid.AddForce(Vector3.right * moveSpeed * 2.0f, ForceMode2D.Force);
					rigid.AddForce(Vector3.up * moveSpeed * 2.0f, ForceMode2D.Force);

				}

				if (hitLeft && !hitDown && player.transform.position.y >= transform.position.y) {

					rigid.AddForce(Vector3.right * moveSpeed * 2.0f, ForceMode2D.Force);
					rigid.AddForce(Vector3.down * moveSpeed * 2.0f, ForceMode2D.Force);

				}

			
			}

		}

	}

	void AnimationControl() {

		anim.SetBool("FacingRight", isFacingRight);
		anim.SetBool("Walking", isWalking);
		anim.SetBool("Biting", isAttackByBite);
		anim.SetBool("Attack", isAttackByClaw);

	}

	void UpdateRayOrigin() {

		rayTarget.origin = transform.position;
		rayUp.origin = transform.position;
		rayDown.origin = transform.position;
		rayLeft.origin = transform.position;
		rayRight.origin = transform.position;

	}

	void UpdateRayTargetDirection() {

		if (player) {

			rayTarget.direction = player.transform.position - transform.position;

		}

	}

	void UpdateRaycastHit() {

		hitTarget = Physics2D.Raycast(rayTarget.origin, rayTarget.direction, hitPlayerCheckLength, hitPlayerCheckLayer);
		hitUp = Physics2D.Raycast(rayUp.origin, rayUp.direction, hitWallCheckLength, hitWallCheckLayer);
		hitDown = Physics2D.Raycast(rayDown.origin, rayDown.direction, hitWallCheckLength, hitWallCheckLayer);
		hitLeft = Physics2D.Raycast(rayLeft.origin, rayLeft.direction, hitWallCheckLength, hitWallCheckLayer);
		hitRight = Physics2D.Raycast(rayRight.origin, rayRight.direction, hitWallCheckLength, hitWallCheckLayer);

	}

	void UpdateMoveTargetPosition() {

		if (player) {

			moveTargetPos = player.transform.position - transform.position;

		}

	}

	void UpdateAttackType() {

		isAttackByBite = currentAttackType == AttackType.BITE;
		isAttackByClaw = currentAttackType == AttackType.CLAW;

	}

	void SetAttackType(AttackType attackType) {

		currentAttackType = attackType;

	}

	AttackType GetRandomAttackType() {

		var randomNum = UnityEngine.Random.Range(-1, 2);
		var result = (randomNum > 0) ? AttackType.BITE : AttackType.CLAW;
		return result;

	}

	void AttackTargetControl() {

		if (hitTarget && hitTarget.transform.gameObject.tag == "Player") {

			if (hitTarget.distance <= 0.5f && Time.time > nextAttack) {

				nextAttack = Time.time + attackRate;
				AttackTarget(hitTarget.transform.gameObject);

			}

		}

	}

	void AttackTarget(GameObject obj) {

		if (obj.tag == "Player" && enemy) {

			var regenHealth = obj.GetComponent<RegenHealth>();
			
			regenHealth.Remove(enemy.AttackPoint);
			regenHealth.ReInitRegen();

			var playerController = obj.GetComponent<PlayerController>();			
			playerController.SetInHurt(true);

		}

	}

}
