using UnityEngine;

public class Stamina : MonoBehaviour, IStatus<int> {

	[SerializeField]
	protected int stamina;

	[SerializeField]
	protected int maxStamina;


	public int Current { get { return stamina; } }
	public int Max { get { return maxStamina; } }


	public Stamina() {
		stamina = maxStamina = 100;	
	}

	public void FullRestore() {
		stamina = maxStamina;
	}

	public void Clear() {
		stamina = 0;
	}

	public void Restore(int point) {
		stamina = ((stamina + point) > maxStamina) ? maxStamina : stamina + point;
	}

	public void Remove(int point) {
		stamina = ((stamina - point) < 0) ? 0 : stamina - point;
	}
}
