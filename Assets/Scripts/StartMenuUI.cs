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

public class StartMenuUI : MonoBehaviour
{
    // Player's Name and Champion's Score
    public string playerName;
    [SerializeField] private TMP_InputField menuInputField;
    [SerializeField] private GameObject championTitle;



    // Start is called before the first frame update
    void Start()
    {
        // Add listener for Input Field
        menuInputField.onEndEdit.AddListener(delegate { StorePlayerName(menuInputField); });
    }


    public void StorePlayerName(TMP_InputField input)
    {
        if (input.text.Length > 0)
        {
            playerName = input.text;
        }
        else if (input.text.Length == 0)
        {
            playerName = "Unknown";
        }
        championTitle.GetComponent<TMP_Text>().text = playerName + ": 0000";
    }


    // Start new game ("Play" button)
    public void StartNew()
    {
        SceneManager.LoadScene(1);
    }


    // Exit Game
    public void Exit()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
}
