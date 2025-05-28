

using System;
using UnityEngine;

public static class CursorUtility
{
    public static void Show() => Cursor.visible = true;
    public static void Hide() => Cursor.visible = false;
    public static void Lock() => Cursor.lockState = CursorLockMode.Locked;
    public static void UnLock() => Cursor.lockState = CursorLockMode.None;

    public static void AdjustForPlayer()
    {
        Hide();
        Lock();
    }

    public static void AdjustForUI()
    {
        Show();
        UnLock();
    }
}