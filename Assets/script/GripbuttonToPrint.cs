using System.Collections;
using UnityEngine;

public class GripbuttonToPrint : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public GameObject currentObjectInstance;
    public GameObject newObjectPrefab;
    public Transform replacementPosition;
    public float replacementDelay = 2f; // 替换延迟时间
    float counter = 2.0f;
    public string excludeChildName = "Quad1"; // 设置不想被渲染的子物体名字


    public void PlayPrintAnim()
    {
        anim.SetTrigger("3dprint");
        InstantiateAndPlayParticleSystem();
        StartCoroutine(DelayedRenderObject());
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

    public IEnumerator DelayedRenderObject()
    {
        // 等待指定的延迟时间
        yield return new WaitForSeconds(replacementDelay);

        // 渲染新的物体
        RenderObjects();
    }

    public void RenderObjects()
    {
        // 确保有一个新的物体实例化
        if (newObjectPrefab != null)
        {
            // 实例化新物体
            GameObject newObjectInstance = Instantiate(newObjectPrefab, replacementPosition.position, replacementPosition.rotation);

            // 启用新物体的渲染器，但排除特定子物体
            Renderer[] newRenderers = newObjectInstance.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in newRenderers)
            {
                if (renderer.gameObject.name != excludeChildName)
                {
                    renderer.enabled = true;
                }
            }

            // 设置新物体的父级为当前物体的父级
            newObjectInstance.transform.SetParent(replacementPosition.parent);

            // 销毁当前物体实例
            if (currentObjectInstance != null)
            {
                Destroy(currentObjectInstance.gameObject);
            }

            // 更新 currentObjectInstance 引用为新物体实例
            currentObjectInstance = newObjectInstance;
        }
    }
}
