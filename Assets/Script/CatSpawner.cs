using UnityEngine;
using System.Collections.Generic;
using System;

public class CatSpawner : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    private Cat catPrefab;

    [SerializeField]
    private CatPool catPool;

    [SerializeField]
    private AudioClip catDropSoud;

    #endregion

    public static Action CatDropEvent;

    #region private

    private Cat _cat;

    private int direction = 1;
    private int speed = 5;

    private int order = 11;

    private float timer;

    private bool canSpawn = false;

    #endregion

    private void Awake()
    {
        GameManager.TouchEvent += DropCat;
        GameManager.GameStartEvent += CatSpawn;
    }

    private void OnDestroy()
    {
        GameManager.TouchEvent -= DropCat;
        GameManager.GameStartEvent -= CatSpawn;
    }

    private void Update()
    {
        if (canSpawn)
        {
            timer += Time.deltaTime;
            if (timer >= 0.5f)
            {
                timer = 0;
                CatSpawn();
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

    private void DropCat()
    {
        if (_cat != null)
        {
            catPool.Register(_cat.gameObject);

            _cat.GetComponent<Rigidbody2D>().simulated = true;
            _cat = null;

            SoundManager.Instance.PlaySFX(catDropSoud);
            CatDropEvent();

            canSpawn = true;
        }
    }

    public void CatSpawn()
    {
        _cat = Instantiate(catPrefab, gameObject.transform);
        _cat.Init(order++);
    }
}
