using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.UI;

public class Ball : MonoBehaviour
{
    public Player player1;
    public Player player2;

    public float speed;
    public TextMeshProUGUI scoreText;

    public int maxScore = 3;
    public Image startPanel;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    }


    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Goal1")
        {
            player2.Score++;
            scoreText.text = player1.Score + " - " + player2.Score;
            if ( player2.Score >= maxScore)
            {
                startPanel.gameObject.SetActive(true);
                startPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Player 2 Wins!\nClick to Restart";
                return;
            }
            ResetBall();
        }
        else if (collision.gameObject.name == "Goal2")
        {
            player1.Score++;
            scoreText.text = player1.Score + " - " + player2.Score;
            if ( player1.Score >= maxScore)
            {
                startPanel.gameObject.SetActive(true);
                startPanel.GetComponentInChildren<TextMeshProUGUI>().text = "Player 1 Wins!\nClick to Restart";
                return;
            }

            ResetBall();
        }
    }

    public void StartGame(int numPlayer)
    {
        startPanel.gameObject.SetActive(false);
        player1.Score = 0;
        player2.Score = 0;
        scoreText.text = player1.Score + " - " + player2.Score;
        if ( numPlayer == 1)
        {
            player1.isCPU = false;
            player2.isCPU = true;
        }
        else if ( numPlayer == 2)
        {
            player1.isCPU = false;
            player2.isCPU = false;
        }
        ResetBall();
    }

    void ResetBall()
    {
        transform.position = Vector3.zero;
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        StartCoroutine(WaitingForStart());
    }
    
    IEnumerator WaitingForStart()
    {

        yield return new WaitForSeconds(2);

        // Genera un angolo casuale tra 30 e 150 gradi (-30 a -150 per il lato opposto)
        float angle;
        if (Random.value < 0.5f)
            angle = Random.Range(30f, 60f);
        else
            angle = Random.Range(120f, 150f);

        if (Random.value < 0.5f) angle = -angle; // 50% di probabilità di andare nell'altra direzione
    
        // Converte l'angolo in radianti e crea il vettore direzione
        float rad = angle * Mathf.Deg2Rad;
        Vector2 randomDirection = new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * speed;
    
        GetComponent<Rigidbody2D>().AddForce(randomDirection, ForceMode2D.Impulse);
    }
}
