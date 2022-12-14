using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
	public Camera lookAtCamera;

	public bool lookOnlyOnAwake;

	public void Start()
	{
		if (lookAtCamera == null)
		{
			lookAtCamera = Camera.main;
		}
		if (lookOnlyOnAwake)
		{
			CamLook();
		}
	}

	public void Update()
	{
		if (!lookOnlyOnAwake)
		{
			CamLook();
		}
	}

	public void CamLook()
	{
		base.transform.LookAt(lookAtCamera.transform);
	}
}
