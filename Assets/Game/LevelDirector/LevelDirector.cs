using NaughtyAttributes;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDirector : MonoBehaviour
{
    public static int soulsGoal { get { return 10; } }
    public static int deliveredSoulsCounter {  get; private set; }
    public static int keysCounter { get; private set; }

    public static event Action OnSoulDeliveredEvent;
    public static event Action OnKeysValueChangedEvent;
    public static event Action<Transform> OnQuestTargetChangedEvent;
    public static event Action OnGameFinishedEvent;

    private static LevelDirector instance;

    private void Awake()
    {
        if (instance != null) Destroy(gameObject); 
        instance = this;
    }
    private void OnEnable()
    {
        Shop.OnItemPurchased += OnItemPurchased;
    }
    private void OnDisable()
    {
        Shop.OnItemPurchased -= OnItemPurchased;
        
    }


    public static void SendNewQuestTarget(Transform target)
    {
        OnQuestTargetChangedEvent?.Invoke(target);
    }
    public static void OnSoulDelivered()
    {
        OnSoulDeliveredEvent?.Invoke();
        deliveredSoulsCounter++;
        if (deliveredSoulsCounter >= soulsGoal)
        {
            FinishGame();
        }
    }
    private void OnItemPurchased(Item item)
    {
        if (item == Item.Key) GetKey();
    }
    public static void GetKey()
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
