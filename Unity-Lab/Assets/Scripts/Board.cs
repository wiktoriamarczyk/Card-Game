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
    Vector3 offset = new Vector3(0.0f, 0.15f, 0.0f);
    private void Awake()
    {
        InitializeVariables();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        Vector3 cardSize = cardCollider.transform.TransformVector(cardCollider.size);
        Vector3 boardSize = boardCollider.transform.TransformVector(boardCollider.size);

        float cardWithOffsetX = cardSize.x + offset.x;
        float cardWithOffsetZ = cardSize.z + offset.z;

        //calculate start position of the first card, based on number of offsets between them
        float startX = -(boardWidth - 1) * (cardWithOffsetX / 2);
        float startZ = -(boardHeight - 1) * (cardWithOffsetZ / 2);

        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                float xPos = startX + x * cardWithOffsetX;
                float zPos = startZ + y * cardWithOffsetZ;
                Vector3 cardPosition = new Vector3(xPos, offset.y, zPos);

                GameObject cardObject = Instantiate(
                    cardFieldPrefab,
                    cardPosition,
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
