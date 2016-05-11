using UnityEngine;
using System.Collections;

public class CamTexture : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Renderer renderer = GetComponent<Renderer>();
        WebCamTexture webCamTexture = new WebCamTexture();

        renderer.material.mainTexture = webCamTexture;
        webCamTexture.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
