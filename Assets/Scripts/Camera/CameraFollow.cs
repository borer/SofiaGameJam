using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	public Vector3 distanceFromObject = new Vector3(0, 1, -1);
    public const float LIMIT = 2;

	public Transform objectToFollow;

    public Vector3 DeltaPos { get { return m_delta; } }
    private Vector3 m_lastPos;
    private Vector3 m_delta;
    private const float MIN_DISTANCE_FACTOR = 0.1f;
    public string m_cutSceneName = "TempScene";
    public float m_startY;
    public int m_direction = 1;
    void Start()
    {
        m_lastPos = transform.position;
        m_startY = transform.position.y;
    }

    void Update()
    {
        m_delta = transform.position - m_lastPos;
        m_lastPos = transform.position;
        
    }

    void LateUpdate()
    {

        var pos = transform.position;
        var playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
        transform.position = new Vector3(pos.x, playerPos.y + m_direction * 1.1f, pos.z);

        var ast = GameObject.FindGameObjectWithTag("Asteroid");
        if (Mathf.Abs(ast.transform.position.y - transform.position.y) < MIN_DISTANCE_FACTOR * Mathf.Abs(ast.transform.position.y - m_startY))
            Application.LoadLevel(m_cutSceneName);
    }


}
