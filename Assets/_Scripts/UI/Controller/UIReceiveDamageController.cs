using UnityEngine;
using UnityEngine.UI;

public class UIReceiveDamageController : MonoBehaviour {

	[SerializeField]
	Canvas playerUI;

	[SerializeField]
	GameObject objTextDamage;

	[SerializeField]
	int maxObjectPooling;


	GameObject[] objTextDamagePooling;


	public UIReceiveDamageController() {
		maxObjectPooling = 40;
	}

	void Awake() {
		objTextDamagePooling = new GameObject[maxObjectPooling];
		
		for (int i = 0; i < objTextDamagePooling.Length; i++) {
			objTextDamagePooling[i] = Instantiate(objTextDamage) as GameObject;
			objTextDamagePooling[i].transform.SetParent(playerUI.transform, false);
			objTextDamagePooling[i].SetActive(false);
		}
	}

	void Update() {

	}

	public void Show(Vector2 worldPoint, int receiveDamage) {

		for (int i = 0; i < maxObjectPooling; i++) {
			if (!objTextDamagePooling[i].activeSelf) {
				
				var screenPoint = Camera.main.WorldToScreenPoint(worldPoint);

				objTextDamagePooling[i].transform.position = screenPoint;
				objTextDamagePooling[i].GetComponent<Text>().text = receiveDamage.ToString();
				objTextDamagePooling[i].SetActive(true);
				objTextDamagePooling[i].gameObject.GetComponent<DisableAgent>().StartDisable();
				
				break;
			}
		}
	}
}
