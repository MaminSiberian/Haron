using Haron;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    [SerializeField] private HaronController hc;
    [SerializeField] internal Camera mainCamera;
    private Ray ray;

    private void Start()
    {
        hc = FindAnyObjectByType<HaronController>();
        mainCamera = Camera.main;
    }
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            hc.isMoving = true;
        }
        else
            hc.isMoving = false;
        //Vector2 mousePos = Input.mousePosition;

        //mousePos = mainCamera.WorldToScreenPoint(mousePos);

        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        hc.direction = new Vector2(mousePos.x - hc.transform.position.x,
           mousePos.y - hc.transform.position.y).normalized;


        //hc.direction = mousePos.normalized;
        //ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        //if (Physics.Raycast(ray, out RaycastHit raycastHit, float.MaxValue, hc.layerGround))
        //{
        //    hc.direction = new Vector3(
        //            raycastHit.point.x - hc.transform.position.x,
        //            hc.transform.position.y,
        //            raycastHit.point.z - hc.transform.position.z);
        //}
    }
}

