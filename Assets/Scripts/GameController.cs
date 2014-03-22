using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {


	private int currentWave;
	private float gamePlayTime;

	private float minSpawnInterval;
	private float maxSpawnInterval;
	private float nextSpawn;

	private bool paused;

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


	public enum Screens { moving, welcome, pause, game };
	private Screens currentScreen = Screens.welcome;

	private TextMesh logoText;
	private TextMesh pauseText;

	// ochrana doba pri prechodu mezi obrazovkama
	private float transitionTime = 0;
	



	
	void Start () {
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer) {
			GameObject joy = GameObject.Find("Dual Joysticks");
			Destroy(joy);
		}

		minSpawnPosX = -11;
		maxSpawnPosX = 11;
		minSpawnPosZ = -8;
		maxSpawnPosZ = 8;

		paused = false;

		// zakladni setup sceny - kopie zdi jako dekorace
		GameObject walls = GameObject.Find ("Walls");
		for (int i = -10; i > -100; i -= 10) {
			Instantiate(walls, new Vector3(walls.transform.position.x, i, walls.transform.position.z), walls.transform.rotation);
		}

		// logo
		prefabText.text = "SHOOT\nSPACE\nTHINGS";
		TextMesh name =  Instantiate (prefabText, new Vector3 (0, -90, 0), Quaternion.Euler(90, 0, 0)) as TextMesh;
		logoText = name;

		Reset ();

		// init kamery
		WelcomScreen ();
	}


	void Reset() {
		gamePlayTime = 0.0f;
		
		minSpawnInterval = 1f;
		maxSpawnInterval = 7f;
		nextSpawn = Random.Range (minSpawnInterval, maxSpawnInterval);

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
		// odecist ochranu proti nasobnym keypressum
		transitionTime -= Time.deltaTime;


		if (currentScreen == Screens.welcome && transitionTime <= 0) {
				// esc na welcome --> exit
				if (Input.GetKeyDown (KeyCode.Escape))  {
					Application.Quit();
					return;
				}
				
				// cokoliv jineho --> game
				if (Input.anyKey || Input.touchCount > 0) {
					GameScreen();
				}
		}

		if (currentScreen == Screens.game && transitionTime <= 0) {
			if (Input.GetKeyDown (KeyCode.Escape))  {
				PauseScreen();
			}
		}

		if (currentScreen == Screens.pause && transitionTime <= 0) {
			if (Input.GetKeyDown (KeyCode.Escape))  {
				GameScreen();
			}
		}


		if (!paused) 
			gamePlayTime += Time.deltaTime;
		
		if (gamePlayTime > nextSpawn) {
			int type = Random.Range(0,5);
			if (type == 1) SpawnBase();
			if (type == 2) SpawnRandom();
			if (type == 3) SpawnFollower();
			if (type == 4) SpawnWave(currentWave);
			
			nextSpawn += Random.Range (minSpawnInterval, maxSpawnInterval);
		}







		// testing -------------------------

		//if (Input.GetKeyDown(KeyCode.Space)) {
		//	SpawnRandom();
		//}

		// testovani kamery
		if (Input.GetKeyDown (KeyCode.G)) {
			CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
			camCtrl.MoveToGame();
		}
		if (Input.GetKeyDown (KeyCode.M)) {
			CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
			camCtrl.MoveToMenu(0);
		}
		if (Input.GetKeyDown (KeyCode.P)) {
			if (paused) Resume();
			else Pause();
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



	public void CameraLerpedTo(Screens scr) {
		currentScreen = scr;
	}



	public void Pause() {
		paused = true;

		Object[] mobs = GameObject.FindObjectsOfType (typeof(BaseMob));
		foreach (BaseMob mob in mobs) {
			mob.paused = true;
		}

		Object[] bullets = Object.FindObjectsOfType (typeof(Bullet));
		foreach (Bullet bullet in bullets) {
			bullet.paused = true;
		}

		player.disableControls = true;

	}

	public void Resume() {
		paused = false;

		// zastavit moby
		Object[] mobs = GameObject.FindObjectsOfType (typeof(BaseMob));
		foreach (BaseMob mob in mobs) {
			mob.paused = false;
		}

		// zastavit kulky
		Object[] bullets = Object.FindObjectsOfType (typeof(Bullet));
		foreach (Bullet bullet in bullets) {
			bullet.paused = false;
		}

		// zastavit hrace
		player.disableControls = false;

		// zrusit pause text
		if (pauseText != null) {
			Destroy(pauseText.gameObject);
		}
	}


	public void WelcomScreen() {
		Pause ();

		currentScreen = Screens.welcome;

		CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
		camCtrl.MoveToMenu(0);

		transitionTime = 0.2f;

		logoText.renderer.enabled = true;
	}

	public void GameScreen() {
		Resume ();

		currentScreen = Screens.game;
		
		CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
		camCtrl.MoveToGame();

		transitionTime = 0.2f;

		logoText.renderer.enabled = false;
	}

	public void PauseScreen() {
		Pause ();
		
		currentScreen = Screens.pause;
		
		CameraController camCtrl = (CameraController)Camera.main.GetComponent("CameraController");
		camCtrl.MoveToMenu(4);
		
		transitionTime = 0.2f;
		
		prefabText.text = "PAUSED";
		pauseText = Instantiate (prefabText, new Vector3(0, -50, 0), Quaternion.Euler(90, 0, 0)) as TextMesh;
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

		GUI.Label(new Rect(1, 200, 100, 100), new GUIContent("menu transition: " + transitionTime));
	}

	

}
