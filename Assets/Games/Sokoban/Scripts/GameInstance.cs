using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Sokoban
{
    public class GameInstance : MonoBehaviour
    {
        private static GameInstance _instance;
        public static GameInstance Instance 
        { 
            get 
            {
                if (!_instance)
                {
                    _instance = FindObjectOfType<GameInstance>();

                    if (!_instance)
                    {
                        GameObject newInstance = new GameObject("Game Instance");
                        _instance = newInstance.AddComponent<GameInstance>();
                    }
                }
                return _instance; 
            } 
        }

        private void Awake()
        {
            if (_instance != null && _instance != this || FindObjectsOfType<GameInstance>().Length > 1)
            {
                Debug.LogAssertionFormat("Please make sure there is only one{0} in the scene", typeof(GameInstance).Name);
                Destroy(this);
                return;
            }
            else
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }


        [Header("Data")]
        public int selectedLevel;

    }
}

