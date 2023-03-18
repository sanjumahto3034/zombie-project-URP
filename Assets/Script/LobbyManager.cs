using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LobbyManager : MonoBehaviour
{
    public GameObject loadingScreen;
    public loadingScene _loader;
    public void DemoScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        StartCoroutine(LoadScene(constant.DEMO_SCENE));
    }
    public void LobbyScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        StartCoroutine(LoadScene(constant.LOBBY_SCENE));
    }
    public void GameScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        StartCoroutine(LoadScene(constant.GAME_SCENE));
        Debug.Log("Wait To Load Scene");

    }
       IEnumerator LoadScene(string sceneName)
    {
        /*
        * @param int which show delay time
        */
        SceneManager.LoadScene(sceneName);
        // _loader.setActive(true);
        // AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);

        // Wait until the asynchronous scene fully loads
        // while (!asyncLoad.isDone)
        // {
            // Debug.Log("[ SceneManager | Loading ]");
            // _loader.setSliderValues(asyncLoad.progress);
            // if(asyncLoad.progress>=0.95)_loader.setActive(false);
            yield return null;
        // }
        

    }
}
