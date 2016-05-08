using UnityEngine;

public class Inventory : MonoBehaviour {

	[SerializeField]
	Item[,] arryWeapon;

	public Item[,] Weapon { get { return arryWeapon; } }


	public Inventory() {
		arryWeapon = new Item[3, 1];
	}
}
