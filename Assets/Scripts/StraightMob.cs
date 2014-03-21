using UnityEngine;
using System.Collections;

/**
 * Mob ktery jen jede rovne od svoji pocatecni pozice
 * Pri kolizi se zdi se otoci a leti zpet
 */
public class StraightMob : BaseMob {

	protected override void UpdateVelocity () {
		gameObject.transform.Translate (Vector3.forward * speed * Time.deltaTime);
	}
	
	
	protected override void OnCollisionWithWalls (Collision collision) {
		transform.Rotate (new Vector3(0, 180, 0));
	}


}
