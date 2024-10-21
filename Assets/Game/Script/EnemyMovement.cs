using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f; 
    public float moveDistance = 2.5f;
    public bool moveV = true;
    private Vector3 pointA;
    private Vector3 pointB; 
    private Vector3 target; // Mục tiêu hiện tại

    private void Start()
    {
		if (moveV == true)
		{
            moveVertical();

		}
		else
		{
            moveHorizontal();

        }
       
    }

    private void Update()
    {
        // Di chuyển enemy về phía mục tiêu
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        // Đổi hướng di chuyển khi đạt đến điểm giới hạn
        if (Vector3.Distance(transform.position, target) < 0.1f)
        { // Gan ta
            target = (target == pointA) ? pointB : pointA;
        }
    }
    public void moveVertical()
	{
        pointA = transform.position;
        pointB = new Vector3(pointA.x, pointA.y + moveDistance, pointA.z);
        target = pointB;
    }
    public void moveHorizontal()
    {
        pointA = transform.position;
        pointB = new Vector3(pointA.x + moveDistance, pointA.y , pointA.z);
        target = pointB;
    }
}
