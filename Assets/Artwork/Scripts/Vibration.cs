using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibration : MonoBehaviour {

    public static Vibration instance;
	private Vector3 originPosition;
	private Quaternion originRotation;

	public float shake_decay = 0.002f;
	public float shake_intensity = .3f;
	public bool isShaking = false;
	private float temp_shake_intensity = 0;
	

	public void Update (){
		if (temp_shake_intensity > 0 && isShaking == true){
			transform.position = originPosition + Random.insideUnitSphere * temp_shake_intensity;
			transform.rotation = new Quaternion(
				originRotation.x + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .5f,
				originRotation.y + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .5f,
				originRotation.z + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .5f,
				originRotation.w + Random.Range (-temp_shake_intensity,temp_shake_intensity) * .5f);
			temp_shake_intensity -= shake_decay;
		}
        else
        {
            isShaking = false;
        }
	}
	
	public void Shake(){
		if (!isShaking)
        {
            isShaking = true;
            originPosition = transform.position;
            originRotation = transform.rotation;
            temp_shake_intensity = shake_intensity;
        }
	}
}
