using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public Joystick moveJoystick;
	public Joystick aimJoystick;
	public float speed = 18.0f;

	public Bullet prefabBullet;
	private float lastShoot = -11;

	// napr kdyz skonci hra
	public bool disableControls = false;
	
	// Use this for initialization
	void Start () {
	}

	void Shoot(Vector3 moveVec) {

		if (Time.realtimeSinceStartup < lastShoot + 0.1 )
						return;

		Vector3 pos = transform.position;
		Bullet bullet = Instantiate(prefabBullet, pos, Quaternion.identity) as Bullet;
		bullet.SetDirection(moveVec);			
		bullet.speed = 22;

		lastShoot = Time.realtimeSinceStartup;

		}

	// Update is called once per frame
	void Update () {
		gameObject.rigidbody.velocity = Vector3.zero;
		gameObject.rigidbody.angularVelocity = Vector3.zero;

		// WSAD - move
		if (!disableControls && (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
		    Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))) {

			Vector3 moveVec = new Vector3();

			if (Input.GetKey(KeyCode.W)) {
				moveVec.z = 1.0f;
			}
			if (Input.GetKey(KeyCode.S)) {
				moveVec.z = -1.0f;
			}
			if (Input.GetKey(KeyCode.A)) {
				moveVec.x = -1.0f;
			}
			if (Input.GetKey(KeyCode.D)) {
				moveVec.x = 1.0f;
			}

			gameObject.transform.forward = Vector3.Normalize(moveVec);
			gameObject.transform.Translate(Vector3.forward * Time.deltaTime * speed);
		}

		// Arrows - shoot
		if (!disableControls && (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
		    Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))) {
			
			Vector3 moveVec = new Vector3();
			
			if (Input.GetKey(KeyCode.UpArrow)) {
				moveVec.z = 1.0f;
			}
			if (Input.GetKey(KeyCode.DownArrow)) {
				moveVec.z = -1.0f;
			}
			if (Input.GetKey(KeyCode.LeftArrow)) {
				moveVec.x = -1.0f;
			}
			if (Input.GetKey(KeyCode.RightArrow)) {
				moveVec.x = 1.0f;
			}

			Shoot (moveVec);

		}


		// --- Joysticks --- //
		if (moveJoystick != null) {
			if (!disableControls && moveJoystick.isFingerDown) { 
				Vector3 joyDir = new Vector3 (moveJoystick.position.x, 0f, moveJoystick.position.y);
				gameObject.transform.forward = Vector3.Normalize (joyDir);
				gameObject.transform.Translate (Vector3.forward * Time.deltaTime * speed);
			}
		}

		if (aimJoystick != null) {
			if (!disableControls && aimJoystick.isFingerDown) {
				Vector3 joyDir = new Vector3 (aimJoystick.position.x, 0f, aimJoystick.position.y);
				Shoot (joyDir);
			}
		}
	}



	void OnCollisionEnter(Collision collision) {
		if (LayerMask.LayerToName (collision.gameObject.layer) == "mobs") {
			BaseMob mob = (BaseMob) collision.gameObject.GetComponent("BaseMob");
			if (mob.alive) {
				GameObject obj = GameObject.Find("GameController");
				GameController ctrl = (GameController) obj.GetComponent("GameController");
				ctrl.GameOverScreen();
			}
		}		
	}

}
