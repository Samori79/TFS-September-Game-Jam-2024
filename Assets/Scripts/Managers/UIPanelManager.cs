using UnityEngine;

public class UIPanelManager : MonoBehaviour
{
    private static UIPanelManager _instance;

    public static UIPanelManager Instance
    {
        get { return _instance; }
    }

    private void Awake()
    {
        // Check if there's already an instance of UIPanelManager
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Make this object persistent
        }
        else
        {
            Destroy(gameObject);  // Destroy the duplicate instance
        }
    }

    // You can add methods to manage your UI here
}