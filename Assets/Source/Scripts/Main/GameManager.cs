using System;
using Kuhpik;
using Source.Scripts.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Source.Scripts.Main
{
    public class GameManager : DontDestroySingleton<GameManager>
    {
        public void LoadScene(int index)
        {
            SceneManager.LoadScene(index);
        } 
    }
}