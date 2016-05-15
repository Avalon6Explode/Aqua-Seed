using UnityEngine;

public class Bullet : Weapon {

	[SerializeField]
	float moveSpeed;


	Rigidbody2D rigid;
	Vector2 direction;

	Vector2 fromPos;
	Vector3 toPos;
	float totalRotation;


	public Bullet() : base() {
		direction = Vector2.zero;
		fromPos = Vector2.up;
		totalRotation = 0.0f;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		Use();
	}

	void OnEnable() {
		toPos = direction;
		totalRotation = Vector3.Angle(fromPos, toPos);

		if (direction.x > 0.0f) {
			totalRotation *= -1.0f;
		}

		transform.Rotate(0.0f, 0.0f, totalRotation, Space.Self);
	}

	void OnDisable() {
		transform.localEulerAngles = Vector3.zero;
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Weapon" && col.gameObject.tag != "Player") {
			gameObject.SetActive(false);
		}
	}

	public override void Use() {
		rigid.AddForce(direction * moveSpeed, ForceMode2D.Force);
	}

	public void SetOrigin(Vector2 originPos) {
		transform.position = originPos;
	}

	public void SetDirection(Vector2 direction) {
		this.direction = direction;
	}
}
