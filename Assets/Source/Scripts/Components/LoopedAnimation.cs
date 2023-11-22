using System.Collections;
using System.Linq;
using Lean.Pool;
using Source.Scripts.Components;
using Source.Scripts.Extensions;
using UnityEngine;

namespace Source.Scripts.Systems.Menu
{
    public class LoopedAnimation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] private Ball ball;
        [SerializeField] private float spawnY = 10f;
        [SerializeField] private Vector2 minMaxX;
        [SerializeField] private Vector2 minMaxSize;
        [SerializeField] private float spawnDelay = 0.05f;
        [SerializeField] private float deactivateDelay = 5f;
        [SerializeField] private int maxBalls;
        
        [Header("Colors")]
        [SerializeField] private Color[] colors;
        
        void Start()
        {
            StartCoroutine(SpawnBallRoutine());
        }

        private IEnumerator SpawnBallRoutine()
        {
            
            while (true)
            {
                yield return new WaitUntil(() => Ball.ActiveBalls.Count < maxBalls);
                
                var spawnPos = new Vector2(Random.Range(minMaxX.x, minMaxX.y), spawnY);
                var spawnedBall = LeanPool.Spawn(ball, spawnPos, Quaternion.identity, transform);
                var color = colors[Random.Range(0, colors.Length)];
                
                spawnedBall.SetColor(color);
                spawnedBall.SetSize(Random.Range(minMaxSize.x, minMaxSize.y));
                
                LeanPool.Despawn(spawnedBall, deactivateDelay);
                
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
