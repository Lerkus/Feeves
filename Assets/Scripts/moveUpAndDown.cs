using UnityEngine;
using System.Collections;

public class moveUpAndDown : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    public float offset = 0;
    public float amplitude = 1;
    public float timeTweaker = 5;
    private float startTime;

    public void Awake()
    {
        startPos = transform.position + new Vector3(0, amplitude, 0);
        endPos = transform.position - new Vector3(0, amplitude, 0);
        startTime = Time.time;
    }

    public void FixedUpdate()
    {
        float ratio = (offset + Time.time - startTime) / timeTweaker;
        transform.position = new Vector3(startPos.x, Mathf.Lerp(startPos.y, endPos.y, (float)ratio), startPos.z);

        if (Mathf.Abs(transform.position.y - endPos.y) < 0.00001f)
        {
            Vector3 tempPos = endPos;
            endPos = startPos;
            startPos = tempPos;

            startTime = Time.time;
        }

    }

}
