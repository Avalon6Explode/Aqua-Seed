using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	GameObject objMeleeSlash;

	[SerializeField]
	Transform initPoint;

	[SerializeField]
	int maxPooling;

	[SerializeField]
	float slashRate;

	[SerializeField]
	int staminaCost;


	GameObject[] objMeleeSlashPooling;
	RegenStamina stamina;
	Vector2 target;

	float nextSlash;
	bool isPressSlash;
	bool isReadyToGiveDamage;


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
		isReadyToGiveDamage = false;
		maxPooling = 10;
		objMeleeSlashPooling = new GameObject[maxPooling];
	}

	void Awake() {
		for (int i = 0; i < objMeleeSlashPooling.Length; i++) {
			objMeleeSlashPooling[i] = Instantiate(objMeleeSlash) as GameObject;
			objMeleeSlashPooling[i].SetActive(false);
		}
	}

	void Update() {

		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		isPressSlash = Input.GetButtonDown("Fire1");

		if (stamina != null) {
			if (isAttackAble && IsUseAble && isPressSlash && Time.time > nextSlash) {
				nextSlash = Time.time + slashRate;
				Use();
				PoolingControl();
			}
		}
		else {
			var player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
			
			if (player) {
				stamina = player.GetComponent<RegenStamina>();
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
}
