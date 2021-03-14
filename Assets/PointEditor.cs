using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEditor : MonoBehaviour
{
    [SerializeField]
    public List<Point> points = new List<Point>(); //список точек на данной модели, с которыми мы счас работаем
    public GameObject activePoint; //активная точка, которую мы перемещаем
    public Material white, red, green;// материалы(цвет точек)

    public static PointEditor pe;//ссылка на экземпляр данного класса

    public Slider rotatSlider;//слайдер поворота
    public Slider xS, yS, zS;//слайдеры перемещения по координатом

    public GameObject pointPrefab;//префаб создаваемой точки

    public MethodManager methodManager;//ссылка на класс управления методами
    public MeredianManager meredianManager;
    public ModelControler modelControler;

    public InputField pointName;
    public InputField pointDescription;

    public Text desc;

    public Dropdown pointsDrop;
    public List<Point> pointsList;

    public bool viewMode;
    
    public void ChangePositionX()//меняем положение точки по X
    {
        if (activePoint)
        {
            Vector3 p = activePoint.transform.position;
            activePoint.gameObject.transform.position = new Vector3(xS.value, p.y, p.z);
        }
    }

    public void ChangePositionY()//меняем положение точки по Y
    {
        if (activePoint)
        {
            Vector3 p = activePoint.transform.position;
            activePoint.gameObject.transform.position = new Vector3(p.x, yS.value, p.z);
        }
    }

    public void ChangePositionZ()//меняем положение точки по Z
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
        pe = this;//инициализируем ссылку на данный класс
    }

    public void NewActivePoint(GameObject newPoint)//делаем точку активной. Выполняется при нажатии на точку
    {
        if (activePoint)
        {
            activePoint.GetComponent<MeshRenderer>().material = white;
        }
        
        activePoint = newPoint;
        activePoint.GetComponent<MeshRenderer>().material = red;

        //меняем положение ползунков слайдера, чтобы при перетаскивании точка резко не дергалась
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
    
    public void AddNewPoint()//создаем новую точку
    {
        var pointGameObject = Instantiate(pointPrefab, modelControler.curentModel.transform);
        Point point = pointGameObject.GetComponent<Point>();
        points.Add(point);
        modelControler.curentModel.GetComponent<Model>().points.Add(point);
    }

    public void DeletePoint()//удаляем выбранную точку
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