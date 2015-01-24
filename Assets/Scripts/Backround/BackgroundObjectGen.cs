using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BackgroundObjectGen : MonoBehaviour {

    public float m_leftLimit = -20;
    public float m_rightLimit = 20;
    public float m_generationOffset = 25;
    public float m_startInterval = 10;
    public float m_endInterval = 50;
    private float m_distance = 0;
    public GameObject[] m_prefabs;
    protected List<GameObject> m_objects = new List<GameObject>();
    public float m_begin = 0;
    public float m_end = 1000;
    public  float m_interval = 10;

    public float m_verticalSpeedFactor = 0;
    private float m_verticalSpeed = 0;
    void Start () 
    {
        m_distance = m_startInterval;
	}

    protected virtual bool Generate()
    {
        float pos = Camera.main.transform.position.y;
        if (pos < m_begin || pos > m_end)
            return false;
        m_distance -= Camera.main.GetComponent<CameraFollow>().DeltaPos.y + m_verticalSpeed * Time.deltaTime;
        bool shouldGenerate = m_distance < 0;

        if (shouldGenerate)
        {
            m_distance = Mathf.Lerp(m_endInterval, m_startInterval, Mathf.Clamp01((m_end - (m_begin + pos)) / (m_end - m_begin)));
        }
        return shouldGenerate;
    }

    protected virtual GameObject makeObject(Vector3 pos)
    {
        return (GameObject)Instantiate(m_prefabs[0], pos, Quaternion.identity);
    }

    protected virtual Vector3 caculatePos()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.y += m_generationOffset;
        pos.z = transform.position.z - 1;
        pos.x += Random.Range(m_leftLimit, m_rightLimit);
        return pos;
    }

	void Update () 
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        m_verticalSpeed = m_verticalSpeedFactor * player.GetComponent<MovementManager>().Speed;
        if (Generate())
        {
            m_objects.Add(makeObject(caculatePos()));
        }

        for (int i = 0; i < m_objects.Count; i++)
        {
            if(m_objects[i] == null)
                m_objects.RemoveAt(i--);

            else if (m_objects[i].transform.position.y < Camera.main.transform.position.y - 100)
            {
                Destroy(m_objects[i]);
                m_objects.RemoveAt(i--);
            }
            else
            {
                Vector3 pos = m_objects[i].transform.position;
                pos.y += m_verticalSpeed * Time.deltaTime;
                m_objects[i].transform.position = pos;
            }
        }


	}
}
