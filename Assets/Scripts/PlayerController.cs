using Core.Interfaces;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour, ICountable
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private ParticleSystem memoryParticle;

    [SerializeField]
    private TextMeshProUGUI score;

    private Transform playerTransform;

    private IInput input;
    private ITime time;
    private IAudiable memorySteal;

    public int Count
    {
        get;
        private set;
    }

    void Start()
    {
        playerTransform = player.GetComponent<Transform>();
        Count = 0;
    }

    private void OnEnable()
    {
        var inputManager = GameObject.Find("InputManager");
        input = inputManager.GetComponent<IInput>();

        var timeManager = GameObject.Find("TimeManager");
        time = timeManager.GetComponent<ITime>();

        memorySteal = GetComponent<IAudiable>();
    }

    void Update()
    {
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("NPC"))
        {
            Count++;
            memorySteal.Play();
            memoryParticle.Play();
            Debug.Log("Stolen memory: " + Count);
            score.text = Count.ToString();
        }
    }
}
