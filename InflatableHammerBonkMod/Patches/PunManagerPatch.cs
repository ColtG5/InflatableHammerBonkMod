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
	[HarmonyPatch(typeof(PunManager))]
	class PunManagerPatch
    {
		private static InflatableHammerBonkMod Instance => InflatableHammerBonkMod.instance;
		private static ManualLogSource Logger => Instance.logger;

		[HarmonyPatch("TruckPopulateItemVolumes")]
		[HarmonyPrefix]
		static void patchTruckPopulateItemVolumes(PunManager __instance)
		{
			var purchasedItemsRef = AccessTools.FieldRefAccess<ItemManager, List<Item>>("purchasedItems");
			List<Item> purchasedItems = purchasedItemsRef(ItemManager.instance);

			Item hammerItem = null;
			Item baseballBatItem = null;

			Item[] allItems = Resources.LoadAll<Item>("Items");

			foreach (Item item in allItems)
			{
				Logger.LogInfo("Item name: " + item.name);

				if (item.name.Contains("aseball"))
				{
					baseballBatItem = item;
				}

				//Logger.LogInfo("Item " + i);
				if (item.name.Contains("nflatable"))
				{
					hammerItem = item;
				}
			}

			if (hammerItem != null)
			{
				purchasedItems.Add(hammerItem);
				Logger.LogInfo("Successfully added " + hammerItem.name + " to the shop.");
			}
			else
			{
				Logger.LogError("Failed to find 'ItemMeleeInflatableHammer' in Resources!");
			}

			if (baseballBatItem != null)
			{
				purchasedItems.Add(baseballBatItem);
				Logger.LogInfo("Successfully added " + baseballBatItem.name + " to the shop.");
			}
			else
			{
				Logger.LogError("Failed to find 'ItemMeleeBaseballBat' in Resources!");
			}

			// log item
			//Logger.LogInfo("New item name: " + newItem.name);
			//Logger.LogInfo("New item: " + newItem);

			//// check if item got added to purchased items
			//Logger.LogInfo("Purchased items as stored after adding inflatable hammer: ");
			//foreach (Item item in purchasedItems)
			//{
			//	Logger.LogInfo("Purchased item name: " + item.name);
			//	Logger.LogInfo("Purchased item: " + item);
			//}
		}

	}
}
