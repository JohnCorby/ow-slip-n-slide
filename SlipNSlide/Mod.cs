using HarmonyLib;
using OWML.ModHelper;
using System.Linq;
using UnityEngine;

namespace SlipNSlide;

[HarmonyPatch]
public class Mod : ModBehaviour
{
	private void Start()
	{
		LoadManager.OnCompleteSceneLoad += (_, _) =>
		{
			var sound = ModHelper.Assets.GetAudio("sound.mp3");

			ModHelper.Events.Unity.FireInNUpdates(() =>
			{
				// friction
				foreach (var collider in Resources.FindObjectsOfTypeAll<Collider>())
				{
					collider.material.dynamicFriction = 0;
				}

				// surface type
				var surfaceTable = Locator.GetSurfaceManager()._lookupTable;
				foreach (var kvp in surfaceTable.ToList())
				{
					surfaceTable[kvp.Key] = SurfaceType.Ice;
				}

				// audio
				var audioTable = Locator.GetAudioManager()._audioLibraryDict;
				foreach (var kvp in audioTable.ToList())
				{
					audioTable[kvp.Key] = new AudioLibrary.AudioEntry((AudioType)kvp.Key, new[] { sound });
				}

				ModHelper.Console.WriteLine("everything is ice");
			}, 10);
		};
	}
}
