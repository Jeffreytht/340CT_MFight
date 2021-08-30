using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkinRepo : MonoBehaviour
{
    private static SkinRepo _instance;
    public List<GameObject> playerObjs = new List<GameObject>();

    private SkinRepo() { }

    void Awake()
    {
        if (_instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
            foreach (GameObject obj in playerObjs)
                DontDestroyOnLoad(obj);
        }
    }

    public static Sprite GetSprite(int index)
    {
        if (index >= 0 && index < _instance.playerObjs.Count)
            return _instance.playerObjs[index].GetComponent<SpriteRenderer>().sprite;
        return null;
    }

    public static GameObject GetObject(int index)
    {
        if (index >= 0 && index < _instance.playerObjs.Count)
            return _instance.playerObjs[index];
        return null;
    }

    public static int Count {
        get
        {
            return _instance.playerObjs.Count;
        }
    }
}
