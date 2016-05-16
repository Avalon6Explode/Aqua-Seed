using UnityEngine;

public class Bullet : Weapon {

	[SerializeField]
	float moveSpeed;


	Rigidbody2D rigid;
	Vector2 direction;

	Vector3 toPos;
	float angle;


	public Bullet() : base() {
		direction = Vector2.zero;
		angle = 0.0f;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		Use();
	}

	void OnEnable() {
		toPos = direction;
		angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90);
	}

	void OnDisable() {
		transform.rotation = Quaternion.identity;
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
