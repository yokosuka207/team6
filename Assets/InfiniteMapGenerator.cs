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
            int stoneCount = 0; // 各列の石の数を追跡
            int consecutiveStoneCount = 0; // 連続する石の数を追跡

            for (int y = 0; y < mapHeight; y++)
            {
                Vector3Int tilePosition = new Vector3Int(
                    chunkPosition.x * chunkSize + x,
                    y,
                    0
                );

                // 既存のタイルマップを変更しないようにする
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

        // 斜めに3つ連続で岩が生成されるのを防ぐチェック
        if (IsDiagonalStonePattern(tilePosition))
        {
            randomValue = Random.Range(0, dirtTileProbability); // 石が選ばれないようにする
        }

        // 石のタイルが列内で2個を超えないようにし、連続で配置されないようにする
        if (randomValue < dirtTileProbability)
        {
            consecutiveStoneCount = 0; // リセット連続石カウント
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
            consecutiveStoneCount = 0; // リセット連続石カウント
            return gemTile;
        }
    }

    bool IsDiagonalStonePattern(Vector3Int tilePosition)
    {
        // 斜めに隣接する位置を確認
        Vector3Int[] diagonalPositions = new Vector3Int[]
        {
            new Vector3Int(tilePosition.x - 1, tilePosition.y + 1, 0),
            new Vector3Int(tilePosition.x + 1, tilePosition.y - 1, 0),
            new Vector3Int(tilePosition.x - 1, tilePosition.y - 1, 0),
            new Vector3Int(tilePosition.x + 1, tilePosition.y + 1, 0)
        };

        // 斜めに岩のパターンが存在するかをチェック
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
