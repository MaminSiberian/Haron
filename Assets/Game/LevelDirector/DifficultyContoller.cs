using System.Collections.Generic;
using UnityEngine;

public class DifficultyContoller : MonoBehaviour
{
    [SerializeField] private List<GameObject> level0 = new List<GameObject>();
    [SerializeField] private List<GameObject> level1 = new List<GameObject>();
    [SerializeField] private List<GameObject> level2 = new List<GameObject>();
    [SerializeField] private List<GameObject> level3 = new List<GameObject>();
    [SerializeField] private List<GameObject> level4 = new List<GameObject>();


    private void Awake()
    {
        ReloadWorld();
    }
    private void OnEnable()
    {
        LevelDirector.OnSoulDeliveredEvent += ReloadWorld;
    }
    private void OnDisable()
    {
        LevelDirector.OnSoulDeliveredEvent -= ReloadWorld;
    }

    private void ReloadWorld()
    {
        TurnOffObjects();

        switch (LevelDirector.deliveredSoulsCounter)
        {
            case 0:
                TurnOnObjects(level0);
                break;
            case 1:
                TurnOnObjects(level1);
                break;
            case 2:
                TurnOnObjects(level2);
                break;
            case 3:
                TurnOnObjects(level3);
                break;
            case 4:
                TurnOnObjects(level4);
                break;
            default:
                TurnOffObjects();
                break;
        }
    }
    private void TurnOnObjects(List<GameObject> levelObjects)
    {
        foreach (var lvl in levelObjects)
        {
            lvl.SetActive(true);
        }
    }
    private void TurnOffObjects()
    {
        foreach (var lvl in level0)
        {
            lvl.SetActive(false);
        }
        foreach (var lvl in level1)
        {
            lvl.SetActive(false);
        }
        foreach (var lvl in level2)
        {
            lvl.SetActive(false);
        }
        foreach (var lvl in level3)
        {
            lvl.SetActive(false);
        }
        foreach (var lvl in level4)
        {
            lvl.SetActive(false);
        }
    }
}
