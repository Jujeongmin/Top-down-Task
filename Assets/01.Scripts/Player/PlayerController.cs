using UnityEngine;

public class PlayerController : BaseController
{
    protected override void HandleAction()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        _movementDir = new Vector2(horizontal, vertical).normalized;

        Vector2 mousePosition = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        _lookDir = (worldPosition - (Vector2)transform.position);

        if (_lookDir.magnitude < 0.9f)
        {
            _lookDir = Vector2.zero;
        }
        else
        {
            _lookDir = _lookDir.normalized;
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            var existing = UIManager.Instance.Get<MonsterCodexUI>();

            if (existing != null && existing.gameObject.activeSelf)
            {
                UIManager.Instance.Hide<MonsterCodexUI>();
                Time.timeScale = 1f;
            }
            else
            {
                UIManager.Instance.Show<MonsterCodexUI>();
                Time.timeScale = 0f;
            }
        }
    }
}
