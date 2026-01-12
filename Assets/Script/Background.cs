using UnityEngine;

public class Background : MonoBehaviour
{
    private void Awake()
    {
        CatSpawner.CatDropEvent += ScrollUp;
    }

    private void OnDestroy()
    {
        CatSpawner.CatDropEvent -= ScrollUp;
    }

    private void ScrollUp()
    {
        Debug.Log(Camera.main.WorldToViewportPoint(transform.position).y);
        if (Camera.main.WorldToViewportPoint(transform.position).y < -0.7f)
        {
            float randomFloat = Random.Range(-0.6f, 0.6f);
            transform.position = new Vector3(randomFloat, transform.position.y + 32, 0);
            Debug.Log("배경 이동");
        }
    }
}
