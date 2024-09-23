using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace Game.Sokoban
{
    public class MenuManager : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private int _levesAvailables;

        [Header("Audio")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private ButtonToggle _toggleMusic;
        [SerializeField] private ButtonToggle _toggleSFX;

        [Header("References")]
        [SerializeField] private MenuSlot[] _menuSlots;
        [SerializeField] private LevelSlot _levelPrefab;
        [SerializeField] private Transform _levelParent;

         private int _maxLevelWinnded;

        private int _currentIndexMenu;
        private int _lastIndexMenu;

        //Audio
        private const string MIXER_MUSIC = "Music volume";
        private const string MIXER_SFX = "SFX Volume";

        private void Start()
        {
            if (GameInstance.Instance.gameFinished)
            {
                GameInstance.Instance.gameFinished = false;
                GoToEndDemo();
            }
            else
            {
                GoToMainMenu();
            }


            _maxLevelWinnded = GameInstance.Instance.LoadInt("MaxLevelWinned",0);

            //Leves for Show
            for (int i = 0; i < _levesAvailables; i++)
            {
                LevelSlot levelSlot = Instantiate(_levelPrefab, _levelParent);
                levelSlot.Initialize(i, i <= _maxLevelWinnded);
                levelSlot.Initialize(i,true);
                levelSlot.onSelect += OnSelecteLevel;
            }

            ToggleMusic(GameInstance.Instance.volumMusicActive);
            ToggleSFX(GameInstance.Instance.volumSFXActive);
        }

        public void ToggleMusic(bool isAtive)
        {
            ChangeVolum(MIXER_MUSIC, isAtive ? 1 : 0.00001f);
            GameInstance.Instance.volumMusicActive = isAtive;
            _toggleMusic.ToggleValue(isAtive);
        }

        public void ToggleSFX(bool isAtive)
        {
            ChangeVolum(MIXER_SFX, isAtive ? 1 : 0.00001f);
            GameInstance.Instance.volumSFXActive = isAtive;
            _toggleSFX.ToggleValue(isAtive);
        }

        public void ChangeVolum(string parameter, float value)
        {
            _mixer.SetFloat(parameter, Mathf.Log10(value) * 20);
        }

        private void OnSelecteLevel(int index)
        {
            //Debug.Log($"Select Level: {index}");
            GameInstance.Instance.selectedLevel = index;
            SceneManager.LoadScene(1);
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
