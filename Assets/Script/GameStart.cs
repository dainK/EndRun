using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndRun
{
    public class GameStart : MonoBehaviour
    {
        [SerializeField] private Button btnStart;

        private void Awake()
        {
            AudioManager.Instance.PlayMusic();
            btnStart.onClick.AddListener(() =>
            {
                AudioManager.Instance.PlayTouch();
                SceneManager.LoadScene("Game");
            });
        }
    }
}
