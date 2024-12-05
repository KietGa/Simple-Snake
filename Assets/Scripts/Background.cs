using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private GameObject[] bgList;
    private void Awake()
    {
        bgList[0].SetActive(true);
        for (int i = 1; i < bgList.Length; i++)
        {
            bgList[i].SetActive(false);
        }

    }
    void Update()
    {
        int score = Score.GetScore();

        if (score >= 17500)
        {
            bgList[4].SetActive(true);
            bgList[3].SetActive(false);
        }
        if (score >= 8000)
        {
            bgList[3].SetActive(true);
            bgList[2].SetActive(false);
        }
        if (score >= 3500)
        {
            bgList[2].SetActive(true);
            bgList[1].SetActive(false);
        }
        if (score >= 1000)
        {
            bgList[1].SetActive(true);
            bgList[0].SetActive(false);
        }
    }
}
