using UnityEngine;
public class Ball : MonoBehaviour
{
    private Rigidbody _myBody;
    [SerializeField] private int bounceForce;
    [SerializeField] private GameObject flame;
    [SerializeField] private GameObject smoke;
    [SerializeField] private GameObject hit;
    [SerializeField] private GameObject magicHit;
    private float _velocityY;

    private void Awake()
    {
        GameManager.OnGameStateChanged += GameManagerOnOnGameStateChanged;
    }

    private void GameManagerOnOnGameStateChanged(GameManager.GameState state)
    {
        if (state is GameManager.GameState.Play)
        {
            gameObject.transform.position = new Vector3(0, 25, -2);
            _myBody.constraints = (RigidbodyConstraints)122;
            _myBody.velocity = new Vector3(0, 0, 0);
        }
        else if (state is GameManager.GameState.Continue)
        {
            _myBody.constraints = (RigidbodyConstraints)122;
            _myBody.velocity = new Vector3(0,_velocityY,0);
            _velocityY = 0;
        }
        else
        {
            _velocityY = _myBody.velocity.y;
            _myBody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    private void OnDisable()
    {
        GameManager.OnGameStateChanged -= GameManagerOnOnGameStateChanged;
    }

    private void Start()
    {
        _myBody = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider trigger)
    {
        if (trigger.TryGetComponent(out HelixTrigger helixTrigger))
        {
            helixTrigger.Score();
            flame.gameObject.SetActive(GameManager.Instance.ComboCount >= 3);
            smoke.gameObject.SetActive(GameManager.Instance.ComboCount >= 5);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        var gameManager = GameManager.Instance;
        if (collision.gameObject.layer == 7 && smoke.gameObject.activeSelf == false)
        {
            gameManager.UpdateGameState(GameManager.GameState.Lose);
        }
        else if (collision.gameObject.layer is 7 or 6)
        {
            _myBody.velocity = Vector2.up * bounceForce;
        }
        Instantiate(gameManager.ComboCount >=5 ? magicHit : hit, transform.position, smoke.transform.rotation);
        SfxManager.Instance.PlaySfx(gameManager.ComboCount >=5 ? 2 : 1);
        HelixController allChunks = collision.gameObject.GetComponentInParent<HelixController>();
        if (gameManager.ComboCount >=5) allChunks.Shatter();
        flame.gameObject.SetActive(false);
        smoke.gameObject.SetActive(false);
        gameManager.ComboCount = 0;
    }
}
