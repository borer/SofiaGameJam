using UnityEngine;
using System.Collections;

public class CutSceneController : MonoBehaviour
{

    public float m_animTime = 8;
    public string m_nextScene;
	void Start () {
        StartCoroutine(WaitForAnimation());
	}

    private IEnumerator WaitForAnimation()
    {
        yield return new WaitForSeconds(m_animTime);
        Application.LoadLevel(m_nextScene);
    }
	// Update is called once per frame
	void Update () {
	
	}
}
