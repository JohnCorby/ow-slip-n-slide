using HarmonyLib;
using OWML.Common;
using OWML.ModHelper;
using System.Reflection;

namespace SlipNSlide;

[HarmonyPatch]
public class Mod : ModBehaviour
{
	public Mod Instance;

	private void Awake()
	{
		Instance = this;
		Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly());
	}

	private void Start()
	{
		var sound = ModHelper.Assets.GetAudio("sound.mp3");
		// Locator.GetAudioManager().
	}
}
