using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class MapGenerator : MonoBehaviour {

    private static MapGenerator instance;
    public static MapGenerator Instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<MapGenerator>();
            }
            return instance;
        }
    }

    public List<Block> blocks;
    public List<string> map;
    static string blackData;
    public Transform startPoint;
    public float xSize;
    public float ySize;
    public int xCount;
    public int yCount;
    List<BlockObject> mapBlocks;

    public void Start()
    {
        GenerateMap();
    }


    public void MakePreGen()
    {
        map = new List<string>();
        map.AddRange(blackData.Replace('\n', ' ').Replace("\r", "").Trim('\n').Split(' '));
    }

    public void GenerateMap()
    {
        mapBlocks = new List<BlockObject>();
        int x = 0;
        int k = 0;
        int y = 0;
        for(int i=0; i < map.Count; i++)
        {
            var tempBlock = GetBlockByID(map[i]);
            
            k = i/xCount;
            x = k == y ? x : 0;
            if (tempBlock != null)
            {
                mapBlocks.Add(Instantiate(tempBlock.blockObject, (startPoint.position + new Vector3(x * xSize, -k * ySize, 0)), startPoint.rotation, startPoint));
                mapBlocks[mapBlocks.Count - 1].unitBase = tempBlock;
            }
            x++;
            y = k;
        }
    }

    public Block GetBlockByID(string id)
    {
        return blocks.Where(x => x.unitID == id).FirstOrDefault();
    }

#if UNITY_EDITOR

    [MenuItem("Tools/Read words")]
    private static void ReadString()
    {
        string path = "Assets/Resources/test.txt";

        //Read the text from directly from the test.txt file
        StreamReader reader = new StreamReader(path);
        blackData = reader.ReadToEnd();
        MapGenerator.Instance.MakePreGen();
        reader.Close();
    }

#endif


}
