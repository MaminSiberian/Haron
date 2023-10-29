using System.Collections.Generic;
using UnityEngine;

public class DifficultyContoller : MonoBehaviour
{
    [SerializeField] private List<GameObject> objects = new List<GameObject>();

    private void OnEnable()
    {
        LevelDirector.OnSoulDeliveredEvent += ReloadWorld;
    }
    private void OnDisable()
    {
        LevelDirector.OnSoulDeliveredEvent -= ReloadWorld;
    }
    public void AddObject(GameObject obj)
    {
        objects.Add(obj);
    }
    public void ReloadWorld()
    {
        ObjectsSetActive(false);
        ObjectsSetActive(true);
    }
    private void ObjectsSetActive(bool state)
    {
        foreach (var obj in objects)
        {
            obj.SetActive(state);
        }
    }
}
