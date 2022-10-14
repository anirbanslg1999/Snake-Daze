using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private static UIManager instance;
    public static UIManager Instance
    {
        get { return instance; }
    }
    private bool isPaused = false;

    PlayerController playerController;
    InputAction pauseButton;

    [SerializeField] GameObject inGamePanel;
    [SerializeField] GameObject pausePanel;
    [SerializeField] GameObject GameOverPanel;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            Debug.Log("Instamce called");
        }
        else
        {
            Destroy(this);
        }
        playerController = new PlayerController();
        pauseButton = playerController.UI.Pause;
        pauseButton.Enable();
        pauseButton.performed += PauseCheck;
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

    }

    void Update()
    {
        PauseMenu();
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


}
