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
        [SerializeField] private Button _buttonHome;

        private void Awake()
        {
            _buttonRestart.onClick.AddListener(() => _gameManager.ResetLevel());
            _buttonHome.onClick.AddListener(() => _gameManager.BackToMainMenu());

            _gameManager.OnChangedLevel += UpdateTextLeve;
        }

        private void UpdateTextLeve(int index)
        {
            _textLevel.text = $"Level {index + 1}";
        }
    }
}


