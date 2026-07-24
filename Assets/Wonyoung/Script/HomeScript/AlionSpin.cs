using Unity.VisualScripting;
using UnityEngine;

public class AlionSpin : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 100f;
 
    [SerializeField] private float floatAmount = 0.3f;

    [SerializeField] private float floatSpeed = 2.5f;

    private Vector3 startPos;

    void Awake()
    {
        startPos = transform.localPosition;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward * rotationSpeed * Time.deltaTime);//돌기

        float newY = startPos.y + Mathf.Sin(Time.time * floatSpeed) * floatAmount;//부유하는 느낌 주기
        transform.localPosition = new Vector3(startPos.x, newY, startPos.z);
    }
}
