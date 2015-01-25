using UnityEngine;
using System.Collections;

public class ScaleToDOOM : MonoBehaviour {

	public RectTransform scaleIndicator;
	public Transform asteroid;
	public Transform player;

	private float maxHeight;

	void Start() {
		maxHeight = asteroid.position.y;
	}

	void LateUpdate () {
		var percent =  (player.position.y / maxHeight) * 100;
	}
}
