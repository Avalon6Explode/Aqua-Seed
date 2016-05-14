using UnityEngine;

public class SceneManager : MonoBehaviour {

	public GameObject Player { get { return GameObject.FindGameObjectWithTag("Player"); } }
	public GameObject PlayerUI { get { return GameObject.FindGameObjectWithTag("PlayerUI"); } }
	public GameObject DamageUI { get { return GameObject.FindGameObjectWithTag("DamageUI"); } }
}
