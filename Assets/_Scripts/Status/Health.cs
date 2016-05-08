using UnityEngine;

public class Health : MonoBehaviour, IStatus<int> {

	[SerializeField]
	protected int health;

	[SerializeField]
	protected int maxHealth;


	public int Current { get { return health; } }
	public int Max { get { return maxHealth; } }


	public Health() {
		health = maxHealth = 100;
	}

	public void FullRestore() {
		health = maxHealth;
	}

	public void Clear() {
		health = 0;
	}

	public void Restore(int point) {
		health = ((health + point) > maxHealth) ? maxHealth : health + point;
	}

	public void Remove(int point) {
		health = ((health - point) < 0) ? 0 : health - point;
	}
}
