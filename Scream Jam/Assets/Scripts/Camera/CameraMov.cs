using Unity.VisualScripting;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    Camera m_Camera;

    [SerializeField]
    GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        m_Camera = GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Move Cam");
    }

}
