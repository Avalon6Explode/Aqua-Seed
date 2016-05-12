using UnityEngine;

public class SceneManager : MonoBehaviour {

	public GameObject Player { get { return GameObject.FindGameObjectWithTag("Player"); } }
}
