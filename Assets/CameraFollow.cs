using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public string target_name="pinya";
    public float speed=10f;
    public Vector3 offset;

    void Start()
    {
        offset = new Vector3(0,0,-10);
    }

    public void setTargetName(string name)
    {
        this.target_name=name;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        target = GameObject.Find(target_name);

        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset, speed*Time.deltaTime);;
    }
}