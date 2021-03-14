using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MeredianManager : MonoBehaviour
{

    public List<Meredian> meredians = new List<Meredian>();//список мередиан
    public InputField meredianNameField;//строка ввода названия мередианы
    public Dropdown meredianListDropDown;//выпадающий список мередиан

    public bool viewMode;
    
    public void CreateNewMeredian()//создать новую мередиану
    {
        string meredianName = meredianNameField.text;
        if (meredianName == "") return;
        Meredian meredian = new Meredian(meredianName);
        meredians.Add(meredian);
        RefreshDropdownOptions();
    }

    public void DeleteCurentMeredian()//удалить выбрануюю мередиану
    {
        meredians.RemoveAt(meredianListDropDown.value);
        RefreshDropdownOptions();
    }

    public void RefreshDropdownOptions()//обновить список мередиан
    {
        meredianListDropDown.ClearOptions();
        List<string> options = new List<string>();
        foreach(Meredian meredian in meredians)
        {
            options.Add(meredian.name);
        }
        meredianListDropDown.AddOptions(options);
    }

    public void CheckForMeredian(Meredian meredian, string namePoint)
    {
        bool has = false;
        int id = 0;
        foreach (Meredian m in meredians)
        {
            if (m.name == meredian.name)
            {
                has = true;
                break;
            }

            id++;
        }
        if (has)
        {
            meredianListDropDown.value = id;
        }
    }

    public void OnSelectMeredian()
    {
        if (!viewMode)
        {
            if (PointEditor.pe.activePoint)
            {
                PointEditor.pe.activePoint.GetComponent<Point>().meredian = meredians[meredianListDropDown.value];
            }
        }
        else
        {
            PointEditor.pe.CheckPointMeredian(meredians[meredianListDropDown.value].name);
        }
    }

    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z)) OnSelectMeredian();
    }
}
