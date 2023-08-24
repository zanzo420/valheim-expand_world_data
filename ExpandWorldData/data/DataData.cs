using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

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
  private static readonly string[] KnownKeys = [
    "TCData",
    "accTime",
    "addedDefaultItems",
    "aggravated",
    "alert",
    "alive_time",
    "ammo",
    "ammoType",
    "attachJoint",
    "author",
    "Bait",
    "bakeTimer",
    "baseValue",
    "BeardItem",
    "body_avel",
    "body_vel",
    "BodyVelocity",
    "catchID",
    "ChestItem",
    "Content",
    "crafterID",
    "crafterName",
    "creator",
    "creatorName",
    "data",
    "dataCount",
    "dead",
    "DebugFly",
    "DespawnInDay",
    "dodgeinv",
    "done",
    "drops",
    "durability",
    "eitr",
    "emote",
    "emoteID",
    "emote_oneshot",
    "enabled",
    "escape",
    "EventCreature",
    "follow",
    "forward",
    "fuel",
    "GrowStart",
    "HairColor",
    "HairItem",
    "HaveSaddle",
    "haveTarget",
    "health",
    "HelmetItem",
    "HitDir",
    "HitPoint",
    "hooked",
    "Hue",
    "huntplayer",
    "inBed",
    "InUse",
    "inWater",
    "InitVel",
    "IsBlocking",
    "item",
    "item0",
    "item1",
    "item2",
    "item3",
    "item4",
    "item5",
    "item6",
    "item7",
    "item8",
    "item9",
    "itemPrefab",
    "itemStack",
    "items",
    "landed",
    "lastAttack",
    "LastSpawn",
    "lastTime",
    "LeftBackItem",
    "LeftBackItemVariant",
    "LeftItem",
    "LeftItemVariant",
    "LegItem",
    "level",
    "LiquidData",
    "location",
    "LookTarget",
    "lovePoints",
    "max_health",
    "MaxInstances",
    "ModelIndex",
    "noise",
    "owner",
    "ownerName",
    "patrol",
    "patrolPoint",
    "permitted",
    "picked",
    "picked_time",
    "plantTime",
    "played",
    "playerID",
    "playerName",
    "plays",
    "pose",
    "pregnant",
    "product",
    "pvp",
    "quality",
    "quality0",
    "quality1",
    "quality2",
    "quality3",
    "quality4",
    "quality5",
    "quality6",
    "quality7",
    "quality8",
    "quality9",
    "queued",
    "RandomSkillFactor",
    "relPos",
    "relRot",
    "RightBackItem",
    "RightItem",
    "rodOwner",
    "rooms",
    "rudder",
    "Saturation",
    "scale",
    "scaleScalar",
    "seAttrib",
    "seed",
    "ShoulderItem",
    "ShoulderItemVariant",
    "ShownAlertMessage",
    "SkinColor",
    "sleeping",
    "SpawnAmount",
    "SpawnOre",
    "stack",
    "stamina",
    "StartTime",
    "state",
    "Stealth",
    "support",
    "tag",
    "tagauthor",
    "TameLastFeeding",
    "TameTimeLeft",
    "tamed",
    "TamedName",
    "TamedNameAuthor",
    "targets",
    "text",
    "tiltrot",
    "timeOfDeath",
    "triggered",
    "user",
    "UtilityItem",
    "Value",
    "variant",
    "variant0",
    "variant1",
    "variant2",
    "variant3",
    "variant4",
    "variant5",
    "variant6",
    "variant7",
    "variant8",
    "variant9",
    "vel",
    "wakeup",
    "WeaponLoaded",
    "lastWorldTime",
    "terrainModifierTimeCreated",
    "spawnpoint",
    "SpawnPoint",
    "spawntime",
    "SpawnTime",
    "spawn_time",
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

  private static readonly Dictionary<int, string> HashToKey = KnownKeys.ToDictionary(x => x.GetStableHashCode(), x => x);
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
