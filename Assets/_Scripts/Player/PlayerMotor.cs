using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	float moveSpeed;


	float inputX;
	float inputY;

	float currentSpeed;

	Vector2 moveVector;
	Rigidbody2D rigid;
	
	RegenHealth health;
	RegenStamina stamina;


	public PlayerMotor() {
		moveSpeed = 2.0f;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		health = GetComponent<RegenHealth>();
		stamina = GetComponent<RegenStamina>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");

		if (stamina.Current < 30) {
			currentSpeed = moveSpeed * 0.5f;
		} 
		else {
			currentSpeed = moveSpeed;
		}
		
		moveVector = new Vector2(inputX, inputY) * currentSpeed;
		moveVector = (moveVector.magnitude > 1) ? new Vector2(moveVector.normalized.x, moveVector.normalized.y) * currentSpeed : moveVector;
	}

	void FixedUpdate() {
		if (health.Current <= 0) {
			rigid.velocity = Vector2.zero;
		}
		else {
			rigid.AddForce(moveVector, ForceMode2D.Impulse);
		}
	}
}
