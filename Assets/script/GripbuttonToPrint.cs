using System.Collections;
using UnityEngine;

public class GripbuttonToPrint : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public GameObject currentObjectInstance;
    public GameObject newObjectPrefab;
    public GameObject secondObjectPrefab; // 新增第二个物体的预制体
    public Transform replacementPosition;
    public Transform secondReplacementPosition; // 第二个物体的替换位置
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
        // 渲染第一个物体
        if (currentObjectInstance != null && newObjectPrefab != null && replacementPosition != null)
        {
            // 禁用当前物体的渲染器
            Renderer[] renderers = currentObjectInstance.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = false;
            }

            // 启用新物体的渲染器，但排除特定子物体
            Renderer[] newRenderers = newObjectPrefab.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in newRenderers)
            {
                if (renderer.gameObject.name != excludeChildName)
                {
                    renderer.enabled = true;
                }
            }

            // 将新物体放置到替换位置
            newObjectPrefab.transform.position = replacementPosition.position;
            newObjectPrefab.transform.rotation = replacementPosition.rotation;

            // 设置新物体的父级为当前物体的父级
            newObjectPrefab.transform.SetParent(replacementPosition.parent);

            // 销毁当前物体
            Destroy(currentObjectInstance);

            // 更新 currentObjectInstance 引用为新物体实例
            currentObjectInstance = newObjectPrefab;
        }

        // 渲染第二个物体
        if (secondObjectPrefab != null && secondReplacementPosition != null)
        {
            // 实例化第二个物体并禁用渲染器
            GameObject secondObjectInstance = Instantiate(secondObjectPrefab, secondReplacementPosition.position, secondReplacementPosition.rotation);
            Renderer[] secondRenderers = secondObjectInstance.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in secondRenderers)
            {
                if (renderer.gameObject.name != excludeChildName)
                {
                    renderer.enabled = false;
                }
            }

            // 将第二个物体放置到第二替换位置
            secondObjectInstance.transform.position = secondReplacementPosition.position;
            secondObjectInstance.transform.rotation = secondReplacementPosition.rotation;

            // 设置第二个物体的父级为当前物体的父级
            secondObjectInstance.transform.SetParent(secondReplacementPosition.parent);
        }
    }
}
