using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
[Serializable]
public class JumpButton : Button {
    
    public bool isPres()
    {
        return IsPressed();
    }
}
