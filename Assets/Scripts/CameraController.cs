using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;
	public bool cameraTilt = true;
	public float tiltRangeX = -4.0f;
	public float tiltRangeZ = 4.0f;

	private Quaternion originalRotation;
	private Quaternion currentRotation; // pro navrat z menu
	private int leftX = -15; 
	private int rightX = 15;
	private int topZ = 10;
	private int bottomZ = -10;

	// pokud jsme v menu, nechceme sledovat hrace
	private bool followPlayer = false;

	private Vector3 gamePosition = new Vector3(0, 20, 0);
	private Vector3 menuPosition = new Vector3(0, -50, 0);
	private int menuLevel = 0;

	private bool isLerpingToGame = false;
	private bool isLerpingToMenu = false;

	// Use this for initialization
	void Start () {
		originalRotation = camera.transform.rotation;
	}

	static float remap(float oldValue, float oldMin, float oldMax, float newMin, float newMax) {
		float oldRange = (oldMax - oldMin);
		float newRange = (newMax - newMin);
		float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
		return newValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (isLerpingToGame) {
			Vector3 dest = player.transform.position + gamePosition;
			transform.position = Vector3.Lerp(transform.position, dest, 0.2f);
			camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, currentRotation, 0.2f);

			// stop
			if (Vector3.Distance (transform.position, dest) < 0.1) {
				isLerpingToGame = false;
				followPlayer = true;
			}
		}

		if (isLerpingToMenu) {
			Vector3 dest = menuPosition;
			dest.y += menuLevel * 10;
			transform.position = Vector3.Lerp(transform.position, dest, 0.2f);
			camera.transform.rotation = Quaternion.Lerp(camera.transform.rotation, originalRotation, 0.2f);

			// stop
			if (Vector3.Distance (transform.position, dest) < 0.1) {
				isLerpingToMenu = false;
				followPlayer = false;
			}
		} 

		if (followPlayer) {
			camera.transform.position = new Vector3 (player.transform.position.x, camera.transform.position.y, player.transform.position.z);
			if (cameraTilt) {
				float xAmount = remap(camera.transform.position.x, leftX, rightX, -tiltRangeX, tiltRangeX);
				float zAmount = remap(camera.transform.position.z, topZ, bottomZ, -tiltRangeZ, tiltRangeZ);
			
				camera.transform.LookAt(new Vector3(camera.transform.position.x + xAmount,
				                                    1,
			                                    	camera.transform.position.z + zAmount),
			                        	new Vector3(0, 0, 1));
			}
			else {
				camera.transform.rotation = originalRotation;
			}
		}
	}


	// presun kamery do pozice menu
	public void MoveToMenu(int level) {
		menuLevel = level;
		isLerpingToGame = false;
		isLerpingToMenu = true;
		followPlayer = false;
		currentRotation = camera.transform.rotation;
	}

	// presun kamery do herni pozice
	public void MoveToGame() {
		isLerpingToMenu = false;
		isLerpingToGame = true;
		followPlayer = false;
	}

	/*
	void OnGUI () {
		GUI.Label(new Rect(1, 1, 100, 100), new GUIContent("lerp to menu: " + isLerpingToMenu));
		GUI.Label(new Rect(1, 100, 100, 100), new GUIContent("lerp to game: " + isLerpingToGame));
	}
	*/
}
