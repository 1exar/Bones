using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point : MonoBehaviour
{

    public float posX, posY, posZ;
    public string _name;
    [SerializeField] public Meredian meredian;
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