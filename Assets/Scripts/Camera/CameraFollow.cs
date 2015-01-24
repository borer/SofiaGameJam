using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Vector3 distanceFromObject = new Vector3(0, 1, -1);
	public Transform objectToFollow;
	
	void LateUpdate () {
		transform.position = objectToFollow.position + distanceFromObject;
	}
}
