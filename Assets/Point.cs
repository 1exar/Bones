using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    public float posX, posY, posZ;
    public string _name;
    [SerializeField]
    public Meredian meredian;
    public string description;

    public void OnMouseDown()
    {
        PointEditor.pe.NewActivePoint(gameObject);
    }

    public void LateUpdate()
    {
        var pos = transform.position;
        posX = pos.x;
        posY = pos.y;
        posZ = pos.z;
    }

    public PointData pointData()
    {
        PointData pointData = new PointData(posX, posY, posZ, _name, description, meredian);
        return pointData;
    }

    public void Start()
    {
        if (meredian != null)
        {
            gameObject.name = meredian.name;
        }
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1)) _name = "1";
        if (Input.GetKeyDown(KeyCode.Keypad2)) _name = "1a";
        if (Input.GetKeyDown(KeyCode.Keypad3)) _name = "1b";
        if (Input.GetKeyDown(KeyCode.Keypad4)) _name = "2";
        if (Input.GetKeyDown(KeyCode.Keypad5)) _name = "2a";
        if (Input.GetKeyDown(KeyCode.Keypad6)) _name = "3";
        if (Input.GetKeyDown(KeyCode.Keypad7)) _name = "4";
    }
}
[Serializable]
public class PointData
{
    public float x, y, z;
    public string pointName;
    public string pointDescription;
    public Meredian meredian;
    public PointData(float x, float y, float z, string pointName, string pointDescription, Meredian meredian)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.pointName = pointName;
        this.pointDescription = pointDescription;
        this.meredian = meredian;
    }
}