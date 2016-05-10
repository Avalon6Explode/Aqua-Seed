using UnityEngine;

public class PlayerWeaponController : MonoBehaviour {

	[SerializeField]
	Inventory weaponInventory;


	int indexSelectedWeapon;
	GameObject objSelectedWeapon;


	public Inventory WeaponInventory { get { return weaponInventory; } }


	public PlayerWeaponController() {
		indexSelectedWeapon = 0;
	}
}
