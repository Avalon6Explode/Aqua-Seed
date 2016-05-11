using UnityEngine;

public class Inventory : MonoBehaviour {

	[SerializeField]
	GameObject objEmptyItem;

	[SerializeField]
	GameObject[] objItem;

	[SerializeField]
	int maxItem;

	
	int totalItem;
	int currentEmptySlotIndex;


	public int Length { get { return objItem.Length; } }
	public int TotalItem { get { return totalItem; } }
	public int CurrentEmptySlotIndex { get { return currentEmptySlotIndex; } }
	public bool IsFull { get { return totalItem == objItem.Length; } }
	public bool IsEmpty { get { return totalItem == 0; } }

	public Inventory() {
		maxItem = 10;
		currentEmptySlotIndex = 0;
		totalItem = 0;
	}

	void Awake() {
		objItem = new GameObject[maxItem];
		
		for (int i = 0; i < objItem.Length; i++) {
			objItem[i] = (objItem[i] == null) ? objEmptyItem : objItem[i];
		}
	}

	void Update() {
		var totalObject = 0;

		for (int i = 0; i < objItem.Length; i++) {
			if (!(objItem[i] == objEmptyItem)) {
				totalObject += 1;
			}
		}

		totalItem = totalObject;
		currentEmptySlotIndex = (totalObject == objItem.Length) ? -1 : totalObject + 1;
	}

	public void Add(GameObject item) {
		var index = (IsEmpty) ? 0 : currentEmptySlotIndex - 1;
		objItem[index] = item;
	}

	public void Add(GameObject item, int index) {
		objItem[index] = item;
	}

	public void Remove(int index) {
		objItem[index] = objEmptyItem;
		currentEmptySlotIndex = index;
	}

	public void RemoveAndArrange(int index) {
		for (int i = index + 1; i < objItem.Length - 1; i++) {
			objItem[i - 1] = objItem[i];
		}
		currentEmptySlotIndex = (!IsEmpty) ? currentEmptySlotIndex - 1 : 0;
		objItem[currentEmptySlotIndex] = objEmptyItem;
	}

	public void RemoveAndArrange(GameObject obj) {
		for (int i = 0; i < objItem.Length; i++) {
			if (objItem[i].Equals(obj)) {
				RemoveAndArrange(i);
			}
		}
	}

	public GameObject GetItem(int index) {
		objItem[index] = (objItem[index] == null) ? objEmptyItem : objItem[index];
		return objItem[index];
	}

	public bool IsItemExit(string itemName) {
		var result = false;

		for (int i = 0; i < objItem.Length; i++) {

			var obj = objItem[i].gameObject.GetComponent<Item>();

			if (obj && obj.Name == itemName) {
				result = true;
				break;
			}
		}

		return result;
	}

	public bool IsSlotEmpty(int index) {
		return objItem[index].Equals(objEmptyItem);
	}
}
