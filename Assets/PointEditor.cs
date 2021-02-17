using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointEditor : MonoBehaviour
{
    private List<Point> points = new List<Point>(); //список точек на данной модели, с которыми мы счас работаем
    private GameObject activePoint; //активная точка, которую мы перемещаем
    public Material white, red;// материалы(цвет точек)

    public static PointEditor pe;//ссылка на экземпляр данного класса

    public Slider rotatSlider;//слайдер поворота
    public Slider xS, yS, zS;//слайдеры перемещения по координатом

    public GameObject pointPrefab;//префаб создаваемой точки

    public MethodManager methodManager;//ссылка на класс управления методами
    public ModelControler modelControler;
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
            Vector3 p = activePoint.transform.localPosition;
            activePoint.gameObject.transform.localPosition = new Vector3(p.x, p.y, zS.value);
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
}
