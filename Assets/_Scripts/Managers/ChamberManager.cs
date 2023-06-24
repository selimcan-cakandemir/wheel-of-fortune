using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Unity.VisualScripting;
using System.Collections;

public class ChamberManager : MonoBehaviour
{
    [SerializeField] private ChamberLayout startingChamberLayout;

    [SerializeField] private Transform parent;
    [SerializeField] private Transform circle;
    [SerializeField] private Transform inventory;
    [SerializeField] private GameObject deathPanel;
    [SerializeField] private GameObject continuePanel;


    [SerializeField] private Piece[] pieces;
    [SerializeField] private List<Piece> pieceInventory;

    private bool isSpinning = true;

    private void Start()
    {
        Generate();
        startingChamberLayout.LoadResources();
    }

    public void Generate()
    {
        for (int i = 0; i < startingChamberLayout.GetNumberOfPieces(); i++)
            DrawChamber(i);
    }

    public void DrawChamber(int i)
    {
        Transform trf = parent.GetChild(i);
        trf.GetComponent<Image>().sprite = startingChamberLayout.GetChamberSpriteAtIndex(i);
        trf.GetChild(0).GetComponent<TextMeshProUGUI>().text = startingChamberLayout.GetChamberTextAtIndex(i);

        if(trf.GetComponent<Piece>() == null) { pieces[i] = trf.AddComponent<Piece>(); }
        Piece piece = trf.GetComponent<Piece>();

        piece.PieceConstructor(startingChamberLayout.GetChamberTextAtIndex(i), startingChamberLayout.GetChamberSpriteAtIndex(i));
    }

    public void Spin()
    {
        if (isSpinning)
        { isSpinning = false; }
        else return;

        float randomAngle = Random.Range(100f, 360*5f);
        circle.DORotate(new Vector3(0,0, randomAngle),2, RotateMode.FastBeyond360).OnComplete(() => {
            RoundEnd();
        });
    }

    public void RoundEnd()
    {
        isSpinning = true; 
        Piece piece = PickWinner();
        if (piece.sprite.name.Contains("death")) { deathPanel.SetActive(true); };
        AddPieceToInventory(piece);
        AddPieceToCanvas(piece);
        if (RoundCounter.Instance.roundCount > 1) { RandomizePieces(); Generate(); }
        RoundCounter.Instance.roundCount++;
        Actions.ChangeCounter();
        continuePanel.SetActive(true);
        continuePanel.transform.GetChild(0).transform.GetComponent<Image>().sprite = piece.sprite;
    }

    private void RandomizePieces()
    {
        startingChamberLayout.RandomizeProperties();
    }

    public Piece PickWinner()
    {
        foreach (var piece in pieces)
        {
            bool isCollide = piece.isColliding();
            if (isCollide) {  return piece; } ;
        }
        return null;
    }

    public void AddPieceToInventory(Piece piece)
    {
        pieceInventory.Add(piece);
    }

    public void AddPieceToCanvas(Piece piece)
    {
        GameObject gO = new GameObject("Image Object");
        gO.transform.SetParent(inventory);
        gO.transform.localScale = Vector3.one;

        Image goImage = gO.AddComponent<Image>();
        goImage.sprite = piece.sprite;
        goImage.preserveAspect = true;
        goImage.raycastTarget= false;
        goImage.maskable= false;
    }

    private void OnEnable()
    {
        Actions.OnSpin += Spin;
    }

    private void OnDisable()
    {
        Actions.OnSpin -= Spin;
    }
}
