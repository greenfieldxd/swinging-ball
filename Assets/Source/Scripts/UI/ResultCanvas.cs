using System;
using Source.Scripts.Main;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class ResultCanvas : MonoBehaviour
    {
        [SerializeField] private Button restartButton;
        [SerializeField] private Button menuButton;
        [SerializeField] private TextMeshProUGUI scoreText;

        private void Start()
        {
            restartButton.onClick.AddListener(() => GameManager.Instance.LoadScene(1));
            menuButton.onClick.AddListener(() => GameManager.Instance.LoadScene(0));

            scoreText.text = GameManager.Instance.Score.ToString();
        }
    }
}
