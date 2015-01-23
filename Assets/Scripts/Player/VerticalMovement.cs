using UnityEngine;
using System.Collections;

public class VerticalMovement : MonoBehaviour {
	public float normalSpeed = 3f;
	public float slowSpeed = 1.5f;
	public float fastSpeed = 5f;
	
	// Update is called once per frame
	void Update () {
		float horizontalOffset = getHorizontalMovement ();
		float verticalOffset = getVericalMovement();

		transform.Rotate(0, 0, 2 * -horizontalOffset );
		transform.Translate (horizontalOffset, verticalOffset, 0);
	}

	private float getHorizontalMovement() {
		return Input.GetAxis("Horizontal") * Time.deltaTime;
	}

	private float getVericalMovement() {
		return normalSpeed * Time.smoothDeltaTime;
	}
}
