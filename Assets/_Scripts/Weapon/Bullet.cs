using UnityEngine;

public class Bullet : MonoBehaviour {

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
		rigid.AddForce(direction * moveSpeed, ForceMode2D.Force);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag != "Player") {
			gameObject.SetActive(false);
		}
	}

	public void SetOrigin(Vector2 originPos) {
		transform.position = originPos;
	}

	public void SetDirection(Vector2 direction) {
		this.direction = direction;
	}
}
