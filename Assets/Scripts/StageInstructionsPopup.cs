using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StageInstructionsPopup : MonoBehaviour
{
    public static StageInstructionsPopup Instance { get; private set; }

    public GameObject popupPanel;  // Reference to the pop-up panel
    public TMP_Text levelTitleText;
    public TMP_Text instructionText; // Reference to the instruction text
    public Button okButton;  // Reference to the OK button

    private void Awake()
    {
        // Implement Singleton Pattern
        if (Instance == null)
        {
            Instance = this; // Assign this instance
            DontDestroyOnLoad(gameObject); // Optional: Keep it alive across scenes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate
        }

        // Ensure the pop-up is hidden at the start
        popupPanel.SetActive(false);
        okButton.onClick.AddListener(OnOkButtonClicked);
    }

    // Call this method to show the pop-up
    public void ShowPopup(string message, string title)
    {
        // Show the pop-up and set the instruction text
        popupPanel.SetActive(true);
        levelTitleText.text = "Level:" + title;
        instructionText.text = message;
        Time.timeScale = 0;  // Pause the game
    }

    private void OnOkButtonClicked()
    {
        // Hide the pop-up and resume the game
        popupPanel.SetActive(false);
        Time.timeScale = 1;  // Resume the game
    }
}