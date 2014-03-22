using UnityEngine;
using System.Collections;

public class MineMob : BaseMob {

	protected override void UpdateVelocity () {
		gameObject.transform.Rotate (new Vector3 (0, yAngle, 0) * Time.deltaTime);
	}
	
	protected override void OnCollisionWithWalls(Collision collision) {
		//Vector3 globalVector = gameObject.transform.TransformDirection(velocity);
		//globalVector = Vector3.Reflect(globalVector, collision.contacts[0].normal);
		//velocity = gameObject.transform.InverseTransformDirection(globalVector);
		//yAngle = Random.Range(-200,200);
	}
}
