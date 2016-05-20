using UnityEngine;

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
	protected Transform initPointLeft;

	[SerializeField]
	protected Transform initPointRight;

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

	AudioPlayer bulletAudioPlayer;
	

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
					initPoint.localPosition = initPointRight.transform.localPosition;
					
				}
				else if (inputMouseVector.x < player.transform.position.x) {
					
					spriteRenderer.flipY = true;
					initPoint.localPosition = initPointLeft.transform.localPosition;
				}
			}
		}
		
		if (energy != null) {
			if (isHolding && isAttackAble && IsUseAble && isPressShoot && Time.time > nextFire) {
				nextFire = Time.time + fireRate;
				Use();
				PlayFireSoundEffect();
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

		if (bulletAudioPlayer == null) {

			if (player) {

				var parentName = "EffectAudioPlayer";
				var childName = string.Empty;

				switch (shootType) {

					case ShootType.SEMI :
						childName = "Bullet(Semi)";
					break;

					case ShootType.AUTOMATIC :
						childName = "Bullet(Auto)";
					break;

				}
				bulletAudioPlayer = player.transform.Find(parentName).Find(childName).gameObject.GetComponent<AudioPlayer>();
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

	void PlayFireSoundEffect() {
		if (bulletAudioPlayer) {
			bulletAudioPlayer.PlayOnce();
		}
	}
}
