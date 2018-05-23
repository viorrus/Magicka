using System.Collections;
using System.Collections.Generic;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class Unit : ScriptableObject {
    public string unitID;
    public string unitName;
    public string unitClass;
    public List<Stat> stats;
    public List<Skill> skills;
    public Sprite sprite;
    public float cost;

    public Stat AddAndGetStat(Stat stat)
    {
        var temp = stats.Where(x => x.stringId == stat.stringId).FirstOrDefault();
        if (temp == null)
        {
            stats.Add(stat);
            return stat;
        }
        else
        {
            return temp;
        }
    }

    public void AddStat(Stat stat)
    {
        if (stats.Where(x => x.stringId == stat.stringId).FirstOrDefault() == null)
        {
            stats.Add(stat);
        }
    }

    public Skill GetSkillByName(string nameSkill)
    {
        return skills.Where(x => x.skillName == nameSkill).FirstOrDefault();
    }

    public bool isHasSkillByName(string nameSkill)
    {
        return skills.Where(x => x.skillName == nameSkill).FirstOrDefault()!=null;
    }

    public Stat GetStatByName(string nameStat)
    {
        return stats.Where(x => x.stringId == nameStat).FirstOrDefault();
    }

    public bool isHasStatByName(string nameStat)
    {
        return stats.Where(x => x.stringId == nameStat).FirstOrDefault() != null;
    }



#if UNITY_EDITOR
    [MenuItem("Tools/CreateUnit")]
    public static void Create()
    {
        string type = Selection.activeObject.name;
        var asset = ScriptableObject.CreateInstance(type);
        AssetDatabase.CreateAsset(asset, "Assets/Prefabs/Units/" + type + ".asset");
        AssetDatabase.SaveAssets();
    }
#endif
}
