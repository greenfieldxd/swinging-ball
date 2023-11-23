using System;
using Source.Scripts.Signals;
using Source.Scripts.Singleton;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Main
{
    public class GameManager : DontDestroySingleton<GameManager>
    {
        public int Score { get; private set; }
        
        public void LoadScene(int index, bool resetScore = true)
        {
            if (resetScore) Score = 0;
            Supyrb.Signals.Clear();
            SceneManager.LoadScene(index);
        }

        public void AddScore(int value)
        {
            Score += value;
        }
    }
}