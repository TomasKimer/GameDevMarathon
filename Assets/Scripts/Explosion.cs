using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

	public float speed = 1.0f;
	public float duration = 1f;
	private float time = 0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		time += Time.deltaTime;
		float progress = time / duration;
		float progress2 = progress * progress;

		Vector3 scale = gameObject.transform.localScale;
		scale.x += speed * Time.deltaTime * (1f - progress2);
		scale.y += speed * Time.deltaTime * (1f - progress2);
		gameObject.transform.localScale = scale;

		Color color = renderer.material.color;
		color.a = Mathf.Max(0f, 1.0f - 3f * progress2);
		gameObject.renderer.material.color = color;

		if (progress >= 1.0f)
			Destroy (gameObject);
	}
}
