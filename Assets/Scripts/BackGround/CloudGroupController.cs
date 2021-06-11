using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudGroupController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] _cloud;

    public void CreateCloud()
    {
        for (int i = 0; i < _cloud.Length; i++)
        {
            if (Random.value <= 0.6f)
            {
                _cloud[i].gameObject.SetActive(true);
                _cloud[i].sprite = ConfigManager.ENVIRONMENTASSETS_DATA[GameController.Instance.environmentType].cloudSprite;
            }
            else
            {
                _cloud[i].gameObject.SetActive(false);
            }
        }
    }

    public void MoveCloud()
    {
        SwapCloud();
        gameObject.transform.position += new Vector3(-1, 0, 0) * Constants.BlockMoveSpeed * Time.deltaTime * 0.7f;
    }

    private void SwapCloud()
    {
        if (gameObject.transform.position.x <= GameController.Instance.backGroundController.GetOffsetXCollectBackGround())
            gameObject.transform.position = new Vector3(GameController.Instance.backGroundController.GetOffsetXReturnBackGround(), gameObject.transform.position.y, gameObject.transform.position.z);
    }
}
