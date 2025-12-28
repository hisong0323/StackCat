using UnityEngine;
using DG.Tweening;

public class TextAnimation : MonoBehaviour
{
    private void Start()
    {
        transform.DOScale(Vector3.one * 1.2f, 1).SetEase(Ease.InQuad).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);        
    }
}
