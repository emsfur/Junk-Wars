using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarageDoorController : MonoBehaviour
{
    public GarageDoorStatus doorStatus;
    public Transform garageDoor;
    public Quaternion targetRotation = new Quaternion(80, 0, 0, 0);


    private void OnTriggerStay(Collider other)
    {

        if (other.gameObject.CompareTag("MainCamera"))
        {
            if (Input.GetKeyUp(KeyCode.E)&& doorStatus.doorIsOpen == false && doorStatus.canRotate)
            {
                doorStatus.canRotate = false;
                StartCoroutine(Rotate(Vector3.right, -80, 1.0f)); 

            }
            if (Input.GetKeyUp(KeyCode.E) && doorStatus.doorIsOpen == true && doorStatus.canRotate)
            {
                doorStatus.canRotate = false;
                StartCoroutine(Rotate(Vector3.right, 80, 1.0f));

            }
        }
    }


    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        Quaternion from = garageDoor.rotation;
        Quaternion to = garageDoor.rotation;
        to *= Quaternion.Euler(axis * angle);

        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            garageDoor.rotation = Quaternion.Slerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }
        garageDoor.rotation = to;
        doorStatus.doorIsOpen = !doorStatus.doorIsOpen;
        doorStatus.canRotate = true;
    }

}
