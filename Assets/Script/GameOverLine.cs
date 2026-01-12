using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        CheckGameOver(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        CheckGameOver(collision);
    }

    private void CheckGameOver(Collider2D collision)
    {
        if (GameManager.Instance.IsGameOver)
            return;

        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            if (rigidbody.linearVelocity.y < -1)
            {
                if (!GameManager.Instance.HasRevive && ScoreManager.Instance.CurrentScore >= 20)
                {
                    GameManager.GameOverEvent?.Invoke();
                }
                else
                {
                    GameManager.GameEndEvent?.Invoke();
                }
            }
        }
    }
}