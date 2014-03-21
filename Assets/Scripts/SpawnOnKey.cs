using UnityEngine;
using System.Collections;

public class SpawnOnKey : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Input.GetKeyDown (KeyCode.X)) {
			GameObject mob = GameObject.Find("Testmob");

			GameObject obj = (GameObject)Instantiate(mob);
			obj.transform.position = new Vector3(4, 1, 0);

			BaseMob objMob = (BaseMob)obj.GetComponent("BaseMob");
			objMob.speed = Random.Range(2, 20);
		}

	}
}
