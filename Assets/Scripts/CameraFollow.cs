using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Vector3 distanceFromObject = new Vector3(0, 1, -1);
    public const float LIMIT = 2;

	public Transform objectToFollow;

    public Vector3 DeltaPos { get { return m_delta; } }
    private Vector3 m_lastPos;
    private Vector3 m_delta;
    void Start()
    {
        m_lastPos = transform.position;
    }

    void Update()
    {
        m_delta = transform.position - m_lastPos;
        m_lastPos = transform.position;
    }

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, objectToFollow.position.y + 1, transform.position.z);
    }


}
