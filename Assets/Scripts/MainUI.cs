using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif


public class MainUI : MonoBehaviour
{

    [SerializeField] private GameObject bestScoreTitle;
    private string bestName;
    private int bestScore;


    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Instance != null)
        {
            bestName = SaveManager.Instance.bestPlayer;
            bestScore = SaveManager.Instance.bestScorePoints;
            ChangeBestScoreTitle(bestName, bestScore);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    // Passes Player's Name to the Best Score title at Main scene
    private void ChangeBestScoreTitle(string name, int points)
    {
        Text bestScore = bestScoreTitle.GetComponent<Text>();
        bestScore.text = "Best Score: " + name + ": " + points;
    }


    // Start new game ("Play" button)
    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

    // Exit
    public void ExitGame()
    {
        SaveManager.Instance.SavePlayerData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
