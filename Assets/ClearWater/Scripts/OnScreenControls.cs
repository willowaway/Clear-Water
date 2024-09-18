using UnityEngine;

public class OnScreenControls : MonoBehaviour
{
    public Canvas controlsCanvas;
    public GameObject buttonRestart;

    void Awake()
    {
        bool isMobile = SystemInfo.deviceType == DeviceType.Handheld;

        if (controlsCanvas != null)
            controlsCanvas.gameObject.SetActive(isMobile);

        if (isMobile)
        {
            Application.targetFrameRate = 60;
        }
        else
        {
            buttonRestart.SetActive(false);
        }
    }

    public void ShowControls()
    {
        controlsCanvas.gameObject.SetActive(true);
    }

    public void HideControls()
    {
        controlsCanvas.gameObject.SetActive(false);
    }
}
