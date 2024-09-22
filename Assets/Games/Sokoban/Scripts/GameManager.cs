using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

            //para configurar la referencia del player para ubicarlo en cada nivel
            GameObject playerStart = GameObject.FindGameObjectWithTag("Respawn");
            _playerController.transform.position = playerStart.transform.position;
            Destroy(playerStart);

            _playerController.enabled = true;
        }
    }
}


