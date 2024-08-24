using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class tiao : MonoBehaviour
{
    public GameObject particleSystemPrefab1; // 第一个粒子系统预设体
    public GameObject particleSystemPrefab2; // 第二个粒子系统预设体
    public Transform spawnLocation1; // 第一个粒子系统生成位置
    public Transform spawnLocation2; // 第二个粒子系统生成位置
    public float scaleGrowthRate = 0.5f; // 粒子系统的增长速度
    public float maxScale = 10.0f; // 粒子系统的最大尺寸
    public GameObject fadeScreen; // 黑幕UI元素
    private float counter = 2.0f;

    private GameObject instantiatedParticleSystem1;
    private GameObject instantiatedParticleSystem2;
    private FadeScreen fadeScreenScript;

    // Start is called before the first frame update
    void Start()
    {
        if (fadeScreen != null)
        {
            fadeScreenScript = fadeScreen.GetComponent<FadeScreen>();
        }
        else
        {
            Debug.LogError("fadeScreen is not assigned.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter -= Time.deltaTime;

        if (counter <= 0)
        {
            counter = 10.0f;
            Jump();
        }
        if (instantiatedParticleSystem1 != null)
        {
            // Increment the scale of the first particle system
            Vector3 currentScale = instantiatedParticleSystem1.transform.localScale;
            Vector3 newScale = currentScale + Vector3.one * scaleGrowthRate * Time.deltaTime;

            // Clamp the scale to the maximum size
            instantiatedParticleSystem1.transform.localScale = Vector3.Min(newScale, Vector3.one * maxScale);
        }

        if (instantiatedParticleSystem2 != null)
        {
            // Increment the scale of the second particle system
            Vector3 currentScale = instantiatedParticleSystem2.transform.localScale;
            Vector3 newScale = currentScale + Vector3.one * scaleGrowthRate * Time.deltaTime;

            // Clamp the scale to the maximum size
            instantiatedParticleSystem2.transform.localScale = Vector3.Min(newScale, Vector3.one * maxScale);
        }
    }

    public void Jump()
    {
        StartCoroutine(PlayParticlesAndSwitchScene());
    }

    private IEnumerator PlayParticlesAndSwitchScene()
    {
        if (spawnLocation1 != null && particleSystemPrefab1 != null)
        {
            instantiatedParticleSystem1 = Instantiate(particleSystemPrefab1, spawnLocation1.position, spawnLocation1.rotation);

            ParticleSystem ps1 = instantiatedParticleSystem1.GetComponent<ParticleSystem>();
            if (ps1 != null)
            {
                ps1.Play();
            }
        }
        else
        {
            Debug.LogError("spawnLocation1 or particleSystemPrefab1 is not assigned.");
        }

        if (spawnLocation2 != null && particleSystemPrefab2 != null)
        {
            instantiatedParticleSystem2 = Instantiate(particleSystemPrefab2, spawnLocation2.position, spawnLocation2.rotation);

            ParticleSystem ps2 = instantiatedParticleSystem2.GetComponent<ParticleSystem>();
            if (ps2 != null)
            {
                ps2.Play();
            }
        }
        else
        {
            Debug.LogError("spawnLocation2 or particleSystemPrefab2 is not assigned.");
        }

        // Fade to black
        if (fadeScreenScript != null)
        {
            fadeScreenScript.FadeOut();
        }

        // Wait for 8 seconds before switching scenes
        yield return new WaitForSeconds(1);

        // Wait for the fade out to complete
        if (fadeScreenScript != null)
        {
            yield return new WaitForSeconds(fadeScreenScript.fadeDuration);
        }

        // Load the next scene
        SceneManager.LoadScene(1);

        // Ensure FadeScreen is found in the new scene
        yield return new WaitUntil(() => {
            fadeScreenScript = FindObjectOfType<FadeScreen>();
            return fadeScreenScript != null;
        });

        // Fade in
        fadeScreenScript.FadeIn();

        // Optionally wait for the fade in to complete
        // yield return new WaitForSeconds(fadeScreenScript.fadeDuration);
    }
}