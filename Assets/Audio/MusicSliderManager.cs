using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Audio
{
	public class MusicSliderManager : MonoBehaviour
	{

		public EventInstance musicInstance;
		public EventInstance menuInstance;
		public EventReference gameplayMusicEvent;
		public EventReference menuMusicEvent;
		[SerializeField] public Slider playerHealth;
		[SerializeField] public TMP_Dropdown MusicTrackDropdown;

		private void Start()
		{
			musicInstance = RuntimeManager.CreateInstance(gameplayMusicEvent);
			menuInstance = RuntimeManager.CreateInstance(menuMusicEvent);
			
			menuInstance.start();
			menuInstance.setPaused(true);
			
			musicInstance.start();
			MusicTrackDropdown.onValueChanged.AddListener(ChangeMusicTrack);
		}

		private void ChangeMusicTrack(int arg0)
		{
			if (arg0 == 0)
			{
				menuInstance.setPaused(true);
				musicInstance.setPaused(false);
				playerHealth.interactable = true;
			}
			else
			{
				musicInstance.setPaused(true);
				menuInstance.setPaused(false);
				playerHealth.interactable = false;
			}
		}


		private void Update()
		{
			musicInstance.setParameterByName("PlayerHp",Mathf.Clamp01(1 - playerHealth.value));
		}
	}
}