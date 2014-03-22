using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private int currentWave;
	private bool showGameOver = false;

	public StraightMob prefabStraightMob;
	public BaseMob prefabBaseMob;
	public RandomMob prefabRandomMob;
	public FollowerMob prefabFollowerMob;
	public TextMesh prefabText;

	public PlayerController player;

	private float minSpawnPosX;
	private float maxSpawnPosX;
	private float minSpawnPosZ;
	private float maxSpawnPosZ;

	



	
	void Start () {
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			GameObject joy = GameObject.Find("Dual Joysticks");
			Destroy(joy);
		}

		minSpawnPosX = -11;
		maxSpawnPosX = 11;
		minSpawnPosZ = -8;
		maxSpawnPosZ = 8;


		// zakladni setup sceny - kopie zdi jako dekorace
		GameObject walls = GameObject.Find ("Walls");
		for (int i = -10; i > -100; i -= 10) {
			Instantiate(walls, new Vector3(walls.transform.position.x, i, walls.transform.position.z), walls.transform.rotation);
		}

		Reset ();

		// init kamery
		WelcomScreen ();
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
			SpawnRandom();
		}

		// testovani kamery
		if (Input.GetKeyDown (KeyCode.G)) {
			CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
			camCtrl.MoveToGame();
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
			camCtrl.MoveToMenu(0);
		}
	}

	private void SpawnFollower() {
		FollowerMob fm = Instantiate (prefabFollowerMob) as FollowerMob;
		Vector3 spawnpos = Vector3.zero;
		while (true) {
			spawnpos = new Vector3((float)Random.Range (minSpawnPosX,maxSpawnPosX), 1, (float)Random.Range(minSpawnPosZ, maxSpawnPosZ));
			if (Vector3.Distance(spawnpos, player.transform.position) > 3 )
				break;
		}

		fm.transform.position = spawnpos;
			
		fm.speed = 5;
	}



	private void SpawnBase() {
		BaseMob fm = Instantiate (prefabBaseMob) as BaseMob;
		Vector3 spawnpos = Vector3.zero;
		while (true) {
			spawnpos = new Vector3((float)Random.Range (minSpawnPosX,maxSpawnPosX), 1, (float)Random.Range(minSpawnPosZ, maxSpawnPosZ));
			if (Vector3.Distance(spawnpos, player.transform.position) > 3 )
				break;
		}
		
		fm.transform.position = spawnpos;
		
		fm.speed = 5;

		}

	private void SpawnRandom() {
		RandomMob fm = Instantiate (prefabRandomMob) as RandomMob;
		Vector3 spawnpos = Vector3.zero;
		while (true) {
			spawnpos = new Vector3((float)Random.Range (minSpawnPosX,maxSpawnPosX), 1, (float)Random.Range(minSpawnPosZ, maxSpawnPosZ));
			if (Vector3.Distance(spawnpos, player.transform.position) > 3 )
				break;
		}
		
		fm.transform.position = spawnpos;
		
		fm.speed = 5;
		
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
			mob.transform.position = new Vector3(-10, 1, i);
			mob.speed = 5;
		}
	}





	public void WelcomScreen() {
		CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
		camCtrl.MoveToMenu(0);

		prefabText.text = "SHOOT\nSPACE\nTHINGS";
		GameObject name = (GameObject) Instantiate (prefabText, new Vector3 (0, -90, 0), Quaternion.Euler(90, 0, 0));
		TextMesh nameText = (TextMesh)name.GetComponent ("TextMesh");

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
