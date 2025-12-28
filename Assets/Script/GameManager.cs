using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action TouchEvent;
    public static Action GameStartEvent;
    public static Action GameOverEvent;
    public static Action GameEndEvent;
    public static Action ReviveEvent;

    private void Awake()
    {
        GameStartEvent += UnfreezeTime;
        GameOverEvent += FreezeTime;
        ReviveEvent += UnfreezeTime;
    }

    private void OnDestroy()
    {
        GameOverEvent -= FreezeTime;
        GameStartEvent -= UnfreezeTime;
    }

    private void Update()
    {
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began || Input.GetMouseButtonDown(0))
        {
            TouchEvent?.Invoke();
        }
    }

    private void FreezeTime()
    {
        Time.timeScale = 0;
    }

    private void UnfreezeTime()
    {
        Time.timeScale = 1;
    }
}
