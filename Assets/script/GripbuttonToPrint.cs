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

        GameObject objectToDestroy = GameObject.Find("meat jitui(1)");

        // 销毁当前物体实例
        if (objectToDestroy != null)
        {
            // 确保只销毁动态生成的预制体实例
            //if (currentObjectInstance.scene.name == null)
            //{
                
                Destroy(objectToDestroy);
            //}
            //else
            //{
            //    Debug.LogWarning("当前物体不是动态生成的预制体，无法销毁。");
            //}
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

        ActivateObjectInScene();
    }

    public void ActivateObjectInScene()
    {
        if (newObjectInScene != null)
        {
            newObjectInScene.transform.position = replacementPosition.position;
            newObjectInScene.transform.rotation = replacementPosition.rotation;

            Renderer[] renderers = newObjectInScene.GetComponentsInChildren<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.enabled = true;
            }

            newObjectInScene.transform.SetParent(replacementPosition.parent);

            currentObjectInstance = newObjectInScene;
        }
    }
}
