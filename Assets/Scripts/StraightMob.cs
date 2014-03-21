using UnityEngine;
using System.Collections;

/**
 * Mob ktery jen jede rovne od svoji pocatecni pozice
 * Pri kolizi se zdi se otoci a leti zpet
 */
public class StraightMob : MonoBehaviour {

	public float speed = 0;

	private int rot = 1;
	private Vector3 dir = Vector3.forward;


	void Update () {
		gameObject.rigidbody.velocity = Vector3.zero;
		gameObject.rigidbody.angularVelocity = Vector3.zero;
		gameObject.transform.Translate (dir * rot * speed * Time.deltaTime);
	}
	
	
	void OnCollisionEnter(Collision collision) {
		if (LayerMask.LayerToName(collision.gameObject.layer) == "walls") {
			rot = rot * -1;
		}

		if (LayerMask.LayerToName(collision.gameObject.layer) == "bullets") {
			Destroy(this.gameObject);
		}
	}

	public void SetDir(Vector3 d) {
		dir = d;
	}

}
