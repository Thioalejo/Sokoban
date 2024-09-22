using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Game.Sokoban
{
    public class LevelSlot : MonoBehaviour
    {
        [Header("References")]
        //Referencia al texto que muestra el nivel
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;
        [Space]
        //Para validar si se muestra icono de candado o esta habilitado para juagar
        [SerializeField] private Sprite _spriteLock;


        public Action<int> onSelect;

        private int _index;
        //Estado actual del nivel
        private bool _isWinned;

        private void Awake()
        {
            _button.onClick.AddListener(()=>OnSelect());
        }

        public void Initialize(int index, bool state)
        {
            _index = index;
            _isWinned = state;

            _text.text = $"Level {index + 1}";

            if (!_isWinned)
            {
                _image.sprite = _spriteLock;
                _button.interactable = false;
            }
        }

        private void OnSelect()
        {
            onSelect.Invoke(_index);
        }
    }
}
