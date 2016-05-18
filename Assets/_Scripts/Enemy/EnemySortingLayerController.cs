using UnityEngine;

public class EnemySortingLayerController : MonoBehaviour {

	const string BEHIND_LAYER = "Humaniod";
	const string INFRONT_LAYER = "Enemy";


	float currentRefPosY;
	SpriteRenderer spriteRenderer;


	void Awake() {
		spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
	}

	void Update() {
		if (currentRefPosY > transform.position.y) {
			spriteRenderer.sortingLayerName = INFRONT_LAYER;
		}
		else if (currentRefPosY < transform.position.y) {
			spriteRenderer.sortingLayerName = BEHIND_LAYER;;
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			currentRefPosY = col.gameObject.transform.position.y;
		}
	}
}
