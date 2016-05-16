﻿using UnityEngine;

public class Gun : Weapon {

	protected enum ShootType {
		SEMI,
		AUTOMATIC,
		BEAM
	}


	[SerializeField]
	protected ShootType shootType;

	[SerializeField]
	protected GameObject objBullet;

	[SerializeField]
	protected Transform initPoint;

	[SerializeField]
	protected float fireRate;

	[SerializeField]
	protected int energyCost;

	[SerializeField]
	protected int maxObjectPooling;


	protected GameObject[] objBulletPooling;
	protected Vector2 target;
	protected RegenEnergy energy;
	protected GameObject player;
	protected float nextFire;

	protected SpriteRenderer spriteRenderer;
	protected bool isPressShoot;

	protected Vector3 inputMouseVector;
	protected Vector3 toPos;
	protected float angle;
	

	public int EnergyCost { get { return energyCost; } }
	public bool IsUseAble { get { return energy.Current >= energyCost; } }


	public Gun() : base() {
		itemName = "Gun";
		weaponType = Weapon.WeaponType.GUN;
		weaponClassify = Weapon.WeaponClassify.PRIMARY;
		maxObjectPooling = 30;
		nextFire = 0.0f;
		shootType = ShootType.SEMI;
		energy = null;
		angle = 0.0f;
	}

	void Awake() {
		objBulletPooling = new GameObject[maxObjectPooling];
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
				
				target.Normalize();

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
