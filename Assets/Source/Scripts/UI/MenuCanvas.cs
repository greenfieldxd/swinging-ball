using Source.Scripts.Main;
using UnityEngine;
using UnityEngine.UI;

namespace Source.Scripts.UI
{
    public class MenuCanvas : MonoBehaviour
    {
        [SerializeField] private Button startButton;

        void Start()
        {
            startButton.onClick.AddListener(() => GameManager.Instance.LoadScene(1));
        }
    }
}