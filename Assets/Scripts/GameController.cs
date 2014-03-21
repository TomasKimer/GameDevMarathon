using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private int currentWave;
	private bool showGameOver = false;


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
		showGameOver = false;
		player.transform.position = new Vector3(0, 1, 0);
		player.transform.rotation = Quaternion.identity;
		player.disableControls = false;

		Object[] mobs = GameObject.FindObjectsOfType (typeof(BaseMob));
		foreach (BaseMob mob in mobs) {
			Destroy(mob.gameObject);
		}

		Object[] bullets = Object.FindObjectsOfType (typeof(Bullet));
		foreach (Bullet bullet in bullets) {
			Destroy(bullet.gameObject);
		}
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
		showGameOver = true;
		player.disableControls = true;
	}


	void OnGUI () {
		if (showGameOver) {
			int boxW = 535;
			int boxH = 109;
			Rect box = new Rect((Screen.width-boxW)/2, (Screen.height-boxH)/2 - 100, boxW, boxH);

			Texture2D texture = Resources.Load("gameover") as Texture2D;
			GUI.DrawTexture (box, texture);

			int btnW = 200;
			int btnH = 200;
			Rect btn = new Rect((Screen.width-btnW)/2, (Screen.height-btnH)/2 + 50, btnW, btnH);

			if (GUI.Button (btn, "Play again")) {
				Reset();
			}
		}
	}


	

}
