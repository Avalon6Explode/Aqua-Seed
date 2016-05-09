using UnityEngine;

public abstract class Weapon : Item {

	public enum WeaponType {
		NONE,
		GUN,
		MELEE,
		GRENADE
	}

	public enum WeaponClassify {
		PRIMARY,
		SECONDARY,
		TERTIARY
	}

	
	[SerializeField]
	protected WeaponType weaponType;

	[SerializeField]
	protected WeaponClassify weaponClassify;


	public WeaponType Type { get { return weaponType; } }
	public WeaponClassify Classify { get { return weaponClassify; } }


	public Weapon() {
		itemName = "Weapon";
		weaponType = WeaponType.NONE;
		weaponClassify = Weapon.WeaponClassify.PRIMARY;
	}
}
