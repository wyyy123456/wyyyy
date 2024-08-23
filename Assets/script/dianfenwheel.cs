using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dianfenwheel : MonoBehaviour

{
    public Animator anim1;
    public Animator anim2;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    
    float counter = 2.0f;


    public void PlayPrintAnim()
    {
        anim1.SetTrigger("zhuan");
      
        InstantiateAndPlayParticleSystem();
        anim2.SetTrigger("zhuan1");
        
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