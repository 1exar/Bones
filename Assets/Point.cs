using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    public float posX, posY, posZ;
    public string _name;
    public Meredian meredian;
    public string description;

    public void OnMouseDown()
    {
        PointEditor.pe.NewActivePoint(gameObject);
    }
}
