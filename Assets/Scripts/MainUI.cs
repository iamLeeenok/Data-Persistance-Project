using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;


public class MainUI : MonoBehaviour
{
    [SerializeField] private GameObject bestScoreTitle;
    // Start is called before the first frame update
    void Start()
    {
        if (SaveManager.Instance != null)
        {
            ChangeBestScoreTitle(SaveManager.Instance.activePlayer);
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void ChangeBestScoreTitle(string name)
    {
        Text titleText = bestScoreTitle.GetComponent<Text>();
        titleText.text = "Best Score: " + SaveManager.Instance.activePlayer + ": 00000";
    }


    // Start new game ("Play" button)
    public void GoToStartMenu()
    {
        SceneManager.LoadScene(0);
    }

}
