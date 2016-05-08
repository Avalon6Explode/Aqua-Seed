using UnityEngine;

public abstract class Item : MonoBehaviour {

	[SerializeField]
	protected string itemName;


	public string Name { get { return itemName; } }


	public Item() {
		itemName = "";
	}


	public abstract void Use();
}
