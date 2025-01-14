using PatheaScript;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Event = PatheaScript.Event;
using ItemAsset.PackageHelper;

namespace PatheaScriptExt
{
    public class ActionMove : Action
    {
        Pathea.Motion_Move mMove;

        Vector3 mLocalDst;
        float mRadius;
        float mSpeed;

        public override bool Parse()
        {
            if (mInfo.Name != "STMT")
            {
                Debug.LogError("error node:" + mInfo);
                return false;
            }

            VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);

            int id = (int)npcId.Value;
            Pathea.PeEntity entity = Pathea.EntityMgr.Instance.Get(id);
            if (null == entity)
            {
                Debug.LogError("can't find entity:"+id);
                return false;
            }

            mMove = entity.GetCmpt<Pathea.Motion_Move>();
            if (null == mMove)
            {
                Debug.LogError("can't find move cmpt:");
                return false;
            }

            return true;
        }

        protected override bool OnInit()
        {
            if (false == base.OnInit())
            {
                return false;
            }

            //mMove.MoveTo();

            //mNpc.SetAiActive(false);
            //mNpcAction = new AiNpcObject.NpcActionMove(mNpc.transform.TransformPoint(mLocalDst), mSpeed, mRadius);
            //mNpc.ActionRunner.RunAction(mNpcAction);
            return true;
        }

        protected override TickResult OnTick()
        {
            if (TickResult.Finished == base.OnTick())
            {
                return TickResult.Finished;
            }

            //if (mNpcAction.Stoped)
            //{
            //    Debug.Log(this + " Finished");
            //    return TickResult.Finished;
            //}

            return TickResult.Running;
        }

        protected override void OnReset()
        {
            base.OnReset();

            //mNpc.SetAiActive(true);
        }

        public override string ToString()
        {
            return string.Format("MoveNpc");
        }
    }

    public class ActionPlayerPosition : ActionImmediate
    {
        protected override bool Exec()
        {
            VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);
            VarRef varRef = PatheaScript.Util.GetVarRefOrValue(mInfo, "pos", VarValue.EType.Vector3, mTrigger);
            
            //TODO:need parse func

            List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            foreach (Pathea.PeEntity player in playerList)
            {
                Pathea.PeTrans v = player.GetCmpt<Pathea.PeTrans>();

                v.position = (Vector3)varRef.Value;
            }

            return true;
        }
    }

    public class ActionPlayerAnimation : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);
            //VarRef animationName = PatheaScript.Util.GetVarRefOrValue(mInfo, "animationName", VarValue.EType.String, mTrigger);

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //TODO:
            //}

            //Debug.Log("execute:"+this);
            return true;
        }

        public override string ToString()
        {
            return string.Format("ActionPlayerAnimation");
        }
    }

    public class ActionModifyPlayerItem : ActionImmediate
    {
        protected override bool Exec()
        {
            VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);
            VarRef itemIdVarRef = PeType.GetItemId(mInfo, mTrigger);
            VarRef count = PatheaScript.Util.GetVarRefOrValue(mInfo, "count", VarValue.EType.Int, mTrigger);
            Functor modify = mFactory.GetFunctor(mInfo, "modify");

            List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            int itemId = (int)itemIdVarRef.Value;

            foreach (Pathea.PeEntity player in playerList)
            {
                Pathea.PlayerPackageCmpt pkg = player.GetCmpt<Pathea.PlayerPackageCmpt>();
                ItemAsset.ItemPackage accessor = pkg.package._playerPak;

                int itemCount = accessor.GetCount(itemId);
                modify.Set(new Variable(itemCount), count.Var);
                modify.Do();

                int newItemCount = (int)(modify.Target.Value);

                if (newItemCount == itemCount)
                {
                    continue;
                }
                else if (newItemCount > itemCount)
                {
                    int dd = newItemCount - itemCount;


                    if (!accessor.Add(itemId, dd))
                    {
                        Debug.LogError("Add item:" + itemId + " to player:" + player + " failed.");
                    }
                    else
                    {
                        Debug.Log("Add item:" + itemId + " count:" + dd + " to player:" + player + " succeed.");
                    }
                }
                else
                {
                    int dd = itemCount - newItemCount;

                    accessor.Destroy(itemId, dd);

                    Debug.Log("remove item:" + itemId + " count:" + dd + "from player:" + player + " succeed.");
                }
            }

            return true;
        }

        public override string ToString()
        {
            return string.Format("ActionPlayerAnimation");
        }
    }

    public class ActionWait : Action
    {
        VarRef mTimeMs;
        float mBeginTime;

        public override bool Parse()
        {
            mTimeMs = PatheaScript.Util.GetVarRefOrValue(mInfo, "ms", VarValue.EType.Int, mTrigger);

            return true;
        }

        protected override bool OnInit()
        {
            if (false == base.OnInit())
            {
                return false;
            }

            mBeginTime = Time.time;
            return true;
        }

        protected override TickResult OnTick()
        {
            if (TickResult.Finished == base.OnTick())
            {
                return TickResult.Finished;
            }

            if ((Time.time - mBeginTime) * 1000 > (int)mTimeMs.Value)
            {
                return TickResult.Finished;
            }

            return TickResult.Running;
        }

        public override void Store(System.IO.BinaryWriter w)
        {
            base.Store(w);
            
            w.Write((float)(Time.time - mBeginTime));
        }
        public override void Restore(System.IO.BinaryReader r)
        {
            base.Restore(r);
            
            float elapedTime = r.ReadSingle();
            mBeginTime = Time.time - elapedTime;
        }
    }

    public class ActionStopWatch : ActionImmediate
    {
        protected override bool Exec()
        {
            VarRef timerName = PatheaScript.Util.GetVarRefOrValue(mInfo, "id", VarValue.EType.String, mTrigger);

            Functor initSecFunctor = mFactory.GetFunctor(mInfo, "set1");
            VarRef initSec = PatheaScript.Util.GetVarRefOrValue(mInfo, "sec", VarValue.EType.Float, mTrigger);

            Functor speedFunctor = mFactory.GetFunctor(mInfo, "set2");
            VarRef speed = PatheaScript.Util.GetVarRefOrValue(mInfo, "speed", VarValue.EType.Float, mTrigger);

            initSecFunctor.Set(new Variable(), initSec.Var);
            initSecFunctor.Do();

            speedFunctor.Set(new Variable(), speed.Var);
            speedFunctor.Do();

            PETimer timer = new PETimer();
            timer.Second = (double)((float)initSecFunctor.Target.Value);
            timer.ElapseSpeed = (float)speedFunctor.Target.Value;

            //PETimerMgr.Instance.Remove(timerName);
            
            PeTimerMgr.Instance.Add((string)timerName.Value, timer);
            return true;
        }
    }

    public class ActionKillStopWatch : ActionImmediate
    {
        protected override bool Exec()
        {
            VarRef timerName = PatheaScript.Util.GetVarRefOrValue(mInfo, "id", VarValue.EType.String, mTrigger);

            PeTimerMgr.Instance.Remove((string)timerName.Value);
            
            return true;
        }
    }

    public class ActionKillPlayer : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //player.ApplyHpChange(null, 100000000, 1, 0);
            //}
            return true;
        }
    }

    public class ActionKillNpc : ActionImmediate
    {
        protected override bool Exec()
        {
           // VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            //if (-1 == npcId.Value)
            //{
            //    List<AiNpcObject>.Enumerator enumerator = NpcManager.Instance.GetNpcEnumerator();
            //    while (enumerator.MoveNext())
            //    {
            //        AiNpcObject npc = enumerator.Current;

            //        if (npc == null)
            //        {
            //            continue;
            //        }

            //        npc.ApplyDamage(100000f);
            //    }
            //}
            //else
            //{
            //    AiNpcObject npc = NpcManager.Instance.GetNpc((int)npcId.Value);
            //    if (null != npc)
            //    {
            //        npc.ApplyDamage(100000f);
            //    }
            //}

            return true;
        }
    }

    public class ActionKillMonster : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef mosterId = PeType.GetMonsterId(mInfo, mTrigger);

            //Debug.Log("kill moster not implemented.");
            return true;
        }
    }

    public class ActionModifyPlayerStatusMax : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);
            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify player status max:"+statusType+" to "+functor.Target);
            //}
            return true;
        }
    }

    public class ActionModifyPlayerStatus : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);
            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)playerId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify player status:" + statusType + " to " + functor.Target);
            //}
            return true;
        }
    }

    public class ActionSetNpcStatus : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);

            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)npcId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify npc status:" + statusType + " to " + functor.Target);
            //}
            return true;
        }
    }

    public class ActionSetMonsterStatus : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef monsterId = PeType.GetMonsterId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);

            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)monsterId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify monster status:" + statusType + " to " + functor.Target);
            //}
            return true;
        }
    }

    public class ActionSetNpcStatusMax : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);

            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)npcId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify npc status max:" + statusType + " to " + functor.Target);
            //}
            return true;
        }
    }

    public class ActionSetMonsterStatusMax : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef monsterId = PeType.GetMonsterId(mInfo, mTrigger);

            //EStatus statusType = PeType.GetStatusType(mInfo);

            //Functor functor = mFactory.GetFunctor(mInfo, "set");
            //VarRef value = PatheaScript.Util.GetVarRefOrValue(mInfo, "value", VarValue.EType.Float, mTrigger);
            //functor.Set(new Variable(), value.Var);
            //functor.Do();

            //List<Pathea.PeEntity> playerList = PeType.GetPlayer((int)monsterId.Value);
            //foreach (Pathea.PeEntity player in playerList)
            //{
            //    //switch (statusType)
            //    //{
            //    //    case EPlayerStatus.Oxygen:
            //    //        //player.ApplyHpChange(null, 100000000, 1, 0);
            //    //        break;
            //    //}
            //    Debug.Log("not implement:modify monster status max:" + statusType + " to " + functor.Target);
            //}
            return true;
        }
    }

    public class ActionSetNpcFollowNpc : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            //VarRef npcIdFollow = PatheaScript.Util.GetVarRefOrValue(mInfo, "targetnpc", PatheaScript.VarValue.EType.Int, mTrigger);

            //Debug.Log("not implement:SetNpcFollowNpc");
            return true;
        }
    }

    public class ActionSetNpcFollowPlayer : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);

            //Debug.Log("not implement:SetNpcFollowPlayer");
            return true;
        }
    }

    public class ActionSetNpcAnimation : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            //VarRef playerId = PeType.GetPlayerId(mInfo, mTrigger);
            //string animation = PatheaScript.Util.GetString(mInfo, "animation");

            //Debug.Log("not implement:SetNpcAnimation");
            return true;
        }
    }

    public class ActionSetNpcAiSwitch : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            
            //bool state = PatheaScript.Util.GetBool(mInfo, "state");

            //Debug.Log("not implement:SetNpcAiSwitch");
            return true;
        }
    }

    public class ActionSetNpcEat : ActionImmediate
    {
        protected override bool Exec()
        {
            //VarRef npcId = PeType.GetNpcId(mInfo, mTrigger);
            //VarRef itemId = PeType.GetItemId(mInfo, mTrigger);

            //Debug.Log("not implement:SetNpcAiSwitch");
            return true;
        }
    }
}