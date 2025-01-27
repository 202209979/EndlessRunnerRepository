using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseButton;
    [SerializeField] private GameObject pauseMenu;
    private bool pausedGame = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) 
        {
            if (pausedGame)
            {
                ResumeGame();
                pausedGame = false;
            }
            else
            {
                PauseGame();
                pausedGame = true;
            }
        }
    }
    public void PauseGame()
    {
        Time.timeScale = 0f;
        pauseButton.SetActive(false);
        pauseMenu.SetActive(true);
        
    }
    
    public void ResumeGame()
    {
        Time.timeScale = 1f;
        pauseButton.SetActive(true);
        pauseMenu.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Debug.Log("Cerrando juego...");
        Application.Quit();
    }
}
