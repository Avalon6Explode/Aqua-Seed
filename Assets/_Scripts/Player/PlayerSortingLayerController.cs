using UnityEngine;

public class PlayerSortingLayerController : MonoBehaviour {

	int currentEnemyRenderOrder;
	float currentEnemyRefPosY;

	SpriteRenderer spriteRenderer;


	void Awake() {
		spriteRenderer = transform.parent.gameObject.GetComponent<SpriteRenderer>();
	}

	void Update() {

		if (currentEnemyRefPosY > transform.position.y) {
			if (currentEnemyRenderOrder >= spriteRenderer.sortingOrder) {
				spriteRenderer.sortingOrder = currentEnemyRenderOrder + 1;
			}
		}
		else if (currentEnemyRefPosY < transform.position.y) {
			if (currentEnemyRenderOrder <= spriteRenderer.sortingOrder) {
				spriteRenderer.sortingOrder = currentEnemyRenderOrder - 1;
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (col.gameObject.tag == "Enemy") {
			currentEnemyRefPosY = col.gameObject.transform.position.y;
			currentEnemyRenderOrder = col.gameObject.GetComponent<SpriteRenderer>().sortingOrder;
		}
	}
}
