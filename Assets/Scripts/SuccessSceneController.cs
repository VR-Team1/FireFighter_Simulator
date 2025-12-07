using UnityEngine;
using UnityEngine.SceneManagement;

public class SuccessSceneController : MonoBehaviour
{
    public void Update()
    {
        
    }
    public void OnClick()
    {
        SceneManager.LoadScene("2_TitleScene");
    }
}
