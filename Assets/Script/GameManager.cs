using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace EndRun
{
    public class GameManager : MonoBehaviour//: Singleton<GameManager>
    {
        [Header("Data")]
        [SerializeField] private PlayerData playerData;
        [SerializeField] private GameData gameData;

        [Header("UI")]
        [SerializeField] private GameObject[] life = new GameObject[3];
        [SerializeField] private Text distance;
        [SerializeField] private GameObject end;
        [SerializeField] private Text score;
        [SerializeField] private Button btnEnd;

        [Header("GameObject")]
        [SerializeField] private Player player;
        [SerializeField] private GroundScroller ground;
        [SerializeField] private ObstactionController obstaction;

        private void Awake()
        {
            GameSettings.playerData = playerData;
            gameData.isEnd = false;
            GameSettings.gameData = gameData;

            GameSettings.scoreData.distance = 0;
            GameSettings.playerData.life = 3;
            
            btnEnd.onClick.AddListener(()=>
            {
                SceneManager.LoadScene("Start");
            });
        }

        private void Start()
        {
            end.SetActive(false);
            
            player.gameObject.SetActive(true);
            ground.gameObject.SetActive(true);
            obstaction.gameObject.SetActive(true);
            
            foreach (var obj in life)
            {
                obj.SetActive(true);
            }
            
            distance.text = GameSettings.scoreData.distance.ToString();
        }

        private void OnEnable()
        {
            player.onHurt += OnHurt;
            ground.onAddScore += OnAddScore;
        }
        private void OnDisable()
        {
            player.onHurt -= OnHurt;
            ground.onAddScore -= OnAddScore;
        }

        private void OnHurt()
        {
            if( GameSettings.gameData.isEnd )
                return;
            
            AudioManager.Instance.PlayHurt();
            GameSettings.playerData.life--;
            life[GameSettings.playerData.life].SetActive(false);

            if (GameSettings.playerData.life == 0)
            {
                EndGame();
            }
        }
        
        private void OnAddScore()
        {
            if( GameSettings.gameData.isEnd )
                return;
            
            GameSettings.scoreData.distance++;
            distance.text = GameSettings.scoreData.distance.ToString();
        }

        private void EndGame()
        {
            GameSettings.gameData.isEnd = true;
            AudioManager.Instance.StopMusic();

            end.SetActive(true);
            
            player.gameObject.SetActive(false);
            ground.gameObject.SetActive(false);
            obstaction.gameObject.SetActive(false);
            
            score.text = "Score : " + GameSettings.scoreData.distance.ToString();
        }
    }

}