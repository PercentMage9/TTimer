using UnityEngine;

public class FullscreenToggle : MonoBehaviour
{
    private bool isFullscreen = true;

    public void ToggleFullscreen()
    {
        isFullscreen = !isFullscreen;

        if (isFullscreen)
        {
            Screen.SetResolution(Screen.currentResolution.width, Screen.currentResolution.height, true);
        }
        else
        {
            Screen.SetResolution(1280, 720, false);
        }
    }
}