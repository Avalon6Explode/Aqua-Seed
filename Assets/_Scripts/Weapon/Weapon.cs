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


	protected bool isAttackAble;


	public int AttackPoint { get { return attackPoint; } }
	public bool IsAttackAble { get { return isAttackAble; } }
	public WeaponType Type { get { return weaponType; } }
	public WeaponClassify Classify { get { return weaponClassify; } }


	public Weapon() {
		itemName = "Weapon";
		weaponType = WeaponType.NONE;
		weaponClassify = Weapon.WeaponClassify.NONE;
		isAttackAble = false;
	}

	public void SetAttackPoint(int point) {
		attackPoint = point;
	}

	public void SetAttackAble(bool value) {
		isAttackAble = value;
	}
}
