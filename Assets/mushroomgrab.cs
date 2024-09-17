using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class mushroomgrab : MonoBehaviour
{
    public InputActionProperty leftHandTrigger;
    public InputActionProperty rightHandTrigger;

    public GameObject particlePrefab; // 粒子系统的预制体
    public Transform particleSpawnPoint; // 粒子效果的生成位置
    public AudioClip grabSound; // 抓取蘑菇的音效
    private AudioSource audioSource; // 播放音效的AudioSource

    private bool isTriggered = false; // 防止重复触发

    private void Start()
    {
        // 获取当前物体的 AudioSource，或根据需要添加 AudioSource 组件
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void OnTriggerStay(Collider other)
    {
        // 检测手部触碰并按下手柄触发按钮
        if (other.gameObject.CompareTag("Hand"))
        {
            if (leftHandTrigger.action.triggered || rightHandTrigger.action.triggered)
            {
                PlayMushroomInteraction(); // 手动触发互动
            }
        }
    }

    // 蘑菇互动逻辑
    private void PlayMushroomInteraction()
    {
        if (!isTriggered)
        {
            isTriggered = true; // 确保只触发一次

            // 播放粒子效果
            PlayParticleEffect();

            // 播放音效
            PlayGrabSound();

            // 销毁蘑菇
            Destroy(this.gameObject);
            
            // 添加蘑菇到背包逻辑可以在这里加入
        }
    }

    // 播放粒子效果
    private void PlayParticleEffect()
    {
        if (particlePrefab != null && particleSpawnPoint != null)
        {
            // 实例化粒子系统并播放
            GameObject particleInstance = Instantiate(particlePrefab, particleSpawnPoint.position, particleSpawnPoint.rotation);
            ParticleSystem particleSystem = particleInstance.GetComponent<ParticleSystem>();
            if (particleSystem != null)
            {
                particleSystem.Play();
            }

            // 销毁粒子系统
            Destroy(particleInstance, particleSystem.main.duration);
        }
    }

    // 播放抓取音效
    private void PlayGrabSound()
    {
        if (grabSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(grabSound);
        }
    }
}
