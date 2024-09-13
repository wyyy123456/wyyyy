using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FlyLerp : MonoBehaviour
{
    public Transform direction;//移动的方向

    public float maxSpeed = 10;//最大速度

    public float lerpSpeed = 1;//加速度
    public Rigidbody thisRb;//刚体
    private Vector3 targetSpeed;//目标速度
    public InputActionReference flyAction;

    // Start is called before the first frame update
    void Start()
    {
        thisRb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        //按下空格键  目标速度就是“方向*最大速度”   不按空格键 目标速度为0
        if (flyAction.action.phase == InputActionPhase.Performed)
        {
            targetSpeed = direction.forward * maxSpeed;//目标速度
        }
        else
        {
            targetSpeed = Vector3.zero;//目标速度为0
        }

        thisRb.velocity = Vector3.Lerp(thisRb.velocity, targetSpeed, lerpSpeed * Time.fixedDeltaTime);//平滑过渡到目标速度
    }
}
