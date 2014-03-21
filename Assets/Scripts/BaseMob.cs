using UnityEngine;
using System.Collections;

public class BaseMob : MonoBehaviour {

	public float yAngle = 10;
	public float speed = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 step = Vector3.forward * speed;

		gameObject.transform.Rotate (new Vector3 (0, yAngle, 0) * Time.deltaTime);
		gameObject.transform.Translate (step * Time.deltaTime);
	}


	void OnCollisionEnter(Collision collision) {
		if (collision.gameObject.tag == "wall") {
			// otocit se dozadu
			gameObject.transform.RotateAround(transform.position, transform.up, 180);
		}

		// trefila me kulka, au!
		string layer = LayerMask.LayerToName (collision.gameObject.layer);
		if (layer == "bullets") {
			Destroy(this.gameObject);
		}
	}

	/*
	void OnGUI () {
		GameObject player = GameObject.FindWithTag ("Player");
		float distance = Vector3.Distance (player.transform.position, gameObject.transform.position);

		GUI.Label(new Rect(1, 1, 100, 100), new GUIContent("distance to player: " + distance));
	}
	*/
}
