using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class CatsController : MonoBehaviour {

    public GameObject catStartTemplate;
    public Transform cameraTransform;
    public Vector3 defaultFinalPosition;
    public Vector3 finalPositionInterval;
    
    private CatBehavior[] cats = new CatBehavior[7];
    private int catCaptured;

	// Use this for initialization
	void Start () {
        randomCreateCats();
        catCaptured = 0;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void OnCatCaptured(int id) {
        if (id != catCaptured) {
            lose();
        } else {
            catCaptured++;
            if (catCaptured == 7) {
                win();
            }
        }
    }

    void randomCreateCats() {
        Vector3[] initPositions = randomCreatePositions();

        for (int i = 0; i < 7; i++) {
            CatBehavior newCat = (Instantiate(catStartTemplate,
                                    initPositions[i],
                                    new Quaternion(0, Random.Range(0f, 360f), 0, 0)
                                 ) as GameObject).GetComponent<CatBehavior>();
            newCat.transform.localScale = Vector3.one * (i*2 + 2);
            newCat.initPosition = initPositions[i];
            newCat.finalPosition = defaultFinalPosition + finalPositionInterval * i;
            newCat.transform.parent = transform;
            newCat.catID = i;
            cats[i] = newCat;
        }
    }

    Vector3[] randomCreatePositions() {
        float[] angles = { 60f, 100f, 140f, 180f, 220f, 260f, 300f };
        Vector3[] positions = new Vector3[7];

        for (int i=0; i<7; i++) {
            float angle = Mathf.Deg2Rad * angles[i];
            positions[i] = new Vector3(-Mathf.Cos(angle), 0, Mathf.Sin(angle)) * 10f;
            positions[i].y = 0.16f;
        }

        System.Random rnd = new System.Random();
        return positions.OrderBy(r => rnd.Next()).ToArray();
    }

    void lose() {
        foreach(CatBehavior cat in cats) {
            cat.clear();
            cat.runBack = true;
        }
        catCaptured = 0;
    }

    void win() {
        Debug.Log("Win");
    }
}
