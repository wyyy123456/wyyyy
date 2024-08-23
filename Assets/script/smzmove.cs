using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Video;


public class smzmove : MonoBehaviour
{
    public Animator anim;
    public GameObject particlePrefab;
    public Transform spawnLocation;
    public Animator secondObjectAnimator;
    public TextMeshProUGUI scoreText;
    public int currentScore = 233; // 初始化分数
    public int scoreIncrement = 7; // 每次增加的分数
    private float counter = 2.0f;
    public AudioSource audioSource; // 用于播放音效
    public AudioClip scoreUpdateSound; // 分数更新时的音效

    public VideoPlayer videoPlayer; // 引用 VideoPlayer 组件
    public GameObject videoUI; // 显示视频的UI元素（例如一个包含Raw Image的GameObject）


private void Start()
    {
        // 确保游戏一开始时视频UI是隐藏的
        videoUI.SetActive(false);
        videoPlayer.Stop(); // 确保视频不播放
    }

    public void PlayPrintAnim()
    {
        // 播放动画
        anim.SetTrigger("airship");
        anim.SetTrigger("move11");

       

        // 启动延迟更新分数、播放粒子特效和音乐的协程
        StartCoroutine(DelayedIncreaseScoreAndPlayEffect());

        // 延迟销毁当前物体（可选）
        // Destroy(gameObject, destroyDelay);
    }

    private IEnumerator DelayedIncreaseScoreAndPlayEffect()
    {
        // 等待 7 秒钟
        yield return new WaitForSeconds(7.0f);

        // 增加分数
        currentScore += scoreIncrement;

        // 更新 UI Text
        if (scoreText != null)
        {
            scoreText.text = currentScore.ToString();
        }

        // 播放音乐
        PlayScoreUpdateSound();

        // 在分数更新的同时播放粒子特效
        InstantiateAndPlayParticleSystem();

        // 显示视频UI
        videoUI.SetActive(true);

        // 开始播放 2D 动画
        videoPlayer.Play();

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

    public void PlayScoreUpdateSound()
    {
        if (audioSource != null && scoreUpdateSound != null)
        {
            audioSource.PlayOneShot(scoreUpdateSound);
        }
    }
}