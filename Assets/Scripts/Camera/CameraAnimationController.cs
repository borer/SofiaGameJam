using UnityEngine;
using System.Collections;

public class CameraAnimationController : MonoBehaviour {

	private Animator animator;
	private bool collectedBooster;
	
	void Start () 
	{
		collectedBooster = false;
		animator = GetComponent<Animator>();
	}

	public void playerCollectedBooster() {
		collectedBooster = true;
	}
	
	// Update is called once per frame
	void Update () {
		animator.SetBool ("collectedBooster", collectedBooster);
		if (collectedBooster) {
			collectedBooster = false;
		}
	}
}
