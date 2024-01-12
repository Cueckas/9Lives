using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class FollowObject : MonoBehaviour
{
    [SerializeField]
    Transform target;

    public bool smooth = true;
    void Update()
    {
        if (target != null)
        {
            Vector3 targetPosition = target.transform.position;
        targetPosition.z = transform.position.z; // Maintain the current z position


        if(smooth){
            Vector3 newCameraPos = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime);
            transform.position = new Vector3(newCameraPos.x, newCameraPos.y, transform.position.z);
        }
        else{
            transform.position = targetPosition;
        }
        }
        
        
        
        
        //Camera.main.transform.Translate(new Vector3(transform.xAxisValue, transform.yAxisValue, 0.0f));
    }

    public void changeTarget( Transform target){

        this.target = target;
    }
}
