﻿using UnityEngine;
using System.Collections;

public class RandomMob : BaseMob {

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

	void UpdateVelocity () {
		gameObject.transform.position = new Vector3 (gameObject.transform.position.x, 1, gameObject.transform.position.z);

		if (yAngle > 0)
			yAngle -= Random.Range (0, 90) * Time.deltaTime;
		if (yAngle < 0)
			yAngle += Random.Range (0, 90) * Time.deltaTime;
		
		
		gameObject.transform.Rotate (new Vector3 (0, yAngle, 0) * Time.deltaTime);
		gameObject.transform.Translate (velocity * Time.deltaTime);
		}

	void OnCollisionWithWalls(Collision collision) {
		Vector3 globalVector = gameObject.transform.TransformDirection(velocity);
		globalVector = Vector3.Reflect(globalVector, collision.contacts[0].normal);
		velocity = gameObject.transform.InverseTransformDirection(globalVector);
		yAngle = Random.Range(-200,200);
		}
	

}
