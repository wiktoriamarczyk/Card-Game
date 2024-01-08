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
    Vector3 offset = new Vector3(0.14f, 0.15f, 0.16f);
    private void Awake()
    {
        InitializeVariables();
        InitializeBoard();
        Game.instance.RebuildNavMesh();
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

        int x_multiplier2 = Random.Range(0, boardWidth - 1);
        int y_multiplier2 = Random.Range(0, boardHeight - 1);
        int x_multiplier3 = Random.Range(0, boardWidth - 1);
        int y_multiplier3 = Random.Range(0, boardHeight - 1);


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
                    Quaternion.identity,
                    cardsContener.transform);

                cardObject.transform.localPosition = cardPosition;
                cardObject.name = "CardField" + x + y;
                CardField cardFieldComponent = cardObject.GetComponent<CardField>();

                if (x == x_multiplier2 && y == y_multiplier2)
                    cardFieldComponent.SetMultiplier2();
                else if (x == x_multiplier3 && y == y_multiplier3)
                    cardFieldComponent.SetMultiplier3();

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
            boardMeshStartingScale.x * boardWidth,
            boardMeshStartingScale.y,
            boardMeshStartingScale.z * boardHeight);

        boardMesh.transform.localScale = boardMeshStartingScale;
    }

}
