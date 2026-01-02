using System.Collections.Generic;
using UnityEngine;

public class CatPool : MonoBehaviour
{
    private List<GameObject> catPool = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Revive();
        }
    }

    private void Awake()
    {
        GameManager.ReviveEvent += Revive;
    }

    private void OnDestroy()
    {
        GameManager.ReviveEvent -= Revive;
    }

    public void Register(GameObject gameObject)
    {
        catPool.Add(gameObject);
        gameObject.transform.parent = transform;
        if (catPool.Count >= 15)
        {
            Destroy(catPool[catPool.Count - 15].gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void Revive()
    {
        if (catPool[catPool.Count - 1].TryGetComponent<Cat>(out Cat iceCream))
        {
            Destroy(iceCream);
        }

        for (int i = 0; i < catPool.Count; i++)
        {
            catPool[i].gameObject.transform.rotation = Quaternion.identity;
            catPool[i].gameObject.transform.position = Vector3.up * (i + 1.5f) * 1.05f;
            Destroy(catPool[i].gameObject.GetComponent<Rigidbody2D>());
            Debug.Log(Vector3.up * (i + 2.2f) * 0.5f);
        }
    }
}
