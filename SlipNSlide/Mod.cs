using HarmonyLib;
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
		return;
		Locator.GetAudioManager()._audioLibraryDict[(int)AudioType.MovementIceLSiding] =
			new AudioLibrary.AudioEntry(AudioType.MovementIceLSiding, new[] { ModHelper.Assets.GetAudio("sound.mp3") });
	}

	[HarmonyPrefix, HarmonyPatch(typeof(PlayerCharacterController), nameof(PlayerCharacterController.IsSlidingOnIce))]
	private static bool PlayerCharacterController_IsSlidingOnIce(PlayerCharacterController __instance, out bool __result)
	{
		__result = __instance.IsGrounded() && __instance._groundCollider.material.dynamicFriction == 0f;
		// __result = __instance.IsGrounded() && __instance._groundSurface == SurfaceType.Ice && __instance._groundCollider.material.dynamicFriction == 0f;
		return false;
	}
}
