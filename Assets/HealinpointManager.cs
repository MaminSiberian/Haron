using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealinpointManager : MonoBehaviour
{
    [SerializeField] private List<GameObject> HealPoints;
    [SerializeField] private float respawnTiem;

    private void Start()
    {
        StartCoroutine(RespawnHealPoint());
    }

    private IEnumerator RespawnHealPoint()
    {
        while (true)
        {
            Debug.Log(1);
            yield return new WaitForSeconds(respawnTiem);
            foreach (var point in HealPoints)
            {
                if (point.activeSelf == false)
                {
                    point.SetActive(true);
                }
            }
        }
    }
}
