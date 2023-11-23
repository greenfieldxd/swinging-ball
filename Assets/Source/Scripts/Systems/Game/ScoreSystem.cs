using Source.Scripts.Configs;
using Source.Scripts.Enums;
using Source.Scripts.Extensions;
using Source.Scripts.Main;
using Source.Scripts.Signals;
using Source.Scripts.UI;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class ScoreSystem : GameSystem
    {
        [SerializeField] private BallConfig ballConfig;
        
        private GameCanvas gameCanvas;
        
        public override void OnInit()
        {
            gameCanvas = FindObjectOfType<GameCanvas>();
            gameCanvas.ScoreText.text = GameManager.Instance.Score.ToString();
            Supyrb.Signals.Get<UpdateScoreSignal>().AddListener(UpdateScore);
        }

        private void UpdateScore(BallType type)
        {
            var toAdd = ballConfig.GetVariant(type).Score;
            GameManager.Instance.AddScore(toAdd);
            
            gameCanvas.ScoreText.text = GameManager.Instance.Score.ToString();
            OtherExtensions.TransformPunchScale(gameCanvas.ScoreText.transform, 0.1f, 0.2f, 2);
        }
    }
}