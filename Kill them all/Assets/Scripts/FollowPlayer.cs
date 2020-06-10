using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    private Transform playerPosition;
    private Transform positionMonster;
    public float speed;
    private bool isFacingRight = true;

    void Start()
    {
        playerPosition = GameObject.FindWithTag("Player").transform;
        positionMonster = GetComponent<Transform>();
    }

    void Update()
    {
        positionMonster.position = Vector3.MoveTowards(positionMonster.position, playerPosition.position, speed * Time.deltaTime);
        if(playerPosition.position.x - positionMonster.position.x > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if(playerPosition.position.x - positionMonster.position.x < 0 && isFacingRight == true)
        {
            Flip();
        }
    }

    void Flip ()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x = -1 * Scalar.x;
        transform.localScale = Scalar;
    }
}
