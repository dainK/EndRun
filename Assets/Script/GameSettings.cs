using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EndRun
{
    [Serializable]
    public class GameSettings 
    {
        static public PlayerData playerData;
        static public GameData gameData;
        static public ScoreData scoreData;
    }

    [Serializable]
    public struct PlayerData
    {
        public int life;
        [SerializeField] public float jumpHight;
        [SerializeField] public float jumpSpeed;
        
    }

    [Serializable]
    public struct GameData
    {
        public bool isEnd;
        [SerializeField]
        public float moveSpeed;
    }

    [Serializable]
    public struct ScoreData
    {
        [SerializeField]
        public int distance;
    }

}