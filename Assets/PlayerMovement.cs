using UnityEngine;
using System.Collections;
using UnityEngine.Tilemaps;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // �ړ����x (�g��Ȃ����A�K�v�ɉ����ĕێ�)
    public Vector2Int gridSize = new Vector2Int(3, 3); // �O���b�h�̃T�C�Y

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

            // ���L�[�̓��͂��擾
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
                // �^�[�Q�b�g�|�W�V������ݒ�
                targetPosition = rb.position + new Vector2(input.x * gridSize.x, input.y * gridSize.y);
                StartCoroutine(MoveToPosition(targetPosition));
            }
        }
    }

    private IEnumerator MoveToPosition(Vector2 target)
    {
        isMoving = true;

        // ��u�Ń^�[�Q�b�g�ʒu�Ɉړ�
        rb.MovePosition(target);

        // ���݂̃^�C���}�b�v�̃^�O������
        IdentifyAndReplaceTile(target);

        // �����ҋ@���Ă���ړ��t���O������
        yield return new WaitForSeconds(0.1f);

        isMoving = false;
    }

    void IdentifyAndReplaceTile(Vector2 position)
    {
        Vector3Int gridPosition = dirtTilemap.WorldToCell(position);

        // �eTilemap���`�F�b�N���ă^�C�������݂��邩�𔻒肵�A���̃^�O���o��
        if (stoneTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Stone Tilemap");
            stoneTilemap.SetTile(gridPosition, null);
        }
        else if (dirtTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Dirt Tilemap");
            dirtTilemap.SetTile(gridPosition, null); // �^�C����u������
        }
        else if (gemTilemap.HasTile(gridPosition))
        {
            Debug.Log("Player is on Gem Tilemap");
            GameManager.AddScore(100);
            gemTilemap.SetTile(gridPosition, null); // �^�C����u������
        }
        else
        {
            Debug.Log("Player is on an empty tile");
        }
    }
}
