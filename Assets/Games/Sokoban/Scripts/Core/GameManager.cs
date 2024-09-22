using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Game.Sokoban
{
    public class GameManager : MonoBehaviour
    {
        [Header("General")]
        //Para referenciar el jugador y evitar que se mueva mientras se crea el nivel
        [SerializeField] private PlayerController _playerController;

        [Header("Levels")]
        [SerializeField] private GameObject _levelParent;
        [SerializeField] private GameObject[] _levels;
        //Levels para ir de nivel en nivel por indice, sea nivel 0, nivel 1 etc.
        [SerializeField] private int _currentLevelIndex;

        //Para guardar referencia actual del objeto instanciado
        private GameObject _currentLevel;

        //Para pasar el numero que tiene el nivel para utilizarlo en UI
        public UnityAction<int> OnChangedLevel;

        //Boxes
        [SerializeField]
        private int _boxTota;

        [SerializeField]
        private int _boxCurrent;

        private void Start()
        {
            OnChangedLevel.Invoke(_currentLevelIndex);
            StartCoroutine(CreateLevel());
        }
        public void ResetLevel()
        {
            _playerController.Restart();
            StartCoroutine(CreateLevel());

        }

        private IEnumerator CreateLevel()
        {
            _playerController.enabled = false;
            if(_currentLevel != null)
                Destroy(_currentLevel);

            yield return new WaitUntil(() => _currentLevel == null);
            _currentLevel = Instantiate(_levels[_currentLevelIndex], _levelParent.transform);

            FindBoxes();

            //para configurar la referencia del player para ubicarlo en cada nivel
            GameObject playerStart = GameObject.FindGameObjectWithTag("Respawn");
            _playerController.transform.position = playerStart.transform.position;
            Destroy(playerStart);

            _playerController.enabled = true;
        }

        private void FindBoxes()
        {
            _boxCurrent = 0;
            _boxTota = 0;

            //Para que busque todas las cajas de la escena y se guarden en tempBoxes
            Box[] tempBoxes = FindObjectsOfType<Box>();

            for (int i = 0; i < tempBoxes.Length; i++)
            {
                tempBoxes[i].onTrigger += OnTriggerBox;
                _boxTota++;
            }

            //Para checkear si alguna caja ya esta encima de algun punto al arrancar el nivel
            for (int i = 0; i < tempBoxes.Length; i++)
            {
                tempBoxes[i].Trigger();
            }
        }

        //para validar si completamos todas las cajas y pasar de nivel etc.
        private void OnTriggerBox(bool isComplete)
        {
            if (isComplete)
            {
                _boxCurrent++;
                if (_boxCurrent == _boxTota)
                {
                    //Al completar las cajas del nivel, incremente en 1 y si hay niveles pasa, si no Win Gana
                    _currentLevelIndex++;
                    OnChangedLevel.Invoke(_currentLevelIndex);
                    if (_currentLevelIndex < _levels.Length)
                    {
                        StartCoroutine(CreateLevel());
                    }
                    else
                    {
                        //Win
                        Debug.Log("WIN");
                    }
                }
            }
            else
            {
                _boxCurrent--;
            }
        }
    }
}


