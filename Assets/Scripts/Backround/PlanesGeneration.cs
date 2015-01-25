using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class PlanesGeneration : BackgroundObjectGen
{

    protected override GameObject makeObject(Vector3 pos) 
    {
        var go  = (GameObject)Instantiate(m_prefabs[Random.Range(0, m_prefabs.Length)], pos, Quaternion.identity);
        return go;
    }

   
}
