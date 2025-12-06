using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public GameObject[] Fires;     // 불 오브젝트들
    public float timeLimit = 60f;  // 제한 시간(초)

    private float timer;

    void Start()
    {
        timer = timeLimit;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SceneManager.LoadScene("5_FailScene");
        }
        else if (AllFiresGone())
        {
            SceneManager.LoadScene("6_SuccessScene");
        }
    }

    // 불 배열을 검사해서 모두 Destroy 되었는지 확인
    bool AllFiresGone()
    {
        foreach (GameObject fire in Fires)
        {
            if (fire != null)   // 하나라도 남아있으면 아직 완료 아님
                return false;
        }
        return true;            // 전부 null → 성공
    }
}
