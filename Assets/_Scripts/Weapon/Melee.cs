using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	float slashRate;

	[SerializeField]
	int staminaCost;

	
	RegenStamina stamina;
	float nextSlash;
	bool isPressSlash;
	bool isReadyToGiveDamage;


	public int StaminaCost { get { return staminaCost; } }
	public bool IsUseAble { get { return stamina.Current >= staminaCost; } }
	public bool IsReadyToGiveDamage { get { return isReadyToGiveDamage; } }


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
	}

	void Update() {

		isPressSlash = Input.GetButtonDown("Fire1");

		if (stamina != null) {
			if (isAttackAble && IsUseAble && isPressSlash && Time.time > nextSlash) {
				nextSlash = Time.time + slashRate;
				Use();
				isReadyToGiveDamage = true;
			}
			else {
				isReadyToGiveDamage = false;
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
}
