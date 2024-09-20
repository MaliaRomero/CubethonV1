using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public Rigidbody rb;
    public float forwardForce = 2000f;
    public float sidewaysForce = 500f;
    //For command pattern
    GameManager gameManager;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void FixedUpdate()
    {
        rb.AddForce(0, 0, forwardForce * Time.deltaTime);

        if (Input.GetKey("d"))
        {
            // rb.AddForce(sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);

            // instead of just calling addforce, we want to package this up as a command
            // and send to an invoker
            // we'll need a command class, some commands, and an invoker...
            Command moveRight = new MoveRight(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveRight);
            invoker.ExecuteCommand();
        }

        if (Input.GetKey("a"))
        {
            //rb.AddForce(-sidewaysForce * Time.deltaTime, 0, 0, ForceMode.VelocityChange);
            Command moveLeft = new MoveLeft(rb, sidewaysForce);
            Invoker invoker = new Invoker();
            invoker.SetCommand(moveLeft);
            invoker.ExecuteCommand();
        }

        if (rb.position.y < -1f)
        {
            FindObjectOfType<GameManager>().EndGame(null);
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
