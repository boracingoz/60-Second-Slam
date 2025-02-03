using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
   [SerializeField] private GameObject _platform;
        
        void Update()
        {
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (_platform.transform.position.x > -7.3)
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x - .3f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                if (_platform.transform.position.x < 7.3)
                _platform.transform.position = Vector3.Lerp(_platform.transform.position, new Vector3(_platform.transform.position.x + .3f, _platform.transform.position.y, _platform.transform.position.z), 0.50f);
            }
        }
}
