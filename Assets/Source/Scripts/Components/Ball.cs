using System;
using System.Collections.Generic;
using Source.Scripts.Enums;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Rigidbody2D rb;
        [SerializeField] private HingeJoint2D joint;
        [SerializeField] private Collider2D coll;
        [SerializeField] private ParticleSystem effect;
        
        public static List<Ball> ActiveBalls = new List<Ball>();
        public Rigidbody2D Rb => rb;
        public HingeJoint2D Joint => joint;
        public Collider2D Coll => coll;
        public ParticleSystem Effect => effect;
        
        public BallType Type { get; private set; }

        private void OnEnable() => ActiveBalls.Add(this);
        private void OnDisable() => ActiveBalls.Remove(this);
        
        public void SetColor(Color color)
        {
            spriteRenderer.color = color;
        }

        public void SetSize(float value)
        {
            transform.localScale = new Vector3(value,value,value);
        }

        public void SetType(BallType type)
        {
            Type = type;
        }
    }
}
