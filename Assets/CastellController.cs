using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CastellController : MonoBehaviour
{
    private System.Random rnd;
    private Camera camera;

    private GameObject pinya;
    private GameObject current_pis;

    private int pis_counter=0;
    public Object[] pissos_sprites;

    public float last_degree = 0f;
    public float sentit=1f;

    public float velocitat_gir = 30f;

    private bool playable=true;

    public GameObject followed_item=null;

    public int random_max_degree=10;

    public bool isPlayable()
    {
        return this.playable;
    }

    GameObject nextPis(Quaternion last_rotation, Vector3 last_position)
    {
        GameObject next_pis = new GameObject("pis "+pis_counter);
        pis_counter++;

        next_pis.transform.SetParent(pinya.transform, false);
        next_pis.transform.localPosition=new Vector3((last_position.x+((last_rotation.z*360)/100)*-1),pis_counter,0);

        next_pis.AddComponent(typeof(SpriteRenderer));
        (next_pis.GetComponent(typeof(SpriteRenderer)) as SpriteRenderer).sprite=pissos_sprites[0] as Sprite;

        next_pis.transform.rotation=last_rotation;
        velocitat_gir*=1.2f;

        ((camera.GetComponent(typeof(CameraFollow))) as CameraFollow).setTargetName(next_pis.name);

        return next_pis;
    }

    // Start is called before the first frame update
    void Start()
    {
        rnd = new System.Random();

        camera = GameObject.Find("camera").GetComponent(typeof(Camera)) as Camera;
        pinya = GameObject.Find("pinya");

        pissos_sprites = Resources.LoadAll(path: "Pissos", systemTypeInstance: typeof(Sprite));

        current_pis = this.nextPis(Quaternion.identity, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            if(playable)
            {
                last_degree+=current_pis.transform.rotation.z;
                current_pis = this.nextPis(current_pis.transform.rotation, current_pis.transform.position);
                if(
                    ((last_degree)*180>15f)||
                    ((last_degree)*180<-15f)||
                    (current_pis.transform.position.x>5)||
                    (current_pis.transform.position.x<-5))
                {
                    Debug.Log("degree: "+((last_degree)*180));
                    Debug.Log("position: "+current_pis.transform.position.x);
                    FallMode();
                }
            }
        }
        else
        {
            current_pis.transform.RotateAround(current_pis.transform.position, new Vector3(0,0,1), sentit * velocitat_gir * Time.deltaTime);

            float rotacio = current_pis.transform.rotation.z*180;

            if((rotacio>random_max_degree)||(rotacio<-random_max_degree))
            {
                sentit*=-1;
                random_max_degree = 10+rnd.Next(0, ((int)last_degree+10));
            }
        }
    }

    void FallMode()
    {
        playable=false;
        StartCoroutine(ResetSceneOn(3f));
        for (int i = 0; i < pinya.transform.childCount; i++)
        {
            pinya.transform.GetChild(i).gameObject.AddComponent(typeof(Rigidbody2D));
        }
    }


	IEnumerator ResetSceneOn(float delay)
	{
		yield return new WaitForSeconds(delay);
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
