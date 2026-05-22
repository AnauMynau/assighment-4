using UnityEngine;

public class CameraSwitcher : MonoBehaviour
{
    public Camera firstPersonCam;
    public Camera thirdPersonCam;
    public GameObject soldierModel;

    void Start()
    {
        firstPersonCam.enabled = true;
        thirdPersonCam.enabled = false;

        if (soldierModel != null) soldierModel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            bool isFirstPerson = firstPersonCam.enabled;

            firstPersonCam.enabled = !isFirstPerson;
            thirdPersonCam.enabled = isFirstPerson;

            if (soldierModel != null) soldierModel.SetActive(isFirstPerson);
        }
    }
}