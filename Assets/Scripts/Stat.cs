using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class Stat : NetworkBehaviour {

    public int id;
    public string stringId;
    private string statName;

    [SerializeField]
    [SyncVar(hook ="NetChanger")]
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
                _value = value;
                if (valueChanged!=null)
                {
                    valueChanged(GetProcent());
                } 
            }
          

        }
    }
    [SerializeField]
    private float minValue;
    [SerializeField]
    private float maxValue;
    public Action valueisMin;
    public Action valueisMax;
    public Action<float> valueChanged;
    public Sprite spriteBar;
    public Sprite spriteIcon;


    void NetChanger(float value)
    {
        this._value = value;
        if (valueChanged != null)
        {
            valueChanged(GetProcent());
        }
    }

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

    public bool IsMax()
    {
        return Value == maxValue;
    }

    public bool IsMin()
    {
        return Value == minValue;
    }

    public void Setup(Stat stat)
    {
       
        minValue = stat.minValue;
        maxValue = stat.maxValue;
        Value = stat.Value;
        if (_value == 0)
        {
            _value = maxValue;
        }  
            NetChanger(_value);
        spriteBar = stat.spriteBar;
        spriteIcon = stat.spriteIcon;
        stringId = stat.stringId;
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
