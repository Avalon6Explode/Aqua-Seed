using UnityEngine;

public class WeaponInventory : MonoBehaviour {

	[SerializeField]
	GameObject objEmptyWeapon;

	[SerializeField]
	GameObject[] objWeapon;


	public WeaponInventory() {
		objWeapon = new GameObject[3];
	}

	public void Add(GameObject weaponObj) {
		var classify = weaponObj.gameObject.GetComponent<Weapon>().Classify;

		switch (classify) {
			case Weapon.WeaponClassify.PRIMARY :
				objWeapon[(int)Weapon.WeaponClassify.PRIMARY] = weaponObj;
				break;

			case Weapon.WeaponClassify.SECONDARY :
				objWeapon[(int)Weapon.WeaponClassify.SECONDARY] = weaponObj;
				break;

			case Weapon.WeaponClassify.TERTIARY :
				objWeapon[(int)Weapon.WeaponClassify.TERTIARY] = weaponObj;
				break;
		}
	}

	public void Remove(int index) {
		objWeapon[index] = objEmptyWeapon;
	}

	public GameObject GetWeapon(int index) {
		var weapon = objWeapon[index];
		weapon = (weapon == null) ? objEmptyWeapon : weapon;
		return weapon;
	}
}
