using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;



public abstract class Skill : NetworkBehaviour {
    public string skillName;
    public int manacost;
    public float castTime;
    public float cdTime;
    public float durationTime;
    public float castRange;
    public float skillPower;//использовать по умолчанию
    [HideInInspector]
    public float timer;
    [Range(0,100)]
    public float usePriority;
    public bool isReady;
    public bool isAlwaysUse;
    public Sprite skillImage;
    public UnitObject unit;


    abstract public  void CheckSkill();
    abstract public void UseSkill();
    abstract public void Init();
    abstract public void Setup(Skill skill);

#if UNITY_EDITOR
    [MenuItem("Tools/CreateSO")]
    public static void Create()
    {
        string type = Selection.activeObject.name;
        var asset = ScriptableObject.CreateInstance(type);
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Skill/" + type + ".asset");
        AssetDatabase.SaveAssets();
    }

   
#endif

}
