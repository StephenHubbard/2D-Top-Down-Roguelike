using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    public bool isOverUI { get; private set; }

    public void EnterUI() {
        isOverUI = true;
    }

    public void ExitUI() {
        isOverUI = false;
    }
}
