using UnityEngine;
using System.Collections;

public class CatBehavior : MonoBehaviour {

    public Vector3 finalPosition { get; set; }
    public bool captured { get; set; }
    public float runSpeed;

    private bool arrived;
    private Animator animator;

	// Use this for initialization
	void Start () {
        arrived = false;
        captured = false;
        animator = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        if (captured && !arrived)
            running();
        else
            idle();
	}

    void idle() {
        animator.SetBool("Run", false);
    }

    void running() {
        if (finalPosition == transform.position) {
            arrived = true;
            return;
        }

        animator.SetBool("Run", true);
        transform.LookAt(finalPosition);
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, runSpeed);
    }
}
