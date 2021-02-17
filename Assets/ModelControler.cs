using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelControler : MonoBehaviour
{

    [Header("Активная модель")]
    public GameObject curentModel;
    [SerializeField]
    public List<Model> models = new List<Model>();

    public void ChooseNewModel(int modelId)//выбрать новую модель из списка доступтных
    {
        HideAllModels();
        curentModel = models[modelId].gameObject;
        curentModel.SetActive(true);
    }

    private void HideAllModels()//скрыть все модели
    {
        foreach (Model model in models)
        {
            model.gameObject.SetActive(false);
        }
    }

}
