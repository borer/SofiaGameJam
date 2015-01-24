using UnityEngine;
using System.Collections;

public class CameraMover : MonoBehaviour {

    public float m_speed = 1;
	public const float LIMIT = 2;
    
    void Start () 
    {

        //for (int i = 0; i < 1000; i+=10)
        //{
        //    Vector3 pos = new Vector3(0, 0, i);
        //    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(0, TopLimit(i), i), Quaternion.identity);
        //    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(0, BottomLimit(i), i), Quaternion.identity);
        //    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(LeftLimit(i), 0, i), Quaternion.identity);
        //    Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), new Vector3(RightLimit(i), 0, i), Quaternion.identity);
        //}
	
	}

    //public Vector3 distanceFromObject = new Vector3(0, 1, -1);
    public Transform objectToFollow;

    void LateUpdate()
    {
        transform.position = new Vector3(transform.position.x, objectToFollow.position.y, transform.position.z) ;
    }

    //void Update() 
    //{

    //    var pos = transform.position;
    //    pos.y += m_speed * Time.deltaTime;
    //    transform.position = pos;
    //}

    public float TopLimit(float distance)
    {
        return transform.position.y + LIMIT;
        //return transform.position.y + Mathf.Tan(camera camera.fieldOfView / 2 ) * distance;
    }

    public float BottomLimit(float distance)
    {
        return transform.position.y - LIMIT;
        //return transform.position.y - Mathf.Tan(camera.fieldOfView / 2) * distance;
    }

    public float LeftLimit(float distance)
    {
        return transform.position.x - LIMIT;
        //return transform.position.x - Mathf.Tan((camera.aspect * camera.fieldOfView) / 2) * distance;
    }

    public float RightLimit(float distance)
    {
        return transform.position.x + LIMIT;
        //return transform.position.x + Mathf.Tan((camera.aspect * camera.fieldOfView) / 2) * distance;
    }

}
