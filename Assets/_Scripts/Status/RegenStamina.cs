﻿using UnityEngine;
using System.Collections;

public class RegenStamina : Stamina, IRegenable {

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


	public RegenStamina() : base() {
		regenRate = 0.1f;
		regenDelay = 1;
		regenPoint = 2;
		isRegenning = false;
		currentRegenDelay = 0.0f;
		isInitRegen = false;
	}

	void FixedUpdate() {
		if (!isInitRegen) {
			if (stamina < maxStamina) {
				currentRegenDelay = Time.fixedTime + regenDelay;
				isInitRegen = true;
			}
		} else {
			if (stamina < maxStamina) {
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
