using UnityEngine;

public class Melee : Weapon {

	[SerializeField]
	int energyCost;


	RegenEnergy energy;
	RegenStamina stamina;


	public int EnergyCost { get { return energyCost; } }
	public bool IsUseAble { get { return energy.Current >= energyCost; } }


	public Melee() : base() {
		itemName = "Melee";
		weaponType = WeaponType.MELEE;
		weaponClassify = WeaponClassify.TERTIARY;
		energy = null;
	}

	void Update() {
		if (energy != null) {

			} else {
				var player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
				
				if (player) {
					energy = player.gameObject.GetComponent<RegenEnergy>();
				}
			}
	}

	public override void Use() {
		return;
	}
}
