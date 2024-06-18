using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EventLoadScean : MonoBehaviour
{
    public string sceneNameToLoad;

    public void OnEventSceneLoading() {
        SceneManager.LoadSceneAsync(sceneNameToLoad);
    }

    public string SceneName {
        get { return sceneNameToLoad; }
        set { sceneNameToLoad = value; }
    }
}