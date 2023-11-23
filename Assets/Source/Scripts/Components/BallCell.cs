using System;
using Source.Scripts.Enums;

namespace Source.Scripts.Components
{
    [Serializable]
    public class BallCell
    {
        public BallType ballType;
        public Ball ball;

        public BallCell(BallType type)
        {
            ballType = type;
            ball = null;
        }
    }
}