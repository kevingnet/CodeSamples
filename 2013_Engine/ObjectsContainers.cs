using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Concurrent;
using System.Data.SqlTypes;
using System.Configuration;
using System.Diagnostics;
using log4net;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;

namespace JakeKnowsEngineComponent
{
    public partial class DataObjects
    {
        public enum TableTypes
        {
            TTABLEINVALID,
            TAssocApplication_NewsChannel,
            TAssocContact_Company,
            TAssocContact_Email,
            TAssocContact_Location,
            TAssocContact_Media,
            TAssocContact_Note,
            TAssocContact_Person,
            TAssocContact_Phone,
            TAssocContact_Prefix,
            TAssocContact_Title,
            TAssocContact_WebAddress,
            TAssocDevice_ServerMessage,
            TAssocGroup_Application,
            TAssocGroup_Group,
            TAssocGroup_InterestAttribute,
            TAssocGroup_MetroArea,
            TAssocGroup_NewsChannel,
            TAssocInterestAttribute_InterestAttribute_Strength,
            TAssocMetroArea_AreaCode,
            TAssocMetroArea_ZipCode,
            TAssocOffer_Group,
            TAssocOffer_Group_InterestAttribute,
            TAssocOffer_MetroArea,
            TAssocProfile_Application,
            TAssocProfile_ArchiveRecord,
            TAssocProfile_Company,
            TAssocProfile_Contact,
            TAssocProfile_Contact_InterestAttributes,
            TAssocProfile_Email,
            TAssocProfile_Group,
            TAssocProfile_InterestAttribute,
            TAssocProfile_Location,
            TAssocProfile_Media,
            TAssocProfile_Media_Relationship,
            TAssocProfile_Merchant,
            TAssocProfile_MetroArea,
            TAssocProfile_Note,
            TAssocProfile_Person,
            TAssocProfile_Phone,
            TAssocProfile_Prefix,
            TAssocProfile_Profile,
            TAssocProfile_Title,
            TAssocProfile_WebAddress,
            TAssocSubscriber_DataSource,
            TCacheInboundRequest,
            TCacheProfileGroupDevice,
            TFltDevice_DataInput,
            TFltProfile_DataAccess,
            TFltProfile_DataManagement,
            THistDeviceToken,
            THistSearch,
            THrvHarvesterResultQueue,
            THrvHarvesterSystem,
            THrvlHarvesterAction,
            TLnkGroup_Profile_Admin,
            TLnkOffer_Group,
            TLnkProfile_Contact,
            TLnkProfile_DataSource_PrioritySearch,
            TLnkProfile_Group_Admin,
            TLnkProfile_Media_Hash,
            TLnkProfile_Profile_MergeReview,
            TOvrDevice_DataInput,
            TOvrProfile_DataAccess,
            TOvrProfile_DataManagement,
            TOvrProfile_Profile_RelationshipType,
            TReviewProfile_Application,
            TReviewProfile_Profile,
            TStatusFieldUpdates,
            TStatusSubscribers,
            TTblAccessKey,
            TTblApplication,
            TTblAreaCode,
            TTblAreaCodeRegion,
            TTblCity,
            TTblCompany,
            TTblContact,
            TTblCountry,
            TTblCreditCard,
            TTblDataSource,
            TTblDataSourceLogin,
            TTblDevice,
            TTblDeviceConfirmationCode,
            TTblDeviceManufacturer,
            TTblDeviceModel,
            TTblDMA,
            TTblEmail,
            TTblGroup,
            TTblInterestArea,
            TTblInterestAttribute,
            TTblLocation,
            TTblLocationCity,
            TTblLocationCountry,
            TTblLocationState,
            TTblLocationStreet,
            TTblLocationStreet2,
            TTblLocationZipCode,
            TTblMedia,
            TTblMerchant,
            TTblMetroArea,
            TTblNewsChannel,
            TTblNewsItem,
            TTblNote,
            TTblNotification,
            TTblNotificationGateway,
            TTblNotificationQueue,
            TTblOffer,
            TTblPerson,
            TTblPersonKind,
            TTblPhone,
            TTblPrioritySearchQueue,
            TTblProfile,
            TTblRegion,
            TTblSearchThrottle,
            TTblSmtpAddress,
            TTblSubscriber,
            TTblTimeZone,
            TTblTransaction,
            TTblWebAddress,
            TTblZipCode,
            TTypeAccount,
            TTypeCarrier,
            TTypeCommunication,
            TTypeCreditCard,
            TTypeDataAccess,
            TTypeDataInput,
            TTypeDataManagement,
            TTypeHarvesterAction,
            TTypeLinkSource,
            TTypeLocation,
            TTypeMediaSource,
            TTypeNotification,
            TTypeNotificationChannel,
            TTypeNotificationGateway,
            TTypeNotificationVendor,
            TTypePingCycle,
            TTypePrefix,
            TTypeRelationship,
            TTypeServerMessage,
            TTypeSource,
            TTypeTitle,
            TTypeZipCode,
            TTypeZipCodeLocation
        }

        protected static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        FieldsDictionary m_Fields = null;
        ConcurrentDictionary<TableTypes, CData> _tables = new ConcurrentDictionary<TableTypes, CData>();
        private FltDevice_DataInputData _FltDevice_DataInput = new FltDevice_DataInputData(0, 0);
        private FltProfile_DataAccessData _FltProfile_DataAccess = new FltProfile_DataAccessData(0, 0);
        private FltProfile_DataManagementData _FltProfile_DataManagement = new FltProfile_DataManagementData(0, 0);
        private StatusFieldUpdatesData _StatusFieldUpdates = new StatusFieldUpdatesData(0, 0);
        private StatusSubscribersData _StatusSubscribers = new StatusSubscribersData(0, 0);
        private TblAccessKeyData _TblAccessKey = new TblAccessKeyData(1000, 100);
        private TblApplicationData _TblApplication = new TblApplicationData(0, 0);
        private TblAreaCodeData _TblAreaCode = new TblAreaCodeData(8000, 100);
        private TblAreaCodeRegionData _TblAreaCodeRegion = new TblAreaCodeRegionData(0, 0);
        private TblCityData _TblCity = new TblCityData(50000, 500);
        private TblCompanyData _TblCompany = new TblCompanyData(9000, 100);
        private TblContactData _TblContact = new TblContactData(50000, 500);
        private TblCountryData _TblCountry = new TblCountryData(0, 0);
        private TblCreditCardData _TblCreditCard = new TblCreditCardData(6000, 100);
        private TblDataSourceData _TblDataSource = new TblDataSourceData(0, 0);
        private TblDataSourceLoginData _TblDataSourceLogin = new TblDataSourceLoginData(0, 0);
        private TblDeviceData _TblDevice = new TblDeviceData(20000, 200);
        private TblDeviceConfirmationCodeData _TblDeviceConfirmationCode = new TblDeviceConfirmationCodeData(5000, 100);
        private TblDeviceManufacturerData _TblDeviceManufacturer = new TblDeviceManufacturerData(0, 0);
        private TblDeviceModelData _TblDeviceModel = new TblDeviceModelData(0, 0);
        private TblDMAData _TblDMA = new TblDMAData(0, 0);
        private TblEmailData _TblEmail = new TblEmailData(50000, 500);
        private TblGroupData _TblGroup = new TblGroupData(5000, 100);
        private TblInterestAreaData _TblInterestArea = new TblInterestAreaData(0, 0);
        private TblInterestAttributeData _TblInterestAttribute = new TblInterestAttributeData(0, 0);
        private TblLocationData _TblLocation = new TblLocationData(50000, 500);
        private TblLocationCityData _TblLocationCity = new TblLocationCityData(0, 0);
        private TblLocationCountryData _TblLocationCountry = new TblLocationCountryData(0, 0);
        private TblLocationStateData _TblLocationState = new TblLocationStateData(0, 0);
        private TblLocationStreetData _TblLocationStreet = new TblLocationStreetData(30000, 300);
        private TblLocationStreet2Data _TblLocationStreet2 = new TblLocationStreet2Data(0, 0);
        private TblLocationZipCodeData _TblLocationZipCode = new TblLocationZipCodeData(0, 0);
        private TblMediaData _TblMedia = new TblMediaData(66000, 100);
        private TblMerchantData _TblMerchant = new TblMerchantData(3000, 100);
        private TblMetroAreaData _TblMetroArea = new TblMetroAreaData(0, 0);
        private TblNewsChannelData _TblNewsChannel = new TblNewsChannelData(1000, 100);
        private TblNewsItemData _TblNewsItem = new TblNewsItemData(1000, 100);
        private TblNoteData _TblNote = new TblNoteData(4000, 100);
        private TblNotificationData _TblNotification = new TblNotificationData(6000, 100);
        private TblNotificationGatewayData _TblNotificationGateway = new TblNotificationGatewayData(2000, 100);
        private TblNotificationQueueData _TblNotificationQueue = new TblNotificationQueueData(7000, 100);
        private TblOfferData _TblOffer = new TblOfferData(2000, 100);
        private TblPersonData _TblPerson = new TblPersonData(50000, 500);
        private TblPersonKindData _TblPersonKind = new TblPersonKindData(0, 0);
        private TblPhoneData _TblPhone = new TblPhoneData(50000, 500);
        private TblPrioritySearchQueueData _TblPrioritySearchQueue = new TblPrioritySearchQueueData(0, 0);
        private TblProfileData _TblProfile = new TblProfileData(50000, 500);
        private TblRegionData _TblRegion = new TblRegionData(5000, 100);
        private TblSearchThrottleData _TblSearchThrottle = new TblSearchThrottleData(0, 0);
        private TblSmtpAddressData _TblSmtpAddress = new TblSmtpAddressData(0, 0);
        private TblSubscriberData _TblSubscriber = new TblSubscriberData(10000, 100);
        private TblTimeZoneData _TblTimeZone = new TblTimeZoneData(0, 0);
        private TblTransactionData _TblTransaction = new TblTransactionData(5000, 100);
        private TblWebAddressData _TblWebAddress = new TblWebAddressData(10000, 100);
        private TblZipCodeData _TblZipCode = new TblZipCodeData(20000, 100);
        private TypeAccountData _TypeAccount = new TypeAccountData(0, 0);
        private TypeCarrierData _TypeCarrier = new TypeCarrierData(0, 0);
        private TypeCommunicationData _TypeCommunication = new TypeCommunicationData(0, 0);
        private TypeCreditCardData _TypeCreditCard = new TypeCreditCardData(0, 0);
        private TypeDataAccessData _TypeDataAccess = new TypeDataAccessData(0, 0);
        private TypeDataInputData _TypeDataInput = new TypeDataInputData(0, 0);
        private TypeDataManagementData _TypeDataManagement = new TypeDataManagementData(0, 0);
        private TypeHarvesterActionData _TypeHarvesterAction = new TypeHarvesterActionData(0, 0);
        private TypeLinkSourceData _TypeLinkSource = new TypeLinkSourceData(0, 0);
        private TypeLocationData _TypeLocation = new TypeLocationData(0, 0);
        private TypeMediaSourceData _TypeMediaSource = new TypeMediaSourceData(0, 0);
        private TypeNotificationData _TypeNotification = new TypeNotificationData(0, 0);
        private TypeNotificationChannelData _TypeNotificationChannel = new TypeNotificationChannelData(0, 0);
        private TypeNotificationGatewayData _TypeNotificationGateway = new TypeNotificationGatewayData(0, 0);
        private TypeNotificationVendorData _TypeNotificationVendor = new TypeNotificationVendorData(0, 0);
        private TypePingCycleData _TypePingCycle = new TypePingCycleData(0, 0);
        private TypePrefixData _TypePrefix = new TypePrefixData(0, 0);
        private TypeRelationshipData _TypeRelationship = new TypeRelationshipData(0, 0);
        private TypeServerMessageData _TypeServerMessage = new TypeServerMessageData(0, 0);
        private TypeSourceData _TypeSource = new TypeSourceData(0, 0);
        private TypeTitleData _TypeTitle = new TypeTitleData(0, 0);
        private TypeZipCodeData _TypeZipCode = new TypeZipCodeData(0, 0);


        public static TableTypes GetTableType(string table)
        {
            TableTypes type = TableTypes.TTABLEINVALID;
            if (Enum.IsDefined(typeof(TableTypes), table))
                type = (TableTypes)Enum.Parse(typeof(TableTypes), table, true);
            return type;
        }

        public void Init(FieldsDictionary fields)
        {
            m_Fields = fields;
            _tables.TryAdd(TableTypes.TFltDevice_DataInput, _FltDevice_DataInput);
            _tables.TryAdd(TableTypes.TFltProfile_DataAccess, _FltProfile_DataAccess);
            _tables.TryAdd(TableTypes.TFltProfile_DataManagement, _FltProfile_DataManagement);
            _tables.TryAdd(TableTypes.TStatusFieldUpdates, _StatusFieldUpdates);
            _tables.TryAdd(TableTypes.TStatusSubscribers, _StatusSubscribers);
            _tables.TryAdd(TableTypes.TTblAccessKey, _TblAccessKey);
            _tables.TryAdd(TableTypes.TTblApplication, _TblApplication);
            _tables.TryAdd(TableTypes.TTblAreaCode, _TblAreaCode);
            _tables.TryAdd(TableTypes.TTblAreaCodeRegion, _TblAreaCodeRegion);
            _tables.TryAdd(TableTypes.TTblCity, _TblCity);
            _tables.TryAdd(TableTypes.TTblCompany, _TblCompany);
            _tables.TryAdd(TableTypes.TTblContact, _TblContact);
            _tables.TryAdd(TableTypes.TTblCountry, _TblCountry);
            _tables.TryAdd(TableTypes.TTblCreditCard, _TblCreditCard);
            _tables.TryAdd(TableTypes.TTblDataSource, _TblDataSource);
            _tables.TryAdd(TableTypes.TTblDataSourceLogin, _TblDataSourceLogin);
            _tables.TryAdd(TableTypes.TTblDevice, _TblDevice);
            _tables.TryAdd(TableTypes.TTblDeviceConfirmationCode, _TblDeviceConfirmationCode);
            _tables.TryAdd(TableTypes.TTblDeviceManufacturer, _TblDeviceManufacturer);
            _tables.TryAdd(TableTypes.TTblDeviceModel, _TblDeviceModel);
            _tables.TryAdd(TableTypes.TTblDMA, _TblDMA);
            _tables.TryAdd(TableTypes.TTblEmail, _TblEmail);
            _tables.TryAdd(TableTypes.TTblGroup, _TblGroup);
            _tables.TryAdd(TableTypes.TTblInterestArea, _TblInterestArea);
            _tables.TryAdd(TableTypes.TTblInterestAttribute, _TblInterestAttribute);
            _tables.TryAdd(TableTypes.TTblLocation, _TblLocation);
            _tables.TryAdd(TableTypes.TTblLocationCity, _TblLocationCity);
            _tables.TryAdd(TableTypes.TTblLocationCountry, _TblLocationCountry);
            _tables.TryAdd(TableTypes.TTblLocationState, _TblLocationState);
            _tables.TryAdd(TableTypes.TTblLocationStreet, _TblLocationStreet);
            _tables.TryAdd(TableTypes.TTblLocationStreet2, _TblLocationStreet2);
            _tables.TryAdd(TableTypes.TTblLocationZipCode, _TblLocationZipCode);
            _tables.TryAdd(TableTypes.TTblMedia, _TblMedia);
            _tables.TryAdd(TableTypes.TTblMerchant, _TblMerchant);
            _tables.TryAdd(TableTypes.TTblMetroArea, _TblMetroArea);
            _tables.TryAdd(TableTypes.TTblNewsChannel, _TblNewsChannel);
            _tables.TryAdd(TableTypes.TTblNewsItem, _TblNewsItem);
            _tables.TryAdd(TableTypes.TTblNote, _TblNote);
            _tables.TryAdd(TableTypes.TTblNotification, _TblNotification);
            _tables.TryAdd(TableTypes.TTblNotificationGateway, _TblNotificationGateway);
            _tables.TryAdd(TableTypes.TTblNotificationQueue, _TblNotificationQueue);
            _tables.TryAdd(TableTypes.TTblOffer, _TblOffer);
            _tables.TryAdd(TableTypes.TTblPerson, _TblPerson);
            _tables.TryAdd(TableTypes.TTblPersonKind, _TblPersonKind);
            _tables.TryAdd(TableTypes.TTblPhone, _TblPhone);
            _tables.TryAdd(TableTypes.TTblPrioritySearchQueue, _TblPrioritySearchQueue);
            _tables.TryAdd(TableTypes.TTblProfile, _TblProfile);
            _tables.TryAdd(TableTypes.TTblRegion, _TblRegion);
            _tables.TryAdd(TableTypes.TTblSearchThrottle, _TblSearchThrottle);
            _tables.TryAdd(TableTypes.TTblSmtpAddress, _TblSmtpAddress);
            _tables.TryAdd(TableTypes.TTblSubscriber, _TblSubscriber);
            _tables.TryAdd(TableTypes.TTblTimeZone, _TblTimeZone);
            _tables.TryAdd(TableTypes.TTblTransaction, _TblTransaction);
            _tables.TryAdd(TableTypes.TTblWebAddress, _TblWebAddress);
            _tables.TryAdd(TableTypes.TTblZipCode, _TblZipCode);
            _tables.TryAdd(TableTypes.TTypeAccount, _TypeAccount);
            _tables.TryAdd(TableTypes.TTypeCarrier, _TypeCarrier);
            _tables.TryAdd(TableTypes.TTypeCommunication, _TypeCommunication);
            _tables.TryAdd(TableTypes.TTypeCreditCard, _TypeCreditCard);
            _tables.TryAdd(TableTypes.TTypeDataAccess, _TypeDataAccess);
            _tables.TryAdd(TableTypes.TTypeDataInput, _TypeDataInput);
            _tables.TryAdd(TableTypes.TTypeDataManagement, _TypeDataManagement);
            _tables.TryAdd(TableTypes.TTypeHarvesterAction, _TypeHarvesterAction);
            _tables.TryAdd(TableTypes.TTypeLinkSource, _TypeLinkSource);
            _tables.TryAdd(TableTypes.TTypeLocation, _TypeLocation);
            _tables.TryAdd(TableTypes.TTypeMediaSource, _TypeMediaSource);
            _tables.TryAdd(TableTypes.TTypeNotification, _TypeNotification);
            _tables.TryAdd(TableTypes.TTypeNotificationChannel, _TypeNotificationChannel);
            _tables.TryAdd(TableTypes.TTypeNotificationGateway, _TypeNotificationGateway);
            _tables.TryAdd(TableTypes.TTypeNotificationVendor, _TypeNotificationVendor);
            _tables.TryAdd(TableTypes.TTypePingCycle, _TypePingCycle);
            _tables.TryAdd(TableTypes.TTypePrefix, _TypePrefix);
            _tables.TryAdd(TableTypes.TTypeRelationship, _TypeRelationship);
            _tables.TryAdd(TableTypes.TTypeServerMessage, _TypeServerMessage);
            _tables.TryAdd(TableTypes.TTypeSource, _TypeSource);
            _tables.TryAdd(TableTypes.TTypeTitle, _TypeTitle);
            _tables.TryAdd(TableTypes.TTypeZipCode, _TypeZipCode);

            LoadData();
        }
    }
}
