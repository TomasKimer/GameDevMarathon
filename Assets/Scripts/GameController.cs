using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private int currentWave;


	public StraightMob prefabStraightMob;
	public BaseMob prefabBaseMob;



	
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
		for (int i = -8; i < 10; i += 2) {
			StraightMob mob =  Instantiate(prefabStraightMob) as StraightMob;
			mob.transform.position = new Vector3(-8, 1, i);
			mob.speed = 5;
			mob.SetDir(new Vector3(1, 0, 0));
		}
	}



}
