/*
 * Graffiti Softwerks 2022
 * SunMotion.cs
 * Author: Nash Ali
 * Creation Date: 06-29-2022
 * Last Update: 06-29-2022
 * 
 * Copyright (c) Graffiti Softwerks 2022
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunMotion : MonoBehaviour
{
    public float theta = 0.001f * Time.deltaTime;
    // Update is called once per frame
    void Update()
    {
        this.transform.RotateAround(Vector3.zero, Vector3.right,theta);
    }
}