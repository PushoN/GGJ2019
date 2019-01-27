using UnityEngine;
using UnityEngine.UI;

public class InputListener : MonoBehaviour
{
    public bool debug;
    public Text debugText;
    public GameObject controller;
    public GameObject controllerRay;

    public void Start()
    {
        debugText.gameObject.SetActive(debug);
    }

    public void Update()
    {
        OVRInput.Update();
        Vector3 localPos = OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTrackedRemote);
        Quaternion localRot = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        controller.transform.localPosition = localPos;
        controller.transform.localRotation = localRot;

        // Controller raycast.
        Ray ray = new Ray(controller.transform.position, controller.transform.forward);
        RaycastHit hit;
        GameObject target;
        string targetName;
        float width = OVRInput.Get(OVRInput.Button.PrimaryIndexTrigger) ? 2 : 1;
        if (Physics.Raycast(ray, out hit, 1000f))
        {
            controllerRay.transform.localScale = new Vector3(width, width, hit.distance);
            target = hit.collider.gameObject;
            targetName = target.name;
        }
        else
        {
            controllerRay.transform.localScale = new Vector3(width, width, 1000f);
            targetName = "None";
        }

        if (debug)
        {
            Vector2 axis = OVRInput.Get(OVRInput.Axis2D.PrimaryTouchpad);
            debugText.text = $"localPos={localPos}\nlocalRot={localRot}\ntarget={targetName}\naxis={axis}";
        }
    }
}