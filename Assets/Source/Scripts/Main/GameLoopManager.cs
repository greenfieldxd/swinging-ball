using System;
using System.Linq;
using Source.Scripts.Data;
using UnityEngine;

namespace Source.Scripts.Main
{
    public class GameLoopManager : MonoBehaviour
    {
        private GameSystem[] systems;
        
        private void Start()
        {
            var data = new GameData();
            systems = GetComponentsInChildren<GameSystem>().OrderBy(x => x.transform.GetSiblingIndex()).ToArray();

            foreach (var system in systems)
            {
                system.SetData(data);
                system.OnInit();
            }
        }

        private void Update()
        {
            foreach (var system in systems)
            {
                system.OnUpdate();
            }
        }
        
        private void FixedUpdate()
        {
            foreach (var system in systems)
            {
                system.OnFixedUpdate();
            }
        }
    }
}