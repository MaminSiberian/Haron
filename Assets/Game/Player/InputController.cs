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
        if (Input.GetMouseButton(1))
        {
            hc.isDash = true;
        }

        DirectionAttack();
        DirectionMove();
    }

    private void DirectionMove()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        hc.directionMove = new Vector2(x, y).normalized;
    }

    private void DirectionAttack()
    {
        Vector2 mousePos = Input.mousePosition;
        mousePos = mainCamera.ScreenToWorldPoint(mousePos);

        hc.directionAttack = new Vector2(mousePos.x - hc.transform.position.x,
           mousePos.y - hc.transform.position.y).normalized;

    }
}

