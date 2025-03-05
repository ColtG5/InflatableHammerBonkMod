using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InflatableHammerBonkMod
{
	[BepInPlugin(modGUID, modName, modVersion)]
	public class InflatableHammerBonkMod : BaseUnityPlugin
	{
		private const string modGUID = "ColtG5.InflatableHammerBonk";
		private const string modName = "InflatableHammerBonk";
		private const string modVersion = "1.0.2";

		private readonly Harmony harmony = new Harmony(modGUID);

		internal static InflatableHammerBonkMod instance;
		internal ManualLogSource logger;

		private void Awake()
		{
			if (instance == null)
			{
				instance = this;
			}
			logger = BepInEx.Logging.Logger.CreateLogSource(modGUID);
			logger.LogInfo("InflatableHammerBonkMod loaded!");

			logger = Logger;
			harmony.PatchAll();
		}
	}
}
