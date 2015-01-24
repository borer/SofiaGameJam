using UnityEngine;
using System.Collections;

public class AlienFloat : MonoBehaviour {

    public float m_radius = 1;
    public float m_speed = 10;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float offsetX = Mathf.Sin(Time.time * m_speed) * m_radius;
        float offsetY = Mathf.Cos(Time.time * m_speed) * m_radius;
        transform.position += new Vector3(offsetX, offsetY, 0);
	}
}
