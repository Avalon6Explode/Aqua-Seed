using UnityEngine;
using UnityEngine.UI;

public class UIStoryController : MonoBehaviour {

	int currentChooseImageIndex;

	[SerializeField]
	Image imageHolder;

	[SerializeField]
	Text textHolder;

	[SerializeField]
	Sprite[] images;

	[SerializeField]
	string[] texts;


	void Start() {

		textHolder.text = texts[currentChooseImageIndex];
		imageHolder.sprite = images[currentChooseImageIndex];

	}



	public void NextImage() {

		var tempNum = currentChooseImageIndex + 1;

		if (tempNum > images.Length - 1) {

			tempNum = 0;

		}

		currentChooseImageIndex = tempNum;
		textHolder.text = texts[currentChooseImageIndex];
		imageHolder.sprite = images[currentChooseImageIndex];

	}


	public void PreviousImage() {

		var tempNum = currentChooseImageIndex - 1;

		if (tempNum < 0) {

			tempNum = images.Length - 1;

		}

		currentChooseImageIndex = tempNum;
		textHolder.text = texts[currentChooseImageIndex];
		imageHolder.sprite = images[currentChooseImageIndex];

	}
}
