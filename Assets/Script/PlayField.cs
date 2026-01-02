using DG.Tweening;
using UnityEngine;

public class PlayField : MonoBehaviour
{
    [SerializeField]
    private Transform cameraTrasnform;

    private int count;

    private void Awake()
    {
        CatSpawner.CatDropEvent += MoveUp;
    }

    private void OnDestroy()
    {
        CatSpawner.CatDropEvent -= MoveUp;
    }

    private void MoveUp()
    {
        transform.DOMoveY(transform.position.y + 1.05f, 0.1f);

        if (count > 2)
            cameraTrasnform.DOMoveY(cameraTrasnform.position.y + 1.05f, 0.1f);
        else
            count++;
    }
}
