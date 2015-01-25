using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StarGeneration : BackgroundObjectGen
{

    protected override GameObject makeObject(Vector3 pos) 
    {
        //pos.y -= m_generationOffset;
        var go  = (GameObject)Instantiate(m_prefabs[Random.Range(0, m_prefabs.Length)], pos, Quaternion.AngleAxis(-90, Vector3.right));
        //go.transform.localScale *= Random.Range(0.5f, 10);
        return go;
    }

   
}
