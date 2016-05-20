using UnityEngine;

public class Enemy : MonoBehaviour {

	[SerializeField]
	Color hurtColor;


	Health health;
	SpriteRenderer spriteRenderer;
	
	UIReceiveDamageController uiDamageControl;
	Inventory playerItemInventory;


	bool isInHurt;
	bool isInitCountdown;

	float normalSelfDelay;
	float nextNormalSelf;


	public bool IsInHurt { get { return isInHurt; } } 


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
		uiDamageControl = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().DamageUI.transform.Find("UIReceiveDamageController").gameObject.GetComponent<UIReceiveDamageController>();
		playerItemInventory = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().Player.GetComponent<PlayerController>().ItemInventory;
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
		if (col.gameObject.tag == "Bullet" || col.gameObject.tag == "MeleeSlash") {
			
			var totalDamage = 0;

			if (col.gameObject.tag == "Bullet") {
				totalDamage = col.gameObject.GetComponent<Bullet>().AttackPoint;
			}
			else if (col.gameObject.tag == "MeleeSlash") {
				totalDamage = col.gameObject.GetComponent<MeleeSlash>().AttackPoint;
			}

			health.Remove(totalDamage);
			isInHurt = true;

			if (uiDamageControl && playerItemInventory.IsItemExit("Suit")) {
				uiDamageControl.Show(col.gameObject.transform.position, totalDamage);
			}
		}
	}

	public void SetInHurt(bool value) {
		isInHurt = value;
	}

	public void ShowDamageUI(Vector3 initPos, int damage) {
		if (uiDamageControl && playerItemInventory.IsItemExit("Suit")) {

			var randomMagnitudeX = Random.Range(-0.3f, 0.3f);
			var randomMagnitudeY = Random.Range(-0.3f, 0.3f);

			var randomPosVector = new Vector3(randomMagnitudeX, randomMagnitudeY);
			uiDamageControl.Show(initPos + randomPosVector, damage);
		}
	}
}
