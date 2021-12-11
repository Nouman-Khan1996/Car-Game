﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Parking : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Time.timeScale = 0;
            if(PlayerPrefs.GetInt("levelsCompleted")< MainMenu.levelNum)
                PlayerPrefs.SetInt("levelsCompleted", MainMenu.levelNum);
            UIManager.instance.completePanel.SetActive(true);
        }
    }
}