using Source.Scripts.Main;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Systems.Game
{
    public class DropBallSystem : GameSystem
    {
        [SerializeField] private float ballMass = 1f;
        
        public override void OnUpdate()
        {
            if (Data.currentBall == null) return;

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                DropBall();
            }
        }

        private void DropBall()
        {
            Destroy(Data.currentBall.Joint);
            Data.currentBall.transform.SetParent(null);
            Data.currentBall.Rb.mass = ballMass;
            Data.currentBall.Coll.enabled = true;
            Data.currentBall.Rb.velocity = Vector2.zero;
            Data.currentBall = null;
            
            Supyrb.Signals.Get<DropBallSignal>().Dispatch();
        }
    }
}