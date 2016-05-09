using UnityEngine;
using System.Collections;

public class EmptyWeapon : Weapon {

	public EmptyWeapon() : base() {
		itemName = "EmptyWeapon";
	}

	public override void Use() {
		return;
	}
}
