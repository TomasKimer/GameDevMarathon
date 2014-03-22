using UnityEngine;
using System.Collections;

public class BaseMob : MonoBehaviour {

	public float yAngle = 10;
	public float speed = 10;
	private float orientation = 1;
	public Vector3 velocity;
	public float freezeTime;
	public bool alive;
	public bool paused;

	
	// Use this for initialization
	void Start () {
		freezeTime = 1;
		alive = false;
		paused = false;
		Collider playerCollider = GameObject.Find ("Player").transform.collider;
		Physics.IgnoreCollision (playerCollider, gameObject.transform.collider,true);
		//rigidbody.AddForce (Vector3.forward * speed);
		//rigidbody.AddTorque (new Vector3 (0, yAngle, 0));
		float x = Random.Range ((float)-1, (float)1);
		float z = Random.Range ((float)-1, (float)1);
		velocity = new Vector3(x,0,z) * speed;
	}

	protected virtual void UpdateVelocity() {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z);
		gameObject.transform.Translate (velocity * Time.deltaTime);
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z);

		gameObject.rigidbody.velocity = Vector3.zero;
		gameObject.rigidbody.angularVelocity = Vector3.zero;

		if (freezeTime > 0)
						freezeTime -= Time.deltaTime;
				else {
						alive = true;
						Collider playerCollider = GameObject.Find ("Player").transform.collider;
						Physics.IgnoreCollision (playerCollider, gameObject.transform.collider,false);
				}


		if (alive && !paused) 	UpdateVelocity ();
	}
	
	protected virtual void OnCollisionWithWalls(Collision collision) {
		Vector3 globalVector = gameObject.transform.TransformDirection(velocity);
		globalVector = Vector3.Reflect(globalVector, collision.contacts[0].normal);
		velocity = gameObject.transform.InverseTransformDirection(globalVector);
	}

	void OnCollisionEnter(Collision collision) {
		if (LayerMask.LayerToName(collision.gameObject.layer) == "walls") {
			gameObject.rigidbody.velocity = Vector3.zero;
			gameObject.rigidbody.angularVelocity = Vector3.zero;
			OnCollisionWithWalls(collision);
		}

		if (LayerMask.LayerToName (collision.gameObject.layer) == "bullets") {
			Destroy(this.gameObject);
			Destroy (collision.gameObject);
		}


	}
	
	/*
	void OnGUI () {
		GameObject player = GameObject.FindWithTag ("Player");
		float distance = Vector3.Distance (player.transform.position, gameObject.transform.position);

		GUI.Label(new Rect(1, 1, 100, 100), new GUIContent("distance to player: " + distance));
	}
	*/
}
