using UnityEngine;
using System.Collections;

public class ExitButton : MonoBehaviour {
	
	public void goToMenu () {
		Application.LoadLevel ("OpeningScene");
	}
}
