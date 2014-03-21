using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	private int currentWave;

	
	void Start () {
		Reset ();
	}


	void Reset() {
		currentWave = 0;
	}


	void Update () {
		if (Input.GetKeyDown(KeyCode.Space)) {
			SpawnWave(0);
		}
	}



	private void SpawnWave(int wave) {
		if (wave == 0) {
			SpawnLineFromLeft();
		}
	}






	private void SpawnLineFromLeft() {
		GameObject prefab = GameObject.Find ("StraightMob");

		for (int i = -8; i < 10; i += 2) {
			GameObject obj = (GameObject)Instantiate(prefab);
			obj.transform.position = new Vector3(-8, 1, i);

			StraightMob mob = (StraightMob)obj.GetComponent("StraightMob");
			mob.speed = 5;
			mob.SetDir(new Vector3(1, 0, 0));
		}
	}



}
