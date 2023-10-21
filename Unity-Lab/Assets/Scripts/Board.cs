using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class responsible for managing the board in the game.
/// </summary>
public class Board : MonoBehaviour
{
    [SerializeField] GameObject cardsContener;
    [SerializeField] GameObject cardFieldPrefab;
    [SerializeField] GameObject boardMesh;
    [SerializeField] int boardWidth;
    [SerializeField] int boardHeight;
    
    CardField[,] cardFields;
    BoxCollider boardCollider;
    BoxCollider cardCollider;
    Vector3 boardMeshStartingScale;
    
    Vector3 offset = new Vector3(0.1f, 0.2f, 0.1f);

    private void Awake()
    {
        InitializeVariables();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Vector3 cardSize = cardCollider.transform.TransformVector(cardCollider.size);
        Vector3 boardSize = boardCollider.transform.TransformVector(boardCollider.size);

        for (int y = 0; y < boardHeight; ++y)
        {
            for (int x = 0; x < boardWidth; ++x)
            {
                GameObject cardObject = Instantiate(
                    cardFieldPrefab,
                    new Vector3(x * cardSize.x + offset.x * x - boardSize.x / 4, offset.y, -(y * cardSize.z + offset.z * y - boardSize.z / 4)),
                    Quaternion.identity);

                cardObject.transform.parent = cardsContener.transform;
                cardObject.name = "CardField" + x + y;
                CardField cardFieldComponent = cardObject.GetComponent<CardField>();
                cardFields[x, y] = cardFieldComponent;

            }
        }
    }

    private void InitializeVariables()
    {
        boardCollider = GetComponentInChildren<BoxCollider>();
        cardCollider = cardFieldPrefab.GetComponentInChildren<BoxCollider>();

        cardFields = new CardField[boardWidth, boardHeight];
        boardMeshStartingScale = boardMesh.transform.localScale;

        boardMeshStartingScale = new Vector3(
            boardMeshStartingScale.x * boardWidth + offset.x * boardWidth,
            boardMeshStartingScale.y,
            boardMeshStartingScale.z * boardHeight + offset.z * boardHeight);

        boardMesh.transform.localScale = boardMeshStartingScale;
    }

}
