using System;
using UnityEngine;
using NaughtyAttributes;

namespace UI
{
    public class UIDirector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _mapButton;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _shopScreen;

        [SerializeField] private GameObject _map;

        private static GameObject pauseButton;
        private static GameObject mapButton;
        private static GameObject pauseScreen;
        private static GameObject gameOverScreen;
        private static GameObject winScreen;
        private static GameObject shopScreen;

        private static GameObject map;

        //public static event Action OnGamePaused;
        //public static event Action OnGameUnpaused;
        #endregion

        #region MONOBEHS
        private void Awake()
        {
            pauseButton = _pauseButton;
            mapButton = _mapButton;
            pauseScreen = _pauseScreen;
            gameOverScreen = _gameOverScreen;
            winScreen = _winScreen;
            shopScreen = _shopScreen;
            map = _map;
        }
        private void OnEnable()
        {
            //TestPlayer.OnPlayerDeath += OnPlayerDeath;
            LevelDirector.OnGameFinishedEvent += OnGameFinished;
        }
        private void OnDisable()
        {
            //TestPlayer.OnPlayerDeath -= OnPlayerDeath;
            LevelDirector.OnGameFinishedEvent -= OnGameFinished;
        }
        #endregion

        public static void PauseGame()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            pauseScreen.SetActive(true);
        }
        public static void UnpauseGame()
        {
            TurnOffAll();
            pauseButton.SetActive(true);
            mapButton.SetActive(true);
            Time.timeScale = 1f;
        }
        [Button]
        private void OnPlayerDeath()
        {
            TurnOffAll();
            gameOverScreen.SetActive(true);
        }
        [Button]
        private void OnGameFinished()
        {
            TurnOffAll();
            winScreen.SetActive(true);
        }
        [Button]
        public static void OpenShop()
        {
            TurnOffAll();
            shopScreen.SetActive(true);
        }
        [Button]
        public static void OpenMap()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            map.SetActive(true);
        }

        private static void TurnOffAll()
        {
            pauseButton.SetActive(false);
            mapButton.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            winScreen.SetActive(false);
            shopScreen.SetActive(false);
            map.SetActive(false);
        }
    }
}
