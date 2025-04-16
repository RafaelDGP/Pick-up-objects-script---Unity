using System.Collections;
using UnityEngine;

public class PickUp : MonoBehaviour
{
    public float rayDistance; // Raycast distance. (2 or 3 is recomended)
    public LayerMask layerMask; // Layer that the raycast can hit.
    public Transform pos; // "Hand" position.
    public float velPick; // Velocity that the object goes to your hand. (High speed recomended)
    public float vel; // Velocity that the object follow your hand.

    private GameObject obj; // Actual object in hand.
    private Rigidbody rb; // RigidBody of the actual object in hand.
    private bool holding = false; // Checks if the player is holding an object.
    private bool moving = false; // Checks if the object is still going to your hand;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Input.GetKeyDown(KeyCode.E))
        {
            // Drop the object
            if (holding)
            {
                holding = false;
                moving = false;

                if (rb != null)
                {
                    rb.isKinematic = false;
                    rb = null;
                    obj = null;
                }
            }
            else if (!moving) // Pick up the object.
            {

                if (Physics.Raycast(ray, out hit, rayDistance, layerMask))
                {
                    obj = hit.collider.gameObject;
                    rb = obj.GetComponent<Rigidbody>();

                    if (rb != null)
                    {
                        StartCoroutine(MoveTo());
                    }
                }
            }
        }

        // If holding keeps the object following the hand position.
        if (holding && obj != null)
        {
            rb.MovePosition(Vector3.Lerp(obj.transform.position, pos.position, vel * Time.deltaTime));
        }

        // If obj is null, set everything to false.
        if(obj == null){
            holding = false;
            moving = false;
            if (rb != null)
                {
                    rb.isKinematic = false;
                    rb = null;
                    obj = null;
                }
        }
    }

    IEnumerator MoveTo()
    {
        moving = true;
        rb.isKinematic = true;

        // Loop to move the object into player's hand.
        while (Vector3.Distance(obj.transform.position, pos.position) > 0.05f)
        {
            // Checks if player drop the item while the loop is active.
            if (!moving)
                yield break;

            // Move obj to player's hand
            obj.transform.position = Vector3.Lerp(obj.transform.position, pos.position, velPick * Time.deltaTime);
            yield return null;
        }

        // Loop ended, player is holding the obj.
        holding = true;
        moving = false;
    }
}
