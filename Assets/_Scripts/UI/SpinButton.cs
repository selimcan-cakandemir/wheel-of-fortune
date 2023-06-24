using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SpinButton : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        Actions.OnSpin();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {

    }
}
