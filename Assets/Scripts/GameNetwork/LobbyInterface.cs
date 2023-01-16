using System;
using uLobby;
using UnityEngine;
using BitStream = uLink.BitStream;

public class LobbyInterface : MonoBehaviour
{
	protected MessageHandlers msgHandlers;

	public void SetHandlers(MessageHandlers handlers)
	{
		msgHandlers = handlers;
	}

	public bool CheckHandler(ELobbyMsgType msgType)
	{
		return null == msgHandlers ? false : msgHandlers.CheckHandler(msgType);
	}

	public static void LobbyRPC(params object[] obj)
	{
		try
		{
			#if !UNITY_2018_1_OR_NEWER
			Lobby.RPC("RPC_LobbyMsg", Lobby.lobby, obj);
			#endif
		}
		catch (Exception e)
		{
			if (LogFilter.logError) Debug.LogErrorFormat("{0}\r\n{1}", e.Message, e.StackTrace);
		}
	}

	#if !UNITY_2018_1_OR_NEWER
	[RPC]
	#endif
	protected void RPC_LobbyMsg(BitStream stream, LobbyMessageInfo info)
	{
		ELobbyMsgType msgType = ELobbyMsgType.Max;

		try
		{
			msgType = stream.Read<ELobbyMsgType>();

			if (CheckHandler(msgType))
				msgHandlers[msgType](stream, info);
			else
				if (LogFilter.logError) Debug.LogWarningFormat("Message:[{0}]|[{1}] does not implement", msgType, GetType());
		}
		catch (Exception e)
		{
			if (LogFilter.logError) Debug.LogErrorFormat("Message:[{0}]\r\n{1}\r\n{2}\r\n{3}", GetType(), msgType, e.Message, e.StackTrace);
		}
	}
}
