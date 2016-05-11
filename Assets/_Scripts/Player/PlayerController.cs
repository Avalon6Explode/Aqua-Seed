using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	GameObject objEmptyItem;

	[SerializeField]
	Inventory itemInventory;

	[SerializeField]
	Inventory weaponInventory;

	[SerializeField]
	Transform weaponTransform;


	float inputX;
	float inputY;

	Vector2 inputMouseVector;

	SpriteRenderer spriteRenderer;
	Animator anim;
	RegenHealth health;


	GameObject currentHoldingItem;
	int currentHoldingItemIndex;


	public PlayerController() {
		currentHoldingItemIndex = 0;
	}

	void Awake() {
		currentHoldingItem = objEmptyItem;
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		health = GetComponent<RegenHealth>();
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		
		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		inputMouseVector -= new Vector2(transform.position.x, transform.position.y); 


		for (int i = 0; i < weaponInventory.Length; i++) {
			weaponInventory.GetItem(i).gameObject.transform.position = weaponTransform.position; 
		}


		if (itemInventory.IsItemExit("Suit")) {
			if (!weaponInventory.IsEmpty) {
				for (int i = 0; i < weaponInventory.Length; i++) {

					var obj = weaponInventory.GetItem(i);

					if (obj.gameObject.tag == "Weapon") {
						obj.GetComponent<Weapon>().SetAttackAble(true);
					} 
					else {
						continue;
					}
				}
			}
		} else {
			if (!weaponInventory.IsEmpty) {
				for (int i = 0; i < weaponInventory.Length; i++) {

					var obj = weaponInventory.GetItem(i);

					if (obj.gameObject.tag == "Weapon") {
						obj.GetComponent<Weapon>().SetAttackAble(false);
					}
					else {
						continue;
					}
				}
			}
		}


		if (health.Current > 0) {
		
			anim.SetFloat("InputX", inputX);
			anim.SetFloat("InputY", inputY);
			anim.SetFloat("SuitState", (itemInventory.IsItemExit("Suit")) ? 1.0f : 0.0f);
			anim.SetBool("IsWalking", inputX != 0.0f || inputY != 0.0f);

			spriteRenderer.flipX = (inputMouseVector.x > 0.0f) ? false : (inputMouseVector.x < 0.0f) ? true : spriteRenderer.flipX;


			if (Input.GetKeyDown(KeyCode.Alpha1)) {
				if (!weaponInventory.IsSlotEmpty(0)) {
					HoldWeapon(0);
				}
			} 
			else if (Input.GetKeyDown(KeyCode.Alpha2)) {
				if (!weaponInventory.IsSlotEmpty(1)) {
					HoldWeapon(1);
				}
			}
			else if (Input.GetKeyDown(KeyCode.Alpha3)) {
				if (!weaponInventory.IsSlotEmpty(2)) {
					HoldWeapon(2);
				}
			}
			else if (Input.GetKey(KeyCode.G)) {
				DropWeapon(currentHoldingItemIndex);
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (Input.GetKeyDown(KeyCode.E)) {

			if (col.gameObject.tag == "Item" && !itemInventory.IsFull) {
				PickUp(col.gameObject);
				col.gameObject.SetActive(false);
			}
			else if (col.gameObject.tag == "Weapon" && !weaponInventory.IsFull) {
				
				switch (col.gameObject.GetComponent<Weapon>().Classify) {
					case Weapon.WeaponClassify.PRIMARY :
						PickUp(col.gameObject, 0);
						HoldWeapon(0);
					break;
					
					case Weapon.WeaponClassify.SECONDARY :
						PickUp(col.gameObject, 1);
						HoldWeapon(1);
					break;
					
					case Weapon.WeaponClassify.TERTIARY :
						PickUp(col.gameObject, 2);
						HoldWeapon(2);
					break;

					default :
						print("Weapon's classify is none and can't be pick up.");
					break;
				}
				
				if (col.gameObject.tag == "Item") {
					col.gameObject.SetActive(false);
				}
			}
		}
	}

	public void PickUp(GameObject item) {
		switch (item.gameObject.tag) {
			case "Item" :
				itemInventory.Add(item);
			break;

			case "Weapon" : 
				weaponInventory.Add(item);
			break;

			default :
				print("Player hasn't picked up the item.");
			break;
		}
		SetEnableCollider2D(item, false);
	}

	public void PickUp(GameObject item, int index) {

		switch (item.gameObject.tag) {
			case "Item" :
				itemInventory.Add(item);
			break;

			case "Weapon" :
				weaponInventory.Add(item, index);
			break;

			default :
				print("Player hasn't picked up the item.");
			break;
		}
		SetEnableCollider2D(item, false);
	}

	public void DropItem(int index) {
		var obj = itemInventory.GetItem(index);
		SetEnableCollider2D(obj, true);
		obj.gameObject.SetActive(true);
		itemInventory.RemoveAndArrange(index);
		currentHoldingItem = objEmptyItem;
	}

	public void DropWeapon(int index) {
		if (!weaponInventory.GetItem(index).Equals(objEmptyItem)) {
			var obj = weaponInventory.GetItem(index);
			SetEnableCollider2D(obj, true);
			obj.GetComponent<Transform>().position -= Vector3.up * 0.4f; 
			obj.GetComponent<Weapon>().SetAttackAble(false);
			obj.gameObject.SetActive(true);
			weaponInventory.Remove(index);
		}
		currentHoldingItem = objEmptyItem;
	}

	public void HoldWeapon(int index) {
		
		var oldOne = currentHoldingItem;
		oldOne.SetActive(false);
		
		currentHoldingItemIndex = index;
		
		var newOne = weaponInventory.GetItem(index);
		currentHoldingItem = newOne;

		newOne.SetActive(true);

		if (itemInventory.IsItemExit("Suit")) {
			
			var oldOneWeapon = GetComponent<Weapon>();
			var newOneWeapon = GetComponent<Weapon>();

			if (oldOneWeapon) {
				oldOneWeapon.SetAttackAble(false);
			}

			if (newOneWeapon) {
				newOneWeapon.SetAttackAble(true);
			}
		}
	}

	public void HoldItem(int index) {
		currentHoldingItemIndex = index;
		currentHoldingItem = itemInventory.GetItem(index);
	}

	void SetEnableCollider2D(GameObject item, bool value) {
		var cols = item.gameObject.GetComponents<Collider2D>();
		for (int i = 0; i < cols.Length; i++) {
			cols[i].enabled = value;
		}
	}
}
