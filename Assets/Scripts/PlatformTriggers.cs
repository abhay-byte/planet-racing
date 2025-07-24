using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformTriggers : MonoBehaviour
{
    [SerializeField] private enum triggersTypes { Accelerate, Retardate, BlowUp};
    [SerializeField] private triggersTypes currentTrigger;
    [SerializeField] private MeshRenderer triggerMesh;

    private void Start()
    {
       
    }
    private void OnTriggerEnter(Collider other)
    {
        triggerMesh.material.EnableKeyword("_EMISSION");
    }
    private void OnTriggerExit(Collider other)
    {
        triggerMesh.material.DisableKeyword("_EMISSION");
    }
    private void OnTriggerStay(Collider other)
    {
        if(currentTrigger == triggersTypes.Accelerate)
        {
            Accelerate(other);
        }

        if (currentTrigger == triggersTypes.Retardate)
        {
            Retardate(other);
        }
        if(currentTrigger == triggersTypes.BlowUp)
        {
            BlowUp(other);
        }
    }
    private void Accelerate(Collider body)
    {
        Vector3 speed = body.gameObject.GetComponent<CarComponent>().carObject.GetComponent<Rigidbody>().velocity;
        body.gameObject.GetComponent<CarComponent>().carObject.GetComponent<Rigidbody>().velocity
            = new Vector3(speed.x + 0.1f, speed.y + 0.1f, speed.z + 0.1f) ;
    }
    private void Retardate(Collider body)
    {
        Vector3 speed = body.gameObject.GetComponent<CarComponent>().carObject.GetComponent<Rigidbody>().velocity;
        body.gameObject.GetComponent<CarComponent>().carObject.GetComponent<Rigidbody>().velocity =
            new Vector3(speed.x - 0.1f, speed.y - 0.1f, speed.z - 0.1f) ;
    }

    private void BlowUp(Collider body)
    {
        body.gameObject.GetComponent<CarComponent>().carObject.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0,50f,0),ForceMode.Acceleration);
    }
}
