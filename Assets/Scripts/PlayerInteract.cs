using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    public LayerMask interactableLayer;
    private void Update()
    {
        Vector2 origin = (Vector2)transform.position;
        Vector2 direction = transform.right;
        float directionLength = 2f;

        Debug.DrawRay(origin, direction * directionLength, Color.red);
        RaycastHit2D hit = Physics2D.Raycast(origin, direction, directionLength, interactableLayer);
        
        if(hit.collider != null)
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null && Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(transform);
            }
        }

    }
}
