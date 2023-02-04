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


[DefaultExecutionOrder(1000)]
public class StartMenuUI : MonoBehaviour
{
    // Player's Name and Champion's Score
    private string defaultPlayerName = "Unknown";
    private int defaultPlayerScore = 0;
    public string playerName;
    public int playerScore;
    [SerializeField] private TMP_InputField menuInputField;
    [SerializeField] private GameObject championTitle;
    [SerializeField] private GameObject championLine;
    //private string bestName = SaveManager.Instance.bestPlayer;
    //private int bestScore = SaveManager.Instance.bestScorePoints;



    // Start is called before the first frame update
    void Start()
    {
        // Passes Player's Name back from Main scene to the Menu scene
        if (SaveManager.Instance.bestPlayer.Length > 0)
        {
            // Pass name
            ChangeChampionText(SaveManager.Instance.bestPlayer, SaveManager.Instance.bestScorePoints);
        }
        else if (SaveManager.Instance.bestPlayer.Length == 0)
        {
            SaveManager.Instance.activePlayer = defaultPlayerName;
            SaveManager.Instance.bestPlayer = defaultPlayerName;
            SaveManager.Instance.bestScorePoints = defaultPlayerScore;
            championLine.SetActive(false);
            championTitle.GetComponent<TMP_Text>().text = "No Champion Yet";
        }

        if (SaveManager.Instance.activePlayer.Length == 0)
        {
            SaveManager.Instance.activePlayer = defaultPlayerName;
        }


        // Add listener for Input Field
        menuInputField.onEndEdit.AddListener(delegate { StorePlayerName(menuInputField); });
    }


    public void StorePlayerName(TMP_InputField input)
    {
        if (input.text.Length > 0)
        {
            SaveManager.Instance.activePlayer = input.text;
        } else if (input.text.Length == 0)
        {
            SaveManager.Instance.activePlayer = defaultPlayerName;
        }

        if (SaveManager.Instance.bestPlayer.Length == 0)
        {
            SaveManager.Instance.bestPlayer = input.text;
        } 

        championLine.GetComponent<TMP_Text>().text = SaveManager.Instance.bestPlayer + ": " + SaveManager.Instance.bestScorePoints + " points";
        championLine.SetActive(true);

        // Save Player's name in the SaveManager instance
        //SaveManager.Instance.activePlayer = playerName;
        //SaveManager.Instance.bestScorePoints = playerScore;
    }


    // Fill Champion Text with Player's Name
    private void ChangeChampionText(string champion, int score)
    {
        championLine.GetComponent<TMP_Text>().text = champion + ": " + score + " points";
        championLine.SetActive(true);
    }


    // Start new game ("Play" button)
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }


    // Exit Game
    public void Exit()
    {
        SaveManager.Instance.SavePlayerData();

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
