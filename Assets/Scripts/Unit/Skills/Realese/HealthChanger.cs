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
    public float armor;
    public string punchSound;


   

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
        if (bar != null && stat.spriteBar !=null)
        {
            bar = Instantiate(bar, unit.transform);
            temp.valueChanged += bar.SetValue;
            temp.valueChanged += HitSound;
            bar.Init(stat.spriteBar);
        }
        temp.Setup(stat);
        stat = temp;
        unit.unitBase.AddAndGetStat(stat); 
        unit.damageAct += CmdOnDamageHasTaken;
      
        //NetChanger(_value);
    }

    void HitSound(float value)
    {
        SoundManager.Instance.PlaySound(punchSound);
    }

 
    [Command]
    public void CmdOnDamageHasTaken(float damage,NetworkInstanceId id)
    {
        if (isServer && id != unit.ID)
        {
            // Value -= damage;
            stat.AddValue(-damage+armor);
            if(unit is UnitObject)
            {
                (unit as UnitObject).idLastHit = id;
            }
           
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
