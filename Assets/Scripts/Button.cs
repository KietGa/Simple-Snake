using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject pause;
    [SerializeField] private GameObject htp;
    [SerializeField] private GameObject end;
    [SerializeField] private GameObject quest;
    [SerializeField] private Snake snake;
    public void ReLoad()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    public void ChangeSound()
    {
        snake.ChangeMusic();
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void Pause()
    {
        Time.timeScale = 0f;
        pause.SetActive(true);
        htp.SetActive(false);
        end.SetActive(false);
        quest.SetActive(false);
    }

    public void Quest()
    {
        Time.timeScale = 0f;
        quest.SetActive(true);
        htp.SetActive(false);
        end.SetActive(false);
        pause.SetActive(false);
    }

    public void HTP()
    {
        Time.timeScale = 0f;
        htp.SetActive(true);
        pause.SetActive(false);
        end.SetActive(false);
        quest.SetActive(false);
    }

    public void ResumePause()
    {
        Time.timeScale = 1f;
        pause.SetActive(false);
    }

    public void ResumeTP()
    {
        Time.timeScale = 1f;
        htp.SetActive(false);
    }

    public void ResumeQuest()
    {
        Time.timeScale = 1f;
        quest.SetActive(false);
    }
}
