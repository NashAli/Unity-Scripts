/*
 * Graffiti Softwerks 2022
 * PlayerControllerTransform.cs
 * Author: Nash Ali
 * Creation Date: 04-16-2022
 * Last Update : 04-30-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControllerTransform : MonoBehaviour
{
    #region defines & vars

    public Transform playerTransform;
    public CharacterController controller;
    public Animator animator;
    private Vector3 playerVelocity;
    [SerializeField] private bool groundedPlayer;
    private float playerSpeed = 2.0f;
    #endregion

    #region MonoBehaviour *****************************************
    public void Start()
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();
    }


    void Update()
    {
        GetPad();
    }
    #endregion *******************

    #region User Code ******************************************************************************
    /// <summary>
    /// Code to get data from the on-screen gamepad.
    /// </summary>
    void GetPad()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }
        var gamepad = Gamepad.current;
        if (gamepad != null)
        {
            float moveAxis = gamepad.leftStick.x.ReadValue();
            float rotationAxis = gamepad.leftStick.y.ReadValue();

            if (moveAxis != 0)
            {
                Vector3 move = new(moveAxis, 0, rotationAxis);
                move.Normalize();
                //animator.SetBool("Waiting_b", false);
                animator.SetFloat("Speed_f", .5f);
                controller.Move(playerSpeed * Time.deltaTime * move);
                playerTransform.forward = move;
            }
            else
            {
                animator.SetFloat("Speed_f", 0f);
                //animator.SetBool("Waiting_b", true);
            }
        }
    }
  

    #endregion ***********************************************************************************************
}




