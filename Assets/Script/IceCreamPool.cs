using System.Collections.Generic;
using UnityEngine;

public class IceCreamPool : MonoBehaviour
{
    private List<GameObject> iceCreamPool = new();

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
        iceCreamPool.Add(gameObject);
        gameObject.transform.parent = transform;
        if (iceCreamPool.Count >= 15)
        {
            Destroy(iceCreamPool[iceCreamPool.Count - 15].gameObject.GetComponent<Rigidbody2D>());
        }
    }

    private void Revive()
    {
        if (iceCreamPool[iceCreamPool.Count - 1].TryGetComponent<IceCream>(out IceCream iceCream))
        {
            Destroy(iceCream);
        }

        for (int i = 0; i < iceCreamPool.Count; i++)
        {
            iceCreamPool[i].gameObject.transform.rotation = Quaternion.identity;
            iceCreamPool[i].gameObject.transform.position = Vector3.up * (i + 1.5f) * 1.05f;
            Destroy(iceCreamPool[i].gameObject.GetComponent<Rigidbody2D>());
            Debug.Log(Vector3.up * (i + 2.2f) * 0.5f);
        }
    }
}
