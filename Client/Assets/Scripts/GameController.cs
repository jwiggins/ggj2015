using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject button;
	public GameObject canvas;

	Communicator communicator;

	void Awake()
	{
		communicator = GameObject.FindObjectOfType<Communicator>();
	}

	void OnGUI()
	{
		if (GUI.Button(new Rect(10, 10, 120, 50), "Reconnect"))
		{
			communicator.Reconnect();
		}
	}
	
	public void ButtonPushed()
	{
		communicator.Attack();
		_setRandomButtonPosition();
	}
	
	void _setRandomButtonPosition()
	{
		RectTransform screenTransform = (RectTransform)canvas.transform;
		RectTransform buttonTransform = (RectTransform)button.transform;
		Rect screenRect = screenTransform.rect;
		Rect buttonRect = buttonTransform.rect;
		Vector3 buttonPos = buttonTransform.position;
		float halfWidth = buttonRect.width / 2.0f;
		float halfHeight = buttonRect.height / 2.0f;
		float newX = Random.Range(halfWidth, screenRect.width - halfWidth);
		float newY = Random.Range(halfHeight, screenRect.height - halfHeight);
		
		button.transform.position = new Vector3(newX, newY, buttonPos.z);
	}
}