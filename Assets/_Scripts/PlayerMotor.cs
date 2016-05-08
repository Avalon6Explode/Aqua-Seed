using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	float moveSpeed;


	float inputX;
	float inputY;

	Vector2 moveVector;
	Rigidbody2D rigid;
	RegenHealth health;
	RegenStamina stamina;


	public PlayerMotor() {
		moveSpeed = 7.0f;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		health = GetComponent<RegenHealth>();
		stamina = GetComponent<RegenStamina>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		
		moveVector = new Vector2(inputX, inputY) * moveSpeed;
		moveVector = (moveVector.magnitude > 1) ? new Vector2(moveVector.normalized.x, moveVector.normalized.y) * moveSpeed : moveVector;
	}

	void FixedUpdate() {
		if (health.Current <= 0) {
			rigid.velocity = Vector2.zero;
		} else {
			rigid.AddForce(moveVector, ForceMode2D.Impulse);
		}
	}
}
