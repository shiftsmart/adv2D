 
using UnityEngine;

public class slime : MonoBehaviour
{

    [SerializeField, Header("≠Ë≈È±±®Ó")]
    public Rigidbody2D body;
    public GameObject effect;

    private void OnTriggerEnter2D(Collider2D other) {
        body = GetComponent<Rigidbody2D>();
        if (other.CompareTag("Player")) {
            Instantiate(effect, transform.position,Quaternion.identity);
            body.AddForce(new Vector2(0, 200));
        }
    }

}
