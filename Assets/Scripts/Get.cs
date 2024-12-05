using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Get : MonoBehaviour
{
    private int highScore;
    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;
    [SerializeField] private GameObject img3;
    [SerializeField] private GameObject img4;
    [SerializeField] private GameObject img5;
    [SerializeField] private GameObject img6;

    private void Awake()
    {
        highScore = Score.GetHighScore();
    }
    public void Get1()
    {
        if (highScore >= 300)
        {
            PlayerPrefs.SetInt("quest1", 1);
            img1.SetActive(true);
        }
        else
        {
            img1.SetActive(false);
        }
    }

    public void Get2()
    {
        if (highScore >= 900)
        {
            PlayerPrefs.SetInt("quest2", 1);
            img2.SetActive(true);
        }
        else
        {
            img2.SetActive(false);
        }
    }

    public void Get3()
    {
        if (highScore >= 2000)
        {
            PlayerPrefs.SetInt("quest3", 1);
            img3.SetActive(true);
        }
        else
        {
            img3.SetActive(false);
        }
    }

    public void Get4()
    {
        if (highScore >= 4500)
        {
            PlayerPrefs.SetInt("quest4", 1);
            img4.SetActive(true);
        }
        else
        {
            img4.SetActive(false);
        }
    }

    public void Get5()
    {
        if (highScore >= 11000)
        {
            PlayerPrefs.SetInt("quest5", 1);
            img5.SetActive(true);
        }
        else
        {
            img5.SetActive(false);
        }
    }

    public void Get6()
    {
        if (highScore >= 20000)
        {
            PlayerPrefs.SetInt("quest6", 1);
            img6.SetActive(true);
        }
        else
        {
            img6.SetActive(false);
        }
    }
}
