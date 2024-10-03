using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameOverMenu : MonoBehaviour
{
    public Button menuButton;
    // Start is called before the first frame update
    void Start()
    {
        GameManager manager = GameManager.Instance;

        if (menuButton != null)
        {
            menuButton.onClick.AddListener(manager.GoToMainMenu); // Add listener programmatically
        }
        else
        {
            Debug.LogWarning("Start Button not assigned in the Inspector.");
        }
    }



    // Update is called once per frame
    void Update()
    {
        
    }
}
