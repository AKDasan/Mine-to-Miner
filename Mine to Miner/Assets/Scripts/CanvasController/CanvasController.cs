using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    public static event Action<bool> OnCanvasToggle;

    public static void ToggleCanvas(bool isOpen)
    {
        OnCanvasToggle?.Invoke(isOpen);
    }
}
