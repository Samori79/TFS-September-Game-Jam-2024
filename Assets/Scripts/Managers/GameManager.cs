using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

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

    public StageDataSO[] stages; //array of stage data SOs

    public float levelTimer;
    public int currentScore;
    public int scoreThreshold = 10; //test value! change later
    public float defaultTimer = 60f; //reset to 60 seconds
    public int defaultScore = 0; //reset to 0 


    public GameObject uiCanvas;  // Assign the persistent canvas in the Inspector
    public TMP_Text timerText;  // Reference to UI Text for timer
    public TMP_Text scoreText;  // Reference to UI Text for score
   
    public Slider timerBar;
    public Image timerImage;
    //sprites for squiddy going to bed
    public Sprite phaseOne;
    public Sprite phaseTwo;
    public Sprite phaseThree;
    public Sprite phaseFour;

    //public List<string> stageList;  // List of stage scene names
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

    //this is the start of the campaign, should only be called from a button in main menu
    public void StartStageMode()
    {
        Debug.Log("Starting Stage mode");
        ResetLevel();
        currentState = GameState.Playing;
        uiCanvas.SetActive(true);  // Show the UI when the game starts
        LoadStage(currentStageIndex);
    }

    //a general load scene to ensure game is always reset and to not have to type scene manager every line
    public void LoadScene(string sceneName)
    {

        //we use this instead of scene manager directly
        ResetLevel();
        SceneManager.LoadScene(sceneName);
    }

    
    //specifically for stage mode, to go from one to the next in sucession
    public void LoadStage(int stageIndex)
    {
        Debug.Log("Current index"+ stageIndex + ", CurrentStageIndex:" + currentStageIndex);
        Debug.Log("Current count" + stages.Length);
       // Debug.Log(stageList);


        if (stageIndex < stages.Length)
        {



            //load the stage based on index
            SetStageData();
            LoadScene(stages[stageIndex].stageName);
           

            StageInstructionsPopup.Instance.ShowPopup(stages[stageIndex].popUpMessage, stages[stageIndex].levelName);


        }
        else
        {

            //this should go to win menu 
            VictoryScreen();
        }
    }

    public void SetStageData()
    {
        // Return the score requirement for the current stage
        if (currentStageIndex < stages.Length)
        {
           scoreThreshold = stages[currentStageIndex].scoreRequirement;
            defaultTimer = stages[currentStageIndex].timeLimit;
        }
         // Default to 0 if no valid stage data
    }

    public void ResetLevel()
    {
        //Debug.Log("Resetting Level...");
        levelTimer = defaultTimer; //set time back to 60
        currentScore = defaultScore; //set score to 0
        timerBar.minValue = 0;  // Slider minimum is always 0
        timerBar.maxValue = 1;  // Slider maximum is always 1
        timerBar.value = 1;     // Start the slider full (1 = 100% time remaining)

        UpdateTimerImage();
    }

    //Timer Management

    public void UpdateTimer(float deltaTime)
    {
        levelTimer -= deltaTime;

        if (levelTimer <= 0)
        {
            GameOver();
        }


        timerBar.value = levelTimer / defaultTimer;

        UpdateTimerImage();

    }

    //changes the sprite to match the sleepiness of the lil guy
    public void UpdateTimerImage()
    {
        //Debug.Log("Updating Timer Image");
        float sliderValue = timerBar.value;

        if (sliderValue >= 0.75f)
        {
            timerImage.sprite = phaseOne;
        } else if (sliderValue >= 0.5f)
        {
            timerImage.sprite = phaseTwo;
        }else if (sliderValue >= 0.25f)
        {
            timerImage.sprite = phaseThree;
        }else
        {
            timerImage.sprite= phaseFour;
        }
    }

    //UI Management

    public void UpdateUI()
    {
        // Update timer display (formatted to 1 decimal places)
        timerText.text = "Time: " + levelTimer.ToString("F1");

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
        Debug.Log("Returning to Main menu...");


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

    public void VictoryScreen()
    {
        Debug.Log("Game won!");
        currentState = GameState.GameOver;
        uiCanvas.SetActive(false);
        LoadScene("VictoryScreen");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting the game...");
        Application.Quit();

#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // Stop play mode in the editor
#endif
    }

}
