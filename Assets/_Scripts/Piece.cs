using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public string text { get; set; }
    public Sprite sprite { get; set; }

    private bool isCollide = false;

    public void PieceConstructor(string text, Sprite sprite)
    {
        this.text = text;
        this.sprite = sprite;
    }

    public bool isColliding()
    {
        //if (isCollide == true) { Debug.Log(isCollide + this.text); }
        return isCollide;
    }

  
    private bool OnTriggerEnter2D(Collider2D collision)
    {
        return isCollide = true;
    }

    private bool OnTriggerExit2D(Collider2D collision)
    {
        return isCollide = false;
    }

}
