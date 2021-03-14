using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEditor : MonoBehaviour
{
    [SerializeField]
    public List<Point> points = new List<Point>(); //������ ����� �� ������ ������, � �������� �� ���� ��������
    public GameObject activePoint; //�������� �����, ������� �� ����������
    public Material white, red, green;// ���������(���� �����)

    public static PointEditor pe;//������ �� ��������� ������� ������

    public Slider rotatSlider;//������� ��������
    public Slider xS, yS, zS;//�������� ����������� �� �����������

    public GameObject pointPrefab;//������ ����������� �����

    public MethodManager methodManager;//������ �� ����� ���������� ��������
    public MeredianManager meredianManager;
    public ModelControler modelControler;

    public InputField pointName;
    public InputField pointDescription;

    public Text desc;

    public Dropdown pointsDrop;
    public List<Point> pointsList;

    public bool viewMode;
    
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
           // activePoint.transform.parent = null;
          //  Vector3 p = activePoint.transform.localPosition;
          //  activePoint.gameObject.transform.localPosition = new Vector3(p.x, p.y, zS.value);
          //  activePoint.transform.parent = modelControler.curentModel.transform;
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

        Point point = activePoint.GetComponent<Point>();

        if (point._name != "")
        {
            pointName.text = point._name;
        }
        else
        {
            pointName.text = "";
        }

        if (point.description != "")
        {
            if (!viewMode)
            {
                pointDescription.text = point.description;
            }
            else
            {
                desc.text = point.description;
            }
        }
        else
        {
            if (!viewMode)
            {
                pointDescription.text = "";
            }
            else
            {
                desc.text = "";
            }
        }

        if (point.meredian.name != "")
        {
            meredianManager.CheckForMeredian(point.meredian, point._name);
        }

        if (viewMode)
        {
            CheckPointMeredian(activePoint.GetComponent<Point>().meredian.name);
        }
    }

    public void CheckPointMeredian(string merName)
    {
        pointsList.Clear();
        foreach (Point p in points)
        {
            if (p.meredian.name == merName)
            {
                pointsList.Add(p);
            }
        }
        pointsDrop.ClearOptions();
        List<string> options = new List<string>();
        options.Clear();
        foreach(Point p in pointsList)
        {
            options.Add(p._name);
        }
        pointsDrop.AddOptions(options);
    }

    public void OnPointChoesen()
    {
        pointsList[pointsDrop.value].gameObject.name = "fgjilsdcjfglksdjfgikls";
        NewActivePoint(pointsList[pointsDrop.value].gameObject);
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

    public void ChangePointName()
    {
        if(activePoint)
        {
            activePoint.GetComponent<Point>()._name = pointName.text;
        }
    }

    public void ChangePointDescription()
    {
        if (activePoint)
        {
            activePoint.GetComponent<Point>().description = pointDescription.text;
        }
    }

    public void SavePoints()
    {
        List<PointData> points = new List<PointData>();
        foreach (var point in modelControler.curentModel.GetComponent<Model>().points)
        {
            PointData p = new PointData(point.posX, point.posY, point.posZ, point._name, point.description, point.meredian);
            points.Add(p);
        }

        SavedPoints data = new SavedPoints();
        data.points = points;
        string savepoints = JsonUtility.ToJson(data);
        PlayerPrefs.SetString("data", savepoints);
    }

    public void LoadPoints()
    {
        modelControler.curentModel.GetComponent<Model>().ClearPoints();
        
        SavedPoints data = JsonUtility.FromJson<SavedPoints>(PlayerPrefs.GetString("data"));
        foreach (var point in data.points)
        {
            var p = Instantiate(pointPrefab, modelControler.curentModel.transform);
            Vector3 pos = new Vector3(point.x, point.y, point.z);
            p.transform.position = pos;
            Point p_ = p.GetComponent<Point>();
            p_._name = point.pointName;
            p_.description = point.pointDescription;
            p_.meredian = point.meredian;
            modelControler.curentModel.GetComponent<Model>().points.Add(p_);
        }
    }
}
[Serializable]
public class SavedPoints
{
    public List<PointData> points = new List<PointData>();
}