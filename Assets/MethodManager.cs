using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class MethodManager : MonoBehaviour
{
    [SerializeField] public List<Method> methods = new List<Method>();
    public InputField methodName;
    public ModelControler modelControler;
    public MeredianManager meredianManager;
    public Dropdown methodList;

    public GameObject pointPrefab;
    public GameObject[] models;

    public InputField saveString;

    static readonly string textFile = @"C:\Save\SaveStringData\save.txt";
    private string text;
    
    public string saveData;
    public void CreateNewMethod()
    {
        if (methodName.text == "") return;
        Method method = new Method();
        method.methodName = methodName.text;
        methods.Add(method);
        RefreshDropDown();
    }

    public Method Curent()
    {
        Method method = new Method();
        method.methodName = methodName.text;

        List<ModelData> models = new List<ModelData>();
        foreach (var model in modelControler.models)
        {
            if (model.points.Count != 0)
            {
                models.Add(model.ModelData());
            }
        }

        method.models = models;
        return method;
    }
    
    public void DeleteMethod()
    {
        methods.RemoveAt(methodList.value);
        RefreshDropDown();
    }

    public void OnChoeseMethodFromList()
    {
        LoadMethod(methodList.value);
    }
    
    public void RefreshDropDown()
    {
        methodList.ClearOptions();
        List<string> options = new List<string>();
        foreach (var method in methods)
        {
            string option = method.methodName;
            options.Add(option);
        }
        methodList.AddOptions(options);
    }

    public void SaveCurentMethod()
    {
        methods[methodList.value] = Curent();

    }

    public void Save()
    {
        SaveCurentMethod();
        MethodsList save = new MethodsList();
        save.methods = methods;
        save.merediands = meredianManager.meredians;
        saveData = JsonUtility.ToJson(save);
        PlayerPrefs.SetString("save", saveData);
        saveString.text = saveData;
    }

    public void Load()
    {
        MethodsList load = JsonUtility.FromJson<MethodsList>(text);
        methods = load.methods;
        meredianManager.meredians = load.merediands;
        RefreshDropDown();
        meredianManager.RefreshDropdownOptions();
        LoadMethod(0);
    }

    public void LoadMethod(int id)
    {
        foreach (var model in models)
        {
            model.GetComponent<Model>().ClearPoints();           
        }
        foreach (var model in methods[id].models)
        {
            foreach (PointData point in model.points)
            {
                var p = Instantiate(pointPrefab, models[model.id].transform);
                Vector3 pos = new Vector3(point.x, point.y, point.z);
                p.transform.position = pos;
                Point point_ = p.GetComponent<Point>();
                PointEditor.pe.points.Add(point_);
                point_._name = point.pointName;
                point_.description = point.pointDescription;
                point_.meredian = point.meredian;
                models[model.id].GetComponent<Model>().points.Add(point_);
            }
        }
    }

    public void Start()
    {
        //text = File.ReadAllText(Resources.Load<TextAsset>("save.txt"));
        text = Resources.Load<TextAsset>("save").text;
        print(text.Length);
        Load();
       // StartCoroutine(AutoSave());
    }

    public IEnumerator AutoSave()
    {
        yield return new WaitForSeconds(10);
        Save();
        StartCoroutine(AutoSave());
    }
    
}

[Serializable]
public class MethodsList
{
    public List<Method> methods = new List<Method>();
    public List<Meredian> merediands = new List<Meredian>();
}
