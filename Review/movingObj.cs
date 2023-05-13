using UnityEngine;

public class movingObj : MonoBehaviour
{
    public float speed = 5f;   // The speed at which the box moves
    public float distance = 5f;   // The distance the box moves in each direction
    
    private Vector3 initialPosition;   // The initial position of the box
    
    private int direction = 1;   // The direction in which the box is moving
    
    void Start()
    {
        initialPosition = transform.localPosition;
    }

    void Update()
    {
        // Move the box left or right based on the direction
        transform.Translate(Vector3.right * speed * direction * Time.deltaTime);
        
        // Check if the box has moved the desired distance in either direction
        if (transform.position.x > initialPosition.x + distance)
        {
            direction = -1;
        }
        else if (transform.position.x < initialPosition.x - distance)
        {
            direction = 1;
        }
    }
}
