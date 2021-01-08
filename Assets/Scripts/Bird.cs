using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    private Vector3 _initialPos;
    private bool _birdWasLaunched;
    private float _timeSittingAround;

    [SerializeField] private float _launchPow = 500;
    

    private void Awake()
    {
        _initialPos = transform.position;
    }

    private void Update()
    {
        if(_birdWasLaunched && GetComponent<Rigidbody2D>().velocity.magnitude <= 0.1)
        {
            _timeSittingAround += Time.deltaTime;
        }

        if (transform.position.y > 10 || transform.position.y < -10 ||
            transform.position.x >10 || transform.position.x < -10
            || _timeSittingAround > 3)
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentSceneName);
        }
    }

    private void OnMouseDown()
    {
        GetComponent<SpriteRenderer>().color = Color.red; 
    }

    private void OnMouseUp()
    {
        GetComponent<SpriteRenderer>().color = Color.white;

        Vector2 diractionToInitialPos = _initialPos - transform.position;
        GetComponent<Rigidbody2D>().AddForce(diractionToInitialPos*_launchPow);
        GetComponent<Rigidbody2D>().gravityScale = 1;
        _birdWasLaunched = true;
    }

    private void OnMouseDrag()
    {
        Vector3 newPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(newPos.x, newPos.y);
    }
}
