using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class InfiniteMapGenerator : MonoBehaviour
{
    public float startScript_X = 20;

    public Tilemap tilemap;
    public TileBase dirtTile;
    public TileBase stoneTile;
    public TileBase gemTile;
    public Transform player;

    public int dirtTileProbability = 70;
    public int stoneTileProbability = 20;
    public int gemTileProbability = 10;

    private int chunkSize = 10;
    private int mapHeight = 3;
    private HashSet<Vector2Int> generatedChunks = new HashSet<Vector2Int>();

    void Update()
    {
        if (player.position.x >= startScript_X)
        {
            Vector2Int playerChunk = new Vector2Int(
                Mathf.FloorToInt(player.position.x / chunkSize),
                Mathf.FloorToInt(player.position.y / chunkSize)
            );

            if (!generatedChunks.Contains(playerChunk))
            {
                GenerateChunk(playerChunk);
                generatedChunks.Add(playerChunk);
            }
        }
    }

    void GenerateChunk(Vector2Int chunkPosition)
    {
        for (int x = 0; x < chunkSize; x++)
        {
            int stoneCount = 0; // �e��̐΂̐���ǐ�
            int consecutiveStoneCount = 0; // �A������΂̐���ǐ�

            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(
                    chunkPosition.x * chunkSize + x,
                    y,
                    0
                );

                // �����̃^�C���}�b�v��ύX���Ȃ��悤�ɂ���
                if (tilemap.HasTile(tilePosition))
                {
                    continue;
                }

                TileBase tile = GetRandomTile(ref stoneCount, ref consecutiveStoneCount, tilePosition);
                tilemap.SetTile(tilePosition, tile);
            }
        }
    }

    TileBase GetRandomTile(ref int stoneCount, ref int consecutiveStoneCount, Vector3Int tilePosition)
    {
        int totalProbability = dirtTileProbability + stoneTileProbability + gemTileProbability;
        int randomValue = Random.Range(0, totalProbability);

        // �΂߂�3�A���Ŋ₪���������̂�h���`�F�b�N
        if (IsDiagonalStonePattern(tilePosition))
        {
            randomValue = Random.Range(0, dirtTileProbability); // �΂��I�΂�Ȃ��悤�ɂ���
        }

        // �΂̃^�C���������2�𒴂��Ȃ��悤�ɂ��A�A���Ŕz�u����Ȃ��悤�ɂ���
        if (randomValue < dirtTileProbability)
        {
            consecutiveStoneCount = 0; // ���Z�b�g�A���΃J�E���g
            return dirtTile;
        }
        else if (randomValue < dirtTileProbability + stoneTileProbability && stoneCount < 2 && consecutiveStoneCount < 1)
        {
            stoneCount++;
            consecutiveStoneCount++;
            return stoneTile;
        }
        else
        {
            consecutiveStoneCount = 0; // ���Z�b�g�A���΃J�E���g
            return gemTile;
        }
    }

    bool IsDiagonalStonePattern(Vector3Int tilePosition)
    {
        // �΂߂ɗאڂ���ʒu���m�F
        Vector3Int[] diagonalPositions = new Vector3Int[]
        {
            new Vector3Int(tilePosition.x - 1, tilePosition.y + 1, 0),
            new Vector3Int(tilePosition.x + 1, tilePosition.y - 1, 0),
            new Vector3Int(tilePosition.x - 1, tilePosition.y - 1, 0),
            new Vector3Int(tilePosition.x + 1, tilePosition.y + 1, 0)
        };

        // �΂߂Ɋ�̃p�^�[�������݂��邩���`�F�b�N
        int stoneCount = 0;
        foreach (var pos in diagonalPositions)
        {
            if (tilemap.GetTile(pos) == stoneTile)
            {
                stoneCount++;
                if (stoneCount >= 2)
                {
                    return true;
                }
            }
        }

        return false;
    }
}
