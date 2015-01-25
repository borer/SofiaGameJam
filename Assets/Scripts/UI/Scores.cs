using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

	public Transform player;

	private Text scoreText;
	private float count;
	void Start () {
		scoreText = GetComponent<Text>();
	}
	
	void Update () {
		count++;
		scoreText.text = "" + count;
	}
}
