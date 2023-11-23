using System;
using Lean.Pool;
using Source.Scripts.Components;
using Source.Scripts.Configs;
using Source.Scripts.Main;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CreateBallSystem : GameSystem
    {
        [SerializeField] private Ball ball;
        [SerializeField] private BallConfig ballConfig;
        
        public override void OnInit()
        {
            Supyrb.Signals.Get<DropBallSignal>().AddListener(SpawnBall);
            SpawnBall();
        }
        
        private void SpawnBall()
        {
            var randVariant = ballConfig.GetRandom();
            
            Data.currentBall = Instantiate(ball, Data.pendulum.SpawnBallPos.position, Quaternion.identity, Data.pendulum.transform);
            Data.currentBall.GetComponent<HingeJoint2D>().connectedBody = Data.pendulum.Rb;
            Data.currentBall.SetType(randVariant.Type);
            Data.currentBall.SetColor(randVariant.Color);
        }
    }
}