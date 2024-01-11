using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody _myBody;
    [SerializeField] private int bounceForce;
    
    void Start()
    {
        _myBody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            _myBody.velocity = Vector2.up * bounceForce;
        }
        if (collision.gameObject.layer == 7)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
