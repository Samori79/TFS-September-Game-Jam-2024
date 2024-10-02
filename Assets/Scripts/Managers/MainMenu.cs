using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button stageButton; // Reference to the button

    private void Start()
    {
        // Find the GameManager if not assigned in the Inspector
        GameManager manager = GameManager.Instance;

        if (stageButton != null)
        {
            stageButton.onClick.AddListener(manager.StartStageMode); // Add listener programmatically
        }
        else
        {
            Debug.LogWarning("Start Button not assigned in the Inspector.");
        }
    }

    public void QuitGame()
    {
        Application.Quit(); // Quit the application
    }
}
