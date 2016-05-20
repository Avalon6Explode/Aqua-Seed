using System;
using UnityEngine;

public class Spawner : MonoBehaviour {

	[SerializeField]
	int maxObjectPooling;

	[SerializeField]
	float delay;

	[SerializeField]
	GameObject objs;
	
	[SerializeField]
	Transform[] trans;

	GameController gameController;
	GameObject[] objPooling;


	bool isInitSpawn;
	float nextSpawn;


	void Initialize() {

		objPooling = new GameObject[maxObjectPooling];

		for (int i = 0; i < objPooling.Length; i++) {

			objPooling[i] = Instantiate(objs) as GameObject;
			objPooling[i].SetActive(false);

		}

	}

	void Awake() {

		Initialize();

	}

	void Start() {

		gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();

	}

	void Update() {

		if (!gameController.IsGameOver) {

			if (!isInitSpawn) {

				nextSpawn = Time.time + delay;
				isInitSpawn = true;

			}
			else {

				if (Time.time > nextSpawn) {

					var selectedObj = GetRandomObj();
					selectedObj.transform.position = GetRandomPos();
					selectedObj.SetActive(true);
					isInitSpawn = false;

				}
			}

		}

	}

	Vector3 GetRandomPos() {

		var randNum = UnityEngine.Random.Range(0, trans.Length);
		var result = trans[randNum].position;
		return result;

	}

	GameObject GetRandomObj() {

		GameObject result = null;

		for (int i = 0; i < objPooling.Length; i++) {

			if (!objPooling[i].activeSelf) {

				result = objPooling[i];
				break;

			}

		}

		return result;

	}
}
