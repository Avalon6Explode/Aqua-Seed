using UnityEngine;

public class SkillController : MonoBehaviour {

	enum Index {
		DASH
	}

	[SerializeField]
	Skill[] arrySkill;


	RegenEnergy energy;


	public SkillController() {
		arrySkill = new Skill[1];
	}

	void Awake() {
		energy = GetComponent<RegenEnergy>();
	}

	void Update() {
		if (Input.GetButtonDown("Dash")) {
			var selectedSkill = arrySkill[(int)Index.DASH];
			
			if (selectedSkill.IsReady && energy.Current > selectedSkill.StaminaCost) {
				arrySkill[(int)Index.DASH].Use();
				energy.Remove(selectedSkill.StaminaCost);
				energy.ReInitRegen();
			}
		}
	}
}
