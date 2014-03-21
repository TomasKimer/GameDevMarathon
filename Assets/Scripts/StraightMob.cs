using UnityEngine;
using System.Collections;

/**
 * Mob ktery jen jede rovne od svoji pocatecni pozice
 * Pri kolizi se zdi se otoci a leti zpet
 */
public class StraightMob : BaseMob {

	private int rot = 1;
	private Vector3 dir = Vector3.forward;


	protected override void UpdateVelocity () {
		gameObject.transform.Translate (dir * rot * speed * Time.deltaTime);
	}
	
	
	protected override void OnCollisionWithWalls (Collision collision) {
		rot = rot * -1;
	}

	public void SetDir(Vector3 d) {
		dir = d;
	}

}
