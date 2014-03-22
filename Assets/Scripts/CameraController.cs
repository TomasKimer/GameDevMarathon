using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;

	public int leftX = -27; 
	public int rightX = 13;
	public int topZ = 8;
	public int bottomZ = -29;

	public float minRotationVertical = 67.0f;
	public float maxRotationVertical = 87.0f;

	public float minRotationHorizontal = -10.0f;
	public float maxRotationHorizontal = 10.0f;

	// pokud jsme v menu, nechceme sledovat hrace
	private bool followPlayer = false;

	private Vector3 gamePosition = new Vector3(0, 19, -5);
	private Vector3 menuPosition = new Vector3(2, -5, 0);

	private bool isLerpingToGame = false;
	private bool isLerpingToMenu = false;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (isLerpingToGame) {
			transform.position = Vector3.Lerp(transform.position, gamePosition, 0.2f);

			// stop
			if (Vector3.Distance (transform.position, gamePosition) < 0.1) {
				isLerpingToGame = false;
			}
		} else if (isLerpingToMenu) {
			transform.position = Vector3.Lerp(transform.position, menuPosition, 0.2f);

			// stop
			if (Vector3.Distance (transform.position, menuPosition) < 0.1) {
				isLerpingToGame = false;
			}
		} else if (followPlayer) {
				camera.transform.position = new Vector3 (player.transform.position.x, camera.transform.position.y, player.transform.position.z);
		}

		// x - up/down
		// y - left/right

//		Vector3 rot = camera.transform.rotation.eulerAngles;

		//float OldRange = (topZ - bottomZ);
		//float NewRange = (1.0f - 0.0f);  
		//float NewValue = (((camera.transform.position.x - bottomZ) * NewRange) / OldRange) + 0.0f;

		//float vertAmount = NewValue;
		//float horizAmount = 0.5f;
		//rot.y = Mathf.Lerp(minRotationHorizontal, maxRotationHorizontal, vertAmount);
		//rot.y = Mathf.Lerp(minRotationHorizontal, maxRotationHorizontal, horizAmount);


		//rot.z += 0.1f;

		//camera.transform.rotation = Quaternion.Euler(rot);
		//camera.transform.Rotate (new Vector3(0, -0.02f, 0));
	}


	// presun kamery do pozice menu
	public void MoveToMenu() {
		isLerpingToGame = false;
		isLerpingToMenu = true;
	}

	// presun kamery do herni pozice
	public void MoveToGame() {
		isLerpingToMenu = false;
		isLerpingToGame = true;
	}


	void OnGUI () {
		GUI.Label(new Rect(1, 1, 100, 100), new GUIContent("lerp to menu: " + isLerpingToMenu));
		GUI.Label(new Rect(1, 100, 100, 100), new GUIContent("lerp to game: " + isLerpingToGame));
	}
}
