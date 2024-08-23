using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottleprotein : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
   
    float counter = 2.0f;





    public void PlayPrintAnim()
    {
        anim.SetTrigger("large");
        InstantiateAndPlayParticleSystem();
       
         //play vfx
        //Instantiate(xxx);
        //play sfx
        //xxx.PlayOneShot();
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
