using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortCut : MonoBehaviour
{
    [SerializeField] private Button button;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            button.Quest();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            button.Pause();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            button.HTP();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            button.ReLoad();
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            button.Exit();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            button.ChangeSound();
        }
    }
}
