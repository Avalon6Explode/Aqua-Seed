using UnityEngine;

public class Bullet : Weapon {

	[SerializeField]
	float moveSpeed;


	Rigidbody2D rigid;
	Vector2 direction;
	

	public Bullet() {
		direction = Vector2.zero;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	void FixedUpdate() {
		Use();
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Player") {
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
