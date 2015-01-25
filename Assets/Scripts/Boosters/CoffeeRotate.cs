using UnityEngine;
using System.Collections;

public class CoffeeRotate : MonoBehaviour {

    private float m_speed = 10;
    void Start()
    {
    }
	// Update is called once per frame
	void Update () {
        transform.localRotation *= Quaternion.AngleAxis(m_speed * Time.smoothDeltaTime, Vector3.up);
	}
}
