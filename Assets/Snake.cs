using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Snake : MonoBehaviour {
    Vector2 dir = Vector2.right;

    List<Transform> tail = new List<Transform>();

    bool ate = true;

    public GameObject tailPrefab;

    public AudioSource eatSound;
    public AudioSource endSound;

    private bool gameEnded = false;

    // Use this for initialization
    void Start () {
        InvokeRepeating("Move", 0.1f, 0.1f);
        Vector2 v = (Vector2)transform.position + Vector2.right;
        GameObject g = (GameObject)Instantiate(tailPrefab, v,
                                                   Quaternion.identity);
        tail.Insert(0, g.transform);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.RightArrow)) {
            dir = Vector2.right;
        } else if (Input.GetKey(KeyCode.LeftArrow)) {
            dir = Vector2.left;
        } else if (Input.GetKey(KeyCode.UpArrow)) {
            dir = Vector2.up;
        } else if (Input.GetKey(KeyCode.DownArrow)) {
            dir = Vector2.down;
        }
    }

    void Move () {
        if (gameEnded) {
            return;
        }
        Vector2 v = transform.position;

        transform.Translate(dir);

        if (ate) {
            Debug.Log("Increasing snake size");
            GameObject g = (GameObject)Instantiate(tailPrefab, v,
                                                   Quaternion.identity);

            tail.Insert(0, g.transform);
            ate = false;
        }
        else if (tail.Count > 0) {
            tail.Last().position = v;

            tail.Insert(0, tail.Last());
            tail.RemoveAt(tail.Count - 1);
        }
    }

    void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log("Collision with ", collision);
        if (collision.name.StartsWith("FoodPrefab")) {
            Debug.Log("Ate food!");
            eatSound.Play();
            ate = true;
            Destroy(collision.gameObject);
        } else {
            Debug.Log("You lost!");
            endSound.Play();
            tail.Clear();
            gameEnded = true;
        }
    }
}
