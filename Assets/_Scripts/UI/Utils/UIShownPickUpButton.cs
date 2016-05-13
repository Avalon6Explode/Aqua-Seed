using UnityEngine;

public class UIShownPickUpButton : MonoBehaviour {

	GameObject objIcon;


	public bool IsDisable { get { return objIcon.activeSelf; } } 


	void Awake() {
		objIcon = transform.GetChild(0).gameObject;
	}

	void Start() {
		objIcon.SetActive(false);
	}

	void OnTriggerEnter2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && !IsDisable) {
			objIcon.SetActive(true);
		}
	}

	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject.tag == "Player" && !IsDisable) {
			objIcon.SetActive(false);
		}
	}

	public void SetEnableUI(bool value) {
		objIcon.SetActive(value);
	}
}
