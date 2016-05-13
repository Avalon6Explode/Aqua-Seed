using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	Color hurtColor;


	Health health;
	SpriteRenderer spriteRenderer;
	UIReceiveDamageController uiDamageControl;


	bool isInHurt;
	bool isInitCountdown;

	float normalSelfDelay;
	float nextNormalSelf;


	public Enemy() {
		isInHurt = false;
		isInitCountdown = false;
		normalSelfDelay = 0.04f;
		nextNormalSelf = 0.0f;
	}

	void Awake() {
		health = GetComponent<Health>();
		spriteRenderer = GetComponent<SpriteRenderer>();
	}

	void Start() {
		uiDamageControl = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().PlayerUI.transform.Find("UIReceiveDamageController").gameObject.GetComponent<UIReceiveDamageController>();
	}

	void Update() {
		if (health.Current > 0) {
			if (!isInitCountdown && isInHurt) {
				spriteRenderer.color = hurtColor;
				nextNormalSelf = Time.time + normalSelfDelay;
				isInitCountdown = true;
			}
			else {
				if (Time.time > nextNormalSelf) {
					spriteRenderer.color = Color.white;
					isInHurt = false;
					isInitCountdown = false;
				}
			}
		}
		else {
			gameObject.SetActive(false);
			spriteRenderer.color = Color.white;
		}
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Bullet") {
			var totalDamage = col.gameObject.GetComponent<Bullet>().AttackPoint;
			health.Remove(totalDamage);
			isInHurt = true;

			if (uiDamageControl) {
				uiDamageControl.Show(col.gameObject.transform.position, totalDamage);
			}
		}
	}

	public void SetInHurt(bool value) {
		isInHurt = value;
	}
}
