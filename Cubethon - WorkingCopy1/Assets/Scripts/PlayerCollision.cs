using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public PlayerMovement movement;

    //New for command
    public delegate void HitObstacle(Collision collisionInfo);
    public static event HitObstacle OnHitObstacle;

    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collisionInfo)
    {
        if (collisionInfo.collider.tag == "Obstacle")
        {
            Debug.Log("We hit an obstacle!");
            if (OnHitObstacle != null)
            {
                OnHitObstacle(collisionInfo);
            }
            movement.enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
            //movement.enabled = false;
            //FindObjectOfType<GameManager>().EndGame();
        }
    }
}
