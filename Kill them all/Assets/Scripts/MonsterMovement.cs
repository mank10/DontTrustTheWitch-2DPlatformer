using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    private Vector3 maxMonsterPosition,minMonsterPosition;
    public float speed;

    public int distance = 4;

    public bool isFacingRight;

    void Start()
    {
        maxMonsterPosition =  new Vector3(transform.position.x + distance,transform.position.y,transform.position.z);
        minMonsterPosition =  new Vector3(transform.position.x - distance,transform.position.y,transform.position.z); 
    }

    void Update()
    {
        transform.position = Vector3.Lerp(minMonsterPosition,maxMonsterPosition, Mathf.PingPong(Time.time * speed, 1.0f));
       
        if (transform.position.x  >= maxMonsterPosition.x - 0.2 && isFacingRight == true)
        {
            Flip();   
        }
        else if(transform.position.x <= minMonsterPosition.x + 0.2 && isFacingRight == false)
        {
            Flip();
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 Scalar = transform.localScale;
        Scalar.x *= -1;
        transform.localScale = Scalar;
    }
}
