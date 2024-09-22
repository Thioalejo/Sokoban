using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sokoban
{
    public class MenuManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private int _levesAvailables;

        [Header("References")]
        [SerializeField] private MenuSlot[] _menuSlots;
        [SerializeField] private LevelSlot _levelPrefab;
        [SerializeField] private Transform _levelParent;

         private int _maxLevelWinnded;

        private int _currentIndexMenu;
        private int _lastIndexMenu;

        private void Start()
        {
            GoToMainMenu();

            //Leves for Show
            for (int i = 0; i < _levesAvailables; i++)
            {
                LevelSlot levelSlot = Instantiate(_levelPrefab, _levelParent);
                //levelSlot.Initialize(i, i <= _maxLevelWinnded);
                levelSlot.Initialize(i,true);
                levelSlot.onSelect += OnSelecteLevel;
            }
        }

        private void OnSelecteLevel(int index)
        {
            Debug.Log($"Select Level: {index}");
        }

        private void ChangeMenu(int index)
        {
            _currentIndexMenu = index;

            _menuSlots[_lastIndexMenu].Hide();
            _menuSlots[_currentIndexMenu].Show();

            _lastIndexMenu = _currentIndexMenu;
        }
        public void GoToMainMenu()
        {
            ChangeMenu(0);
        }

        public void GoToLevelSelector()
        {
            ChangeMenu(1);
        }

        public void GoToEndDemo()
        {
            ChangeMenu(2);
        }
    }
}
