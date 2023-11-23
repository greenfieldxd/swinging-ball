using UnityEngine;

namespace Source.Scripts.Main
{
    public abstract class GameSystem : MonoBehaviour
    {
        public GameData Data { get; private set; }

        public void SetData(GameData data)
        {
            Data = data;
        }

        public virtual void OnInit()
        {
        }

        public virtual void OnUpdate()
        {
        }

        public virtual void OnFixedUpdate()
        {
        }
    }
}