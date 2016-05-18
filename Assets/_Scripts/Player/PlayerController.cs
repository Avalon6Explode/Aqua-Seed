using UnityEngine;

public class PlayerController : MonoBehaviour {

	[SerializeField]
	GameObject objEmptyItem;

	[SerializeField]
	GameObject objMelee;

	[SerializeField]
	Inventory itemInventory;

	[SerializeField]
	Inventory weaponInventory;

	[SerializeField]
	Transform weaponTransformRight;

	[SerializeField]
	Transform weaponTransformLeft;


	float inputX;
	float inputY;

	Vector2 inputMouseVector;
	Vector2 newHoldWeaponPos;

	SpriteRenderer spriteRenderer;
	Animator anim;
	RegenHealth health;

	bool isPressPickUp;
	
	GameObject currentHoldingItem;
	GameObject currentDropItem;

	int currentHoldingItemIndex;
	int prevHoldingItemIndex;


	public GameObject CurrentHoldingItem { get { return currentHoldingItem; } }
	public int CurrentHoldingItemIndex { get { return currentHoldingItemIndex; } }

	public Inventory ItemInventory { get { return itemInventory; } }
	public Inventory WeaponInventory { get { return weaponInventory; } }


	public PlayerController() {
		currentHoldingItemIndex = 0;
		prevHoldingItemIndex = 0;
	}

	void Awake() {
		currentHoldingItem = objEmptyItem;
		currentDropItem = null;
		spriteRenderer = GetComponent<SpriteRenderer>();
		anim = GetComponent<Animator>();
		health = GetComponent<RegenHealth>();
	}

	void Start() {
		var melee = Instantiate(objMelee) as GameObject;
		PickUp(melee, 2);
		HoldWeapon(2);
	}

	void Update() {
		inputX = Input.GetAxisRaw("Horizontal");
		inputY = Input.GetAxisRaw("Vertical");
		isPressPickUp = Input.GetButtonDown("PickUp");
		
		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		for (int i = 0; i < weaponInventory.Length; i++) {
			 if (inputMouseVector.x > transform.position.x) {
			 	weaponInventory.GetItem(i).gameObject.transform.position = weaponTransformRight.position;
			 }
			 else if (inputMouseVector.x < transform.position.x) {
			 	weaponInventory.GetItem(i).gameObject.transform.position = weaponTransformLeft.position;
			 } 
		}

		inputMouseVector -= new Vector2(transform.position.x, transform.position.y); 

		for (int i = 0; i < weaponInventory.Length; i++) {

			var obj = weaponInventory.GetItem(i);

			if (obj.gameObject.tag == "Weapon") {
				if (i != 2) {
					obj.GetComponent<Weapon>().SetAttackAble(itemInventory.IsItemExit("Suit"));
				}
				obj.GetComponent<SpriteRenderer>().sortingOrder = spriteRenderer.sortingOrder + 1;
			}
			else {
				continue;
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
			else if (Input.GetKeyDown(KeyCode.Q)) {
				SwapWeapon();
			}
			else if (Input.GetKeyDown(KeyCode.G)) {
				if (currentHoldingItemIndex != 2) {
					DropWeapon(currentHoldingItemIndex);
					HoldMostPowerfulWeapon();
				}
			}
		}
	}

	void OnTriggerStay2D(Collider2D col) {
		if (isPressPickUp) {

			if (col.gameObject.tag == "Item" && !itemInventory.IsFull) {
				PickUp(col.gameObject);
				col.gameObject.SetActive(false);
			}
			else if (col.gameObject.tag == "Weapon") {
				
				col.transform.Find("IconPos").gameObject.SetActive(false);

				switch (col.gameObject.GetComponent<Weapon>().Classify) {

					case Weapon.WeaponClassify.PRIMARY :
						if (weaponInventory.IsSlotEmpty(0)) {
							PickUp(col.gameObject, 0);
							HoldWeapon(0);
						}
						else {
							DropWeapon(0);
							SetEnableCollider2D(currentDropItem, false);

							PickUp(col.gameObject, 0);
							HoldWeapon(0);

							if (!currentDropItem.Equals(weaponInventory.GetItem(0))) {
								SetEnableCollider2D(currentDropItem, true);
							}
						}
					break;
					
					case Weapon.WeaponClassify.SECONDARY :
						if (weaponInventory.IsSlotEmpty(1)) {
							PickUp(col.gameObject, 1);
							HoldWeapon(1);
						}
						else {
							DropWeapon(1);
							SetEnableCollider2D(currentDropItem, false);

							PickUp(col.gameObject, 1);
							HoldWeapon(1);

							if (!currentDropItem.Equals(weaponInventory.GetItem(1))) {
								SetEnableCollider2D(currentDropItem, true);
							}
						}
					break;
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
				itemInventory.Add(item, index);
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
			
			currentDropItem = obj;
			SetEnableCollider2D(obj, true);

			obj.GetComponent<Weapon>().SetAttackAble(false);
			obj.GetComponent<Weapon>().SetHolding(false);
			obj.transform.position -= Vector3.up * 0.15f;
			obj.transform.eulerAngles = Vector3.zero;
			obj.gameObject.SetActive(true);
			
			var objSpriteRenderer = obj.GetComponent<SpriteRenderer>();
			objSpriteRenderer.flipY = false;
			objSpriteRenderer.sortingLayerName = "Gun";
			objSpriteRenderer.sortingOrder = 0;

			weaponInventory.Remove(index);
		}
		currentHoldingItem = objEmptyItem;
	}

	public void HoldWeapon(int index) {

		var oldOne = weaponInventory.GetItem(currentHoldingItemIndex);	
		var oldOneWeapon = oldOne.GetComponent<Weapon>();
		
		if (oldOneWeapon) {
			oldOneWeapon.SetHolding(false);
		}

		oldOne.SetActive(false);
		
		if (index != currentHoldingItemIndex) {
			prevHoldingItemIndex = currentHoldingItemIndex;
		}

		currentHoldingItemIndex = index;
		
		var newOne = weaponInventory.GetItem(index);
		currentHoldingItem = newOne;

		newOne.GetComponent<Weapon>().SetHolding(true);
		////
		newOne.GetComponent<SpriteRenderer>().sortingLayerName = spriteRenderer.sortingLayerName;
		newOne.SetActive(true);
	}

	public void HoldMostPowerfulWeapon() {
		if (!weaponInventory.IsEmpty) {

			var mostPowerfulWeaponIndex = 0;

			for (int i = 0; i < weaponInventory.Length; i++) {

				var obj = weaponInventory.GetItem(i);
				
				if (obj.gameObject.tag == "Weapon") {
					mostPowerfulWeaponIndex = i;
					break;
				}
			}
			HoldWeapon(mostPowerfulWeaponIndex);
		}
	}

	public void HoldItem(int index) {
		currentHoldingItemIndex = index;
		currentHoldingItem = itemInventory.GetItem(index);
	}

	public void SwapWeapon() {
		if (!weaponInventory.IsSlotEmpty(prevHoldingItemIndex)) {
			var nextWeaponIndex = prevHoldingItemIndex;
			prevHoldingItemIndex = currentHoldingItemIndex;
			HoldWeapon(nextWeaponIndex);
		}
	}

	void SetEnableCollider2D(GameObject item, bool value) {
		var cols = item.gameObject.GetComponents<Collider2D>();
		for (int i = 0; i < cols.Length; i++) {
			cols[i].enabled = value;
		}
	}
}
