using PatheaScript;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;
using Event = PatheaScript.Event;

namespace PatheaScriptExt
{
    public class EFrameProxy : EventProxy
    {
        public override void Tick()
        {
            Emit();
        }
    }

    public class EFrame : Event
    {
        VarRef mT;
        VarRef mQ;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            mT = PatheaScript.Util.GetVarRefOrValue(mInfo, "t", VarValue.EType.Int, mTrigger);
            mQ = PatheaScript.Util.GetVarRefOrValue(mInfo, "q", VarValue.EType.Int, mTrigger);

            return true;
        }

        public override bool Filter(params object[] param)
        {
            if (mQ.Value == Time.frameCount  % mT.Value)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("Event[Frame:mod{0}={1}]", mT, mQ);
        }
    }

    public class EPlayerTalkToNpcProxy : EventProxy
    {
        public override bool Subscribe()
        {
            //List<AiNpcObject>.Enumerator iter = NpcManager.Instance.GetNpcEnumerator();

            //while (iter.MoveNext())
            //{
            //    AiNpcObject npc = iter.Current;
            //    npc.MouseCtrl.MouseEvent.SubscribeEvent(NpcMouseEventHandler);
            //}

            return true;
        }

        //void NpcMouseEventHandler(NpcMouseCtrl.MouseCtrlEvent e)
        //{
        //    Emit(e);
        //}

        public override void Unsubscribe()
        {
            //List<AiNpcObject>.Enumerator iter = NpcManager.Instance.GetNpcEnumerator();

            //while (iter.MoveNext())
            //{
            //    AiNpcObject npc = iter.Current;
            //    npc.MouseCtrl.MouseEvent.UnsubscribeEvent(NpcMouseEventHandler);
            //}
        }
    }

    public class EPlayerTalkToNpc : Event
    {
        //AiNpcObject mNpc;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            //int npcId = 0;
            //if (int.TryParse(mInfo.Attributes["npc"].Value, out npcId))
            //{
            //    mNpc = NpcManager.Instance.GetNpc(npcId);
            //    if (null != mNpc)
            //    {
            //        return true;
            //    }
            //}

            //else

            return false;
        }

        public override bool Filter(params object[] param)
        {
            //if(1 != param.Length)
            //{
            //    return false;
            //}

            //NpcMouseCtrl.MouseCtrlEvent e = param[0] as NpcMouseCtrl.MouseCtrlEvent;

            //if (null == e)
            //{
            //    return false;
            //}

            //AiNpcObject npc = e.mSender as AiNpcObject;

            //if (mNpc != npc)
            //{
            //    return false;
            //}

            return true;
        }

        public override string ToString()
        {
            //return string.Format("Event[PlayerTalkToNpc:{0}]", mNpc);
            return null;
        }
    }

    public class EUiClickedProxy : EventProxy
    {
        public override bool Subscribe()
        {
            return true;
        }

        void UiClicked(int id)
        {
            Emit(id);
        }

        public override void Unsubscribe()
        {

        }
    }

    public class EUiClicked : Event
    {
        VarRef mVarRef;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            mVarRef = PeType.GetUIId(mInfo, mTrigger);
            
            return true;
        }

        public override bool Filter(params object[] param)
        {
            if (1 != param.Length)
            {
                return false;
            }

            int uiId = (int)param[0];

            if (-1 == uiId)
            {
                return true;
            }

            if (mVarRef.Value == uiId)
            {
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("Event[Ui Clicked:{0}]", mVarRef);
        }
    }

    public class EPlayerDeadProxy : EventProxy
    {
        //public override bool Subscribe()
        //{
        //    return true;
        //}

        //void EventTrigger(Player player)
        //{
        //    Emit(player);
        //}
        //public override void Unsubscribe()
        //{

        //}

        public override void Tick()
        {
            base.Tick();

            //if (null != PlayerFactory.mMainPlayer && PlayerFactory.mMainPlayer.dead)
            //{
            //    Emit(PlayerFactory.mMainPlayer);
            //}
        }
    }

    public class EPlayerDead : Event
    {
        VarRef mPlayerId;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            mPlayerId = PeType.GetPlayerId(mInfo, mTrigger);

            return true;
        }

        public override bool Filter(params object[] param)
        {
            if (1 != param.Length)
            {
                return false;
            }

            if (0 == mPlayerId.Value)
            {
                //Player player = (Player)param[0];
                //if (null != player && PlayerFactory.mMainPlayer == player)
                //{
                //    return true;
                //}
            }

            return false;
        }

        public override string ToString()
        {
            return string.Format("EPlayerDead:{0}]", mPlayerId);
        }
    }

    public class ENpcDeadProxy : EventProxy
    {
        public override void Tick()
        {
            base.Tick();

            //List<AiNpcObject>.Enumerator enumerator = NpcManager.Instance.GetNpcEnumerator();
            //while (enumerator.MoveNext())
            //{
            //    AiNpcObject npc = enumerator.Current;

            //    if (npc == null)
            //    {
            //        continue;
            //    }

            //    if (npc.dead)
            //    {
            //        Emit(npc);
            //    }
            //}
        }
    }

    public class ENpcDead : Event
    {
        VarRef mNpcId;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            mNpcId = PeType.GetNpcId(mInfo, mTrigger);

            return true;
        }

        public override bool Filter(params object[] param)
        {
            if (1 != param.Length)
            {
                return false;
            }

            //AiNpcObject npc = (AiNpcObject)param[0];

            //if (null != npc && mNpcId.Value == npc.mNpcId)
            //{
            //    return true;
            //}
            return false;
        }

        public override string ToString()
        {
            return string.Format("ENpcDead:{0}]", mNpcId);
        }
    }

    public class EMonsterDeadProxy : EventProxy
    {
    }

    public class EMonsterDead : Event
    {
        VarRef mMosterId;

        public override bool Parse()
        {
            if (false == base.Parse())
            {
                return false;
            }

            mMosterId = PeType.GetMonsterId(mInfo, mTrigger);

            return true;
        }

        public override bool Filter(params object[] param)
        {
            if (1 != param.Length)
            {
                return false;
            }

            //AiDataObject monster = (AiDataObject)param[0];

            //if (null != monster && mMosterId.Value == monster.dataId)
            //{
            //    return true;
            //}
            return false;
        }

        public override string ToString()
        {
            return string.Format("EPlayerDead:{0}]", mMosterId);
        }
    }
}