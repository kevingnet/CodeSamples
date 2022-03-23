using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Concurrent;
using System.Data.SqlTypes;

namespace JakeKnowsEngineComponent
{
    public enum FieldTypes
    {
        FieldType_Plain,
        FieldType_ID,
        FieldType_Type,
        FieldType_Status
    }

    public enum Input_Types
    {
        IN_string,
        IN_int,
        IN_decimal,
        IN_DateTime,
        IN_bool,
        IN_float
    }

    public enum SQL_Types
    {
        SQL_bigint,
        SQL_bit,
        SQL_datetime,
        SQL_decimal,
        SQL_float,
        SQL_image,
        SQL_int,
        SQL_money,
        SQL_nvarchar,
        SQL_smallint,
        SQL_tinyint,
        SQL_varchar
    }
    public class InputFieldInfo
    {
        public string Name { get; private set; }
        public bool IsIdentity { get; private set; }
        public bool IsSecure { get; private set; }
        public bool IsRequired { get; private set; }
        public Input_Types Type { get; private set; }
        public string ConvertTo { get; private set; }
        public string ValidateTo { get; private set; }
        public string ResolvesTo { get; private set; }

        private InputFieldInfo() { }
        public InputFieldInfo(string name, bool id, bool secure, bool required, Input_Types inputType, string sqlType, string validatesTo, string resolvesTo)
        {
            Name = name;
            IsIdentity = id;
            IsSecure = secure;
            IsRequired = required;
            Type = inputType;
            ConvertTo = sqlType;
            ValidateTo = validatesTo;
            ResolvesTo = resolvesTo;
        }
    }

    public enum ColumnTypes
    {
        TCOLUMNINVALID,
        TIDAccessKey,
        TIDApplication,
        TIDAreaCode,
        TIDCity,
        TIDCompany,
        TIDContact,
        TIDCountry,
        TIDCreditCard,
        TIDDataSource,
        TIDDataSourceLogin,
        TIDDevice,
        TIDDeviceManufacturer,
        TIDDeviceModel,
        TIDDMA,
        TIDEmail,
        TIDGroup,
        TIDHarvesterSystem,
        TIDInterestArea,
        TIDInterestAttribute,
        TIDLocation,
        TIDLocationCity,
        TIDLocationCountry,
        TIDLocationState,
        TIDLocationStreet,
        TIDLocationStreet2,
        TIDLocationZipCode,
        TIDMedia,
        TIDMerchant,
        TIDNewsChannel,
        TIDNewsItem,
        TIDNote,
        TIDNotification,
        TIDNotificationGateway,
        TIDNotificationQueue,
        TIDOffer,
        TIDPerson,
        TIDPersonKind,
        TIDPhone,
        TIDProfile,
        TIDProfileDataAccessMasterFilter,
        TIDProfileDataManagementMasterFilter,
        TIDRegion,
        TIDResult,
        TIDSearchThrottle,
        TIDSmtpAddress,
        TIDSubscriber,
        TIDTimeZone,
        TIDTransaction,
        TIDWebAddress,
        TIDZipCode,
        TSTFieldUpdate,
        TSTSubscriber,
        TTYAccount,
        TTYCarrier,
        TTYCommunication,
        TTYCreditCard,
        TTYDataAccess,
        TTYDataInput,
        TTYDataManagement,
        TTYHarvesterAction,
        TTYLinkSource,
        TTYLocation,
        TTYMediaSource,
        TTYNotification,
        TTYNotificationChannel,
        TTYNotificationGateway,
        TTYNotificationVendor,
        TTYPingCycle,
        TTYPrefix,
        TTYRelationship,
        TTYServerMessage,
        TTYSource,
        TTYTitle,
        TTYZipCode,
        TTYZipCodeLocation,
    }

    public class FieldsDictionary
    {
        static int initialCapacity = 101;
        int numProcs = Environment.ProcessorCount;
        static int concurrencyLevel = Environment.ProcessorCount * 2;

        ConcurrentDictionary<ColumnTypes, DataObjects.TableTypes> _idFieldTableMap = new ConcurrentDictionary<ColumnTypes, DataObjects.TableTypes>();
        ConcurrentDictionary<string, InputFieldInfo> _InputFieldMapper = new ConcurrentDictionary<string, InputFieldInfo>(concurrencyLevel, initialCapacity);

        public DataObjects.TableTypes GetTableType(ColumnTypes field)
        {
            return _idFieldTableMap[field];
        }

        public string GetNewName(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.Name;
        }
        public string GetValidateTo(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.ValidateTo;
        }
        public string GetResolveTo(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.ResolvesTo;
        }
        public bool IsIdentity(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.IsIdentity;
        }
        public bool IsSecure(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.IsSecure;
        }
        public bool IsRequred(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.IsRequired;
        }
        public Input_Types GetType(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.Type;
        }
        public string GetConvertTo(string param)
        {
            InputFieldInfo fi = _InputFieldMapper[param];
            return fi.ConvertTo;
        }

        public static bool IsID(string col)
        {
            return col.Substring(0, 2) == "ID";
        }

        public static bool IsType(string col)
        {
            return col.Substring(0, 2) == "TY";
        }

        public static bool IsStatus(string col)
        {
            return col.Substring(0, 2) == "ST";
        }

        public static FieldTypes GetFieldType(string type)
        {
            switch (type.Substring(0, 2))
            {
                case "ID":
                    return FieldTypes.FieldType_ID;
                case "TY":
                    return FieldTypes.FieldType_Type;
                case "ST":
                    return FieldTypes.FieldType_Status;
                default:
                    return FieldTypes.FieldType_Plain;
            }
        }

        public static SQL_Types GetSQL_Type(string type)
        {
            switch (type)
            {
                case "bigint":
                    return SQL_Types.SQL_bigint;
                case "bit":
                    return SQL_Types.SQL_bit;
                case "datetime":
                    return SQL_Types.SQL_datetime;
                case "decimal":
                    return SQL_Types.SQL_decimal;
                case "float":
                    return SQL_Types.SQL_float;
                case "image":
                    return SQL_Types.SQL_image;
                case "int":
                    return SQL_Types.SQL_int;
                case "money":
                    return SQL_Types.SQL_money;
                case "nvarchar":
                    return SQL_Types.SQL_nvarchar;
                case "smallint":
                    return SQL_Types.SQL_smallint;
                case "tinyint":
                    return SQL_Types.SQL_tinyint;
                case "varchar":
                    return SQL_Types.SQL_varchar;
                default:
                    return SQL_Types.SQL_tinyint;
            }
        }

        public static Input_Types GetInput_Type(string type)
        {
            switch (type)
            {
                case "string":
                    return Input_Types.IN_string;
                case "int":
                    return Input_Types.IN_int;
                case "DateTime":
                    return Input_Types.IN_DateTime;
                case "decimal":
                    return Input_Types.IN_decimal;
                case "double":
                    return Input_Types.IN_float;
                case "bool":
                    return Input_Types.IN_bool;
                default:
                    return Input_Types.IN_string;
            }
        }

        public void Init()
        {
            _idFieldTableMap[ColumnTypes.TIDAccessKey] = DataObjects.TableTypes.TTblAccessKey;
            _idFieldTableMap[ColumnTypes.TIDApplication] = DataObjects.TableTypes.TTblApplication;
            _idFieldTableMap[ColumnTypes.TIDAreaCode] = DataObjects.TableTypes.TTblAreaCodeRegion;
            _idFieldTableMap[ColumnTypes.TIDCity] = DataObjects.TableTypes.TTblCity;
            _idFieldTableMap[ColumnTypes.TIDCompany] = DataObjects.TableTypes.TTblCompany;
            _idFieldTableMap[ColumnTypes.TIDContact] = DataObjects.TableTypes.TTblContact;
            _idFieldTableMap[ColumnTypes.TIDCountry] = DataObjects.TableTypes.TTblCountry;
            _idFieldTableMap[ColumnTypes.TIDCreditCard] = DataObjects.TableTypes.TTblCreditCard;
            _idFieldTableMap[ColumnTypes.TIDDataSource] = DataObjects.TableTypes.TTblDataSource;
            _idFieldTableMap[ColumnTypes.TIDDataSourceLogin] = DataObjects.TableTypes.TTblDataSourceLogin;
            _idFieldTableMap[ColumnTypes.TIDDevice] = DataObjects.TableTypes.TTblDevice;
            _idFieldTableMap[ColumnTypes.TIDDeviceManufacturer] = DataObjects.TableTypes.TTblDeviceManufacturer;
            _idFieldTableMap[ColumnTypes.TIDDeviceModel] = DataObjects.TableTypes.TTblDeviceModel;
            _idFieldTableMap[ColumnTypes.TIDDMA] = DataObjects.TableTypes.TTblDMA;
            _idFieldTableMap[ColumnTypes.TIDEmail] = DataObjects.TableTypes.TTblEmail;
            _idFieldTableMap[ColumnTypes.TIDGroup] = DataObjects.TableTypes.TTblGroup;
            _idFieldTableMap[ColumnTypes.TIDHarvesterSystem] = DataObjects.TableTypes.THrvHarvesterSystem;
            _idFieldTableMap[ColumnTypes.TIDInterestArea] = DataObjects.TableTypes.TTblInterestArea;
            _idFieldTableMap[ColumnTypes.TIDInterestAttribute] = DataObjects.TableTypes.TTblInterestAttribute;
            _idFieldTableMap[ColumnTypes.TIDLocation] = DataObjects.TableTypes.TTblLocation;
            _idFieldTableMap[ColumnTypes.TIDLocationCity] = DataObjects.TableTypes.TTblLocationCity;
            _idFieldTableMap[ColumnTypes.TIDLocationCountry] = DataObjects.TableTypes.TTblLocationCountry;
            _idFieldTableMap[ColumnTypes.TIDLocationState] = DataObjects.TableTypes.TTblLocationState;
            _idFieldTableMap[ColumnTypes.TIDLocationStreet] = DataObjects.TableTypes.TTblLocationStreet;
            _idFieldTableMap[ColumnTypes.TIDLocationStreet2] = DataObjects.TableTypes.TTblLocationStreet2;
            _idFieldTableMap[ColumnTypes.TIDLocationZipCode] = DataObjects.TableTypes.TTblLocationZipCode;
            _idFieldTableMap[ColumnTypes.TIDMedia] = DataObjects.TableTypes.TTblMedia;
            _idFieldTableMap[ColumnTypes.TIDMerchant] = DataObjects.TableTypes.TTblMerchant;
            _idFieldTableMap[ColumnTypes.TIDNewsChannel] = DataObjects.TableTypes.TTblNewsChannel;
            _idFieldTableMap[ColumnTypes.TIDNewsItem] = DataObjects.TableTypes.TTblNewsItem;
            _idFieldTableMap[ColumnTypes.TIDNote] = DataObjects.TableTypes.TTblNote;
            _idFieldTableMap[ColumnTypes.TIDNotification] = DataObjects.TableTypes.TTblNotification;
            _idFieldTableMap[ColumnTypes.TIDNotificationGateway] = DataObjects.TableTypes.TTblNotificationGateway;
            _idFieldTableMap[ColumnTypes.TIDNotificationQueue] = DataObjects.TableTypes.TTblNotificationQueue;
            _idFieldTableMap[ColumnTypes.TIDOffer] = DataObjects.TableTypes.TTblOffer;
            _idFieldTableMap[ColumnTypes.TIDPerson] = DataObjects.TableTypes.TTblPerson;
            _idFieldTableMap[ColumnTypes.TIDPersonKind] = DataObjects.TableTypes.TTblPersonKind;
            _idFieldTableMap[ColumnTypes.TIDPhone] = DataObjects.TableTypes.TTblPhone;
            _idFieldTableMap[ColumnTypes.TIDProfile] = DataObjects.TableTypes.TTblProfile;
            _idFieldTableMap[ColumnTypes.TIDProfileDataAccessMasterFilter] = DataObjects.TableTypes.TFltProfile_DataAccess;
            _idFieldTableMap[ColumnTypes.TIDProfileDataManagementMasterFilter] = DataObjects.TableTypes.TFltProfile_DataManagement;
            _idFieldTableMap[ColumnTypes.TIDRegion] = DataObjects.TableTypes.TTblRegion;
            _idFieldTableMap[ColumnTypes.TIDResult] = DataObjects.TableTypes.THrvHarvesterResultQueue;
            _idFieldTableMap[ColumnTypes.TIDSearchThrottle] = DataObjects.TableTypes.TTblSearchThrottle;
            _idFieldTableMap[ColumnTypes.TIDSmtpAddress] = DataObjects.TableTypes.TTblSmtpAddress;
            _idFieldTableMap[ColumnTypes.TIDSubscriber] = DataObjects.TableTypes.TTblSubscriber;
            _idFieldTableMap[ColumnTypes.TIDTimeZone] = DataObjects.TableTypes.TTblTimeZone;
            _idFieldTableMap[ColumnTypes.TIDTransaction] = DataObjects.TableTypes.TTblTransaction;
            _idFieldTableMap[ColumnTypes.TIDWebAddress] = DataObjects.TableTypes.TTblWebAddress;
            _idFieldTableMap[ColumnTypes.TIDZipCode] = DataObjects.TableTypes.TTblZipCode;
            _idFieldTableMap[ColumnTypes.TSTFieldUpdate] = DataObjects.TableTypes.TStatusFieldUpdates;
            _idFieldTableMap[ColumnTypes.TSTSubscriber] = DataObjects.TableTypes.TStatusSubscribers;
            _idFieldTableMap[ColumnTypes.TTYAccount] = DataObjects.TableTypes.TTypeAccount;
            _idFieldTableMap[ColumnTypes.TTYCarrier] = DataObjects.TableTypes.TTypeCarrier;
            _idFieldTableMap[ColumnTypes.TTYCommunication] = DataObjects.TableTypes.TTypeCommunication;
            _idFieldTableMap[ColumnTypes.TTYCreditCard] = DataObjects.TableTypes.TTypeCreditCard;
            _idFieldTableMap[ColumnTypes.TTYDataAccess] = DataObjects.TableTypes.TTypeDataAccess;
            _idFieldTableMap[ColumnTypes.TTYDataInput] = DataObjects.TableTypes.TTypeDataInput;
            _idFieldTableMap[ColumnTypes.TTYDataManagement] = DataObjects.TableTypes.TTypeDataManagement;
            _idFieldTableMap[ColumnTypes.TTYHarvesterAction] = DataObjects.TableTypes.TTypeHarvesterAction;
            _idFieldTableMap[ColumnTypes.TTYLinkSource] = DataObjects.TableTypes.TTypeLinkSource;
            _idFieldTableMap[ColumnTypes.TTYLocation] = DataObjects.TableTypes.TTypeLocation;
            _idFieldTableMap[ColumnTypes.TTYMediaSource] = DataObjects.TableTypes.TTypeMediaSource;
            _idFieldTableMap[ColumnTypes.TTYNotification] = DataObjects.TableTypes.TTypeNotification;
            _idFieldTableMap[ColumnTypes.TTYNotificationChannel] = DataObjects.TableTypes.TTypeNotificationChannel;
            _idFieldTableMap[ColumnTypes.TTYNotificationGateway] = DataObjects.TableTypes.TTypeNotificationGateway;
            _idFieldTableMap[ColumnTypes.TTYNotificationVendor] = DataObjects.TableTypes.TTypeNotificationVendor;
            _idFieldTableMap[ColumnTypes.TTYPingCycle] = DataObjects.TableTypes.TTypePingCycle;
            _idFieldTableMap[ColumnTypes.TTYPrefix] = DataObjects.TableTypes.TTypePrefix;
            _idFieldTableMap[ColumnTypes.TTYRelationship] = DataObjects.TableTypes.TTypeRelationship;
            _idFieldTableMap[ColumnTypes.TTYServerMessage] = DataObjects.TableTypes.TTypeServerMessage;
            _idFieldTableMap[ColumnTypes.TTYSource] = DataObjects.TableTypes.TTypeSource;
            _idFieldTableMap[ColumnTypes.TTYTitle] = DataObjects.TableTypes.TTypeTitle;
            _idFieldTableMap[ColumnTypes.TTYZipCode] = DataObjects.TableTypes.TTypeZipCode;
            _idFieldTableMap[ColumnTypes.TTYZipCodeLocation] = DataObjects.TableTypes.TTypeZipCodeLocation;

            _InputFieldMapper[""] = new InputFieldInfo("", false, false, true, GetInput_Type(""), "", "", "");
            _InputFieldMapper["accessKey"] = new InputFieldInfo("AccessKey", false, true, true, GetInput_Type("string"), "String", "GUID", "AccessKey");
            _InputFieldMapper["AccountSid"] = new InputFieldInfo("SID", false, false, false, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["allMembersFlag"] = new InputFieldInfo("UseAllMembers", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["annotation"] = new InputFieldInfo("Annotation", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["appConB64String"] = new InputFieldInfo("ApplicationData", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["applicationId"] = new InputFieldInfo("IDApplication", true, false, true, GetInput_Type("int"), "Int32", "", "IDApplication");
            _InputFieldMapper["approvalStatus"] = new InputFieldInfo("ApprovalStatus", false, false, true, GetInput_Type("int"), "Int64", "", "ApprovalStatus");
            _InputFieldMapper["attributes"] = new InputFieldInfo("Attributes", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["billingCity"] = new InputFieldInfo("CityBilling", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationCity");
            _InputFieldMapper["billingCountryCode"] = new InputFieldInfo("CountryCodeBilling", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationCountry");
            _InputFieldMapper["billingPhoneNumber"] = new InputFieldInfo("PhoneBilling", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["billingPostalCode"] = new InputFieldInfo("ZipCodeBilling", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationZipCode");
            _InputFieldMapper["billingState"] = new InputFieldInfo("StateBilling", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationState");
            _InputFieldMapper["billingStreet1"] = new InputFieldInfo("Street1Billing", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationStreet");
            _InputFieldMapper["billingStreet2"] = new InputFieldInfo("Street2Billing", false, false, true, GetInput_Type("string"), "String", "string", "IDLocationStreet2");
            _InputFieldMapper["Body"] = new InputFieldInfo("MessageBody", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["cardType"] = new InputFieldInfo("CreditCard", false, false, true, GetInput_Type("string"), "String", "string", "TYCreditCard");
            _InputFieldMapper["ccDataId"] = new InputFieldInfo("IDCreditCard", true, false, true, GetInput_Type("int"), "Int64", "", "IDCreditCard");
            _InputFieldMapper["ccNumber"] = new InputFieldInfo("CreditCardNumber", false, true, true, GetInput_Type("string"), "String", "string", "CreditCardNumber");
            _InputFieldMapper["ccSelfFlag"] = new InputFieldInfo("IsSelf", false, false, false, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["code"] = new InputFieldInfo("CreditCardCode", false, true, true, GetInput_Type("string"), "String", "string", "CreditCardCode");
            _InputFieldMapper["comData"] = new InputFieldInfo("CommunicationData", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["communicationsChannel"] = new InputFieldInfo("CommunicationsChannel", false, false, true, GetInput_Type("int"), "Int32", "", "TYCommunication");
            _InputFieldMapper["communicationsChannel"] = new InputFieldInfo("CommunicationsChannel", false, false, true, GetInput_Type("string"), "Int32", "int", "TYCommunication");
            _InputFieldMapper["communicationsType"] = new InputFieldInfo("CommunicationsType", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["companyName"] = new InputFieldInfo("Company", false, false, true, GetInput_Type("string"), "String", "string", "IDCompany");
            _InputFieldMapper["completeFlag"] = new InputFieldInfo("HasCompleted", false, false, true, GetInput_Type("int"), "Boolean", "", "HasCompleted");
            _InputFieldMapper["compressionFlagInput"] = new InputFieldInfo("CompressInput", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["compressionFlagOutput"] = new InputFieldInfo("CompressOutput", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["confirmationCode"] = new InputFieldInfo("ConfirmationCode", false, false, true, GetInput_Type("string"), "String", "string", "ConfirmationCode");
            _InputFieldMapper["confirmFaxPhone"] = new InputFieldInfo("ConfirmPhoneFax", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmFirstName"] = new InputFieldInfo("ConfirmFirstName", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmFlag"] = new InputFieldInfo("Confirm", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["confirmFlag"] = new InputFieldInfo("Confirm", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmHome2Phone"] = new InputFieldInfo("ConfirmPhoneHome2", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmHomeAddress"] = new InputFieldInfo("ConfirmAddressHome", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmHomePhone"] = new InputFieldInfo("ConfirmPhoneHome", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmLastName"] = new InputFieldInfo("ConfirmLastName", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmMedia"] = new InputFieldInfo("ConfirmMedia", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmMobilePhone"] = new InputFieldInfo("ConfirmPhoneMobile", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmNote"] = new InputFieldInfo("ConfirmNote", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmOrg"] = new InputFieldInfo("ConfirmCompany", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmOtherPhone"] = new InputFieldInfo("ConfirmPhoneOther", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmPagerPhone"] = new InputFieldInfo("ConfirmPhonePager", false, false, true, GetInput_Type("string"), "String", "phone", "");
            _InputFieldMapper["confirmPrefix"] = new InputFieldInfo("ConfirmPrefix", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmTitle"] = new InputFieldInfo("ConfirmTitle", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmWork2Phone"] = new InputFieldInfo("ConfirmPhoneWork2", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmWorkAddress"] = new InputFieldInfo("ConfirmAddressWork", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["confirmWorkPhone"] = new InputFieldInfo("ConfirmPhoneWork", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["contactCount"] = new InputFieldInfo("ContactCount", false, false, false, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["contactCount"] = new InputFieldInfo("ContactCount", false, false, false, GetInput_Type("string"), "Int32", "int", "");
            _InputFieldMapper["contactCount"] = new InputFieldInfo("ContactCount", false, false, false, GetInput_Type("string"), "Int64", "int", "");
            _InputFieldMapper["contactEmail"] = new InputFieldInfo("Email", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail1"] = new InputFieldInfo("Email1", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail2"] = new InputFieldInfo("Email2", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail3"] = new InputFieldInfo("Email3", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail4"] = new InputFieldInfo("Email4", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail5"] = new InputFieldInfo("Email5", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactEmail6"] = new InputFieldInfo("Email6", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["contactFirstName"] = new InputFieldInfo("FirstName", false, false, true, GetInput_Type("string"), "String", "name", "IDPerson");
            _InputFieldMapper["contactLastName"] = new InputFieldInfo("LastName", false, false, true, GetInput_Type("string"), "String", "name", "IDPerson");
            _InputFieldMapper["contactOrg"] = new InputFieldInfo("Company", false, false, true, GetInput_Type("string"), "String", "string", "IDCompany");
            _InputFieldMapper["contactPrefix"] = new InputFieldInfo("Prefix", false, false, true, GetInput_Type("string"), "String", "string", "TYPrefix");
            _InputFieldMapper["contactTitle"] = new InputFieldInfo("Title", false, false, true, GetInput_Type("string"), "String", "string", "TYTitle");
            _InputFieldMapper["contactUpdateSetting"] = new InputFieldInfo("DataInput", false, false, true, GetInput_Type("int"), "Int32", "", "TYDataInput");
            _InputFieldMapper["contactXML"] = new InputFieldInfo("XMLContact", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["correctFlag"] = new InputFieldInfo("IsCorrect", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["dataSource"] = new InputFieldInfo("DataSource", false, false, true, GetInput_Type("string"), "String", "string", "IDDataSource");
            _InputFieldMapper["dayCount"] = new InputFieldInfo("DayCount", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["deviceChunkSize"] = new InputFieldInfo("ChunkCount", false, false, true, GetInput_Type("string"), "Int32", "int", "ChunkCount");
            _InputFieldMapper["deviceId"] = new InputFieldInfo("IDDevice", true, false, true, GetInput_Type("int"), "Int64", "", "IDDevice");
            _InputFieldMapper["deviceManufacturer"] = new InputFieldInfo("DeviceManufacturer", false, false, true, GetInput_Type("string"), "String", "string", "IDDeviceManufacturer");
            _InputFieldMapper["deviceModel"] = new InputFieldInfo("DeviceModel", false, false, true, GetInput_Type("string"), "String", "string", "IDDeviceModel");
            _InputFieldMapper["deviceNetwork"] = new InputFieldInfo("Carrier", false, false, true, GetInput_Type("string"), "String", "string", "TYCarrier");
            _InputFieldMapper["deviceOSVersion"] = new InputFieldInfo("VersionOS", false, false, true, GetInput_Type("string"), "String", "string", "VersionOS");
            _InputFieldMapper["devicePhoneNumber"] = new InputFieldInfo("PhoneNumber", false, false, true, GetInput_Type("string"), "String", "phone", "PhoneNumber");
            _InputFieldMapper["devicePIN"] = new InputFieldInfo("PIN", false, true, true, GetInput_Type("string"), "String", "string", "PIN");
            _InputFieldMapper["deviceToken"] = new InputFieldInfo("Token", false, true, true, GetInput_Type("string"), "String", "string", "Token");
            _InputFieldMapper["deviceUID"] = new InputFieldInfo("Device", false, true, true, GetInput_Type("string"), "", "string", "IDDevice");
            _InputFieldMapper["disableCommunicationsFlag"] = new InputFieldInfo("DisableCommunication", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["dropProfileFlag"] = new InputFieldInfo("DropProfile", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["echoString"] = new InputFieldInfo("Echo", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["email"] = new InputFieldInfo("Email", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["emailAddress"] = new InputFieldInfo("Email", false, false, true, GetInput_Type("string"), "String", "email", "IDEmail");
            _InputFieldMapper["emailXML"] = new InputFieldInfo("XMLEmail", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["endDay"] = new InputFieldInfo("EndDay", false, false, true, GetInput_Type("int"), "Int64", "", "");
            _InputFieldMapper["faxPhoneNumber"] = new InputFieldInfo("PhoneFax", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["feedUrl"] = new InputFieldInfo("FeedUrl", false, false, true, GetInput_Type("string"), "String", "url", "");
            _InputFieldMapper["firstName"] = new InputFieldInfo("FirstName", false, false, true, GetInput_Type("string"), "String", "name", "IDPerson");
            _InputFieldMapper["From"] = new InputFieldInfo("PhoneFrom", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["gatewayKey"] = new InputFieldInfo("GatewayKey", false, true, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["gatewaySystemId"] = new InputFieldInfo("IDNotificationGateway", true, false, false, GetInput_Type("int"), "Int64", "", "IDNotificationGateway");
            _InputFieldMapper["gatewaySystemId"] = new InputFieldInfo("IDNotificationGateway", true, false, true, GetInput_Type("int"), "Int64", "", "IDNotificationGateway");
            _InputFieldMapper["groupCommunicationsType"] = new InputFieldInfo("GroupCommunicationType", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["groupFlag"] = new InputFieldInfo("IsGroup", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["groupFlag"] = new InputFieldInfo("IsGroup", false, false, true, GetInput_Type("string"), "Boolean", "int", "");
            _InputFieldMapper["groupId"] = new InputFieldInfo("IDGroup", true, false, true, GetInput_Type("int"), "Int64", "", "IDGroup");
            _InputFieldMapper["groupId"] = new InputFieldInfo("IDGroup", true, false, true, GetInput_Type("string"), "Int64", "int", "IDGroup");
            _InputFieldMapper["groupName"] = new InputFieldInfo("Group", false, false, true, GetInput_Type("string"), "String", "string", "IDGroup");
            _InputFieldMapper["groupVisibilityFlag"] = new InputFieldInfo("IsVisibleGroup", false, false, true, GetInput_Type("string"), "Boolean", "int", "");
            _InputFieldMapper["harvesterCertificate"] = new InputFieldInfo("HarvesterCertificate", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["harvesterSystemId"] = new InputFieldInfo("HarvesterSystemID", false, false, true, GetInput_Type("int"), "Int64", "", "");
            _InputFieldMapper["home2PhoneNumber"] = new InputFieldInfo("PhoneHome2", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["homeAddress1"] = new InputFieldInfo("AddressHome", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet");
            _InputFieldMapper["homeAddress2"] = new InputFieldInfo("AddressHome2", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet2");
            _InputFieldMapper["homeCity"] = new InputFieldInfo("CityHome", false, false, true, GetInput_Type("string"), "String", "city", "IDLocationCity");
            _InputFieldMapper["homeCountry"] = new InputFieldInfo("CountryHome", false, false, true, GetInput_Type("string"), "String", "country", "IDLocationCountry");
            _InputFieldMapper["homeCountryCode"] = new InputFieldInfo("CountryCodeHome", false, false, true, GetInput_Type("string"), "String", "country", "IDLocationCountry");
            _InputFieldMapper["homePhoneNumber"] = new InputFieldInfo("PhoneHome", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["homePostalCode"] = new InputFieldInfo("ZipCodeHome", false, false, true, GetInput_Type("string"), "String", "zip", "IDLocationZipCode");
            _InputFieldMapper["homeState"] = new InputFieldInfo("StateHome", false, false, true, GetInput_Type("string"), "String", "state", "IDLocationState");
            _InputFieldMapper["imageB64String"] = new InputFieldInfo("Image", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["imageData"] = new InputFieldInfo("Image", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["inlineMediaFlag"] = new InputFieldInfo("HasMedia", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["inlineNoteFlag"] = new InputFieldInfo("HasNote", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["inputDatetime"] = new InputFieldInfo("InputDate", false, false, true, GetInput_Type("DateTime"), "DateTime", "", "");
            _InputFieldMapper["itemParameters"] = new InputFieldInfo("ItemProperties", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["itemParameters"] = new InputFieldInfo("ItemProperties", false, false, true, GetInput_Type("string"), "String", "string", "ItemProperties");
            _InputFieldMapper["jakeVersion"] = new InputFieldInfo("VersionApp", false, false, true, GetInput_Type("string"), "String", "string", "VersionApp");
            _InputFieldMapper["joinableFlag"] = new InputFieldInfo("CanJoin", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["key"] = new InputFieldInfo("Key", false, true, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["lastName"] = new InputFieldInfo("LastName", false, false, true, GetInput_Type("string"), "String", "name", "IDPerson");
            _InputFieldMapper["limit"] = new InputFieldInfo("Limit", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["logString"] = new InputFieldInfo("LogString", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["maxReturn"] = new InputFieldInfo("MaxReturn", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["mediaB64String"] = new InputFieldInfo("Media", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["mediaId"] = new InputFieldInfo("IDMedia", true, false, true, GetInput_Type("int"), "Int64", "", "IDMedia");
            _InputFieldMapper["mediaId_1"] = new InputFieldInfo("IDMedia_1", true, false, true, GetInput_Type("int"), "Int64", "", "IDMedia");
            _InputFieldMapper["mediaId_2"] = new InputFieldInfo("IDMedia_2", true, false, true, GetInput_Type("int"), "Int64", "", "IDMedia");
            _InputFieldMapper["memberCount"] = new InputFieldInfo("MemberCount", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["memberXML"] = new InputFieldInfo("XMLMember", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["messageBody"] = new InputFieldInfo("MessageBody", false, false, true, GetInput_Type("string"), "String", "string", "Body");
            _InputFieldMapper["messageTitle"] = new InputFieldInfo("MessageTitle", false, false, true, GetInput_Type("string"), "String", "string", "Title");
            _InputFieldMapper["mobile"] = new InputFieldInfo("PhoneMobile", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["mobilePhoneNumber"] = new InputFieldInfo("PhoneMobile", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["month"] = new InputFieldInfo("ExpirationMonth", false, false, true, GetInput_Type("string"), "Int16", "int", "ExpirationMonth");
            _InputFieldMapper["name"] = new InputFieldInfo("Name", false, false, true, GetInput_Type("string"), "String", "name", "IDPerson");
            _InputFieldMapper["newProfileOnTieFlag"] = new InputFieldInfo("HasNewProfile", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["noteId"] = new InputFieldInfo("IDNote", true, false, true, GetInput_Type("int"), "Int64", "", "IDNote");
            _InputFieldMapper["noteXML"] = new InputFieldInfo("XMLNote", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["notificationId"] = new InputFieldInfo("IDNotification", true, false, true, GetInput_Type("int"), "Int64", "", "IDNotification");
            _InputFieldMapper["notificationKey"] = new InputFieldInfo("NotificationKey", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["notificationQueueId"] = new InputFieldInfo("IDNotificationQueue", true, false, true, GetInput_Type("int"), "Int64", "", "IDNotificationQueue");
            _InputFieldMapper["offerId"] = new InputFieldInfo("IDOffer", true, false, true, GetInput_Type("int"), "Int64", "", "IDOffer");
            _InputFieldMapper["onlyNonContactsFlag"] = new InputFieldInfo("UseOnlyNonContacts", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["otherPhoneNumber"] = new InputFieldInfo("PhoneOther", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["pagerPhoneNumber"] = new InputFieldInfo("PhonePager", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["period"] = new InputFieldInfo("Period", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["phoneNumber"] = new InputFieldInfo("Phone", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["pimContactUID"] = new InputFieldInfo("PIMContactID", false, false, true, GetInput_Type("string"), "Int64", "int", "PIMContactID");
            _InputFieldMapper["pimContactUID_new"] = new InputFieldInfo("PIMContactID_New", false, false, true, GetInput_Type("string"), "Int64", "int", "PIMContactID");
            _InputFieldMapper["pimContactUID_original"] = new InputFieldInfo("PIMContactID_Original", false, false, true, GetInput_Type("string"), "Int64", "int", "PIMContactID");
            _InputFieldMapper["preferredProfileId"] = new InputFieldInfo("IDProfile_Preferred", true, false, true, GetInput_Type("string"), "Int64", "int", "IDProfile");
            _InputFieldMapper["profileBuilderSetting"] = new InputFieldInfo("ProfileBuilderSetting", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["profileCount"] = new InputFieldInfo("ProfileCount", false, false, true, GetInput_Type("string"), "Int32", "int", "");
            _InputFieldMapper["profileGroupId"] = new InputFieldInfo("IDGroup", true, false, true, GetInput_Type("int"), "Int64", "", "IDGroup");
            _InputFieldMapper["profileId"] = new InputFieldInfo("IDProfile", true, false, true, GetInput_Type("int"), "Int64", "", "IDProfile");
            _InputFieldMapper["profileId"] = new InputFieldInfo("IDProfile", true, false, true, GetInput_Type("string"), "Int64", "int", "IDProfile");
            _InputFieldMapper["profileShareXML"] = new InputFieldInfo("XMLProfileShare", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["profileSharingSetting"] = new InputFieldInfo("ProfileSharingSetting", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["profileXML"] = new InputFieldInfo("XMLProfile", false, false, true, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["purchasedSinceUnixtime"] = new InputFieldInfo("PurchasedSince", false, false, true, GetInput_Type("string"), "DateTime", "date", "");
            _InputFieldMapper["purchasedThroughUnixtime"] = new InputFieldInfo("PurchasedThrough", false, false, true, GetInput_Type("string"), "DateTime", "date", "");
            _InputFieldMapper["quantity"] = new InputFieldInfo("Quantity", false, false, true, GetInput_Type("int"), "Int32", "", "");
            _InputFieldMapper["relationshipType"] = new InputFieldInfo("RelationshipType", false, false, true, GetInput_Type("string"), "String", "string", "TYRelationship");
            _InputFieldMapper["relationshipType"] = new InputFieldInfo("TYRelationship", true, false, true, GetInput_Type("int"), "Int32", "", "TYRelationship");
            _InputFieldMapper["repeatFlag"] = new InputFieldInfo("Repeat", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["request"] = new InputFieldInfo("Request", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["responseFormat"] = new InputFieldInfo("ResponseFormat", false, false, false, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["returnAllFlag"] = new InputFieldInfo("ReturnAll", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["returnDefaultOnly"] = new InputFieldInfo("ReturnDefaultOnly", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["serverId"] = new InputFieldInfo("ServerID", true, false, false, GetInput_Type("string"), "String", "", "");
            _InputFieldMapper["serverKey"] = new InputFieldInfo("ServerKey", false, true, true, GetInput_Type("string"), "String", "GUID", "");
            _InputFieldMapper["shippingCity"] = new InputFieldInfo("CityShipping", false, false, true, GetInput_Type("string"), "String", "city", "IDLocationCity");
            _InputFieldMapper["shippingCountry"] = new InputFieldInfo("CountryShipping", false, false, true, GetInput_Type("string"), "String", "country", "IDLocationCountry");
            _InputFieldMapper["shippingDescription"] = new InputFieldInfo("ShippingDescription", false, false, true, GetInput_Type("string"), "String", "string", "Shipping");
            _InputFieldMapper["shippingPostalCode"] = new InputFieldInfo("ZipCodeShipping", false, false, true, GetInput_Type("string"), "String", "zip", "IDLocationZipCode");
            _InputFieldMapper["shippingState"] = new InputFieldInfo("StateShipping", false, false, true, GetInput_Type("string"), "String", "state", "IDLocationState");
            _InputFieldMapper["shippingStreet1"] = new InputFieldInfo("Street1_Shipping", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet");
            _InputFieldMapper["shippingStreet2"] = new InputFieldInfo("Street2_Shipping", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet2");
            _InputFieldMapper["shippingTotalAmount"] = new InputFieldInfo("ShippingTotal", false, false, true, GetInput_Type("decimal"), "Decimal", "", "");
            _InputFieldMapper["smsNotify"] = new InputFieldInfo("SMSNotify", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["SmsSid"] = new InputFieldInfo("SMSSID", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["SmsStatus"] = new InputFieldInfo("SMSStatus", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["start"] = new InputFieldInfo("Start", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["subject"] = new InputFieldInfo("Subject", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["systemToken"] = new InputFieldInfo("SystemToken", false, false, true, GetInput_Type("string"), "String", "GUID", "");
            _InputFieldMapper["taxAmount"] = new InputFieldInfo("TaxAmount", false, false, true, GetInput_Type("decimal"), "Decimal", "", "");
            _InputFieldMapper["taxDescription"] = new InputFieldInfo("TaxDescription", false, false, true, GetInput_Type("string"), "String", "string", "Tax");
            _InputFieldMapper["testString"] = new InputFieldInfo("Test", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["thisDate"] = new InputFieldInfo("ThisDate", false, false, true, GetInput_Type("string"), "DateTime", "date", "");
            _InputFieldMapper["timeZoneIdString1"] = new InputFieldInfo("TimeZone1", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["timeZoneIdString2"] = new InputFieldInfo("TimeZone2", false, false, true, GetInput_Type("string"), "String", "string", "");
            _InputFieldMapper["To"] = new InputFieldInfo("PhoneTo", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["totalPrice"] = new InputFieldInfo("Total", false, false, true, GetInput_Type("decimal"), "Decimal", "", "");
            _InputFieldMapper["transactionId"] = new InputFieldInfo("IDTransaction", true, false, true, GetInput_Type("int"), "Int64", "", "IDTransaction");
            _InputFieldMapper["updatesOnlyFlag"] = new InputFieldInfo("UpdatesOnly", false, false, true, GetInput_Type("string"), "Boolean", "bool", "");
            _InputFieldMapper["urlEncodeDataFlag"] = new InputFieldInfo("EncodeUrl", false, false, true, GetInput_Type("int"), "Boolean", "", "");
            _InputFieldMapper["usrPwd"] = new InputFieldInfo("Password", false, true, true, GetInput_Type("string"), "String", "password", "Password");
            _InputFieldMapper["work2PhoneNumber"] = new InputFieldInfo("PhoneWork2", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["workAddress1"] = new InputFieldInfo("AddressWork", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet");
            _InputFieldMapper["workAddress2"] = new InputFieldInfo("AddressWork2", false, false, true, GetInput_Type("string"), "String", "address", "IDLocationStreet2");
            _InputFieldMapper["workCity"] = new InputFieldInfo("CityWork", false, false, true, GetInput_Type("string"), "String", "city", "IDLocationCity");
            _InputFieldMapper["workCountry"] = new InputFieldInfo("CountryWork", false, false, true, GetInput_Type("string"), "String", "country", "IDLocationCountry");
            _InputFieldMapper["workPhoneNumber"] = new InputFieldInfo("PhoneWork", false, false, true, GetInput_Type("string"), "String", "phone", "IDPhone");
            _InputFieldMapper["workPostalCode"] = new InputFieldInfo("ZipCodeWork", false, false, true, GetInput_Type("string"), "String", "zip", "IDLocationZipCode");
            _InputFieldMapper["workState"] = new InputFieldInfo("StateWork", false, false, true, GetInput_Type("string"), "String", "state", "IDLocationState");
            _InputFieldMapper["year"] = new InputFieldInfo("ExpirationYear", false, false, true, GetInput_Type("string"), "Int16", "int", "ExpirationYear");

        }
    }
}

/*
    public class MultiMap<V>
    {
        ConcurrentDictionary<string, List<V>> _dictionary = new ConcurrentDictionary<string, List<V>>();

        public void Add(string key, V value)
        {
            List<V> list;
            if (this._dictionary.TryGetValue(key, out list))
            {
                list.Add(value);
            }
            else
            {
                list = new List<V>();
                list.Add(value);
                this._dictionary[key] = list;
            }
        }

        public IEnumerable<string> Keys
        {
            get
            {
                return this._dictionary.Keys;
            }
        }

        public List<V> this[string key]
        {
            get
            {
                List<V> list;
                if (!this._dictionary.TryGetValue(key, out list))
                {
                    list = new List<V>();
                    this._dictionary[key] = list;
                }
                return list;
            }
        }
    }
*/
/*
    public class FieldInfo
    {
        public string TableName { get; private set; }
        public SQL_Types Type { get; private set; }
        public int Len { get; private set; }
        public bool Nullable { get; private set; }
        public bool IsIdentity { get; private set; }
        public FieldTypes FieldType { get; private set; }

        private FieldInfo() { }
        public FieldInfo(string tbl, bool id, SQL_Types type, int len, bool nul, FieldTypes fldType)
        {
            TableName = tbl;
            Type = type;
            Len = len;
            Nullable = nul;
            IsIdentity = id;
            FieldType = fldType;
        }
    }
*/
//MultiMap<FieldInfo> _FieldInfo = new MultiMap<FieldInfo>();

/*
_FieldInfo.Add("AccessKey", new FieldInfo("TblSubscriber", false, GetSQL_Type("varchar"), 50, true, GetFieldType("AccessKey")));
_FieldInfo.Add("AccessKey", new FieldInfo("TblAccessKey", false, GetSQL_Type("varchar"), 20, false, GetFieldType("AccessKey")));
_FieldInfo.Add("AccessKey", new FieldInfo("AssocSubscriber_DataSource", false, GetSQL_Type("varchar"), 50, true, GetFieldType("AccessKey")));
_FieldInfo.Add("AccessLimit", new FieldInfo("TblAccessKey", false, GetSQL_Type("int"), 4, false, GetFieldType("AccessLimit")));
_FieldInfo.Add("AccessString", new FieldInfo("AssocSubscriber_DataSource", false, GetSQL_Type("varchar"), 100, true, GetFieldType("AccessString")));
_FieldInfo.Add("Account", new FieldInfo("TypeAccount", false, GetSQL_Type("varchar"), 20, false, GetFieldType("Account")));
_FieldInfo.Add("Added", new FieldInfo("LnkProfile_Profile_MergeReview", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Added")));
_FieldInfo.Add("Added", new FieldInfo("ReviewProfile_Profile", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Added")));
_FieldInfo.Add("Added", new FieldInfo("HistDeviceToken", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Added")));
_FieldInfo.Add("AddManually", new FieldInfo("AssocOffer_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("AddManually")));
_FieldInfo.Add("ADM1Code", new FieldInfo("TblRegion", false, GetSQL_Type("nvarchar"), 16, true, GetFieldType("ADM1Code")));
_FieldInfo.Add("AdminNotification", new FieldInfo("TblProfile", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("AdminNotification")));
_FieldInfo.Add("AmountShiping", new FieldInfo("TblTransaction", false, GetSQL_Type("money"), 8, true, GetFieldType("AmountShiping")));
_FieldInfo.Add("AmountTax", new FieldInfo("TblTransaction", false, GetSQL_Type("money"), 8, true, GetFieldType("AmountTax")));
_FieldInfo.Add("AmountTotal", new FieldInfo("TblTransaction", false, GetSQL_Type("money"), 8, false, GetFieldType("AmountTotal")));
_FieldInfo.Add("Application", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Application")));
_FieldInfo.Add("ApprovalStatus", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("ApprovalStatus")));
_FieldInfo.Add("ArchiveID", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("ArchiveID")));
_FieldInfo.Add("AreaCode", new FieldInfo("TblAreaCode", false, GetSQL_Type("int"), 4, false, GetFieldType("AreaCode")));
_FieldInfo.Add("AreaCode", new FieldInfo("AssocMetroArea_AreaCode", false, GetSQL_Type("int"), 4, false, GetFieldType("AreaCode")));
_FieldInfo.Add("AreaCode", new FieldInfo("TblAreaCodeRegion", false, GetSQL_Type("int"), 4, false, GetFieldType("AreaCode")));
_FieldInfo.Add("AssociationStrength", new FieldInfo("AssocInterestAttribute_InterestAttribute_Strength", false, GetSQL_Type("int"), 4, false, GetFieldType("AssociationStrength")));
_FieldInfo.Add("AssocStrength", new FieldInfo("AssocProfile_Media", false, GetSQL_Type("int"), 4, false, GetFieldType("AssocStrength")));
_FieldInfo.Add("AttemptsCounter", new FieldInfo("TblDeviceConfirmationCode", false, GetSQL_Type("int"), 4, false, GetFieldType("AttemptsCounter")));
_FieldInfo.Add("AuthenticationLevel", new FieldInfo("TblContact", false, GetSQL_Type("int"), 4, false, GetFieldType("AuthenticationLevel")));
_FieldInfo.Add("BgColor", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 6, false, GetFieldType("BgColor")));
_FieldInfo.Add("BitDepth", new FieldInfo("TblMedia", false, GetSQL_Type("int"), 4, true, GetFieldType("BitDepth")));
_FieldInfo.Add("Body", new FieldInfo("TblNotification", false, GetSQL_Type("varchar"), 4000, true, GetFieldType("Body")));
_FieldInfo.Add("Brief", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Brief")));
_FieldInfo.Add("CanBlock", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanBlock")));
_FieldInfo.Add("CanDrop", new FieldInfo("TblNewsItem", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanDrop")));
_FieldInfo.Add("CanEmail", new FieldInfo("TblMerchant", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanEmail")));
_FieldInfo.Add("CanEmail", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, true, GetFieldType("CanEmail")));
_FieldInfo.Add("CanInsert", new FieldInfo("HrvHarvesterSystem", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanInsert")));
_FieldInfo.Add("CanIVR", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, true, GetFieldType("CanIVR")));
_FieldInfo.Add("CanManageClient", new FieldInfo("TblContact", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanManageClient")));
_FieldInfo.Add("CanProcess", new FieldInfo("ReviewProfile_Application", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("CanProcess")));
_FieldInfo.Add("CanProcess", new FieldInfo("AssocGroup_Application", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanProcess")));
_FieldInfo.Add("CanProcess", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanProcess")));
_FieldInfo.Add("CanSearch", new FieldInfo("LnkProfile_DataSource_PrioritySearch", false, GetSQL_Type("bit"), 1, true, GetFieldType("CanSearch")));
_FieldInfo.Add("CanSearch", new FieldInfo("TblPrioritySearchQueue", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanSearch")));
_FieldInfo.Add("CanSMS", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, true, GetFieldType("CanSMS")));
_FieldInfo.Add("CanUpdate", new FieldInfo("TblContact", false, GetSQL_Type("bit"), 1, false, GetFieldType("CanUpdate")));
_FieldInfo.Add("Capital", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 100, true, GetFieldType("Capital")));
_FieldInfo.Add("Carrier", new FieldInfo("TblSmtpAddress", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Carrier")));
_FieldInfo.Add("Carrier", new FieldInfo("TypeCarrier", false, GetSQL_Type("varchar"), 50, true, GetFieldType("Carrier")));
_FieldInfo.Add("ChargeTax", new FieldInfo("TblOffer", false, GetSQL_Type("bit"), 1, false, GetFieldType("ChargeTax")));
_FieldInfo.Add("ChunkCount", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("ChunkCount")));
_FieldInfo.Add("City", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 80, true, GetFieldType("City")));
_FieldInfo.Add("City", new FieldInfo("TblZipCode", false, GetSQL_Type("nvarchar"), 160, false, GetFieldType("City")));
_FieldInfo.Add("City", new FieldInfo("TblCity", false, GetSQL_Type("nvarchar"), 160, false, GetFieldType("City")));
_FieldInfo.Add("Code", new FieldInfo("TblCity", false, GetSQL_Type("nvarchar"), 20, true, GetFieldType("Code")));
_FieldInfo.Add("Code", new FieldInfo("TblRegion", false, GetSQL_Type("nvarchar"), 8, false, GetFieldType("Code")));
_FieldInfo.Add("Communication", new FieldInfo("TypeCommunication", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Communication")));
_FieldInfo.Add("Company", new FieldInfo("TblCompany", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Company")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Confidence", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("int"), 4, false, GetFieldType("Confidence")));
_FieldInfo.Add("Copyright", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Copyright")));
_FieldInfo.Add("Counter", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("bigint"), 8, false, GetFieldType("Counter")));
_FieldInfo.Add("Country", new FieldInfo("TblPhone", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Country")));
_FieldInfo.Add("Country", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 50, true, GetFieldType("Country")));
_FieldInfo.Add("Country", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 100, false, GetFieldType("Country")));
_FieldInfo.Add("Country", new FieldInfo("TblZipCode", false, GetSQL_Type("nvarchar"), 8, false, GetFieldType("Country")));
_FieldInfo.Add("CountryCode", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 4, true, GetFieldType("CountryCode")));
_FieldInfo.Add("CountryCode", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 5, true, GetFieldType("CountryCode")));
_FieldInfo.Add("Created", new FieldInfo("TblDeviceConfirmationCode", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Created")));
_FieldInfo.Add("Created", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Created")));
_FieldInfo.Add("CreditCard", new FieldInfo("TypeCreditCard", false, GetSQL_Type("varchar"), 50, false, GetFieldType("CreditCard")));
_FieldInfo.Add("CreditCardCode", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 200, false, GetFieldType("CreditCardCode")));
_FieldInfo.Add("CreditCardNumber", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 200, false, GetFieldType("CreditCardNumber")));
_FieldInfo.Add("CreditCardType", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 50, false, GetFieldType("CreditCardType")));
_FieldInfo.Add("Currency", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 80, true, GetFieldType("Currency")));
_FieldInfo.Add("CurrencyCode", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 8, true, GetFieldType("CurrencyCode")));
_FieldInfo.Add("CurrencyCode", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 10, true, GetFieldType("CurrencyCode")));
_FieldInfo.Add("CurrentContactPosition", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("CurrentContactPosition")));
_FieldInfo.Add("DataAccess", new FieldInfo("TypeDataAccess", false, GetSQL_Type("varchar"), 20, false, GetFieldType("DataAccess")));
_FieldInfo.Add("DataGatewayKey", new FieldInfo("HrvHarvesterSystem", false, GetSQL_Type("varchar"), 50, true, GetFieldType("DataGatewayKey")));
_FieldInfo.Add("DataInput", new FieldInfo("TypeDataInput", false, GetSQL_Type("varchar"), 50, false, GetFieldType("DataInput")));
_FieldInfo.Add("DataManagement", new FieldInfo("TypeDataManagement", false, GetSQL_Type("varchar"), 100, false, GetFieldType("DataManagement")));
_FieldInfo.Add("DataManagementTypeID", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, false, GetFieldType("DataManagementTypeID")));
_FieldInfo.Add("DataSource", new FieldInfo("TblDataSource", false, GetSQL_Type("varchar"), 50, false, GetFieldType("DataSource")));
_FieldInfo.Add("DateAccess", new FieldInfo("TblAccessKey", false, GetSQL_Type("datetime"), 8, true, GetFieldType("DateAccess")));
_FieldInfo.Add("DateAction", new FieldInfo("HrvlHarvesterAction", false, GetSQL_Type("datetime"), 8, true, GetFieldType("DateAction")));
_FieldInfo.Add("DateAdded", new FieldInfo("TblDevice", false, GetSQL_Type("datetime"), 8, true, GetFieldType("DateAdded")));
_FieldInfo.Add("DateLastConnect", new FieldInfo("TblDevice", false, GetSQL_Type("datetime"), 8, true, GetFieldType("DateLastConnect")));
_FieldInfo.Add("DateLastSync", new FieldInfo("TblDevice", false, GetSQL_Type("datetime"), 8, true, GetFieldType("DateLastSync")));
_FieldInfo.Add("DateLastUpdate", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("datetime"), 8, false, GetFieldType("DateLastUpdate")));
_FieldInfo.Add("Decommisioned", new FieldInfo("TblZipCode", false, GetSQL_Type("bit"), 1, false, GetFieldType("Decommisioned")));
_FieldInfo.Add("DefaultPull", new FieldInfo("TblDataSource", false, GetSQL_Type("bit"), 1, false, GetFieldType("DefaultPull")));
_FieldInfo.Add("DefaultPush", new FieldInfo("TblDataSource", false, GetSQL_Type("bit"), 1, false, GetFieldType("DefaultPush")));
_FieldInfo.Add("DefaultStrength", new FieldInfo("TblDataSource", false, GetSQL_Type("int"), 4, false, GetFieldType("DefaultStrength")));
_FieldInfo.Add("Delivered", new FieldInfo("AssocDevice_ServerMessage", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Delivered")));
_FieldInfo.Add("DeliveryStatus", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("int"), 4, false, GetFieldType("DeliveryStatus")));
_FieldInfo.Add("Description", new FieldInfo("TblNotification", false, GetSQL_Type("varchar"), 500, true, GetFieldType("Description")));
_FieldInfo.Add("Description", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 500, false, GetFieldType("Description")));
_FieldInfo.Add("Description", new FieldInfo("TblInterestAttribute", false, GetSQL_Type("varchar"), 150, true, GetFieldType("Description")));
_FieldInfo.Add("Description", new FieldInfo("TblAreaCodeRegion", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Description")));
_FieldInfo.Add("Description", new FieldInfo("TblInterestArea", false, GetSQL_Type("varchar"), 150, true, GetFieldType("Description")));
_FieldInfo.Add("Description", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 300, false, GetFieldType("Description")));
_FieldInfo.Add("Details", new FieldInfo("CacheInboundRequest", false, GetSQL_Type("varchar"), 300, true, GetFieldType("Details")));
_FieldInfo.Add("Device", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Device")));
_FieldInfo.Add("DeviceConfirmationCode", new FieldInfo("TblDeviceConfirmationCode", false, GetSQL_Type("int"), 4, false, GetFieldType("DeviceConfirmationCode")));
_FieldInfo.Add("DeviceDataInputMasterFilterID", new FieldInfo("OvrDevice_DataInput", false, GetSQL_Type("bigint"), 8, false, GetFieldType("DeviceDataInputMasterFilterID")));
_FieldInfo.Add("DeviceDataInputMasterFilterID", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("bigint"), 8, false, GetFieldType("DeviceDataInputMasterFilterID")));
_FieldInfo.Add("DeviceManufacturer", new FieldInfo("TblDeviceManufacturer", false, GetSQL_Type("varchar"), 50, false, GetFieldType("DeviceManufacturer")));
_FieldInfo.Add("DeviceModel", new FieldInfo("TblDeviceModel", false, GetSQL_Type("varchar"), 50, false, GetFieldType("DeviceModel")));
_FieldInfo.Add("DeviceStatusID", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("DeviceStatusID")));
_FieldInfo.Add("DeviceToken", new FieldInfo("HistDeviceToken", false, GetSQL_Type("varchar"), 40, false, GetFieldType("DeviceToken")));
_FieldInfo.Add("DeviceUID", new FieldInfo("TblDeviceConfirmationCode", false, GetSQL_Type("varchar"), 50, false, GetFieldType("DeviceUID")));
_FieldInfo.Add("DisableCommunications", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("DisableCommunications")));
_FieldInfo.Add("DisableCommunications", new FieldInfo("AssocGroup_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("DisableCommunications")));
_FieldInfo.Add("Display", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 90, false, GetFieldType("Display")));
_FieldInfo.Add("DMA", new FieldInfo("TblDMA", false, GetSQL_Type("smallint"), 2, false, GetFieldType("DMA")));
_FieldInfo.Add("Email", new FieldInfo("TblEmail", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Email")));
_FieldInfo.Add("EncodeForURL", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("EncodeForURL")));
_FieldInfo.Add("EncodeForURL", new FieldInfo("TblDeviceModel", false, GetSQL_Type("bit"), 1, false, GetFieldType("EncodeForURL")));
_FieldInfo.Add("Ended", new FieldInfo("TblOffer", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Ended")));
_FieldInfo.Add("ExpirationMonth", new FieldInfo("TblCreditCard", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("ExpirationMonth")));
_FieldInfo.Add("ExpirationYear", new FieldInfo("TblCreditCard", false, GetSQL_Type("int"), 4, false, GetFieldType("ExpirationYear")));
_FieldInfo.Add("FaceValue", new FieldInfo("TblOffer", false, GetSQL_Type("money"), 8, false, GetFieldType("FaceValue")));
_FieldInfo.Add("FgColor", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 6, false, GetFieldType("FgColor")));
_FieldInfo.Add("FieldDisplayName", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("varchar"), 20, false, GetFieldType("FieldDisplayName")));
_FieldInfo.Add("FieldDisplayName", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("nvarchar"), 40, true, GetFieldType("FieldDisplayName")));
_FieldInfo.Add("FieldDisplayName", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("varchar"), 50, false, GetFieldType("FieldDisplayName")));
_FieldInfo.Add("FieldUpdateStatus", new FieldInfo("StatusFieldUpdates", false, GetSQL_Type("varchar"), 50, false, GetFieldType("FieldUpdateStatus")));
_FieldInfo.Add("FIPS", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 8, false, GetFieldType("FIPS")));
_FieldInfo.Add("FirstName", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 90, false, GetFieldType("FirstName")));
_FieldInfo.Add("FrameID", new FieldInfo("HrvHarvesterResultQueue", false, GetSQL_Type("int"), 4, true, GetFieldType("FrameID")));
_FieldInfo.Add("GetResult", new FieldInfo("HrvHarvesterSystem", false, GetSQL_Type("bit"), 1, false, GetFieldType("GetResult")));
_FieldInfo.Add("GMTOffset", new FieldInfo("TblTimeZone", false, GetSQL_Type("float"), 8, false, GetFieldType("GMTOffset")));
_FieldInfo.Add("Group", new FieldInfo("TblGroup", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Group")));
_FieldInfo.Add("GUID", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 200, false, GetFieldType("GUID")));
_FieldInfo.Add("HarvesterAction", new FieldInfo("HrvlHarvesterAction", false, GetSQL_Type("varchar"), 100, true, GetFieldType("HarvesterAction")));
_FieldInfo.Add("HarvesterAction", new FieldInfo("TypeHarvesterAction", false, GetSQL_Type("varchar"), 50, false, GetFieldType("HarvesterAction")));
_FieldInfo.Add("HarvesterCertificate", new FieldInfo("HrvHarvesterSystem", false, GetSQL_Type("nvarchar"), 200, true, GetFieldType("HarvesterCertificate")));
_FieldInfo.Add("HarvesterUrl", new FieldInfo("HrvHarvesterSystem", false, GetSQL_Type("varchar"), 50, true, GetFieldType("HarvesterUrl")));
_FieldInfo.Add("HasCompleted", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("HasCompleted")));
_FieldInfo.Add("Height", new FieldInfo("TblMedia", false, GetSQL_Type("int"), 4, true, GetFieldType("Height")));
_FieldInfo.Add("HistoryCommunicationType", new FieldInfo("TblGroup", false, GetSQL_Type("tinyint"), 1, true, GetFieldType("HistoryCommunicationType")));
_FieldInfo.Add("IDAccessKey", new FieldInfo("TblAccessKey", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDAccessKey")));
_FieldInfo.Add("IDApplication", new FieldInfo("TblApplication", true, GetSQL_Type("int"), 4, false, GetFieldType("IDApplication")));
_FieldInfo.Add("IDApplication", new FieldInfo("TblTransaction", false, GetSQL_Type("int"), 4, true, GetFieldType("IDApplication")));
_FieldInfo.Add("IDApplication", new FieldInfo("AssocApplication_NewsChannel", false, GetSQL_Type("int"), 4, false, GetFieldType("IDApplication")));
_FieldInfo.Add("IDApplication", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("int"), 4, false, GetFieldType("IDApplication")));
_FieldInfo.Add("IDApplication", new FieldInfo("AssocGroup_Application", false, GetSQL_Type("int"), 4, false, GetFieldType("IDApplication")));
_FieldInfo.Add("IDApplication", new FieldInfo("ReviewProfile_Application", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDApplication")));
_FieldInfo.Add("IDArchive", new FieldInfo("AssocProfile_ArchiveRecord", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDArchive")));
_FieldInfo.Add("IDAreaCode", new FieldInfo("TblAreaCodeRegion", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDAreaCode")));
_FieldInfo.Add("IDCity", new FieldInfo("TblCity", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDCity")));
_FieldInfo.Add("IDCity", new FieldInfo("TblZipCode", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDCity")));
_FieldInfo.Add("IDCity", new FieldInfo("TblLocation", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDCity")));
_FieldInfo.Add("IDCompany", new FieldInfo("TblCompany", true, GetSQL_Type("int"), 4, false, GetFieldType("IDCompany")));
_FieldInfo.Add("IDCompany", new FieldInfo("TblMerchant", false, GetSQL_Type("int"), 4, false, GetFieldType("IDCompany")));
_FieldInfo.Add("IDCompany", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("int"), 4, false, GetFieldType("IDCompany")));
_FieldInfo.Add("IDCompany", new FieldInfo("AssocContact_Company", false, GetSQL_Type("int"), 4, false, GetFieldType("IDCompany")));
_FieldInfo.Add("IDContact", new FieldInfo("TblContact", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Company", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Note", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("TblMedia", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocProfile_Contact", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("LnkProfile_Contact", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Media", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Email", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Person", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_WebAddress", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Prefix", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDContact", new FieldInfo("AssocContact_Title", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDContact")));
_FieldInfo.Add("IDCountry", new FieldInfo("TblCountry", true, GetSQL_Type("int"), 4, false, GetFieldType("IDCountry")));
_FieldInfo.Add("IDCountry", new FieldInfo("TblDMA", false, GetSQL_Type("int"), 4, false, GetFieldType("IDCountry")));
_FieldInfo.Add("IDCountry", new FieldInfo("TblCity", false, GetSQL_Type("int"), 4, true, GetFieldType("IDCountry")));
_FieldInfo.Add("IDCountry", new FieldInfo("TblRegion", false, GetSQL_Type("int"), 4, false, GetFieldType("IDCountry")));
_FieldInfo.Add("IDCreditCard", new FieldInfo("TblCreditCard", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDCreditCard")));
_FieldInfo.Add("IDCreditCard", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDCreditCard")));
_FieldInfo.Add("IDDataSource", new FieldInfo("TblDataSource", true, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("HistSearch", false, GetSQL_Type("int"), 4, true, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("TblPrioritySearchQueue", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("LnkProfile_DataSource_PrioritySearch", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("HrvHarvesterResultQueue", false, GetSQL_Type("int"), 4, true, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("AssocSubscriber_DataSource", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSource", new FieldInfo("TblDataSourceLogin", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSource")));
_FieldInfo.Add("IDDataSourceLogin", new FieldInfo("TblDataSourceLogin", true, GetSQL_Type("int"), 4, false, GetFieldType("IDDataSourceLogin")));
_FieldInfo.Add("IDDevice", new FieldInfo("TblDevice", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDevice", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDevice", new FieldInfo("AssocDevice_ServerMessage", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDevice", new FieldInfo("HistDeviceToken", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDevice", new FieldInfo("OvrDevice_DataInput", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDevice", new FieldInfo("TblContact", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDDevice")));
_FieldInfo.Add("IDDeviceManufacturer", new FieldInfo("TblDeviceManufacturer", true, GetSQL_Type("int"), 4, false, GetFieldType("IDDeviceManufacturer")));
_FieldInfo.Add("IDDeviceManufacturer", new FieldInfo("TblDeviceModel", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDeviceManufacturer")));
_FieldInfo.Add("IDDeviceModel", new FieldInfo("TblDeviceModel", true, GetSQL_Type("int"), 4, false, GetFieldType("IDDeviceModel")));
_FieldInfo.Add("IDDeviceModel", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("IDDeviceModel")));
_FieldInfo.Add("IDDMA", new FieldInfo("TblDMA", true, GetSQL_Type("int"), 4, false, GetFieldType("IDDMA")));
_FieldInfo.Add("IDDMA", new FieldInfo("TblCity", false, GetSQL_Type("int"), 4, true, GetFieldType("IDDMA")));
_FieldInfo.Add("IDEmail", new FieldInfo("TblEmail", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail", new FieldInfo("AssocContact_Email", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDEmail")));
_FieldInfo.Add("IDEmail_NotificationGateway", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail_NotificationGateway")));
_FieldInfo.Add("IDEmail_NotificationGateway", new FieldInfo("TblGroup", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail_NotificationGateway")));
_FieldInfo.Add("IDEmail_Preferred", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail_Preferred")));
_FieldInfo.Add("IDEmail_Preferred", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail_Preferred")));
_FieldInfo.Add("IDEmail_Sender", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDEmail_Sender")));
_FieldInfo.Add("IDGroup", new FieldInfo("TblGroup", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("LnkGroup_Profile_Admin", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("TblNotification", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("LnkOffer_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocGroup_NewsChannel", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocGroup_MetroArea", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocGroup_Application", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("TblContact", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocOffer_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocOffer_Group_InterestAttribute", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("AssocGroup_InterestAttribute", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup", new FieldInfo("LnkProfile_Group_Admin", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup")));
_FieldInfo.Add("IDGroup_Child", new FieldInfo("AssocGroup_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup_Child")));
_FieldInfo.Add("IDGroup_Parent", new FieldInfo("AssocGroup_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDGroup_Parent")));
_FieldInfo.Add("IDHarvesterActionType", new FieldInfo("HrvlHarvesterAction", false, GetSQL_Type("int"), 4, false, GetFieldType("IDHarvesterActionType")));
_FieldInfo.Add("IDHarvesterSystem", new FieldInfo("HrvHarvesterSystem", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDHarvesterSystem")));
_FieldInfo.Add("IDInterestArea", new FieldInfo("TblInterestArea", true, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestArea")));
_FieldInfo.Add("IDInterestArea", new FieldInfo("TblInterestAttribute", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestArea")));
_FieldInfo.Add("IDInterestArea", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestArea")));
_FieldInfo.Add("IDInterestAttribute", new FieldInfo("TblInterestAttribute", true, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute")));
_FieldInfo.Add("IDInterestAttribute", new FieldInfo("AssocOffer_Group_InterestAttribute", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute")));
_FieldInfo.Add("IDInterestAttribute", new FieldInfo("AssocGroup_InterestAttribute", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute")));
_FieldInfo.Add("IDInterestAttribute", new FieldInfo("AssocProfile_InterestAttribute", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute")));
_FieldInfo.Add("IDInterestAttribute_1", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute_1")));
_FieldInfo.Add("IDInterestAttribute_1", new FieldInfo("AssocInterestAttribute_InterestAttribute_Strength", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute_1")));
_FieldInfo.Add("IDInterestAttribute_2", new FieldInfo("AssocInterestAttribute_InterestAttribute_Strength", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute_2")));
_FieldInfo.Add("IDInterestAttribute_2", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("int"), 4, false, GetFieldType("IDInterestAttribute_2")));
_FieldInfo.Add("IDLinkSource", new FieldInfo("AssocProfile_Profile", false, GetSQL_Type("int"), 4, false, GetFieldType("IDLinkSource")));
_FieldInfo.Add("IDLinkSource", new FieldInfo("AssocProfile_Contact", false, GetSQL_Type("int"), 4, false, GetFieldType("IDLinkSource")));
_FieldInfo.Add("IDLocation", new FieldInfo("TblLocation", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocation")));
_FieldInfo.Add("IDLocation", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocation")));
_FieldInfo.Add("IDLocation", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocation")));
_FieldInfo.Add("IDLocation", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocation")));
_FieldInfo.Add("IDLocation", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocation")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("TblLocationCity", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCity", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCity")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("TblLocationCountry", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationCountry", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationCountry")));
_FieldInfo.Add("IDLocationState", new FieldInfo("TblLocationState", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationState", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationState", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationState", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationState", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationState", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationState")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("TblLocationStreet", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("TblLocationStreet2", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationStreet2", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationStreet2")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("TblLocationZipCode", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDLocationZipCode", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDLocationZipCode")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblMedia", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("AssocProfile_Media_Relationship", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("AssocProfile_Media", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("AssocContact_Media", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblContact", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblOffer", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblNotification", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblApplication", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia", new FieldInfo("TblGroup", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDMedia")));
_FieldInfo.Add("IDMedia_AppCon", new FieldInfo("TblApplication", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDMedia_AppCon")));
_FieldInfo.Add("IDMedia_Header", new FieldInfo("TblApplication", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDMedia_Header")));
_FieldInfo.Add("IDMerchant", new FieldInfo("TblMerchant", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMerchant")));
_FieldInfo.Add("IDMerchant", new FieldInfo("AssocProfile_Merchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMerchant")));
_FieldInfo.Add("IDMerchant", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMerchant")));
_FieldInfo.Add("IDMerchant", new FieldInfo("TblOffer", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDMerchant")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("AssocProfile_MetroArea", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("AssocMetroArea_ZipCode", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("AssocOffer_MetroArea", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("AssocGroup_MetroArea", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("AssocMetroArea_AreaCode", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDMetroArea", new FieldInfo("TblMetroArea", false, GetSQL_Type("int"), 4, false, GetFieldType("IDMetroArea")));
_FieldInfo.Add("IDNewsChannel", new FieldInfo("TblNewsChannel", true, GetSQL_Type("int"), 4, false, GetFieldType("IDNewsChannel")));
_FieldInfo.Add("IDNewsChannel", new FieldInfo("TblNewsItem", false, GetSQL_Type("int"), 4, false, GetFieldType("IDNewsChannel")));
_FieldInfo.Add("IDNewsChannel", new FieldInfo("AssocApplication_NewsChannel", false, GetSQL_Type("int"), 4, false, GetFieldType("IDNewsChannel")));
_FieldInfo.Add("IDNewsChannel", new FieldInfo("AssocGroup_NewsChannel", false, GetSQL_Type("int"), 4, false, GetFieldType("IDNewsChannel")));
_FieldInfo.Add("IDNewsItem", new FieldInfo("TblNewsItem", true, GetSQL_Type("int"), 4, false, GetFieldType("IDNewsItem")));
_FieldInfo.Add("IDNote", new FieldInfo("TblNote", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNote")));
_FieldInfo.Add("IDNote", new FieldInfo("TblGroup", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDNote")));
_FieldInfo.Add("IDNote", new FieldInfo("AssocProfile_Note", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNote")));
_FieldInfo.Add("IDNote", new FieldInfo("AssocContact_Note", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNote")));
_FieldInfo.Add("IDNotification", new FieldInfo("TblNotification", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNotification")));
_FieldInfo.Add("IDNotification", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNotification")));
_FieldInfo.Add("IDNotificationGateway", new FieldInfo("TblNotificationGateway", true, GetSQL_Type("int"), 4, false, GetFieldType("IDNotificationGateway")));
_FieldInfo.Add("IDNotificationQueue", new FieldInfo("TblNotificationQueue", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDNotificationQueue")));
_FieldInfo.Add("IDOffer", new FieldInfo("TblOffer", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDOffer", new FieldInfo("AssocOffer_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDOffer", new FieldInfo("AssocOffer_MetroArea", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDOffer", new FieldInfo("LnkOffer_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDOffer", new FieldInfo("AssocOffer_Group_InterestAttribute", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDOffer", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDOffer")));
_FieldInfo.Add("IDPerson", new FieldInfo("TblPerson", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPerson")));
_FieldInfo.Add("IDPerson", new FieldInfo("AssocContact_Person", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPerson")));
_FieldInfo.Add("IDPerson", new FieldInfo("TblPersonKind", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPerson")));
_FieldInfo.Add("IDPerson", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPerson")));
_FieldInfo.Add("IDPersonKind", new FieldInfo("TblPersonKind", true, GetSQL_Type("int"), 4, false, GetFieldType("IDPersonKind")));
_FieldInfo.Add("IDPhone", new FieldInfo("TblPhone", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDPhone")));
_FieldInfo.Add("IDPhone_Fax", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Fax")));
_FieldInfo.Add("IDPhone_NotificationGateway", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_NotificationGateway")));
_FieldInfo.Add("IDPhone_NotificationGateway", new FieldInfo("TblGroup", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_NotificationGateway")));
_FieldInfo.Add("IDPhone_Other", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Other")));
_FieldInfo.Add("IDPhone_Preferred", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Preferred")));
_FieldInfo.Add("IDPhone_Preferred", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Preferred")));
_FieldInfo.Add("IDPhone_Primary", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Primary")));
_FieldInfo.Add("IDPhone_Sender", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Sender")));
_FieldInfo.Add("IDPhone_Work", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDPhone_Work")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblProfile", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblDevice", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblNotification", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblApplication", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("LnkGroup_Profile_Admin", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Media_Relationship", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Merchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblSubscriber", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblPrioritySearchQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("HistSearch", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("LnkProfile_DataSource_PrioritySearch", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Contact", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("LnkProfile_Contact", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblMedia", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_ArchiveRecord", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblAccessKey", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("LnkProfile_Group_Admin", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("TblCreditCard", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Media", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("LnkProfile_Media_Hash", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("OvrProfile_DataAccess", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("OvrProfile_DataManagement", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("ReviewProfile_Application", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Note", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_MetroArea", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_InterestAttribute", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile")));
_FieldInfo.Add("IDProfile_1", new FieldInfo("ReviewProfile_Profile", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_1")));
_FieldInfo.Add("IDProfile_1", new FieldInfo("AssocProfile_Profile", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_1")));
_FieldInfo.Add("IDProfile_1", new FieldInfo("LnkProfile_Profile_MergeReview", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_1")));
_FieldInfo.Add("IDProfile_2", new FieldInfo("LnkProfile_Profile_MergeReview", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_2")));
_FieldInfo.Add("IDProfile_2", new FieldInfo("AssocProfile_Profile", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_2")));
_FieldInfo.Add("IDProfile_2", new FieldInfo("ReviewProfile_Profile", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_2")));
_FieldInfo.Add("IDProfile_2", new FieldInfo("TblProfile", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDProfile_2")));
_FieldInfo.Add("IDProfile_Owner", new FieldInfo("OvrProfile_Profile_RelationshipType", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_Owner")));
_FieldInfo.Add("IDProfile_Receiver", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_Receiver")));
_FieldInfo.Add("IDProfile_Requester", new FieldInfo("OvrProfile_Profile_RelationshipType", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_Requester")));
_FieldInfo.Add("IDProfile_Sender", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfile_Sender")));
_FieldInfo.Add("IDProfileDataAccess", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfileDataAccess")));
_FieldInfo.Add("IDProfileDataAccessMasterFilter", new FieldInfo("FltProfile_DataAccess", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDProfileDataAccessMasterFilter")));
_FieldInfo.Add("IDProfileDataManagementMasterFilter", new FieldInfo("FltProfile_DataManagement", true, GetSQL_Type("int"), 4, false, GetFieldType("IDProfileDataManagementMasterFilter")));
_FieldInfo.Add("IDRegion", new FieldInfo("TblRegion", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDRegion")));
_FieldInfo.Add("IDRegion", new FieldInfo("TblMetroArea", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDRegion")));
_FieldInfo.Add("IDRegion", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDRegion")));
_FieldInfo.Add("IDRegion", new FieldInfo("TblAreaCodeRegion", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDRegion")));
_FieldInfo.Add("IDRegion", new FieldInfo("TblCity", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDRegion")));
_FieldInfo.Add("IDResult", new FieldInfo("HrvHarvesterResultQueue", true, GetSQL_Type("int"), 4, false, GetFieldType("IDResult")));
_FieldInfo.Add("IDSearchThrottle", new FieldInfo("TblSearchThrottle", true, GetSQL_Type("int"), 4, false, GetFieldType("IDSearchThrottle")));
_FieldInfo.Add("IDSmtpAddress", new FieldInfo("TblSmtpAddress", true, GetSQL_Type("int"), 4, false, GetFieldType("IDSmtpAddress")));
_FieldInfo.Add("IDSubscriber", new FieldInfo("TblSubscriber", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDSubscriber")));
_FieldInfo.Add("IDSubscriber", new FieldInfo("TblDevice", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDSubscriber")));
_FieldInfo.Add("IDSubscriber", new FieldInfo("AssocSubscriber_DataSource", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDSubscriber")));
_FieldInfo.Add("IDTimeZone", new FieldInfo("TblTimeZone", true, GetSQL_Type("int"), 4, false, GetFieldType("IDTimeZone")));
_FieldInfo.Add("IDTransaction", new FieldInfo("TblTransaction", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDTransaction")));
_FieldInfo.Add("IDWebAddress", new FieldInfo("TblWebAddress", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDWebAddress")));
_FieldInfo.Add("IDWebAddress", new FieldInfo("TblMerchant", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDWebAddress")));
_FieldInfo.Add("IDWebAddress", new FieldInfo("AssocContact_WebAddress", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDWebAddress")));
_FieldInfo.Add("IDWebAddress", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDWebAddress")));
_FieldInfo.Add("IDZipCode", new FieldInfo("TblZipCode", true, GetSQL_Type("bigint"), 8, false, GetFieldType("IDZipCode")));
_FieldInfo.Add("IDZipCode", new FieldInfo("TblLocation", false, GetSQL_Type("bigint"), 8, true, GetFieldType("IDZipCode")));
_FieldInfo.Add("IDZipCode", new FieldInfo("AssocMetroArea_ZipCode", false, GetSQL_Type("bigint"), 8, false, GetFieldType("IDZipCode")));
_FieldInfo.Add("ImageURL", new FieldInfo("HistSearch", false, GetSQL_Type("varchar"), 100, true, GetFieldType("ImageURL")));
_FieldInfo.Add("ImageURL", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 200, false, GetFieldType("ImageURL")));
_FieldInfo.Add("InterestArea", new FieldInfo("TblInterestArea", false, GetSQL_Type("varchar"), 50, false, GetFieldType("InterestArea")));
_FieldInfo.Add("InterestAttribute", new FieldInfo("TblInterestAttribute", false, GetSQL_Type("varchar"), 50, false, GetFieldType("InterestAttribute")));
_FieldInfo.Add("Internet", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 4, false, GetFieldType("Internet")));
_FieldInfo.Add("InvitationRequested", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("datetime"), 8, true, GetFieldType("InvitationRequested")));
_FieldInfo.Add("InvitationSent", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("datetime"), 8, true, GetFieldType("InvitationSent")));
_FieldInfo.Add("IPAddress", new FieldInfo("TblAccessKey", false, GetSQL_Type("varchar"), 50, true, GetFieldType("IPAddress")));
_FieldInfo.Add("IPAddress", new FieldInfo("CacheInboundRequest", false, GetSQL_Type("int"), 4, true, GetFieldType("IPAddress")));
_FieldInfo.Add("IPAddressString", new FieldInfo("CacheInboundRequest", false, GetSQL_Type("varchar"), 20, false, GetFieldType("IPAddressString")));
_FieldInfo.Add("IsActive", new FieldInfo("TblContact", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsActive")));
_FieldInfo.Add("IsAdmin", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsAdmin")));
_FieldInfo.Add("IsAdmin", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsAdmin")));
_FieldInfo.Add("IsAvailable", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsAvailable")));
_FieldInfo.Add("IsChunking", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsChunking")));
_FieldInfo.Add("IsConfirmed", new FieldInfo("TblProfile", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsConfirmed")));
_FieldInfo.Add("IsCorrect", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsCorrect")));
_FieldInfo.Add("IsDefault", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsDefault")));
_FieldInfo.Add("IsDelivered", new FieldInfo("AssocDevice_ServerMessage", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsDelivered")));
_FieldInfo.Add("IsDiscoverable", new FieldInfo("TblGroup", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsDiscoverable")));
_FieldInfo.Add("IsEnabled", new FieldInfo("TypeNotificationChannel", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsEnabled")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocGroup_InterestAttribute", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_MetroArea", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocGroup_MetroArea", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_InterestAttribute", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsExcluded", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsExcluded")));
_FieldInfo.Add("IsForTesting", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsForTesting")));
_FieldInfo.Add("IsGallery", new FieldInfo("TblMedia", false, GetSQL_Type("bit"), 1, true, GetFieldType("IsGallery")));
_FieldInfo.Add("IsJoinable", new FieldInfo("TblApplication", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsJoinable")));
_FieldInfo.Add("IsLocked", new FieldInfo("OvrProfile_DataManagement", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsLocked")));
_FieldInfo.Add("IsLocked", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsLocked")));
_FieldInfo.Add("IsLocked", new FieldInfo("OvrDevice_DataInput", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsLocked")));
_FieldInfo.Add("IsLocked", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("tinyint"), 1, true, GetFieldType("IsLocked")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Location", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Note", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Company", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_WebAddress", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Prefix", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Title", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Email", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManaged", new FieldInfo("AssocContact_Person", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManaged")));
_FieldInfo.Add("IsManagedByDevice", new FieldInfo("TblNote", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManagedByDevice")));
_FieldInfo.Add("IsManual", new FieldInfo("AssocGroup_InterestAttribute", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManual")));
_FieldInfo.Add("IsManual", new FieldInfo("AssocProfile_InterestAttribute", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManual")));
_FieldInfo.Add("IsManual", new FieldInfo("AssocGroup_MetroArea", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManual")));
_FieldInfo.Add("IsManual", new FieldInfo("AssocProfile_MetroArea", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManual")));
_FieldInfo.Add("IsManualRelationship", new FieldInfo("LnkProfile_Contact", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManualRelationship")));
_FieldInfo.Add("IsManualTarget", new FieldInfo("TblGroup", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsManualTarget")));
_FieldInfo.Add("IsNewRecord", new FieldInfo("TblContact", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsNewRecord")));
_FieldInfo.Add("ISO", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 4, false, GetFieldType("ISO")));
_FieldInfo.Add("ISO3", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 6, false, GetFieldType("ISO3")));
_FieldInfo.Add("ISON", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 8, false, GetFieldType("ISON")));
_FieldInfo.Add("IsProcessing", new FieldInfo("TblNewsItem", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsProcessing")));
_FieldInfo.Add("IsProcessing", new FieldInfo("AssocContact_Email", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsProcessing")));
_FieldInfo.Add("IsRemoved", new FieldInfo("TblCreditCard", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsRemoved")));
_FieldInfo.Add("IsRemoved", new FieldInfo("TblMerchant", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsRemoved")));
_FieldInfo.Add("IsRemoved", new FieldInfo("TblOffer", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsRemoved")));
_FieldInfo.Add("IsShared", new FieldInfo("OvrProfile_DataAccess", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsShared")));
_FieldInfo.Add("IsShared", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsShared")));
_FieldInfo.Add("IsVisible", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsVisible")));
_FieldInfo.Add("IsVoid", new FieldInfo("TblTransaction", false, GetSQL_Type("bit"), 1, false, GetFieldType("IsVoid")));
_FieldInfo.Add("ItemProperties", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 100, true, GetFieldType("ItemProperties")));
_FieldInfo.Add("iTunesURL", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 250, true, GetFieldType("iTunesURL")));
_FieldInfo.Add("JoinableType", new FieldInfo("TblGroup", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("JoinableType")));
_FieldInfo.Add("KeySearchFieldType", new FieldInfo("TblDataSource", false, GetSQL_Type("varchar"), 50, false, GetFieldType("KeySearchFieldType")));
_FieldInfo.Add("LastChecked", new FieldInfo("TblNewsChannel", false, GetSQL_Type("datetime"), 8, false, GetFieldType("LastChecked")));
_FieldInfo.Add("LastName", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 90, false, GetFieldType("LastName")));
_FieldInfo.Add("LastNotification", new FieldInfo("TblProfile", false, GetSQL_Type("datetime"), 8, true, GetFieldType("LastNotification")));
_FieldInfo.Add("LastSearch", new FieldInfo("HistSearch", false, GetSQL_Type("datetime"), 8, true, GetFieldType("LastSearch")));
_FieldInfo.Add("LastTargetCheck", new FieldInfo("TblGroup", false, GetSQL_Type("datetime"), 8, false, GetFieldType("LastTargetCheck")));
_FieldInfo.Add("LastUpdateSAQ", new FieldInfo("TblProfile", false, GetSQL_Type("datetime"), 8, true, GetFieldType("LastUpdateSAQ")));
_FieldInfo.Add("Lat", new FieldInfo("TblZipCode", false, GetSQL_Type("float"), 8, true, GetFieldType("Lat")));
_FieldInfo.Add("Lat", new FieldInfo("TblCity", false, GetSQL_Type("float"), 8, true, GetFieldType("Lat")));
_FieldInfo.Add("Length", new FieldInfo("TblNote", false, GetSQL_Type("int"), 4, false, GetFieldType("Length")));
_FieldInfo.Add("Link", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 200, false, GetFieldType("Link")));
_FieldInfo.Add("Link", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 200, false, GetFieldType("Link")));
_FieldInfo.Add("LinkSource", new FieldInfo("TypeLinkSource", false, GetSQL_Type("varchar"), 25, false, GetFieldType("LinkSource")));
_FieldInfo.Add("LinkStrength", new FieldInfo("AssocProfile_Contact", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("LinkStrength")));
_FieldInfo.Add("LinkStrength", new FieldInfo("AssocProfile_Profile", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("LinkStrength")));
_FieldInfo.Add("LinkStrength", new FieldInfo("AssocProfile_Contact_InterestAttributes", false, GetSQL_Type("int"), 4, false, GetFieldType("LinkStrength")));
_FieldInfo.Add("ListOrder", new FieldInfo("AssocContact_Email", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("ListOrder")));
_FieldInfo.Add("Location", new FieldInfo("TblZipCode", false, GetSQL_Type("nvarchar"), 120, true, GetFieldType("Location")));
_FieldInfo.Add("Location", new FieldInfo("TypeLocation", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Location")));
_FieldInfo.Add("LocationCity", new FieldInfo("TblLocationCity", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LocationCity")));
_FieldInfo.Add("LocationCountry", new FieldInfo("TblLocationCountry", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LocationCountry")));
_FieldInfo.Add("LocationDisplay", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 50, true, GetFieldType("LocationDisplay")));
_FieldInfo.Add("LocationState", new FieldInfo("TblLocationState", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LocationState")));
_FieldInfo.Add("LocationStreet", new FieldInfo("TblLocationStreet", false, GetSQL_Type("varchar"), 150, false, GetFieldType("LocationStreet")));
_FieldInfo.Add("LocationStreet2", new FieldInfo("TblLocationStreet2", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LocationStreet2")));
_FieldInfo.Add("LocationText", new FieldInfo("TblZipCode", false, GetSQL_Type("nvarchar"), 120, true, GetFieldType("LocationText")));
_FieldInfo.Add("LocationZipCode", new FieldInfo("TblLocationZipCode", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LocationZipCode")));
_FieldInfo.Add("Login", new FieldInfo("TblDataSourceLogin", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Login")));
_FieldInfo.Add("Login", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Login")));
_FieldInfo.Add("LoginTitle", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 50, false, GetFieldType("LoginTitle")));
_FieldInfo.Add("LogVerbose", new FieldInfo("TblDevice", false, GetSQL_Type("bit"), 1, false, GetFieldType("LogVerbose")));
_FieldInfo.Add("Lon", new FieldInfo("TblZipCode", false, GetSQL_Type("float"), 8, true, GetFieldType("Lon")));
_FieldInfo.Add("Lon", new FieldInfo("TblCity", false, GetSQL_Type("float"), 8, true, GetFieldType("Lon")));
_FieldInfo.Add("MapReference", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 100, true, GetFieldType("MapReference")));
_FieldInfo.Add("Market", new FieldInfo("TblDMA", false, GetSQL_Type("nvarchar"), 120, false, GetFieldType("Market")));
_FieldInfo.Add("MaxAvailable", new FieldInfo("TblOffer", false, GetSQL_Type("int"), 4, false, GetFieldType("MaxAvailable")));
_FieldInfo.Add("MaxPerCustomer", new FieldInfo("TblOffer", false, GetSQL_Type("int"), 4, false, GetFieldType("MaxPerCustomer")));
_FieldInfo.Add("MD5Hash", new FieldInfo("AssocContact_Note", false, GetSQL_Type("varchar"), 50, true, GetFieldType("MD5Hash")));
_FieldInfo.Add("MD5Hash", new FieldInfo("LnkProfile_Media_Hash", false, GetSQL_Type("varchar"), 50, false, GetFieldType("MD5Hash")));
_FieldInfo.Add("MD5Hash", new FieldInfo("AssocContact_Media", false, GetSQL_Type("varchar"), 50, true, GetFieldType("MD5Hash")));
_FieldInfo.Add("MD5Hash", new FieldInfo("TblNote", false, GetSQL_Type("varchar"), 50, true, GetFieldType("MD5Hash")));
_FieldInfo.Add("MediaFileName", new FieldInfo("TblMedia", false, GetSQL_Type("varchar"), 50, true, GetFieldType("MediaFileName")));
_FieldInfo.Add("MediaHeight", new FieldInfo("TblDeviceModel", false, GetSQL_Type("int"), 4, false, GetFieldType("MediaHeight")));
_FieldInfo.Add("MediaLength", new FieldInfo("TblNewsItem", false, GetSQL_Type("int"), 4, false, GetFieldType("MediaLength")));
_FieldInfo.Add("MediaSource", new FieldInfo("TypeMediaSource", false, GetSQL_Type("varchar"), 50, false, GetFieldType("MediaSource")));
_FieldInfo.Add("MediaType", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 50, false, GetFieldType("MediaType")));
_FieldInfo.Add("MediaURI", new FieldInfo("TblMedia", false, GetSQL_Type("varchar"), 255, true, GetFieldType("MediaURI")));
_FieldInfo.Add("MediaWidth", new FieldInfo("TblDeviceModel", false, GetSQL_Type("int"), 4, false, GetFieldType("MediaWidth")));
_FieldInfo.Add("Merchant", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 30, false, GetFieldType("Merchant")));
_FieldInfo.Add("MerchantSKU", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 50, true, GetFieldType("MerchantSKU")));
_FieldInfo.Add("MerchantTransactionID", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("MerchantTransactionID")));
_FieldInfo.Add("MetroArea", new FieldInfo("TblMetroArea", false, GetSQL_Type("varchar"), 100, false, GetFieldType("MetroArea")));
_FieldInfo.Add("NameOnCard", new FieldInfo("TblCreditCard", false, GetSQL_Type("varchar"), 90, true, GetFieldType("NameOnCard")));
_FieldInfo.Add("NamePosition", new FieldInfo("AssocContact_Person", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("NamePosition")));
_FieldInfo.Add("NamePosition", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("NamePosition")));
_FieldInfo.Add("NationalityPlural", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 80, true, GetFieldType("NationalityPlural")));
_FieldInfo.Add("NationalitySingular", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 80, true, GetFieldType("NationalitySingular")));
_FieldInfo.Add("Note", new FieldInfo("TblNote", false, GetSQL_Type("varchar"), 4000, false, GetFieldType("Note")));
_FieldInfo.Add("NoteLength", new FieldInfo("AssocContact_Note", false, GetSQL_Type("int"), 4, false, GetFieldType("NoteLength")));
_FieldInfo.Add("Notes", new FieldInfo("TblAccessKey", false, GetSQL_Type("varchar"), 500, true, GetFieldType("Notes")));
_FieldInfo.Add("Notification", new FieldInfo("TypeNotification", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Notification")));
_FieldInfo.Add("NotificationChannel", new FieldInfo("TypeNotificationChannel", false, GetSQL_Type("varchar"), 50, false, GetFieldType("NotificationChannel")));
_FieldInfo.Add("NotificationGateway", new FieldInfo("TypeNotificationGateway", false, GetSQL_Type("varchar"), 20, false, GetFieldType("NotificationGateway")));
_FieldInfo.Add("NotificationGatewayType", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("varchar"), 20, true, GetFieldType("NotificationGatewayType")));
_FieldInfo.Add("NotificationVendor", new FieldInfo("TypeNotificationVendor", false, GetSQL_Type("varchar"), 30, false, GetFieldType("NotificationVendor")));
_FieldInfo.Add("OfferKey", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 50, false, GetFieldType("OfferKey")));
_FieldInfo.Add("Options", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 150, true, GetFieldType("Options")));
_FieldInfo.Add("Password", new FieldInfo("TblSubscriber", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Password")));
_FieldInfo.Add("Password", new FieldInfo("TblDataSourceLogin", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Password")));
_FieldInfo.Add("Password", new FieldInfo("TblProfile", false, GetSQL_Type("varchar"), 20, true, GetFieldType("Password")));
_FieldInfo.Add("PercentOfBase", new FieldInfo("AssocGroup_InterestAttribute", false, GetSQL_Type("decimal"), 13, true, GetFieldType("PercentOfBase")));
_FieldInfo.Add("PercentOfBase", new FieldInfo("AssocGroup_MetroArea", false, GetSQL_Type("decimal"), 13, true, GetFieldType("PercentOfBase")));
_FieldInfo.Add("Person", new FieldInfo("TblPerson", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Person")));
_FieldInfo.Add("PersonKind", new FieldInfo("TblPersonKind", false, GetSQL_Type("varchar"), 50, false, GetFieldType("PersonKind")));
_FieldInfo.Add("Phone", new FieldInfo("TblPhone", false, GetSQL_Type("varchar"), 20, false, GetFieldType("Phone")));
_FieldInfo.Add("PhoneNumber", new FieldInfo("TblDeviceConfirmationCode", false, GetSQL_Type("varchar"), 20, false, GetFieldType("PhoneNumber")));
_FieldInfo.Add("PhoneNumber", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 20, false, GetFieldType("PhoneNumber")));
_FieldInfo.Add("PictureData", new FieldInfo("TblMedia", false, GetSQL_Type("image"), 16, true, GetFieldType("PictureData")));
_FieldInfo.Add("PictureDataB64", new FieldInfo("TblMedia", false, GetSQL_Type("varchar"), 8000, true, GetFieldType("PictureDataB64")));
_FieldInfo.Add("PIMContactUID", new FieldInfo("TblContact", false, GetSQL_Type("varchar"), 20, false, GetFieldType("PIMContactUID")));
_FieldInfo.Add("PIMContactUID", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("varchar"), 20, true, GetFieldType("PIMContactUID")));
_FieldInfo.Add("PIN", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 20, false, GetFieldType("PIN")));
_FieldInfo.Add("PingCycle", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("PingCycle")));
_FieldInfo.Add("PingCycle", new FieldInfo("TypePingCycle", false, GetSQL_Type("varchar"), 20, false, GetFieldType("PingCycle")));
_FieldInfo.Add("PositionIndex", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("int"), 4, true, GetFieldType("PositionIndex")));
_FieldInfo.Add("PositionIndex", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("int"), 4, false, GetFieldType("PositionIndex")));
_FieldInfo.Add("PositionIndex", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("int"), 4, true, GetFieldType("PositionIndex")));
_FieldInfo.Add("ppAckValue", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("ppAckValue")));
_FieldInfo.Add("ppAmount", new FieldInfo("TblTransaction", false, GetSQL_Type("money"), 8, true, GetFieldType("ppAmount")));
_FieldInfo.Add("ppAvsCode", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 10, true, GetFieldType("ppAvsCode")));
_FieldInfo.Add("ppCvv2Match", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 10, true, GetFieldType("ppCvv2Match")));
_FieldInfo.Add("ppErrorCode", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("ppErrorCode")));
_FieldInfo.Add("ppLongMessage", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 200, true, GetFieldType("ppLongMessage")));
_FieldInfo.Add("ppTransactionID", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("ppTransactionID")));
_FieldInfo.Add("Precedence", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("Precedence")));
_FieldInfo.Add("Prefix", new FieldInfo("TypePrefix", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Prefix")));
_FieldInfo.Add("Price", new FieldInfo("TblOffer", false, GetSQL_Type("money"), 8, false, GetFieldType("Price")));
_FieldInfo.Add("Priority", new FieldInfo("TblAreaCodeRegion", false, GetSQL_Type("int"), 4, true, GetFieldType("Priority")));
_FieldInfo.Add("Processed", new FieldInfo("TblTransaction", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Processed")));
_FieldInfo.Add("ProfileDataAccessMasterFilterID", new FieldInfo("OvrProfile_DataAccess", false, GetSQL_Type("bigint"), 8, false, GetFieldType("ProfileDataAccessMasterFilterID")));
_FieldInfo.Add("ProfileDataManagementMasterFilterID", new FieldInfo("OvrProfile_DataManagement", false, GetSQL_Type("bigint"), 8, false, GetFieldType("ProfileDataManagementMasterFilterID")));
_FieldInfo.Add("ProfileNameAndMediaXML", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("varchar"), 1000, true, GetFieldType("ProfileNameAndMediaXML")));
_FieldInfo.Add("Promotion", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 250, false, GetFieldType("Promotion")));
_FieldInfo.Add("Published", new FieldInfo("TblNewsItem", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Published")));
_FieldInfo.Add("Quantity", new FieldInfo("TblTransaction", false, GetSQL_Type("bigint"), 8, false, GetFieldType("Quantity")));
_FieldInfo.Add("QueueDigest", new FieldInfo("TblProfile", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("QueueDigest")));
_FieldInfo.Add("ReadOnly", new FieldInfo("TblContact", false, GetSQL_Type("bit"), 1, true, GetFieldType("ReadOnly")));
_FieldInfo.Add("ReceiverImportCacheID", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("bigint"), 8, false, GetFieldType("ReceiverImportCacheID")));
_FieldInfo.Add("Recieved", new FieldInfo("TblTransaction", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Recieved")));
_FieldInfo.Add("RedeemBy", new FieldInfo("TblOffer", false, GetSQL_Type("datetime"), 8, false, GetFieldType("RedeemBy")));
_FieldInfo.Add("Redeemed", new FieldInfo("TblTransaction", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Redeemed")));
_FieldInfo.Add("Region", new FieldInfo("TblMetroArea", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Region")));
_FieldInfo.Add("Region", new FieldInfo("TblRegion", false, GetSQL_Type("nvarchar"), 120, false, GetFieldType("Region")));
_FieldInfo.Add("Region", new FieldInfo("TblAreaCode", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Region")));
_FieldInfo.Add("Region", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 50, true, GetFieldType("Region")));
_FieldInfo.Add("Registration", new FieldInfo("TblApplication", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Registration")));
_FieldInfo.Add("RejectCount", new FieldInfo("ReviewProfile_Application", false, GetSQL_Type("int"), 4, false, GetFieldType("RejectCount")));
_FieldInfo.Add("Relationship", new FieldInfo("TypeRelationship", false, GetSQL_Type("varchar"), 20, false, GetFieldType("Relationship")));
_FieldInfo.Add("ReplyToAll", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("ReplyToAll")));
_FieldInfo.Add("Requested", new FieldInfo("CacheInboundRequest", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Requested")));
_FieldInfo.Add("RequestLimit", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("int"), 4, false, GetFieldType("RequestLimit")));
_FieldInfo.Add("RequestType", new FieldInfo("CacheInboundRequest", false, GetSQL_Type("tinyint"), 1, true, GetFieldType("RequestType")));
_FieldInfo.Add("ResultDate", new FieldInfo("HrvHarvesterResultQueue", false, GetSQL_Type("datetime"), 8, true, GetFieldType("ResultDate")));
_FieldInfo.Add("ResultTypeID", new FieldInfo("HrvHarvesterResultQueue", false, GetSQL_Type("int"), 4, true, GetFieldType("ResultTypeID")));
_FieldInfo.Add("RetryCount", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("int"), 4, false, GetFieldType("RetryCount")));
_FieldInfo.Add("ReviewStatus", new FieldInfo("LnkProfile_Profile_MergeReview", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("ReviewStatus")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ", new FieldInfo("TblProfile", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ")));
_FieldInfo.Add("SAQ_City", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_City")));
_FieldInfo.Add("SAQ_Country", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_Country")));
_FieldInfo.Add("SAQ_PostalCode", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_PostalCode")));
_FieldInfo.Add("SAQ_State", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_State")));
_FieldInfo.Add("SAQ_Street1", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_Street1")));
_FieldInfo.Add("SAQ_Street2", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("decimal"), 13, false, GetFieldType("SAQ_Street2")));
_FieldInfo.Add("ScraperSystemID", new FieldInfo("HistSearch", false, GetSQL_Type("bigint"), 8, true, GetFieldType("ScraperSystemID")));
_FieldInfo.Add("Sent", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Sent")));
_FieldInfo.Add("ServerMessage", new FieldInfo("TypeServerMessage", false, GetSQL_Type("varchar"), 500, false, GetFieldType("ServerMessage")));
_FieldInfo.Add("ServerProcessingTime", new FieldInfo("TblContact", false, GetSQL_Type("int"), 4, true, GetFieldType("ServerProcessingTime")));
_FieldInfo.Add("Shipping", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("Shipping")));
_FieldInfo.Add("ShippingTerms", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 50, false, GetFieldType("ShippingTerms")));
_FieldInfo.Add("ShouldPoll", new FieldInfo("TblSmtpAddress", false, GetSQL_Type("bit"), 1, false, GetFieldType("ShouldPoll")));
_FieldInfo.Add("SmtpAddress", new FieldInfo("TblSmtpAddress", false, GetSQL_Type("varchar"), 50, false, GetFieldType("SmtpAddress")));
_FieldInfo.Add("SoldCount", new FieldInfo("TblOffer", false, GetSQL_Type("int"), 4, false, GetFieldType("SoldCount")));
_FieldInfo.Add("Source", new FieldInfo("TypeSource", false, GetSQL_Type("varchar"), 15, false, GetFieldType("Source")));
_FieldInfo.Add("SourceUID", new FieldInfo("HistSearch", false, GetSQL_Type("varchar"), 50, true, GetFieldType("SourceUID")));
_FieldInfo.Add("SourceURL", new FieldInfo("TblMedia", false, GetSQL_Type("varchar"), 255, true, GetFieldType("SourceURL")));
_FieldInfo.Add("STAccount", new FieldInfo("TblDataSourceLogin", false, GetSQL_Type("int"), 4, false, GetFieldType("STAccount")));
_FieldInfo.Add("Started", new FieldInfo("TblOffer", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Started")));
_FieldInfo.Add("State", new FieldInfo("TblZipCode", false, GetSQL_Type("nvarchar"), 8, false, GetFieldType("State")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("StatusFieldUpdates", true, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Note", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Company", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Location", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Media", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Email", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Person", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_WebAddress", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Prefix", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Title", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STFieldUpdate", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("int"), 4, false, GetFieldType("STFieldUpdate")));
_FieldInfo.Add("STNotification", new FieldInfo("ReviewProfile_Application", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("STNotification")));
_FieldInfo.Add("Street", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 60, true, GetFieldType("Street")));
_FieldInfo.Add("Street2", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 40, true, GetFieldType("Street2")));
_FieldInfo.Add("Strength", new FieldInfo("HistSearch", false, GetSQL_Type("int"), 4, true, GetFieldType("Strength")));
_FieldInfo.Add("STReview", new FieldInfo("ReviewProfile_Profile", false, GetSQL_Type("tinyint"), 1, false, GetFieldType("STReview")));
_FieldInfo.Add("STSubscriber", new FieldInfo("StatusSubscribers", true, GetSQL_Type("int"), 4, false, GetFieldType("STSubscriber")));
_FieldInfo.Add("STSubscriber", new FieldInfo("TblSubscriber", false, GetSQL_Type("int"), 4, false, GetFieldType("STSubscriber")));
_FieldInfo.Add("SubscriberStatus", new FieldInfo("StatusSubscribers", false, GetSQL_Type("varchar"), 20, false, GetFieldType("SubscriberStatus")));
_FieldInfo.Add("Summary", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 1000, false, GetFieldType("Summary")));
_FieldInfo.Add("SyncCycleTypeID", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("SyncCycleTypeID")));
_FieldInfo.Add("TableName", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("varchar"), 50, false, GetFieldType("TableName")));
_FieldInfo.Add("TableName", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("varchar"), 50, true, GetFieldType("TableName")));
_FieldInfo.Add("TableName", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("varchar"), 50, false, GetFieldType("TableName")));
_FieldInfo.Add("Tax", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("Tax")));
_FieldInfo.Add("Terms", new FieldInfo("TblOffer", false, GetSQL_Type("varchar"), 120, false, GetFieldType("Terms")));
_FieldInfo.Add("TextVoid", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, true, GetFieldType("TextVoid")));
_FieldInfo.Add("TimeAdded", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("datetime"), 8, false, GetFieldType("TimeAdded")));
_FieldInfo.Add("TimeInMinutes", new FieldInfo("TypePingCycle", false, GetSQL_Type("int"), 4, false, GetFieldType("TimeInMinutes")));
_FieldInfo.Add("TimeSpan", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("int"), 4, false, GetFieldType("TimeSpan")));
_FieldInfo.Add("TimeZone", new FieldInfo("TblTimeZone", false, GetSQL_Type("varchar"), 50, false, GetFieldType("TimeZone")));
_FieldInfo.Add("TimeZoneRegion", new FieldInfo("TblTimeZone", false, GetSQL_Type("varchar"), 100, false, GetFieldType("TimeZoneRegion")));
_FieldInfo.Add("Title", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 200, false, GetFieldType("Title")));
_FieldInfo.Add("Title", new FieldInfo("TypeTitle", false, GetSQL_Type("varchar"), 50, false, GetFieldType("Title")));
_FieldInfo.Add("Title", new FieldInfo("TblCountry", false, GetSQL_Type("nvarchar"), 160, false, GetFieldType("Title")));
_FieldInfo.Add("Title", new FieldInfo("TblNotification", false, GetSQL_Type("varchar"), 200, true, GetFieldType("Title")));
_FieldInfo.Add("Title", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 100, false, GetFieldType("Title")));
_FieldInfo.Add("Token", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 40, true, GetFieldType("Token")));
_FieldInfo.Add("TransactionFee", new FieldInfo("TblOffer", false, GetSQL_Type("money"), 8, false, GetFieldType("TransactionFee")));
_FieldInfo.Add("TransactionIDSeed", new FieldInfo("TblMerchant", false, GetSQL_Type("int"), 4, false, GetFieldType("TransactionIDSeed")));
_FieldInfo.Add("TransactionKey", new FieldInfo("TblTransaction", false, GetSQL_Type("varchar"), 50, false, GetFieldType("TransactionKey")));
_FieldInfo.Add("TransactionStatusID", new FieldInfo("TblTransaction", false, GetSQL_Type("int"), 4, false, GetFieldType("TransactionStatusID")));
_FieldInfo.Add("TYAccount", new FieldInfo("TypeAccount", true, GetSQL_Type("int"), 4, false, GetFieldType("TYAccount")));
_FieldInfo.Add("TYAccount", new FieldInfo("AssocProfile_Merchant", false, GetSQL_Type("int"), 4, false, GetFieldType("TYAccount")));
_FieldInfo.Add("TYCarrier", new FieldInfo("TypeCarrier", true, GetSQL_Type("int"), 4, false, GetFieldType("TYCarrier")));
_FieldInfo.Add("TYCarrier", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 50, false, GetFieldType("TYCarrier")));
_FieldInfo.Add("TYCommunication", new FieldInfo("TypeCommunication", true, GetSQL_Type("int"), 4, false, GetFieldType("TYCommunication")));
_FieldInfo.Add("TYCommunication", new FieldInfo("TblGroup", false, GetSQL_Type("int"), 4, false, GetFieldType("TYCommunication")));
_FieldInfo.Add("TYCreditCard", new FieldInfo("TypeCreditCard", true, GetSQL_Type("int"), 4, false, GetFieldType("TYCreditCard")));
_FieldInfo.Add("TYCreditCard", new FieldInfo("TblCreditCard", false, GetSQL_Type("int"), 4, true, GetFieldType("TYCreditCard")));
_FieldInfo.Add("TYDataAccess", new FieldInfo("TypeDataAccess", true, GetSQL_Type("int"), 4, false, GetFieldType("TYDataAccess")));
_FieldInfo.Add("TYDataAccess", new FieldInfo("TblProfile", false, GetSQL_Type("int"), 4, false, GetFieldType("TYDataAccess")));
_FieldInfo.Add("TYDataInput", new FieldInfo("TypeDataInput", true, GetSQL_Type("int"), 4, false, GetFieldType("TYDataInput")));
_FieldInfo.Add("TYDataInput", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("TYDataInput")));
_FieldInfo.Add("TYDataInput", new FieldInfo("FltDevice_DataInput", false, GetSQL_Type("int"), 4, false, GetFieldType("TYDataInput")));
_FieldInfo.Add("TYDataManagement", new FieldInfo("TypeDataManagement", true, GetSQL_Type("int"), 4, false, GetFieldType("TYDataManagement")));
_FieldInfo.Add("TYDataManagement", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("int"), 4, true, GetFieldType("TYDataManagement")));
_FieldInfo.Add("TYDataSource", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("int"), 4, false, GetFieldType("TYDataSource")));
_FieldInfo.Add("TYHarvesterAction", new FieldInfo("TypeHarvesterAction", true, GetSQL_Type("int"), 4, false, GetFieldType("TYHarvesterAction")));
_FieldInfo.Add("TYLinkSource", new FieldInfo("TypeLinkSource", true, GetSQL_Type("int"), 4, false, GetFieldType("TYLinkSource")));
_FieldInfo.Add("TYLinkSource", new FieldInfo("AssocProfile_Profile", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLinkSource")));
_FieldInfo.Add("TYLinkSource", new FieldInfo("AssocProfile_Contact", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLinkSource")));
_FieldInfo.Add("TYLocation", new FieldInfo("TypeLocation", true, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocContact_Email", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocContact_Location", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYLocation", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("int"), 4, false, GetFieldType("TYLocation")));
_FieldInfo.Add("TYMedia", new FieldInfo("LnkProfile_Media_Hash", false, GetSQL_Type("int"), 4, false, GetFieldType("TYMedia")));
_FieldInfo.Add("TYMediaSource", new FieldInfo("TypeMediaSource", true, GetSQL_Type("int"), 4, false, GetFieldType("TYMediaSource")));
_FieldInfo.Add("TYMediaSource", new FieldInfo("TblMedia", false, GetSQL_Type("int"), 4, true, GetFieldType("TYMediaSource")));
_FieldInfo.Add("TYNotification", new FieldInfo("TypeNotification", true, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotification", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotification", new FieldInfo("TblNotification", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotification", new FieldInfo("AssocProfile_Application", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotification", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotification", new FieldInfo("AssocGroup_Application", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotification")));
_FieldInfo.Add("TYNotificationChannel", new FieldInfo("TypeNotificationChannel", true, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationChannel")));
_FieldInfo.Add("TYNotificationChannel", new FieldInfo("TblNotificationQueue", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationChannel")));
_FieldInfo.Add("TYNotificationChannel", new FieldInfo("TblNotification", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationChannel")));
_FieldInfo.Add("TYNotificationGateway", new FieldInfo("TypeNotificationGateway", true, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationGateway")));
_FieldInfo.Add("TYNotificationVendor", new FieldInfo("TypeNotificationVendor", true, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationVendor")));
_FieldInfo.Add("TYNotificationVendor", new FieldInfo("TblNotificationGateway", false, GetSQL_Type("int"), 4, false, GetFieldType("TYNotificationVendor")));
_FieldInfo.Add("TYPingCycle", new FieldInfo("TypePingCycle", true, GetSQL_Type("int"), 4, false, GetFieldType("TYPingCycle")));
_FieldInfo.Add("TYPingCycle", new FieldInfo("TblDevice", false, GetSQL_Type("int"), 4, false, GetFieldType("TYPingCycle")));
_FieldInfo.Add("TYPrefix", new FieldInfo("TypePrefix", true, GetSQL_Type("int"), 4, false, GetFieldType("TYPrefix")));
_FieldInfo.Add("TYPrefix", new FieldInfo("AssocContact_Prefix", false, GetSQL_Type("int"), 4, false, GetFieldType("TYPrefix")));
_FieldInfo.Add("TYPrefix", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("int"), 4, false, GetFieldType("TYPrefix")));
_FieldInfo.Add("TYRelationship", new FieldInfo("TypeRelationship", true, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("OvrProfile_Profile_RelationshipType", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("TblGroup", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("FltProfile_DataManagement", false, GetSQL_Type("int"), 4, true, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("FltProfile_DataAccess", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("AssocProfile_Note", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("TblContact", false, GetSQL_Type("int"), 4, true, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("LnkProfile_Contact", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYRelationship", new FieldInfo("AssocProfile_Media_Relationship", false, GetSQL_Type("int"), 4, false, GetFieldType("TYRelationship")));
_FieldInfo.Add("TYServerMessage", new FieldInfo("TypeServerMessage", true, GetSQL_Type("int"), 4, false, GetFieldType("TYServerMessage")));
_FieldInfo.Add("TYServerMessage", new FieldInfo("AssocDevice_ServerMessage", false, GetSQL_Type("int"), 4, false, GetFieldType("TYServerMessage")));
_FieldInfo.Add("TYSource", new FieldInfo("TypeSource", true, GetSQL_Type("int"), 4, false, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_WebAddress", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYSource", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("int"), 4, true, GetFieldType("TYSource")));
_FieldInfo.Add("TYTitle", new FieldInfo("TypeTitle", true, GetSQL_Type("int"), 4, false, GetFieldType("TYTitle")));
_FieldInfo.Add("TYTitle", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("int"), 4, false, GetFieldType("TYTitle")));
_FieldInfo.Add("TYTitle", new FieldInfo("AssocContact_Title", false, GetSQL_Type("int"), 4, false, GetFieldType("TYTitle")));
_FieldInfo.Add("TYZipCode", new FieldInfo("TypeZipCode", true, GetSQL_Type("int"), 4, false, GetFieldType("TYZipCode")));
_FieldInfo.Add("TYZipCode", new FieldInfo("TblZipCode", false, GetSQL_Type("int"), 4, false, GetFieldType("TYZipCode")));
_FieldInfo.Add("TYZipCodeLocation", new FieldInfo("TypeZipCodeLocation", true, GetSQL_Type("int"), 4, false, GetFieldType("TYZipCodeLocation")));
_FieldInfo.Add("TYZipCodeLocation", new FieldInfo("TblZipCode", false, GetSQL_Type("int"), 4, false, GetFieldType("TYZipCodeLocation")));
_FieldInfo.Add("TZ", new FieldInfo("TblCity", false, GetSQL_Type("nvarchar"), 20, true, GetFieldType("TZ")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Person", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Email", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Media", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Prefix", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Title", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Phone", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_WebAddress", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("TblSearchThrottle", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_ArchiveRecord", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Title", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Company", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Prefix", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Note", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("OvrProfile_DataAccess", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocContact_Location", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Location", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Note", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Phone", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Person", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("TblContact", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Email", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("AssocProfile_Company", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("TblNote", false, GetSQL_Type("datetime"), 8, false, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("OvrProfile_Profile_RelationshipType", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("CacheProfileGroupDevice", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("TblProfile", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("Updated", new FieldInfo("TblNotification", false, GetSQL_Type("datetime"), 8, true, GetFieldType("Updated")));
_FieldInfo.Add("UpdateFrequency", new FieldInfo("TblDataSource", false, GetSQL_Type("int"), 4, false, GetFieldType("UpdateFrequency")));
_FieldInfo.Add("URL", new FieldInfo("TblNewsChannel", false, GetSQL_Type("varchar"), 200, false, GetFieldType("URL")));
_FieldInfo.Add("URL", new FieldInfo("TblNewsItem", false, GetSQL_Type("varchar"), 500, false, GetFieldType("URL")));
_FieldInfo.Add("UseCommEmail", new FieldInfo("TblProfile", false, GetSQL_Type("bit"), 1, false, GetFieldType("UseCommEmail")));
_FieldInfo.Add("UseCommIVR", new FieldInfo("TblProfile", false, GetSQL_Type("bit"), 1, false, GetFieldType("UseCommIVR")));
_FieldInfo.Add("UseCommSMS", new FieldInfo("TblProfile", false, GetSQL_Type("bit"), 1, false, GetFieldType("UseCommSMS")));
_FieldInfo.Add("UseContactData", new FieldInfo("AssocProfile_Group", false, GetSQL_Type("bit"), 1, false, GetFieldType("UseContactData")));
_FieldInfo.Add("VersionApp", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 20, false, GetFieldType("VersionApp")));
_FieldInfo.Add("VersionOS", new FieldInfo("TblDevice", false, GetSQL_Type("varchar"), 20, false, GetFieldType("VersionOS")));
_FieldInfo.Add("WebAddress", new FieldInfo("TblWebAddress", false, GetSQL_Type("varchar"), 500, false, GetFieldType("WebAddress")));
_FieldInfo.Add("Width", new FieldInfo("TblMedia", false, GetSQL_Type("int"), 4, true, GetFieldType("Width")));
_FieldInfo.Add("Xaxis", new FieldInfo("TblZipCode", false, GetSQL_Type("float"), 8, true, GetFieldType("Xaxis")));
_FieldInfo.Add("Yaxis", new FieldInfo("TblZipCode", false, GetSQL_Type("float"), 8, true, GetFieldType("Yaxis")));
_FieldInfo.Add("Zaxis", new FieldInfo("TblZipCode", false, GetSQL_Type("float"), 8, true, GetFieldType("Zaxis")));
_FieldInfo.Add("ZipCode", new FieldInfo("TblZipCode", false, GetSQL_Type("varchar"), 12, false, GetFieldType("ZipCode")));
_FieldInfo.Add("ZipCode", new FieldInfo("TypeZipCode", false, GetSQL_Type("varchar"), 10, false, GetFieldType("ZipCode")));
_FieldInfo.Add("ZipCode", new FieldInfo("TblLocation", false, GetSQL_Type("varchar"), 20, true, GetFieldType("ZipCode")));
_FieldInfo.Add("ZipCode", new FieldInfo("AssocMetroArea_ZipCode", false, GetSQL_Type("varchar"), 20, false, GetFieldType("ZipCode")));
_FieldInfo.Add("ZipCodeLocation", new FieldInfo("TypeZipCodeLocation", false, GetSQL_Type("varchar"), 15, false, GetFieldType("ZipCodeLocation")));

*/
/*		
        // Initialize the dictionary
        _InputFieldMapper["deviceUID"] = "DeviceUID"; //107 CALLS
        _InputFieldMapper["deviceToken"] = "Token"; //100 CALLS
        _InputFieldMapper["compressionFlagOutput"] = ""; //35 CALLS
        _InputFieldMapper["compressionFlagInput"] = ""; //20 CALLS
        _InputFieldMapper["accessKey"] = "AccessKey"; //15 CALLS
        _InputFieldMapper["gatewayKey"] = "";  //10 CALLS //freaking weird number to compare app token or something

        _InputFieldMapper["contactXML"] = ""; //14 CALLS
        _InputFieldMapper["inlineMediaFlag"] = ""; //14 CALLS 
        _InputFieldMapper["inlineNoteFlag"] = ""; //13 CALLS 
        _InputFieldMapper["pimContactUID"] = "PIMContactID"; //12 CALLS 
        _InputFieldMapper["contactCount"] = "";  //10 CALLS //does nothing
        _InputFieldMapper["devicePhoneNumber"] = "Phone"; //11 CALLS
        _InputFieldMapper["contactFirstName"] = "Person"; //10 CALLS
        _InputFieldMapper["contactLastName"] = "Person"; //10 CALLS
        _InputFieldMapper["usrPwd"] = "Password"; //8 CALLS 

        _InputFieldMapper["deviceManufacturer"] = "Manufacturer"; //6 CALLS
        _InputFieldMapper["deviceModel"] = "Model"; //6 CALLS
        _InputFieldMapper["deviceNetwork"] = "Carrier"; //6 CALLS
        _InputFieldMapper["deviceOSVersion"] = "VersionOS"; //6 CALLS
        _InputFieldMapper["devicePIN"] = "PIN"; //6 CALLS
        _InputFieldMapper["imageB64String"] = ""; //6 CALLS 
        _InputFieldMapper["serverId"] = ""; //6 CALLS //completely useless
        _InputFieldMapper["serverKey"] = ""; //6 CALLS //hardcoded GUID
        _InputFieldMapper["jakeVersion"] = "VersionApp"; //6 CALLS 

        _InputFieldMapper["echoString"] = ""; //5 CALLS
        _InputFieldMapper["emailAddress"] = "Email"; //5 CALLS
        _InputFieldMapper["groupName"] = "Group"; //5 CALLS
        _InputFieldMapper["mobilePhoneNumber"] = "Phone"; //5 CALLS 
        _InputFieldMapper["newProfileOnTieFlag"] = ""; //5 CALLS 

        _InputFieldMapper["contactEmail2"] = "Email"; //4 CALLS
        _InputFieldMapper["contactEmail3"] = "Email"; //4 CALLS
        _InputFieldMapper["contactOrg"] = "Company"; //4 CALLS
        _InputFieldMapper["contactPrefix"] = "Prefix"; //4 CALLS
        _InputFieldMapper["contactTitle"] = "Title"; //4 CALLS
        _InputFieldMapper["faxPhoneNumber"] = "Phone"; //4 CALLS
        _InputFieldMapper["home2PhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["homeAddress1"] = "Street"; //4 CALLS 
        _InputFieldMapper["homeAddress2"] = "Street2"; //4 CALLS 
        _InputFieldMapper["homeCity"] = "City"; //4 CALLS 
        _InputFieldMapper["homeCountry"] = "Country"; //4 CALLS 
        _InputFieldMapper["homePhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["homePostalCode"] = "ZipCode"; //4 CALLS 
        _InputFieldMapper["homeState"] = "State"; //4 CALLS 
        _InputFieldMapper["otherPhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["pagerPhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["preferredProfileId"] = "IDProfile"; //4 CALLS 
        _InputFieldMapper["testString"] = ""; //4 CALLS 
        _InputFieldMapper["work2PhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["workAddress1"] = "Street"; //4 CALLS 
        _InputFieldMapper["workAddress2"] = "Street2"; //4 CALLS 
        _InputFieldMapper["workCity"] = "City"; //4 CALLS 
        _InputFieldMapper["workCountry"] = "Country";  //4 CALLS 
        _InputFieldMapper["workPhoneNumber"] = "Phone"; //4 CALLS 
        _InputFieldMapper["workPostalCode"] = "ZipCode"; //4 CALLS 
        _InputFieldMapper["workState"] = "State"; //4 CALLS 

        _InputFieldMapper["communicationsType"] = "";  //3 CALLS //TODO: create table
        _InputFieldMapper["contactEmail"] = "Email"; //3 CALLS
        _InputFieldMapper["groupId"] = "IDGroup"; //3 CALLS
        _InputFieldMapper["mediaB64String"] = ""; //3 CALLS 
        _InputFieldMapper["onlyNonContactsFlag"] = ""; //3 CALLS 
        _InputFieldMapper["systemToken"] = ""; //3 CALLS //hardcoded GUID
        _InputFieldMapper["profileXML"] = ""; //3 CALLS 
		
        _InputFieldMapper["Body"] = "";  //2 CALLS //message body
        _InputFieldMapper["communicationsChannel"] = "Communication"; //2 CALLS
        _InputFieldMapper["confirmationCode"] = "ConfirmationCode"; //2 CALLS
        _InputFieldMapper["disableCommunicationsFlag"] = ""; //2 CALLS
        _InputFieldMapper["groupVisibilityFlag"] = ""; //2 CALLS //a flag that can hold more values than binary, nice!
        _InputFieldMapper["imageData"] = ""; //2 CALLS 
        _InputFieldMapper["itemParameters"] = "ItemProperties"; //2 CALLS 
        _InputFieldMapper["memberXML"] = ""; //2 CALLS 
        _InputFieldMapper["phoneNumber"] = "Phone"; //2 CALLS 
        _InputFieldMapper["profileCount"] = ""; //2 CALLS 
        _InputFieldMapper["profileId"] = "IDProfile"; //2 CALLS 
        _InputFieldMapper["profileShareXML"] = ""; //2 CALLS 
        _InputFieldMapper["purchasedSinceUnixtime"] = ""; //2 CALLS 
        _InputFieldMapper["relationshipType"] = "Relationship"; //2 CALLS 
        _InputFieldMapper["shippingCity"] = "City"; //2 CALLS 
        _InputFieldMapper["shippingCountry"] = "Country"; //2 CALLS 
        _InputFieldMapper["shippingDescription"] = "Shipping"; //2 CALLS 
        _InputFieldMapper["shippingPostalCode"] = "ZipCode"; //2 CALLS 
        _InputFieldMapper["shippingState"] = "State"; //2 CALLS 
        _InputFieldMapper["shippingStreet1"] = "Street"; //2 CALLS 
        _InputFieldMapper["shippingStreet2"] = "Street2"; //2 CALLS 
        _InputFieldMapper["taxDescription"] = "Tax"; //2 CALLS 
        _InputFieldMapper["thisDate"] = ""; //2 CALLS 
        _InputFieldMapper["SmsSid"] = ""; //2 CALLS //used in one call for logging

        _InputFieldMapper["AccountSid"] = "";
        _InputFieldMapper["annotation"] = ""; //part of a message
        _InputFieldMapper["appConB64String"] = ""; //application binary data
        _InputFieldMapper["attributes"] = ""; //harvester test
        _InputFieldMapper["billingCity"] = "City";
        _InputFieldMapper["billingCountryCode"] = "Country";
        _InputFieldMapper["billingPhoneNumber"] = "Phone";
        _InputFieldMapper["billingPostalCode"] = "ZipCode";
        _InputFieldMapper["billingState"] = "State";
        _InputFieldMapper["billingStreet1"] = "Street";
        _InputFieldMapper["billingStreet2"] = "Street2";
        _InputFieldMapper["cardType"] = "CreditCard";
        _InputFieldMapper["ccNumber"] = "CreditCardNumber";
        _InputFieldMapper["code"] = "CreditCardCode";
        _InputFieldMapper["comData"] = "";
        _InputFieldMapper["companyName"] = "Company";
		
        _InputFieldMapper["confirmFaxPhone"] = ""; //using weird codes here!
        _InputFieldMapper["confirmFirstName"] = "";
        _InputFieldMapper["confirmFlag"] = "";
        _InputFieldMapper["confirmHome2Phone"] = "";
        _InputFieldMapper["confirmHomeAddress"] = "";
        _InputFieldMapper["confirmHomePhone"] = "";
        _InputFieldMapper["confirmLastName"] = "";
        _InputFieldMapper["confirmMedia"] = "";
        _InputFieldMapper["confirmMobilePhone"] = "";
        _InputFieldMapper["confirmNote"] = "";
        _InputFieldMapper["confirmOrg"] = "";
        _InputFieldMapper["confirmOtherPhone"] = "";
        _InputFieldMapper["confirmPagerPhone"] = "";
        _InputFieldMapper["confirmPrefix"] = "";
        _InputFieldMapper["confirmTitle"] = "";
        _InputFieldMapper["confirmWork2Phone"] = "";
        _InputFieldMapper["confirmWorkAddress"] = "";
        _InputFieldMapper["confirmWorkPhone"] = "";
		
        _InputFieldMapper["contactEmail1"] = "Email";
        _InputFieldMapper["contactEmail4"] = "Email";
        _InputFieldMapper["contactEmail5"] = "Email";
        _InputFieldMapper["contactEmail6"] = "Email";
        _InputFieldMapper["dataSource"] = "DataSource";
        _InputFieldMapper["deviceChunkSize"] = "ChunkCount";
        _InputFieldMapper["dropProfileFlag"] = "";
        _InputFieldMapper["email"] = "Email";
        _InputFieldMapper["emailXML"] = ""; //weirdness here, watch out!
        _InputFieldMapper["feedUrl"] = "";
        _InputFieldMapper["firstName"] = "Person";
        _InputFieldMapper["From"] = "Phone";
        _InputFieldMapper["groupCommunicationsType"] = ""; //TODO: create table
        _InputFieldMapper["groupFlag"] = "IDRelationship";
        _InputFieldMapper["harvesterCertificate"] = "";
        _InputFieldMapper["homeCountryCode"] = "FIPS";
        _InputFieldMapper["key"] = ""; //ServerKey
        _InputFieldMapper["lastName"] = "Person";
        _InputFieldMapper["limit"] = ""; //harvester
        _InputFieldMapper["logString"] = "";
        _InputFieldMapper["messageBody"] = "Body";
        _InputFieldMapper["messageTitle"] = "Title";
        _InputFieldMapper["mobile"] = "Phone";
        _InputFieldMapper["month"] = "ExpirationMonth";
        _InputFieldMapper["name"] = "Person";
        _InputFieldMapper["noteXML"] = "";
        _InputFieldMapper["notificationKey"] = ""; //hardcoded GUID
        _InputFieldMapper["period"] = ""; //harvester
        _InputFieldMapper["pimContactUID_new"] = "PIMContactID";
        _InputFieldMapper["pimContactUID_original"] = "PIMContactID";
        _InputFieldMapper["purchasedThroughUnixtime"] = "";
        _InputFieldMapper["repeatFlag"] = "";
        _InputFieldMapper["request"] = ""; //harvester
        _InputFieldMapper["responseFormat"] = ""; //completely useless
        _InputFieldMapper["returnDefaultOnly"] = "";
        _InputFieldMapper["SmsStatus"] = ""; //used in one call for logging
        _InputFieldMapper["start"] = ""; //harvester
        _InputFieldMapper["subject"] = ""; //used as part of a string that goes into cache table inboundRequestCache
        _InputFieldMapper["timeZoneIdString1"] = ""; //test time convert
        _InputFieldMapper["timeZoneIdString2"] = ""; //test time convert
        _InputFieldMapper["To"] = "Phone";
        _InputFieldMapper["updatesOnlyFlag"] = "";
        _InputFieldMapper["year"] = "ExpirationYear";
*/

