using UnityEngine;
using System.Collections;

public class PlaneScript : MonoBehaviour {

    private int m_direction = 1;
    private float m_speed = 0;
	void Start () {

        m_direction = Random.Range(0, 1) < 0.5f ? -1 : 1;

        if (m_direction < 0)
            transform.rotation *= Quaternion.AngleAxis(180, Vector3.up);

        m_speed = Random.Range(3, 8);
	}
	
	// Update is called once per frame
	void Update () {
        var pos = transform.position;
        pos.x -= m_direction * m_speed * Time.smoothDeltaTime;
        transform.position = pos;
	
	}
}
