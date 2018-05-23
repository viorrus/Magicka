using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Stat : ScriptableObject {

    public int id;
    public string stringId;
    private string statName;
    [SerializeField]
    private float _value;
    [SerializeField]
    private float Value
    {
        get
        {
            return _value;
        }
        set
        {
            if(value <= minValue)
            {
                value = minValue;
                if (valueisMin!=null)
                {
                    valueisMin();
                }
            }
            else if(value >= maxValue)
            {
                value = maxValue;
                if (valueisMax != null)
                {
                    valueisMax();
                }
            }
            if(_value != value)
            {
                if (valueChanged!=null)
                {
                    valueChanged();
                } 
            }
            _value = value;

        }
    }
    [SerializeField]
    private float minValue;
    [SerializeField]
    private float maxValue;
    public Action valueisMin;
    public Action valueisMax;
    public Action valueChanged;
    public Sprite spriteBar;
    public Sprite spriteIcon;



    public string GetName()
    {
        return statName;
    }

    public float GetValue()
    {
        return Value;
    }

    public float GetProcent()
    {
        return Value / (maxValue - minValue);
    }

    public void AddValue(float amount)
    {
        Value += amount;
    }
    public void SetValue(float amount)
    {
        Value = amount;
    }

    public bool isMax()
    {
        return Value == maxValue;
    }

    public bool isMin()
    {
        return Value == minValue;
    }

#if UNITY_EDITOR
    [MenuItem("Tools/CreateStat")]
    public static void Create()
    {
        string type = Selection.activeObject.name;
        var asset = ScriptableObject.CreateInstance(type);
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Stats/" + type + ".asset");
        AssetDatabase.SaveAssets();
    }
#endif

}
