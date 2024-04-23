using UnityEngine;

public class TriggerAnimation : MonoBehaviour
{
    // 动画控制器
    private Animator animator;

    private void Start()
    {
        // 获取动画控制器组件
        animator = GetComponent<Animator>();
    }

    // 当其他物体进入碰撞器范围时触发
    private void OnTriggerEnter(Collider other)
    {
        // 检查进入碰撞器的物体是否具有指定的标签（例如，触发器物体的标签为"Player"）
        if (other.CompareTag("Player"))
        {
            // 播放形变动画
            animator.Play("YourAnimationName");
        }
    }
}

