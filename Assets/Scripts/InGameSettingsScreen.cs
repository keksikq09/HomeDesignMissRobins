using GGMatch3;
using UnityEngine;
using UnityEngine.UI;

public class InGameSettingsScreen : MonoBehaviour
{
	[SerializeField]
	private Image testerImage;

	[SerializeField]
	private int tries = 10;

	[SerializeField]
	private float waitSeconds = 1f;

	private float lastClickTime;

	private int clicks;

	private void OnEnable()
	{
		GGSoundSystem.Play(GGSoundSystem.SFXType.FlyIn);
		Init();
	}

	private void Init()
	{
		float alpha = GGPlayerSettings.instance.Model.isTestUser ? 1 : 0;
		GGUtil.SetAlpha(testerImage, alpha);
	}

	public void ButtonCallback_OnExit()
	{
		NavigationManager.instance.Pop();
		GGSoundSystem.Play(GGSoundSystem.SFXType.CancelPress);
	}

	public void ButtonCallback_OnRate()
	{
		GGSupportMenu.instance.showRateApp(ConfigBase.instance.platformRateProvider);
		GGSoundSystem.Play(GGSoundSystem.SFXType.ButtonPress);
	}

	public void ButtonCallback_OnTesterClick()
	{
		float num = Time.unscaledTime - lastClickTime;
		lastClickTime = Time.unscaledTime;
		if (num > waitSeconds)
		{
			clicks = 0;
		}
		clicks++;
		if (clicks > tries)
		{
			GGPlayerSettings instance = GGPlayerSettings.instance;
			instance.Model.isTestUser = !instance.Model.isTestUser;
			instance.Save();
			clicks = 0;
			Init();
		}
	}

	public void ButtonCallback_OnTermsOfService()
	{
		Application.OpenURL("http://www.giraffe-games.com/terms-of-use/");
		GGSoundSystem.Play(GGSoundSystem.SFXType.ButtonPress);
	}

	public void ButtonCallback_PrivacyPolicy()
	{
		Application.OpenURL("http://www.giraffe-games.com/privacy-policy/");
		GGSoundSystem.Play(GGSoundSystem.SFXType.ButtonPress);
	}
}
