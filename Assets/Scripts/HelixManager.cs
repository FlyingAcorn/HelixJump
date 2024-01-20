using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class HelixManager : Singleton<HelixManager>
{
    //BUILD ALIRKEN ADAPTIVE PERFORMANGE IGNORE DEDIN CIHANA SOR 
    public float rotationSpeed;
    public bool reverse;
    [SerializeField] private GameObject[] midHelixes;
    [SerializeField] private GameObject startHelix;
    public List<GameObject> pooledFalseHelixes = new List<GameObject>();
    public List<GameObject> pooledActiveHelixes = new List<GameObject>();
    private int _midHelixAmount = 5;
    private int _startHelixAmount = 10;
    private float _lastYPos = 23;
    private float _lastYAngle;
    private GameObject _chosenHelix;

    private void Awake()
    {
        LoadHelixes();
    }
    
    private void Update()
    {
        HelixControls();
    }
    private void HelixControls()
    {
        if ( GameManager.Instance.state is not (GameManager.GameState.Continue or GameManager.GameState.Play)) return;
        if (Input.touchCount != 1) return;
        var touchInput = Input.GetTouch(0);
        if (touchInput.phase == TouchPhase.Moved)
        {
            transform.Rotate(new Vector3(0, touchInput.deltaPosition.x),
                rotationSpeed * Time.deltaTime * (reverse ? -1 : 1));
        }
    }
    
    private void LoadHelixes()
    {
        //MidHelixes
        for (int i = 0; i < midHelixes.Length; i++)
        {
            for (int j = 0; j < _midHelixAmount; j++)
            {
                GameObject objmid = Instantiate(midHelixes[i],transform);
                pooledFalseHelixes.Add(objmid);
            }
        }
        //StartHelix
        for (int i = 0; i < _startHelixAmount; i++)
        {
            GameObject objstart = Instantiate(startHelix,transform);
            pooledFalseHelixes.Add(objstart);
        }
    }
    public void InitialHelixSpawner()
    {
        _lastYPos = 23;
        for (int i = 0; i < 5; i++)
        {
            SpawnHelix();
        }
    }
    public void SpawnHelix()
    {
        var helixSpawnTypeRando = Random.Range(0, 6);
        float randomHeight = Random.Range(2, 3);
        _lastYPos -= randomHeight;
        _lastYAngle = Random.Range(0, 360);
        var gameObjects = pooledFalseHelixes.FindAll(t => t.CompareTag("StartHelix"));
        if (helixSpawnTypeRando == 5 && gameObjects.Count >= 5)
        {
            for (int i = 0; i < 5; i++)
            {
                _chosenHelix = gameObjects.First();
                gameObjects.Remove(_chosenHelix);
                pooledFalseHelixes.Remove(_chosenHelix);
                pooledActiveHelixes.Add(_chosenHelix);
                _chosenHelix.transform.SetPositionAndRotation
                    (new Vector3(0, _lastYPos -(0.65f*i), 0),Quaternion.Euler(new Vector3(0,_lastYAngle + (i*3),0)));
                _chosenHelix.SetActive(true);
            }
            gameObjects.Clear();
        }
        else
        {
             pooledFalseHelixes.Shuffle();
             _chosenHelix = pooledFalseHelixes.First();
             pooledFalseHelixes.Remove(_chosenHelix);
             pooledActiveHelixes.Add(_chosenHelix);
             _chosenHelix.transform.SetPositionAndRotation
                 (new Vector3(0, _lastYPos, 0),Quaternion.Euler(new Vector3(0,_lastYAngle ,0)));
             _chosenHelix.SetActive(true);
        }
        _lastYPos = _chosenHelix.transform.position.y;
        _chosenHelix = null;
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
