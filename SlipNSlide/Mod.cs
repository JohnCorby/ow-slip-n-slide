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
			var slide = ModHelper.Assets.GetAudio("slide.mp3");
			var slide2 = ModHelper.Assets.GetAudio("slide2.mp3");
			var bonk = ModHelper.Assets.GetAudio("bonk.mp3");
			var music = ModHelper.Assets.GetAudio("music.mp3");

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
				audioTable[(int)AudioType.MovementIceLSiding] = new AudioLibrary.AudioEntry(AudioType.MovementIceLSiding, new[] { slide, slide2 });

				audioTable[(int)AudioType.ImpactUnderwater] = new AudioLibrary.AudioEntry(AudioType.ImpactUnderwater, new[] { bonk });
				audioTable[(int)AudioType.ImpactLowSpeed] = new AudioLibrary.AudioEntry(AudioType.ImpactLowSpeed, new[] { bonk });
				audioTable[(int)AudioType.ImpactMediumSpeed] = new AudioLibrary.AudioEntry(AudioType.ImpactMediumSpeed, new[] { bonk });
				audioTable[(int)AudioType.ImpactHighSpeed] = new AudioLibrary.AudioEntry(AudioType.ImpactHighSpeed, new[] { bonk });
				audioTable[(int)AudioType.ShipImpact_NoDamage] = new AudioLibrary.AudioEntry(AudioType.ShipImpact_NoDamage, new[] { bonk });
				audioTable[(int)AudioType.ShipImpact_LightDamage] = new AudioLibrary.AudioEntry(AudioType.ShipImpact_LightDamage, new[] { bonk });
				audioTable[(int)AudioType.ShipImpact_MediumDamage] = new AudioLibrary.AudioEntry(AudioType.ShipImpact_MediumDamage, new[] { bonk });
				audioTable[(int)AudioType.ShipImpact_HeavyDamage] = new AudioLibrary.AudioEntry(AudioType.ShipImpact_HeavyDamage, new[] { bonk });

				audioTable[(int)AudioType.NomaiRuinsBaseTrack] = new AudioLibrary.AudioEntry(AudioType.NomaiRuinsBaseTrack, new[] { music });
				audioTable[(int)AudioType.DreamRuinsBaseTrack] = new AudioLibrary.AudioEntry(AudioType.DreamRuinsBaseTrack, new[] { music });

				ModHelper.Console.WriteLine("everything is ice");
			}, 10);
		};
	}
}
