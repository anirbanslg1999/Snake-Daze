using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    PlayerController playerController;
    InputAction pauseButton;

    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject GameOverPanel;
    [SerializeField] GameObject sheepIconUI;
    [SerializeField] TextMeshProUGUI coinTxt;

    [SerializeField] TextMeshProUGUI timerTxt;
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
        sheepIconUI.SetActive(false);

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
        DisplayIcon();
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
        if (isPaused)
        {
            inGamePanel.SetActive(false);
            pausePanel.SetActive(true);
            GameOverPanel.SetActive(false);
        }
        else
        {
            inGamePanel.SetActive(true);
            pausePanel.SetActive(false);
            GameOverPanel.SetActive(false);
        }
        
    }

    public void GameOver()
    {
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
        SceneManager.LoadScene(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ResumeGame()
    {
        isPaused = false;
    }


}
