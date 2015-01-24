using UnityEngine;
using System.Collections;

public class BottleRotate : MonoBehaviour {

    public float m_speed = 10;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation *= Quaternion.AngleAxis(m_speed * Time.smoothDeltaTime, Vector3.right);
	}
}
