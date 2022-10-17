using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

using static SnakeController;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    private bool isPaused = false;
    private bool isDisplayingIcon = false;
    private float timer = 10f;
    private int coinCount = 0;
    public GameObject objectCheck;
    private bool isGameOver = false;

    [Header("Variables")]
    [SerializeField] float levelTimerCount = 30;

    PlayerController playerController;
    InputAction pauseButton;

    [Header("Panels")]
    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject sheepIconUI;
    [SerializeField] GameObject LevelTimer;

    [Header("Text Gameobject")]
    [SerializeField] TextMeshProUGUI coinTxt;
    [SerializeField] TextMeshProUGUI timerTxt;
    [SerializeField] TextMeshProUGUI endPanelDialogueBox;
    [SerializeField] TextMeshProUGUI levelTimerTxt;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            playerController = new PlayerController();
            pauseButton = playerController.UI.Pause;
            pauseButton.Enable();
            pauseButton.performed += PauseCheck;
        }
        else
        {
            Destroy(this);
        }

    }
    private void OnDisable()
    {
        pauseButton.Disable();
    }
    void Start()
    {
        inGamePanel.SetActive(true);
        pausePanel.SetActive(false);
        GameOverPanel.SetActive(false);
        if (objectCheck == null)
        {
            sheepIconUI.SetActive(true);
        }
        if (objectCheck != null)
        {
            LevelTimer.SetActive(true);
        }

    }
    private void OnEnable()
    {
        objectCheck = GameObject.FindGameObjectWithTag("Multiplayer");
    }
    public void StartDisplayIcon()
    {
        isDisplayingIcon = true;
    }
    public void StopDisplayingIcon()
    {
        isDisplayingIcon = false;
    }

    void Update()
    {
        PauseMenu();
        if(objectCheck == null) DisplayIcon();
        if (objectCheck != null) LevelTimerCount();
    }

    private void LevelTimerCount()
    {
        levelTimerTxt.color = levelTimerCount > 10 ? Color.green : Color.red;
        levelTimerTxt.text = levelTimerCount.ToString("0.00");
        if(levelTimerCount > 0)
        {
            levelTimerCount -= Time.deltaTime;
        }
        else
        {
            GameOverScenarioFourTimeOut();
        }
    }
    private void DisplayIcon()
    {
        if (isDisplayingIcon)
        {
            sheepIconUI.SetActive(true);
            timerTxt.color =(timer < 3 ) ? Color.red : Color.green;
            timerTxt.text = timer.ToString("0.00");
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                timer = 10f;
                isDisplayingIcon = false;

            }
        }
        else
        {
            sheepIconUI.SetActive(false);
            timer = 10f;
        }
    }
    private void PauseMenu()
    {
        if (isPaused || isGameOver)
        {
            Time.timeScale = 0;
            if (isPaused)
            {
            inGamePanel.SetActive(false);
            pausePanel.SetActive(true);
            }
        }
        else
        {
            Time.timeScale = 1;
            inGamePanel.SetActive(true);
            pausePanel.SetActive(false);
        }
        
    }
    // Scenario 1. Player collided with the wall will die and the opposite player will win.
    public void GameOverScenarioOneWithBoundary(characterTypes character)
    {
        GameOver();
        if (objectCheck != null)
        {
            int tempValue = MultiplayerManager.Instance.getPlayer2Score();
            if (character == characterTypes.player1)
            {
                endPanelDialogueBox.text = "Player 1 lost and Player 2 won win with a score of : " + tempValue.ToString();
            }
            else if (character == characterTypes.player2)
            {
                endPanelDialogueBox.text = "Player 2 lost and Player 1 won win with a score of : " + coinCount.ToString();
            }
        }
        else
        {
            endPanelDialogueBox.text = "Your total score is : " + coinCount.ToString();
        }
    }
    // Scenario 2. where player 1 eats player 2 or viceversa.
    public void GameOverScenarioTwoEating(characterTypes character)
    {
        GameOver();
        if (objectCheck != null)
        {
            int tempValue = MultiplayerManager.Instance.getPlayer2Score();
            if (character == characterTypes.player1)
            {
                endPanelDialogueBox.text = "Player 2 got eaten by Player 1 and Player 1 total score : " + coinCount.ToString();
            }
            else if (character == characterTypes.player2)
            {
                endPanelDialogueBox.text = "Player 1 got eaten by Player 2 and Player 2 total score : " + tempValue.ToString();
            }
        }
        
    }
    // Senario 3. If player eats his own body therefore opponent should win and viceversa.
    public void GameOverScenarioThreePlayerEatsHisOwnBody(characterTypes character)
    {
        GameOver();
        if (objectCheck != null)
        {
            int tempValue = MultiplayerManager.Instance.getPlayer2Score();
            if (character == characterTypes.player1)
            {
                endPanelDialogueBox.text = "Player 1 ate his own body, So Player 2 won with a total score : " + tempValue.ToString();
            }
            else if(character == characterTypes.player2)
            {
                endPanelDialogueBox.text = "Player 2 ate his own body, So Player 1 won with a total score : " + coinCount.ToString();
            }
        }
        else
        {
            endPanelDialogueBox.text = "Your total score is : " + coinCount.ToString();
        }
    }

    // Scenario 4. where game gets over after certain time has passed.
    private void GameOverScenarioFourTimeOut()
    {
        GameOver();
        int tempValue = MultiplayerManager.Instance.getPlayer2Score();
        if (coinCount == tempValue)
        {
            endPanelDialogueBox.text = "It's a draw between the player with a tie score of : " + coinCount.ToString();
        }
        else if (coinCount > tempValue)
        {
            endPanelDialogueBox.text = "Player 1 won with a score of : " + coinCount.ToString();
        }
        else if (coinCount < tempValue)
        {
            endPanelDialogueBox.text = "Player 2 won with a score of : " + tempValue.ToString();
        }

    }

    private void GameOver()
    {
        isGameOver = true;
        AudioManager.Instance.PlayEffectSound(SoundTypes.GameOver);
        inGamePanel.SetActive(false);
        pausePanel.SetActive(false);
        GameOverPanel.SetActive(true);
    }


    private void PauseCheck(InputAction.CallbackContext context)
    {
        isPaused = !isPaused; 
    }
    public void IncrementCoinUI(int amount)
    {
        coinCount += amount;
        coinTxt.text = coinCount.ToString();
    }

    public void ReturnToMainMenu()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        Application.Quit();
    }

    public void ResumeGame()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        isPaused = false;
    }
    public void PlayAgain()
    {
        AudioManager.Instance.PlayEffectSound(SoundTypes.ButtonPressed);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }



}
