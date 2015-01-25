using UnityEngine;
using System.Collections;

public class CreditsButton : MonoBehaviour {
	
	public void goToCredits () {
		Application.LoadLevel ("CreditsScene");
	}
}
