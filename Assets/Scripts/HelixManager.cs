using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelixManager : Singleton<HelixManager>
{
    [SerializeField] private GameObject[] midHelixes;
    [SerializeField] private GameObject startHelix;
    [SerializeField] private GameObject winHelix;
    public List<GameObject> pooledHelixes = new List<GameObject>();
    private int _midHelixAmount = 5;
    private int _startHelixAmount = 10;
    private float _lastYPos = 23;
    private float _lastYAngle;
    private GameObject _chosenHelix;

    protected override void Awake()
    {
        base.Awake();
        LoadHelixes();
    }

    public void LoadHelixes()
    {
        //MidHelixes
        for (int i = 0; i < midHelixes.Length; i++)
        {
            for (int j = 0; j < _midHelixAmount; j++)
            {
                GameObject objmid = Instantiate(midHelixes[i],transform);
                pooledHelixes.Add(objmid);
            }
        }
        //StartHelix
        for (int i = 0; i < _startHelixAmount; i++)
        {
            GameObject objstart = Instantiate(startHelix,transform);
            pooledHelixes.Add(objstart);
        }
        // winHelix
        /*GameObject objwin = Instantiate(winHelix,transform);
        pooledHelixes.Add(objwin);*/
    }
    public void InitialHelixSpawner()
    {
        for (int i = 0; i < 5; i++)
        {
            SpawnHelix();
        }
    }
    //HELIX TRIGGERLA HELIX CONTROLLERI KONTROL ET VE PLAYTEST YAP
    public void SpawnHelix()
    {
        var helixSpawnTypeRando = Random.Range(0, 6);
            float randomHeight = Random.Range(2, 3);
            _lastYPos -= randomHeight;
            _lastYAngle = Random.Range(0, 360);
            var gameObjects = pooledHelixes.FindAll(t => t.CompareTag("StartHelix"));
            if (helixSpawnTypeRando == 5 && gameObjects.Count >= 5)
            {
                for (int i = 0; i < 5; i++)
                {
                    _chosenHelix = gameObjects.First();
                    _chosenHelix.transform.SetPositionAndRotation
                        (new Vector3(0, _lastYPos -(0.65f*i), 0),Quaternion.Euler(new Vector3(0,_lastYAngle + (i*3),0)));
                    _chosenHelix.SetActive(true);
                    gameObjects.Remove(_chosenHelix);
                    pooledHelixes.Remove(_chosenHelix);
                }
            }
            else
            {
                 pooledHelixes.Shuffle();
                 _chosenHelix = pooledHelixes.First();
                 _chosenHelix.transform.SetPositionAndRotation
                     (new Vector3(0, _lastYPos, 0),Quaternion.Euler(new Vector3(0,_lastYAngle ,0)));
                 _chosenHelix.SetActive(true);
                pooledHelixes.Remove(_chosenHelix);
            }
            _lastYPos = _chosenHelix.transform.position.y;
    }
}

public static class Shuffler 
{
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            (ts[i], ts[r]) = (ts[r], ts[i]);
        }
    }
}
