using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public enum CollisionTag
    {
        BounceWall,
        Player,
        ScoreWall
    }

    [SerializeField] private float ballSpeed = 8f;
    [SerializeField] private List<string> tags;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip scoreClip;
    [SerializeField] private AudioClip wallCollideClip;
    [SerializeField] private AudioClip playerCollideClip;
    private Vector2 velocity;

    void Start()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    void Update()
    {
        transform.Translate(velocity * ballSpeed * Time.deltaTime);
    }

    private void ResetBall()
    {
        transform.position = Vector2.zero;
        velocity = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
    }

    /// <summary>
    /// Checks for collision. Then either resets the ball to starting position, increase score, plays audio, or sends the ball to a different direction
    /// based on what the ball collides with
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(tags[(int)CollisionTag.ScoreWall]))
        {
            ResetBall();
            GameManager.IncrementScore(other.GetComponent<ScoreWall>().scoringPlayer);
            audioSource.clip = scoreClip;
            audioSource.Play();
        }

        else if (other.CompareTag(tags[(int)CollisionTag.BounceWall]))
        {
            velocity.y = -velocity.y;
            audioSource.clip = wallCollideClip;
            audioSource.Play();
        }
        else if (other.CompareTag(tags[(int)CollisionTag.Player]))
        {
            velocity.x = -velocity.x;
            velocity.y = transform.position.y - other.transform.position.y;
            velocity = velocity.normalized;
            audioSource.clip = playerCollideClip;
            audioSource.Play();
        }
    }
    /// </summary>
}
