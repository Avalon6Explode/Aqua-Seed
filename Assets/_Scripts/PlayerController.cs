using UnityEngine;

public class PlayerController : MonoBehaviour {

	float inputX;
	float inputY;

	Vector2 inputMouseVector;

	SpriteRenderer spriteRenderer;
	Animator anim;
	RegenHealth health;


	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		health = GetComponent<RegenHealth>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		
		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputMouseVector -= new Vector2(transform.position.x, transform.position.y); 

		if (health.Current > 0) {
			anim.SetFloat("InputX", inputX);
			anim.SetFloat("InputY", inputY);
			anim.SetBool("IsWalking", inputX != 0.0f || inputY != 0.0f);
			
			spriteRenderer.flipX = (inputMouseVector.x > 0.0f) ? false : (inputMouseVector.x < 0.0f) ? true : spriteRenderer.flipX;
		}
	}
}
