using UnityEngine;

public class ClickManager : MonoBehaviour
{
    Camera mainCamera;

    private void Start()
    {
        mainCamera = CameraController.Instance.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

            if (hit)
            {
                Debug.Log("we hit something");
                IClickable clickable = hit.collider.GetComponent<IClickable>();
                clickable?.Click();
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
}
