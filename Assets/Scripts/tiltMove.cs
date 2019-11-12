using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tiltMove : MonoBehaviour
{
   // Move object using accelerometer
   public float speed = 10.0f;
   public float rotSpeed = 60f;

   Transform body;

   Vector3 calibratedDir;

   void Start() {
       body = this.transform.GetChild(0);
       Calibrate();
   }

   void Update() {
       Vector3 dir = Vector3.zero;

       // we assume that device is held parallel to the ground
       // and Home button is in the left hand

       // remap device acceleration axis to game cords
       // 1) XY plane of the device is mapped onto XZ plane
       // 2) rotated 90 degrees around Y axis
   }

   void Calibrate() {
       calibratedDir.x = Input.acceleration.x - calibratedDir.x;      // x to x
       calibratedDir.z = Input.acceleration.y - calibratedDir.z;      // y to z
       Debut.Log("Calibrated Dir = " + calibratedDir);

       // clamp acceleration vector to unit sphere
       if(dir.sqrMagnitude > 1) {
           dir.Normalize();
       }

   }
}

