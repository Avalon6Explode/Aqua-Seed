using UnityEngine;

public abstract class Weapon : Item {

	public enum WeaponType {
		NONE,
		GUN,
		MELEE,
		GRENADE
	}

	public enum WeaponClassify {
		NONE,
		PRIMARY,
		SECONDARY,
		TERTIARY
	}

	
	[SerializeField]
	protected int attackPoint;

	[SerializeField]
	protected WeaponType weaponType;

	[SerializeField]
	protected WeaponClassify weaponClassify;


	public WeaponType Type { get { return weaponType; } }
	public WeaponClassify Classify { get { return weaponClassify; } }
	public int AttackPoint { get { return attackPoint; } }


	public Weapon() {
		itemName = "Weapon";
		weaponType = WeaponType.NONE;
		weaponClassify = Weapon.WeaponClassify.NONE;
	}

	public void SetAttackPoint(int point) {
		attackPoint = point;
	}
}
