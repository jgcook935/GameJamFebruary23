using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Camera mainCamera;
    public bool clicksEnabled;

    static ClickManager _instance;
    public static ClickManager Instance
    {
        get
        {
            if (_instance == null) _instance = FindObjectOfType<ClickManager>();
            return _instance;
        }
    }

    private void Start()
    {
        clicksEnabled = true;
        mainCamera = CameraController.Instance.GetComponent<Camera>();
    }

    void Update()
    {
        if (!clicksEnabled) return;

        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit)
            {
                Debug.Log($"we hit something {hit.collider.gameObject.name}, {hit.collider.gameObject.tag}");
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click(hit.collider.gameObject.tag == "Enemy");
            }

            Debug.Log("we clicked but didn't hit anything");
        }
        else
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            // we can do cursor specific stuff here if we want
            //if (hit)
            //{
            //    UIManager.Instance.DoCursorHover();
            //}
            //else
            //{
            //    UIManager.Instance.DoCursorDefault();
            //}
        }
    }

    public void SetClicksEnabled(bool enabled)
    {
        clicksEnabled = enabled;
    }
}
