using UnityEngine;
using System.Collections;

public class RocketScript : MonoBehaviour {


    private float m_speed;
	void Start () {
        m_speed = Random.Range(1, 10);
	
	}
	

    void Update () {
        transform.Translate(0, m_speed * Time.smoothDeltaTime, 0);
	}
}
