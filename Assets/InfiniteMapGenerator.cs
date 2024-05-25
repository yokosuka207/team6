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

    void Start()
    {
        GenerateInitialChunks();
    }

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

    void GenerateInitialChunks()
    {
        for (int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                Vector2Int chunkPosition = new Vector2Int(x, y);
                GenerateChunk(chunkPosition, true);
                generatedChunks.Add(chunkPosition);
            }
        }
    }

    void GenerateChunk(Vector2Int chunkPosition, bool isInitial = false)
    {
        int passageCount = 2; // �ʘH�̐�
        HashSet<int> passageXs = new HashSet<int>();

        // �ʘH�̈ʒu�������_���Ɍ���
        while (passageXs.Count < passageCount)
        {
            passageXs.Add(Random.Range(0, chunkSize));
        }

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

                // �ʘH�̈ʒu�̏ꍇ�͕K���y�^�C����z�u
                TileBase tile = passageXs.Contains(x) ? dirtTile : GetRandomTile(ref stoneCount, ref consecutiveStoneCount, tilePosition, passageXs.Contains(x));
                tilemap.SetTile(tilePosition, tile);

                // ��u���b�N�̗��ׂ�y�܂��͕�΂ɐݒ�
                if (tile == stoneTile)
                {
                    SetAdjacentTilesToNonStone(tilePosition);
                }
            }
        }
    }

    TileBase GetRandomTile(ref int stoneCount, ref int consecutiveStoneCount, Vector3Int tilePosition, bool isPassage)
    {
        if (isPassage) // �ʘH�̏ꍇ�͓y�^�C����Ԃ�
        {
            return dirtTile;
        }

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

    void SetAdjacentTilesToNonStone(Vector3Int tilePosition)
    {
        Vector3Int[] adjacentPositions = new Vector3Int[]
        {
            new Vector3Int(tilePosition.x - 1, tilePosition.y, 0),
            new Vector3Int(tilePosition.x + 1, tilePosition.y, 0)
        };

        foreach (var pos in adjacentPositions)
        {
            if (!tilemap.HasTile(pos) || tilemap.GetTile(pos) == stoneTile)
            {
                // �y�܂��͕�΂������_���ɔz�u
                tilemap.SetTile(pos, Random.Range(0, 2) == 0 ? dirtTile : gemTile);
            }
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
