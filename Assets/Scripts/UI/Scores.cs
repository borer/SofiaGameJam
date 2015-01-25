using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Scores : MonoBehaviour {

	public Transform player;

	private Text scoreText;
    private float highestSpeed = 0;
	void Start () {
		scoreText = GetComponent<Text>();
	}
	
	void Update () {
        highestSpeed = Mathf.Max(GameObject.FindGameObjectWithTag("Player").GetComponent<MovementManager>().Speed);
		scoreText.text = "" + highestSpeed;
	}
}
