using UnityEngine;

public abstract class Item : MonoBehaviour {

	[SerializeField]
	protected string itemName;

	[SerializeField]
	protected GameObject objIconPickUp;


	public string Name { get { return itemName; } }


	public Item() {
		itemName = "";
	}

	void Start() {
		objIconPickUp.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			objIconPickUp.gameObject.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player") {
			objIconPickUp.gameObject.SetActive(false);
		}
	}

	public abstract void Use();
}
