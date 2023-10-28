using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class LevelDirector
{
    public static int soulsGoal { get { return 10; } }
    public static int deliveredSoulsCounter {  get; private set; }
    public static event Action<Transform> OnQuestTargetChanged;
    public static event Action OnGameFinished;

    public static void SendNewQuestTarget(Transform target)
    {
        OnQuestTargetChanged?.Invoke(target);
    }
    public static void OnSoulDelivered()
    {
        deliveredSoulsCounter++;
        if (deliveredSoulsCounter >= soulsGoal)
        {
            FinishGame();
        }
    }
    private static void FinishGame()
    {
        OnGameFinished?.Invoke();
    }
    public static void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
