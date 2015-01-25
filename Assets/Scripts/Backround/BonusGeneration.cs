using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BonusGeneration : BackgroundObjectGen {



    protected override GameObject makeObject(Vector3 pos)
    {
        return (GameObject)Instantiate(m_prefabs[Random.Range(0, m_prefabs.Length)], pos, Quaternion.identity);
    }


    protected override  Vector3 caculatePos()
    {
        Vector3 pos = Camera.main.transform.position;
        pos.y += m_generationOffset;
        pos.z = transform.position.z;
        pos.x += Random.Range(m_leftLimit, m_rightLimit);
        return pos;
    }

}
