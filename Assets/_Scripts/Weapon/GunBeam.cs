using UnityEngine;

public class GunBeam : Gun {

	[SerializeField]
	float beamInterval;

	[SerializeField]
	float energyLostRate;

	[SerializeField]
	LayerMask layerMask;


	float currentBeamInterval;
	bool isInUse;
	bool isReadyToGiveDamage;

	float nextEnergyLost;


	public bool IsInUse { get { return isInUse; } }
	public bool IsReadyToGiveDamage { get { return isReadyToGiveDamage; } }


	public GunBeam() {
		itemName = "GunBeam";
		shootType = ShootType.BEAM;
		fireRate = 0.0f;
		energyCost = 4;
		attackPoint = 1;
		beamInterval = 0.35f;
		energyLostRate = 0.1f;
		currentBeamInterval = 0.0f;
		isInUse = false;
	}

	void Awake() {

		objBulletPooling = new GameObject[maxObjectPooling];
		spriteRenderer = GetComponent<SpriteRenderer>();

		for (int i = 0; i < maxObjectPooling; i++) {
			objBulletPooling[i] = Instantiate(objBullet) as GameObject;
			objBulletPooling[i].transform.parent = transform;
			objBulletPooling[i].SetActive(false);
		}
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("SceneManager").GetComponent<SceneManager>().Player;
	}

	void Update() {
		isPressShoot = Input.GetButton("Fire1");
		inputMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	
		target = inputMouseVector;
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		toPos = inputMouseVector;
		toPos -= new Vector3(transform.position.x, transform.position.y);
		toPos.Normalize();

		if (isHolding) {

			angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

			if (player) {
				if (inputMouseVector.x > player.transform.position.x) {
					spriteRenderer.flipY = false;
				}
				else if (inputMouseVector.x < player.transform.position.x) {
					spriteRenderer.flipY = true;
				}
			}
		}

		if (energy != null) {
			if (isHolding && isAttackAble && IsUseAble && isPressShoot && Time.time > nextFire) {

				nextFire = Time.time + fireRate;
				PoolingControl();

				if (Time.time > nextEnergyLost) {
					Use();
					nextEnergyLost = Time.time + energyLostRate;
				}

				currentBeamInterval += beamInterval;
				isInUse = true;
				isReadyToGiveDamage = true;
			}
			else {
				currentBeamInterval = 0.0f;
				isInUse = false;
				isReadyToGiveDamage = false;

				for (int i = 0; i < objBulletPooling.Length; i++) {
					if (objBulletPooling[i].activeSelf) {
						objBulletPooling[i].SetActive(false);
					}
					else {
						continue;
					}
				}
			}
		}
		else {
			if (player) {
				energy = player.GetComponent<RegenEnergy>();
			}
			else {
				player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
			}
		}
	}

	public override void Use() {
		energy.Remove(energyCost);
		energy.ReInitRegen();
	}

	void PoolingControl() {

		for (int i = 0; i < objBulletPooling.Length; i++) {

			if (!objBulletPooling[i].activeSelf) {

				target.Normalize();

				var bullet = objBulletPooling[i].GetComponent<BulletBeam>();
				var beamOrigin = initPoint.position + new Vector3(target.x, target.y) * currentBeamInterval; //new Vector3(initPoint.position.x + target.x, initPoint.position.y + target.y, 0.0f) * currentBeamInterval;
				
				bullet.SetOrigin(beamOrigin);
				bullet.SetDirection(target);
				bullet.SetAttackPoint(attackPoint);

				objBulletPooling[i].SetActive(true);
				break;
			}
		}
	}
}
