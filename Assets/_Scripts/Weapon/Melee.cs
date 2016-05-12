using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	float slashRate;

	[SerializeField]
	int staminaCost;

	
	RegenStamina stamina;
	float nextSlash;
	bool isPressSlash;


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
	}

	void Update() {

		isPressSlash = Input.GetButtonDown("Fire1");

		if (stamina != null) {
			if (isAttackAble && IsUseAble && isPressSlash && Time.time > nextSlash) {
				nextSlash = Time.time + slashRate;
				Use();
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
