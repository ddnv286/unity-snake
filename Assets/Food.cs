using UnityEngine;

public class Food : MonoBehaviour
{
    public BoxCollider2D gridArea;

    private void Start ()
    {
        RandomizePosition();
    }

    private void RandomizePosition ()
    {
        // the idea is to refer the bounds of the game area and randomly spawn the food just inside of it
        Bounds bounds = this.gridArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        this.transform.position = new Vector3(Mathf.Round(x), Mathf.Round(y), 0.0f);
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        // add snake's tag as player first so the food can recognize if the snake collides it or not
        if (other.tag == "Player")
        {
            RandomizePosition();
        }
    }
}
