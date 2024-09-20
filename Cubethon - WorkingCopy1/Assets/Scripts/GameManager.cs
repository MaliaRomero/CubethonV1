using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    //public float restartDelay = 2f;
    public GameObject completeLevelUI;
    //new for command

    bool instantReplay = false;
    GameObject player;
    float replayStartTime;

    //NEW
    private void OnEnable()
    {
        PlayerCollision.OnHitObstacle += EndGame;
    }

    //NEW
    private void OnDisable()
    {
        PlayerCollision.OnHitObstacle -= EndGame;
    }

    //NEW
    // Start is called before the first frame update
    void Start()
    {
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        player = playerMovement.gameObject;

        if (CommandLog.commands.Count > 0)
        {
            instantReplay = true;
            replayStartTime = Time.timeSinceLevelLoad;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (instantReplay)
        {
            RunInstantReplay();
        }
    }

    //From original
    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    //Changed
    //public void EndGame()
    public void EndGame(Collision collisionInfo)
    {
        player.GetComponent<PlayerMovement>().enabled = false;
        PlayerCollision.OnHitObstacle -= EndGame;

        if (collisionInfo != null)
        {
            Debug.Log("Hit: " + collisionInfo.collider.name);
        }

        // this flag prevents responding to multiple hit events:
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Invoke("Restart", 2f);
        }
        /*
        if (gameHasEnded == false)
        {
            gameHasEnded = true;
            Debug.Log("Game Over");
            Restart();
            Invoke("Restart", restartDelay);
        }*/
    }

    //Moved out of end game
    void Restart ()
    {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void RunInstantReplay()
    {

        Debug.Log("Replay!");
        if (CommandLog.commands.Count == 0)
        {
            return;
        }

        Command command = CommandLog.commands.Peek();
        if (Time.timeSinceLevelLoad >= command.timestamp) // + replayStartTime)
        {
            command = CommandLog.commands.Dequeue();
            command._player = player.GetComponent<Rigidbody>();
            Invoker invoker = new Invoker();
            invoker.disableLog = true;
            invoker.SetCommand(command);
            invoker.ExecuteCommand();
        }
    }
}
