using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Sokoban
{
    public class UIManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameManager _gameManager;

        [Header("References")]
        [SerializeField] private TextMeshProUGUI _textLevel;
        [SerializeField] private Button _buttonRestart;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(() => _gameManager.ResetLevel());
            _gameManager.OnChangedLevel += UpdateTextLeve;
        }

        private void UpdateTextLeve(int index)
        {
            _textLevel.text = $"Level {index + 1}";
        }
    }
}


