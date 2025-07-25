using UnityEngine;

public class UseItem : MonoBehaviour, IUsableItem
{
    public LayerMask playerLayer;

    public void Use(GameObject user)
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((playerLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            Use(other.gameObject);
        }
    }
}
