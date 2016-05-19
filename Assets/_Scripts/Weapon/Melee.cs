using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	GameObject objMeleeSlash;

	[SerializeField]
	Transform initPoint;

	[SerializeField]
	Transform initPointLeft;

	[SerializeField]
	Transform initPointRight;

	[SerializeField]
	int maxPooling;

	[SerializeField]
	float slashRate;

	[SerializeField]
	int staminaCost;


	GameObject player;
	GameObject[] objMeleeSlashPooling;

	RegenStamina stamina;
	SpriteRenderer spriteRenderer;

	Vector2 target;

	float nextSlash;
	bool isPressSlash;

	Vector3 inputMouseVector;
	Vector3 toPos;
	float angle;

	BulletAudioPlayer slashAudioPlayer;


	public int StaminaCost { get { return staminaCost; } }
	public bool IsUseAble { get { return stamina.Current >= staminaCost; } }


	public Melee() : base() {
		itemName = "Melee";
		weaponType = WeaponType.MELEE;
		weaponClassify = WeaponClassify.TERTIARY;
		staminaCost = 10;
		slashRate = 0.2f;
		stamina = null;
		isPressSlash = false;
		nextSlash = 0.0f;
		maxPooling = 10;
		isAttackAble = true;
		objMeleeSlashPooling = new GameObject[maxPooling];
		angle = 0.0f;
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();

		for (int i = 0; i < objMeleeSlashPooling.Length; i++) {
			objMeleeSlashPooling[i] = Instantiate(objMeleeSlash) as GameObject;
			objMeleeSlashPooling[i].SetActive(false);
		}
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
	}

	void Update() {
		isPressSlash = Input.GetButtonDown("Fire1");
		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		target = inputMouseVector;
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		toPos = inputMouseVector;
		toPos -= new Vector3(transform.position.x, transform.position.y);
		toPos.Normalize();

		if (isHolding) {
			angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
			
			if (player) {

				if (inputMouseVector.x > player.transform.position.x) {
					
					spriteRenderer.flipY = false;
					initPoint.localPosition = initPointRight.transform.localPosition;
				}
				else if (inputMouseVector.x < player.transform.position.x) {
					
					spriteRenderer.flipY = true;
					initPoint.localPosition = initPointLeft.transform.localPosition;
				}
			}
		}

		if (stamina != null) {
			if (isAttackAble && IsUseAble && isPressSlash && Time.time > nextSlash) {
				nextSlash = Time.time + slashRate;
				Use();
				PlaySlashEffect();
				PoolingControl();
			}
		}
		else {
			if (player) {
				stamina = player.GetComponent<RegenStamina>();
			} 
			else {
				player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
			}
		}

		if (slashAudioPlayer == null) {

			if (player) {

				var parentName = "EffectAudioPlayer";
				var childName = "MeleeSlash";

				slashAudioPlayer = player.transform.Find(parentName).Find(childName).gameObject.GetComponent<BulletAudioPlayer>();
			}
			else {
				player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
			}
		}
	}

	public override void Use() {
		stamina.Remove(staminaCost);
		stamina.ReInitRegen();
	}

	void PoolingControl() {

		for (int i = 0; i < objMeleeSlashPooling.Length; i++) {
			if (!objMeleeSlashPooling[i].activeSelf) {

				if (target.magnitude > 1) {
					target.Normalize();
				}

				var meleeSlash = objMeleeSlashPooling[i].GetComponent<MeleeSlash>();

				meleeSlash.SetOrigin(initPoint.position);
				meleeSlash.SetDirection(target);
				meleeSlash.SetAttackPoint(attackPoint);

				objMeleeSlashPooling[i].SetActive(true);

				break;
			}
		}
	}

	void PlaySlashEffect() {
		if (slashAudioPlayer) {
			slashAudioPlayer.PlaySoundEffect();
		}
	}
}
