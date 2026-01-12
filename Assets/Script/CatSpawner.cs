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

    private WaitForSeconds wait06 = new WaitForSeconds(0.6f);
    #endregion

    private void Awake()
    {
        GameManager.GameStartEvent += CatSpawn;
    }

    private void OnDestroy()
    {
        GameManager.GameStartEvent -= CatSpawn;
    }

    private void Update()
    {
        if (GameManager.Instance.IsGameOver)
            return;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            DropCat();
        }

#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
        {
            DropCat();
        }
#endif
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
        yield return wait06;
        _cat = Instantiate(catPrefab, gameObject.transform);
        _cat.Init(order++);
    }

    public void CatSpawn()
    {
        StartCoroutine(CatSpawnCoroutine());
    }
}
