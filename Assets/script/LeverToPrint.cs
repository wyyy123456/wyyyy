using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeverToPrint : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public GameObject currentObjectInstance;
    public GameObject newObjectPrefab; 
    public Transform replacementPosition;
    public float replacementDelay = 2f; // 替换延迟时间
    float counter = 2.0f;




    public void PlayPrintAnim()
    {
        anim.SetTrigger("Print");
        InstantiateAndPlayParticleSystem();
        StartCoroutine(DelayedReplaceObject());
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
    private IEnumerator DelayedReplaceObject()
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(replacementDelay);

        // 替换物体
        ReplaceObject();
    }
   public void ReplaceObject()
    {
        if (currentObjectInstance != null && newObjectPrefab != null && replacementPosition != null)
        {
            // 销毁当前物体实例
            Renderer[] renderers = currentObjectInstance.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            // 实例化新的物体
            GameObject newObject = Instantiate(newObjectPrefab, replacementPosition.position, replacementPosition.rotation);

            // 可选：设置新物体的父级为当前物体的父级，以保持层次结构
            newObject.transform.SetParent(replacementPosition.parent);

            // 更新 currentObjectInstance 引用为新的物体实例
            currentObjectInstance = newObject;
        }
    }
}
