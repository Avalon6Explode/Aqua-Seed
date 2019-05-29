using UnityEngine;

public class BulletBeam : Bullet {

	int bulletID;
	bool isHitEnemy;
	bool isHitWall;

	
	public int ID { get { return bulletID; } }
	public bool IsHitEnemy { get { return isHitEnemy; } }
	public bool IsHitWall { get { return isHitWall; } }


	public BulletBeam() {
		isHitEnemy = false;
		isHitWall = false;
	}

	void Awake() {
		return;
	}

	void OnTriggerEnter2D(Collider2D col) {
		
		if (col.gameObject.tag == "Enemy") {
			SetHitEnemy(true);
		}

		if (col.gameObject.tag == "Wall") {
			SetHitWall(true);
		}

		if (col.gameObject.tag != "Player" && col.gameObject.tag != "Weapon" && col.gameObject.tag != gameObject.tag && col.gameObject.tag != "Enemy" && col.gameObject.tag != "MeleeSlash" && col.gameObject.tag != "Wall") {
			gameObject.GetComponent<SpriteRenderer>().enabled = false;
		}
	}

	public override void Use() {
		return;
	}

	public void SetID(int value) {
		bulletID = value;
	}

	public void SetHitEnemy(bool value) {
		isHitEnemy = value;
	}

	public void SetHitWall(bool value) {
		isHitWall = value;
	}
}
