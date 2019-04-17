using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CsUIControll : MonoBehaviour
{
    public static CsUIControll instance;

    // Start is called before the first frame update
    public int score = 0;

    int maxScore = 0;


    public Text scoreText;

    public GameObject Menu;

    public GameObject Fail;

    public Text bestScore;

    public Text currentScore;
    bool isMenuOpen;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
         instance = this;
    }
    void Start()
    {
        isMenuOpen = false;
        score = 0;
        scoreText.text = score.ToString();

        maxScore = PlayerPrefs.GetInt("maxScore");
        
    }

    void Update()
    {
        
    }

    // Update is called once per frame
    public void GetScore()
    {
        score += 1;

        scoreText.text = score.ToString();
    }
    public void PressStopButton()
    {
        isMenuOpen = !isMenuOpen;
        Menu.SetActive(isMenuOpen);
        if (isMenuOpen)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;


    }

    
    public void Die()
    {
       
        Fail.SetActive(true);
        Time.timeScale = 0;

        if(maxScore < score)
        {
            maxScore = score;
            PlayerPrefs.SetInt("maxScore",score);
        }

        bestScore.text = maxScore.ToString();
        currentScore.text = score.ToString();

        
    }
    public void PressResumeButton()
    {
        Menu.SetActive(false);
        Time.timeScale = 1;
    }
    public void PressResetButton()
    {
        SceneManager.LoadScene("InGameScene");
        Time.timeScale = 1f;
    }
    public void PressTitleButton()
    {
        SceneManager.LoadScene("TitleScene");
        Time.timeScale = 1f;
    }
}
