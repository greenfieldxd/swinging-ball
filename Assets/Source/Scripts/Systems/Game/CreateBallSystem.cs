using System;
using Lean.Pool;
using Source.Scripts.Components;
using Source.Scripts.Data;
using Source.Scripts.Main;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class CreateBallSystem : GameSystem
    {
        [SerializeField] private Ball ball;
        
        public override void OnInit()
        {
            SpawnBall();
        }
        
        private void SpawnBall()
        {
            Data.currentBall = LeanPool.Spawn(ball, Data.pendulum.SpawnBallPos.position, Quaternion.identity, Data.pendulum.transform);
            Data.currentBall.GetComponent<HingeJoint2D>().connectedBody = Data.pendulum.Rb;
        }
    }
}