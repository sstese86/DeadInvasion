using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using NaviEnt.Level;
using NaviEnt;

public class SplashScreen : MonoBehaviour
{
    [SerializeField]
    Slider _loadingSlider = null;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.PlayMusic();
        StartCoroutine(LoadMainMenuAsync());
    }

    IEnumerator LoadMainMenuAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("MainMenu", LoadSceneMode.Additive);
        // use fake Progress to check updating progress is working.
        float fakeProgress = 0f;        
        while(fakeProgress < 1f)
        {
            fakeProgress += 0.02f;

            UpdateProgress(fakeProgress);
            yield return new WaitForSeconds(0.01f);
        }

        
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
        Destroy(gameObject);
        SceneManager.UnloadSceneAsync("Splash");
    }

    void UpdateProgress(float progress)
    {
        if(_loadingSlider!=null)
        {
            _loadingSlider.value = progress;
        }
    }

}
