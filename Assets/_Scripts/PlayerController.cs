using UnityEngine;

public class PlayerController : MonoBehaviour {

	float inputX;
	float inputY;
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
		
		if (health.Current > 0) {
			anim.SetFloat("InputX", inputX);
			anim.SetFloat("InputY", inputY);
			anim.SetBool("IsWalking", inputX != 0.0f || inputY != 0.0f);
			
			spriteRenderer.flipX = (inputX > 0.0f) ? false : (inputX < 0.0f) ? true : spriteRenderer.flipX;
		}
	}
}
