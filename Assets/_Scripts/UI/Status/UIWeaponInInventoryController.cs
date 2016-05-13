using UnityEngine;
using UnityEngine.UI;

public class UIWeaponInInventoryController : MonoBehaviour {

	[SerializeField]
	GameObject player;

	[SerializeField]
	Weapon.WeaponClassify weaponType;


	Image imgWeapon;
	Inventory weaponInventory;
	int slotIndex;


	public UIWeaponInInventoryController() {
		slotIndex = 0;
	}

	void Awake() {
		imgWeapon = GetComponent<Image>();
		weaponInventory = player.GetComponent<PlayerController>().WeaponInventory;
		
		switch (weaponType) {
				case Weapon.WeaponClassify.PRIMARY :
					slotIndex = 0;
				break;

				case Weapon.WeaponClassify.SECONDARY :
					slotIndex = 1;
				break;

				case Weapon.WeaponClassify.TERTIARY :
					slotIndex = 2;
				break;
		}
	}

	void Start() {
		imgWeapon.enabled = false;
	}

	void Update() {
		if (!weaponInventory.IsEmpty && !weaponInventory.IsSlotEmpty(slotIndex)) {
			imgWeapon.enabled = true;
			imgWeapon.sprite = weaponInventory.GetItem(slotIndex).GetComponent<SpriteRenderer>().sprite;
		}
		else {
			imgWeapon.enabled = false;
		}
	}
}
