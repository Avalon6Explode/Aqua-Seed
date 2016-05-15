using UnityEngine;

public class MeleeSlash : Weapon {

	[SerializeField]
	float moveSpeed;

	[SerializeField]
	float maxMagnitude;


	Vector2 direction;
	Vector3 originPos;

	Rigidbody2D rigid;


	public MeleeSlash() : base() {
		direction = Vector2.zero;
		originPos = Vector3.zero;
	}

	void Awake() {
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update() {
		var currentMagnitude = Mathf.Abs((transform.position - originPos).magnitude);
		
		if (currentMagnitude > maxMagnitude) {
			gameObject.SetActive(false);
		}
	}

	void FixedUpdate() {
		Use();
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
		this.originPos = originPos;
		transform.position = originPos;
	}

	public void SetDirection(Vector2 direction) {
		this.direction = direction;
	}
}
