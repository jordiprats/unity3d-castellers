using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public GameObject target;
    public GameObject background;
    public string target_name="pinya";
    public float speed=5f;
    public Vector3 offset;
    private Vector3 base_background_position;
    public float diferencial=0.001f;

    void Start()
    {
        offset = new Vector3(0,0,-10);
        background = GameObject.Find("background");

        base_background_position = background.transform.position;
    }

    public void setTargetName(string name)
    {
        this.target_name=name;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        target = GameObject.Find(target_name);

        Vector3 original_position=transform.position;
        Vector3 desired_position = Vector3.Lerp(transform.position, target.transform.position + offset, speed*Time.deltaTime);
        transform.position = new Vector3(original_position.x, Mathf.Clamp(desired_position.y, 0, float.MaxValue), original_position.z);

        //diferencial = transform.position.y/4;

        if(target_name=="pinya")
            background.transform.position = base_background_position;
        else
            background.transform.position=new Vector3(0, base_background_position.y - (diferencial*((int)target.transform.position.y/1.2f)), 90);
    }
}