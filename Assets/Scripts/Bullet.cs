using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 0;

	private Vector3 direction;

	void Update () {
		//Vector3 step = Vector3.forward * speed;
		gameObject.transform.Translate (direction * Time.deltaTime * speed);
	}

	void OnCollisionEnter(Collision collision) {
		if (LayerMask.LayerToName(collision.gameObject.layer) == "walls") {
			Destroy(this.gameObject);
		}
	}

	public void SetDirection(Vector3 dir) {
		direction = dir;
		direction.Normalize();
	}
}
