using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainManager : MonoBehaviour
{
    public Brick BrickPrefab;
    public int LineCount = 6;
    public Rigidbody Ball;

    public Text ScoreText;
    [SerializeField] private GameObject gameOverAlert;
    [SerializeField] private GameObject alertMessage;
    [SerializeField] private GameObject alertScore;

    private bool m_Started = false;
    public int m_Points;
    
    public bool m_GameOver = false;

    
    // Start is called before the first frame update
    void Start()
    {
        const float step = 0.6f;
        int perLine = Mathf.FloorToInt(4.0f / step);
        
        int[] pointCountArray = new [] {1,1,2,2,5,5};
        for (int i = 0; i < LineCount; ++i)
        {
            for (int x = 0; x < perLine; ++x)
            {
                Vector3 position = new Vector3(-1.5f + step * x, 2.5f + i * 0.3f, 0);
                var brick = Instantiate(BrickPrefab, position, Quaternion.identity);
                brick.PointValue = pointCountArray[i];
                brick.onDestroyed.AddListener(AddPoint);
            }
        }
    }

    private void Update()
    {
        if (!m_Started)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                m_Started = true;
                float randomDirection = Random.Range(-1.0f, 1.0f);
                Vector3 forceDir = new Vector3(randomDirection, 1, 0);
                forceDir.Normalize();

                Ball.transform.SetParent(null);
                Ball.AddForce(forceDir * 2.0f, ForceMode.VelocityChange);
            }
        }
        else if (m_GameOver)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    void AddPoint(int point)
    {
        m_Points += point;
        ScoreText.text = $"Score : {m_Points}";
    }

    public void GameOver()
    {
        bool newRecord = false;
        m_GameOver = true;

        if (m_Points > SaveManager.Instance.bestScorePoints)
        {
            newRecord = true;
            SaveManager.Instance.bestScorePoints = m_Points;
            SaveManager.Instance.bestPlayer = SaveManager.Instance.activePlayer;
        }
        ShowGameOverAlert(newRecord);
    }


    // Game Over alert window build
    public void ShowGameOverAlert(bool isNewRecord)
    {
        if (isNewRecord)
        {
            alertMessage.GetComponent<Text>().text = SaveManager.Instance.bestPlayer + " now CHAMPION!";
            alertScore.GetComponent<Text>().text = SaveManager.Instance.bestScorePoints.ToString();
        }
        else if (!isNewRecord)
        {
            alertMessage.GetComponent<Text>().text = SaveManager.Instance.activePlayer + ", your score:";
        }
        alertScore.GetComponent<Text>().text = m_Points.ToString();

        gameOverAlert.SetActive(true);

        // Save Player's data
        SaveManager.Instance.SavePlayerData();
    }
}
