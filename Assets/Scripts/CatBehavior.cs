using UnityEngine;
using System.Collections;

public class CatBehavior : MonoBehaviour, ICardboardGazeResponder {

    public CatsController catsController;
    public float runSpeed;

    public Vector3 initPosition { get; set; }
    public Vector3 finalPosition { get; set; }
    public int catID { get; set; }
    public bool runBack { get; set; }

    private bool captured;
    private bool arrived;
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = GetComponent<Animator>();
        clear();
	}
	
	// Update is called once per frame
	void Update () {
        if (runBack)
            goBackRunning();
        else if (captured && !arrived)
            running();
        else
            idle();
	}

    public void clear() {
        arrived = false;
        captured = false;
        runBack = false;
    }

    void idle() {
        animator.SetBool("Run", false);
    }

    void running() {
        if (finalPosition == transform.position) {
			arrived = true;

			Vector3 originalAngle = transform.eulerAngles;
			originalAngle.x = 0;
			originalAngle.z = 0;
			transform.eulerAngles = originalAngle;
            return;
        }

        animator.SetBool("Run", true);
        transform.LookAt(finalPosition);
        transform.position = Vector3.MoveTowards(transform.position, finalPosition, runSpeed * Time.deltaTime);
    }

    void goBackRunning() {
        if (initPosition == transform.position) {
            runBack = false;

			Vector3 originalAngle = transform.eulerAngles;
			originalAngle.x = 0;
			originalAngle.z = 0;
			transform.eulerAngles = originalAngle;
            return;
        }

        animator.SetBool("Run", true);
        transform.LookAt(initPosition);
        transform.position = Vector3.MoveTowards(transform.position, initPosition, runSpeed * Time.deltaTime);
    }

	public void OnGazeEnter() {
		animator.SetBool ("Jump", true);
    }

	public void OnGazeExit() {
		animator.SetBool ("Jump", false);
    }

    public void OnGazeTrigger() {
        captured = true;
        catsController.OnCatCaptured(catID);
    }
}
