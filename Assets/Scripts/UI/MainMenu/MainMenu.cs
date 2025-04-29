using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public float delayBeforeLoad = 1f; // Adjust to match button sound length

    // Called when Play button is clicked
    public void PlayGame()
    {
        // Start coroutine to delay scene load
        StartCoroutine(DelayedLoad());
    }

    // Called when Quit button is clicked
    public void QuitGame()
    {
        Debug.Log("Quit Game"); // Shows in editor
        Application.Quit();     // Quits in build
    }

    private System.Collections.IEnumerator DelayedLoad()
    {
        yield return new WaitForSeconds(delayBeforeLoad);
        SceneManager.LoadScene("Testing Main");
    }
}
