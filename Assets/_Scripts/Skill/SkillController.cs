using UnityEngine;

public class SkillController : MonoBehaviour {

	enum Index {
		DASH
	}

	[SerializeField]
	Skill[] arrySkill;


	RegenStamina stamina;


	public SkillController() {
		arrySkill = new Skill[1];
	}

	void Awake() {
		stamina = GetComponent<RegenStamina>();
	}

	void Update() {
		if (Input.GetButtonDown("Dash")) {
			var selectedSkill = arrySkill[(int)Index.DASH];
			
			if (selectedSkill.IsReady && stamina.Current > selectedSkill.StaminaCost) {
				arrySkill[(int)Index.DASH].Use();
				stamina.ReInitRegen();
				stamina.Remove(selectedSkill.StaminaCost);
			}
		}
	}
}
