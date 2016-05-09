using UnityEngine;

public class RegenEnergy : Energy, IRegenable {

	[SerializeField]
	float regenRate;

	[SerializeField]
	float regenDelay;

	[SerializeField]
	int regenPoint;


	bool isRegenning;
	float currentRegenDelay;
	bool isInitRegen;


	public float RegenRate { get { return regenRate; } }
	public float RegenDelay { get { return regenDelay; } }
	public int RegenPoint { get { return regenPoint; } }
	public bool IsRegenning { get { return isRegenning; } }


	public RegenEnergy() : base() {
		regenRate = 0.0f;
		regenDelay = 0;
		regenPoint = 0;
		isRegenning = false;
		currentRegenDelay = 0.0f;
		isInitRegen = false;
	}

	void FixedUpdate() {
		if (!isInitRegen) {
		 	if (energy < maxEnergy) {
		 		currentRegenDelay = Time.fixedTime + regenDelay;
		 		isInitRegen = true;
		 	}
		}
		else {
			if (energy < maxEnergy) {
				if (Time.fixedTime > currentRegenDelay) {
					isRegenning = true;
					Regen();
					currentRegenDelay = Time.fixedTime + regenRate;
				}	
			}
			else {
				isRegenning = false;
				isInitRegen = false;
			}
		}
	}

	public void Regen() {
		Restore(regenPoint);
	}

	public void ReInitRegen() {
		isInitRegen = false;
	}
}
