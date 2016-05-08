using UnityEngine;
using System.Collections;

public class PlayerMotor : MonoBehaviour {

	[SerializeField]
	float moveSpeed;


	float inputX;
	float inputY;

	Vector2 moveVector;
	Rigidbody2D rigid;
	Health health;
	Stamina stamina;


	public PlayerMotor() {
		moveSpeed = 7.0f;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		health = GetComponent<Health>();
		stamina = GetComponent<Stamina>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		
		moveVector = new Vector2(inputX, inputY) * moveSpeed;
		moveVector = (moveVector.magnitude > 1) ? new Vector2(moveVector.normalized.x, moveVector.normalized.y) * moveSpeed : moveVector;
	}

	void FixedUpdate() {
		rigid.velocity = (health.Current > 0) ? moveVector : Vector2.zero;
	}
}
