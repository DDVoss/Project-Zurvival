using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
[RequireComponent(typeof(Rigidbody))]
public class CameraTriggerVolume : MonoBehaviour
{

    [SerializeField] private CinemachineCamera cam;
    [SerializeField] private Vector3 boxSize;

    private BoxCollider _box;
    private Rigidbody _rb;

    private void Awake()
    {
        _box = GetComponent<BoxCollider>();
        _rb = GetComponent<Rigidbody>();
        _box.isTrigger = true;
        _box.size = boxSize;
        _rb.isKinematic = true;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, boxSize);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (CameraSwitcher.ActiveCamera != cam) CameraSwitcher.SwitchCamera(cam);
        }
    }
}
