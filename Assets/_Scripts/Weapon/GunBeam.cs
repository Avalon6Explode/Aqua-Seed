using UnityEngine;

public class GunBeam : Gun {

	[SerializeField]
	float energyLostRate;

	[SerializeField]
	float backWardForce;


	float nextEnergyLost;
	
	bool isInUse;
	bool isReadyToGiveDamage;

	BulletBeamController bulletBeamController;


	public bool IsInUse { get { return isInUse; } }
	public bool IsReadyToGiveDamage { get { return isReadyToGiveDamage; } }


	public GunBeam() {
		itemName = "GunBeam";
		shootType = ShootType.BEAM;
		fireRate = 0.0f;
		energyCost = 4;
		attackPoint = 1;
		energyLostRate = 0.1f;
		isInUse = false;
		backWardForce = 7.0f;
	}

	void Awake() {
		spriteRenderer = GetComponent<SpriteRenderer>();
		bulletBeamController = GetComponent<BulletBeamController>();
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
	
		if (isHolding) {

			toPos.Normalize();
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

				if (player) {

					var playerRigid = player.GetComponent<Rigidbody2D>();
					var moveTarget = target * -1.0f;
					moveTarget.Normalize();
					
					playerRigid.AddForce(moveTarget * backWardForce, ForceMode2D.Force);
				}

				if (Time.time > nextEnergyLost) {
					Use();
					nextEnergyLost = Time.time + energyLostRate;
				}
				bulletBeamController.StartFire();
				isInUse = true;
				isReadyToGiveDamage = true;
			}
			else {
				bulletBeamController.StopFire();
				isInUse = false;
				isReadyToGiveDamage = false;
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

	protected override void PlaySoundEffect() {

	}
}
