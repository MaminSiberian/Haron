

using UnityEngine;

public class Marina : MonoBehaviour
{
    public int index;
    public int countSouls;
    public Transform souldelivered;
    public Transform posForSoulDelivired;
    [SerializeField] private Transform target;

    private void Awake()
    {
        if (index == 1) LevelDirector.SetStartPier(target);
    }
}
