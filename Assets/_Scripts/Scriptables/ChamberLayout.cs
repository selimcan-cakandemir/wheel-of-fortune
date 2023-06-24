using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Objects/Chamber Layout")]
public class ChamberLayout : ScriptableObject
{
    [Serializable]
    private class ChamberProperties
    {
        public string text;
        public Sprite sprite;
        public Piece piece;
    }

    [SerializeField] private ChamberProperties[] chamberProperties;
    private UnityEngine.Object[] sprites;
    private List<Sprite> chosenSprites;

    public void LoadResources()
    {
        if(chosenSprites != null) { chosenSprites.Clear(); } 

        sprites = Resources.LoadAll("Images/Sprites", typeof(Sprite));

        for (int i = 0; i < sprites.Length; i++)
        {
            if (sprites[i].name.Contains("value"))
            {
                chosenSprites.Add((Sprite)sprites[i]);
            }
        }

    }

    public int GetNumberOfPieces()
    {
        return chamberProperties.Length;
    }

    public string GetChamberTextAtIndex(int index)
    {
        return chamberProperties[index].text;
    }

    public Sprite GetChamberSpriteAtIndex(int index)
    {
        return chamberProperties[index].sprite;
    }

    public void RandomizeProperties() {
        
        for (int i = 0; i < chamberProperties.Length; i++)
        {
            chamberProperties[i].text = UnityEngine.Random.Range(10, 100).ToString() + "x";
            chamberProperties[i].sprite = GrabRandomSprite();
        }
    }

    public Sprite GrabRandomSprite()
    {
        int random = RandomNumber();
        if (RoundCounter.Instance.roundCount == 5 && chosenSprites[random].name.Contains ("death")) { GrabRandomSprite(); }
        return (Sprite)chosenSprites[random];

    }

    public int RandomNumber()
    {
        int random = UnityEngine.Random.Range(0, chosenSprites.Count);
        return random;
    }
}
