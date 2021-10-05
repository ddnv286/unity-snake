using UnityEngine;
using System.Collections.Generic;

public class Snake : MonoBehaviour
{
    private Vector2 _direction = Vector2.right;
    private List<Transform> _segments = new List<Transform>();
    public Transform segmentPrefab;
    public int initialSize = 4;

    private void Start ()
    {
        ResetState();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _direction = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            _direction = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            _direction = Vector2.down;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            _direction = Vector2.right;
        }
    }

    // handle physics and movemen after fixed time
    private void FixedUpdate ()
    {
        // the idea is to move the snake's i_th segment's position to the one in front of it
        // must do this in reverse order so the position is set to the previous
        // position, otherwise they will all be stacked on top of each other
        for (int i = _segments.Count - 1; i > 0; i--)
        {
            _segments[i].position = _segments[i - 1].position;
        }

        this.transform.position = new Vector3(
            Mathf.Round(this.transform.position.x) + _direction.x,
            Mathf.Round(this.transform.position.y) + _direction.y, 
            0.0f
         );
    }

    private void Grow ()
    {
        Transform segment = Instantiate(this.segmentPrefab);
        // adding the segment prefab to the end of the snake
        segment.position = _segments[_segments.Count - 1].position;
        _segments.Add(segment);
    }

    private void ResetState ()
    {
        // first iterate through the segments and destroy the game object, then reset snake's position
        // start at 1 to skip destroying the head
        for (int i  = 1; i < _segments.Count; i++)
        {
            Destroy(_segments[i].gameObject);
        }
        _segments.Clear();
        _segments.Add(this.transform);

        // -1 since the head is already in the list, that's why when I didn't minus the head, 
        // the prefab spawned right at the head so the game won't work
        for (int i = 1; i< initialSize - 1; i++)
        {
            _segments.Add(Instantiate(this.segmentPrefab));
        }

        this.transform.position = Vector3.zero;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // add food's tag as food first so the snake can recognize if the it collides the food or not
        if (other.tag == "Food")
        {
            Grow();
        } else if (other.tag == "Obstacle")
        {
            ResetState();
        }
    }
}
