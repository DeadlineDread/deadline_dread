using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathMove : MonoBehaviour
{
    [SerializeField] Transform[] thePath;
    [SerializeField] float speed = 5f;
    int waypoint = 0;


    void Start()
    {
    }

    void Update(){
        CheckPlayerIn();
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
}