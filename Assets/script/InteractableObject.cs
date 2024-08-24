using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public bool useClick = true; // 是否使用点击事件
    public bool useCollision = false; // 是否使用碰撞事件
    public bool goToNextScene = false; // 是否切换到下一个场景
    public string nextSceneName = "NextScene"; // 下一个场景的名称
    public GameObject popup; // 弹出窗口的 GameObject

    private void Update()
    {
        if (useClick && Input.GetMouseButtonDown(0)) // 如果启用点击事件并且鼠标按下
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // 从鼠标位置发射射线

            if (Physics.Raycast(ray, out hit)) // 射线检测
            {
                if (hit.collider.gameObject == this.gameObject) // 如果射线击中当前对象
                {
                    HandleInteraction();
                }
            }
        }

        if (useCollision)
        {
            // 碰撞检测代码如果需要
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (useCollision && collision.gameObject.CompareTag("Player1")) // 如果启用碰撞事件且碰撞的对象标签为 "Player1"
        {
            goToNextScene = true;

            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        // 输出调试信息
        Debug.Log("Interaction handled.");
        Debug.Log("Popup is set to active: " + (popup != null ? popup.activeSelf.ToString() : "No popup assigned"));
        Debug.Log("goToNextScene value: " + goToNextScene);

        if (popup != null)
        {
            popup.SetActive(true); // 激活弹出窗口
        }

        if (goToNextScene)
        {
            Debug.Log("Loading next scene: " + nextSceneName);
            SceneManager.LoadScene(nextSceneName); // 切换到下一个场景
        }
        else
        {
            Debug.LogWarning("Scene not loaded because goToNextScene is false.");
        }
    }
}
