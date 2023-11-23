using TMPro;
using UnityEngine;

namespace Source.Scripts.UI
{
    public class GameCanvas : MonoBehaviour
    {
        [field:SerializeField] public TextMeshProUGUI ScoreText { get; private set; }
    }
}
