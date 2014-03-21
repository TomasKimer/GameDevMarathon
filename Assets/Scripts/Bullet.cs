using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	public float speed = 0;

	void Update () {
		Vector3 step = Vector3.forward * speed;
		gameObject.transform.Translate (step * Time.deltaTime);
	}

	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "wall") {
			Destroy(this.gameObject);
		}
	}
}
