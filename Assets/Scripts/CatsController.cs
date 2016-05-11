using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CatsController : MonoBehaviour {

    public GameObject catStartTemplate;
    public Transform cameraTransform;
    public Vector3 defaultFinalPosition;
    public Vector3 finalPositionInterval;

    private List<CatBehavior> cats = new List<CatBehavior>();

	// Use this for initialization
	void Start () {
        randomCreateCat();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void randomCreateCat() {
        CatBehavior newCat = (Instantiate(catStartTemplate, 
                                new Vector3(Random.Range(5f, 10f), catStartTemplate.transform.position.y, Random.Range(5f, 10f)) + cameraTransform.position, 
                                new Quaternion(0, Random.Range(0f, 360f), 0, 0)
                             ) as GameObject).GetComponent<CatBehavior>();
        newCat.finalPosition = defaultFinalPosition + finalPositionInterval * cats.Count;
        newCat.transform.parent = transform;
        cats.Add(newCat);
    }
}
