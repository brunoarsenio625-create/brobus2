using UnityEngine;

public class CameraMouseLook : MonoBehaviour
{
    public float sensitivity = 200f;

    float rotationY = 0f;

    void Update()
    {
        if (Input.GetMouseButton(1)) // botão direito segurado
        {
            float mouseX = Input.GetAxis("Mouse X");

            rotationY += mouseX * sensitivity * Time.deltaTime;

            transform.localRotation = Quaternion.Euler(0f, rotationY, 0f);
        }
    }
}
