using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{

    public GameObject StartUI;
    public GameObject InGameUI;
    public GameObject HoldUI;
    public Player player;
    public Text helpText;
    public GameObject OkButton;
    public Button pauseButton;

    // Use this for initialization
    void Start()
    {
        helpText.text = "In this game\nyou are constantly jumping...";
        Time.timeScale = 1f;
        pauseButton.interactable = false;
        StartUI.SetActive(true);
        HoldUI.SetActive(false);
        OkButton.SetActive(true);
        InGameUI.SetActive(false);
    }

    public void OkClick()
    {
        OkButton.SetActive(false);
        helpText.text = "Tap and hold screen now!";
        Time.timeScale = 1f;
        player.currSpeed = 0f;
    }

    public void TapScreen()
    {
        player.currSpeed = player.maxSpeed;
        helpText.text = "Practice!";
    }

    public void FinishTutorial()
    {
        SceneManager.LoadScene(0);
    }

    public void BeginTutorial()
    {
        player.currSpeed = player.maxSpeed;
        StartUI.SetActive(false);
        InGameUI.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.name == "HoldCol")
        {
            HoldUI.SetActive(true);
            helpText.text = "...but you can tap and hold screen to run and prepare\nextra jump!";
            Time.timeScale = 0f;
        }
        else if (col.name == "FinishCol")
        {
            Time.timeScale = 0f;
            HoldUI.SetActive(false);
            helpText.text = "Now you are ready to play\nDon't Jump!\nClick back button on the left...";
        }
    }

}
