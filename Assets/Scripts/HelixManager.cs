using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HelixManager : Singleton<HelixManager>
{
    [SerializeField] private GameObject[] midHelixes;
    [SerializeField] private GameObject startHelix;
    [SerializeField] private GameObject winHelix;
    private List<GameObject> allHelixTypes = new List<GameObject>();
    public List<GameObject> pooledHelixes = new List<GameObject>();
    private int _midHelixAmount = 5;
    private int _startHelixAmount = 10;
    private int _winHelixAmount = 1;
    private Coroutine _spawnerCoroutine;
    private float _lastYPos = 23;
    private float _lastYAngle;

    protected override void Awake()
    {
        base.Awake();
        LoadHelixes();
        for (int i = 0; i < midHelixes.Length; i++)
        {
            allHelixTypes.Add(midHelixes[i]);
        }
        allHelixTypes.Add(startHelix);
    }

    public void LoadHelixes()
    {
        //MidHelixes
        for (int i = 0; i < midHelixes.Length; i++)
        {
            for (int j = 0; j < _midHelixAmount; j++)
            {
                GameObject objmid = Instantiate(midHelixes[i],transform);
                objmid.SetActive(false);
                pooledHelixes.Add(objmid);
            }
        }
        //StartHelix
        for (int i = 0; i < _startHelixAmount; i++)
        {
            GameObject objstart = Instantiate(startHelix,transform);
            objstart.SetActive(false);
            pooledHelixes.Add(objstart);
        }
        // winHelix
        GameObject objwin = Instantiate(startHelix,transform);
        objwin.SetActive(false);
        pooledHelixes.Add(objwin);
    }

    private GameObject GetHelix()
    {
        for (int i = 0; i < pooledHelixes.Count; i++)
        {
            if (pooledHelixes[i].CompareTag("StartHelix"))
            {
                return pooledHelixes[i];
            }
        }

        return null;
    }

    public void HelixSpawner()
    {
        _spawnerCoroutine = StartCoroutine(SpawnerCoroutine());
    }
    
    
// sorun  beyaz chunk yok olmuyor sorun 2 2 kere kırılıyor bazen sorun 3 obje nullsa duruyor
// sorun 4? bütün objelerin addlist ve removuna bak shatter olduğunda ne olduğunu vs ayarla
    IEnumerator SpawnerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            var helixSpawnTypeRando = Random.Range(0, 6);
            var randomHeight = Random.Range(3, 5);
            _lastYAngle = Random.Range(0, 360);
            if (helixSpawnTypeRando == 5)
            {
                _lastYPos -= randomHeight;
                for (int i = 0; i < 5; i++)
                {
                    var startingHelix = GetHelix();
                    if (startHelix == null) yield return null;
                    startingHelix.transform.position = new Vector3(0, _lastYPos -0.65f, 0);
                    _lastYPos = startingHelix.transform.position.y;
                    startingHelix.transform.Rotate(new Vector3(0,_lastYAngle + (i*10),0));
                    startingHelix.SetActive(true);
                    pooledHelixes.Remove(startingHelix);
                }
            }
            else
            {
                pooledHelixes.Shuffle();
                var midHelix = pooledHelixes.First();
                midHelix.transform.position = new Vector3(0, _lastYPos - randomHeight, 0);
                _lastYPos = midHelix.transform.position.y;
                midHelix.transform.Rotate(new Vector3(0,_lastYAngle ,0));
                midHelix.SetActive(true);
                pooledHelixes.Remove(midHelix);
            }
        }
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
            var r = UnityEngine.Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}
