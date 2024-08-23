using System.Collections;
using UnityEngine;

public class GripbuttonToPrint : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public GameObject currentObjectInstance; // 当前动态生成的物体
    public GameObject newObjectInScene; // 场景中已存在但渲染器关闭的物体
    public Transform replacementPosition;
    public float replacementDelay = 2f; // 替换延迟时间
    float counter = 2.0f;



    public void PlayPrintAnim()
    {
        // 播放动画
        anim.SetTrigger("3dprint");

        // 销毁当前物体实例
        if (currentObjectInstance != null)
        {
            // 确保只销毁动态生成的预制体实例
            if (currentObjectInstance.scene.name == null)
            {
                Destroy(currentObjectInstance.gameObject);
            }
            else
            {
                Debug.LogWarning("当前物体不是动态生成的预制体，无法销毁。");
            }
        }

        // 播放粒子效果
        InstantiateAndPlayParticleSystem();

        // 延迟渲染新物体
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
        ActivateObjectInScene();
    }

    public void ActivateObjectInScene()
    {
        // 确保场景中存在新物体
        if (newObjectInScene != null)
        {
            // 设置新物体的位置和旋转（如果需要的话）
            newObjectInScene.transform.position = replacementPosition.position;
            newObjectInScene.transform.rotation = replacementPosition.rotation;

            // 启用渲染器
            Renderer[] renderers = newObjectInScene.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            // 设置新物体的父级为当前物体的父级
            newObjectInScene.transform.SetParent(replacementPosition.parent);

            // 更新 currentObjectInstance 引用为新物体
            currentObjectInstance = newObjectInScene;
        }
    }
}
