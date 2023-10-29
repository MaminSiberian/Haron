using System;
using UnityEngine;
using NaughtyAttributes;
using Haron;
using TMPro;

namespace UI
{
    public class UIDirector : MonoBehaviour
    {
        #region FIELDS
        [SerializeField] private GameObject target;
        [SerializeField] private GameObject _pauseButton;
        [SerializeField] private GameObject _mapButton;
        [SerializeField] private GameObject _pauseScreen;
        [SerializeField] private GameObject _gameOverScreen;
        [SerializeField] private GameObject _winScreen;
        [SerializeField] private GameObject _shopScreen;
        [SerializeField] private GameObject _HTPScreen;

        [SerializeField] private GameObject _map;
        [SerializeField] private Camera _mapCam;
        [SerializeField] private Camera _minimapCam;
        [SerializeField] private MessageBox _messageBox;

        [SerializeField] private TextMeshProUGUI maxHPText;
        [SerializeField] private TextMeshProUGUI damageText;
        [SerializeField] private TextMeshProUGUI cooldawnDashText;

        private static GameObject pauseButton;
        private static GameObject mapButton;
        private static GameObject pauseScreen;
        private static GameObject gameOverScreen;
        private static GameObject winScreen;
        private static GameObject shopScreen;
        private static GameObject HTPScreen;

        private static GameObject map;
        private static MessageBox messageBox;

        private Transform player;
        private HaronController hc;
        private static bool isSetText = false;
        //public static event Action OnGamePaused;
        //public static event Action OnGameUnpaused;
        #endregion

        #region MONOBEHS

        private void Start()
        {
            hc = FindObjectOfType<HaronController>();
        }
        private void Awake()
        {
            target.SetActive(false);

            pauseButton = _pauseButton;
            mapButton = _mapButton;
            pauseScreen = _pauseScreen;
            gameOverScreen = _gameOverScreen;
            winScreen = _winScreen;
            shopScreen = _shopScreen;
            HTPScreen = _HTPScreen;
            map = _map;
            messageBox = _messageBox;

            player = FindAnyObjectByType<Haron.HaronController>().transform;
        }
        private void OnEnable()
        {
            //TestPlayer.OnPlayerDeath += OnPlayerDeath;
            LevelDirector.OnGameFinishedEvent += OnGameFinished;
            LevelDirector.OnQuestTargetChangedEvent += OnQuestTargetChanged;
        }
        private void OnDisable()
        {
            //TestPlayer.OnPlayerDeath -= OnPlayerDeath;
            LevelDirector.OnGameFinishedEvent -= OnGameFinished;
            LevelDirector.OnQuestTargetChangedEvent -= OnQuestTargetChanged;
        }
        private void Update()
        {
            _mapCam.transform.position = new Vector3(player.position.x, player.position.y, _mapCam.transform.position.z);
            _minimapCam.transform.position = new Vector3(player.position.x, player.position.y, _minimapCam.transform.position.z);
            if (isSetText)
            {
                SetText();
                isSetText = false;
            }
        }
        #endregion

        public static void SendMessage(string message, float time = 20f)
        {
            messageBox.SendMessage(message, time);
        }
        private void OnQuestTargetChanged(Transform target)
        {
            if (target == null)
            {
                this.target.transform.SetParent(transform);
                this.target.SetActive(false);
                return;
            }
            this.target.SetActive(true);
            this.target.transform.position = target.transform.position;
            this.target.transform.SetParent(target);
        }
        public static void PauseGame()
        {
            Time.timeScale = 0f;
            TurnOffAll();
            pauseScreen.SetActive(true);
            isSetText = true;
        }

        private void SetText()
        {
            maxHPText.text = "Max HP - " + hc.maxHP.ToString();
            damageText.text = "Damage - " + hc.damage.ToString();
            cooldawnDashText.text = "Cooldawn Dash - " + hc.cooldownDash.ToString();
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
        public static void OpenHTP()
        {
            TurnOffAll();
            HTPScreen.SetActive(true);
        }

        private static void TurnOffAll()
        {
            pauseButton.SetActive(false);
            mapButton.SetActive(false);
            pauseScreen.SetActive(false);
            gameOverScreen.SetActive(false);
            winScreen.SetActive(false);
            shopScreen.SetActive(false);
            HTPScreen.SetActive(false);
            map.SetActive(false);
        }
    }
}
