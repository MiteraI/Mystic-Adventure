using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string sceneTransitionName;
    [SerializeField] private TextMeshProUGUI messageText; // Reference to the TextMeshProUGUI component in the UI Canvas

    private float waitToLoadTime = 1f;
    private float messageDuration = 5f; // Duration in seconds for the message to display

    private Dictionary<string, int> coinsRequiredPerScene = new Dictionary<string, int>()
    {
        { "Scene1", 1 },
        { "Scene2", 2 },
        { "Scene3", 2 },
        { "Scene4", 2 },
        { "Scene5", 5 },
        // Add more scenes and coin requirements as needed
    };

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            int currentCoins = EconomyManager.Instance.GetCurrentGold();

            // Check if the player has enough coins for this specific scene transition
            if (currentCoins >= coinsRequiredPerScene[SceneManager.GetActiveScene().name])
            {
                Debug.Log("Player has enough coins to transition to the next scene.");
                SceneManagement.Instance.SetTransitionName(sceneTransitionName);
                UIFade.Instance.FadeToBlack();
                StartCoroutine(LoadSceneRoutine());
            }
            else
            {
                Debug.Log("Player does not have enough coins to transition to the next scene.");
                // Display a message indicating the requirement for coins in the TextMeshProUGUI component
                messageText.text = "You must gain at least " + coinsRequiredPerScene[SceneManager.GetActiveScene().name] + " coins to move to the next ground.";
                StartCoroutine(ClearMessageAfterDelay());
            }
        }
    }

    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        Debug.Log("Loading next scene: " + sceneToLoad);
        SceneManager.LoadScene(sceneToLoad);
    }

    private IEnumerator ClearMessageAfterDelay()
    {
        yield return new WaitForSeconds(messageDuration);
        Debug.Log("Clearing message text.");
        messageText.text = ""; // Clear the text after the specified duration
    }
}
