using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartGame : MonoBehaviour
{
	public void RetryGame()
	{
		// Destroy the player health and stamina instance
		PlayerHealth.Instance.ResetHealth();
		Stamina.Instance.ResetStamina();
		EconomyManager.Instance.ResetEconomy();
		SceneManager.LoadScene(1);
	}
}
