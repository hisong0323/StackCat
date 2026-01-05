using UnityEngine;
using System;
using System.Collections;
using Random = UnityEngine.Random;

public class CatSpawner : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    private Cat catPrefab;

    [SerializeField]
    private CatPool catPool;

    [SerializeField]
    private AudioClip[] catDropSouds;

    #endregion

    public static Action CatDropEvent;

    #region private

    private Cat _cat;

    private int direction = 1;
    private int speed = 5;

    private int order = 11;

    private WaitForSeconds wait05 = new WaitForSeconds(0.5f);
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

            int randomInt = Random.Range(0, catDropSouds.Length);
            SoundManager.Instance.PlaySFX(catDropSouds[randomInt]);
            CatDropEvent();
            CatSpawn();
        }
    }

    private IEnumerator CatSpawnCoroutine()
    {
        yield return wait05;
        _cat = Instantiate(catPrefab, gameObject.transform);
        _cat.Init(order++);
    }

    public void CatSpawn()
    {
        StartCoroutine(CatSpawnCoroutine());
    }
}
