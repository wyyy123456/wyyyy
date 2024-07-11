using System.Collections;
using UnityEngine;

public class smzmove : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public Animator secondObjectAnimator;
    float counter = 2.0f;



    public void PlayPrintAnim()
    {
        // 播放动画
        anim.SetTrigger("airship");
        anim.SetTrigger("move11");

        // 实例化并播放粒子系统
        InstantiateAndPlayParticleSystem();

        // 延迟销毁当前物体
        // Destroy(gameObject, destroyDelay);
    }

    public void PlaySecondObjectAnim()
    {
        if (secondObjectAnimator != null)
        {
            secondObjectAnimator.SetTrigger("move11");
        }
    }

    public void InstantiateAndPlayParticleSystem()
    {
        if (particlePrefab != null && spawnLocation != null)
        {
            // 实例化粒子系统
            GameObject particleInstance = Instantiate(particlePrefab, spawnLocation.position, spawnLocation.rotation);

            // 获取并播放粒子系统
            ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }
        }
    }
}
