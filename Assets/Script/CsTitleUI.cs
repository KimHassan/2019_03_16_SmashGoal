using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CsTitleUI : MonoBehaviour
{
    public GameObject howToButton;
    public GameObject startButton;

    int num = 2;
    private void Start()
    {
        
        howToButton = GameObject.FindGameObjectWithTag("HowToUI");
        startButton = GameObject.FindGameObjectWithTag("StartUI");
    }
    public void StartButton()
    {
        SceneManager.LoadScene("InGameScene");
    }
    public void HowToButton()
    {
        Debug.Log("눌림");
        howToButton.transform.Find("HowToScene").gameObject.SetActive(true);
        howToButton.transform.Find("HowToScene1").gameObject.SetActive(true);
        howToButton.GetComponent<Button>().interactable = false;
        startButton.GetComponent<Button>().interactable = false;
    }
    public void HowToScene1()
    {
        howToButton.transform.Find("HowToScene1").gameObject.SetActive(false);
        howToButton.transform.Find("HowToScene2").gameObject.SetActive(true);
        GetComponent<Button>().interactable = false;
    }
    public void HowToScene2()
    {
        howToButton.transform.Find("HowToScene").gameObject.SetActive(false);
        howToButton.transform.Find("HowToScene1").gameObject.SetActive(false);
        howToButton.transform.Find("HowToScene2").gameObject.SetActive(false);
        howToButton.GetComponent<Button>().interactable = true;
        startButton.GetComponent<Button>().interactable = true;
    }
    public void HowToExit()
    {
        Debug.Log("누름");
        howToButton.transform.Find("HowToScene").gameObject.SetActive(false);
        howToButton.transform.Find("HowToScene1").gameObject.SetActive(false);
        howToButton.transform.Find("HowToScene2").gameObject.SetActive(false);
        howToButton.GetComponent<Button>().interactable = true;
        startButton.GetComponent<Button>().interactable = true;
    }
}
