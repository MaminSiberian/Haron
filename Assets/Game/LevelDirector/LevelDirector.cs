using NaughtyAttributes;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class LevelDirector : MonoBehaviour
{
    public static int soulsGoal { get { return 5; } }
    public static int deliveredSoulsCounter {  get; private set; }
    public static int keysCounter { get; private set; }
    public static Transform lastPier { get; private set; }

    public static event Action OnSoulDeliveredEvent;
    public static event Action OnKeysValueChangedEvent;
    public static event Action<Transform> OnQuestTargetChangedEvent;
    public static event Action OnGameFinishedEvent;

    private static LevelDirector instance;
    private static DifficultyContoller dc;
    private static Transform nextPier = null;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject);
        instance = this;
        deliveredSoulsCounter = 0;
        dc = FindAnyObjectByType<DifficultyContoller>();
    }
    private void OnEnable()
    {
        Shop.OnItemPurchasedEvent += OnItemPurchased;
    }
    private void OnDisable()
    {
        Shop.OnItemPurchasedEvent -= OnItemPurchased;
    }
    public static void SetStartPier(Transform pier)
    {
        lastPier = pier;
    }
    public static void AddObject(GameObject obj)
    {
        dc.AddObject(obj);
    }
    public static void SendNewQuestTarget(Transform target)
    {
        if (nextPier != null) lastPier = nextPier;
        nextPier = target;
        OnQuestTargetChangedEvent?.Invoke(target);
    }
    [Button]
    public static void OnSoulDelivered()
    {
        deliveredSoulsCounter++;
        if (deliveredSoulsCounter >= soulsGoal)
        {
            FinishGame();
        }
        OnSoulDeliveredEvent?.Invoke();
    }
    private void OnItemPurchased(Item item)
    {
        if (item == Item.Key) GetKey();
    }
    [Button]
    private void GetKey()
    {
        keysCounter++;
        OnKeysValueChangedEvent?.Invoke();
    }
    [Button]
    public static void WasteKey()
    {
        if (!GotKey()) return;
        keysCounter--;
        OnKeysValueChangedEvent?.Invoke();
    }
    public static bool GotKey()
    {
        return keysCounter > 0;
    }
    private static void FinishGame()
    {
        OnGameFinishedEvent?.Invoke();
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
