using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallax : MonoBehaviour

{
    private Material material;
    public float xVel;

    // Start is called before the first frame update
    void Start()
    {
        material = this.GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        material.mainTextureOffset += new Vector2(xVel, 0) * Time.deltaTime;
    }
}