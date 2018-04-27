using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


[RequireComponent(typeof(Rigidbody))]
public class PlayerBehaviour : MonoBehaviour
{
    public GameObject introText;
    public GameObject endText;
    public GameObject cam;
    public GameObject ghost;
    public GameObject master;
    public int sceneIndex;
    public int allowedGhost;

    public int imagination;

    public Text playerStats;

    public bool reverse = false;
    private CameraBehaviour instance;
    private GhostScript instance2;
    private Vector3 start = new Vector3(-5.79f, 0.07f, -0.9279823f);
    public Vector3 manualStart;
    public bool activeGhost = false;
    public bool gotBoost = false;
    public int activeGhosts = 0;
    public int sentRequest = 0;
    public bool lockMove = false;
    public bool alreadyEnd = false;

    public ParticleSystem poof;

    private Queue<MyStatus> playerOnQueue = new Queue<MyStatus>();

    [System.Serializable]
    public class InputSettings
    {
        public string SIDEWAYS_AXIS = "Horizontal";
        public string JUMP_AXIS = "Jump";
    }

    public MoveSettings moveSettings;
    public InputSettings inputSettings;
    private Rigidbody playerRigidbody;
    private Vector3 velocity;
    private float sidewaysInput, jumpInput;

    void Awake()
    {
        velocity = Vector3.zero;
        sidewaysInput = jumpInput = 0;
        moveSettings = GetComponent<MoveSettings>();
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
        sceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    // Use this for initialization
    void Start()
    {
        UpdateStats(); 
        if (manualStart.x == 0 && manualStart.y == 0 && manualStart.z == 0)
           manualStart = start;
        transform.position = manualStart;
        instance = cam.GetComponent<CameraBehaviour>();
        instance2 = ghost.GetComponent<GhostScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneIndex);
        }
        GetInput();
        if (Input.GetKeyDown(KeyCode.Z) && reverse == false && imagination > 0 && !activeGhost)
        {
            Instantiate(poof, transform.position, transform.rotation);
            startGhost();
            imagination--;
            UpdateStats();
            transform.position = manualStart;
            gotBoost = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && gotBoost == false)
        {
            foreach (Transform child in master.transform)
            {
                child.gameObject.GetComponent<MoveSettings>().runVelocity += 4;
                gotBoost = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            Debug.Log(playerRigidbody.velocity.y);
        }
        else
            savePlayer();
    }

    void FixedUpdate()
    {
        
        if (lockMove)
        {
            if (Input.GetKey(KeyCode.N))
            {
                SceneManager.LoadScene(sceneIndex + 1);
            }
            if (alreadyEnd == false)
            {
                Destroy(introText);
                Instantiate(endText);
                alreadyEnd = true;
            }     
            return;
        }
        if (!playerRigidbody.isKinematic)
        {
            if (!instance.isMoving)
            {
                Run();
                Jump();
            }
        }
        
    }

    // Update is called once per frame
    void GetInput()
    {
        if (inputSettings.SIDEWAYS_AXIS.Length != 0)
            sidewaysInput = Input.GetAxis(inputSettings.SIDEWAYS_AXIS);
        if (inputSettings.JUMP_AXIS.Length != 0)
            jumpInput = Input.GetAxisRaw(inputSettings.JUMP_AXIS);
    }

    void Run()
    {
        velocity.x = sidewaysInput * moveSettings.runVelocity;
        if (reverse)
            velocity.x = -velocity.x;
        velocity.y = playerRigidbody.velocity.y;
        playerRigidbody.velocity = transform.TransformDirection(velocity);
    }

    void Jump()
    {
        if (jumpInput != 0 && Grounded())
        {
            playerRigidbody.velocity = new Vector3(playerRigidbody.velocity.x,
                moveSettings.jumpVelocity, playerRigidbody.velocity.z);
        }

    }

    bool Grounded()
    {
        return Physics.Raycast(transform.position, Vector3.down,
            moveSettings.distanceToGround, moveSettings.ground);
    }

    void savePlayer()
    {
        MyStatus status = new MyStatus();
        status.jumpInput = jumpInput;
        status.sidewaysInput = sidewaysInput;
        playerOnQueue.Enqueue(status);
    }

    void startGhost()
    {
        if (!activeGhost)
        {
            Instantiate(ghost);
            activeGhost = true;
            activeGhosts++;
        }
    }

    public MyStatus[] getPlayerOnQueue()
    {
        MyStatus[] clonedQueue = playerOnQueue.ToArray();
        return clonedQueue;
    }

    public void clearQueue()
    {
        playerOnQueue.Clear();
    }

    public void setActiveGhost()
    {
        sentRequest++;
        if (sentRequest == activeGhosts)
        {
            sentRequest = 0;
            activeGhost = false;
        }
    }

    public void UpdateStats()
    {
        playerStats.text = "Imagination: " + imagination.ToString();
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.tag == "Ammo")
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }
}
