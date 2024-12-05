using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverWindow : MonoBehaviour
{
    private static GameOverWindow instance;
    private void Awake()
    {
        instance = this;
        Hide();
    }
    public void Show(bool isNewHighScore)
    {
        gameObject.SetActive(true);

        transform.Find("NewHighScore").gameObject.SetActive(isNewHighScore);
        transform.Find("GameOver").gameObject.SetActive(true);
    }

    public static void StaticShow(bool isNewHighScore)
    {
        instance.Show(isNewHighScore);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
