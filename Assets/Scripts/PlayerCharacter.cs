using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{

    PlayerCursor playerCursor;
    Rigidbody rb;

    [SerializeField] Transform directionIndicator;

    [SerializeField] [Range(0.01f, 1f)] float maxSpeed = 0.1f;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void setPlayerCursor(PlayerCursor playerCursor)
    {
        this.playerCursor = playerCursor;
    }

    void FixedUpdate()
    {
        Vector3 moveDir = playerCursor.transform.position - rb.transform.position;
        Vector3 indicatorDir = moveDir;
        if (moveDir.magnitude > maxSpeed)
        {
            moveDir = moveDir.normalized * maxSpeed;
        }
        rb.MovePosition(rb.transform.position + moveDir);

        Quaternion lookRotation = Quaternion.LookRotation(playerCursor.transform.position - rb.transform.position);
        directionIndicator.rotation = lookRotation;

        directionIndicator.localScale = new Vector3(1f, 1f, indicatorDir.magnitude);
    }
}
