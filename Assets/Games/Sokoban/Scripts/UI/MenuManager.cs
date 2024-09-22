using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sokoban
{
    public class MenuManager : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] private MenuSlot[] _menuSlots;

        private int _currentIndexMenu;
        private int _lastIndexMenu;

        private void Start()
        {
            GoToMainMenu();
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
