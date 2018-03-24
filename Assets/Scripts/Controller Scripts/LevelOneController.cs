using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelOneController : MonoBehaviour
{
    //      ************       //
    // Evasive Maneuver Script //
    private float targetManeuver;
    public float dodge = 5f;
    public float smoothing = 7.5f;
    public float tilt = 10f;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWait;
    public Boundary boundary;

    // enemy ship flies toward player
    //private Transform playerTransform;
    private float currentSpeed;

    public Transform playerTransform;
    private Transform enemyTransform;
    // Evasive Maneuver Script //
    //      ************       //


    //enum Waves { Zero, One, Two, Three };
    //Waves wave = Waves.Zero;

    public float thrust;

    public Transform target;
    public float speed;

    Vector3 right = new Vector3(1f, 0, 0); //Vector in the direction you want to move in.
    Vector3 left = new Vector3(-3f, 0, 0); //Vector " 

    private float enemyPosition;
    private float playerPosition;

    float[] enemyXPositions = new float[10];
    Vector3[] enemyPositions;
    private int enemyNumber;  // enemy number associated with enemyPositions
                                // i.e. enemyPositions[enemyNumber] = enemyPositions[i]






    // commented out stuff I don't think I need for this controller script
    // or if they're duplicates from the other class I copied over

    public GameObject[] hazards;
    //public static GameObject[] hazardsArray;
    public Vector3 spawnValues;
    public int hazardCount;
    //public float spawnWait;
    //public float startWait;
    public float waveWait;
    //public int waveCount;

    float xPos = 3f;  // leftmost x position on playing field
    float zPos = 3f;  // bottom-most z position on playing field
    float xOffset = 0f;
    float zOffset = 0f;
    float xMax = 12f;  // rightmost x position on playing field
    float zMax = 12f;  // uppermost z position on playing field

    public GameObject shot;
    public Transform shotSpawn;
    // automate player/enemy moves


    enum IsMoving { None, Left, Right }
    IsMoving moving = IsMoving.None;
    Vector3 testVector;

    //LevelOnePlayerController levelOnePlayerController = new LevelOnePlayerController();


    // Use this for initialization
    void Start ()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        //StartCoroutine(SpawnWaves());
        SpawnWaveOne();
    }

    // Update is called once per frame
    void Update ()
    {
        // NOTE: This way of automating movement works,
        // but think about how to do it with a Rigidbody and FixedUpdate()
        // for more realistic movement and quaternion rotation
        
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        playerPosition = playerTransform.position.x;

        if (moving == IsMoving.Left && playerTransform.position.x <= enemyPosition)  // enemy and player x positions are the same, so just fire
        {
            Debug.Log("enemy is at the same x position as player");
            // Instantiate shot here
            moving = IsMoving.None;
        }
        else if (moving == IsMoving.Right && playerTransform.position.x >= enemyPosition)
        {
            Debug.Log("enemy was on the right but is at same x pos as player now");
            // Instantiate shot here
            moving = IsMoving.None;
        }

        if (moving == IsMoving.Left)
        {
            playerTransform.Translate(Vector3.left * Time.deltaTime);
            //StartCoroutine(SmoothMove(left, 1f)); // .0001f is the speed
        }
        if (moving == IsMoving.Right)
        {
            playerTransform.Translate(Vector3.right * Time.deltaTime);
            //StartCoroutine(SmoothMove(right, 1f)); // .0001f is the speed
        }
    }

    //IEnumerator SpawnWaves()
    private void SpawnWaveOne()
    {
        enemyPositions = new Vector3[10];
        enemyNumber = 0;  // enemyNumber should be set to 0 at start of each spawn

        //yield return new WaitForSeconds(startWait);
        for (int i = 0; i < hazardCount; i++)
        {
            GameObject hazard = hazards[1];

            Vector3 spawnPosition = new Vector3(xPos + xOffset, 0f, zPos + zOffset);
            Quaternion spawnRotation = Quaternion.identity;
            Instantiate(hazard, spawnPosition, spawnRotation);

            enemyPositions[i] = new Vector3(xPos + xOffset, 0f, zPos + zOffset);
            enemyXPositions[i] = (xPos + xOffset);

            // NOTE: all three of these positions print the correct numbers
            Debug.Log("[Vector3] enemyPositions[i] = " + enemyPositions[i]);
            Debug.Log("[float] enemyXpositions[i] = " + enemyXPositions[i]);
            Debug.Log("enemy " + i + "'s position is: " + (xPos + xOffset));

            xOffset += 3;
            if (xPos >= xMax && zPos < zMax) { xOffset = 3; zOffset += 3; }
        }

    }



    // Use this for initialization
    public void WaveOneAction()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        enemyTransform = GameObject.FindGameObjectWithTag("Enemy").transform;
        //rb = GetComponent<Rigidbody>();

    }

    public void WaveTwoAction()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        testVector = new Vector3(13, 0, 0);

        Debug.Log("WaveTwoAction invoked...");
        Debug.Log("Player location is: " + playerTransform.position);

        for (int i = 0; i < 4; i++)
        {
            if (i == 0)
            {
                enemyPosition = 3;
            }
            else
            {
                enemyPosition = i * 3;
            }
            //enemyPosition = i + 3;// enemyXPositions[i];
            //enemyTransform.position = enemyPositions[i];
            Debug.Log("Enemy's position: " + enemyPosition);

            //while (playerTransform.position.x < enemyPosition)
            if (playerTransform.position.x < enemyPosition)
            {//Using update to capture the keypress.
                Debug.Log("player is farther right than enemy");
                //playerTransform.Translate(Vector3.right);// * Time.deltaTime);

                moving = IsMoving.Right;  // tells Update() to call SmoothMove() Coroutine with a right direction

                //levelOnePlayerController.MoveRight(playerTransform, 3f);
            }
            //while (playerTransform.position.x > enemyPosition)
            if (playerTransform.position.x > enemyPosition)
            {
                Debug.Log("player is farther right than enemy");
                //playerTransform.Translate(Vector3.left);// * Time.deltaTime);

                moving = IsMoving.Left;  // tells Update() to call SmoothMove() Coroutine with a left direction

                //levelOnePlayerController.MoveLeft(playerTransform, 3f);
            }

            Wait();
        }


    }

    void WaveThreeAction()
    {
        /////////////////////////
    }

    
    private void FixedUpdate()
    {
        /*
            rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
            currentSpeed = rb.velocity.z;

            float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
            rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
            rb.position = new Vector3
                (
                    Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
                    0.0f,
                    Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
                );

            rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
            */
    }
    

    IEnumerator SmoothMove(Vector3 direction, float speed)
    {
        /*
        while (playerTransform.position.x != testVector.x)
        {
            playerTransform.Translate(new Vector3())
        }
        */
        
        Vector3 enemyPos = new Vector3(3f, 0f, 9f);
        float startime = Time.time;
        Vector3 start_pos = playerTransform.position; //Starting position.
        Vector3 end_pos = enemyPositions[enemyNumber] + direction; //Ending position.
        //enemyNumber++;

        Debug.Log("end_pos = " + end_pos);
        Debug.Log("enemy position = " + enemyPositions[enemyNumber]);
        Debug.Log("player_pos = " + playerTransform.position);

        /*
        while (playerTransform.position.x != end_pos.x && ((Time.time - startime) * speed) < 1f)
        {
            float move = Mathf.Lerp(playerTransform.position.x, end_pos.x, (Time.time - startime) * speed);
            playerTransform.position += direction * move *.001f;  // * .001f slows down the movement

            yield return null;
        }
        */
        

        yield return new WaitForSeconds(5);
    }

    IEnumerator Wait()
    {
        Debug.Log("Before Waiting 7 seconds");
        yield return new WaitForSeconds(7);
        Debug.Log("After Waiting 7 Seconds");
    }

}
