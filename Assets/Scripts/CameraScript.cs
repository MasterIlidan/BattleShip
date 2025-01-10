using DefaultNamespace;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    [SerializeField] public Vector3 cameraPos; // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraPos = new(-58f, 0f, -10f);
        GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>().OnCameraPosChanged.AddListener(SetNewCameraPos);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 smoothedCameraPosition = Vector3.Slerp(transform.position, cameraPos, 0.125f);
        transform.position = smoothedCameraPosition;
    }

    void SetNewCameraPos(Vector3 pos)
    {
        cameraPos = pos;
    }
}
