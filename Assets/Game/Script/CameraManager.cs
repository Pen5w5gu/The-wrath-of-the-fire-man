using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;        // Đối tượng nhân vật mà camera sẽ theo dõi
       public float smoothing = 5f;    // Độ mượt khi camera di chuyển
       public float offsetY = 0f;      // Khoảng cách theo trục Y giữa camera và nhân vật (nếu có)
   
       void FixedUpdate()
       {
           // Lấy vị trí hiện tại của camera
           Vector3 cameraPos = transform.position;
   
           // Tính vị trí mục tiêu mới của camera chỉ theo trục X (vị trí Y và Z giữ nguyên)
           Vector3 targetCamPos = new Vector3(target.position.x, cameraPos.y + offsetY, cameraPos.z);
   
           // Di chuyển camera từ vị trí hiện tại tới vị trí mục tiêu một cách mượt mà
           transform.position = Vector3.Lerp(cameraPos, targetCamPos, smoothing * Time.deltaTime);
       }
}
