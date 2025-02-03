using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPowerL : MonoBehaviour
{
    [SerializeField] private float _angle;
    [SerializeField] private float _power;

    void OnCollisionEnter(Collision other)
    {
        other.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(_angle,90,0) * _power, ForceMode.Force);
    }


}
