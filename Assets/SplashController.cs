using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartOn(2f));
    }

    IEnumerator StartOn(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene("castells");
    }
}
