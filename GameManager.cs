/*
 * Graffiti Softwerks 2022
 * Game Manager.cs
 * Author: Nash Ali
 * Creation Date: 04-10-2022
 * Last Update: 04-30-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 * 
 * Defines how the program flows from the various scenes.
 * gameplay mechanics can be created and adjusted from here.
 *****************************************************/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.IO;
using UnityEngine.Networking;
using UnityEngine.AdaptivePerformance;
#if UNITY_EDITOR
using UnityEditor;
#endif



public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("Game Manager is null.");
            }
            return _instance;
        }
    }
    public SoundManager soundManager;
    public GameObject player, bullet, bomb;
    public List<GameObject> enemies;
    public float difficulty = 1;
    public int _scoreValue;
    private int cowsNumber = 0 , bullsNumber = 0;
    [SerializeField]
    public bool isGameActive, isPlayerAlive = false;
    
    [SerializeField]
    private GameObject InGameMenu;
    [SerializeField]
    public TextMeshProUGUI scoreText, levelText, myTime;
    [SerializeField]
    private Button startButton, restartButton, stopButton, continueButton;
    [SerializeField]
    private RawImage gameOverImage;
    string stringToSave;
    // Frequency of spawning, this will get smaller as difficulty increases.
    private float spawnRate = 10.0f;
    //200 meters range to start and it decrements as difficulty increases ie they get closer.
    private float spawnRange = 50;
    private IAdaptivePerformance ap;
    #region ******************* Monobehaviour ********************
    private void Awake()
    {
        Input.backButtonLeavesApp = true;
    }
    void Start()
    {
        Debug.Log("GAME MANAGER: game started - menu buttons ready!");
        CheckAP();
        player.GetComponent<GameObject>();
        startButton.GetComponent<Button>();
        restartButton.GetComponent<Button>();
        stopButton.GetComponent<Button>();
        continueButton.GetComponent<Button>();
        myTime.GetComponent<Text>();
        gameOverImage.GetComponent<RawImage>();
        //add listeners for game menu.
        startButton.onClick.AddListener(StartGame);
        stopButton.onClick.AddListener(StopGame);
        continueButton.onClick.AddListener(ContinueGame);
        restartButton.onClick.AddListener(RestartGame);
    }
    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        ShowTime();
    }

    #endregion *********************


    #region ****************** User Code *******************************
    void CheckAP()
    {
        ap = Holder.Instance;
        if (ap == null || !ap.Active)
        {
            Debug.Log("[AP ClusterInfo] Adaptive Performance not active.");
            return;
        }
        if (!ap.SupportedFeature(UnityEngine.AdaptivePerformance.Provider.Feature.ClusterInfo))
        {
            Debug.Log("[AP ClusterInfo] Feature not supported.");
        }
        _ = ap.PerformanceStatus.PerformanceMetrics.ClusterInfo;
    }


    /// <summary>
    /// When the game starts, these are the items that need to be initialized correctly.
    /// This is done when the game start has been pressed or a restart is called.
    /// </summary>
    public void StartGame()
    {
        Debug.Log("GAME MANAGER: game running - player pressed start!");
        isPlayerAlive = true;
        isGameActive = true;
        _scoreValue = 0;
        UpdateScore(0);
        spawnRate /= difficulty;
        StartCoroutine(SpawnTarget());
    }
    //  Handles the clean exit from app.
    public void StopGame()
    {
        SaveGame();

#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    public void ShowTime()
    {
        if (myTime.isActiveAndEnabled)
        {
            myTime.text = System.DateTime.Now.ToString("HH:mm  dd MMMM, yyyy");
        }
    }
    public void ContinueGame()
    {
        Debug.Log("GAME MANAGER: game running - player continue!");
        InGameMenu.SetActive(false);
        isGameActive = true;
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void PauseGame()
    {
        Debug.Log("GAME MANAGER: game running - player pressed pause!");
        isGameActive = false;
        InGameMenu.SetActive(true);
    }
    public void UpdateScore(int scoreToAdd)
    {
        Debug.Log("GAME MANAGER: game running - score updated!");
        _scoreValue += scoreToAdd;
        scoreText.text = "Score: " + _scoreValue;
    }
    void SaveGame()
    {
        PlayerPrefs.SetInt("HighScore", _scoreValue);
        PlayerPrefs.SetString("PlayerName", stringToSave);
        PlayerPrefs.Save();
        Debug.Log("Game data saved!");
    }
    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
    }

    /*****************************************************************
     * 
     * This will spawn the cows in progressive waves.
     *****************************************************************/
    IEnumerator SpawnTarget()
    {
        while (isGameActive && isPlayerAlive)
        {
            yield return new WaitForSeconds(spawnRate);
            Vector3 randomPosition = new(RandomPosition().x, 0, RandomPosition().z);
            //set gender - random.
            int sex = Random.Range(0, targets.Count);
            Instantiate(targets[sex], randomPosition, Quaternion.Euler(0, Random.Range(0f, 360f), 0f));
            //call up the Sound Manager to play the sound
            soundManager.PlaySpawnSound();
            
            if (sex == 0)
            {
                cowsNumber++;
                //cowsText.text = "Cows : " + cowsNumber;
            }
            else
            {
                bullsNumber++;
                //bullsText.text = "Bulls : " + bullsNumber;
            }
        }
    }

    public void GameOver()
    {
        isGameActive = false;
        isPlayerAlive = false;
        gameOverImage.gameObject.SetActive(true);
        InGameMenu.SetActive(true);
    }

#endregion ********************************************
}
