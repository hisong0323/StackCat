using UnityEngine;
using System.Collections.Generic;
using System;

public class IceCreamSpawner : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    private IceCream iceCreamPrefab;

    [SerializeField]
    private IceCreamPool iceCreamPool;

    [SerializeField]
    private AudioClip iceCreamDropSound;

    #endregion

    public static Action IceCreamDropEvent;

    #region private

    private IceCream _iceCream;

    private int direction = 1;
    private int speed = 5;

    private int order = 11;

    private float timer;

    private bool canSpawn = false;

    #endregion

    private void Awake()
    {
        GameManager.TouchEvent += DropIceCream;
        GameManager.GameStartEvent += IceCreamSpawn;
    }

    private void OnDestroy()
    {
        GameManager.TouchEvent -= DropIceCream;
        GameManager.GameStartEvent -= IceCreamSpawn;
    }

    private void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0;
                IceCreamSpawn();
                canSpawn = false;
            }
        }
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.right * speed * direction * Time.fixedDeltaTime);

        if (Mathf.Abs(transform.position.x) >= 2)
            direction *= -1;
    }

    private void DropIceCream()
    {
        if (_iceCream != null)
        {
            iceCreamPool.Register(_iceCream.gameObject);

            _iceCream.GetComponent<Rigidbody2D>().simulated = true;
            _iceCream = null;

            SoundManager.Instance.PlaySFX(iceCreamDropSound);
            IceCreamDropEvent();

            canSpawn = true;
        }
    }

    public void IceCreamSpawn()
    {
        _iceCream = Instantiate(iceCreamPrefab, gameObject.transform);
        _iceCream.Init(order++);
    }
}
