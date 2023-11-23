using System;
using Source.Scripts.Signals;
using UnityEngine;

namespace Source.Scripts.Components
{
    public class ColumnTrigger : MonoBehaviour
    {
        [SerializeField] private int columnIndex;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            var ball = other.GetComponent<Ball>();
            if (ball) Supyrb.Signals.Get<ColumnTriggerSignal>().Dispatch(ball, columnIndex);
        }
    }
}