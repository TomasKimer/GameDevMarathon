using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	private GameObject factory;
	private GameObject player;

	void Start () {
		factory = GameObject.Find ("Bullet");
		player = GameObject.Find ("Player");
	}

	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			Vector3 pos = player.transform.position;
			Quaternion rot = player.transform.rotation;
			GameObject newBullet = (GameObject)Instantiate(factory, pos, rot);
			Bullet bullet = (Bullet)newBullet.GetComponent("Bullet");
			bullet.speed = 5;
		}
	}
}
