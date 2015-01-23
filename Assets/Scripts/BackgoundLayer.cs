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

        m_offset = (maxY - minY) * go.transform.localScale.y;
        Debug.Log(minY);
        Debug.Log(maxY);

        m_planes.Add(go);
	}
	
	// Update is called once per frame
	void Update () {
 
        var last = m_planes[m_planes.Count - 1];

        float size = last.transform.localScale.y;

        if (last.transform.position.y + (size * MARGIN) / 2 < Camera.main.GetComponent<CameraMover>().TopLimit(0))
        {
            Vector3 offset = new Vector3(0, m_offset, 0);

            var go = (GameObject)Instantiate(m_prefab, last.transform.position + offset, Quaternion.identity);
            if (m_heightIndex < m_heights.Length && go.transform.position.y > m_heights[m_heightIndex])
                go.renderer.material = m_transitions[m_heightIndex++];  
            else
                go.renderer.material = m_skyColours[m_heightIndex];


            m_planes.Add(go);            
            if (m_planes.Count > 4)
            {
                Destroy(m_planes[0]);
                m_planes.RemoveAt(0);
            }

        }
	}
}
