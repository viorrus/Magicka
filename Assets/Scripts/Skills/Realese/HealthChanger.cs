using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

public class HealthChanger : StatChanger {

    public SimpleBar bar;
    /*[SerializeField]
    [SyncVar(hook = "NetChanger")]
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
            if (value <= minValue)
            {
                value = minValue;
                if (valueisMin != null)
                {
                    valueisMin();
                }
            }
            else if (value >= maxValue)
            {
                value = maxValue;
                if (valueisMax != null)
                {
                    valueisMax();
                }
            }
            if (_value != value)
            {
                _value = value;
                if (valueChanged != null)
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
    public Action<float> valueChanged;*/
    public Sprite spriteBar;
    public Sprite spriteIcon;


   

    //void NetChanger(float value)
    //{
    //    this._value = value;
    //    bar.SetValue(GetProcent());

    //}

    //public float GetProcent()
    //{
    //    return Value / (maxValue - minValue);
    //}


    public override void Init()
    {
        //base.Init();
        var temp = unit.GetComponentsInChildren<Stat>().Where(x => x.stringId == stat.stringId).FirstOrDefault();
        bar = Instantiate(bar, unit.transform);
        temp.valueChanged += bar.SetValue;
        temp.Setup(stat);
        bar.Init(stat.spriteBar);
        stat = temp;
        unit.unitBase.AddAndGetStat(stat); 
        unit.damageAct += CmdOnDamageHasTaken;
      
    
        //NetChanger(_value);
    }

 
    [Command]
    public void CmdOnDamageHasTaken(float damage,string id)
    {
        if (isServer && id != unit.ID)
        {
            // Value -= damage;
            stat.AddValue(-damage);
            (unit as UnitPlayerObject).idLastHit = id;
        }
     
        
      //  RpcSetHealth(damage);

    }

    public override void Setup(Skill skill)
    {
        base.Setup(skill);

        bar = (skill as HealthChanger).bar;
      /*  minValue = (skill as HealthChanger).minValue;
       maxValue = (skill as HealthChanger).maxValue;
        if(_value == 0)
        {
            _value = maxValue;
        }
        else
        {
            NetChanger(_value);
        }*/
    }

}
