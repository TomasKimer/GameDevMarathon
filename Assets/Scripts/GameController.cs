using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private int currentWave;


	public StraightMob prefabStraightMob;
	public BaseMob prefabBaseMob;
	public PlayerController player;



	
	void Start () {
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			GameObject joy = GameObject.Find("Dual Joysticks");
			Destroy(joy);
		}

		Reset ();
	}


	void Reset() {
		currentWave = 0;
		player.transform.position = new Vector3(0, 1, 0);
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
			mob.transform.forward = new Vector3(1, 0, 0);
			mob.transform.position = new Vector3(-8, 1, i);
			mob.speed = 5;
		}
	}






	public void GameOver() {
		Debug.Log ("died");
		GUI.Label(new Rect(1, 1, 100, 100), new GUIContent("died"));
	}


}
