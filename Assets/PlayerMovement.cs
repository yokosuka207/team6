using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // 移動速度 (使わないが、必要に応じて保持)
    public Vector2Int gridSize = new Vector2Int(3, 3); // グリッドのサイズ

    private Rigidbody2D rb;
    private Vector2 targetPosition;
    private bool isMoving = false;

    public Tilemap dirtTilemap;
    public Tilemap stoneTilemap;
    public Tilemap gemTilemap;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetPosition = rb.position;
    }

    void Update()
    {
        if (!isMoving)
        {
            Vector2 input = Vector2.zero;

            // 矢印キーの入力を取得
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (transform.position.y <= 9)
                    input = Vector2.up;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                input = Vector2.right;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                if (transform.position.y >= 3.1)
                    input = Vector2.down;
            }

            if (input != Vector2.zero)
            {
                // ターゲットポジションを設定
                targetPosition = rb.position + new Vector2(input.x * gridSize.x, input.y * gridSize.y);
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        isMoving = true;

        // 一瞬でターゲット位置に移動
        rb.MovePosition(target);

        // 現在のタイルマップのタグを識別
        IdentifyAndReplaceTile(target);

        // 少し待機してから移動フラグを解除
        yield return new WaitForSeconds(0.1f);

        isMoving = false;
    }

    void IdentifyAndReplaceTile(Vector2 position)
    {
        Vector3Int gridPosition = dirtTilemap.WorldToCell(position);

        // 各Tilemapをチェックしてタイルが存在するかを判定し、そのタグを出力
        if (stoneTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Stone Tilemap");
            stoneTilemap.SetTile(gridPosition, null);
        }
        else if (dirtTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Dirt Tilemap");
            dirtTilemap.SetTile(gridPosition, null); // タイルを置き換え
        }
        else if (gemTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Gem Tilemap");
            GameManager.AddScore(100);
            gemTilemap.SetTile(gridPosition, null); // タイルを置き換え
        }
        else
        {
            Debug.Log("Player is on an empty tile");
        }
    }
}
