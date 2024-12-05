using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    [SerializeField] private GameObject img1;
    [SerializeField] private GameObject img2;
    [SerializeField] private GameObject img3;
    [SerializeField] private GameObject img4;
    [SerializeField] private GameObject img5;
    [SerializeField] private GameObject img6;
    private void Awake()
    {
        if (PlayerPrefs.GetInt("quest1") == 1)
        {
            img1.SetActive(true);
        }
        if (PlayerPrefs.GetInt("quest2" , 0) == 1)
        {
            img2.SetActive(true);
        }
        if (PlayerPrefs.GetInt("quest3", 0) == 1)
        {
            img3.SetActive(true);
        }
        if (PlayerPrefs.GetInt("quest4", 0) == 1)
        {
            img4.SetActive(true);
        }
        if (PlayerPrefs.GetInt("quest5", 0) == 1)
        {
            img5.SetActive(true);
        }
        if (PlayerPrefs.GetInt("quest6", 0) == 1)
        {
            img6.SetActive(true);
        }
    }
}
