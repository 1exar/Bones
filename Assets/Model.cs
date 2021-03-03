using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model : MonoBehaviour
{

    public List<Point> points = new List<Point>();
    public int id;
    public ModelData ModelData()
    {
        ModelData modelData = new ModelData();
        List<PointData> points = new List<PointData>();

        foreach (var point in this.points)
        {
            points.Add(point.pointData());
        }

        modelData.points = points;
        modelData.id = id;
        return modelData;
    }

    public void ClearPoints()
    {
        foreach (var point in points)
        {
            if (point.gameObject)
            {
                Destroy(point.gameObject);
            }
        }
        points.Clear();
    }
}
[Serializable]
public class ModelData
{
    public List<PointData> points = new List<PointData>();
    public int id;
}