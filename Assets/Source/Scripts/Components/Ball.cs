using System;
using System.Collections.Generic;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class Ball : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        
        public static List<Ball> ActiveBalls = new List<Ball>();

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
    }
}
