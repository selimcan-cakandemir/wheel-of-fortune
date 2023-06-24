using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class DeathButton : Button
{
    public override void OnPointerDown(PointerEventData eventData)
    {
        Actions.Death();
    }

    public override void OnPointerUp(PointerEventData eventData)
    {

    }

    private void OnEnable()
    {
        Actions.Death += Death;
    }

    private void OnDisable()
    {
        Actions.Death -= Death;
    }

    public void Death()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
