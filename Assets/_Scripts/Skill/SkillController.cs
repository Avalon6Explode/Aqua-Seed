using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour {

	enum Index {
		DASH
	}

	[SerializeField]
	Inventory itemInventory;

	[SerializeField]
	Skill[] arrySkill;

	[SerializeField]
	Canvas playerUI;


	RegenEnergy energy;


	public SkillController() {
		arrySkill = new Skill[1];
	}

	void Awake() {
		energy = GetComponent<RegenEnergy>();
	}

	void Update() {
		if (itemInventory.IsItemExit("Suit")) {

			playerUI.enabled = true;

			if (Input.GetButtonDown("Dash")) {

				var selectedSkill = arrySkill[(int)Index.DASH];

				if (selectedSkill.IsReady && energy.Current > selectedSkill.EnergyCost) {
					selectedSkill.Use();
					energy.Remove(selectedSkill.EnergyCost);
					energy.ReInitRegen();
				}
			}
		}
		else {
			playerUI.enabled = false;
		}
	}
}