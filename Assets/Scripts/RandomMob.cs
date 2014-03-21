using UnityEngine;
using System.Collections;

public class RandomMob : MonoBehaviour {

	public float yAngle = 10;
	public float speed = 10;
	private float orientation = 1;
	private Vector3 velocity;
	
	// Use this for initialization
	void Start () {
		//rigidbody.AddForce (Vector3.forward * speed);
		//rigidbody.AddTorque (new Vector3 (0, yAngle, 0));
		float x = Random.Range ((float)-1, (float)1);
		float z = Random.Range ((float)-1, (float)1);
		velocity = new Vector3(x,0,z) * speed;
	}
	
	// Update is called once per frame
	void Update () {

		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z);
		//gameObject.rigidbody.velocity = Vector3.zero;
		gameObject.rigidbody.angularVelocity = Vector3.zero;

		if (yAngle > 0)
			yAngle -= Random.Range (0, 90) * Time.deltaTime;
		if (yAngle < 0)
			yAngle += Random.Range (0, 90) * Time.deltaTime;
		
		
		gameObject.transform.Rotate (new Vector3 (0, yAngle, 0) * Time.deltaTime);
		gameObject.transform.Translate (velocity * Time.deltaTime);

	}
	
	
	void OnCollisionEnter(Collision collision) {
		if (LayerMask.LayerToName(collision.gameObject.layer) == "walls") {
			// otocit se dozadu
			//gameObject.transform.RotateAround(transform.position, transform.up, 180);
			Vector3 globalVector = gameObject.transform.TransformDirection(velocity);
			globalVector = Vector3.Reflect(globalVector, collision.contacts[0].normal);
			velocity = gameObject.transform.InverseTransformDirection(globalVector);
			yAngle = Random.Range(-200,200);
		}
		
		if (LayerMask.LayerToName (collision.gameObject.layer) == "bullets") {
			Destroy(this.gameObject);
		}
		
		
	}
}
