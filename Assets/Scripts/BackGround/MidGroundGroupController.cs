using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MidGroundGroupController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _midGrounds;

    public void CreateMidGround()
    {
        for (int i = 0; i < _midGrounds.Length; i++)
        {
            if (Random.value <= 0.7f)
            {
                _midGrounds[i].gameObject.SetActive(true);
                _midGrounds[i].sprite = ConfigManager.ENVIRONMENTASSETS_DATA[GameController.Instance.environmentType].midGroundSpirte;
            }
            else
            {
                _midGrounds[i].gameObject.SetActive(false);
            }
        }
    }

    public void MoveMidGround()
    {
        SwapMidGround();
        gameObject.transform.position += new Vector3(-1, 0, 0) * Time.deltaTime * Constants.BlockMoveSpeed;
    }

    private void SwapMidGround()
    {
        if (gameObject.transform.position.x <= GameController.Instance.backGroundController.GetOffsetXCollectBackGround())
        {
            gameObject.transform.position = new Vector3(GameController.Instance.backGroundController.GetOffsetXReturnBackGround(), gameObject.transform.position.y, gameObject.transform.position.z);
            CreateMidGround();
        }
    }
}
