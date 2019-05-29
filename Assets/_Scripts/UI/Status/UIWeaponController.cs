using UnityEngine;
using UnityEngine.UI;

public class UIWeaponController : MonoBehaviour {

	[SerializeField]
	GameObject player;


	Image imgWeapon;
	PlayerController playerController;
	GameObject selectedWeapon;


	void Awake() {
		imgWeapon = GetComponent<Image>();
		playerController = player.gameObject.GetComponent<PlayerController>();
		selectedWeapon = playerController.CurrentHoldingItem;
	}

	void Start() {
		imgWeapon.enabled = false;
	}

	void Update() {
		
		selectedWeapon = playerController.CurrentHoldingItem;
		
		if (selectedWeapon) {
			if (selectedWeapon.gameObject.tag == "Weapon") {
				imgWeapon.enabled = true;
				imgWeapon.sprite = selectedWeapon.gameObject.GetComponent<SpriteRenderer>().sprite;
			}
			else {
				imgWeapon.enabled = false;
				imgWeapon.sprite = null;
			}
		}
	}
}
