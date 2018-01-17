using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

[RequireComponent(typeof(AudioSource))]
public class MainManager : MonoBehaviour {

    public int distancePoint;
    public int distance;
    public int bestDistance;
    public Transform playerT;
    public Player player;

    public GameObject PauseUI;
    public GameObject StartUI;
    public GameObject InGameUI;
    public GameObject FinishUI;
    public GameObject HelpUI;

    public Text distanceText;
    public Text startBestText;
    public Text finishBestDistanceText;
    public Text finishDistanceText;

    public GameObject recordBoardPref;

    //public AdsManager am;
    private bool logged;

    private AudioSource merry;
    private AudioSource jingle;
    public Image MusicButtonIm;
    public Button MusicButton;
    public Sprite[] musicS;
    public Sprite[] musicC;
    

    // Use this for initialization

    void Awake()
    {
        jingle = GameObject.Find("MusicHolder").GetComponent<AudioSource>();
        merry = GetComponent<AudioSource>();
        if (!PlayerPrefs.HasKey("Volume"))
        {
            PlayerPrefs.SetInt("Volume", 1);
        }
        else
        {
            merry.volume = jingle.volume = PlayerPrefs.GetInt("Volume");
            ChangeMusicSprites();
        }

    }

    void Start () {
        Time.timeScale = 1f;
        distance = 0;
        distancePoint = 500;
        PauseUI.SetActive(false);
        InGameUI.SetActive(false);
        StartUI.SetActive(true);
        FinishUI.SetActive(false);
        HelpUI.SetActive(false);
        if(!PlayerPrefs.HasKey("skippedTutorial"))
            HelpUI.SetActive(true);
        bestDistance = PlayerPrefs.GetInt("BestDistance", 0);
        if(bestDistance != 0)
        {
            Instantiate(recordBoardPref, new Vector3(bestDistance, 0, 0), Quaternion.identity);
        }
        startBestText.text = bestDistance + "m";
    }
	
	// Update is called once per frame
	void Update () {
        if (player.transform.position.x > distance)
            distance = (int)player.transform.position.x;
        distanceText.text = distance + "m";
	}

    void ChangeMusicSprites()
    {
        MusicButtonIm.sprite = musicS[(int)merry.volume];
        SpriteState st = new SpriteState();
        st.disabledSprite = st.highlightedSprite = st.pressedSprite = musicC[(int)merry.volume];
        MusicButton.spriteState = st;
    }

    public void FinishRound()
    {
        Time.timeScale = 0f;
        if (distance > bestDistance)
        {
            PlayerPrefs.SetInt("BestDistance", distance);
            bestDistance = distance;
        }
        finishBestDistanceText.text = bestDistance + "m";
        finishDistanceText.text = distance + "m";
        PauseUI.SetActive(false);
        InGameUI.SetActive(false);
        StartUI.SetActive(false);
        FinishUI.SetActive(true);
    }


    public void Pause()
    {
        Time.timeScale = 0f;
        PauseUI.SetActive(true);

    }

    public void OpenTurorial()
    {
        PlayerPrefs.SetInt("skippedTutorial", 1);
        SceneManager.LoadScene(1);
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void Resume()
    {
        Time.timeScale = 1f;
        PauseUI.SetActive(false);
    }

    public void MuteButton()
    {
        PlayerPrefs.SetInt("Volume", 1 - PlayerPrefs.GetInt("Volume"));
        merry.volume = jingle.volume = PlayerPrefs.GetInt("Volume");
        ChangeMusicSprites();
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame()
    {
        player.currSpeed = player.maxSpeed;

        PauseUI.SetActive(false);
        InGameUI.SetActive(true);
        StartUI.SetActive(false);
        FinishUI.SetActive(false);
        merry.Play();
    }

    public void ShowLead()
    {
        EventSystem.current.SetSelectedGameObject(null);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SkipTutorial()
    {
        HelpUI.SetActive(false);
        PlayerPrefs.SetInt("skippedTutorial", 1);
    }

}
