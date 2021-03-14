using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ActivationManager : MonoBehaviour
{

    public string Identifier;
    public string key;

    public InputField key_, identifiner_;
    
    void Start()
    {
        Identifier = SystemInfo.deviceUniqueIdentifier;
        key_.text = Identifier;
        GeneratedCode(Identifier);
    }

    public string GeneratedCode(string identifer)
    {
        string code = "";
        
        List<int> nums = new List<int>();
        
        foreach (var c in identifer)
        {
            int num = (int) Char.GetNumericValue(c);
            nums.Add(num);
        }

        foreach (var num in nums)
        {
            if (num >= 0)
            {
                code += num / 2;
            }
        }
        
        string result = "";
        
        foreach (char c in code)
        {
            if (c != '0')
            {
                result += c;
            }
        }

        key = result;
        return result;
    }

    public void CheckKey()
    {
        if (identifiner_.text == key)
        {
            SceneManager.LoadScene(1);
        }
    }
    
}
