using UnityEngine;
public class Ball : MonoBehaviour
{
    private Rigidbody _myBody;
    [SerializeField] private int bounceForce;
    [SerializeField] private GameObject flame;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject magicHit;
    private void Start()
    {
        _myBody = GetComponent<Rigidbody>();
    }
    private void Update()
    {
        VelocityEffects();
    }
    private void VelocityEffects()
    { 
        flame.gameObject.SetActive(_myBody.velocity.y < -10);
        smoke.gameObject.SetActive(_myBody.velocity.y < -15);
    }

    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.gameObject.layer == 8)
        {
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Instantiate(smoke.gameObject.activeSelf ? magicHit : hit, transform.position, smoke.transform.rotation);
            HelixController allChunks = collision.gameObject.GetComponentInParent<HelixController>();
            if (smoke.gameObject.activeSelf) allChunks.Shatter();
            _myBody.velocity = Vector2.up * bounceForce;
        }
        if (collision.gameObject.layer == 7)
        {
            GameManager.Instance.UpdateGameState(GameManager.GameState.Lose);
        }
    }
}
