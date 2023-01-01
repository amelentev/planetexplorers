using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class CharacterSex
{
    //玩家性别
    public const int FEMALE = 1;   //女性
    public const int MALE = 2;    //男性
}

public class MISSIONFlag
{
    public const int Mission_No = 0;    //
    public const int Mission_Follow = 1;    //跟随npc
    public const int Mission_Discovery = 2;   //探索npc
    public const int Mission_Dif = 3;   //塔防npc
}

public class ErrorMessage
{
    public const int NAME_HAS_EXISTED = 8000040;
}

public class MapIconID
{
    public const int ALIEN_BASE_NOT_OPENABLE = 18;
}
public class VArtifactTownConstant
{
    public const int NATIVE_TOWER_BUILDING_ID = -1;
}

public class PlantConst
{
    public const int DIRTY_TYPE0 = 19;
    public const int DIRTY_TYPE1 = 63;
}

public class BoxMapTypeInt
{
    public const int NEAR_TOWN = 1;
    public const int NEAR_CAMP = 2;
    public const int GRASSLAND = 3;
    public const int DESERT = 4;
    public const int REDSTONE = 5;
    public const int IN_WATER = 6;
    public const int IN_CAVE = 7;
}

public class RandomItemType
{
    public const int EQUIPMENT = 0;
    public const int TOOL = 1;
    public const int SCRIPT = 2;
    public const int CONSUMABLE = 3;
}

public class ArtifactTownConst
{
	public const int HEIGHT_DIFF_CHANGE = 8;//8;
    public const int HEIGHT_DIFF_NONE = 15;
	public const int DEPTH_IN_TERRAIN = 4;//4:--to do this may change to 6
	public const int HEIGHT_COMPUTE_RADIUS = 15;
}
public class ForceConstant
{
    public const int  NEUTRAL = 0;
    public const int  PLAYER = 1;
    public const int  EARTH_NPC = 2;
    public const int  MARTIAN_NPC = 3;
    public const int  MONSTER = 4;
    public const int  PUJA = 5;
    public const int  PAJA = 6;
    public const int  ALIEN = 7;
    public const int  TOWER_DEFENCE = 8;
    public const int  PLAYER_TOWER = 9;
    public const int  CREATION =99;
}


public class TradeTalkConst
{
    public const int DEFAULT_00 = 82201056;//Got something to trade?
    public const int DEFAULT_01 = 82201057;//Got anything exciting and new?
    public const int DEFAULT_02 = 82201058;//I've got a ton of items, take a look!
    public const int UNFAIR = 82201054;
    public const int NO_PACKAGE_ROOM = 82209001;
    public const int GOOD_TRADE = 82201053;
    public const int THANK_YOU = 82201055;
}


#region colony
public class CSTrainMsgID
{
	public const int SAME_OR_MORE_SKILL = 82209014;
	public const int START_TRAINING = 8000896;
}
public class ColonyErrorMsg
{
    public const int TOO_CLOSE_TO_NATIVE_CAMP0 = 82209010;
    public const int TOO_CLOSE_TO_NATIVE_CAMP1 = 82209009;
    public const int TOO_CLOSE_TO_NATIVE_CAMP2 = 82209008;
//	public const int AREA_UNAVAILABLE = 82209004;//--to do
}
public class ColonyNameID
{
	public const int ASSEMBLY = 82210001;//核心
	public const int ENGINEER = 82210009; //工程
	public const int ENHANCE = 82210003;
	public const int REPAIR = 82210004;
	public const int RECYCLE = 82210005;
	public const int DWELLING = 82210006;
	public const int FACTORY = 82210010;
	public const int PPCOAL = 82210007;//发电站
	public const int FARM = 82210008;//农场
	public const int STORAGE = 82210002;//仓库
    public const int TRADE_POST=82210011;//贸易站
	public const int PROCESSING_FACILITY=82210012;//采集场
	public const int TRAINING_CENTER=82210013;//训练所
	public const int MEDICAL_DETECTOR=82210014;//检查仪
	public const int MEDICAL_LAB=82210015;//治疗仪
	public const int QUARANTINE_TENT=82210016;//隔离帐篷
	public const int PPFUSION=82210017;//核电站
}public class ProcessingConst
{
	public const int TASK_NUM = 4;
	public const int WORKER_AMOUNT_MAX = 5;
	public const int WORKER_MAX = 4;
	public const int OBJ_MAX = 12;
	public const int NEED_HUMAN = 82201059;//Need a human processor to process
	public const string RESULT_MODEL_PATH = "Prefab/RandomItems/item_drop";//"Prefab/Other/ItemBox";//Prefab/RandomItems/random_box01";
	public const int INFORM_FINISH_TO_STORAGE = 82201067;
	public const int INFORM_FINISH_TO_RANDOMITEM = 82201068;
    public const int NotHaveCollect = 82201097;
}

public class RecycleConst
{
    public const int INFORM_FINISH_TO_PACKAGE = 82201094;
    public const int INFORM_FINISH_TO_STORAGE = 82201095;
    public const int INFORM_FINISH_TO_RANDOMITEM = 82201096;
}

public class ColonyConst{
	public const int FACTORY_COMPOUND_GRID_COUNT= 168;
	public const int START_MONEY =5000;
	public const float TRADE_POST_CHARGE_RATE =0.15f;
}

public class AutoCycleTips
{
	public const int PROCESS_FOR_RESOURCE = 82201060;
	public const int PROCESS_FOR_STORAGE = 82201062;
	public const int REPLICATE_FOR = 82201064;
	public const int STORAGE_FULL = 82201066;
	public const int FACTORY_TO_STORAGE = 82201083;
	public const int MEDICINE_SUPPLY = 82201087;
}

public class ColonyMessage{
	//enhance
	public const int CANNOT_ENHANCE_ITEM = 8000180;
	public const int CANNOT_ENHANCE_MORE = 8000181;
	//recyle
	public const int Recycle_End = 82200056;
	//npc
	public const int OCCUPATION_NPC_TOO_MANY = 82201069;
	public const int CANNOT_WORK_HasAnyQuest = 82201070;
	public const int CANNOT_WORK_IsNeedMedicine = 82201071;
	public const int CANNOT_WORK_IsHunger = 82201072;
	public const int CANNOT_WORK_HpLow = 82201092;
	public const int CANNOT_WORK_IsUncomfortable = 82201073;
	public const int CANNOT_WORK_IsInSleepTime = 82201074;
	public const int CANNOT_WORK_IsInDinnerTime = 82201075;
	public const int CANNOT_WORK_None = 82201093;

	//tradePost
	public const int TRADE_POST_NO_MONEY  = 82201075;//--TO DO:"The TradePost has no money to pay for you!"
}

public class ColonyStatusWarning{
	public const int PPCOAL_RUNNING_LOW = 8000530;
	public const int DURABILITY_LOW = 8000531;
	public const int PPFUSION_RUNNING_LOW = 9500408;
}

public class ColonyNoMgrMachine{
	public const int DOODAD_ID_REPAIR = 63;
	public const int DOODAD_ID_SOLARPOWER = 64;

}
#endregion
public class FecesModelPath
{
	public const string PATH_01 = "AiPrefab/MonsterSpecialItem/Monster_feces01";
	public const string PATH_02 = "AiPrefab/MonsterSpecialItem/Monster_feces02";
	public const string PATH_03 = "AiPrefab/MonsterSpecialItem/Monster_feces03";
}



public class EffectId {
    public const int PICK_FECES = 220;
}

public class TradeCampId{
	public const int MISSION_NATIVE = 31;
}

public class SleepTime
{
    public const int MinHours = 1;
    public const int MaxHours = 26;
    public const int NormalHours = 16;
}
#region dungeon
public class DungeonConstants{
	public const int TASK_LEVEL_START = 100;
}

public class DungenMessage{
	public const int ENTER_DUNGEN = 8000534;
	public const int EXIT_DUNGEN = 8000186;
	public const int TASK_ENTER_DUNGEN = 8000713;//--to do
}

public class DunItemId{
	public const int UNFINISHED_ISO = 281;
}
public class DungeonMonster{
	public const int CAMP_ID = 26;
	public const int DAMAGE_ID = 26;
}

public class IsoTags{
	public const string Creation = "Creation";//0
	public const string Equipment ="Equipment";//1
	public const string Sword ="Sword";//2
	public const string Axe ="Axe";//3
	public const string Bow ="Bow";//4
	public const string Shield ="Shield";//5
	public const string Gun ="Gun";//6
	public const string Carrier ="Carrier";//7
	public const string Vehicle ="Vehicle";//8
	public const string Ship ="Ship";//9
	public const string Aircraft ="Aircraft";//10
	public const string Armor ="Armor";//11
	public const string Head ="Head";//12
	public const string Body ="Body";//13
	public const string ArmAndLeg ="Arm And Leg";//14
	public const string HeadAndFoot ="Head And Foot";//15
	public const string Decoration ="Decoration";//16
	public const string Robot ="Robot";//17
	public const string AITurret ="AI Turret";//18
	public const string ObjectItem ="Object";//19
}
#endregion

#region ally
public class AdventureAlly{
	public const int AllyIDMax = 7;
	public const int DefaultPlayerId = 1;
	public const int PujaStartPlayerId = 20;
	public const int PajaStartPlayerId = 30;
	public const int EnemyNpcStartPlayerId = 40;
}

public class LabelColor{
	public const int WHITE =0;
	public const int ORANGE = 1;
	public const int BLUE = 2;
	public const int PURPLE = 3;
	public const int CYAN = 4;
	public const int GREEN = 5;
	public const int YELLOW = 6;
	public const int RED = 7;
}

public class AllyColor {

    //lz-2016.08.31 这个颜色主要是联盟要用
    public static Color32[] AllianceCols = new Color32[]
    { new Color32(255,255,255,255),//白色
      new Color32(255,175,95,255), //橙色   ----
      new Color32(88,119,233,255), //蓝色    
      new Color32(236,143,255,255),//紫色   ---
      new Color32(154,255,255,255),//淡蓝  
      new Color32(135,240,136,255),//绿色    ----
      new Color32(255,255,132,255),//黄色
      new Color32(255,137,137,255),//红色 
    };
}

public class AllyIcon
{
    public static string[] HummanIcon = new string[]
    {
        "Alliance_Human_1",
        "Alliance_Human_2",
        "Alliance_Human_3",
        "Alliance_Human_4",
        "Alliance_Human_5",
        "Alliance_Human_6"
    };

   public static string[] PajaIcon = new string[]
   {
        "Alliance_Paja_1",
        "Alliance_Paja_2",
        "Alliance_Paja_3",
        "Alliance_Paja_4",
        "Alliance_Paja_5",
   };

   public static string[] PujaIcon = new string[]
   {
        "Alliance_Puja_1",
        "Alliance_Puja_2",
        "Alliance_Puja_3",
        "Alliance_Puja_4",
        "Alliance_Puja_5",
   };
}

public class AllyConstants{
	public const int EnemyNpcIdAddNum = 9900;
	public const int EnemyNpcCampId = 8;
	public const int EnemyNpcDamageId = 8;
	public const int PujaCampId = 15;
	public const int PujaDamageId = 15;
	public const int PajaCampId=18;
	public const int PajaDamageId=18;
}
#endregion 

#region MissionMapIconColor
public class MissionMapLabelColor
{
    //lz-2016.10.11 主线任务颜色（包括可接和完成）
    public static Color32 MainLineCol = new Color32(236, 143, 255, 255);
    //lz-2016.10.11 支线任务颜色（包括可接和完成）
    public static Color32 SideLineCol = new Color32(99, 202, 253, 255);
    //lz-2016.10.11 任务位置范围颜色
    public static Color32 MissionTargetCol = new Color32(255, 255, 255, 255);
    //lz-2016.10.11 未完成的任务颜色（包括主线和支线）
    public static Color32 UnFinishedCol = new Color32(125,125,125,255);
}
#endregion
