using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItemAsset
{
    public class ProtoTypeId
    {
        #region 道具ID
        //public const int REVIVE_SHOT = 31200001;  //复活枪
		//public const int MUTILPLE_STARTING_ITEM = 82000013;  //多人模式初始物品
		public const int ARROW = 49;
		public const int BULLET = 50;
		public const int BATTERY = 228;
		public const int HERBAL_JUICE = 916;
		
		public const int PUREE = 387;
		public const int RICE = 401;
		public const int NUTS = 388;
		public const int HEAT_PACK = 1582;


		public const int COMFORT_INJECTION_1 = 1479;
			
		public const int COAL= 983;

        public const int ASSEMBLY_CORE= 1127;
        public const int PPCoal = 1128;
        public const int STORAGE = 1129;
        public const int REPAIR_MACHINE = 1130;
        public const int DWELLING_BED = 1131;
        public const int ENHANCE_MACHINE = 1132;
        public const int RECYCLE_MACHINE = 1133;
        public const int FARM = 1134;
        public const int FACTORY_REPLICATOR = 1135;
        public const int PROCESSING = 1356;
        public const int TRADE_POST = 1357;
        public const int TRAINING_CENTER = 1423;
        public const int MEDICAL_CHECK = 1424;
        public const int MEDICAL_TREAT = 1422;
        public const int MEDICAL_TENT = 1421;
		public const int PPFusion = 1558;
		public const int COLONY_FAMILY = 4007;

        //lw:添加火把和木炭，基地自动回收到仓库
        public const int TORCH = 10;
        public const int CHARCOAL = 1001;
        public const int FLOUR = 1024;

        #endregion


        #region 装备ID
        //public const int F_ISSUED_OVERALLS = 20210001;    //女性初始工装衣
        //public const int F_ISSUED_PANTS = 20310001;   //女性初始工装裤
        //public const int F_ISSUED_SHOES = 20610001;   //女性初始工装鞋

        //public const int M_ISSUED_OVERALLS = 20220001;    //男性初始工装衣
        //public const int M_ISSUED_PANTS = 20320001;   //男性初始工装裤
        //public const int M_ISSUED_SHOES = 20620001;   //男性初始工装鞋
        #endregion


        #region 资源ID
        //public const int MEAT = 30000000;//肉 货币
        public const int WATER = 1003;//水
        public const int INSECTICIDE = 1002;//杀虫剂
        #endregion

    }

    public class ItemTabIndex
    {
        //物品类型
        public const int ITEM = 0;       //道具类物品
        public const int EQUIPMENT = 1;   //装备类物品
        public const int RESOURCE = 2;    //资源类物品
    }

    public class EquipmentIndex //装备栏索引
    {
        public const int HAT = 0;     //头部
        public const int COAT = 1;    //上装
        public const int GLOVE = 2;   //手套
        public const int RING = 3;    //戒指
        public const int MAINHAND = 4;    //主手
        public const int NECKLACE = 5;    //项链
        public const int TROUSERS = 6;    //下装
        public const int FOOT = 7;    //鞋子
        public const int BACK = 8;    //背部
        public const int OTHERHAND = 9;   //副手
    }
}
