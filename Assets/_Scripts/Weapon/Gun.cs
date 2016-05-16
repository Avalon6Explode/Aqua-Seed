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


	GameObject player;

	int maxObjectPooling;
	float nextFire;
	bool isPressShoot;

	Vector3 inpuMouseVector;
	Vector3 toPos;
	float angle;

	GameObject[] objBulletPooling;
	RegenEnergy energy;
	
	Vector2 target;
	SpriteRenderer spriteRenderer;


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
		spriteRenderer = GetComponent<SpriteRenderer>();

		for (int i = 0; i < maxObjectPooling; i++) {
			objBulletPooling[i] = Instantiate(objBullet) as GameObject;
			objBulletPooling[i].SetActive(false);
		}
	}

	void Start() {
		player = GameObject.FindGameObjectWithTag("SceneManager").gameObject.GetComponent<SceneManager>().Player;
	}

	void Update() {
		isPressShoot = (shootType == ShootType.SEMI) ? Input.GetButtonDown("Fire1") : Input.GetButton("Fire1");
		inpuMouseVector = Camera.main.ScreenToWorldPoint(Input.mousePosition);

		target = inpuMouseVector;
		target -= new Vector2(initPoint.position.x, initPoint.position.y);

		toPos = inpuMouseVector;
		toPos -= new Vector3(transform.position.x, transform.position.y);
		toPos.Normalize();

		if (isHolding) {
			angle = Mathf.Atan2(toPos.y, toPos.x) * Mathf.Rad2Deg;
			transform.rotation = Quaternion.Euler(0.0f, 0.0f, angle);

			if (player) {
				if (inpuMouseVector.x > player.transform.position.x) {
					spriteRenderer.flipY = false;
				}
				else if (inpuMouseVector.x < player.transform.position.x) {
					spriteRenderer.flipY = true;
				}
			}
		}
		
		if (energy != null) {
			if (isAttackAble && IsUseAble && isPressShoot && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Use();
				PoolingControl();
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
