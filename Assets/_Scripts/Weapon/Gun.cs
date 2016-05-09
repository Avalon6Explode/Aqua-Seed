using UnityEngine;

public class Gun : Weapon {

	enum ShootType {
		SEMI,
		AUTOMATIC
	}

	//debug only
	[SerializeField]
	GameObject player;

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

	GameObject[] objBulletPooling;
	RegenEnergy energy;
	
	Vector2 target;


	public bool IsUseAble { get { return energy.Current >= energyCost; } }
	public int EnergyCost { get { return energyCost; } }


	public Gun() : base() {
		itemName = "Gun";
		weaponType = Weapon.WeaponType.GUN;
		weaponClassify = Weapon.WeaponClassify.PRIMARY;
		maxObjectPooling = 30;
		objBulletPooling = new GameObject[maxObjectPooling];
		nextFire = 0.0f;
		shootType = ShootType.SEMI;
	}

	void Awake() {
		for (int i = 0; i < maxObjectPooling; i++) {
			objBulletPooling[i] = Instantiate(objBullet) as GameObject;
			objBulletPooling[i].SetActive(false);
		}
		energy = player.gameObject.GetComponent<RegenEnergy>();
	}

	void Update() {
		
		target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		isPressShoot = (shootType == ShootType.SEMI) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");

		if (IsUseAble && isPressShoot && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Use();
			PoolingControl();
		}
	}

	public override void Use() {
		energy.Remove(energyCost);
		energy.ReInitRegen();
	}

	void PoolingControl() {

		for (int i = 0; i < maxObjectPooling; i++) {
			if (objBulletPooling[i].activeSelf == false) {
				
				if (target.magnitude > 1) {
					target.Normalize();
				}

				objBulletPooling[i].gameObject.GetComponent<Bullet>().SetOrigin(initPoint.position);
				objBulletPooling[i].gameObject.GetComponent<Bullet>().SetDirection(target);
				objBulletPooling[i].gameObject.GetComponent<Bullet>().SetAttackPoint(attackPoint);
				objBulletPooling[i].SetActive(true);
				
				break;
			}
		}
	}
}
