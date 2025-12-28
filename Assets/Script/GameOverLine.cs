using UnityEngine;

public class GameOverLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("IceCream"))
        {
            if (collision.GetComponent<Rigidbody2D>().linearVelocity.y < -1)
            {
                Debug.Log("게임오버");
                GameManager.GameOverEvent?.Invoke();
            }
        }
    }
}
