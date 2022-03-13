using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    private float length, startpos; // variables dictating length and starting position of background sprites
    public GameObject cam; // camera
    public float parallaxEffect; // intensity of parallax
    private Component component;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;

    // Start is called before the first frame update
    void Start()
    {
        component = GetComponent<SpriteRenderer>();
        if(component == null) // kill floor
        {
            box = GetComponent<BoxCollider2D>();
            length = box.bounds.size.x;
        }
        else //background 
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            length = spriteRenderer.bounds.size.x;
        }
        
        startpos = transform.position.x;
        

        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float dist = (cam.transform.position.x * (1 - parallaxEffect));
        float temp = (cam.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + length) startpos += length;
        else if (temp < startpos - length) startpos -= length;
    }
}
