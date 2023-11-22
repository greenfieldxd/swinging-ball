using UnityEngine;

namespace Source.Scripts.Components
{
    public class Pendulum : MonoBehaviour
    {
        [field:SerializeField] public Transform SpawnBallPos { get; private set; }
        [field:SerializeField] public Rigidbody2D Rb { get; private set; }
    }
}
