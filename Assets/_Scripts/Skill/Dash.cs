using UnityEngine;

public class Dash : Skill {

	[SerializeField]
	float dashSpeed;


	float inputX;
	float inputY;
	float nextDash;

	bool isInitDash;

	SpriteRenderer spriteRenderer;
	Rigidbody2D rigid;
	
	Vector2 force;
	Vector2 inputVector;


	public float DashSpeed { get { return dashSpeed; } }


	public Dash() : base() {
		staminaCost = 40;
		dashSpeed = 20.0f;
		nextDash = 0.0f;
		isInitDash = false;
		force = Vector2.zero;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		inputVector = new Vector2(inputX, inputY);
	}

	void FixedUpdate() {
		if (!isInitDash) {
			if (state == State.ACTIVE) {
				nextDash = Time.fixedTime + cooldownRate;
				state = State.COOLDOWN;
				isInitDash = true;
			}
		}
		else {
			if (Time.fixedTime > nextDash) {
				state = State.READY;
				isInitDash = false;
			}
		}
	}

	public override void Use() {
		if (inputX != 0.0f || inputY != 0.0f) {
			if (inputVector.magnitude > 1) {
				force = new Vector2(inputVector.normalized.x, inputVector.normalized.y) * dashSpeed;
			}
			else {
				force = inputVector * dashSpeed;
			}
		} else {
			force = (!spriteRenderer.flipX) ? Vector2.right * dashSpeed : Vector2.left * dashSpeed;
		}
		rigid.AddForce(force, ForceMode2D.Impulse);
		state = State.ACTIVE;
	}
}
