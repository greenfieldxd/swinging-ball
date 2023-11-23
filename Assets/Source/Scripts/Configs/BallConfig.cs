using System;
using System.Linq;
using Source.Scripts.Enums;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Source.Scripts.Configs
{
    [CreateAssetMenu(fileName = "BallConfig", menuName = "Configs/BallConfig")]
    public class BallConfig : ScriptableObject
    {
        [SerializeField] private BallVariant[] variants;

        public BallVariant GetRandom()
        {
            return variants[Random.Range(0, variants.Length)];
        }
        
        public BallVariant GetVariant(BallType type)
        {
            return variants.First(x => x.Type == type);
        }
        
        [Serializable]
        public class BallVariant
        {
            [field: SerializeField] public BallType Type { get; private set; }
            [field: SerializeField] public Color Color { get; private set; }
            [field: SerializeField] public int Score { get; private set; }
        }
    }
}