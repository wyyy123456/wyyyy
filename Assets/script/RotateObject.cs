using UnityEngine;

public class RotateObject : MonoBehaviour
{
    public float rotationSpeedX = 10f;
    public float rotationSpeedY = 20f;
    public float rotationSpeedZ = 30f;

    void Update()
    {
        float rotationX = rotationSpeedX * Time.deltaTime;
        float rotationY = rotationSpeedY * Time.deltaTime;
        float rotationZ = rotationSpeedZ * Time.deltaTime;

        transform.Rotate(new Vector3(rotationX, rotationY, rotationZ));
    }
}
