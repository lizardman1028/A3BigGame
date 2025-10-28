using Unity.Mathematics;
using UnityEngine;

public class BounceAround : MonoBehaviour
{
    public Rigidbody2D rigidBody;
    
    private Vector2 velocity;
    
    private void Start() {
      velocity = UnityEngine.Random.insideUnitCircle;
      velocity.Normalize();
    }

    private void Update() {
      if (transform.position.x > 7) {
        velocity = new Vector2(math.abs(velocity.x) * -1, velocity.y);
      }
      if (transform.position.x < -7) {
        velocity = new Vector2(math.abs(velocity.x), velocity.y);
      }
      
      if (transform.position.y > 5) {
        velocity = new Vector2(velocity.x, math.abs(velocity.y) * -1);
      }
      
      if (transform.position.y < -5) {
        velocity = new Vector2(velocity.x, math.abs(velocity.y));
      }
      
      transform.position = new Vector3(
        transform.position.x + velocity.x * Time.deltaTime, 
        transform.position.y + velocity.y * Time.deltaTime, 
        transform.position.z
      );

    }
}
