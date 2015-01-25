using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class StarGeneration : BackgroundObjectGen
{

    protected override GameObject makeObject(Vector3 pos) 
    {
        var go  = (GameObject)Instantiate(m_prefabs[Random.Range(0, m_prefabs.Length)], pos, Quaternion.AngleAxis(-90, Vector3.right));
        go.transform.localScale *= Random.Range(1, 3);
        return go;
    }

   
}
