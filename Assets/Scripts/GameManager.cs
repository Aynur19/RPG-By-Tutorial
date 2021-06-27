using UnityEngine;

public class GameManager : MonoBehaviour
{
	public void GameOver()
	{
		Application.Quit();
		Debug.Log("EXIT");
	}
}
