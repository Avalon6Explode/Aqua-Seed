using UnityEngine;

public class Enemy : MonoBehaviour {

	Health health;


	void Awake() {
		health = GetComponent<Health>();
	}

	void Update() {
		if (health.Current <= 0) {
			gameObject.SetActive(false);
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Bullet") {
			var totalDamage = col.gameObject.GetComponent<Bullet>().AttackPoint;
			health.Remove(totalDamage);
		}
	}
}
