using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForGroundGroupController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _forGrounds;

    public void CreateForGround()
    {
        // Create forground for map
        for (int i = 0; i < _forGrounds.Length; i++)
        {
            if (Random.value <= 0.6f)
            {
                _forGrounds[i].gameObject.SetActive(true);
                _forGrounds[i].sprite = ConfigManager.ENVIRONMENTASSETS_DATA[GameController.Instance.environmentType].forGroundSprite;
            }
            else
            {
                _forGrounds[i].gameObject.SetActive(false);
            }
        }
    }

    public void MoveForGround()
    {
        SwapForGroundPos();
        gameObject.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * Constants.BlockMoveSpeed;
    }

    private void SwapForGroundPos()
    {
        if (gameObject.transform.position.x <= GameController.Instance.backGroundController.GetOffsetXCollectBackGround())
        {
            gameObject.transform.position = new Vector3(GameController.Instance.backGroundController.GetOffsetXReturnBackGround(), gameObject.transform.position.y, gameObject.transform.position.z);
            CreateForGround();
        }
    }
}
