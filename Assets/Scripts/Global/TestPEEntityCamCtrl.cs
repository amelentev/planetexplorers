using UnityEngine;

public class TestPEEntityCamCtrl : MonoBehaviour
{
	private static TestPEEntityCamCtrl m_Instance;

	private CameraThirdPerson m_Normal;

	private CameraThirdPerson m_ShootMode;

	private CamController m_CamCtrl;

	public static TestPEEntityCamCtrl Instance
	{
		get
		{
			return m_Instance;
		}
	}

	private CamController camCtrl
	{
		get
		{
			if (null == m_CamCtrl)
			{
				m_CamCtrl = GetComponent<CamController>();
			}
			return m_CamCtrl;
		}
	}

	private void Awake()
	{
		m_Instance = this;
		QualitySettings.vSyncCount = 1;
	}

	public void InitCam(Transform followTarget, Transform bone, string camString)
	{
		if (null != camCtrl)
		{
			m_Normal = (camCtrl.PushMode(camString) as CameraThirdPerson);
			m_Normal.m_Character = followTarget;
			m_Normal.m_Bone = bone;
			CameraSpringEffect cameraSpringEffect = camCtrl.AddEffect("Spring Effect") as CameraSpringEffect;
			cameraSpringEffect.m_Character = followTarget;
			cameraSpringEffect.m_Bone = bone;
			camCtrl.AddEffect("Breathe Effect");
		}
	}

	public void SetCamMode(Transform followTarget, Transform bone, string camString)
	{
	}

	public Camera GetCam()
	{
		if (null != camCtrl)
		{
			return camCtrl.m_TargetCam;
		}
		return null;
	}
}
