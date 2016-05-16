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
	protected bool isHolding;


	public int AttackPoint { get { return attackPoint; } }
	public bool IsAttackAble { get { return isAttackAble; } }
	public bool IsHolding { get { return isHolding; } }
	public WeaponType Type { get { return weaponType; } }
	public WeaponClassify Classify { get { return weaponClassify; } }


	public Weapon() {
		itemName = "Weapon";
		weaponType = WeaponType.NONE;
		weaponClassify = Weapon.WeaponClassify.NONE;
		isAttackAble = false;
		isHolding = false;
	}

	void OnTriggerEnter2D(Collider2D col) {		
		if (!isHolding && col.gameObject.tag == "Player") {
			objIconPickUp.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			objIconPickUp.SetActive(false);
		}
	}

	public void SetAttackPoint(int point) {
		attackPoint = point;
	}

	public void SetAttackAble(bool value) {
		isAttackAble = value;
	}

	public void SetHolding(bool value) {
		isHolding = value;
	}

	public void SetDisablePickUpUI(bool value) {
		objIconPickUp.SetActive(value);
	}
}
