using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class CloudGeneration :  BackgroundObjectGen {

    protected override GameObject makeObject(Vector3 pos) 
    {
        return (GameObject)Instantiate(m_prefabs[Random.Range(0, m_prefabs.Length)], pos, Quaternion.AngleAxis(-90, Vector3.right));
    }

   
}
