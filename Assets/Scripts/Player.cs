using UnityEngine;

public class Player : MonoBehaviour
{
    public int Score = 0;
    public float speed = 10f;

    Rigidbody2D rb;

    public bool isCPU = false;
    public Rigidbody2D ball;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void FixedUpdate()
    {
        if (isCPU)
        {   if ( ( transform.position.x > 0 && ball.linearVelocity.x > 0 ) || ( transform.position.x < 0 && ball.linearVelocity.x < 0 ))
            {
                float step = speed * Time.fixedDeltaTime;
                Vector2 targetPosition = new Vector2(rb.position.x, ball.position.y);
                Vector2 newPos = Vector2.MoveTowards(rb.position, targetPosition, step);
                newPos.y = Mathf.Clamp(newPos.y, -3.5f, 3.5f);
                rb.MovePosition(newPos);
            }
        }
        else
        {
            Vector2 newPos = rb.position + speed * Time.fixedDeltaTime * new Vector2(0, Input.GetAxis("Vertical"));
            newPos.y = Mathf.Clamp(newPos.y, -3.5f, 3.5f);
            rb.MovePosition(newPos);

        }
    }

}
