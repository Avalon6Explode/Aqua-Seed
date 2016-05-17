using UnityEngine;

public class BulletBeamController : MonoBehaviour {
	
	[SerializeField]
	GameObject bulletBeamParent;

	[SerializeField]
	GameObject objBulletBeam;

	[SerializeField]
	Transform initPoint;

	[SerializeField]
	LayerMask layerMask;

	[SerializeField]
	int maxObjectPooling;

	[SerializeField]
	float beamInterval;


	Vector3 target;
	Vector3 inputMouseVector;

	bool isFire;
	float currentInterval;
	GameObject[] objBulletBeamPooling;

	Vector3 toPos;
	float angle;

	Ray2D ray;
	RaycastHit2D hit;

	int bulletReceiveID;
	int wallReceiveID;

	GameObject objTargetPointBeam;
	float approximateID;

	public bool IsFire { get { return isFire; } }


	public BulletBeamController() {
		isFire = false;
		maxObjectPooling = 100;
		beamInterval = 0.35f;
		currentInterval = 0.0f;
	}

	void Awake() {
		
		ray = new Ray2D();

		objBulletBeamPooling = new GameObject[maxObjectPooling];

		for (int i = 0; i <  objBulletBeamPooling.Length; i++) {
			
			objBulletBeamPooling[i] = Instantiate(objBulletBeam) as GameObject;

			var bullet = objBulletBeamPooling[i].GetComponent<BulletBeam>();

			bullet.SetID(i);
			bullet.SetOrigin(initPoint.position + Vector3.right * currentInterval);

			objBulletBeamPooling[i].transform.parent = bulletBeamParent.transform;
			objBulletBeamPooling[i].GetComponent<SpriteRenderer>().enabled = false;
			
			currentInterval += beamInterval;
		}	
	}

	void Start() {
		bulletBeamParent.SetActive(false);
	}

	void FixedUpdate() {
		hit = Physics2D.Raycast(ray.origin, ray.direction, 100.0f, layerMask);
	}

	void Update() {

		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		
		toPos = inputMouseVector;
		toPos -= new Vector3(initPoint.position.x, initPoint.position.y);
		toPos.Normalize();

		angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
		bulletBeamParent.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

		target = inputMouseVector;
		target -= new Vector3(initPoint.position.x, initPoint.position.y);
		target.Normalize();

		ray.origin = initPoint.position;
		ray.direction = target;
		
		foreach (GameObject obj in objBulletBeamPooling) {
			obj.transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle - 90);
		}

		if (isFire) {

			bulletBeamParent.SetActive(true);
			bulletBeamParent.transform.localPosition = initPoint.localPosition;
		
		}
		else {
			SetEnableBulletInRange(0, true);
			bulletBeamParent.SetActive(false);
		}

		if (hit) {

			if (hit.transform.tag == "Enemy") {

				bulletReceiveID = (int)GetAppoximateID();
				SetEnableBulletInRange(0, bulletReceiveID, true);
				SetEnableBulletInRange(bulletReceiveID + 1, false);


				if (isFire) {

					var totalDamage = objBulletBeam.GetComponent<BulletBeam>().AttackPoint;
					hit.transform.GetComponent<Health>().Remove(totalDamage);
					hit.transform.GetComponent<Enemy>().SetInHurt(true);
					hit.transform.GetComponent<Enemy>().ShowDamageUI(hit.transform.position, totalDamage);
				}
			}
			else {

				if (hit.transform.tag == "Wall") {
					wallReceiveID = (int)GetAppoximateID();
					SetEnableBulletInRange(0, wallReceiveID, true);
					SetEnableBulletInRange(wallReceiveID + 1, false);
				}
				else {
					SetEnableBulletInRange(0, true);
				}
			}
		}

		ResetBulletHitEnemyID();
		ResetBulletHitWallID();
	}

	public void StartFire() {
		isFire = true;
	}

	public void StopFire() {
		isFire = false;
	}

	public void SetTarget(Vector3 value) {
		target = value;
	}

	bool IsHasHitEnemy() {
		var result = false;

		for (int i = 0; i < objBulletBeamPooling.Length; i++) {

			var bullet = objBulletBeamPooling[i].GetComponent<BulletBeam>();

			if (bullet.IsHitEnemy) {
				result =  true;
				break;
			}
		}

		return result;
	}

	bool IsHasHitWall() {
		var result = false;

		for (int i = 0; i < objBulletBeamPooling.Length; i++) {

			var bullet = objBulletBeamPooling[i].GetComponent<BulletBeam>();

			if (bullet.IsHitWall) {
				result =  true;
				break;
			}
		}

		return result;
	}

	void ResetBulletHitEnemyID() {
		for (int i = 0; i < objBulletBeamPooling.Length; i++) {
			objBulletBeamPooling[i].GetComponent<BulletBeam>().SetHitEnemy(false);
		}
	}

	void ResetBulletHitWallID() {
		for (int i = 0; i < objBulletBeamPooling.Length; i++) {
			objBulletBeamPooling[i].GetComponent<BulletBeam>().SetHitWall(false);
		}
	}

	void SetEnableBulletInRange(int start, bool value) {

		for (int i = start; i < objBulletBeamPooling.Length; i++) {
			objBulletBeamPooling[i].GetComponent<SpriteRenderer>().enabled = value;
			objBulletBeamPooling[i].SetActive(value);
		}
	}

	void SetEnableBulletInRange(int from, int to, bool value) {

		for (int i = from; i <= to; i++) {
			objBulletBeamPooling[i].GetComponent<SpriteRenderer>().enabled = value;
			objBulletBeamPooling[i].SetActive(value);
		}
	}

	float GetAppoximateID() {
		var result = hit.distance / beamInterval;
		return result;
	}
}
