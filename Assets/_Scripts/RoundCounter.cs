using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class RoundCounter : Singleton<RoundCounter>
{
    public int roundCount = 1;
    public TextMeshProUGUI ui;

    private void OnEnable()
    {
        Actions.ChangeCounter += ChangeCounter;
    }

    private void OnDisable()
    {
        Actions.ChangeCounter -= ChangeCounter;
    }

    private void ChangeCounter()
    {
        ui.text= roundCount.ToString();
        ui.GetComponent<TextMeshProUGUI>().color = roundCount % 5 == 0 ? Color.green : Color.white;
    }
}
