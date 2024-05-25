using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;

public class InfiniteMapGenerator : MonoBehaviour
{
    public Tilemap dirtTilemap; // 土タイル用のTilemap
    public Tilemap stoneTilemap; // 石タイル用のTilemap
    public Tilemap gemTilemap; // 宝石タイル用のTilemap

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
        int passageCount = 2; // 通路の数
        HashSet<int> passageXs = new HashSet<int>();

        // 通路の位置をランダムに決定
        while (passageXs.Count < passageCount)
        {
            passageXs.Add(Random.Range(0, chunkSize));
        }

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
                if (dirtTilemap.HasTile(tilePosition) || stoneTilemap.HasTile(tilePosition) || gemTilemap.HasTile(tilePosition))
                {
                    continue;
                }

                TileBase tile;

                // 初期チャンクかつx=0の列の場合、すべて土タイルにする
                if (isInitial && chunkPosition.x == 0)
                {
                    tile = dirtTile;
                }
                else
                {
                    // 通路の位置の場合は必ず土タイルを配置
                    tile = passageXs.Contains(x) ? dirtTile : GetRandomTile(ref stoneCount, ref consecutiveStoneCount, tilePosition, passageXs.Contains(x));
                }

                // 適切なTilemapにタイルを配置
                SetTile(tilePosition, tile);

                // 岩ブロックの両隣を土または宝石に設定
                if (tile == stoneTile)
                {
                    SetAdjacentTilesToNonStone(tilePosition);
                }
            }
        }
    }

    TileBase GetRandomTile(ref int stoneCount, ref int consecutiveStoneCount, Vector3Int tilePosition, bool isPassage)
    {
        if (isPassage) // 通路の場合は土タイルを返す
        {
            return dirtTile;
        }

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

    void SetTile(Vector3Int position, TileBase tile)
    {
        if (tile == dirtTile)
        {
            dirtTilemap.SetTile(position, tile);
        }
        else if (tile == stoneTile)
        {
            stoneTilemap.SetTile(position, tile);
        }
        else if (tile == gemTile)
        {
            gemTilemap.SetTile(position, tile);
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
            if (!dirtTilemap.HasTile(pos) && !stoneTilemap.HasTile(pos) && !gemTilemap.HasTile(pos))
            {
                // 土または宝石をランダムに配置
                TileBase tile = Random.Range(0, 2) == 0 ? dirtTile : gemTile;
                SetTile(pos, tile);
            }
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
            if (stoneTilemap.GetTile(pos) == stoneTile)
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
