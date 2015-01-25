using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BackgoundLayer : MonoBehaviour {


    public GameObject m_prefab;
    private List<GameObject> m_planes = new List<GameObject>();
    private const float MARGIN = 0.2f;
    public Material[] m_skyColours;
    public Material[] m_transitions;
    public float[] m_heights;
    private int m_heightIndex = 0;
    private float m_offset = 0;
    private float m_maxDistance = 0;
    public float m_direction = 1;
	void Start () 
    {
        var go = (GameObject)Instantiate(m_prefab, transform.position, Quaternion.identity);
        float maxY = float.MinValue;
        float minY = float.MaxValue;
        for (int i = 0; i < go.GetComponent<MeshFilter>().mesh.vertexCount; i++)
        {
            if (go.GetComponent<MeshFilter>().mesh.vertices[i].y > maxY)
                maxY = go.GetComponent<MeshFilter>().mesh.vertices[i].y;
            if (go.GetComponent<MeshFilter>().mesh.vertices[i].y < minY)
                minY = go.GetComponent<MeshFilter>().mesh.vertices[i].y;
        }

        m_offset = m_direction * (maxY - minY) * go.transform.localScale.y;
        m_planes.Add(go);

        m_maxDistance = Mathf.Abs(transform.position.y - GameObject.FindGameObjectWithTag("Asteroid").transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
 
        var last = m_planes[m_planes.Count - 1];

        float size = m_direction * last.transform.localScale.y;
        float top = Camera.main.transform.position.y + m_direction * 25;
        if ((m_direction > 0 && last.transform.position.y + (size * MARGIN) / 2 < top) ||
            (m_direction < 0 && last.transform.position.y + (size * MARGIN) / 2 > top))
        {
            Vector3 offset = new Vector3(0, m_offset, 0);

            var go = (GameObject)Instantiate(m_prefab, last.transform.position + offset, Quaternion.identity);

            if (m_direction > 0)
            {
                if (m_heightIndex < m_heights.Length && go.transform.position.y > m_heights[m_heightIndex] * m_maxDistance)
                    go.renderer.material = m_transitions[m_heightIndex++];
                else
                    go.renderer.material = m_skyColours[m_heightIndex];
            }
            else
            {
                float distance = Mathf.Abs(go.transform.position.y - GameObject.FindGameObjectWithTag("Asteroid").transform.position.y);
                if (m_heightIndex < m_heights.Length && distance < (1.0f - m_heights[m_heightIndex]) * m_maxDistance)
                    go.renderer.material = m_transitions[m_heightIndex++];
                else
                    go.renderer.material = m_skyColours[m_heightIndex];
            }
            m_planes.Add(go);            
            if (m_planes.Count > 4)
            {
                Destroy(m_planes[0]);
                m_planes.RemoveAt(0);
            }

        }
	}
}
