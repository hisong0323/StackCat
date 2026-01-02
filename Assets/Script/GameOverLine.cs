using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<Rigidbody2D>(out Rigidbody2D rigidbody))
        {
            if (rigidbody.linearVelocity.y < -1)
            {
                Debug.Log("게임오버");
                GameManager.GameOverEvent?.Invoke();
            }
        }
    }
}
