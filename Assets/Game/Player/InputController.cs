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
            hc.isAttacking = true;
        }
        else
            hc.isAttacking = false;

        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        hc.direction = new Vector2(x, y).normalized;
        //Vector2 mousePos = Input.mousePosition;
        //mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        //hc.direction = new Vector2(mousePos.x - hc.transform.position.x,
        //   mousePos.y - hc.transform.position.y).normalized;

    }
}

