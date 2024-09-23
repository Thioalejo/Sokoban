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

        //para guardado local
        #region PlayerPrefs

        public void SaveString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
            PlayerPrefs.Save();
        }

        public string LoadString(string key, string defaultValue)
        {
            return PlayerPrefs.GetString(key, defaultValue);

        }

        public void SaveInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
            PlayerPrefs.Save();
        }

        public int LoadInt(string key, int defaultValue)
        {
            return PlayerPrefs.GetInt(key, defaultValue);

        }

        [ContextMenu("Delete All Saves")]
        public void DeleteAll()
        {
            PlayerPrefs.DeleteAll();
        }
        #endregion


        [Header("Data")]
        public int selectedLevel;
        public int maxLevelWinneed;
        public bool gameFinished;
        public bool volumMusicActive = true;
        public bool volumSFXActive = true;


        public void SaveMaxLevelWinned(int value)
        {
            if (value <= maxLevelWinneed) return;
            SaveInt("MaxLevelWinned", value);
        }
    }
}

