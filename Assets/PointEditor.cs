using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEditor : MonoBehaviour
{
    private List<Point> points = new List<Point>(); //������ ����� �� ������ ������, � �������� �� ���� ��������
    private GameObject activePoint; //�������� �����, ������� �� ����������
    public Material white, red;// ���������(���� �����)

    public static PointEditor pe;//������ �� ��������� ������� ������

    public Slider rotatSlider;//������� ��������
    public Slider xS, yS, zS;//�������� ����������� �� �����������

    public GameObject pointPrefab;//������ ����������� �����

    public MethodManager methodManager;//������ �� ����� ���������� ��������
    public ModelControler modelControler;
    public void ChangePositionX()//������ ��������� ����� �� X
    {
        if (activePoint)
        {
            Vector3 p = activePoint.transform.position;
            activePoint.gameObject.transform.position = new Vector3(xS.value, p.y, p.z);
        }
    }

    public void ChangePositionY()//������ ��������� ����� �� Y
    {
        if (activePoint)
        {
            Vector3 p = activePoint.transform.position;
            activePoint.gameObject.transform.position = new Vector3(p.x, yS.value, p.z);
        }
    }

    public void ChangePositionZ()//������ ��������� ����� �� Z
    {
        if (activePoint)
        {
            Vector3 p = activePoint.transform.localPosition;
            activePoint.gameObject.transform.localPosition = new Vector3(p.x, p.y, zS.value);
        }
    }

    private void Awake()
    {
        pe = this;//�������������� ������ �� ������ �����
    }

    public void NewActivePoint(GameObject newPoint)//������ ����� ��������. ����������� ��� ������� �� �����
    {
        if (activePoint)
        {
            activePoint.GetComponent<MeshRenderer>().material = white;
        }

        activePoint = newPoint;
        activePoint.GetComponent<MeshRenderer>().material = red;

        //������ ��������� ��������� ��������, ����� ��� �������������� ����� ����� �� ���������
        xS.value = activePoint.transform.position.x;
        yS.value = activePoint.transform.position.y;
        zS.value = activePoint.transform.localPosition.z;

    }

    public void AddNewPoint()//������� ����� �����
    {
        var pointGameObject = Instantiate(pointPrefab, modelControler.curentModel.transform);
        Point point = pointGameObject.GetComponent<Point>();
        points.Add(point);
        modelControler.curentModel.GetComponent<Model>().points.Add(point);
    }

    public void DeletePoint()//������� ��������� �����
    {
        if (activePoint)
        {
            Point point = activePoint.GetComponent<Point>();
            points.Remove(point);
            modelControler.curentModel.GetComponent<Model>().points.Remove(point);
            Destroy(activePoint);
        }
    }
}
