using UnityEngine;
using System.Collections;

public class AsteroidScript : MonoBehaviour {

	// Use this for initialization
    private Vector3 m_direction;
    private float m_speed;
	void Start () {

        m_direction = Quaternion.AngleAxis(Random.Range(-10, 10), Vector3.forward) * Vector3.down;
        m_direction.Normalize();
        m_speed = Random.Range(10, 20);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(m_direction * m_speed * Time.smoothDeltaTime);
	
	}
}
