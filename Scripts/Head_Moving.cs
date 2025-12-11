using System.Collections;
using UnityEngine;

public class Head_Moviving : MonoBehaviour
{
    public Transform Head;
    public float nodAngle = 20f;
    public float nodSpeed = 5f;

    private Quaternion originalRotation;
    private bool isInitialized = false;
    private void Start()
    {
     //   Head = GameObject.Find("A/Armature/Hips/Spine/Neck/Head").transform;
    }
    public void InitializeFromAvatar(GameObject avatar)
    {
        var headObject = avatar.transform.Find("Armature/Hips/Spine/Neck/Head");
        print(headObject);
        if (headObject != null)
        {
            Head = headObject;
            originalRotation = Head.localRotation;
            isInitialized = true;
        }
        else
        {
            Debug.LogError("Head not in avatar!");
        }
    }

    void Update()
    {
        if (!isInitialized||Head == null)
           

        {
            if (GameObject.Find("Armature/Hips/Spine/Neck/Head"))
            {
                Head = GameObject.Find("Armature/Hips/Spine/Neck/Head").GetComponent<Transform>();
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            StopAllCoroutines();
            StartCoroutine(NodHead());
        }
    }

    IEnumerator NodHead()
    {
        if (Head == null) yield break;

        Quaternion downRotation = Quaternion.Euler(originalRotation.eulerAngles.x + nodAngle, originalRotation.eulerAngles.y, originalRotation.eulerAngles.z);

        float elapsedTime = 0f;
        while (elapsedTime < 1f / nodSpeed)
        {
            Head.localRotation = Quaternion.Slerp(originalRotation, downRotation, elapsedTime * nodSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Head.localRotation = downRotation;

        elapsedTime = 0f;
        while (elapsedTime < 1f / nodSpeed)
        {
            Head.localRotation = Quaternion.Slerp(downRotation, originalRotation, elapsedTime * nodSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        Head.localRotation = originalRotation;
    }
}
