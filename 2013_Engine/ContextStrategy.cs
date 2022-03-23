using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using log4net;

namespace JakeKnowsEngineComponent
{
	public class ContextStrategy
	{
		protected static Logging debug = new Logging();
		protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static Dictionary<int, IStrategyOperation> _strategies = new Dictionary<int, IStrategyOperation>();
		
		public static void Init()
		{
			_strategies.Add((int)StrategyOperation.WS_CheckVersion, new StrategyCheckVersion());
			_strategies.Add((int)StrategyOperation.WS_RegisterDeviceAndSubscriber, new StrategyRegisterDeviceAndSubscriber());
			_strategies.Add((int)StrategyOperation.WS_RegisterDeviceAndSubscriberExtraData, new StrategyRegisterDeviceAndSubscriberExtraData());
			_strategies.Add((int)StrategyOperation.WS_RegisterDeviceAndSubscriberSetEncoding, new StrategyRegisterDeviceAndSubscriberSetEncoding());
			_strategies.Add((int)StrategyOperation.WS_RegisterDeviceAndSubscriberConfirmation, new StrategyRegisterDeviceAndSubscriberConfirmation());
			_strategies.Add((int)StrategyOperation.WS_RegisterDeviceAndSubscriberConfirmationSetEncoding, new StrategyRegisterDeviceAndSubscriberConfirmationSetEncoding());
			_strategies.Add((int)StrategyOperation.WS_RequestDevicePhoneConfirmation, new StrategyRequestDevicePhoneConfirmation());
			_strategies.Add((int)StrategyOperation.WS_VerifyProfileByEmail, new StrategyVerifyProfileByEmail());
			_strategies.Add((int)StrategyOperation.WS_RegisterSubscriberByServer, new StrategyRegisterSubscriberByServer());
			_strategies.Add((int)StrategyOperation.WS_GetProfileFromIDByServer, new StrategyGetProfileFromIDByServer());
			_strategies.Add((int)StrategyOperation.WS_GetProfileByServer, new StrategyGetProfileByServer());
			_strategies.Add((int)StrategyOperation.WS_AssociateContactToGroup, new StrategyAssociateContactToGroup());
			_strategies.Add((int)StrategyOperation.WS_AssociateProfileToGroup, new StrategyAssociateProfileToGroup());
			_strategies.Add((int)StrategyOperation.WS_AuthenticateUser, new StrategyAuthenticateUser());
			_strategies.Add((int)StrategyOperation.WS_BatchAssociateContactToGroup, new StrategyBatchAssociateContactToGroup());
			_strategies.Add((int)StrategyOperation.WS_BatchConfirmContactUpdate, new StrategyBatchConfirmContactUpdate());
			_strategies.Add((int)StrategyOperation.WS_BatchConfirmContactUpdateByField, new StrategyBatchConfirmContactUpdateByField());
			_strategies.Add((int)StrategyOperation.WS_BatchDropContact, new StrategyBatchDropContact());
			_strategies.Add((int)StrategyOperation.WS_BatchGetContactData, new StrategyBatchGetContactData());
			_strategies.Add((int)StrategyOperation.WS_BatchGetMyGroupsProfiles, new StrategyBatchGetMyGroupsProfiles());
			_strategies.Add((int)StrategyOperation.WS_BatchGetMyPhonesProfiles, new StrategyBatchGetMyPhonesProfiles());
			_strategies.Add((int)StrategyOperation.WS_BatchResetContactRejections, new StrategyBatchResetContactRejections());
			_strategies.Add((int)StrategyOperation.WS_BatchUpdatePIM, new StrategyBatchUpdatePIM());
			_strategies.Add((int)StrategyOperation.WS_BatchUploadContacts, new StrategyBatchUploadContacts());
			_strategies.Add((int)StrategyOperation.WS_BatchReconcileContacts, new StrategyBatchReconcileContacts());
			_strategies.Add((int)StrategyOperation.WS_BatchSyncContacts, new StrategyBatchSyncContacts());
			_strategies.Add((int)StrategyOperation.WS_BlockProfileMediaForGroup, new StrategyBlockProfileMediaForGroup());
			_strategies.Add((int)StrategyOperation.WS_MatchContactCount, new StrategyMatchContactCount());
			_strategies.Add((int)StrategyOperation.WS_CheckContactServerUpdate, new StrategyCheckContactServerUpdate());
			_strategies.Add((int)StrategyOperation.WS_CheckApp, new StrategyCheckApp());
			_strategies.Add((int)StrategyOperation.WS_ConfirmContactUpdate, new StrategyConfirmContactUpdate());
			_strategies.Add((int)StrategyOperation.WS_ConfirmContactUpdateByField, new StrategyConfirmContactUpdateByField());
			_strategies.Add((int)StrategyOperation.WS_ConfirmDeviceNotification, new StrategyConfirmDeviceNotification());
			_strategies.Add((int)StrategyOperation.WS_DownloadMedia, new StrategyDownloadMedia());
			_strategies.Add((int)StrategyOperation.WS_DownloadNote, new StrategyDownloadNote());
			_strategies.Add((int)StrategyOperation.WS_DropContact, new StrategyDropContact());
			_strategies.Add((int)StrategyOperation.WS_DropDevice, new StrategyDropDevice());
			_strategies.Add((int)StrategyOperation.WS_DropProfileMediaForGroup, new StrategyDropProfileMediaForGroup());
			_strategies.Add((int)StrategyOperation.WS_CountContacts, new StrategyCountContacts());
			_strategies.Add((int)StrategyOperation.WS_GetContact, new StrategyGetContact());
			_strategies.Add((int)StrategyOperation.WS_GetContactList, new StrategyGetContactList());
			_strategies.Add((int)StrategyOperation.WS_GetContactMediaURI, new StrategyGetContactMediaURI());
			_strategies.Add((int)StrategyOperation.WS_GetDeviceChunkSize, new StrategyGetDeviceChunkSize());
			_strategies.Add((int)StrategyOperation.WS_GetDeviceNotification, new StrategyGetDeviceNotification());
			_strategies.Add((int)StrategyOperation.WS_GetMyPhonesProfile, new StrategyGetMyPhonesProfile());
			_strategies.Add((int)StrategyOperation.WS_GetMyPhonesProfileList, new StrategyGetMyPhonesProfileList());
			_strategies.Add((int)StrategyOperation.WS_GetMyPhonesProfileAndMediaList, new StrategyGetMyPhonesProfileAndMediaList());
			_strategies.Add((int)StrategyOperation.WS_GetProfileByDevice, new StrategyGetProfileByDevice());
			_strategies.Add((int)StrategyOperation.WS_GetProfileMediaByDevice, new StrategyGetProfileMediaByDevice());
			_strategies.Add((int)StrategyOperation.WS_GetProfileGroupAssociations, new StrategyGetProfileGroupAssociations());
			_strategies.Add((int)StrategyOperation.WS_GetServerContactList, new StrategyGetServerContactList());
			_strategies.Add((int)StrategyOperation.WS_GetServerDirectives, new StrategyGetServerDirectives());
			_strategies.Add((int)StrategyOperation.WS_GetSubscriberSettings, new StrategyGetSubscriberSettings());
			_strategies.Add((int)StrategyOperation.WS_LogEvent, new StrategyLogEvent());
			_strategies.Add((int)StrategyOperation.WS_ManageProfile, new StrategyManageProfile());
			_strategies.Add((int)StrategyOperation.WS_RecoverSubscriberPassword, new StrategyRecoverSubscriberPassword());
			_strategies.Add((int)StrategyOperation.WS_ResetContactRejections, new StrategyResetContactRejections());
			_strategies.Add((int)StrategyOperation.WS_ResetDeviceRejections, new StrategyResetDeviceRejections());
			_strategies.Add((int)StrategyOperation.WS_SyncronizeContact, new StrategySyncronizeContact());
			_strategies.Add((int)StrategyOperation.WS_RestoreDefaultProfileMediaForGroup, new StrategyRestoreDefaultProfileMediaForGroup());
			_strategies.Add((int)StrategyOperation.WS_UpdatePIM, new StrategyUpdatePIM());
			_strategies.Add((int)StrategyOperation.WS_UpdateProfileGroupAssociations, new StrategyUpdateProfileGroupAssociations());
			_strategies.Add((int)StrategyOperation.WS_UpdateProfileSharingByGroup, new StrategyUpdateProfileSharingByGroup());
			_strategies.Add((int)StrategyOperation.WS_UpdateSubscriberSettings, new StrategyUpdateSubscriberSettings());
			_strategies.Add((int)StrategyOperation.WS_UploadContactBinary, new StrategyUploadContactBinary());
			_strategies.Add((int)StrategyOperation.WS_UploadDeviceCommunicationLog, new StrategyUploadDeviceCommunicationLog());
			_strategies.Add((int)StrategyOperation.WS_UploadDeviceSummary, new StrategyUploadDeviceSummary());
			_strategies.Add((int)StrategyOperation.WS_SetDeviceURLEncodeFlag, new StrategySetDeviceURLEncodeFlag());
			_strategies.Add((int)StrategyOperation.WS_UploadProfile, new StrategyUploadProfile());
			_strategies.Add((int)StrategyOperation.WS_UploadProfileBinary, new StrategyUploadProfileBinary());
			_strategies.Add((int)StrategyOperation.WS_UploadSubscriberProfile, new StrategyUploadSubscriberProfile());
			_strategies.Add((int)StrategyOperation.WS_VerifyDeviceSubscriberStatus, new StrategyVerifyDeviceSubscriberStatus());
			_strategies.Add((int)StrategyOperation.WS_AddGroup, new StrategyAddGroup());
			_strategies.Add((int)StrategyOperation.WS_AddGroupSyncronous, new StrategyAddGroupSyncronous());
			_strategies.Add((int)StrategyOperation.WS_AddJoinableGroup, new StrategyAddJoinableGroup());
			_strategies.Add((int)StrategyOperation.WS_AddMemberToGroup, new StrategyAddMemberToGroup());
			_strategies.Add((int)StrategyOperation.WS_CacheUpdateProfileGroupByDevice, new StrategyCacheUpdateProfileGroupByDevice());
			_strategies.Add((int)StrategyOperation.WS_BatchAddContactsToGroup, new StrategyBatchAddContactsToGroup());
			_strategies.Add((int)StrategyOperation.WS_BatchConfirmApplicationMembers, new StrategyBatchConfirmApplicationMembers());
			_strategies.Add((int)StrategyOperation.WS_BatchConfirmGroupMembers, new StrategyBatchConfirmGroupMembers());
			_strategies.Add((int)StrategyOperation.WS_BatchDropContactsFromGroup, new StrategyBatchDropContactsFromGroup());
			_strategies.Add((int)StrategyOperation.WS_DeleteGroup, new StrategyDeleteGroup());
			_strategies.Add((int)StrategyOperation.WS_DropFromGroup, new StrategyDropFromGroup());
			_strategies.Add((int)StrategyOperation.WS_GetMyApplications, new StrategyGetMyApplications());
			_strategies.Add((int)StrategyOperation.WS_GetApplicationPendingMembers, new StrategyGetApplicationPendingMembers());
			_strategies.Add((int)StrategyOperation.WS_GetMyGroups, new StrategyGetMyGroups());
			_strategies.Add((int)StrategyOperation.WS_GetMyGroupsByApplication, new StrategyGetMyGroupsByApplication());
			_strategies.Add((int)StrategyOperation.WS_GetMyGroupsProfileAndMediaList, new StrategyGetMyGroupsProfileAndMediaList());
			_strategies.Add((int)StrategyOperation.WS_GetGroupInvitation, new StrategyGetGroupInvitation());
			_strategies.Add((int)StrategyOperation.WS_GetJoinableGroups, new StrategyGetJoinableGroups());
			_strategies.Add((int)StrategyOperation.WS_CacheProcessContactsForGroup, new StrategyCacheProcessContactsForGroup());
			_strategies.Add((int)StrategyOperation.WS_UpdateGroup, new StrategyUpdateGroup());
			_strategies.Add((int)StrategyOperation.WS_UpdateJoinableGroup, new StrategyUpdateJoinableGroup());
			_strategies.Add((int)StrategyOperation.WS_SetGroupCommunicationPreferencesForDevice, new StrategySetGroupCommunicationPreferencesForDevice());
			_strategies.Add((int)StrategyOperation.WS_SetGlobalGroupCommunicationPreferences, new StrategySetGlobalGroupCommunicationPreferences());
			_strategies.Add((int)StrategyOperation.WS_SetGroupCommunicationPreferencesForMembers, new StrategySetGroupCommunicationPreferencesForMembers());
			_strategies.Add((int)StrategyOperation.WS_SendMessageToGroup, new StrategySendMessageToGroup());
			_strategies.Add((int)StrategyOperation.WS_UpdateGroupMembershipStatusForProfile, new StrategyUpdateGroupMembershipStatusForProfile());
			_strategies.Add((int)StrategyOperation.WS_SendInvitationToGroup, new StrategySendInvitationToGroup());
			_strategies.Add((int)StrategyOperation.WS_CacheUpdateProfileGroupByProfile, new StrategyCacheUpdateProfileGroupByProfile());
			_strategies.Add((int)StrategyOperation.WS_CacheUpdateProfileGroupByGroup, new StrategyCacheUpdateProfileGroupByGroup());
			_strategies.Add((int)StrategyOperation.WS_CacheUpdateProfileSubGroupByGroup, new StrategyCacheUpdateProfileSubGroupByGroup());
			_strategies.Add((int)StrategyOperation.WS_JoinGroup, new StrategyJoinGroup());
			_strategies.Add((int)StrategyOperation.WS_GetGroupPendingMembers, new StrategyGetGroupPendingMembers());
			_strategies.Add((int)StrategyOperation.WS_AddApplication, new StrategyAddApplication());
			_strategies.Add((int)StrategyOperation.WS_HarvesterTest, new StrategyHarvesterTest());
			_strategies.Add((int)StrategyOperation.WS_HarvesterGetProcessedResults, new StrategyHarvesterGetProcessedResults());
			_strategies.Add((int)StrategyOperation.WS_HarvesterExecute, new StrategyHarvesterExecute());
			_strategies.Add((int)StrategyOperation.WS_HarvesterProcessResults, new StrategyHarvesterProcessResults());
			_strategies.Add((int)StrategyOperation.WS_HarvesterCheckGatewayStatus, new StrategyHarvesterCheckGatewayStatus());
			_strategies.Add((int)StrategyOperation.WS_MaintenanceProcessSAQMerge, new StrategyMaintenanceProcessSAQMerge());
			_strategies.Add((int)StrategyOperation.WS_MaintenanceProcessSAQMergeForProfile, new StrategyMaintenanceProcessSAQMergeForProfile());
			_strategies.Add((int)StrategyOperation.WS_GetRootPath, new StrategyGetRootPath());
			_strategies.Add((int)StrategyOperation.WS_MaintenanceCompareMedia, new StrategyMaintenanceCompareMedia());
			_strategies.Add((int)StrategyOperation.WS_ProcessNotificationQueue, new StrategyProcessNotificationQueue());
			_strategies.Add((int)StrategyOperation.WS_ProcessEmailNotificationQueue, new StrategyProcessEmailNotificationQueue());
			_strategies.Add((int)StrategyOperation.WS_MaintenanceProcessNewContactMerge, new StrategyMaintenanceProcessNewContactMerge());
			_strategies.Add((int)StrategyOperation.WS_TestCompression, new StrategyTestCompression());
			_strategies.Add((int)StrategyOperation.WS_TestDecompression, new StrategyTestDecompression());
			_strategies.Add((int)StrategyOperation.WS_TestEncryption, new StrategyTestEncryption());
			_strategies.Add((int)StrategyOperation.WS_TestDecryption, new StrategyTestDecryption());
			_strategies.Add((int)StrategyOperation.WS_TestEcho, new StrategyTestEcho());
			_strategies.Add((int)StrategyOperation.WS_TestTimeConvert, new StrategyTestTimeConvert());
			_strategies.Add((int)StrategyOperation.WS_TestSetGroupTargetingUpdates, new StrategyTestSetGroupTargetingUpdates());
			_strategies.Add((int)StrategyOperation.WS_TestURLEncode, new StrategyTestURLEncode());
			_strategies.Add((int)StrategyOperation.WS_TestURLDecoce, new StrategyTestURLDecoce());
			_strategies.Add((int)StrategyOperation.WS_TestXMLEncode, new StrategyTestXMLEncode());
			_strategies.Add((int)StrategyOperation.WS_TestHTMLEncode, new StrategyTestHTMLEncode());
			_strategies.Add((int)StrategyOperation.WS_BatchLoadArchiveRecords, new StrategyBatchLoadArchiveRecords());
			_strategies.Add((int)StrategyOperation.WS_KPIDevicesByOEM, new StrategyKPIDevicesByOEM());
			_strategies.Add((int)StrategyOperation.WS_KPIDevicesByModel, new StrategyKPIDevicesByModel());
			_strategies.Add((int)StrategyOperation.WS_KPIDevicesByCarrier, new StrategyKPIDevicesByCarrier());
			_strategies.Add((int)StrategyOperation.WS_KPISummary, new StrategyKPISummary());
			_strategies.Add((int)StrategyOperation.WS_KPINewDevicesByDay, new StrategyKPINewDevicesByDay());
			_strategies.Add((int)StrategyOperation.WS_KPIProfileSummary, new StrategyKPIProfileSummary());
			_strategies.Add((int)StrategyOperation.WS_KPIContactSummary, new StrategyKPIContactSummary());
			_strategies.Add((int)StrategyOperation.WS_KPIDeviceSummary, new StrategyKPIDeviceSummary());
			_strategies.Add((int)StrategyOperation.WS_KPIPicturesByDataSource, new StrategyKPIPicturesByDataSource());
			_strategies.Add((int)StrategyOperation.WS_KPIUpdateStatusSummary, new StrategyKPIUpdateStatusSummary());
			_strategies.Add((int)StrategyOperation.WS_KPINewSubscribersForDate, new StrategyKPINewSubscribersForDate());
			_strategies.Add((int)StrategyOperation.WS_KPISyncronizationMetricsForDate, new StrategyKPISyncronizationMetricsForDate());
			_strategies.Add((int)StrategyOperation.WS_KPISAQDistribution, new StrategyKPISAQDistribution());
			_strategies.Add((int)StrategyOperation.WS_AddCreditCard, new StrategyAddCreditCard());
			_strategies.Add((int)StrategyOperation.WS_GetCreditCardData, new StrategyGetCreditCardData());
			_strategies.Add((int)StrategyOperation.WS_GetOffersForDevice, new StrategyGetOffersForDevice());
			_strategies.Add((int)StrategyOperation.WS_GetOffersForGroup, new StrategyGetOffersForGroup());
			_strategies.Add((int)StrategyOperation.WS_GetPurchases, new StrategyGetPurchases());
			_strategies.Add((int)StrategyOperation.WS_GetReceipt, new StrategyGetReceipt());
			_strategies.Add((int)StrategyOperation.WS_Purchase, new StrategyPurchase());
			_strategies.Add((int)StrategyOperation.WS_PurchaseAvanced, new StrategyPurchaseAvanced());
			_strategies.Add((int)StrategyOperation.WS_InitializePurchase, new StrategyInitializePurchase());
			_strategies.Add((int)StrategyOperation.WS_CompletePurchaseAdvanced, new StrategyCompletePurchaseAdvanced());
			_strategies.Add((int)StrategyOperation.WS_RemoveCreditCard, new StrategyRemoveCreditCard());
			_strategies.Add((int)StrategyOperation.WS_ShareOfferWithGroup, new StrategyShareOfferWithGroup());
			_strategies.Add((int)StrategyOperation.WS_GetTransactions, new StrategyGetTransactions());
			_strategies.Add((int)StrategyOperation.WS_GetAllMerchants, new StrategyGetAllMerchants());
			_strategies.Add((int)StrategyOperation.WS_ProcessInboundSMS, new StrategyProcessInboundSMS());
			_strategies.Add((int)StrategyOperation.WS_DeliverVoiceNotification, new StrategyDeliverVoiceNotification());
			_strategies.Add((int)StrategyOperation.WS_ProcessSMSDeliveryStatus, new StrategyProcessSMSDeliveryStatus());
			_strategies.Add((int)StrategyOperation.WS_CustomerServiceRequest, new StrategyCustomerServiceRequest());
			_strategies.Add((int)StrategyOperation.WS_TestReadNews, new StrategyTestReadNews());
			_strategies.Add((int)StrategyOperation.WS_GetNews, new StrategyGetNews());

		}

        public static bool Execute(WSCall ws, DataObjects obj)
		{
            int op = ws.GetFunctionID();
            Logging.LogDebug("Execute op " + op);
            if (op == (int)StrategyOperation.WS_INVALID)
			{
                log.Error("Execute op WS_INVALID");
                return false;
			}
            return _strategies[op].Execute(ws, obj);
		}	
	public enum StrategyOperation
	{
		WS_INVALID,
		WS_CheckVersion, //checkWebServiceVersion
		WS_RegisterDeviceAndSubscriber, //registerDeviceAndSubscriber
		WS_RegisterDeviceAndSubscriberExtraData, //registerDeviceAndSubscriberWithEnhancedData
		WS_RegisterDeviceAndSubscriberSetEncoding, //registerDeviceAndSubscriberSetEncoding
		WS_RegisterDeviceAndSubscriberConfirmation, //registerDeviceAndSubscriberConfirmed
		WS_RegisterDeviceAndSubscriberConfirmationSetEncoding, //registerDeviceAndSubscriberConfirmedSetEncoding
		WS_RequestDevicePhoneConfirmation, //requestDevicePhoneNumberConfirmation
		WS_VerifyProfileByEmail, //verifyProfileIdByEmailAddress
		WS_RegisterSubscriberByServer, //registerSubscriberByServer
		WS_GetProfileFromIDByServer, //getProfileFromProfileIdByServer
		WS_GetProfileByServer, //getProfileByServer
		WS_AssociateContactToGroup, //associateContactWithProfileGroup
		WS_AssociateProfileToGroup, //associateProfileWithProfileGroup
		WS_AuthenticateUser, //authenticateUser
		WS_BatchAssociateContactToGroup, //batchAssociateContactWithProfileGroup
		WS_BatchConfirmContactUpdate, //batchConfirmContactRecordUpdate
		WS_BatchConfirmContactUpdateByField, //batchConfirmContactRecordUpdateByField
		WS_BatchDropContact, //batchDropContact
		WS_BatchGetContactData, //batchGetContactData
		WS_BatchGetMyGroupsProfiles, //batchGetGroupsImInProfileData
		WS_BatchGetMyPhonesProfiles, //batchGetPhonesImInProfileData
		WS_BatchResetContactRejections, //batchResetContactRejections
		WS_BatchUpdatePIM, //batchUpdatePimContactUID
		WS_BatchUploadContacts, //batchUploadContacts
		WS_BatchReconcileContacts, //batchReconcileContacts
		WS_BatchSyncContacts, //batchSyncContacts
		WS_BlockProfileMediaForGroup, //blockProfileMediaForGroup
		WS_MatchContactCount, //checkContactCount
		WS_CheckContactServerUpdate, //checkContactForServerUpdate
		WS_CheckApp, //checkJake
		WS_ConfirmContactUpdate, //confirmContactRecordUpdate
		WS_ConfirmContactUpdateByField, //confirmContactRecordUpdateByField
		WS_ConfirmDeviceNotification, //confirmDeviceNotification
		WS_DownloadMedia, //downloadMediaB64String
		WS_DownloadNote, //downloadNoteString
		WS_DropContact, //dropContact
		WS_DropDevice, //dropDevice
		WS_DropProfileMediaForGroup, //dropProfileMediaForGroup
		WS_CountContacts, //getContactCount
		WS_GetContact, //getContactData
		WS_GetContactList, //getContactList
		WS_GetContactMediaURI, //getContactMediaURI
		WS_GetDeviceChunkSize, //getDeviceChunkSize
		WS_GetDeviceNotification, //getDeviceNotification
		WS_GetMyPhonesProfile, //getPhonesImInProfileData
		WS_GetMyPhonesProfileList, //getPhonesImInProfileIdList
		WS_GetMyPhonesProfileAndMediaList, //getPhonesImInProfileNameAndMediaList
		WS_GetProfileByDevice, //getProfileByDevice
		WS_GetProfileMediaByDevice, //getProfileMediaByDevice
		WS_GetProfileGroupAssociations, //getProfileFieldGroupAssociations
		WS_GetServerContactList, //getServerContactList
		WS_GetServerDirectives, //getServerDirectives
		WS_GetSubscriberSettings, //getSubscriberSettings
		WS_LogEvent, //logEvent
		WS_ManageProfile, //manageProfile
		WS_RecoverSubscriberPassword, //recoverSubscriberPassword
		WS_ResetContactRejections, //resetContactRejections
		WS_ResetDeviceRejections, //resetDeviceRejections
		WS_SyncronizeContact, //syncContact
		WS_RestoreDefaultProfileMediaForGroup, //restoreDefaultProfileMediaForGroup
		WS_UpdatePIM, //updatePimContactUID
		WS_UpdateProfileGroupAssociations, //updateProfileFieldGroupAssociations
		WS_UpdateProfileSharingByGroup, //updateProfileFieldSharingByGroup
		WS_UpdateSubscriberSettings, //updateSubscriberSettings
		WS_UploadContactBinary, //uploadContactBinary
		WS_UploadDeviceCommunicationLog, //uploadDeviceComLog
		WS_UploadDeviceSummary, //uploadDeviceSummary
		WS_SetDeviceURLEncodeFlag, //setDeviceUrlEncodeDataFlag
		WS_UploadProfile, //uploadProfile
		WS_UploadProfileBinary, //uploadProfileBinary
		WS_UploadSubscriberProfile, //uploadSubscriberProfile
		WS_VerifyDeviceSubscriberStatus, //verifyDeviceSubscriberStatus
		WS_AddGroup, //addGroup
		WS_AddGroupSyncronous, //addGroupSyncronous
		WS_AddJoinableGroup, //addJoinableGroup
		WS_AddMemberToGroup, //addNewMemberToGroup
		WS_CacheUpdateProfileGroupByDevice, //addUpdateProfileProfileGroupCacheByProfileIdAndDev
		WS_BatchAddContactsToGroup, //batchAddContactsToGroup
		WS_BatchConfirmApplicationMembers, //batchConfirmApplicationMembers
		WS_BatchConfirmGroupMembers, //batchConfirmGroupMembers
		WS_BatchDropContactsFromGroup, //batchDropContactsFromGroup
		WS_DeleteGroup, //deleteGroup
		WS_DropFromGroup, //dropFromGroup
		WS_GetMyApplications, //getApplicationsImIn
		WS_GetApplicationPendingMembers, //getApplicationPendingMembers
		WS_GetMyGroups, //getGroupsImIn
		WS_GetMyGroupsByApplication, //getGroupsImInByApplication
		WS_GetMyGroupsProfileAndMediaList, //getGroupsImInProfileNameAndMediaList
		WS_GetGroupInvitation, //getGroupInvitationContent
		WS_GetJoinableGroups, //getJoinableGroups
		WS_CacheProcessContactsForGroup, //processContactImportCacheForGroup
		WS_UpdateGroup, //updateGroup
		WS_UpdateJoinableGroup, //updateJoinableGroup
		WS_SetGroupCommunicationPreferencesForDevice, //setGroupCommunicationsPreferencesForDevice
		WS_SetGlobalGroupCommunicationPreferences, //setGlobalGroupCommunicationsPreferences
		WS_SetGroupCommunicationPreferencesForMembers, //setGroupCommunicationsPreferencesForGroupMembers
		WS_SendMessageToGroup, //sendMessageToGroup
		WS_UpdateGroupMembershipStatusForProfile, //updateGroupMembershipStatusForProfile
		WS_SendInvitationToGroup, //sendInvitationToGroup
		WS_CacheUpdateProfileGroupByProfile, //updateProfileProfileGroupCacheByProfileId
		WS_CacheUpdateProfileGroupByGroup, //updateProfileProfileGroupCacheByProfileGroupId
		WS_CacheUpdateProfileSubGroupByGroup, //updateProfileProfileSubGroupCacheByProfileGroupId
		WS_JoinGroup, //joinGroup
		WS_GetGroupPendingMembers, //getGroupPendingMembers
		WS_AddApplication, //addApplication
		WS_HarvesterTest, //harversterTest
		WS_HarvesterGetProcessedResults, //harversterGetProcessedResults
		WS_HarvesterExecute, //harvesterExecute
		WS_HarvesterProcessResults, //harversterProcessResults
		WS_HarvesterCheckGatewayStatus, //harversterCheckGatewayStatus
		WS_MaintenanceProcessSAQMerge, //maintenanceProcessSAQMerge
		WS_MaintenanceProcessSAQMergeForProfile, //maintenanceProcessSAQMergeForProfile
		WS_GetRootPath, //getRootPath
		WS_MaintenanceCompareMedia, //maintenanceCompareMedia
		WS_ProcessNotificationQueue, //processNotificationQueue
		WS_ProcessEmailNotificationQueue, //processEmailNotificationQueue
		WS_MaintenanceProcessNewContactMerge, //maintenanceProcessNewContactMerge
		WS_TestCompression, //testCompression
		WS_TestDecompression, //testDecompression
		WS_TestEncryption, //testEncryption
		WS_TestDecryption, //testDecryption
		WS_TestEcho, //testEcho
		WS_TestTimeConvert, //testTimeConvert
		WS_TestSetGroupTargetingUpdates, //testSetGroupTargetingUpdates
		WS_TestURLEncode, //testURLEncode
		WS_TestURLDecoce, //testURLDecode
		WS_TestXMLEncode, //testXMLEncode
		WS_TestHTMLEncode, //testHTMLEncode
		WS_BatchLoadArchiveRecords, //batchLoadArchiveRecords
		WS_KPIDevicesByOEM, //kpiDevicesByOEM
		WS_KPIDevicesByModel, //kpiDevicesByModel
		WS_KPIDevicesByCarrier, //kpiDevicesByCarrier
		WS_KPISummary, //kpiDataSummary
		WS_KPINewDevicesByDay, //kpiNewDevicesByDay
		WS_KPIProfileSummary, //kpiProfileDataSummary
		WS_KPIContactSummary, //kpiContactDataSummary
		WS_KPIDeviceSummary, //kpiDeviceDataSummary
		WS_KPIPicturesByDataSource, //kpiPicturesByDataSource
		WS_KPIUpdateStatusSummary, //kpiFieldUpdateStatusDataSummary
		WS_KPINewSubscribersForDate, //kpiNewSubscribersForDate
		WS_KPISyncronizationMetricsForDate, //kpiSyncMetricsForDate
		WS_KPISAQDistribution, //kpiSAQDistribution
		WS_AddCreditCard, //addCreditCard
		WS_GetCreditCardData, //getCreditCardDisplayData
		WS_GetOffersForDevice, //getOffersForDevice
		WS_GetOffersForGroup, //getOffersForGroup
		WS_GetPurchases, //getPurchases
		WS_GetReceipt, //getReceipt
		WS_Purchase, //makePurchase
		WS_PurchaseAvanced, //makePurchaseAdvanced
		WS_InitializePurchase, //initializePurchase
		WS_CompletePurchaseAdvanced, //completePurchaseAdvanced
		WS_RemoveCreditCard, //removeCreditCardData
		WS_ShareOfferWithGroup, //shareOfferWithFullGroup
		WS_GetTransactions, //getTransactions
		WS_GetAllMerchants, //getAllMerchants
		WS_ProcessInboundSMS, //processInboundSMS
		WS_DeliverVoiceNotification, //deliverVoiceNotification
		WS_ProcessSMSDeliveryStatus, //processSmsDeliveryStatus
		WS_CustomerServiceRequest, //customerServiceRequest
		WS_TestReadNews, //testReadNews
		WS_GetNews, //getNews
	}
	}
}
