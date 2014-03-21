using UnityEngine;
using System.Collections;

public class FollowerMob : BaseMob {


	//GameObject player = (GameObject)GameObject.Find ("Player");
	
	protected override void UpdateVelocity () {
		Vector3 playerPos = Vector3.zero;
		playerPos = GameObject.Find("Player").transform.position;
		Vector3 myPos = Vector3.zero;
		myPos = gameObject.transform.position;
		velocity = (playerPos - myPos);
		gameObject.transform.Translate (velocity * Time.deltaTime, Space.World);
		gameObject.transform.Rotate (new Vector3 (0, 180, 0) * Time.deltaTime);
		
	}
	
	protected override void OnCollisionWithWalls(Collision collision) {
		Vector3 globalVector = gameObject.transform.TransformDirection(velocity);
		globalVector = Vector3.Reflect(globalVector, collision.contacts[0].normal);
		velocity = gameObject.transform.InverseTransformDirection(globalVector);
	}
}
