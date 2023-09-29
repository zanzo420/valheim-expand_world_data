using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using BepInEx.Bootstrap;
using UnityEngine;

namespace ExpandWorldData;

public class DataData
{
  public string name = "";
  [DefaultValue(null)]
  public string[]? ints = null;
  [DefaultValue(null)]
  public string[]? floats = null;
  [DefaultValue(null)]
  public string[]? strings = null;
  [DefaultValue(null)]
  public string[]? longs = null;
  [DefaultValue(null)]
  public string[]? vecs = null;
  [DefaultValue(null)]
  public string[]? quats = null;
  [DefaultValue(null)]
  public string[]? bytes = null;
}

public class DefaultData
{
  private static string[]? knownKeys;
  private static string[] KnownKeys => knownKeys ??= GenerateKeys();
  private static string[] GenerateKeys()
  {
    List<string> keys = [.. StaticKeys];
    List<Assembly> assemblies = [Assembly.GetAssembly(typeof(ZNetView)), .. Chainloader.PluginInfos.Values.Where(p => p.Instance != null).Select(p => p.Instance.GetType().Assembly)];
    var assembly = Assembly.GetAssembly(typeof(ZNetView));
    var baseType = typeof(MonoBehaviour);
    keys.AddRange(assemblies.SelectMany(s =>
    {
      try
      {
        return s.GetTypes();
      }
      catch (ReflectionTypeLoadException e)
      {
        return e.Types.Where(t => t != null);
      }
    }).Where(baseType.IsAssignableFrom).Select(t => $"HasFields{t.Name}"));

    keys.AddRange(typeof(ZDOVars).GetFields(BindingFlags.Static | BindingFlags.Public).Select(f => f.Name.Replace("s_", "")));
    for (var i = 0; i < 10; i++)
    {
      keys.Add($"item{i}");
      keys.Add($"quality{i}");
      keys.Add($"variant{i}");
    }
    return [.. keys.Distinct()];
  }
  private static readonly string[] StaticKeys = [
    "HasFields",
    "user_u",
    "user_i",
    "RodOwner_u",
    "RodOwner_i",
    "CatchID_u",
    "CatchID_i",
    "target_u",
    "target_i",
    "spawn_id_u",
    "spawn_id_i",
    "parent_id_u",
    "parent_id_i",
    // CLLC mod
    "CL&LC effect",
    // Structure / Spawner Tweaks
    "override_amount",
    "override_attacks",
    "override_biome",
    "override_boss",
    "override_collision",
    "override_compendium",
    "override_component",
    "override_conversion",
    "override_cover_offset",
    "override_data",
    "override_delay",
    "override_destroy",
    "override_discover",
    "override_dungeon_enter_hover",
    "override_dungeon_enter_text",
    "override_dungeon_exit_hover",
    "override_dungeon_exit_text",
    "override_dungeon_weather",
    "override_effect",
    "override_event",
    "override_faction",
    "override_fall",
    "override_fuel",
    "override_fuel_effect",
    "override_globalkey",
    "override_growth",
    "override_health",
    "override_input_effect",
    "override_interact",
    "override_item",
    "override_item_offset",
    "override_item_stand_prefix",
    "override_item_stand_range",
    "override_items",
    "override_level_chance",
    "override_maximum_amount",
    "override_maximum_cover",
    "override_maximum_fuel",
    "override_maximum_level",
    "override_max_near",
    "override_max_total",
    "override_minimum_amount",
    "override_minimum_level",
    "override_name",
    "override_near_radius",
    "override_output_effect",
    "override_pickable_spawn",
    "override_pickable_respawn",
    "override_render",
    "override_resistances",
    "override_respawn",
    "override_restrict",
    "override_smoke",
    "override_spawn",
    "override_spawn_condition",
    "override_spawn_effect",
    "override_spawn_max_y",
    "override_spawn_offset",
    "override_spawn_radius",
    "override_spawnarea_spawn",
    "override_spawnarea_respawn",
    "override_spawn_item",
    "override_start_effect",
    "override_text",
    "override_text_biome",
    "override_text_check",
    "override_text_extract",
    "override_text_happy",
    "override_text_sleep",
    "override_text_space",
    "override_topic",
    "override_trigger_distance",
    "override_trigger_noise",
    "override_unlock",
    "override_use_effect",
    "override_water",
    "override_wear",
    "override_weather",
    // Marketplace
    "KGmarketNPC",
    "KGnpcProfile",
    "KGnpcModelOverride",
    "KGnpcNameOverride",
    "KGnpcDialogue",
    "KGleftItem",
    "KGrightItem",
    "KGhelmetItem",
    "KGchestItem",
    "KGlegsItem",
    "KGcapeItem",
    "KGhairItem",
    "KGhairItemColor",
    "KGLeftItemBack",
    "KGRightItemBack",
    "KGinteractAnimation",
    "KGgreetingAnimation",
    "KGbyeAnimation",
    "KGgreetingText",
    "KGbyeText",
    "KGskinColor",
    "KGcraftingAnimation",
    "KGbeardItem",
    "KGbeardColor",
    "KGinteractSound",
    "KGtextSize",
    "KGtextHeight",
    "KGperiodicAnimation",
    "KGperiodicAnimationTime",
    "KGperiodicSound",
    "KGperiodicSoundTime",
    "KGnpcScale",
  ];

  private static Dictionary<int, string>? hashToKey;
  private static Dictionary<int, string> HashToKey => hashToKey ??= KnownKeys.ToDictionary(x => x.GetStableHashCode(), x => x);
  public static string Convert(int hash) => HashToKey.TryGetValue(hash, out var key) ? key : hash.ToString();
  public static DataData[] Data = [
    new()
    {
      name = "infinite_health",
      floats = ["health, 1E30"]
    },
    new()
    {
      name = "default_health",
      floats = ["health, 0"]
    },
    new()
    {
      name = "st_healthy",
      floats = ["health, 1E30"],
      ints = ["override_wear, 0"]
    },
    new()
    {
      name = "st_damaged",
      floats = ["health, 1E30"],
      ints = ["override_wear, 1"]
    },
    new()
    {
      name = "st_broken",
      floats = ["health, 1E30"],
      ints = ["override_wear, 3"]
    }
  ];
}
