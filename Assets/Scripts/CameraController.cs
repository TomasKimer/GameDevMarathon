using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public PlayerController player;

	private int leftX = -22; 
	private int rightX = 20;
	private int topZ = 9;
	private int bottomZ = -26;

	// Use this for initialization
	void Start () {

	}

	static float remap(float oldValue, float oldMin, float oldMax, float newMin, float newMax) {
		float oldRange = (oldMax - oldMin);
		float newRange = (newMax - newMin);
		float newValue = (((oldValue - oldMin) * newRange) / oldRange) + newMin;
		return newValue;
	}
	
	// Update is called once per frame
	void Update () {
		camera.transform.position = new Vector3(player.transform.position.x, camera.transform.position.y, player.transform.position.z);

		float vertAmount = remap(camera.transform.position.x, bottomZ, topZ, -2.5f, 2.5f);
		float horizAmount = remap(camera.transform.position.y, leftX, rightX, -2.5f, 2.5f);

		camera.transform.LookAt(new Vector3(camera.transform.position.x + vertAmount,
		                                    1,
		                                    camera.transform.position.z + horizAmount),
		                        new Vector3(0,0,1));
	}
}
