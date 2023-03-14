using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LobbyManager : MonoBehaviour
{
    public GameObject LoadingLayer;
    void Start(){
        LoadingLayer.SetActive(false);
    }
    public void DemoScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        SceneManager.LoadScene(constant.DEMO_SCENE);
    }
    public void LobbyScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        SceneManager.LoadScene(constant.LOBBY_SCENE);
    }
    public void GameScene()
    {
        /*
        * This function will trigger from UI will call Demo Scene
        */
        SceneManager.LoadScene(constant.GAME_SCENE);
    }
}
