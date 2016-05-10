using UnityEngine;

public class Inventory : MonoBehaviour {

	[SerializeField]
	GameObject objEmptyItem;

	[SerializeField]
	GameObject[] objItem;

	[SerializeField]
	int maxItem;

	
	int currentEmptySlotIndex;


	public int Length { get { return objItem.Length; } }
	public bool IsFull { get { return currentEmptySlotIndex == objItem.Length; } }
	public bool IsEmpty { get { return currentEmptySlotIndex == 0; } }


	public Inventory() {
		maxItem = 10;
		currentEmptySlotIndex = 0;
	}

	void Awake() {
		objItem = new GameObject[maxItem];
		
		for (int i = 0; i < objItem.Length; i++) {
			objItem[i] = (objItem[i] == null) ? objEmptyItem : objItem[i];
		}
	}

	public void Add(GameObject item) {
		objItem[currentEmptySlotIndex] = item;
		currentEmptySlotIndex += 1;
	}

	public void Remove(int index) {
		objItem[index] = objEmptyItem;

		for (int i = index + 1; i < objItem.Length; i++) {
			objItem[i - 1] = objItem[i];
		}

		currentEmptySlotIndex -= 1;
	}

	public GameObject GetItem(int index) {
		objItem[index] = (objItem[index] == null) ? objEmptyItem : objItem[index];
		return objItem[index];
	}
}
