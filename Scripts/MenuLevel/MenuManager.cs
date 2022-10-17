using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.AdaptivePerformance;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startButton, settingsButton, exitButton;

    void Start()
    {
        FadeOut(); 
    }

    void FadeOut()
    {
        startButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        settingsButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);
        exitButton.GetComponent<CanvasGroup>().DOFade(1, 0.8f);

    }
   public void  ExitGame()
    {
        Application.Quit();
    }

    public void StartGameLevel()
    {
        SceneManager.LoadScene("Oyunici");
    }


}
