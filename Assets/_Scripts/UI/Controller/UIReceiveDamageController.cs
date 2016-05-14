using UnityEngine;
using UnityEngine.UI;

public class UIReceiveDamageController : MonoBehaviour {

	[SerializeField]
	Canvas renderUI;

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
			objTextDamagePooling[i].transform.SetParent(renderUI.transform, false);
			objTextDamagePooling[i].SetActive(false);
		}
	}

	public void Show(Vector2 worldPoint, int receiveDamage) {

		for (int i = 0; i < maxObjectPooling; i++) {
			if (!objTextDamagePooling[i].activeSelf) {

				objTextDamagePooling[i].transform.position = worldPoint;
				objTextDamagePooling[i].GetComponent<Text>().text = receiveDamage.ToString();
				objTextDamagePooling[i].SetActive(true);
				objTextDamagePooling[i].gameObject.GetComponent<DisableAgent>().StartDisable();
				
				break;
			}
		}
	}
}
