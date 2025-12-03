using UnityEngine;

public enum ViewMode
{
    FirstPerson,
    ThirdPerson
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public ViewMode CurrentViewMode = ViewMode.ThirdPerson; // ±âº»°ª: 3ÀÎÄª

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
}
