using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button stageButton; // Reference to the button
    public Button quitButton;

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

        if (quitButton != null)
        {
            quitButton.onClick.AddListener(manager.QuitGame); // Add listener programmatically
        }
        else
        {
            Debug.LogWarning("Quit Button not assigned in the Inspector.");
        }
    }

    
}
