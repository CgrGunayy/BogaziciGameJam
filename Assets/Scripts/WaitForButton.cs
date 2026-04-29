using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForButton : CustomYieldInstruction
{
    public override bool keepWaiting => !Input.GetButtonDown(buttonName);
    private string buttonName;

    public WaitForButton(string buttonName)
    {
        this.buttonName = buttonName;
    }
}
