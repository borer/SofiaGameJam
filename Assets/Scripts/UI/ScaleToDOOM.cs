using UnityEngine;
using System.Collections;

public class ScaleToDOOM : MonoBehaviour {

	public RectTransform scaleOstrichIndicator;
	public RectTransform scaleAsteroidIndicator;
	public Transform asteroid;
	public Transform player;

	private float targetHeight;
	private float scaleMinHeight;
	private float scaleWidth;

	void Start() {
		targetHeight = asteroid.position.y;
		scaleMinHeight = scaleOstrichIndicator.anchoredPosition.y;
		scaleWidth = scaleOstrichIndicator.anchoredPosition.y - scaleAsteroidIndicator.anchoredPosition.y;
	}

	void LateUpdate () {
		var progress = (player.position.y / targetHeight);
		var oldPosition = scaleOstrichIndicator.anchoredPosition;
		scaleOstrichIndicator.anchoredPosition = new Vector2(oldPosition.x, scaleMinHeight - progress * scaleWidth );
	}
}
