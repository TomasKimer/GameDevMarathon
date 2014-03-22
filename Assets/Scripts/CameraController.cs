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



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, player.transform.position.z);

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
}
