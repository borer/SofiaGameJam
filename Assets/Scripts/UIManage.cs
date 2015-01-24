using UnityEngine;
using System.Collections;

public class UIManage : MonoBehaviour {
	  

		
		public Animator startButton;
		public Animator exitButton;
		
		public void OpenSettings()
		{
			startButton.SetBool("isHidden", true);
			exitButton.SetBool("isHidden",true);
		}
	}
