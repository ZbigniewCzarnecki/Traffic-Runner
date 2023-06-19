using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    private bool _isFirstUpdate = true;

    private void Update()
    {
        if (_isFirstUpdate)
        {
            _isFirstUpdate = false;

            SceneLoader.LoaderCallback();
        }
    }
}