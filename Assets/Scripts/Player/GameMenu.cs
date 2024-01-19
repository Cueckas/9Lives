using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public GameObject menuPanel;

    public GameObject collectionPanel;

    private bool isPaused = false;

    private bool collectionOpen = false;


    void Start()
    {
        // Hide the menu panel at the start
        SetMenuPanelActive(false);

    }

    void Update()
    {
        // Check for input to toggle the menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                // If the menu is already active, close it
                CloseMenu();
            }
            else
            {
                // If the menu is not active, open it
                ToggleMenu();
            }
        }
    }

    void ToggleMenu()
    {
        // Toggle the menu panel and pause/resume the game
        isPaused = !isPaused;
        if (collectionOpen)
        {
            ToggleCollection();
        }
        SetMenuPanelActive(isPaused);
        Time.timeScale = isPaused ? 0f : 1f;

        // Update sliders based on the values from YourScript
    }

    public void ToggleCollection(){

        collectionOpen = !collectionOpen;
        collectionPanel.SetActive(collectionOpen);

    }

    void SetMenuPanelActive(bool active)
    {
        // Enable or disable the menu panel
        menuPanel.SetActive(active);
    }

    // Call this method from the "Resume" button in the UI
    public void ResumeGame()
    {
        ToggleMenu();
    }

    // Call this method from the "Close" button in the UI
    public void CloseMenu()
    {
        if (isPaused)
        {
            ToggleMenu();
        }
    }

    public void BackMain()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        isPaused = !isPaused;
        Time.timeScale = isPaused ? 0f : 1f;
        SceneManager.LoadScene(1);
    }
}

