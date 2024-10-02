using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public enum GameState
{
    MainMenu,
    Playing,
    GameOver
}

public class GameManager : MonoBehaviour
{
    static GameManager _instance;
    public static GameManager Instance => _instance; //Important! This allows any script to refer to the game manager using GameManager.Instance. No need to make a reference to it

    public GameState currentState = GameState.MainMenu; //default to main menu


    public float levelTimer;
    public int currentScore;
    public int scoreThreshold = 10; //test value! change later
    public float defaultTimer = 60f; //reset to 60 seconds
    public int defaultScore = 0; //reset to 0 


    public GameObject uiCanvas;  // Assign the persistent canvas in the Inspector
    public TMP_Text timerText;  // Reference to UI Text for timer
    public TMP_Text scoreText;  // Reference to UI Text for score

    public List<string> stageList;  // List of stage scene names
    private int currentStageIndex = 0; // Track the current stage


    //leaderboard (localized)
    private List<int> devHighScores = new List<int>(); //store our dev scores for the player to compare to
    public List<int> playerHighScores = new List<int>(); // player leaderboard
    // Start is called before the first frame update



    //singleton
    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        ResetLevel();
        uiCanvas.SetActive(false);  // Hide UI initially in the main menu

    }


    private void Update()
    {
        //this will only update timer and ui if game is playing
        if (currentState == GameState.Playing)
        {
            UpdateTimer(Time.deltaTime); //update timer 
            UpdateUI(); //update UI elements to track score and time
        }
    }

    // Level Management

    public void StartStageMode()
    {
        ResetLevel();
        currentState = GameState.Playing;
        uiCanvas.SetActive(true);  // Show the UI when the game starts
        LoadStage(currentStageIndex);
    }
    public void LoadScene(string sceneName)
    {

        //we use this instead of scene manager directly
        ResetLevel();
        SceneManager.LoadScene(sceneName);
    }

    public void LoadStage(int stageIndex)
    {
        if (stageIndex < stageList.Count)
        {
            //load the stage based on index
            LoadScene(stageList[stageIndex]);
        }else
        {
            GoToMainMenu();
        }
    }


    public void ResetLevel()
    {
        levelTimer = defaultTimer; //set time back to 60
        currentScore = defaultScore; //set score to 0
    }

    //Timer Management

    public void UpdateTimer(float deltaTime)
    {
        levelTimer -= deltaTime;

        if (levelTimer <= 0)
        {
            GameOver();
        }
    }

    //UI Management

    public void UpdateUI()
    {
        // Update timer display (formatted to 2 decimal places)
        timerText.text = "Time: " + levelTimer.ToString("F2");

        // Update score display
        scoreText.text = "Score: " + currentScore.ToString() + "/" + scoreThreshold.ToString();


    }

    //Score Management

    public void AddScore(int score)
    {
        currentScore += score;
        if (currentScore >= scoreThreshold) 
        {
            WinLevel();
        }

    }

    public void GoToMainMenu()
    {
        currentState = GameState.MainMenu;
        currentStageIndex = 0; //reset stage index
        
        uiCanvas.SetActive(false);  // Hide the UI when returning to the main menu
        LoadScene("MainMenu");
    }




    //Win/lose logic

    //TODO: Make this reference the list of stages we have and cycle through them until the player beats them all.
    public void WinLevel()
{
    Debug.Log("Level won! Loading next level...");

        //Move to the next stage
        currentStageIndex++;
        LoadStage(currentStageIndex);
    
   
}
    public void GameOver()
    {
        Debug.Log("Game Over!");
        currentState = GameState.GameOver;
        uiCanvas.SetActive(false);  // Hide the UI when returning to the main menu
        LoadScene("GameOver");
    }

}
