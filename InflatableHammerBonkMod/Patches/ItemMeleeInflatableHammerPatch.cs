using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace InflatableHammerBonkMod.Patches
{
	[HarmonyPatch(typeof(ItemMelee))]
	class ItemMeleeInflatableHammerPatch
	{
		private static InflatableHammerBonkMod Instance => InflatableHammerBonkMod.instance;
		private static ManualLogSource Logger => Instance.logger;

		internal static List<AudioClip> newHitSound;
		internal static AssetBundle Bundle;

		[HarmonyPatch("Start")]
		[HarmonyPostfix]
		static void PatchItemMeleeStart(ItemMelee __instance)
		{
			if (__instance.gameObject.name.Contains("nflatable"))
			{
				string folderPath = Instance.Info.Location;
				folderPath = folderPath.TrimEnd("InflatableHammerBonkMod.dll".ToCharArray());
				Bundle = AssetBundle.LoadFromFile(folderPath + "bonk");

				if (Bundle != null)
				{
					newHitSound = Bundle.LoadAllAssets<AudioClip>().ToList();
					Logger.LogInfo("Asset bundle loaded!");
				}
				else
				{
					Logger.LogError("Failed to load asset bundle!");
				}

				__instance.soundHit.Sounds = newHitSound.ToArray();
				__instance.soundHit.Volume = 2.0f;
			}
		}

	}
}
