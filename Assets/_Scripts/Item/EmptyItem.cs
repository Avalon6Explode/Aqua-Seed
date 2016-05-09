using UnityEngine;

public class EmptyItem : Item {

	public EmptyItem() : base() {
		itemName = "EmptyItem";
	}

	public override void Use() {
		return;
	}
}
