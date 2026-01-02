using UnityEngine;

public class Cat : MonoBehaviour
{
    [SerializeField]
    private Sprite[] catSprites;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = catSprites[Random.Range(0, catSprites.Length)];
    }

    public void Init(int order)
    {
        _renderer.sortingOrder = order;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        ScoreManager.Instance.IncreasesScore(1);
        Destroy(this);
    }
}
