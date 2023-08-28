using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DebugWindow : MonoBehaviour
{
    public static DebugWindow instance;
    [SerializeField] private TextMeshProUGUI debugText = default;

    private ScrollRect scrollRect;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

        private void Start()
    {
        // Cache references
        scrollRect = GetComponentInChildren<ScrollRect>();

        // Subscribe to log message events
        Application.logMessageReceived += HandleLog;

        // Set the starting text
        debugText.text = "Debug messages will appear here.\n\n";
    }

    private void OnDestroy()
    {
        Application.logMessageReceived -= HandleLog;
    }

    private void HandleLog(string message, string stackTrace, LogType type)
    {
        debugText.text += message + " \n";
        Canvas.ForceUpdateCanvases();
        scrollRect.verticalNormalizedPosition = 0;
    }
}
