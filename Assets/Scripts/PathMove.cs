using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PathMove : MonoBehaviour
{
[SerializeField] Transform[] thePath;
[SerializeField] float speed = 5f;
int waypoint = 0;
public string sceneNameToLoad;
private bool firstCollision = true;
private bool canDetectCollision = true;


    void Start()
    {
    }

    void Update(){
        CheckPlayerIn();
        if (canDetectCollision)
        {
            CheckPlayerout();
        }
    }

    public void move()
    {
        Vector3 dir = thePath[waypoint].position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, thePath[waypoint].transform.position, speed * Time.deltaTime);
        transform.forward = Vector3.Lerp(transform.forward, dir, 0.1f * Time.deltaTime);
    
        if (transform.position == thePath[waypoint].transform.position) {
            waypoint++;
        }
    }

    private void CheckPlayerIn(){
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 10f);
        if(cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player"))
                {
                    move();
                }
            }
        }
    }

    private void CheckPlayerout(){
        Collider[] cols = Physics.OverlapSphere(this.transform.position, 100f);
        if(cols.Length > 0)
        {
            for (int i = 0; i < cols.Length; i++)
            {
                if (cols[i].CompareTag("Player"))
                {
                    if(firstCollision){
                        firstCollision = false;
                        canDetectCollision = false;
                        StartCoroutine(delay());
                    } else 
                        OnEventSceneLoading();
                }
            }
        }
    }

    public void OnEventSceneLoading() {
        SceneManager.LoadSceneAsync(sceneNameToLoad);
    }

    public string SceneName {
        get { return sceneNameToLoad; }
        set { sceneNameToLoad = value; }
    }
    IEnumerator delay(){
        yield return new WaitForSecondsRealtime(300f);
        canDetectCollision = true;
    }
}