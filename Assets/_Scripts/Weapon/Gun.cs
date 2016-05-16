using UnityEngine;

public class Gun : Weapon {

	enum ShootType {
		SEMI,
		AUTOMATIC
	}


	[SerializeField]
	ShootType shootType;

	[SerializeField]
	GameObject objBullet;

	[SerializeField]
	Transform initPoint;

	[SerializeField]
	float fireRate;

	[SerializeField]
	int energyCost;


	int maxObjectPooling;
	float nextFire;
	bool isPressShoot;

	Vector3 toPos;
	float angle;

	GameObject[] objBulletPooling;
	RegenEnergy energy;
	
	Vector2 target;


	public int EnergyCost { get { return energyCost; } }
	public bool IsUseAble { get { return energy.Current >= energyCost; } }


	public Gun() : base() {
		itemName = "Gun";
		weaponType = Weapon.WeaponType.GUN;
		weaponClassify = Weapon.WeaponClassify.PRIMARY;
		maxObjectPooling = 30;
		objBulletPooling = new GameObject[maxObjectPooling];
		nextFire = 0.0f;
		shootType = ShootType.SEMI;
		energy = null;
		angle = 0.0f;
	}

	void Awake() {
		for (int i = 0; i < maxObjectPooling; i++) {
			objBulletPooling[i] = Instantiate(objBullet) as GameObject;
			objBulletPooling[i].SetActive(false);
		}
	}

	void Update() {

		isPressShoot = (shootType == ShootType.SEMI) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");

		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		toPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		toPos -= new Vector3(transform.position.x, transform.position.y);
		toPos.Normalize();

		if (isHolding) {
			angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);
		}
		
		if (energy != null) {
			if (isAttackAble && IsUseAble && isPressShoot && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Use();
				PoolingControl();
			}
		}
		else {
			var player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
			
			if (player) {
				energy = player.GetComponent<RegenEnergy>();
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
				
				if (target.magnitude > 1) {
					target.Normalize();
				}

				var bullet = objBulletPooling[i].GetComponent<Bullet>();

				bullet.SetOrigin(initPoint.position);
				bullet.SetDirection(target);
				bullet.SetAttackPoint(attackPoint);

				objBulletPooling[i].SetActive(true);
				
				break;
			}
		}
	}
}
