﻿namespace GFProxy.Protocol.CommandIDs;

public static class WorldServerCommands {
    public enum CW_Commands : ushort {
        NC_CW_ClientTextOutput = 39,
        NC_CW_ClientUpdateFamilyMember = 49,
        NC_CW_ClientTextOutputIndex = 45,
        NC_CW_ClientAskFamilyTransform = 79,
        NC_CW_ClientFamilyInvitation = 70,
        NC_CW_ClientRepeatedLogin = 52,
        NC_CW_ClientItemMallList = 60,
        NC_CW_ServerChangeWorldAck = 0,
        NC_CW_ServerRemoveFriend = 1,
        NC_CW_ServerItemMallReqBuyItem = 2,
        NC_CW_ServerItemMallListQuery = 3,
        NC_CW_ServerCaptchaReturn = 4,
        NC_CW_ServerHello = 5,
        NC_CW_ServerReceiveTicket = 6,
        NC_CW_ServerKeepAlive = 7,
        NC_CW_ServerLookingForMember = 8,
        NC_CW_ServerLoginCharacter = 9,
        NC_CW_ServerCreateCharacterName = 10,
        NC_CW_ServerItemMallPointInfo = 11,
        NC_CW_ServerCreateCharacterData = 12,
        NC_CW_ServerRemoveCharacter = 13,
        NC_CW_ServerCancelCreateCharacterReq = 14,
        NC_CW_ServerFamilyOp = 15,
        NC_CW_ServerQueryFamilyInfo = 16,
        NC_CW_ServerLookingForGroup = 17,
        NC_CW_ServerAddFriend = 18,
        NC_CW_ServerQueryFriendInfo = 19,
        NC_CW_ServerIllPluginInfo = 20,
        NC_CW_ServerFriendInvitation = 21,
        NC_CW_ServerChangeWorld = 22,
        NC_CW_ServerItemMallAnythingReceivable = 23,
        NC_CW_ServerItemMallReceivableList = 24,
        NC_CW_ServerItemMallReceivableQuery = 25,
        NC_CW_ServerItemMallReceieveAttempt = 26,
        NC_CW_ServerQueryFamilyRecruitList = 27,
        NC_CW_ServerQueryFamilyRecruitJoin = 28,
        NC_CW_ServerCharPassword = 29,
        NC_CW_ServerUpdateWorldServers = 30,
        NC_CW_ServerQueryMail = 31,
        NC_CW_ServerFortuneBagItemQuery = 32,
        NC_CW_ServerFamilyTransformInvite = 33,
        NC_CW_ServerFamilyTransformAgree = 34,
        NC_CW_ServerFamilyTreeOperation = 35,
        NC_CW_ServerQueryFunctionSwitch = 36,
        NC_CW_ClientReceiveTicketToWorld = 37,
        NC_CW_ClientHello = 38,
        NC_CW_ClientReceiveTicketToZoneServer = 40,
        NC_CW_ClientGetCharacters = 41,
        NC_CW_ClientVerifyCharacterName = 42,
        NC_CW_ClientResetTime = 43,
        NC_CW_ClientCancelCreateCharacterResp = 44,
        NC_CW_ClientChannelMsg = 46,
        NC_CW_ClientLookingForMemberResult = 47,
        NC_CW_ClientXTrapCSStep1To2 = 48,
        NC_CW_ClientUpdateFriendMember = 50,
        NC_CW_ClientFriendInvitation = 51,
        NC_CW_ClientException = 53,
        NC_CW_ClientItemMallAnythingReceivable = 54,
        NC_CW_ClientItemMallReceivableQuery = 55,
        NC_CW_ClientItemMallReceieveAttempt = 56,
        NC_CW_ClientTicketInvalid = 57,
        NC_CW_ClientUpdatePrestigeData = 58,
        NC_CW_ClientUpdateBlackList = 59,
        NC_CW_ClientItemMallReqBuyItem = 61,
        NC_CW_ClientItemMallPointInfo = 62,
        NC_CW_ClientGetWorldServers = 63,
        NC_CW_ClientFamilyAchievementUpdate = 64,
        NC_CW_ClientCaptcha = 65,
        NC_CW_ClientCrossZone = 66,
        NC_CW_ClientQueryFamilyRecruitList = 67,
        NC_CW_ClientTell = 68,
        NC_CW_ClientFamilyOpException = 69,
        NC_CW_ClientFamilyQuit = 71,
        NC_CW_ClientQueryFamilyInfo = 72,
        NC_CW_ClientFamilySchedule = 73,
        NC_CW_ClientCharPassword = 74,
        NC_CW_ClientApexBufferTransfer = 75,
        NC_CW_ClientMailList = 76,
        NC_CW_ClientMailRead = 77,
        NC_CW_ClientFortuneBagItem = 78,
        NC_CW_ClientGetMotionEndTime = 80,
        NC_CW_ClientFunctionSwitch = 81,
        NC_CW_ServerTextCommand = 0
    }

    public const ushort NC_DBA_AgentDBACommand = 20;
    public const ushort NC_G_ServerCharacterLogin = 16;
    public const ushort NC_G_ServerCharacterLogout = 17;
    public const ushort NC_ZMis_MissionServerAddFamilyMessage = 54;
    public const ushort NC_ZMis_MissionServerCheckFamilyInvite = 8;
    public const ushort NC_ZW_ZoneServerTextCommand = 24;
    public const ushort NC_G_ServerAccountLogout = 1;
    public const ushort NC_ZW_ZoneServerUpdateFortuneBag = 27;
    public const ushort NC_ZW_ZoneServerUpdateLottery = 29;
    public const ushort NC_G_ServerSaveHealthTime = 21;
    public const ushort NC_ZW_ZoneServerReportID = 16;
    public const ushort NC_T_ServerGetTicket = 0;
    public const ushort NC_G_ServerGetPointInfo = 4;
    public const ushort NC_ZW_ZoneServerItemMallReceieveAttempt = 25;
    public const ushort NC_ZMis_MissionServerItemMallMailCheck = 53;
    public const ushort NC_ZW_ZoneServerGetCloneItemID = 26;
    public const ushort NC_G_ServerReqBuyItem = 5;
    public const ushort NC_T_ServerUseTicket = 1;
    public const ushort NC_T_ClientGetTicket = 2;
    public const ushort NC_T_ClientUseTicket = 3;
    public const ushort NC_T_ServerCheckPin = 4;
    public const ushort NC_T_ClientCheckPin = 5;
    public const ushort NC_G_ServerBillingLogin = 2;
    public const ushort NC_G_ServerAccountLogin = 0;
    public const ushort NC_G_ServerClientTypeReport = 3;
    public const ushort NC_G_ClientAccountLogin = 6;
    public const ushort NC_G_ClientAccountLogout = 7;
    public const ushort NC_G_ClientKickUser = 8;
    public const ushort NC_G_ClientServerTypeReport = 9;
    public const ushort NC_G_ClientSetWorldStates = 10;
    public const ushort NC_G_ClientBillingLogin = 11;
    public const ushort NC_G_ClientGetPointInfo = 12;
    public const ushort NC_G_ClientReqBuyItem = 13;
    public const ushort NC_G_ServerTextCommand = 14;
    public const ushort NC_G_ClientTextCommand = 15;
    public const ushort NC_G_ServerExchangePinItem = 18;
    public const ushort NC_G_ServerExchangePinUpdateState = 19;
    public const ushort NC_G_ClientExchangePinItemResult = 20;
    public const ushort NC_G_ClientUpdateHealthTime = 22;
    public const ushort NC_ZMis_MissionServerItemMallMailSend = 52;
    public const ushort NC_ZW_WorldServerIDReport = 0;
    public const ushort NC_ZW_WorldServerTell = 1;
    public const ushort NC_ZW_WorldServerAllUsers = 2;
    public const ushort NC_ZW_WorldServerNotifyEntry = 3;
    public const ushort NC_ZW_WorldServerCharacterArrived = 4;
    public const ushort NC_ZW_WorldServerResetTime = 5;
    public const ushort NC_ZW_WorldServerReturnToCharacterList = 6;
    public const ushort NC_ZW_WorldServerTeamLeavedNotify = 7;
    public const ushort NC_ZW_WorldServerChannelMsg = 8;
    public const ushort NC_ZW_WorldServerFamilyCreation = 9;
    public const ushort NC_ZW_WorldServerLogout = 10;
    public const ushort NC_ZW_WorldServerTextCommand = 11;
    public const ushort NC_ZW_WorldServerItemMallReceieveAttempt = 12;
    public const ushort NC_ZW_WorldServerGetCloneItemID = 13;
    public const ushort NC_ZW_WorldServerFBMsgUpdate = 14;
    public const ushort NC_ZW_WorldServerTextExCmd = 15;
    public const ushort NC_ZW_ZoneServerDestShutdown = 17;
    public const ushort NC_ZW_ZoneServerCharacterArrive = 18;
    public const ushort NC_ZW_ZoneServerCharacterArrivedDest = 19;
    public const ushort NC_ZW_ZoneServerChannelMsg = 20;
    public const ushort NC_ZW_ZoneServerChangeFamily = 21;
    public const ushort NC_ZW_ZoneServerNewFamily = 22;
    public const ushort NC_ZW_ZoneServerRemoveFamily = 23;
    public const ushort NC_ZW_ZoneServerFamilyUseSpell = 28;
    public const ushort NC_ZW_ZoneServerTextExCmd = 30;
    public const ushort NC_ZW_ZoneServerGMToolGetInfo = 31;
    public const ushort NC_MW_WorldServerTell = 0;
    public const ushort NC_MW_WorldServerItemMallMailSendResult = 1;
    public const ushort NC_MW_WorldServerItemMallMailCheckOK = 2;
    public const ushort NC_MW_WorldServerCrossZone = 3;
    public const ushort NC_MW_WorldServerUpdateBlackList = 4;
    public const ushort NC_MW_WorldServerUpdatePrestigeData = 5;
    public const ushort NC_MW_WorldServerUpdateFriendMember = 6;
    public const ushort NC_MW_WorldServerCheckFamilyInvite = 7;
    public const ushort NC_MW_WorldServerFBMsgUpdate = 9;
    public const ushort NC_MW_WorldServerTextExCmd = 10;
    public const ushort NC_MW_WorldServerTextExCmdRemoteWorld = 11;
    public const ushort NC_MW_WorldServerTextExCmdRemoteZone = 12;
    public const ushort NC_MW_WorldServerFunctionSwitchUpdate = 13;
    public const ushort NC_MW_MissionServerTextExCmd = 14;
    public const ushort NC_MW_MissionServerTextExCmdRemoteWorld = 15;
    public const ushort NC_MW_MissionServerTextExCmdRemoteZone = 16;
    public const ushort NC_ZM_ZoneServerUpdatePKList = 17;
    public const ushort NC_ZMis_MissionServerUpdatePKList = 18;
    public const ushort NC_MW_WorldTextCommand = 19;
    public const ushort NC_DBA_ClientContainer = 21;
    public const ushort NC_DBA_ClientEnchant = 22;
    public const ushort NC_DBA_ClientNodeID = 23;
    public const ushort NC_DBA_ClientPlayerData = 24;
    public const ushort NC_DBA_ClientDelPlayerOK = 25;
    public const ushort NC_DBA_ClientAllFamily = 26;
    public const ushort NC_DBA_ClientNewFamily = 27;
    public const ushort NC_DBA_ClientChangeFamily = 28;
    public const ushort NC_DBA_ClientRemoveFamily = 29;
    public const ushort NC_DBA_ClientFamilyAllMember = 30;
    public const ushort NC_DBA_ClientAddFamilyMember = 31;
    public const ushort NC_DBA_ClientMigrationCharacter = 32;
    public const ushort NC_DBA_ClientChannelMsg = 33;
    public const ushort NC_DBA_ClientElfData = 34;
    public const ushort NC_DBA_ClientShortcut = 35;
    public const ushort NC_DBA_ClientFinishPlayerData = 36;
    public const ushort NC_DBA_ClientCollection = 37;
    public const ushort NC_DBA_ClientFamilyAllMessage = 38;
    public const ushort NC_DBA_ClientAllLover = 39;
    public const ushort NC_DBA_ClientAllTerritory = 40;
    public const ushort NC_DBA_ClientFamilyStorage = 41;
    public const ushort NC_DBA_ClientSpellMaster = 42;
    public const ushort NC_DBA_ClientTransportData = 43;
    public const ushort NC_DBA_ClientRecommendedEvents = 44;
    public const ushort NC_DBA_ReloadTranslate = 45;
    public const ushort NC_DBA_ClientFamilyCropInfo = 46;
    public const ushort NC_DBA_ClientRaceTeamInfo = 47;
    public const ushort NC_DBA_ClientMentorship = 48;
    public const ushort NC_ZMis_MissionServerRefreshRecommendedEvents = 49;
    public const ushort NC_ZMis_MissionServerUpdateRecommendedEvents = 50;
    public const ushort NC_ZMis_ZoneServerUpdateRecommendedEvents = 51;
    public const ushort NC_ZMis_ZoneServerReportID = 55;
    public const ushort NC_ZMis_MissionServerIDReport = 56;
}