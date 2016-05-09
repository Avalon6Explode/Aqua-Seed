using UnityEngine;

public class Energy : MonoBehaviour, IStatus<int> {

	[SerializeField]
	protected int energy;

	[SerializeField]
	protected int maxEnergy;


	public int Current { get { return energy; } }
	public int Max { get { return maxEnergy; } }


	public Energy() {
		energy = maxEnergy = 100;
	}

	public void FullRestore() {
		energy = maxEnergy;
	}

	public void Clear() {
		energy = 0;
	}

	public void Restore(int point) {
		energy = ((energy + point) > maxEnergy) ? maxEnergy : energy + point;
	}

	public void Remove(int point) {
		energy = ((energy - point) < 0) ? 0 : energy - point;
	}
}
