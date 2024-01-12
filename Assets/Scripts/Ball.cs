using System;
using UnityEngine;
public class Ball : MonoBehaviour
{
    private Rigidbody _myBody;
    [SerializeField] private int bounceForce;
    private void Start()
    {
        _myBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
    }

    private void VelocityEffects()
    {
       // istediğin effectleri buldun onları bağla
       // trigger olduğunda olacakalrda var onu bağla
       // cok hızlı indiğinde helixchunklar parcalansın sonra yokolsun onuda ayarla
    }
    
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.layer == 6)
        {
            _myBody.velocity = Vector2.up * bounceForce;
        }
        if (trigger.gameObject.layer == 7)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
