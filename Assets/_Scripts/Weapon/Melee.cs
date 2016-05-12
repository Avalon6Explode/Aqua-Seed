using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	int staminaCost;


	RegenStamina stamina;
	bool isPressSlash;


	public int StaminaCost { get { return staminaCost; } }
	public bool IsUseAble { get { return stamina.Current >= staminaCost; } }


	public Melee() : base() {
		itemName = "Melee";
		weaponType = WeaponType.MELEE;
		weaponClassify = WeaponClassify.TERTIARY;
		stamina = null;
		isPressSlash = false;
	}

	void Update() {

		isPressSlash = Input.GetButtonDown("Fire1");

		if (stamina != null) {
			if (isAttackAble && IsUseAble && isPressSlash) {
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
