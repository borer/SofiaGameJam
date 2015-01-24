using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BackgroundObjectGen : MonoBehaviour {

    public float m_leftLimit = -20;
    public float m_rightLimit = 20;
    public float m_generationOffset = 25;
    public float m_startInterval = 2;
    public float m_endInterval = 10;
    private float m_timer = 0;
    public GameObject[] m_prefabs;
    protected List<GameObject> m_objects = new List<GameObject>();
    public float m_begin = 0;
    public float m_end = 1000;
    
    void Start () {
        m_timer = m_startInterval;
	}

    protected virtual bool Generate()
    {
        float pos = Camera.main.transform.position.y;
        if (pos < m_begin || pos > m_end)
            return false;
        m_timer -= Time.deltaTime;
        bool shouldGenerate = m_timer < 0;

        if (shouldGenerate)
        {
            m_timer = Mathf.Lerp(m_endInterval, m_startInterval, Mathf.Clamp01((m_end - (m_begin + pos)) / (m_end - m_begin)));
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
        if (Generate())
        {
            m_objects.Add(makeObject(caculatePos()));
        }

        for (int i = 0; i < m_objects.Count; i++)
        {
            if (m_objects[i].transform.position.y < Camera.main.transform.position.y - 100)
            {
                Destroy(m_objects[i]);
                m_objects.RemoveAt(i--);
            }
        }

	}
}
