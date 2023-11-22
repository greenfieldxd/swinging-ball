using Source.Scripts.Components;
using Source.Scripts.Main;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class PendulumSystem : GameSystem
    {
        [Header("Settings")]
        [SerializeField] private float moveSpeed;
        [SerializeField] private float leftAngle;
        [SerializeField] private float rightAngle;

        private bool moveRight;

        public override void OnInit()
        {
            Data.pendulum = FindObjectOfType<Pendulum>();
            moveRight = true;
        }

        public override void OnUpdate()
        {
            Move();
        }

        private void ChangeMoveDir()
        {
            var tr = Data.pendulum.Rb.transform;
            
            if (tr.rotation.z > rightAngle / 100) moveRight = false;
            if (tr.rotation.z < leftAngle / 100) moveRight = true;
        }

        private void Move()
        {
            ChangeMoveDir();
            Data.pendulum.Rb.angularVelocity = moveRight ?  moveSpeed : - moveSpeed;
        }
    }
}
