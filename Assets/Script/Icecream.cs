using UnityEngine;
using UnityEngine.UIElements;

public class IceCream : MonoBehaviour
{
    [SerializeField]
    private Sprite[] iceCreamSprites;

    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
        _renderer.sprite = iceCreamSprites[Random.Range(0, iceCreamSprites.Length)];
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
