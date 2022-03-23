using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Collections.Concurrent;

namespace JakeKnowsEngineComponent
{
    public partial class AssocApplication_NewsChannel : CTable
    {
        private SqlInt32 _IDApplication;
        private SqlInt32 _IDNewsChannel;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDApplication = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDApplication; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDNewsChannel; }
        public override void SetValue(SqlString val) { }

        public AssocApplication_NewsChannel() { }
        public AssocApplication_NewsChannel(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocApplication_NewsChannel(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDApplication = rd.SafeGetInt32("IDApplication");
            _IDNewsChannel = rd.SafeGetInt32("IDNewsChannel");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDApplication);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDNewsChannel);
        }

        #endregion
        #region accessors
        public SqlInt32 IDApplication
        {
            get { return _IDApplication; }
            set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDNewsChannel
        {
            get { return _IDNewsChannel; }
            set
            {
                _IDNewsChannel = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Company : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt32 _IDCompany;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDCompany; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Company() { }
        public AssocContact_Company(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Company(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDCompany = rd.SafeGetInt32("IDCompany");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDCompany);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCompany
        {
            get { return _IDCompany; }
            set
            {
                _IDCompany = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Email : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDEmail;
        private SqlInt32 _TYLocation;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;
        private SqlBoolean _IsProcessing;
        private SqlByte _ListOrder;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDEmail; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Email() { }
        public AssocContact_Email(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Email(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            _IsProcessing = rd.SafeGetBoolean("IsProcessing");
            _ListOrder = rd.SafeGetByte("ListOrder");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDEmail);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _IsProcessing);
            cmd.Parameters.AddWithValue("@@colp@", _ListOrder);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsProcessing
        {
            get { return _IsProcessing; }
            set
            {
                _IsProcessing = value;
                IsDirty = true;
            }
        }
        public SqlByte ListOrder
        {
            get { return _ListOrder; }
            set
            {
                _ListOrder = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Location : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDLocation;
        private SqlInt32 _TYLocation;
        private SqlInt64 _IDLocationStreet;
        private SqlInt64 _IDLocationStreet2;
        private SqlInt64 _IDLocationCity;
        private SqlInt64 _IDLocationState;
        private SqlInt64 _IDLocationCountry;
        private SqlInt64 _IDLocationZipCode;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDLocation; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Location() { }
        public AssocContact_Location(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Location(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDLocation = rd.SafeGetInt64("IDLocation");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDLocation);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationCity);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationState);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationZipCode);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocation
        {
            get { return _IDLocation; }
            set
            {
                _IDLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Media : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDMedia;
        private SqlInt32 _STFieldUpdate;
        private SqlString _MD5Hash;  //50
        public const int MD5HashMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMedia; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Media() { }
        public AssocContact_Media(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Media(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _MD5Hash = rd.SafeGetString("MD5Hash");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMedia);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _MD5Hash);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlString MD5Hash
        {
            get { return _MD5Hash; }
            set
            {
                _MD5Hash = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Note : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDNote;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;
        private SqlInt32 _NoteLength;
        private SqlString _MD5Hash;  //50
        public const int MD5HashMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDNote; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Note() { }
        public AssocContact_Note(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Note(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDNote = rd.SafeGetInt64("IDNote");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            _NoteLength = rd.SafeGetInt32("NoteLength");
            _MD5Hash = rd.SafeGetString("MD5Hash");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDNote);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _NoteLength);
            cmd.Parameters.AddWithValue("@@colp@", _MD5Hash);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDNote
        {
            get { return _IDNote; }
            set
            {
                _IDNote = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlInt32 NoteLength
        {
            get { return _NoteLength; }
            set
            {
                _NoteLength = value;
                IsDirty = true;
            }
        }
        public SqlString MD5Hash
        {
            get { return _MD5Hash; }
            set
            {
                _MD5Hash = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Person : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDPerson;
        private SqlByte _NamePosition;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDPerson; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Person() { }
        public AssocContact_Person(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Person(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDPerson = rd.SafeGetInt64("IDPerson");
            _NamePosition = rd.SafeGetByte("NamePosition");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDPerson);
            cmd.Parameters.AddWithValue("@@colp@", _NamePosition);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPerson
        {
            get { return _IDPerson; }
            set
            {
                _IDPerson = value;
                IsDirty = true;
            }
        }
        public SqlByte NamePosition
        {
            get { return _NamePosition; }
            set
            {
                _NamePosition = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Phone : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDPhone;
        private SqlInt32 _TYLocation;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDPhone; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Phone() { }
        public AssocContact_Phone(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Phone(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDPhone);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Prefix : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt32 _TYPrefix;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYPrefix; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Prefix() { }
        public AssocContact_Prefix(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Prefix(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _TYPrefix = rd.SafeGetInt32("TYPrefix");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYPrefix);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYPrefix
        {
            get { return _TYPrefix; }
            set
            {
                _TYPrefix = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_Title : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt32 _TYTitle;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYTitle; }
        public override void SetValue(SqlString val) { }

        public AssocContact_Title() { }
        public AssocContact_Title(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_Title(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _TYTitle = rd.SafeGetInt32("TYTitle");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYTitle);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYTitle
        {
            get { return _TYTitle; }
            set
            {
                _TYTitle = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocContact_WebAddress : CTable
    {
        private SqlInt64 _IDContact;
        private SqlInt64 _IDWebAddress;
        private SqlBoolean _IsManaged;
        private SqlInt32 _STFieldUpdate;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDWebAddress; }
        public override void SetValue(SqlString val) { }

        public AssocContact_WebAddress() { }
        public AssocContact_WebAddress(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocContact_WebAddress(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDWebAddress = rd.SafeGetInt64("IDWebAddress");
            _IsManaged = rd.SafeGetBoolean("IsManaged");
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDWebAddress);
            cmd.Parameters.AddWithValue("@@colp@", _IsManaged);
            cmd.Parameters.AddWithValue("@@colp@", _STFieldUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDWebAddress
        {
            get { return _IDWebAddress; }
            set
            {
                _IDWebAddress = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManaged
        {
            get { return _IsManaged; }
            set
            {
                _IsManaged = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocDevice_ServerMessage : CTable
    {
        private SqlInt64 _IDDevice;
        private SqlInt32 _TYServerMessage;
        private SqlBoolean _IsDelivered;
        private SqlDateTime _Delivered;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDevice = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDevice; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYServerMessage; }
        public override void SetValue(SqlString val) { }

        public AssocDevice_ServerMessage() { }
        public AssocDevice_ServerMessage(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocDevice_ServerMessage(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _TYServerMessage = rd.SafeGetInt32("TYServerMessage");
            _IsDelivered = rd.SafeGetBoolean("IsDelivered");
            _Delivered = rd.SafeGetDateTime("Delivered");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@TYServerMessage", _TYServerMessage);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@TYServerMessage", _TYServerMessage);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYServerMessage", _TYServerMessage);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYServerMessage);
            cmd.Parameters.AddWithValue("@@colp@", _IsDelivered);
            cmd.Parameters.AddWithValue("@@colp@", _Delivered);
        }

        #endregion
        #region accessors
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYServerMessage
        {
            get { return _TYServerMessage; }
            set
            {
                _TYServerMessage = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsDelivered
        {
            get { return _IsDelivered; }
            set
            {
                _IsDelivered = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Delivered
        {
            get { return _Delivered; }
            set
            {
                _Delivered = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocGroup_Application : CTable
    {
        private SqlInt64 _IDGroup;
        private SqlInt32 _IDApplication;
        private SqlInt32 _TYNotification;
        private SqlBoolean _CanProcess;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDApplication; }
        public override void SetValue(SqlString val) { }

        public AssocGroup_Application() { }
        public AssocGroup_Application(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocGroup_Application(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDApplication = rd.SafeGetInt32("IDApplication");
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _CanProcess = rd.SafeGetBoolean("CanProcess");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDApplication);
            cmd.Parameters.AddWithValue("@@colp@", _TYNotification);
            cmd.Parameters.AddWithValue("@@colp@", _CanProcess);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDApplication
        {
            get { return _IDApplication; }
            set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanProcess
        {
            get { return _CanProcess; }
            set
            {
                _CanProcess = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocGroup_Group : CTable
    {
        private SqlInt64 _IDGroup_Parent;
        private SqlInt64 _IDGroup_Child;
        private SqlBoolean _DisableCommunications;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup_Parent = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup_Parent; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup_Child; }
        public override void SetValue(SqlString val) { }

        public AssocGroup_Group() { }
        public AssocGroup_Group(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocGroup_Group(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup_Parent = rd.SafeGetInt64("IDGroup_Parent");
            _IDGroup_Child = rd.SafeGetInt64("IDGroup_Child");
            _DisableCommunications = rd.SafeGetBoolean("DisableCommunications");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup_Parent", _IDGroup_Parent);
            cmd.Parameters.AddWithValue("@IDGroup_Child", _IDGroup_Child);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup_Parent", _IDGroup_Parent);
            cmd.Parameters.AddWithValue("@IDGroup_Child", _IDGroup_Child);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup_Child", _IDGroup_Child);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup_Parent);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup_Child);
            cmd.Parameters.AddWithValue("@@colp@", _DisableCommunications);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup_Parent
        {
            get { return _IDGroup_Parent; }
            set
            {
                _IDGroup_Parent = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup_Child
        {
            get { return _IDGroup_Child; }
            set
            {
                _IDGroup_Child = value;
                IsDirty = true;
            }
        }
        public SqlBoolean DisableCommunications
        {
            get { return _DisableCommunications; }
            set
            {
                _DisableCommunications = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocGroup_InterestAttribute : CTable
    {
        private SqlInt64 _IDGroup;
        private SqlInt32 _IDInterestAttribute;
        private SqlBoolean _IsManual;
        private SqlBoolean _IsExcluded;
        private SqlDecimal _PercentOfBase;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDInterestAttribute; }
        public override void SetValue(SqlString val) { }

        public AssocGroup_InterestAttribute() { }
        public AssocGroup_InterestAttribute(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocGroup_InterestAttribute(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDInterestAttribute = rd.SafeGetInt32("IDInterestAttribute");
            _IsManual = rd.SafeGetBoolean("IsManual");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _PercentOfBase = rd.SafeGetDecimal("PercentOfBase");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            cmd.Parameters.AddWithValue("@IsManual", _IsManual);
            cmd.Parameters.AddWithValue("@IsExcluded", _IsExcluded);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            cmd.Parameters.AddWithValue("@IsManual", _IsManual);
            cmd.Parameters.AddWithValue("@IsExcluded", _IsExcluded);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            cmd.Parameters.AddWithValue("@IsManual", _IsManual);
            cmd.Parameters.AddWithValue("@IsExcluded", _IsExcluded);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute);
            cmd.Parameters.AddWithValue("@@colp@", _IsManual);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _PercentOfBase);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute
        {
            get { return _IDInterestAttribute; }
            set
            {
                _IDInterestAttribute = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManual
        {
            get { return _IsManual; }
            set
            {
                _IsManual = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDecimal PercentOfBase
        {
            get { return _PercentOfBase; }
            set
            {
                _PercentOfBase = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocGroup_MetroArea : CTable
    {
        private SqlInt64 _IDGroup;
        private SqlInt32 _IDMetroArea;
        private SqlBoolean _IsManual;
        private SqlBoolean _IsExcluded;
        private SqlDecimal _PercentOfBase;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMetroArea; }
        public override void SetValue(SqlString val) { }

        public AssocGroup_MetroArea() { }
        public AssocGroup_MetroArea(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocGroup_MetroArea(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            _IsManual = rd.SafeGetBoolean("IsManual");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _PercentOfBase = rd.SafeGetDecimal("PercentOfBase");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMetroArea);
            cmd.Parameters.AddWithValue("@@colp@", _IsManual);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _PercentOfBase);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManual
        {
            get { return _IsManual; }
            set
            {
                _IsManual = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDecimal PercentOfBase
        {
            get { return _PercentOfBase; }
            set
            {
                _PercentOfBase = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocGroup_NewsChannel : CTable
    {
        private SqlInt64 _IDGroup;
        private SqlInt32 _IDNewsChannel;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDNewsChannel; }
        public override void SetValue(SqlString val) { }

        public AssocGroup_NewsChannel() { }
        public AssocGroup_NewsChannel(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocGroup_NewsChannel(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDNewsChannel = rd.SafeGetInt32("IDNewsChannel");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDNewsChannel);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDNewsChannel
        {
            get { return _IDNewsChannel; }
            set
            {
                _IDNewsChannel = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocInterestAttribute_InterestAttribute_Strength : CTable
    {
        private SqlInt32 _IDInterestAttribute_1;
        private SqlInt32 _IDInterestAttribute_2;
        private SqlInt32 _AssociationStrength;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDInterestAttribute_1 = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDInterestAttribute_1; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDInterestAttribute_2; }
        public override void SetValue(SqlString val) { }

        public AssocInterestAttribute_InterestAttribute_Strength() { }
        public AssocInterestAttribute_InterestAttribute_Strength(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocInterestAttribute_InterestAttribute_Strength(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDInterestAttribute_1 = rd.SafeGetInt32("IDInterestAttribute_1");
            _IDInterestAttribute_2 = rd.SafeGetInt32("IDInterestAttribute_2");
            _AssociationStrength = rd.SafeGetInt32("AssociationStrength");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDInterestAttribute_1", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDInterestAttribute_1", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestAttribute_1);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute_2);
            cmd.Parameters.AddWithValue("@@colp@", _AssociationStrength);
        }

        #endregion
        #region accessors
        public SqlInt32 IDInterestAttribute_1
        {
            get { return _IDInterestAttribute_1; }
            set
            {
                _IDInterestAttribute_1 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute_2
        {
            get { return _IDInterestAttribute_2; }
            set
            {
                _IDInterestAttribute_2 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AssociationStrength
        {
            get { return _AssociationStrength; }
            set
            {
                _AssociationStrength = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocMetroArea_AreaCode : CTable
    {
        private SqlInt32 _IDMetroArea;
        private SqlInt32 _AreaCode;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDMetroArea = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDMetroArea; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_AreaCode; }
        public override void SetValue(SqlString val) { }

        public AssocMetroArea_AreaCode() { }
        public AssocMetroArea_AreaCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocMetroArea_AreaCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            _AreaCode = rd.SafeGetInt32("AreaCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            cmd.Parameters.AddWithValue("@AreaCode", _AreaCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            cmd.Parameters.AddWithValue("@AreaCode", _AreaCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@AreaCode", _AreaCode);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _AreaCode);
        }

        #endregion
        #region accessors
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AreaCode
        {
            get { return _AreaCode; }
            set
            {
                _AreaCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocMetroArea_ZipCode : CTable
    {
        private SqlInt32 _IDMetroArea;
        private SqlInt64 _IDZipCode;
        private SqlString _ZipCode;  //20
        public const int ZipCodeMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDMetroArea = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDMetroArea; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDZipCode; }
        public override void SetValue(SqlString val) { }

        public AssocMetroArea_ZipCode() { }
        public AssocMetroArea_ZipCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocMetroArea_ZipCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            _IDZipCode = rd.SafeGetInt64("IDZipCode");
            _ZipCode = rd.SafeGetString("ZipCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            cmd.Parameters.AddWithValue("@IDZipCode", _IDZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            cmd.Parameters.AddWithValue("@IDZipCode", _IDZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDZipCode", _IDZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDZipCode);
            cmd.Parameters.AddWithValue("@@colp@", _ZipCode);
        }

        #endregion
        #region accessors
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDZipCode
        {
            get { return _IDZipCode; }
            set
            {
                _IDZipCode = value;
                IsDirty = true;
            }
        }
        public SqlString ZipCode
        {
            get { return _ZipCode; }
            set
            {
                _ZipCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocOffer_Group : CTable
    {
        private SqlInt64 _IDOffer;
        private SqlInt64 _IDGroup;
        private SqlBoolean _AddManually;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDOffer = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDOffer; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { }

        public AssocOffer_Group() { }
        public AssocOffer_Group(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocOffer_Group(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _AddManually = rd.SafeGetBoolean("AddManually");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup);
            cmd.Parameters.AddWithValue("@@colp@", _AddManually);
        }

        #endregion
        #region accessors
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlBoolean AddManually
        {
            get { return _AddManually; }
            set
            {
                _AddManually = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocOffer_Group_InterestAttribute : CTable
    {
        private SqlInt64 _IDOffer;
        private SqlInt64 _IDGroup;
        private SqlInt32 _IDInterestAttribute;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDOffer = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDOffer; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { }

        public AssocOffer_Group_InterestAttribute() { }
        public AssocOffer_Group_InterestAttribute(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocOffer_Group_InterestAttribute(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDInterestAttribute = rd.SafeGetInt32("IDInterestAttribute");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup);
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute);
        }

        #endregion
        #region accessors
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute
        {
            get { return _IDInterestAttribute; }
            set
            {
                _IDInterestAttribute = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocOffer_MetroArea : CTable
    {
        private SqlInt64 _IDOffer;
        private SqlInt32 _IDMetroArea;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDOffer = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDOffer; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMetroArea; }
        public override void SetValue(SqlString val) { }

        public AssocOffer_MetroArea() { }
        public AssocOffer_MetroArea(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocOffer_MetroArea(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMetroArea);
        }

        #endregion
        #region accessors
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Application : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDApplication;
        private SqlInt32 _TYNotification;
        private SqlBoolean _CanProcess;
        private SqlBoolean _CanBlock;
        private SqlBoolean _IsAdmin;
        private SqlBoolean _IsDefault;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDApplication; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Application() { }
        public AssocProfile_Application(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Application(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDApplication = rd.SafeGetInt32("IDApplication");
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _CanProcess = rd.SafeGetBoolean("CanProcess");
            _CanBlock = rd.SafeGetBoolean("CanBlock");
            _IsAdmin = rd.SafeGetBoolean("IsAdmin");
            _IsDefault = rd.SafeGetBoolean("IsDefault");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDApplication);
            cmd.Parameters.AddWithValue("@@colp@", _TYNotification);
            cmd.Parameters.AddWithValue("@@colp@", _CanProcess);
            cmd.Parameters.AddWithValue("@@colp@", _CanBlock);
            cmd.Parameters.AddWithValue("@@colp@", _IsAdmin);
            cmd.Parameters.AddWithValue("@@colp@", _IsDefault);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDApplication
        {
            get { return _IDApplication; }
            set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanProcess
        {
            get { return _CanProcess; }
            set
            {
                _CanProcess = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanBlock
        {
            get { return _CanBlock; }
            set
            {
                _CanBlock = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsDefault
        {
            get { return _IsDefault; }
            set
            {
                _IsDefault = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_ArchiveRecord : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDArchive;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDArchive; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_ArchiveRecord() { }
        public AssocProfile_ArchiveRecord(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_ArchiveRecord(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDArchive = rd.SafeGetInt64("IDArchive");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDArchive", _IDArchive);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDArchive", _IDArchive);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDArchive", _IDArchive);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDArchive);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDArchive
        {
            get { return _IDArchive; }
            set
            {
                _IDArchive = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Company : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDCompany;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDCompany; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Company() { }
        public AssocProfile_Company(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Company(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDCompany = rd.SafeGetInt32("IDCompany");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDCompany);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCompany
        {
            get { return _IDCompany; }
            set
            {
                _IDCompany = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Contact : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDContact;
        private SqlInt32 _TYLinkSource;
        private SqlInt32 _IDLinkSource;
        private SqlByte _LinkStrength;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDContact; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Contact() { }
        public AssocProfile_Contact(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Contact(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDContact = rd.SafeGetInt64("IDContact");
            _TYLinkSource = rd.SafeGetInt32("TYLinkSource");
            _IDLinkSource = rd.SafeGetInt32("IDLinkSource");
            _LinkStrength = rd.SafeGetByte("LinkStrength");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDContact);
            cmd.Parameters.AddWithValue("@@colp@", _TYLinkSource);
            cmd.Parameters.AddWithValue("@@colp@", _IDLinkSource);
            cmd.Parameters.AddWithValue("@@colp@", _LinkStrength);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLinkSource
        {
            get { return _TYLinkSource; }
            set
            {
                _TYLinkSource = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDLinkSource
        {
            get { return _IDLinkSource; }
            set
            {
                _IDLinkSource = value;
                IsDirty = true;
            }
        }
        public SqlByte LinkStrength
        {
            get { return _LinkStrength; }
            set
            {
                _LinkStrength = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Contact_InterestAttributes : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDContact;
        private SqlInt32 _LinkStrength;
        private SqlInt32 _IDInterestAttribute_1;
        private SqlInt32 _IDInterestAttribute_2;
        private SqlInt32 _IDInterestArea;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDContact; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Contact_InterestAttributes() { }
        public AssocProfile_Contact_InterestAttributes(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Contact_InterestAttributes(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDContact = rd.SafeGetInt64("IDContact");
            _LinkStrength = rd.SafeGetInt32("LinkStrength");
            _IDInterestAttribute_1 = rd.SafeGetInt32("IDInterestAttribute_1");
            _IDInterestAttribute_2 = rd.SafeGetInt32("IDInterestAttribute_2");
            _IDInterestArea = rd.SafeGetInt32("IDInterestArea");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_1", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_1", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_1", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@IDInterestAttribute_2", _IDInterestAttribute_2);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDContact);
            cmd.Parameters.AddWithValue("@@colp@", _LinkStrength);
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute_1);
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute_2);
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestArea);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 LinkStrength
        {
            get { return _LinkStrength; }
            set
            {
                _LinkStrength = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute_1
        {
            get { return _IDInterestAttribute_1; }
            set
            {
                _IDInterestAttribute_1 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute_2
        {
            get { return _IDInterestAttribute_2; }
            set
            {
                _IDInterestAttribute_2 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestArea
        {
            get { return _IDInterestArea; }
            set
            {
                _IDInterestArea = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Email : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDEmail;
        private SqlInt32 _TYLocation;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDEmail; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Email() { }
        public AssocProfile_Email(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Email(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDEmail);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Group : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDGroup;
        private SqlInt64 _IDContact;
        private SqlInt64 _IDPhone_Preferred;
        private SqlInt64 _IDEmail_Preferred;
        private SqlInt64 _IDEmail_NotificationGateway;
        private SqlInt64 _IDPhone_NotificationGateway;
        private SqlInt32 _TYRelationship;
        private SqlInt32 _TYNotification;
        private SqlDateTime _Updated;
        private SqlDateTime _InvitationSent;
        private SqlDateTime _InvitationRequested;
        private SqlByte _ApprovalStatus;
        private SqlBoolean _CanSMS;
        private SqlBoolean _CanEmail;
        private SqlBoolean _CanIVR;
        private SqlBoolean _IsAdmin;
        private SqlBoolean _IsVisible;
        private SqlBoolean _DisableCommunications;
        private SqlBoolean _ReplyToAll;
        private SqlBoolean _UseContactData;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Group() { }
        public AssocProfile_Group(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Group(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDPhone_Preferred = rd.SafeGetInt64("IDPhone_Preferred");
            _IDEmail_Preferred = rd.SafeGetInt64("IDEmail_Preferred");
            _IDEmail_NotificationGateway = rd.SafeGetInt64("IDEmail_NotificationGateway");
            _IDPhone_NotificationGateway = rd.SafeGetInt64("IDPhone_NotificationGateway");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _Updated = rd.SafeGetDateTime("Updated");
            _InvitationSent = rd.SafeGetDateTime("InvitationSent");
            _InvitationRequested = rd.SafeGetDateTime("InvitationRequested");
            _ApprovalStatus = rd.SafeGetByte("ApprovalStatus");
            _CanSMS = rd.SafeGetBoolean("CanSMS");
            _CanEmail = rd.SafeGetBoolean("CanEmail");
            _CanIVR = rd.SafeGetBoolean("CanIVR");
            _IsAdmin = rd.SafeGetBoolean("IsAdmin");
            _IsVisible = rd.SafeGetBoolean("IsVisible");
            _DisableCommunications = rd.SafeGetBoolean("DisableCommunications");
            _ReplyToAll = rd.SafeGetBoolean("ReplyToAll");
            _UseContactData = rd.SafeGetBoolean("UseContactData");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup);
            cmd.Parameters.AddWithValue("@@colp@", _IDContact);
            cmd.Parameters.AddWithValue("@@colp@", _IDPhone_Preferred);
            cmd.Parameters.AddWithValue("@@colp@", _IDEmail_Preferred);
            cmd.Parameters.AddWithValue("@@colp@", _IDEmail_NotificationGateway);
            cmd.Parameters.AddWithValue("@@colp@", _IDPhone_NotificationGateway);
            cmd.Parameters.AddWithValue("@@colp@", _TYRelationship);
            cmd.Parameters.AddWithValue("@@colp@", _TYNotification);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _InvitationSent);
            cmd.Parameters.AddWithValue("@@colp@", _InvitationRequested);
            cmd.Parameters.AddWithValue("@@colp@", _ApprovalStatus);
            cmd.Parameters.AddWithValue("@@colp@", _CanSMS);
            cmd.Parameters.AddWithValue("@@colp@", _CanEmail);
            cmd.Parameters.AddWithValue("@@colp@", _CanIVR);
            cmd.Parameters.AddWithValue("@@colp@", _IsAdmin);
            cmd.Parameters.AddWithValue("@@colp@", _IsVisible);
            cmd.Parameters.AddWithValue("@@colp@", _DisableCommunications);
            cmd.Parameters.AddWithValue("@@colp@", _ReplyToAll);
            cmd.Parameters.AddWithValue("@@colp@", _UseContactData);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Preferred
        {
            get { return _IDPhone_Preferred; }
            set
            {
                _IDPhone_Preferred = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail_Preferred
        {
            get { return _IDEmail_Preferred; }
            set
            {
                _IDEmail_Preferred = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail_NotificationGateway
        {
            get { return _IDEmail_NotificationGateway; }
            set
            {
                _IDEmail_NotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_NotificationGateway
        {
            get { return _IDPhone_NotificationGateway; }
            set
            {
                _IDPhone_NotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDateTime InvitationSent
        {
            get { return _InvitationSent; }
            set
            {
                _InvitationSent = value;
                IsDirty = true;
            }
        }
        public SqlDateTime InvitationRequested
        {
            get { return _InvitationRequested; }
            set
            {
                _InvitationRequested = value;
                IsDirty = true;
            }
        }
        public SqlByte ApprovalStatus
        {
            get { return _ApprovalStatus; }
            set
            {
                _ApprovalStatus = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanSMS
        {
            get { return _CanSMS; }
            set
            {
                _CanSMS = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanEmail
        {
            get { return _CanEmail; }
            set
            {
                _CanEmail = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanIVR
        {
            get { return _CanIVR; }
            set
            {
                _CanIVR = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsAdmin
        {
            get { return _IsAdmin; }
            set
            {
                _IsAdmin = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsVisible
        {
            get { return _IsVisible; }
            set
            {
                _IsVisible = value;
                IsDirty = true;
            }
        }
        public SqlBoolean DisableCommunications
        {
            get { return _DisableCommunications; }
            set
            {
                _DisableCommunications = value;
                IsDirty = true;
            }
        }
        public SqlBoolean ReplyToAll
        {
            get { return _ReplyToAll; }
            set
            {
                _ReplyToAll = value;
                IsDirty = true;
            }
        }
        public SqlBoolean UseContactData
        {
            get { return _UseContactData; }
            set
            {
                _UseContactData = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_InterestAttribute : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDInterestAttribute;
        private SqlBoolean _IsManual;
        private SqlBoolean _IsExcluded;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDInterestAttribute; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_InterestAttribute() { }
        public AssocProfile_InterestAttribute(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_InterestAttribute(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDInterestAttribute = rd.SafeGetInt32("IDInterestAttribute");
            _IsManual = rd.SafeGetBoolean("IsManual");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDInterestAttribute", _IDInterestAttribute);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDInterestAttribute);
            cmd.Parameters.AddWithValue("@@colp@", _IsManual);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestAttribute
        {
            get { return _IDInterestAttribute; }
            set
            {
                _IDInterestAttribute = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManual
        {
            get { return _IsManual; }
            set
            {
                _IsManual = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Location : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDLocation;
        private SqlInt32 _TYLocation;
        private SqlInt64 _IDLocationStreet;
        private SqlInt64 _IDLocationStreet2;
        private SqlInt64 _IDLocationCity;
        private SqlInt64 _IDLocationState;
        private SqlInt64 _IDLocationCountry;
        private SqlInt64 _IDLocationZipCode;
        private SqlDecimal _SAQ_Street1;
        private SqlDecimal _SAQ_Street2;
        private SqlDecimal _SAQ_City;
        private SqlDecimal _SAQ_State;
        private SqlDecimal _SAQ_PostalCode;
        private SqlDecimal _SAQ_Country;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDLocation; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Location() { }
        public AssocProfile_Location(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Location(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDLocation = rd.SafeGetInt64("IDLocation");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            _SAQ_Street1 = rd.SafeGetDecimal("SAQ_Street1");
            _SAQ_Street2 = rd.SafeGetDecimal("SAQ_Street2");
            _SAQ_City = rd.SafeGetDecimal("SAQ_City");
            _SAQ_State = rd.SafeGetDecimal("SAQ_State");
            _SAQ_PostalCode = rd.SafeGetDecimal("SAQ_PostalCode");
            _SAQ_Country = rd.SafeGetDecimal("SAQ_Country");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDLocation);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationCity);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationState);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@@colp@", _IDLocationZipCode);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_Street1);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_Street2);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_City);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_State);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_PostalCode);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ_Country);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocation
        {
            get { return _IDLocation; }
            set
            {
                _IDLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_Street1
        {
            get { return _SAQ_Street1; }
            set
            {
                _SAQ_Street1 = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_Street2
        {
            get { return _SAQ_Street2; }
            set
            {
                _SAQ_Street2 = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_City
        {
            get { return _SAQ_City; }
            set
            {
                _SAQ_City = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_State
        {
            get { return _SAQ_State; }
            set
            {
                _SAQ_State = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_PostalCode
        {
            get { return _SAQ_PostalCode; }
            set
            {
                _SAQ_PostalCode = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ_Country
        {
            get { return _SAQ_Country; }
            set
            {
                _SAQ_Country = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Media : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDMedia;
        private SqlInt32 _AssocStrength;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMedia; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Media() { }
        public AssocProfile_Media(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Media(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _AssocStrength = rd.SafeGetInt32("AssocStrength");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMedia);
            cmd.Parameters.AddWithValue("@@colp@", _AssocStrength);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AssocStrength
        {
            get { return _AssocStrength; }
            set
            {
                _AssocStrength = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Media_Relationship : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDMedia;
        private SqlInt32 _TYRelationship;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMedia; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Media_Relationship() { }
        public AssocProfile_Media_Relationship(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Media_Relationship(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMedia);
            cmd.Parameters.AddWithValue("@@colp@", _TYRelationship);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Merchant : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDMerchant;
        private SqlInt32 _TYAccount;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMerchant; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Merchant() { }
        public AssocProfile_Merchant(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Merchant(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMerchant = rd.SafeGetInt64("IDMerchant");
            _TYAccount = rd.SafeGetInt32("TYAccount");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMerchant", _IDMerchant);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMerchant", _IDMerchant);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMerchant", _IDMerchant);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMerchant);
            cmd.Parameters.AddWithValue("@@colp@", _TYAccount);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMerchant
        {
            get { return _IDMerchant; }
            set
            {
                _IDMerchant = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYAccount
        {
            get { return _TYAccount; }
            set
            {
                _TYAccount = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_MetroArea : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDMetroArea;
        private SqlBoolean _IsManual;
        private SqlBoolean _IsExcluded;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDMetroArea; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_MetroArea() { }
        public AssocProfile_MetroArea(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_MetroArea(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            _IsManual = rd.SafeGetBoolean("IsManual");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMetroArea", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDMetroArea);
            cmd.Parameters.AddWithValue("@@colp@", _IsManual);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManual
        {
            get { return _IsManual; }
            set
            {
                _IsManual = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Note : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDNote;
        private SqlInt32 _TYRelationship;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDNote; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Note() { }
        public AssocProfile_Note(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Note(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDNote = rd.SafeGetInt64("IDNote");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDNote);
            cmd.Parameters.AddWithValue("@@colp@", _TYRelationship);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDNote
        {
            get { return _IDNote; }
            set
            {
                _IDNote = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Person : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDPerson;
        private SqlByte _NamePosition;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDPerson; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Person() { }
        public AssocProfile_Person(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Person(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDPerson = rd.SafeGetInt64("IDPerson");
            _NamePosition = rd.SafeGetByte("NamePosition");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
            cmd.Parameters.AddWithValue("@NamePosition", _NamePosition);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDPerson);
            cmd.Parameters.AddWithValue("@@colp@", _NamePosition);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPerson
        {
            get { return _IDPerson; }
            set
            {
                _IDPerson = value;
                IsDirty = true;
            }
        }
        public SqlByte NamePosition
        {
            get { return _NamePosition; }
            set
            {
                _NamePosition = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Phone : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDPhone;
        private SqlInt32 _TYLocation;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _TimeAdded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDPhone; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Phone() { }
        public AssocProfile_Phone(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Phone(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _TimeAdded = rd.SafeGetDateTime("TimeAdded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@TYLocation", _TYLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDPhone);
            cmd.Parameters.AddWithValue("@@colp@", _TYLocation);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _TimeAdded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime TimeAdded
        {
            get { return _TimeAdded; }
            set
            {
                _TimeAdded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Prefix : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _TYPrefix;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYPrefix; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Prefix() { }
        public AssocProfile_Prefix(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Prefix(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _TYPrefix = rd.SafeGetInt32("TYPrefix");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYPrefix", _TYPrefix);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYPrefix);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYPrefix
        {
            get { return _TYPrefix; }
            set
            {
                _TYPrefix = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Profile : CTable
    {
        private SqlInt64 _IDProfile_1;
        private SqlInt64 _IDProfile_2;
        private SqlInt32 _TYLinkSource;
        private SqlInt32 _IDLinkSource;
        private SqlByte _LinkStrength;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile_1 = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile_1; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile_2; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Profile() { }
        public AssocProfile_Profile(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Profile(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile_1 = rd.SafeGetInt64("IDProfile_1");
            _IDProfile_2 = rd.SafeGetInt64("IDProfile_2");
            _TYLinkSource = rd.SafeGetInt32("TYLinkSource");
            _IDLinkSource = rd.SafeGetInt32("IDLinkSource");
            _LinkStrength = rd.SafeGetByte("LinkStrength");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            cmd.Parameters.AddWithValue("@TYLinkSource", _TYLinkSource);
            cmd.Parameters.AddWithValue("@IDLinkSource", _IDLinkSource);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile_1);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile_2);
            cmd.Parameters.AddWithValue("@@colp@", _TYLinkSource);
            cmd.Parameters.AddWithValue("@@colp@", _IDLinkSource);
            cmd.Parameters.AddWithValue("@@colp@", _LinkStrength);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile_1
        {
            get { return _IDProfile_1; }
            set
            {
                _IDProfile_1 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_2
        {
            get { return _IDProfile_2; }
            set
            {
                _IDProfile_2 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYLinkSource
        {
            get { return _TYLinkSource; }
            set
            {
                _TYLinkSource = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDLinkSource
        {
            get { return _IDLinkSource; }
            set
            {
                _IDLinkSource = value;
                IsDirty = true;
            }
        }
        public SqlByte LinkStrength
        {
            get { return _LinkStrength; }
            set
            {
                _LinkStrength = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_Title : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _TYTitle;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _Updated;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYTitle; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_Title() { }
        public AssocProfile_Title(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_Title(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _TYTitle = rd.SafeGetInt32("TYTitle");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _Updated = rd.SafeGetDateTime("Updated");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYTitle", _TYTitle);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYTitle);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYTitle
        {
            get { return _TYTitle; }
            set
            {
                _TYTitle = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocProfile_WebAddress : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDWebAddress;
        private SqlInt32 _Confidence;
        private SqlBoolean _IsExcluded;
        private SqlDateTime _DateLastUpdate;
        private SqlDecimal _SAQ;
        private SqlInt32 _TYSource;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDWebAddress; }
        public override void SetValue(SqlString val) { }

        public AssocProfile_WebAddress() { }
        public AssocProfile_WebAddress(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocProfile_WebAddress(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDWebAddress = rd.SafeGetInt64("IDWebAddress");
            _Confidence = rd.SafeGetInt32("Confidence");
            _IsExcluded = rd.SafeGetBoolean("IsExcluded");
            _DateLastUpdate = rd.SafeGetDateTime("DateLastUpdate");
            _SAQ = rd.SafeGetDecimal("SAQ");
            _TYSource = rd.SafeGetInt32("TYSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDWebAddress);
            cmd.Parameters.AddWithValue("@@colp@", _Confidence);
            cmd.Parameters.AddWithValue("@@colp@", _IsExcluded);
            cmd.Parameters.AddWithValue("@@colp@", _DateLastUpdate);
            cmd.Parameters.AddWithValue("@@colp@", _SAQ);
            cmd.Parameters.AddWithValue("@@colp@", _TYSource);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDWebAddress
        {
            get { return _IDWebAddress; }
            set
            {
                _IDWebAddress = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Confidence
        {
            get { return _Confidence; }
            set
            {
                _Confidence = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsExcluded
        {
            get { return _IsExcluded; }
            set
            {
                _IsExcluded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateLastUpdate
        {
            get { return _DateLastUpdate; }
            set
            {
                _DateLastUpdate = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class AssocSubscriber_DataSource : CTable
    {
        private SqlInt64 _IDSubscriber;
        private SqlInt32 _IDDataSource;
        private SqlString _AccessString;  //100
        private SqlString _AccessKey;  //50
        public const int AccessStringMaxLen = 100;
        public const int AccessKeyMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDSubscriber = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDSubscriber; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDDataSource; }
        public override void SetValue(SqlString val) { }

        public AssocSubscriber_DataSource() { }
        public AssocSubscriber_DataSource(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public AssocSubscriber_DataSource(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDSubscriber = rd.SafeGetInt64("IDSubscriber");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _AccessString = rd.SafeGetString("AccessString");
            _AccessKey = rd.SafeGetString("AccessKey");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDSubscriber", _IDSubscriber);
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDSubscriber", _IDSubscriber);
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSubscriber);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDDataSource);
            cmd.Parameters.AddWithValue("@@colp@", _AccessString);
            cmd.Parameters.AddWithValue("@@colp@", _AccessKey);
        }

        #endregion
        #region accessors
        public SqlInt64 IDSubscriber
        {
            get { return _IDSubscriber; }
            set
            {
                _IDSubscriber = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlString AccessString
        {
            get { return _AccessString; }
            set
            {
                _AccessString = value;
                IsDirty = true;
            }
        }
        public SqlString AccessKey
        {
            get { return _AccessKey; }
            set
            {
                _AccessKey = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class CacheInboundRequest : CTable
    {
        private SqlInt32 _IPAddress;
        private SqlString _IPAddressString;  //20
        private SqlDateTime _Requested;
        private SqlByte _RequestType;
        private SqlString _Details;  //300
        public const int IPAddressStringMaxLen = 20;
        public const int DetailsMaxLen = 300;

        #region functions
        protected override void SetID(SqlInt64 id) { _IPAddress = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IPAddress; }
        public override void SetValue(SqlString val) { }

        public CacheInboundRequest() { }
        public CacheInboundRequest(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public CacheInboundRequest(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IPAddress = rd.SafeGetInt32("IPAddress");
            _IPAddressString = rd.SafeGetString("IPAddressString");
            _Requested = rd.SafeGetDateTime("Requested");
            _RequestType = rd.SafeGetByte("RequestType");
            _Details = rd.SafeGetString("Details");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IPAddress);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IPAddress);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IPAddress);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IPAddressString", _IPAddressString);
            cmd.Parameters.AddWithValue("@Requested", _Requested);
            cmd.Parameters.AddWithValue("@RequestType", _RequestType);
            cmd.Parameters.AddWithValue("@Details", _Details);
        }

        #endregion
        #region accessors
        public SqlInt32 IPAddress
        {
            get { return _IPAddress; }
            set
            {
                _IPAddress = value;
                IsDirty = true;
            }
        }
        public SqlString IPAddressString
        {
            get { return _IPAddressString; }
            set
            {
                _IPAddressString = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Requested
        {
            get { return _Requested; }
            set
            {
                _Requested = value;
                IsDirty = true;
            }
        }
        public SqlByte RequestType
        {
            get { return _RequestType; }
            set
            {
                _RequestType = value;
                IsDirty = true;
            }
        }
        public SqlString Details
        {
            get { return _Details; }
            set
            {
                _Details = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class CacheProfileGroupDevice : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDGroup;
        private SqlInt64 _IDDevice;
        private SqlString _PIMContactUID;  //20
        private SqlString _ProfileNameAndMediaXML;  //1000
        private SqlDateTime _Updated;
        public const int PIMContactUIDMaxLen = 20;
        public const int ProfileNameAndMediaXMLMaxLen = 1000;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public CacheProfileGroupDevice() { }
        public CacheProfileGroupDevice(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public CacheProfileGroupDevice(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _PIMContactUID = rd.SafeGetString("PIMContactUID");
            _ProfileNameAndMediaXML = rd.SafeGetString("ProfileNameAndMediaXML");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@PIMContactUID", _PIMContactUID);
            cmd.Parameters.AddWithValue("@ProfileNameAndMediaXML", _ProfileNameAndMediaXML);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlString PIMContactUID
        {
            get { return _PIMContactUID; }
            set
            {
                _PIMContactUID = value;
                IsDirty = true;
            }
        }
        public SqlString ProfileNameAndMediaXML
        {
            get { return _ProfileNameAndMediaXML; }
            set
            {
                _ProfileNameAndMediaXML = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class FltDevice_DataInput : CTable
    {
        private SqlInt32 _TYDataInput;
        private SqlInt64 _DeviceDataInputMasterFilterID;
        private SqlString _FieldDisplayName;  //50
        private SqlString _TableName;  //50
        private SqlInt32 _PositionIndex;
        private SqlBoolean _IsLocked;
        public const int FieldDisplayNameMaxLen = 50;
        public const int TableNameMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYDataInput = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYDataInput; }
        public override void SetValue(SqlString val) { }

        public FltDevice_DataInput() { }
        public FltDevice_DataInput(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public FltDevice_DataInput(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYDataInput = rd.SafeGetInt32("TYDataInput");
            _DeviceDataInputMasterFilterID = rd.SafeGetInt64("DeviceDataInputMasterFilterID");
            _FieldDisplayName = rd.SafeGetString("FieldDisplayName");
            _TableName = rd.SafeGetString("TableName");
            _PositionIndex = rd.SafeGetInt32("PositionIndex");
            _IsLocked = rd.SafeGetBoolean("IsLocked");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DeviceDataInputMasterFilterID", _DeviceDataInputMasterFilterID);
            cmd.Parameters.AddWithValue("@FieldDisplayName", _FieldDisplayName);
            cmd.Parameters.AddWithValue("@TableName", _TableName);
            cmd.Parameters.AddWithValue("@PositionIndex", _PositionIndex);
            cmd.Parameters.AddWithValue("@IsLocked", _IsLocked);
        }

        #endregion
        #region accessors
        public SqlInt32 TYDataInput
        {
            get { return _TYDataInput; }
            set
            {
                _TYDataInput = value;
                IsDirty = true;
            }
        }
        public SqlInt64 DeviceDataInputMasterFilterID
        {
            get { return _DeviceDataInputMasterFilterID; }
            set
            {
                _DeviceDataInputMasterFilterID = value;
                IsDirty = true;
            }
        }
        public SqlString FieldDisplayName
        {
            get { return _FieldDisplayName; }
            set
            {
                _FieldDisplayName = value;
                IsDirty = true;
            }
        }
        public SqlString TableName
        {
            get { return _TableName; }
            set
            {
                _TableName = value;
                IsDirty = true;
            }
        }
        public SqlInt32 PositionIndex
        {
            get { return _PositionIndex; }
            set
            {
                _PositionIndex = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class FltProfile_DataAccess : CTable
    {
        private SqlInt64 _IDProfileDataAccessMasterFilter; //IDENTITY 
        private SqlInt64 _IDProfileDataAccess;
        private SqlString _FieldDisplayName;  //20
        private SqlString _TableName;  //50
        private SqlInt32 _PositionIndex;
        private SqlBoolean _IsShared;
        private SqlInt32 _TYRelationship;
        public const int FieldDisplayNameMaxLen = 20;
        public const int TableNameMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfileDataAccessMasterFilter = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfileDataAccessMasterFilter; }
        public override void SetValue(SqlString val) { }

        public FltProfile_DataAccess() { }
        public FltProfile_DataAccess(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public FltProfile_DataAccess(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfileDataAccessMasterFilter = rd.SafeGetInt64("IDProfileDataAccessMasterFilter");
            _IDProfileDataAccess = rd.SafeGetInt64("IDProfileDataAccess");
            _FieldDisplayName = rd.SafeGetString("FieldDisplayName");
            _TableName = rd.SafeGetString("TableName");
            _PositionIndex = rd.SafeGetInt32("PositionIndex");
            _IsShared = rd.SafeGetBoolean("IsShared");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataAccessMasterFilter);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataAccessMasterFilter);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataAccessMasterFilter);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfileDataAccess", _IDProfileDataAccess);
            cmd.Parameters.AddWithValue("@FieldDisplayName", _FieldDisplayName);
            cmd.Parameters.AddWithValue("@TableName", _TableName);
            cmd.Parameters.AddWithValue("@PositionIndex", _PositionIndex);
            cmd.Parameters.AddWithValue("@IsShared", _IsShared);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfileDataAccessMasterFilter
        {
            get { return _IDProfileDataAccessMasterFilter; }
            private set
            {
                _IDProfileDataAccessMasterFilter = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfileDataAccess
        {
            get { return _IDProfileDataAccess; }
            set
            {
                _IDProfileDataAccess = value;
                IsDirty = true;
            }
        }
        public SqlString FieldDisplayName
        {
            get { return _FieldDisplayName; }
            set
            {
                _FieldDisplayName = value;
                IsDirty = true;
            }
        }
        public SqlString TableName
        {
            get { return _TableName; }
            set
            {
                _TableName = value;
                IsDirty = true;
            }
        }
        public SqlInt32 PositionIndex
        {
            get { return _PositionIndex; }
            set
            {
                _PositionIndex = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsShared
        {
            get { return _IsShared; }
            set
            {
                _IsShared = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class FltProfile_DataManagement : CTable
    {
        private SqlInt32 _IDProfileDataManagementMasterFilter; //IDENTITY 
        private SqlInt32 _TYDataManagement;
        private SqlInt32 _TYRelationship;
        private SqlString _FieldDisplayName;  //40
        private SqlString _TableName;  //50
        private SqlInt32 _PositionIndex;
        private SqlByte _IsLocked;
        public const int FieldDisplayNameMaxLen = 40;
        public const int TableNameMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfileDataManagementMasterFilter = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfileDataManagementMasterFilter; }
        public override void SetValue(SqlString val) { }

        public FltProfile_DataManagement() { }
        public FltProfile_DataManagement(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public FltProfile_DataManagement(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfileDataManagementMasterFilter = rd.SafeGetInt32("IDProfileDataManagementMasterFilter");
            _TYDataManagement = rd.SafeGetInt32("TYDataManagement");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _FieldDisplayName = rd.SafeGetString("FieldDisplayName");
            _TableName = rd.SafeGetString("TableName");
            _PositionIndex = rd.SafeGetInt32("PositionIndex");
            _IsLocked = rd.SafeGetByte("IsLocked");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataManagementMasterFilter);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataManagementMasterFilter);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfileDataManagementMasterFilter);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYDataManagement", _TYDataManagement);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            cmd.Parameters.AddWithValue("@FieldDisplayName", _FieldDisplayName);
            cmd.Parameters.AddWithValue("@TableName", _TableName);
            cmd.Parameters.AddWithValue("@PositionIndex", _PositionIndex);
            cmd.Parameters.AddWithValue("@IsLocked", _IsLocked);
        }

        #endregion
        #region accessors
        public SqlInt32 IDProfileDataManagementMasterFilter
        {
            get { return _IDProfileDataManagementMasterFilter; }
            private set
            {
                _IDProfileDataManagementMasterFilter = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYDataManagement
        {
            get { return _TYDataManagement; }
            set
            {
                _TYDataManagement = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlString FieldDisplayName
        {
            get { return _FieldDisplayName; }
            set
            {
                _FieldDisplayName = value;
                IsDirty = true;
            }
        }
        public SqlString TableName
        {
            get { return _TableName; }
            set
            {
                _TableName = value;
                IsDirty = true;
            }
        }
        public SqlInt32 PositionIndex
        {
            get { return _PositionIndex; }
            set
            {
                _PositionIndex = value;
                IsDirty = true;
            }
        }
        public SqlByte IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class HistDeviceToken : CTable
    {
        private SqlInt64 _IDDevice;
        private SqlString _DeviceToken;  //40
        private SqlDateTime _Added;
        public const int DeviceTokenMaxLen = 40;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDevice = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDevice; }
        public override void SetValue(SqlString val) { }

        public HistDeviceToken() { }
        public HistDeviceToken(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public HistDeviceToken(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _DeviceToken = rd.SafeGetString("DeviceToken");
            _Added = rd.SafeGetDateTime("Added");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DeviceToken", _DeviceToken);
            cmd.Parameters.AddWithValue("@Added", _Added);
        }

        #endregion
        #region accessors
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlString DeviceToken
        {
            get { return _DeviceToken; }
            set
            {
                _DeviceToken = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Added
        {
            get { return _Added; }
            set
            {
                _Added = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class HistSearch : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDDataSource;
        private SqlDateTime _LastSearch;
        private SqlString _ImageURL;  //100
        private SqlString _SourceUID;  //50
        private SqlInt32 _Strength;
        private SqlInt64 _ScraperSystemID;
        public const int ImageURLMaxLen = 100;
        public const int SourceUIDMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public HistSearch() { }
        public HistSearch(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public HistSearch(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _LastSearch = rd.SafeGetDateTime("LastSearch");
            _ImageURL = rd.SafeGetString("ImageURL");
            _SourceUID = rd.SafeGetString("SourceUID");
            _Strength = rd.SafeGetInt32("Strength");
            _ScraperSystemID = rd.SafeGetInt64("ScraperSystemID");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            cmd.Parameters.AddWithValue("@LastSearch", _LastSearch);
            cmd.Parameters.AddWithValue("@ImageURL", _ImageURL);
            cmd.Parameters.AddWithValue("@SourceUID", _SourceUID);
            cmd.Parameters.AddWithValue("@Strength", _Strength);
            cmd.Parameters.AddWithValue("@ScraperSystemID", _ScraperSystemID);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlDateTime LastSearch
        {
            get { return _LastSearch; }
            set
            {
                _LastSearch = value;
                IsDirty = true;
            }
        }
        public SqlString ImageURL
        {
            get { return _ImageURL; }
            set
            {
                _ImageURL = value;
                IsDirty = true;
            }
        }
        public SqlString SourceUID
        {
            get { return _SourceUID; }
            set
            {
                _SourceUID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Strength
        {
            get { return _Strength; }
            set
            {
                _Strength = value;
                IsDirty = true;
            }
        }
        public SqlInt64 ScraperSystemID
        {
            get { return _ScraperSystemID; }
            set
            {
                _ScraperSystemID = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class HrvHarvesterResultQueue : CTable
    {
        private SqlInt32 _IDResult; //IDENTITY 
        private SqlInt32 _IDDataSource;
        private SqlInt32 _ResultTypeID;
        private SqlInt32 _FrameID;
        private SqlDateTime _ResultDate;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDResult = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDResult; }
        public override void SetValue(SqlString val) { }

        public HrvHarvesterResultQueue() { }
        public HrvHarvesterResultQueue(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public HrvHarvesterResultQueue(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDResult = rd.SafeGetInt32("IDResult");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _ResultTypeID = rd.SafeGetInt32("ResultTypeID");
            _FrameID = rd.SafeGetInt32("FrameID");
            _ResultDate = rd.SafeGetDateTime("ResultDate");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDResult);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDResult);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDResult);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            cmd.Parameters.AddWithValue("@ResultTypeID", _ResultTypeID);
            cmd.Parameters.AddWithValue("@FrameID", _FrameID);
            cmd.Parameters.AddWithValue("@ResultDate", _ResultDate);
        }

        #endregion
        #region accessors
        public SqlInt32 IDResult
        {
            get { return _IDResult; }
            private set
            {
                _IDResult = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlInt32 ResultTypeID
        {
            get { return _ResultTypeID; }
            set
            {
                _ResultTypeID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 FrameID
        {
            get { return _FrameID; }
            set
            {
                _FrameID = value;
                IsDirty = true;
            }
        }
        public SqlDateTime ResultDate
        {
            get { return _ResultDate; }
            set
            {
                _ResultDate = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class HrvHarvesterSystem : CTable
    {
        private SqlInt64 _IDHarvesterSystem; //IDENTITY 
        private SqlString _HarvesterUrl;  //50
        private SqlString _HarvesterCertificate;  //200
        private SqlBoolean _CanInsert;
        private SqlBoolean _GetResult;
        private SqlString _DataGatewayKey;  //50
        public const int HarvesterUrlMaxLen = 50;
        public const int HarvesterCertificateMaxLen = 200;
        public const int DataGatewayKeyMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDHarvesterSystem = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDHarvesterSystem; }
        public override void SetValue(SqlString val) { }

        public HrvHarvesterSystem() { }
        public HrvHarvesterSystem(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public HrvHarvesterSystem(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDHarvesterSystem = rd.SafeGetInt64("IDHarvesterSystem");
            _HarvesterUrl = rd.SafeGetString("HarvesterUrl");
            _HarvesterCertificate = rd.SafeGetString("HarvesterCertificate");
            _CanInsert = rd.SafeGetBoolean("CanInsert");
            _GetResult = rd.SafeGetBoolean("GetResult");
            _DataGatewayKey = rd.SafeGetString("DataGatewayKey");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterSystem);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterSystem);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterSystem);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@HarvesterUrl", _HarvesterUrl);
            cmd.Parameters.AddWithValue("@HarvesterCertificate", _HarvesterCertificate);
            cmd.Parameters.AddWithValue("@CanInsert", _CanInsert);
            cmd.Parameters.AddWithValue("@GetResult", _GetResult);
            cmd.Parameters.AddWithValue("@DataGatewayKey", _DataGatewayKey);
        }

        #endregion
        #region accessors
        public SqlInt64 IDHarvesterSystem
        {
            get { return _IDHarvesterSystem; }
            private set
            {
                _IDHarvesterSystem = value;
                IsDirty = true;
            }
        }
        public SqlString HarvesterUrl
        {
            get { return _HarvesterUrl; }
            set
            {
                _HarvesterUrl = value;
                IsDirty = true;
            }
        }
        public SqlString HarvesterCertificate
        {
            get { return _HarvesterCertificate; }
            set
            {
                _HarvesterCertificate = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanInsert
        {
            get { return _CanInsert; }
            set
            {
                _CanInsert = value;
                IsDirty = true;
            }
        }
        public SqlBoolean GetResult
        {
            get { return _GetResult; }
            set
            {
                _GetResult = value;
                IsDirty = true;
            }
        }
        public SqlString DataGatewayKey
        {
            get { return _DataGatewayKey; }
            set
            {
                _DataGatewayKey = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class HrvlHarvesterAction : CTable
    {
        private SqlInt32 _IDHarvesterActionType;
        private SqlDateTime _DateAction;
        private SqlString _HarvesterAction;  //100
        public const int HarvesterActionMaxLen = 100;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDHarvesterActionType = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDHarvesterActionType; }
        public override void SetValue(SqlString val) { }

        public HrvlHarvesterAction() { }
        public HrvlHarvesterAction(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public HrvlHarvesterAction(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDHarvesterActionType = rd.SafeGetInt32("IDHarvesterActionType");
            _DateAction = rd.SafeGetDateTime("DateAction");
            _HarvesterAction = rd.SafeGetString("HarvesterAction");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterActionType);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterActionType);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDHarvesterActionType);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DateAction", _DateAction);
            cmd.Parameters.AddWithValue("@HarvesterAction", _HarvesterAction);
        }

        #endregion
        #region accessors
        public SqlInt32 IDHarvesterActionType
        {
            get { return _IDHarvesterActionType; }
            set
            {
                _IDHarvesterActionType = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateAction
        {
            get { return _DateAction; }
            set
            {
                _DateAction = value;
                IsDirty = true;
            }
        }
        public SqlString HarvesterAction
        {
            get { return _HarvesterAction; }
            set
            {
                _HarvesterAction = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkGroup_Profile_Admin : CTable
    {
        private SqlInt64 _IDGroup;
        private SqlInt64 _IDProfile;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public LnkGroup_Profile_Admin() { }
        public LnkGroup_Profile_Admin(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkGroup_Profile_Admin(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkOffer_Group : CTable
    {
        private SqlInt64 _IDOffer;
        private SqlInt64 _IDGroup;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDOffer = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDOffer; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { }

        public LnkOffer_Group() { }
        public LnkOffer_Group(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkOffer_Group(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup);
        }

        #endregion
        #region accessors
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkProfile_Contact : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDContact;
        private SqlInt32 _TYRelationship;
        private SqlBoolean _IsManualRelationship;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDContact; }
        public override void SetValue(SqlString val) { }

        public LnkProfile_Contact() { }
        public LnkProfile_Contact(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkProfile_Contact(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDContact = rd.SafeGetInt64("IDContact");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _IsManualRelationship = rd.SafeGetBoolean("IsManualRelationship");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDContact);
            cmd.Parameters.AddWithValue("@@colp@", _TYRelationship);
            cmd.Parameters.AddWithValue("@@colp@", _IsManualRelationship);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManualRelationship
        {
            get { return _IsManualRelationship; }
            set
            {
                _IsManualRelationship = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkProfile_DataSource_PrioritySearch : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDDataSource;
        private SqlBoolean _CanSearch;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDDataSource; }
        public override void SetValue(SqlString val) { }

        public LnkProfile_DataSource_PrioritySearch() { }
        public LnkProfile_DataSource_PrioritySearch(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkProfile_DataSource_PrioritySearch(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _CanSearch = rd.SafeGetBoolean("CanSearch");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDDataSource);
            cmd.Parameters.AddWithValue("@@colp@", _CanSearch);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanSearch
        {
            get { return _CanSearch; }
            set
            {
                _CanSearch = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkProfile_Group_Admin : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDGroup;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { }

        public LnkProfile_Group_Admin() { }
        public LnkProfile_Group_Admin(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkProfile_Group_Admin(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDGroup);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkProfile_Media_Hash : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _TYMedia;
        private SqlString _MD5Hash;  //50
        public const int MD5HashMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_TYMedia; }
        public override void SetValue(SqlString val) { }

        public LnkProfile_Media_Hash() { }
        public LnkProfile_Media_Hash(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkProfile_Media_Hash(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _TYMedia = rd.SafeGetInt32("TYMedia");
            _MD5Hash = rd.SafeGetString("MD5Hash");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _TYMedia);
            cmd.Parameters.AddWithValue("@@colp@", _MD5Hash);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYMedia
        {
            get { return _TYMedia; }
            set
            {
                _TYMedia = value;
                IsDirty = true;
            }
        }
        public SqlString MD5Hash
        {
            get { return _MD5Hash; }
            set
            {
                _MD5Hash = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class LnkProfile_Profile_MergeReview : CTable
    {
        private SqlInt64 _IDProfile_1;
        private SqlInt64 _IDProfile_2;
        private SqlByte _ReviewStatus;
        private SqlDateTime _Added;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile_1 = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile_1; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile_2; }
        public override void SetValue(SqlString val) { }

        public LnkProfile_Profile_MergeReview() { }
        public LnkProfile_Profile_MergeReview(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public LnkProfile_Profile_MergeReview(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile_1 = rd.SafeGetInt64("IDProfile_1");
            _IDProfile_2 = rd.SafeGetInt64("IDProfile_2");
            _ReviewStatus = rd.SafeGetByte("ReviewStatus");
            _Added = rd.SafeGetDateTime("Added");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile_1);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile_2);
            cmd.Parameters.AddWithValue("@@colp@", _ReviewStatus);
            cmd.Parameters.AddWithValue("@@colp@", _Added);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile_1
        {
            get { return _IDProfile_1; }
            set
            {
                _IDProfile_1 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_2
        {
            get { return _IDProfile_2; }
            set
            {
                _IDProfile_2 = value;
                IsDirty = true;
            }
        }
        public SqlByte ReviewStatus
        {
            get { return _ReviewStatus; }
            set
            {
                _ReviewStatus = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Added
        {
            get { return _Added; }
            set
            {
                _Added = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class OvrDevice_DataInput : CTable
    {
        private SqlInt64 _IDDevice;
        private SqlInt64 _DeviceDataInputMasterFilterID;
        private SqlBoolean _IsLocked;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDevice = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDevice; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_DeviceDataInputMasterFilterID; }
        public override void SetValue(SqlString val) { }

        public OvrDevice_DataInput() { }
        public OvrDevice_DataInput(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public OvrDevice_DataInput(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _DeviceDataInputMasterFilterID = rd.SafeGetInt64("DeviceDataInputMasterFilterID");
            _IsLocked = rd.SafeGetBoolean("IsLocked");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@DeviceDataInputMasterFilterID", _DeviceDataInputMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@DeviceDataInputMasterFilterID", _DeviceDataInputMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DeviceDataInputMasterFilterID", _DeviceDataInputMasterFilterID);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _DeviceDataInputMasterFilterID);
            cmd.Parameters.AddWithValue("@@colp@", _IsLocked);
        }

        #endregion
        #region accessors
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlInt64 DeviceDataInputMasterFilterID
        {
            get { return _DeviceDataInputMasterFilterID; }
            set
            {
                _DeviceDataInputMasterFilterID = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class OvrProfile_DataAccess : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _ProfileDataAccessMasterFilterID;
        private SqlBoolean _IsShared;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_ProfileDataAccessMasterFilterID; }
        public override void SetValue(SqlString val) { }

        public OvrProfile_DataAccess() { }
        public OvrProfile_DataAccess(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public OvrProfile_DataAccess(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _ProfileDataAccessMasterFilterID = rd.SafeGetInt64("ProfileDataAccessMasterFilterID");
            _IsShared = rd.SafeGetBoolean("IsShared");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@ProfileDataAccessMasterFilterID", _ProfileDataAccessMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@ProfileDataAccessMasterFilterID", _ProfileDataAccessMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ProfileDataAccessMasterFilterID", _ProfileDataAccessMasterFilterID);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _ProfileDataAccessMasterFilterID);
            cmd.Parameters.AddWithValue("@@colp@", _IsShared);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 ProfileDataAccessMasterFilterID
        {
            get { return _ProfileDataAccessMasterFilterID; }
            set
            {
                _ProfileDataAccessMasterFilterID = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsShared
        {
            get { return _IsShared; }
            set
            {
                _IsShared = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class OvrProfile_DataManagement : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt64 _ProfileDataManagementMasterFilterID;
        private SqlBoolean _IsLocked;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_ProfileDataManagementMasterFilterID; }
        public override void SetValue(SqlString val) { }

        public OvrProfile_DataManagement() { }
        public OvrProfile_DataManagement(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public OvrProfile_DataManagement(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _ProfileDataManagementMasterFilterID = rd.SafeGetInt64("ProfileDataManagementMasterFilterID");
            _IsLocked = rd.SafeGetBoolean("IsLocked");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@ProfileDataManagementMasterFilterID", _ProfileDataManagementMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@ProfileDataManagementMasterFilterID", _ProfileDataManagementMasterFilterID);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ProfileDataManagementMasterFilterID", _ProfileDataManagementMasterFilterID);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _ProfileDataManagementMasterFilterID);
            cmd.Parameters.AddWithValue("@@colp@", _IsLocked);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 ProfileDataManagementMasterFilterID
        {
            get { return _ProfileDataManagementMasterFilterID; }
            set
            {
                _ProfileDataManagementMasterFilterID = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsLocked
        {
            get { return _IsLocked; }
            set
            {
                _IsLocked = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class OvrProfile_Profile_RelationshipType : CTable
    {
        private SqlInt64 _IDProfile_Owner;
        private SqlInt64 _IDProfile_Requester;
        private SqlInt32 _TYRelationship;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile_Owner = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile_Owner; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile_Requester; }
        public override void SetValue(SqlString val) { }

        public OvrProfile_Profile_RelationshipType() { }
        public OvrProfile_Profile_RelationshipType(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public OvrProfile_Profile_RelationshipType(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile_Owner = rd.SafeGetInt64("IDProfile_Owner");
            _IDProfile_Requester = rd.SafeGetInt64("IDProfile_Requester");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_Owner", _IDProfile_Owner);
            cmd.Parameters.AddWithValue("@IDProfile_Requester", _IDProfile_Requester);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_Owner", _IDProfile_Owner);
            cmd.Parameters.AddWithValue("@IDProfile_Requester", _IDProfile_Requester);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_Requester", _IDProfile_Requester);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile_Owner);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile_Requester);
            cmd.Parameters.AddWithValue("@@colp@", _TYRelationship);
            cmd.Parameters.AddWithValue("@@colp@", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile_Owner
        {
            get { return _IDProfile_Owner; }
            set
            {
                _IDProfile_Owner = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_Requester
        {
            get { return _IDProfile_Requester; }
            set
            {
                _IDProfile_Requester = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class ReviewProfile_Application : CTable
    {
        private SqlInt64 _IDApplication;
        private SqlInt64 _IDProfile;
        private SqlByte _CanProcess;
        private SqlInt32 _RejectCount;
        private SqlByte _STNotification;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDApplication = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDApplication; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public ReviewProfile_Application() { }
        public ReviewProfile_Application(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public ReviewProfile_Application(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDApplication = rd.SafeGetInt64("IDApplication");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _CanProcess = rd.SafeGetByte("CanProcess");
            _RejectCount = rd.SafeGetInt32("RejectCount");
            _STNotification = rd.SafeGetByte("STNotification");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDApplication);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile);
            cmd.Parameters.AddWithValue("@@colp@", _CanProcess);
            cmd.Parameters.AddWithValue("@@colp@", _RejectCount);
            cmd.Parameters.AddWithValue("@@colp@", _STNotification);
        }

        #endregion
        #region accessors
        public SqlInt64 IDApplication
        {
            get { return _IDApplication; }
            set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlByte CanProcess
        {
            get { return _CanProcess; }
            set
            {
                _CanProcess = value;
                IsDirty = true;
            }
        }
        public SqlInt32 RejectCount
        {
            get { return _RejectCount; }
            set
            {
                _RejectCount = value;
                IsDirty = true;
            }
        }
        public SqlByte STNotification
        {
            get { return _STNotification; }
            set
            {
                _STNotification = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class ReviewProfile_Profile : CTable
    {
        private SqlInt64 _IDProfile_1;
        private SqlInt64 _IDProfile_2;
        private SqlByte _STReview;
        private SqlDateTime _Added;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile_1 = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile_1; }
        public SqlInt64 GetAssociatedID() { return (SqlInt64)_IDProfile_2; }
        public override void SetValue(SqlString val) { }

        public ReviewProfile_Profile() { }
        public ReviewProfile_Profile(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public ReviewProfile_Profile(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile_1 = rd.SafeGetInt64("IDProfile_1");
            _IDProfile_2 = rd.SafeGetInt64("IDProfile_2");
            _STReview = rd.SafeGetByte("STReview");
            _Added = rd.SafeGetDateTime("Added");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_1", _IDProfile_1);
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile_1);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@@colp@", _IDProfile_2);
            cmd.Parameters.AddWithValue("@@colp@", _STReview);
            cmd.Parameters.AddWithValue("@@colp@", _Added);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile_1
        {
            get { return _IDProfile_1; }
            set
            {
                _IDProfile_1 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_2
        {
            get { return _IDProfile_2; }
            set
            {
                _IDProfile_2 = value;
                IsDirty = true;
            }
        }
        public SqlByte STReview
        {
            get { return _STReview; }
            set
            {
                _STReview = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Added
        {
            get { return _Added; }
            set
            {
                _Added = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class StatusFieldUpdates : CTable
    {
        private SqlInt32 _STFieldUpdate; //IDENTITY 
        private SqlString _FieldUpdateStatus;  //50
        public const int FieldUpdateStatusMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _STFieldUpdate = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_STFieldUpdate; }
        public override void SetValue(SqlString val) { }

        public StatusFieldUpdates() { }
        public StatusFieldUpdates(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public StatusFieldUpdates(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _STFieldUpdate = rd.SafeGetInt32("STFieldUpdate");
            _FieldUpdateStatus = rd.SafeGetString("FieldUpdateStatus");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STFieldUpdate);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STFieldUpdate);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STFieldUpdate);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@FieldUpdateStatus", _FieldUpdateStatus);
        }

        #endregion
        #region accessors
        public SqlInt32 STFieldUpdate
        {
            get { return _STFieldUpdate; }
            private set
            {
                _STFieldUpdate = value;
                IsDirty = true;
            }
        }
        public SqlString FieldUpdateStatus
        {
            get { return _FieldUpdateStatus; }
            set
            {
                _FieldUpdateStatus = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class StatusSubscribers : CTable
    {
        private SqlInt32 _STSubscriber; //IDENTITY 
        private SqlString _SubscriberStatus;  //20
        public const int SubscriberStatusMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _STSubscriber = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_STSubscriber; }
        public override void SetValue(SqlString val) { }

        public StatusSubscribers() { }
        public StatusSubscribers(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public StatusSubscribers(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _STSubscriber = rd.SafeGetInt32("STSubscriber");
            _SubscriberStatus = rd.SafeGetString("SubscriberStatus");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STSubscriber);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STSubscriber);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _STSubscriber);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@SubscriberStatus", _SubscriberStatus);
        }

        #endregion
        #region accessors
        public SqlInt32 STSubscriber
        {
            get { return _STSubscriber; }
            private set
            {
                _STSubscriber = value;
                IsDirty = true;
            }
        }
        public SqlString SubscriberStatus
        {
            get { return _SubscriberStatus; }
            set
            {
                _SubscriberStatus = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblAccessKey : CTable
    {
        private SqlInt64 _IDAccessKey; //IDENTITY 
        private SqlString _AccessKey;  //20
        private SqlInt64 _IDProfile;
        private SqlInt32 _AccessLimit;
        private SqlString _Notes;  //500
        private SqlDateTime _DateAccess;
        private SqlString _IPAddress;  //50
        public const int AccessKeyMaxLen = 20;
        public const int NotesMaxLen = 500;
        public const int IPAddressMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDAccessKey = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDAccessKey; }
        public override void SetValue(SqlString val) { _AccessKey = val; }

        public TblAccessKey() { }
        public TblAccessKey(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblAccessKey(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDAccessKey = rd.SafeGetInt64("IDAccessKey");
            _AccessKey = rd.SafeGetString("AccessKey");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _AccessLimit = rd.SafeGetInt32("AccessLimit");
            _Notes = rd.SafeGetString("Notes");
            _DateAccess = rd.SafeGetDateTime("DateAccess");
            _IPAddress = rd.SafeGetString("IPAddress");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAccessKey);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAccessKey);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAccessKey);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@AccessKey", _AccessKey);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@AccessLimit", _AccessLimit);
            cmd.Parameters.AddWithValue("@Notes", _Notes);
            cmd.Parameters.AddWithValue("@DateAccess", _DateAccess);
            cmd.Parameters.AddWithValue("@IPAddress", _IPAddress);
        }

        #endregion
        #region accessors
        public SqlInt64 IDAccessKey
        {
            get { return _IDAccessKey; }
            private set
            {
                _IDAccessKey = value;
                IsDirty = true;
            }
        }
        public SqlString AccessKey
        {
            get { return _AccessKey; }
            set
            {
                _AccessKey = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AccessLimit
        {
            get { return _AccessLimit; }
            set
            {
                _AccessLimit = value;
                IsDirty = true;
            }
        }
        public SqlString Notes
        {
            get { return _Notes; }
            set
            {
                _Notes = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateAccess
        {
            get { return _DateAccess; }
            set
            {
                _DateAccess = value;
                IsDirty = true;
            }
        }
        public SqlString IPAddress
        {
            get { return _IPAddress; }
            set
            {
                _IPAddress = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblApplication : CTable
    {
        private SqlInt32 _IDApplication; //IDENTITY 
        private SqlString _Application;  //50
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDMedia_Header;
        private SqlInt64 _IDMedia_AppCon;
        private SqlString _Promotion;  //250
        private SqlString _Registration;  //100
        private SqlString _Login;  //100
        private SqlString _LoginTitle;  //50
        private SqlString _BgColor;  //6
        private SqlString _FgColor;  //6
        private SqlString _iTunesURL;  //250
        private SqlBoolean _IsJoinable;
        public const int ApplicationMaxLen = 50;
        public const int PromotionMaxLen = 250;
        public const int RegistrationMaxLen = 100;
        public const int LoginMaxLen = 100;
        public const int LoginTitleMaxLen = 50;
        public const int BgColorMaxLen = 6;
        public const int FgColorMaxLen = 6;
        public const int iTunesURLMaxLen = 250;
        private AssocApplication_NewsChannelAssoc _AssocApplication_NewsChannel;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDApplication = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDApplication; }
        public override void SetValue(SqlString val) { _Application = val; }

        public TblApplication() { }
        public TblApplication(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblApplication(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocApplication_NewsChannel = new AssocApplication_NewsChannelAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocApplication_NewsChannel, _AssocApplication_NewsChannel);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDApplication = rd.SafeGetInt32("IDApplication");
            _Application = rd.SafeGetString("Application");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDMedia_Header = rd.SafeGetInt64("IDMedia_Header");
            _IDMedia_AppCon = rd.SafeGetInt64("IDMedia_AppCon");
            _Promotion = rd.SafeGetString("Promotion");
            _Registration = rd.SafeGetString("Registration");
            _Login = rd.SafeGetString("Login");
            _LoginTitle = rd.SafeGetString("LoginTitle");
            _BgColor = rd.SafeGetString("BgColor");
            _FgColor = rd.SafeGetString("FgColor");
            _iTunesURL = rd.SafeGetString("iTunesURL");
            _IsJoinable = rd.SafeGetBoolean("IsJoinable");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDApplication);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDApplication);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Application", _Application);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDMedia_Header", _IDMedia_Header);
            cmd.Parameters.AddWithValue("@IDMedia_AppCon", _IDMedia_AppCon);
            cmd.Parameters.AddWithValue("@Promotion", _Promotion);
            cmd.Parameters.AddWithValue("@Registration", _Registration);
            cmd.Parameters.AddWithValue("@Login", _Login);
            cmd.Parameters.AddWithValue("@LoginTitle", _LoginTitle);
            cmd.Parameters.AddWithValue("@BgColor", _BgColor);
            cmd.Parameters.AddWithValue("@FgColor", _FgColor);
            cmd.Parameters.AddWithValue("@iTunesURL", _iTunesURL);
            cmd.Parameters.AddWithValue("@IsJoinable", _IsJoinable);
        }

        #endregion
        #region accessors
        public SqlInt32 IDApplication
        {
            get { return _IDApplication; }
            private set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlString Application
        {
            get { return _Application; }
            set
            {
                _Application = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia_Header
        {
            get { return _IDMedia_Header; }
            set
            {
                _IDMedia_Header = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia_AppCon
        {
            get { return _IDMedia_AppCon; }
            set
            {
                _IDMedia_AppCon = value;
                IsDirty = true;
            }
        }
        public SqlString Promotion
        {
            get { return _Promotion; }
            set
            {
                _Promotion = value;
                IsDirty = true;
            }
        }
        public SqlString Registration
        {
            get { return _Registration; }
            set
            {
                _Registration = value;
                IsDirty = true;
            }
        }
        public SqlString Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                IsDirty = true;
            }
        }
        public SqlString LoginTitle
        {
            get { return _LoginTitle; }
            set
            {
                _LoginTitle = value;
                IsDirty = true;
            }
        }
        public SqlString BgColor
        {
            get { return _BgColor; }
            set
            {
                _BgColor = value;
                IsDirty = true;
            }
        }
        public SqlString FgColor
        {
            get { return _FgColor; }
            set
            {
                _FgColor = value;
                IsDirty = true;
            }
        }
        public SqlString iTunesURL
        {
            get { return _iTunesURL; }
            set
            {
                _iTunesURL = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsJoinable
        {
            get { return _IsJoinable; }
            set
            {
                _IsJoinable = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblAreaCode : CTable
    {
        private SqlInt32 _AreaCode;
        private SqlString _Region;  //50
        public const int RegionMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _AreaCode = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_AreaCode; }
        public override void SetValue(SqlString val) { }

        public TblAreaCode() { }
        public TblAreaCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblAreaCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _AreaCode = rd.SafeGetInt32("AreaCode");
            _Region = rd.SafeGetString("Region");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _AreaCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _AreaCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _AreaCode);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Region", _Region);
        }

        #endregion
        #region accessors
        public SqlInt32 AreaCode
        {
            get { return _AreaCode; }
            set
            {
                _AreaCode = value;
                IsDirty = true;
            }
        }
        public SqlString Region
        {
            get { return _Region; }
            set
            {
                _Region = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblAreaCodeRegion : CTable
    {
        private SqlInt64 _IDAreaCode; //IDENTITY 
        private SqlInt32 _AreaCode;
        private SqlInt64 _IDRegion;
        private SqlString _Description;  //50
        private SqlInt32 _Priority;
        public const int DescriptionMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDAreaCode = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDAreaCode; }
        public override void SetValue(SqlString val) { _AreaCode = System.Convert.ToInt32(val); }

        public TblAreaCodeRegion() { }
        public TblAreaCodeRegion(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblAreaCodeRegion(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDAreaCode = rd.SafeGetInt64("IDAreaCode");
            _AreaCode = rd.SafeGetInt32("AreaCode");
            _IDRegion = rd.SafeGetInt64("IDRegion");
            _Description = rd.SafeGetString("Description");
            _Priority = rd.SafeGetInt32("Priority");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAreaCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAreaCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDAreaCode);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@AreaCode", _AreaCode);
            cmd.Parameters.AddWithValue("@IDRegion", _IDRegion);
            cmd.Parameters.AddWithValue("@Description", _Description);
            cmd.Parameters.AddWithValue("@Priority", _Priority);
        }

        #endregion
        #region accessors
        public SqlInt64 IDAreaCode
        {
            get { return _IDAreaCode; }
            private set
            {
                _IDAreaCode = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AreaCode
        {
            get { return _AreaCode; }
            set
            {
                _AreaCode = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDRegion
        {
            get { return _IDRegion; }
            set
            {
                _IDRegion = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Priority
        {
            get { return _Priority; }
            set
            {
                _Priority = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblCity : CTable
    {
        private SqlInt64 _IDCity; //IDENTITY 
        private SqlString _City;  //160
        private SqlInt64 _IDRegion;
        private SqlInt32 _IDCountry;
        private SqlInt32 _IDDMA;
        private SqlString _TZ;  //20
        private SqlDouble _Lat;
        private SqlDouble _Lon;
        private SqlString _Code;  //20
        public const int CityMaxLen = 160;
        public const int TZMaxLen = 20;
        public const int CodeMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDCity = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDCity; }
        public override void SetValue(SqlString val) { _City = val; }

        public TblCity() { }
        public TblCity(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblCity(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDCity = rd.SafeGetInt64("IDCity");
            _City = rd.SafeGetString("City");
            _IDRegion = rd.SafeGetInt64("IDRegion");
            _IDCountry = rd.SafeGetInt32("IDCountry");
            _IDDMA = rd.SafeGetInt32("IDDMA");
            _TZ = rd.SafeGetString("TZ");
            _Lat = rd.SafeGetDouble("Lat");
            _Lon = rd.SafeGetDouble("Lon");
            _Code = rd.SafeGetString("Code");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCity);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCity);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCity);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@City", _City);
            cmd.Parameters.AddWithValue("@IDRegion", _IDRegion);
            cmd.Parameters.AddWithValue("@IDCountry", _IDCountry);
            cmd.Parameters.AddWithValue("@IDDMA", _IDDMA);
            cmd.Parameters.AddWithValue("@TZ", _TZ);
            cmd.Parameters.AddWithValue("@Lat", _Lat);
            cmd.Parameters.AddWithValue("@Lon", _Lon);
            cmd.Parameters.AddWithValue("@Code", _Code);
        }

        #endregion
        #region accessors
        public SqlInt64 IDCity
        {
            get { return _IDCity; }
            private set
            {
                _IDCity = value;
                IsDirty = true;
            }
        }
        public SqlString City
        {
            get { return _City; }
            set
            {
                _City = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDRegion
        {
            get { return _IDRegion; }
            set
            {
                _IDRegion = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCountry
        {
            get { return _IDCountry; }
            set
            {
                _IDCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDMA
        {
            get { return _IDDMA; }
            set
            {
                _IDDMA = value;
                IsDirty = true;
            }
        }
        public SqlString TZ
        {
            get { return _TZ; }
            set
            {
                _TZ = value;
                IsDirty = true;
            }
        }
        public SqlDouble Lat
        {
            get { return _Lat; }
            set
            {
                _Lat = value;
                IsDirty = true;
            }
        }
        public SqlDouble Lon
        {
            get { return _Lon; }
            set
            {
                _Lon = value;
                IsDirty = true;
            }
        }
        public SqlString Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblCompany : CTable
    {
        private SqlInt32 _IDCompany; //IDENTITY 
        private SqlString _Company;  //50
        public const int CompanyMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDCompany = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDCompany; }
        public override void SetValue(SqlString val) { _Company = val; }

        public TblCompany() { }
        public TblCompany(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblCompany(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDCompany = rd.SafeGetInt32("IDCompany");
            _Company = rd.SafeGetString("Company");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCompany);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCompany);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Company", _Company);
        }

        #endregion
        #region accessors
        public SqlInt32 IDCompany
        {
            get { return _IDCompany; }
            private set
            {
                _IDCompany = value;
                IsDirty = true;
            }
        }
        public SqlString Company
        {
            get { return _Company; }
            set
            {
                _Company = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblContact : CTable
    {
        private SqlInt64 _IDContact; //IDENTITY 
        private SqlInt64 _IDDevice;
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDGroup;
        private SqlString _PIMContactUID;  //20
        private SqlInt32 _TYRelationship;
        private SqlInt32 _AuthenticationLevel;
        private SqlInt32 _ServerProcessingTime;
        private SqlDateTime _Updated;
        private SqlBoolean _CanManageClient;
        private SqlBoolean _ReadOnly;
        private SqlBoolean _CanUpdate;
        private SqlBoolean _IsNewRecord;
        private SqlBoolean _IsActive;
        public const int PIMContactUIDMaxLen = 20;
        private AssocContact_CompanyAssoc _AssocContact_Company;
        private AssocContact_EmailAssoc _AssocContact_Email;
        private AssocContact_LocationAssoc _AssocContact_Location;
        private AssocContact_MediaAssoc _AssocContact_Media;
        private AssocContact_NoteAssoc _AssocContact_Note;
        private AssocContact_PersonAssoc _AssocContact_Person;
        private AssocContact_PhoneAssoc _AssocContact_Phone;
        private AssocContact_PrefixAssoc _AssocContact_Prefix;
        private AssocContact_TitleAssoc _AssocContact_Title;
        private AssocContact_WebAddressAssoc _AssocContact_WebAddress;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDContact = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDContact; }
        public override void SetValue(SqlString val) { }

        public TblContact() { }
        public TblContact(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblContact(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocContact_Company = new AssocContact_CompanyAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Company, _AssocContact_Company);
            _AssocContact_Email = new AssocContact_EmailAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Email, _AssocContact_Email);
            _AssocContact_Location = new AssocContact_LocationAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Location, _AssocContact_Location);
            _AssocContact_Media = new AssocContact_MediaAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Media, _AssocContact_Media);
            _AssocContact_Note = new AssocContact_NoteAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Note, _AssocContact_Note);
            _AssocContact_Person = new AssocContact_PersonAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Person, _AssocContact_Person);
            _AssocContact_Phone = new AssocContact_PhoneAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Phone, _AssocContact_Phone);
            _AssocContact_Prefix = new AssocContact_PrefixAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Prefix, _AssocContact_Prefix);
            _AssocContact_Title = new AssocContact_TitleAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_Title, _AssocContact_Title);
            _AssocContact_WebAddress = new AssocContact_WebAddressAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocContact_WebAddress, _AssocContact_WebAddress);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDContact = rd.SafeGetInt64("IDContact");
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _PIMContactUID = rd.SafeGetString("PIMContactUID");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _AuthenticationLevel = rd.SafeGetInt32("AuthenticationLevel");
            _ServerProcessingTime = rd.SafeGetInt32("ServerProcessingTime");
            _Updated = rd.SafeGetDateTime("Updated");
            _CanManageClient = rd.SafeGetBoolean("CanManageClient");
            _ReadOnly = rd.SafeGetBoolean("ReadOnly");
            _CanUpdate = rd.SafeGetBoolean("CanUpdate");
            _IsNewRecord = rd.SafeGetBoolean("IsNewRecord");
            _IsActive = rd.SafeGetBoolean("IsActive");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDContact);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDevice", _IDDevice);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@PIMContactUID", _PIMContactUID);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            cmd.Parameters.AddWithValue("@AuthenticationLevel", _AuthenticationLevel);
            cmd.Parameters.AddWithValue("@ServerProcessingTime", _ServerProcessingTime);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
            cmd.Parameters.AddWithValue("@CanManageClient", _CanManageClient);
            cmd.Parameters.AddWithValue("@ReadOnly", _ReadOnly);
            cmd.Parameters.AddWithValue("@CanUpdate", _CanUpdate);
            cmd.Parameters.AddWithValue("@IsNewRecord", _IsNewRecord);
            cmd.Parameters.AddWithValue("@IsActive", _IsActive);
        }

        #endregion
        #region accessors
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            private set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlString PIMContactUID
        {
            get { return _PIMContactUID; }
            set
            {
                _PIMContactUID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AuthenticationLevel
        {
            get { return _AuthenticationLevel; }
            set
            {
                _AuthenticationLevel = value;
                IsDirty = true;
            }
        }
        public SqlInt32 ServerProcessingTime
        {
            get { return _ServerProcessingTime; }
            set
            {
                _ServerProcessingTime = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanManageClient
        {
            get { return _CanManageClient; }
            set
            {
                _CanManageClient = value;
                IsDirty = true;
            }
        }
        public SqlBoolean ReadOnly
        {
            get { return _ReadOnly; }
            set
            {
                _ReadOnly = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanUpdate
        {
            get { return _CanUpdate; }
            set
            {
                _CanUpdate = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsNewRecord
        {
            get { return _IsNewRecord; }
            set
            {
                _IsNewRecord = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsActive
        {
            get { return _IsActive; }
            set
            {
                _IsActive = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblCountry : CTable
    {
        private SqlInt32 _IDCountry; //IDENTITY 
        private SqlString _Country;  //100
        private SqlString _FIPS;  //8
        private SqlString _ISO;  //4
        private SqlString _ISO3;  //6
        private SqlString _ISON;  //8
        private SqlString _Internet;  //4
        private SqlString _Capital;  //100
        private SqlString _MapReference;  //100
        private SqlString _NationalitySingular;  //80
        private SqlString _NationalityPlural;  //80
        private SqlString _Currency;  //80
        private SqlString _CurrencyCode;  //8
        private SqlString _Title;  //160
        public const int CountryMaxLen = 100;
        public const int FIPSMaxLen = 8;
        public const int ISOMaxLen = 4;
        public const int ISO3MaxLen = 6;
        public const int ISONMaxLen = 8;
        public const int InternetMaxLen = 4;
        public const int CapitalMaxLen = 100;
        public const int MapReferenceMaxLen = 100;
        public const int NationalitySingularMaxLen = 80;
        public const int NationalityPluralMaxLen = 80;
        public const int CurrencyMaxLen = 80;
        public const int CurrencyCodeMaxLen = 8;
        public const int TitleMaxLen = 160;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDCountry = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDCountry; }
        public override void SetValue(SqlString val) { _Country = val; }

        public TblCountry() { }
        public TblCountry(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblCountry(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDCountry = rd.SafeGetInt32("IDCountry");
            _Country = rd.SafeGetString("Country");
            _FIPS = rd.SafeGetString("FIPS");
            _ISO = rd.SafeGetString("ISO");
            _ISO3 = rd.SafeGetString("ISO3");
            _ISON = rd.SafeGetString("ISON");
            _Internet = rd.SafeGetString("Internet");
            _Capital = rd.SafeGetString("Capital");
            _MapReference = rd.SafeGetString("MapReference");
            _NationalitySingular = rd.SafeGetString("NationalitySingular");
            _NationalityPlural = rd.SafeGetString("NationalityPlural");
            _Currency = rd.SafeGetString("Currency");
            _CurrencyCode = rd.SafeGetString("CurrencyCode");
            _Title = rd.SafeGetString("Title");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCountry);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCountry);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCountry);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Country", _Country);
            cmd.Parameters.AddWithValue("@FIPS", _FIPS);
            cmd.Parameters.AddWithValue("@ISO", _ISO);
            cmd.Parameters.AddWithValue("@ISO3", _ISO3);
            cmd.Parameters.AddWithValue("@ISON", _ISON);
            cmd.Parameters.AddWithValue("@Internet", _Internet);
            cmd.Parameters.AddWithValue("@Capital", _Capital);
            cmd.Parameters.AddWithValue("@MapReference", _MapReference);
            cmd.Parameters.AddWithValue("@NationalitySingular", _NationalitySingular);
            cmd.Parameters.AddWithValue("@NationalityPlural", _NationalityPlural);
            cmd.Parameters.AddWithValue("@Currency", _Currency);
            cmd.Parameters.AddWithValue("@CurrencyCode", _CurrencyCode);
            cmd.Parameters.AddWithValue("@Title", _Title);
        }

        #endregion
        #region accessors
        public SqlInt32 IDCountry
        {
            get { return _IDCountry; }
            private set
            {
                _IDCountry = value;
                IsDirty = true;
            }
        }
        public SqlString Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                IsDirty = true;
            }
        }
        public SqlString FIPS
        {
            get { return _FIPS; }
            set
            {
                _FIPS = value;
                IsDirty = true;
            }
        }
        public SqlString ISO
        {
            get { return _ISO; }
            set
            {
                _ISO = value;
                IsDirty = true;
            }
        }
        public SqlString ISO3
        {
            get { return _ISO3; }
            set
            {
                _ISO3 = value;
                IsDirty = true;
            }
        }
        public SqlString ISON
        {
            get { return _ISON; }
            set
            {
                _ISON = value;
                IsDirty = true;
            }
        }
        public SqlString Internet
        {
            get { return _Internet; }
            set
            {
                _Internet = value;
                IsDirty = true;
            }
        }
        public SqlString Capital
        {
            get { return _Capital; }
            set
            {
                _Capital = value;
                IsDirty = true;
            }
        }
        public SqlString MapReference
        {
            get { return _MapReference; }
            set
            {
                _MapReference = value;
                IsDirty = true;
            }
        }
        public SqlString NationalitySingular
        {
            get { return _NationalitySingular; }
            set
            {
                _NationalitySingular = value;
                IsDirty = true;
            }
        }
        public SqlString NationalityPlural
        {
            get { return _NationalityPlural; }
            set
            {
                _NationalityPlural = value;
                IsDirty = true;
            }
        }
        public SqlString Currency
        {
            get { return _Currency; }
            set
            {
                _Currency = value;
                IsDirty = true;
            }
        }
        public SqlString CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                _CurrencyCode = value;
                IsDirty = true;
            }
        }
        public SqlString Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblCreditCard : CTable
    {
        private SqlInt64 _IDCreditCard; //IDENTITY 
        private SqlInt32 _TYCreditCard;
        private SqlString _CreditCardType;  //50
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDLocation;
        private SqlInt64 _IDPhone;
        private SqlString _FirstName;  //90
        private SqlString _LastName;  //90
        private SqlString _NameOnCard;  //90
        private SqlString _CreditCardNumber;  //200
        private SqlString _CreditCardCode;  //200
        private SqlByte _ExpirationMonth;
        private SqlInt32 _ExpirationYear;
        private SqlString _CountryCode;  //4
        private SqlString _Display;  //90
        private SqlBoolean _IsRemoved;
        private SqlInt64 _IDLocationStreet;
        private SqlInt64 _IDLocationStreet2;
        private SqlInt64 _IDLocationCity;
        private SqlInt64 _IDLocationState;
        private SqlInt64 _IDLocationCountry;
        private SqlInt64 _IDLocationZipCode;
        public const int CreditCardTypeMaxLen = 50;
        public const int FirstNameMaxLen = 90;
        public const int LastNameMaxLen = 90;
        public const int NameOnCardMaxLen = 90;
        public const int CreditCardNumberMaxLen = 200;
        public const int CreditCardCodeMaxLen = 200;
        public const int CountryCodeMaxLen = 4;
        public const int DisplayMaxLen = 90;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDCreditCard = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDCreditCard; }
        public override void SetValue(SqlString val) { }

        public TblCreditCard() { }
        public TblCreditCard(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblCreditCard(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDCreditCard = rd.SafeGetInt64("IDCreditCard");
            _TYCreditCard = rd.SafeGetInt32("TYCreditCard");
            _CreditCardType = rd.SafeGetString("CreditCardType");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDLocation = rd.SafeGetInt64("IDLocation");
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _FirstName = rd.SafeGetString("FirstName");
            _LastName = rd.SafeGetString("LastName");
            _NameOnCard = rd.SafeGetString("NameOnCard");
            _CreditCardNumber = rd.SafeGetString("CreditCardNumber");
            _CreditCardCode = rd.SafeGetString("CreditCardCode");
            _ExpirationMonth = rd.SafeGetByte("ExpirationMonth");
            _ExpirationYear = rd.SafeGetInt32("ExpirationYear");
            _CountryCode = rd.SafeGetString("CountryCode");
            _Display = rd.SafeGetString("Display");
            _IsRemoved = rd.SafeGetBoolean("IsRemoved");
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCreditCard);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCreditCard);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDCreditCard);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYCreditCard", _TYCreditCard);
            cmd.Parameters.AddWithValue("@CreditCardType", _CreditCardType);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDLocation", _IDLocation);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@FirstName", _FirstName);
            cmd.Parameters.AddWithValue("@LastName", _LastName);
            cmd.Parameters.AddWithValue("@NameOnCard", _NameOnCard);
            cmd.Parameters.AddWithValue("@CreditCardNumber", _CreditCardNumber);
            cmd.Parameters.AddWithValue("@CreditCardCode", _CreditCardCode);
            cmd.Parameters.AddWithValue("@ExpirationMonth", _ExpirationMonth);
            cmd.Parameters.AddWithValue("@ExpirationYear", _ExpirationYear);
            cmd.Parameters.AddWithValue("@CountryCode", _CountryCode);
            cmd.Parameters.AddWithValue("@Display", _Display);
            cmd.Parameters.AddWithValue("@IsRemoved", _IsRemoved);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
        }

        #endregion
        #region accessors
        public SqlInt64 IDCreditCard
        {
            get { return _IDCreditCard; }
            private set
            {
                _IDCreditCard = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYCreditCard
        {
            get { return _TYCreditCard; }
            set
            {
                _TYCreditCard = value;
                IsDirty = true;
            }
        }
        public SqlString CreditCardType
        {
            get { return _CreditCardType; }
            set
            {
                _CreditCardType = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocation
        {
            get { return _IDLocation; }
            set
            {
                _IDLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlString FirstName
        {
            get { return _FirstName; }
            set
            {
                _FirstName = value;
                IsDirty = true;
            }
        }
        public SqlString LastName
        {
            get { return _LastName; }
            set
            {
                _LastName = value;
                IsDirty = true;
            }
        }
        public SqlString NameOnCard
        {
            get { return _NameOnCard; }
            set
            {
                _NameOnCard = value;
                IsDirty = true;
            }
        }
        public SqlString CreditCardNumber
        {
            get { return _CreditCardNumber; }
            set
            {
                _CreditCardNumber = value;
                IsDirty = true;
            }
        }
        public SqlString CreditCardCode
        {
            get { return _CreditCardCode; }
            set
            {
                _CreditCardCode = value;
                IsDirty = true;
            }
        }
        public SqlByte ExpirationMonth
        {
            get { return _ExpirationMonth; }
            set
            {
                _ExpirationMonth = value;
                IsDirty = true;
            }
        }
        public SqlInt32 ExpirationYear
        {
            get { return _ExpirationYear; }
            set
            {
                _ExpirationYear = value;
                IsDirty = true;
            }
        }
        public SqlString CountryCode
        {
            get { return _CountryCode; }
            set
            {
                _CountryCode = value;
                IsDirty = true;
            }
        }
        public SqlString Display
        {
            get { return _Display; }
            set
            {
                _Display = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsRemoved
        {
            get { return _IsRemoved; }
            set
            {
                _IsRemoved = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDataSource : CTable
    {
        private SqlInt32 _IDDataSource; //IDENTITY 
        private SqlString _DataSource;  //50
        private SqlString _KeySearchFieldType;  //50
        private SqlInt32 _UpdateFrequency;
        private SqlInt32 _DefaultStrength;
        private SqlBoolean _DefaultPull;
        private SqlBoolean _DefaultPush;
        public const int DataSourceMaxLen = 50;
        public const int KeySearchFieldTypeMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDataSource = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDataSource; }
        public override void SetValue(SqlString val) { _DataSource = val; }

        public TblDataSource() { }
        public TblDataSource(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDataSource(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _DataSource = rd.SafeGetString("DataSource");
            _KeySearchFieldType = rd.SafeGetString("KeySearchFieldType");
            _UpdateFrequency = rd.SafeGetInt32("UpdateFrequency");
            _DefaultStrength = rd.SafeGetInt32("DefaultStrength");
            _DefaultPull = rd.SafeGetBoolean("DefaultPull");
            _DefaultPush = rd.SafeGetBoolean("DefaultPush");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSource);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DataSource", _DataSource);
            cmd.Parameters.AddWithValue("@KeySearchFieldType", _KeySearchFieldType);
            cmd.Parameters.AddWithValue("@UpdateFrequency", _UpdateFrequency);
            cmd.Parameters.AddWithValue("@DefaultStrength", _DefaultStrength);
            cmd.Parameters.AddWithValue("@DefaultPull", _DefaultPull);
            cmd.Parameters.AddWithValue("@DefaultPush", _DefaultPush);
        }

        #endregion
        #region accessors
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            private set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlString DataSource
        {
            get { return _DataSource; }
            set
            {
                _DataSource = value;
                IsDirty = true;
            }
        }
        public SqlString KeySearchFieldType
        {
            get { return _KeySearchFieldType; }
            set
            {
                _KeySearchFieldType = value;
                IsDirty = true;
            }
        }
        public SqlInt32 UpdateFrequency
        {
            get { return _UpdateFrequency; }
            set
            {
                _UpdateFrequency = value;
                IsDirty = true;
            }
        }
        public SqlInt32 DefaultStrength
        {
            get { return _DefaultStrength; }
            set
            {
                _DefaultStrength = value;
                IsDirty = true;
            }
        }
        public SqlBoolean DefaultPull
        {
            get { return _DefaultPull; }
            set
            {
                _DefaultPull = value;
                IsDirty = true;
            }
        }
        public SqlBoolean DefaultPush
        {
            get { return _DefaultPush; }
            set
            {
                _DefaultPush = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDataSourceLogin : CTable
    {
        private SqlInt32 _IDDataSourceLogin; //IDENTITY 
        private SqlInt32 _IDDataSource;
        private SqlString _Login;  //50
        private SqlString _Password;  //50
        private SqlInt32 _STAccount;
        public const int LoginMaxLen = 50;
        public const int PasswordMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDataSourceLogin = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDataSourceLogin; }
        public override void SetValue(SqlString val) { }

        public TblDataSourceLogin() { }
        public TblDataSourceLogin(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDataSourceLogin(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDataSourceLogin = rd.SafeGetInt32("IDDataSourceLogin");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _Login = rd.SafeGetString("Login");
            _Password = rd.SafeGetString("Password");
            _STAccount = rd.SafeGetInt32("STAccount");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSourceLogin);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSourceLogin);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDataSourceLogin);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            cmd.Parameters.AddWithValue("@Login", _Login);
            cmd.Parameters.AddWithValue("@Password", _Password);
            cmd.Parameters.AddWithValue("@STAccount", _STAccount);
        }

        #endregion
        #region accessors
        public SqlInt32 IDDataSourceLogin
        {
            get { return _IDDataSourceLogin; }
            private set
            {
                _IDDataSourceLogin = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlString Login
        {
            get { return _Login; }
            set
            {
                _Login = value;
                IsDirty = true;
            }
        }
        public SqlString Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STAccount
        {
            get { return _STAccount; }
            set
            {
                _STAccount = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDevice : CTable
    {
        private SqlInt64 _IDDevice; //IDENTITY 
        private SqlString _Device;  //50
        private SqlString _PhoneNumber;  //20
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDSubscriber;
        private SqlString _TYCarrier;  //50
        private SqlInt32 _IDDeviceModel;
        private SqlString _PIN;  //20
        private SqlString _Token;  //40
        private SqlString _VersionOS;  //20
        private SqlString _VersionApp;  //20
        private SqlDateTime _DateAdded;
        private SqlDateTime _DateLastConnect;
        private SqlDateTime _DateLastSync;
        private SqlInt32 _TYDataInput;
        private SqlInt32 _PingCycle;
        private SqlInt32 _TYPingCycle;
        private SqlInt32 _SyncCycleTypeID;
        private SqlInt32 _DeviceStatusID;
        private SqlInt32 _CurrentContactPosition;
        private SqlInt32 _ChunkCount;
        private SqlBoolean _IsChunking;
        private SqlBoolean _HasCompleted;
        private SqlBoolean _IsCorrect;
        private SqlBoolean _LogVerbose;
        private SqlBoolean _IsForTesting;
        private SqlBoolean _EncodeForURL;
        public const int DeviceMaxLen = 50;
        public const int PhoneNumberMaxLen = 20;
        public const int TYCarrierMaxLen = 50;
        public const int PINMaxLen = 20;
        public const int TokenMaxLen = 40;
        public const int VersionOSMaxLen = 20;
        public const int VersionAppMaxLen = 20;
        private AssocDevice_ServerMessageAssoc _AssocDevice_ServerMessage;
        private OvrDevice_DataInputAssoc _OvrDevice_DataInput;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDevice = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDevice; }
        public override void SetValue(SqlString val) { _Device = val; }

        public TblDevice() { }
        public TblDevice(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDevice(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocDevice_ServerMessage = new AssocDevice_ServerMessageAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocDevice_ServerMessage, _AssocDevice_ServerMessage);
            _OvrDevice_DataInput = new OvrDevice_DataInputAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TOvrDevice_DataInput, _OvrDevice_DataInput);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDevice = rd.SafeGetInt64("IDDevice");
            _Device = rd.SafeGetString("Device");
            _PhoneNumber = rd.SafeGetString("PhoneNumber");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDSubscriber = rd.SafeGetInt64("IDSubscriber");
            _TYCarrier = rd.SafeGetString("TYCarrier");
            _IDDeviceModel = rd.SafeGetInt32("IDDeviceModel");
            _PIN = rd.SafeGetString("PIN");
            _Token = rd.SafeGetString("Token");
            _VersionOS = rd.SafeGetString("VersionOS");
            _VersionApp = rd.SafeGetString("VersionApp");
            _DateAdded = rd.SafeGetDateTime("DateAdded");
            _DateLastConnect = rd.SafeGetDateTime("DateLastConnect");
            _DateLastSync = rd.SafeGetDateTime("DateLastSync");
            _TYDataInput = rd.SafeGetInt32("TYDataInput");
            _PingCycle = rd.SafeGetInt32("PingCycle");
            _TYPingCycle = rd.SafeGetInt32("TYPingCycle");
            _SyncCycleTypeID = rd.SafeGetInt32("SyncCycleTypeID");
            _DeviceStatusID = rd.SafeGetInt32("DeviceStatusID");
            _CurrentContactPosition = rd.SafeGetInt32("CurrentContactPosition");
            _ChunkCount = rd.SafeGetInt32("ChunkCount");
            _IsChunking = rd.SafeGetBoolean("IsChunking");
            _HasCompleted = rd.SafeGetBoolean("HasCompleted");
            _IsCorrect = rd.SafeGetBoolean("IsCorrect");
            _LogVerbose = rd.SafeGetBoolean("LogVerbose");
            _IsForTesting = rd.SafeGetBoolean("IsForTesting");
            _EncodeForURL = rd.SafeGetBoolean("EncodeForURL");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDevice);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Device", _Device);
            cmd.Parameters.AddWithValue("@PhoneNumber", _PhoneNumber);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDSubscriber", _IDSubscriber);
            cmd.Parameters.AddWithValue("@TYCarrier", _TYCarrier);
            cmd.Parameters.AddWithValue("@IDDeviceModel", _IDDeviceModel);
            cmd.Parameters.AddWithValue("@PIN", _PIN);
            cmd.Parameters.AddWithValue("@Token", _Token);
            cmd.Parameters.AddWithValue("@VersionOS", _VersionOS);
            cmd.Parameters.AddWithValue("@VersionApp", _VersionApp);
            cmd.Parameters.AddWithValue("@DateAdded", _DateAdded);
            cmd.Parameters.AddWithValue("@DateLastConnect", _DateLastConnect);
            cmd.Parameters.AddWithValue("@DateLastSync", _DateLastSync);
            cmd.Parameters.AddWithValue("@TYDataInput", _TYDataInput);
            cmd.Parameters.AddWithValue("@PingCycle", _PingCycle);
            cmd.Parameters.AddWithValue("@TYPingCycle", _TYPingCycle);
            cmd.Parameters.AddWithValue("@SyncCycleTypeID", _SyncCycleTypeID);
            cmd.Parameters.AddWithValue("@DeviceStatusID", _DeviceStatusID);
            cmd.Parameters.AddWithValue("@CurrentContactPosition", _CurrentContactPosition);
            cmd.Parameters.AddWithValue("@ChunkCount", _ChunkCount);
            cmd.Parameters.AddWithValue("@IsChunking", _IsChunking);
            cmd.Parameters.AddWithValue("@HasCompleted", _HasCompleted);
            cmd.Parameters.AddWithValue("@IsCorrect", _IsCorrect);
            cmd.Parameters.AddWithValue("@LogVerbose", _LogVerbose);
            cmd.Parameters.AddWithValue("@IsForTesting", _IsForTesting);
            cmd.Parameters.AddWithValue("@EncodeForURL", _EncodeForURL);
        }

        #endregion
        #region accessors
        public SqlInt64 IDDevice
        {
            get { return _IDDevice; }
            private set
            {
                _IDDevice = value;
                IsDirty = true;
            }
        }
        public SqlString Device
        {
            get { return _Device; }
            set
            {
                _Device = value;
                IsDirty = true;
            }
        }
        public SqlString PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDSubscriber
        {
            get { return _IDSubscriber; }
            set
            {
                _IDSubscriber = value;
                IsDirty = true;
            }
        }
        public SqlString TYCarrier
        {
            get { return _TYCarrier; }
            set
            {
                _TYCarrier = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDeviceModel
        {
            get { return _IDDeviceModel; }
            set
            {
                _IDDeviceModel = value;
                IsDirty = true;
            }
        }
        public SqlString PIN
        {
            get { return _PIN; }
            set
            {
                _PIN = value;
                IsDirty = true;
            }
        }
        public SqlString Token
        {
            get { return _Token; }
            set
            {
                _Token = value;
                IsDirty = true;
            }
        }
        public SqlString VersionOS
        {
            get { return _VersionOS; }
            set
            {
                _VersionOS = value;
                IsDirty = true;
            }
        }
        public SqlString VersionApp
        {
            get { return _VersionApp; }
            set
            {
                _VersionApp = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateAdded
        {
            get { return _DateAdded; }
            set
            {
                _DateAdded = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateLastConnect
        {
            get { return _DateLastConnect; }
            set
            {
                _DateLastConnect = value;
                IsDirty = true;
            }
        }
        public SqlDateTime DateLastSync
        {
            get { return _DateLastSync; }
            set
            {
                _DateLastSync = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYDataInput
        {
            get { return _TYDataInput; }
            set
            {
                _TYDataInput = value;
                IsDirty = true;
            }
        }
        public SqlInt32 PingCycle
        {
            get { return _PingCycle; }
            set
            {
                _PingCycle = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYPingCycle
        {
            get { return _TYPingCycle; }
            set
            {
                _TYPingCycle = value;
                IsDirty = true;
            }
        }
        public SqlInt32 SyncCycleTypeID
        {
            get { return _SyncCycleTypeID; }
            set
            {
                _SyncCycleTypeID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 DeviceStatusID
        {
            get { return _DeviceStatusID; }
            set
            {
                _DeviceStatusID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 CurrentContactPosition
        {
            get { return _CurrentContactPosition; }
            set
            {
                _CurrentContactPosition = value;
                IsDirty = true;
            }
        }
        public SqlInt32 ChunkCount
        {
            get { return _ChunkCount; }
            set
            {
                _ChunkCount = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsChunking
        {
            get { return _IsChunking; }
            set
            {
                _IsChunking = value;
                IsDirty = true;
            }
        }
        public SqlBoolean HasCompleted
        {
            get { return _HasCompleted; }
            set
            {
                _HasCompleted = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsCorrect
        {
            get { return _IsCorrect; }
            set
            {
                _IsCorrect = value;
                IsDirty = true;
            }
        }
        public SqlBoolean LogVerbose
        {
            get { return _LogVerbose; }
            set
            {
                _LogVerbose = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsForTesting
        {
            get { return _IsForTesting; }
            set
            {
                _IsForTesting = value;
                IsDirty = true;
            }
        }
        public SqlBoolean EncodeForURL
        {
            get { return _EncodeForURL; }
            set
            {
                _EncodeForURL = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDeviceConfirmationCode : CTable
    {
        private SqlString _PhoneNumber;  //20
        private SqlString _DeviceUID;  //50
        private SqlInt32 _DeviceConfirmationCode;
        private SqlDateTime _Created;
        private SqlInt32 _AttemptsCounter;
        public const int PhoneNumberMaxLen = 20;
        public const int DeviceUIDMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _PhoneNumber = (SqlString)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_PhoneNumber; }
        public override void SetValue(SqlString val) { }

        public TblDeviceConfirmationCode() { }
        public TblDeviceConfirmationCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDeviceConfirmationCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _PhoneNumber = rd.SafeGetString("PhoneNumber");
            _DeviceUID = rd.SafeGetString("DeviceUID");
            _DeviceConfirmationCode = rd.SafeGetInt32("DeviceConfirmationCode");
            _Created = rd.SafeGetDateTime("Created");
            _AttemptsCounter = rd.SafeGetInt32("AttemptsCounter");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@PhoneNumber", _PhoneNumber);
            cmd.Parameters.AddWithValue("@DeviceUID", _DeviceUID);
            cmd.Parameters.AddWithValue("@DeviceConfirmationCode", _DeviceConfirmationCode);
            cmd.Parameters.AddWithValue("@Created", _Created);
            cmd.Parameters.AddWithValue("@AttemptsCounter", _AttemptsCounter);
        }

        #endregion
        #region accessors
        public SqlString PhoneNumber
        {
            get { return _PhoneNumber; }
            set
            {
                _PhoneNumber = value;
                IsDirty = true;
            }
        }
        public SqlString DeviceUID
        {
            get { return _DeviceUID; }
            set
            {
                _DeviceUID = value;
                IsDirty = true;
            }
        }
        public SqlInt32 DeviceConfirmationCode
        {
            get { return _DeviceConfirmationCode; }
            set
            {
                _DeviceConfirmationCode = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Created
        {
            get { return _Created; }
            set
            {
                _Created = value;
                IsDirty = true;
            }
        }
        public SqlInt32 AttemptsCounter
        {
            get { return _AttemptsCounter; }
            set
            {
                _AttemptsCounter = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDeviceManufacturer : CTable
    {
        private SqlInt32 _IDDeviceManufacturer; //IDENTITY 
        private SqlString _DeviceManufacturer;  //50
        public const int DeviceManufacturerMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDeviceManufacturer = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDeviceManufacturer; }
        public override void SetValue(SqlString val) { _DeviceManufacturer = val; }

        public TblDeviceManufacturer() { }
        public TblDeviceManufacturer(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDeviceManufacturer(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDeviceManufacturer = rd.SafeGetInt32("IDDeviceManufacturer");
            _DeviceManufacturer = rd.SafeGetString("DeviceManufacturer");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceManufacturer);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceManufacturer);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceManufacturer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DeviceManufacturer", _DeviceManufacturer);
        }

        #endregion
        #region accessors
        public SqlInt32 IDDeviceManufacturer
        {
            get { return _IDDeviceManufacturer; }
            private set
            {
                _IDDeviceManufacturer = value;
                IsDirty = true;
            }
        }
        public SqlString DeviceManufacturer
        {
            get { return _DeviceManufacturer; }
            set
            {
                _DeviceManufacturer = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDeviceModel : CTable
    {
        private SqlInt32 _IDDeviceModel; //IDENTITY 
        private SqlString _DeviceModel;  //50
        private SqlInt32 _IDDeviceManufacturer;
        private SqlInt32 _MediaHeight;
        private SqlInt32 _MediaWidth;
        private SqlBoolean _EncodeForURL;
        public const int DeviceModelMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDeviceModel = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDeviceModel; }
        public override void SetValue(SqlString val) { _DeviceModel = val; }

        public TblDeviceModel() { }
        public TblDeviceModel(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDeviceModel(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDeviceModel = rd.SafeGetInt32("IDDeviceModel");
            _DeviceModel = rd.SafeGetString("DeviceModel");
            _IDDeviceManufacturer = rd.SafeGetInt32("IDDeviceManufacturer");
            _MediaHeight = rd.SafeGetInt32("MediaHeight");
            _MediaWidth = rd.SafeGetInt32("MediaWidth");
            _EncodeForURL = rd.SafeGetBoolean("EncodeForURL");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceModel);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceModel);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDeviceModel);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DeviceModel", _DeviceModel);
            cmd.Parameters.AddWithValue("@IDDeviceManufacturer", _IDDeviceManufacturer);
            cmd.Parameters.AddWithValue("@MediaHeight", _MediaHeight);
            cmd.Parameters.AddWithValue("@MediaWidth", _MediaWidth);
            cmd.Parameters.AddWithValue("@EncodeForURL", _EncodeForURL);
        }

        #endregion
        #region accessors
        public SqlInt32 IDDeviceModel
        {
            get { return _IDDeviceModel; }
            private set
            {
                _IDDeviceModel = value;
                IsDirty = true;
            }
        }
        public SqlString DeviceModel
        {
            get { return _DeviceModel; }
            set
            {
                _DeviceModel = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDeviceManufacturer
        {
            get { return _IDDeviceManufacturer; }
            set
            {
                _IDDeviceManufacturer = value;
                IsDirty = true;
            }
        }
        public SqlInt32 MediaHeight
        {
            get { return _MediaHeight; }
            set
            {
                _MediaHeight = value;
                IsDirty = true;
            }
        }
        public SqlInt32 MediaWidth
        {
            get { return _MediaWidth; }
            set
            {
                _MediaWidth = value;
                IsDirty = true;
            }
        }
        public SqlBoolean EncodeForURL
        {
            get { return _EncodeForURL; }
            set
            {
                _EncodeForURL = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblDMA : CTable
    {
        private SqlInt32 _IDDMA; //IDENTITY 
        private SqlInt16 _DMA;
        private SqlInt32 _IDCountry;
        private SqlString _Market;  //120
        public const int MarketMaxLen = 120;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDDMA = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDDMA; }
        public override void SetValue(SqlString val) { _DMA = System.Convert.ToInt16(val); }

        public TblDMA() { }
        public TblDMA(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblDMA(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDDMA = rd.SafeGetInt32("IDDMA");
            _DMA = rd.SafeGetInt16("DMA");
            _IDCountry = rd.SafeGetInt32("IDCountry");
            _Market = rd.SafeGetString("Market");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDMA);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDMA);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDDMA);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DMA", _DMA);
            cmd.Parameters.AddWithValue("@IDCountry", _IDCountry);
            cmd.Parameters.AddWithValue("@Market", _Market);
        }

        #endregion
        #region accessors
        public SqlInt32 IDDMA
        {
            get { return _IDDMA; }
            private set
            {
                _IDDMA = value;
                IsDirty = true;
            }
        }
        public SqlInt16 DMA
        {
            get { return _DMA; }
            set
            {
                _DMA = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCountry
        {
            get { return _IDCountry; }
            set
            {
                _IDCountry = value;
                IsDirty = true;
            }
        }
        public SqlString Market
        {
            get { return _Market; }
            set
            {
                _Market = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblEmail : CTable
    {
        private SqlInt64 _IDEmail; //IDENTITY 
        private SqlString _Email;  //100
        public const int EmailMaxLen = 100;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDEmail = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDEmail; }
        public override void SetValue(SqlString val) { _Email = val; }

        public TblEmail() { }
        public TblEmail(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblEmail(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _Email = rd.SafeGetString("Email");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDEmail);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDEmail);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDEmail);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Email", _Email);
        }

        #endregion
        #region accessors
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            private set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlString Email
        {
            get { return _Email; }
            set
            {
                _Email = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblGroup : CTable
    {
        private SqlInt64 _IDGroup; //IDENTITY 
        private SqlString _Group;  //100
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDNote;
        private SqlInt64 _IDPhone_NotificationGateway;
        private SqlInt64 _IDEmail_NotificationGateway;
        private SqlInt32 _TYRelationship;
        private SqlInt32 _TYCommunication;
        private SqlBoolean _IsManualTarget;
        private SqlDateTime _LastTargetCheck;
        private SqlBoolean _IsDiscoverable;
        private SqlByte _JoinableType;
        private SqlByte _HistoryCommunicationType;
        public const int GroupMaxLen = 100;
        private AssocGroup_ApplicationAssoc _AssocGroup_Application;
        private AssocGroup_GroupAssoc _AssocGroup_Group;
        private AssocGroup_InterestAttributeAssoc _AssocGroup_InterestAttribute;
        private AssocGroup_MetroAreaAssoc _AssocGroup_MetroArea;
        private AssocGroup_NewsChannelAssoc _AssocGroup_NewsChannel;
        private LnkGroup_Profile_AdminAssoc _LnkGroup_Profile_Admin;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDGroup = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDGroup; }
        public override void SetValue(SqlString val) { _Group = val; }

        public TblGroup() { }
        public TblGroup(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblGroup(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocGroup_Application = new AssocGroup_ApplicationAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocGroup_Application, _AssocGroup_Application);
            _AssocGroup_Group = new AssocGroup_GroupAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocGroup_Group, _AssocGroup_Group);
            _AssocGroup_InterestAttribute = new AssocGroup_InterestAttributeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocGroup_InterestAttribute, _AssocGroup_InterestAttribute);
            _AssocGroup_MetroArea = new AssocGroup_MetroAreaAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocGroup_MetroArea, _AssocGroup_MetroArea);
            _AssocGroup_NewsChannel = new AssocGroup_NewsChannelAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocGroup_NewsChannel, _AssocGroup_NewsChannel);
            _LnkGroup_Profile_Admin = new LnkGroup_Profile_AdminAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkGroup_Profile_Admin, _LnkGroup_Profile_Admin);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _Group = rd.SafeGetString("Group");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDNote = rd.SafeGetInt64("IDNote");
            _IDPhone_NotificationGateway = rd.SafeGetInt64("IDPhone_NotificationGateway");
            _IDEmail_NotificationGateway = rd.SafeGetInt64("IDEmail_NotificationGateway");
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _TYCommunication = rd.SafeGetInt32("TYCommunication");
            _IsManualTarget = rd.SafeGetBoolean("IsManualTarget");
            _LastTargetCheck = rd.SafeGetDateTime("LastTargetCheck");
            _IsDiscoverable = rd.SafeGetBoolean("IsDiscoverable");
            _JoinableType = rd.SafeGetByte("JoinableType");
            _HistoryCommunicationType = rd.SafeGetByte("HistoryCommunicationType");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDGroup);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Group", _Group);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDNote", _IDNote);
            cmd.Parameters.AddWithValue("@IDPhone_NotificationGateway", _IDPhone_NotificationGateway);
            cmd.Parameters.AddWithValue("@IDEmail_NotificationGateway", _IDEmail_NotificationGateway);
            cmd.Parameters.AddWithValue("@TYRelationship", _TYRelationship);
            cmd.Parameters.AddWithValue("@TYCommunication", _TYCommunication);
            cmd.Parameters.AddWithValue("@IsManualTarget", _IsManualTarget);
            cmd.Parameters.AddWithValue("@LastTargetCheck", _LastTargetCheck);
            cmd.Parameters.AddWithValue("@IsDiscoverable", _IsDiscoverable);
            cmd.Parameters.AddWithValue("@JoinableType", _JoinableType);
            cmd.Parameters.AddWithValue("@HistoryCommunicationType", _HistoryCommunicationType);
        }

        #endregion
        #region accessors
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            private set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlString Group
        {
            get { return _Group; }
            set
            {
                _Group = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDNote
        {
            get { return _IDNote; }
            set
            {
                _IDNote = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_NotificationGateway
        {
            get { return _IDPhone_NotificationGateway; }
            set
            {
                _IDPhone_NotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail_NotificationGateway
        {
            get { return _IDEmail_NotificationGateway; }
            set
            {
                _IDEmail_NotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYCommunication
        {
            get { return _TYCommunication; }
            set
            {
                _TYCommunication = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManualTarget
        {
            get { return _IsManualTarget; }
            set
            {
                _IsManualTarget = value;
                IsDirty = true;
            }
        }
        public SqlDateTime LastTargetCheck
        {
            get { return _LastTargetCheck; }
            set
            {
                _LastTargetCheck = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsDiscoverable
        {
            get { return _IsDiscoverable; }
            set
            {
                _IsDiscoverable = value;
                IsDirty = true;
            }
        }
        public SqlByte JoinableType
        {
            get { return _JoinableType; }
            set
            {
                _JoinableType = value;
                IsDirty = true;
            }
        }
        public SqlByte HistoryCommunicationType
        {
            get { return _HistoryCommunicationType; }
            set
            {
                _HistoryCommunicationType = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblInterestArea : CTable
    {
        private SqlInt32 _IDInterestArea; //IDENTITY 
        private SqlString _InterestArea;  //50
        private SqlString _Description;  //150
        public const int InterestAreaMaxLen = 50;
        public const int DescriptionMaxLen = 150;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDInterestArea = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDInterestArea; }
        public override void SetValue(SqlString val) { _InterestArea = val; }

        public TblInterestArea() { }
        public TblInterestArea(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblInterestArea(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDInterestArea = rd.SafeGetInt32("IDInterestArea");
            _InterestArea = rd.SafeGetString("InterestArea");
            _Description = rd.SafeGetString("Description");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestArea);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestArea);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestArea);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@InterestArea", _InterestArea);
            cmd.Parameters.AddWithValue("@Description", _Description);
        }

        #endregion
        #region accessors
        public SqlInt32 IDInterestArea
        {
            get { return _IDInterestArea; }
            private set
            {
                _IDInterestArea = value;
                IsDirty = true;
            }
        }
        public SqlString InterestArea
        {
            get { return _InterestArea; }
            set
            {
                _InterestArea = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblInterestAttribute : CTable
    {
        private SqlInt32 _IDInterestAttribute; //IDENTITY 
        private SqlString _InterestAttribute;  //50
        private SqlInt32 _IDInterestArea;
        private SqlString _Description;  //150
        public const int InterestAttributeMaxLen = 50;
        public const int DescriptionMaxLen = 150;
        private AssocInterestAttribute_InterestAttribute_StrengthAssoc _AssocInterestAttribute_InterestAttribute_Strength;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDInterestAttribute = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDInterestAttribute; }
        public override void SetValue(SqlString val) { _InterestAttribute = val; }

        public TblInterestAttribute() { }
        public TblInterestAttribute(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblInterestAttribute(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocInterestAttribute_InterestAttribute_Strength = new AssocInterestAttribute_InterestAttribute_StrengthAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocInterestAttribute_InterestAttribute_Strength, _AssocInterestAttribute_InterestAttribute_Strength);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDInterestAttribute = rd.SafeGetInt32("IDInterestAttribute");
            _InterestAttribute = rd.SafeGetString("InterestAttribute");
            _IDInterestArea = rd.SafeGetInt32("IDInterestArea");
            _Description = rd.SafeGetString("Description");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestAttribute);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDInterestAttribute);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@InterestAttribute", _InterestAttribute);
            cmd.Parameters.AddWithValue("@IDInterestArea", _IDInterestArea);
            cmd.Parameters.AddWithValue("@Description", _Description);
        }

        #endregion
        #region accessors
        public SqlInt32 IDInterestAttribute
        {
            get { return _IDInterestAttribute; }
            private set
            {
                _IDInterestAttribute = value;
                IsDirty = true;
            }
        }
        public SqlString InterestAttribute
        {
            get { return _InterestAttribute; }
            set
            {
                _InterestAttribute = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDInterestArea
        {
            get { return _IDInterestArea; }
            set
            {
                _IDInterestArea = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocation : CTable
    {
        private SqlInt64 _IDLocation; //IDENTITY 
        private SqlInt64 _IDZipCode;
        private SqlInt64 _IDCity;
        private SqlString _Street;  //60
        private SqlString _Street2;  //40
        private SqlString _City;  //80
        private SqlString _Region;  //50
        private SqlString _Country;  //50
        private SqlString _ZipCode;  //20
        public const int StreetMaxLen = 60;
        public const int Street2MaxLen = 40;
        public const int CityMaxLen = 80;
        public const int RegionMaxLen = 50;
        public const int CountryMaxLen = 50;
        public const int ZipCodeMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocation = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocation; }
        public override void SetValue(SqlString val) { }

        public TblLocation() { }
        public TblLocation(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocation(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocation = rd.SafeGetInt64("IDLocation");
            _IDZipCode = rd.SafeGetInt64("IDZipCode");
            _IDCity = rd.SafeGetInt64("IDCity");
            _Street = rd.SafeGetString("Street");
            _Street2 = rd.SafeGetString("Street2");
            _City = rd.SafeGetString("City");
            _Region = rd.SafeGetString("Region");
            _Country = rd.SafeGetString("Country");
            _ZipCode = rd.SafeGetString("ZipCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDZipCode", _IDZipCode);
            cmd.Parameters.AddWithValue("@IDCity", _IDCity);
            cmd.Parameters.AddWithValue("@Street", _Street);
            cmd.Parameters.AddWithValue("@Street2", _Street2);
            cmd.Parameters.AddWithValue("@City", _City);
            cmd.Parameters.AddWithValue("@Region", _Region);
            cmd.Parameters.AddWithValue("@Country", _Country);
            cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocation
        {
            get { return _IDLocation; }
            private set
            {
                _IDLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDZipCode
        {
            get { return _IDZipCode; }
            set
            {
                _IDZipCode = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDCity
        {
            get { return _IDCity; }
            set
            {
                _IDCity = value;
                IsDirty = true;
            }
        }
        public SqlString Street
        {
            get { return _Street; }
            set
            {
                _Street = value;
                IsDirty = true;
            }
        }
        public SqlString Street2
        {
            get { return _Street2; }
            set
            {
                _Street2 = value;
                IsDirty = true;
            }
        }
        public SqlString City
        {
            get { return _City; }
            set
            {
                _City = value;
                IsDirty = true;
            }
        }
        public SqlString Region
        {
            get { return _Region; }
            set
            {
                _Region = value;
                IsDirty = true;
            }
        }
        public SqlString Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                IsDirty = true;
            }
        }
        public SqlString ZipCode
        {
            get { return _ZipCode; }
            set
            {
                _ZipCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationCity : CTable
    {
        private SqlInt64 _IDLocationCity; //IDENTITY 
        private SqlString _LocationCity;  //50
        public const int LocationCityMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationCity = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationCity; }
        public override void SetValue(SqlString val) { _LocationCity = val; }

        public TblLocationCity() { }
        public TblLocationCity(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationCity(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _LocationCity = rd.SafeGetString("LocationCity");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCity);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCity);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCity);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationCity", _LocationCity);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            private set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlString LocationCity
        {
            get { return _LocationCity; }
            set
            {
                _LocationCity = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationCountry : CTable
    {
        private SqlInt64 _IDLocationCountry; //IDENTITY 
        private SqlString _LocationCountry;  //50
        public const int LocationCountryMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationCountry = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationCountry; }
        public override void SetValue(SqlString val) { _LocationCountry = val; }

        public TblLocationCountry() { }
        public TblLocationCountry(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationCountry(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _LocationCountry = rd.SafeGetString("LocationCountry");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCountry);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCountry);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationCountry);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationCountry", _LocationCountry);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            private set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlString LocationCountry
        {
            get { return _LocationCountry; }
            set
            {
                _LocationCountry = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationState : CTable
    {
        private SqlInt64 _IDLocationState; //IDENTITY 
        private SqlString _LocationState;  //50
        public const int LocationStateMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationState = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationState; }
        public override void SetValue(SqlString val) { _LocationState = val; }

        public TblLocationState() { }
        public TblLocationState(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationState(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _LocationState = rd.SafeGetString("LocationState");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationState);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationState);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationState);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationState", _LocationState);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            private set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlString LocationState
        {
            get { return _LocationState; }
            set
            {
                _LocationState = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationStreet : CTable
    {
        private SqlInt64 _IDLocationStreet; //IDENTITY 
        private SqlString _LocationStreet;  //150
        public const int LocationStreetMaxLen = 150;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationStreet = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationStreet; }
        public override void SetValue(SqlString val) { _LocationStreet = val; }

        public TblLocationStreet() { }
        public TblLocationStreet(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationStreet(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _LocationStreet = rd.SafeGetString("LocationStreet");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationStreet", _LocationStreet);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            private set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlString LocationStreet
        {
            get { return _LocationStreet; }
            set
            {
                _LocationStreet = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationStreet2 : CTable
    {
        private SqlInt64 _IDLocationStreet2; //IDENTITY 
        private SqlString _LocationStreet2;  //50
        public const int LocationStreet2MaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationStreet2 = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationStreet2; }
        public override void SetValue(SqlString val) { _LocationStreet2 = val; }

        public TblLocationStreet2() { }
        public TblLocationStreet2(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationStreet2(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _LocationStreet2 = rd.SafeGetString("LocationStreet2");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet2);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet2);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationStreet2);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationStreet2", _LocationStreet2);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            private set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlString LocationStreet2
        {
            get { return _LocationStreet2; }
            set
            {
                _LocationStreet2 = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblLocationZipCode : CTable
    {
        private SqlInt64 _IDLocationZipCode; //IDENTITY 
        private SqlString _LocationZipCode;  //50
        public const int LocationZipCodeMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDLocationZipCode = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDLocationZipCode; }
        public override void SetValue(SqlString val) { _LocationZipCode = val; }

        public TblLocationZipCode() { }
        public TblLocationZipCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblLocationZipCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            _LocationZipCode = rd.SafeGetString("LocationZipCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDLocationZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LocationZipCode", _LocationZipCode);
        }

        #endregion
        #region accessors
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            private set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }
        public SqlString LocationZipCode
        {
            get { return _LocationZipCode; }
            set
            {
                _LocationZipCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblMedia : CTable
    {
        private SqlInt64 _IDMedia; //IDENTITY 
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDContact;
        private SqlInt32 _TYMediaSource;
        private SqlString _SourceURL;  //255
        private SqlString _MediaURI;  //255
        private SqlString _MediaFileName;  //50
        private SqlBinary _PictureData;
        private SqlString _PictureDataB64;  //8000
        private SqlInt32 _Width;
        private SqlInt32 _Height;
        private SqlInt32 _BitDepth;
        private SqlBoolean _IsGallery;
        public const int SourceURLMaxLen = 255;
        public const int MediaURIMaxLen = 255;
        public const int MediaFileNameMaxLen = 50;
        public const int PictureDataB64MaxLen = 8000;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDMedia = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDMedia; }
        public override void SetValue(SqlString val) { }

        public TblMedia() { }
        public TblMedia(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblMedia(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDContact = rd.SafeGetInt64("IDContact");
            _TYMediaSource = rd.SafeGetInt32("TYMediaSource");
            _SourceURL = rd.SafeGetString("SourceURL");
            _MediaURI = rd.SafeGetString("MediaURI");
            _MediaFileName = rd.SafeGetString("MediaFileName");
            _PictureData = rd.SafeGetBinary("PictureData");
            _PictureDataB64 = rd.SafeGetString("PictureDataB64");
            _Width = rd.SafeGetInt32("Width");
            _Height = rd.SafeGetInt32("Height");
            _BitDepth = rd.SafeGetInt32("BitDepth");
            _IsGallery = rd.SafeGetBoolean("IsGallery");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMedia);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMedia);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDContact", _IDContact);
            cmd.Parameters.AddWithValue("@TYMediaSource", _TYMediaSource);
            cmd.Parameters.AddWithValue("@SourceURL", _SourceURL);
            cmd.Parameters.AddWithValue("@MediaURI", _MediaURI);
            cmd.Parameters.AddWithValue("@MediaFileName", _MediaFileName);
            cmd.Parameters.AddWithValue("@PictureData", _PictureData);
            cmd.Parameters.AddWithValue("@PictureDataB64", _PictureDataB64);
            cmd.Parameters.AddWithValue("@Width", _Width);
            cmd.Parameters.AddWithValue("@Height", _Height);
            cmd.Parameters.AddWithValue("@BitDepth", _BitDepth);
            cmd.Parameters.AddWithValue("@IsGallery", _IsGallery);
        }

        #endregion
        #region accessors
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            private set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDContact
        {
            get { return _IDContact; }
            set
            {
                _IDContact = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYMediaSource
        {
            get { return _TYMediaSource; }
            set
            {
                _TYMediaSource = value;
                IsDirty = true;
            }
        }
        public SqlString SourceURL
        {
            get { return _SourceURL; }
            set
            {
                _SourceURL = value;
                IsDirty = true;
            }
        }
        public SqlString MediaURI
        {
            get { return _MediaURI; }
            set
            {
                _MediaURI = value;
                IsDirty = true;
            }
        }
        public SqlString MediaFileName
        {
            get { return _MediaFileName; }
            set
            {
                _MediaFileName = value;
                IsDirty = true;
            }
        }
        public SqlBinary PictureData
        {
            get { return _PictureData; }
            set
            {
                _PictureData = value;
                IsDirty = true;
            }
        }
        public SqlString PictureDataB64
        {
            get { return _PictureDataB64; }
            set
            {
                _PictureDataB64 = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Width
        {
            get { return _Width; }
            set
            {
                _Width = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Height
        {
            get { return _Height; }
            set
            {
                _Height = value;
                IsDirty = true;
            }
        }
        public SqlInt32 BitDepth
        {
            get { return _BitDepth; }
            set
            {
                _BitDepth = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsGallery
        {
            get { return _IsGallery; }
            set
            {
                _IsGallery = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblMerchant : CTable
    {
        private SqlInt64 _IDMerchant; //IDENTITY 
        private SqlInt32 _IDCompany;
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDLocation;
        private SqlInt64 _IDWebAddress;
        private SqlInt64 _IDPhone_Work;
        private SqlInt64 _IDPhone_Fax;
        private SqlInt64 _IDPhone_Other;
        private SqlInt64 _IDEmail;
        private SqlInt64 _IDLocationStreet;
        private SqlInt64 _IDLocationStreet2;
        private SqlInt64 _IDLocationCity;
        private SqlInt64 _IDLocationState;
        private SqlInt64 _IDLocationCountry;
        private SqlInt64 _IDLocationZipCode;
        private SqlInt32 _TransactionIDSeed;
        private SqlBoolean _CanEmail;
        private SqlBoolean _IsRemoved;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDMerchant = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDMerchant; }
        public override void SetValue(SqlString val) { }

        public TblMerchant() { }
        public TblMerchant(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblMerchant(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDMerchant = rd.SafeGetInt64("IDMerchant");
            _IDCompany = rd.SafeGetInt32("IDCompany");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDLocation = rd.SafeGetInt64("IDLocation");
            _IDWebAddress = rd.SafeGetInt64("IDWebAddress");
            _IDPhone_Work = rd.SafeGetInt64("IDPhone_Work");
            _IDPhone_Fax = rd.SafeGetInt64("IDPhone_Fax");
            _IDPhone_Other = rd.SafeGetInt64("IDPhone_Other");
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            _TransactionIDSeed = rd.SafeGetInt32("TransactionIDSeed");
            _CanEmail = rd.SafeGetBoolean("CanEmail");
            _IsRemoved = rd.SafeGetBoolean("IsRemoved");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMerchant);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMerchant);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMerchant);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDCompany", _IDCompany);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDLocation", _IDLocation);
            cmd.Parameters.AddWithValue("@IDWebAddress", _IDWebAddress);
            cmd.Parameters.AddWithValue("@IDPhone_Work", _IDPhone_Work);
            cmd.Parameters.AddWithValue("@IDPhone_Fax", _IDPhone_Fax);
            cmd.Parameters.AddWithValue("@IDPhone_Other", _IDPhone_Other);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            cmd.Parameters.AddWithValue("@TransactionIDSeed", _TransactionIDSeed);
            cmd.Parameters.AddWithValue("@CanEmail", _CanEmail);
            cmd.Parameters.AddWithValue("@IsRemoved", _IsRemoved);
        }

        #endregion
        #region accessors
        public SqlInt64 IDMerchant
        {
            get { return _IDMerchant; }
            private set
            {
                _IDMerchant = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCompany
        {
            get { return _IDCompany; }
            set
            {
                _IDCompany = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocation
        {
            get { return _IDLocation; }
            set
            {
                _IDLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDWebAddress
        {
            get { return _IDWebAddress; }
            set
            {
                _IDWebAddress = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Work
        {
            get { return _IDPhone_Work; }
            set
            {
                _IDPhone_Work = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Fax
        {
            get { return _IDPhone_Fax; }
            set
            {
                _IDPhone_Fax = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Other
        {
            get { return _IDPhone_Other; }
            set
            {
                _IDPhone_Other = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TransactionIDSeed
        {
            get { return _TransactionIDSeed; }
            set
            {
                _TransactionIDSeed = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanEmail
        {
            get { return _CanEmail; }
            set
            {
                _CanEmail = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsRemoved
        {
            get { return _IsRemoved; }
            set
            {
                _IsRemoved = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblMetroArea : CTable
    {
        private SqlInt32 _IDMetroArea;
        private SqlString _MetroArea;  //100
        private SqlInt64 _IDRegion;
        private SqlString _Region;  //50
        public const int MetroAreaMaxLen = 100;
        public const int RegionMaxLen = 50;
        private AssocMetroArea_AreaCodeAssoc _AssocMetroArea_AreaCode;
        private AssocMetroArea_ZipCodeAssoc _AssocMetroArea_ZipCode;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDMetroArea = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDMetroArea; }
        public override void SetValue(SqlString val) { }

        public TblMetroArea() { }
        public TblMetroArea(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblMetroArea(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocMetroArea_AreaCode = new AssocMetroArea_AreaCodeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocMetroArea_AreaCode, _AssocMetroArea_AreaCode);
            _AssocMetroArea_ZipCode = new AssocMetroArea_ZipCodeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocMetroArea_ZipCode, _AssocMetroArea_ZipCode);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDMetroArea = rd.SafeGetInt32("IDMetroArea");
            _MetroArea = rd.SafeGetString("MetroArea");
            _IDRegion = rd.SafeGetInt64("IDRegion");
            _Region = rd.SafeGetString("Region");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMetroArea);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDMetroArea);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@MetroArea", _MetroArea);
            cmd.Parameters.AddWithValue("@IDRegion", _IDRegion);
            cmd.Parameters.AddWithValue("@Region", _Region);
        }

        #endregion
        #region accessors
        public SqlInt32 IDMetroArea
        {
            get { return _IDMetroArea; }
            set
            {
                _IDMetroArea = value;
                IsDirty = true;
            }
        }
        public SqlString MetroArea
        {
            get { return _MetroArea; }
            set
            {
                _MetroArea = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDRegion
        {
            get { return _IDRegion; }
            set
            {
                _IDRegion = value;
                IsDirty = true;
            }
        }
        public SqlString Region
        {
            get { return _Region; }
            set
            {
                _Region = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNewsChannel : CTable
    {
        private SqlInt32 _IDNewsChannel; //IDENTITY 
        private SqlString _Title;  //100
        private SqlString _Description;  //500
        private SqlString _Copyright;  //100
        private SqlString _URL;  //200
        private SqlString _Link;  //200
        private SqlString _ImageURL;  //200
        private SqlDateTime _LastChecked;
        public const int TitleMaxLen = 100;
        public const int DescriptionMaxLen = 500;
        public const int CopyrightMaxLen = 100;
        public const int URLMaxLen = 200;
        public const int LinkMaxLen = 200;
        public const int ImageURLMaxLen = 200;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNewsChannel = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNewsChannel; }
        public override void SetValue(SqlString val) { }

        public TblNewsChannel() { }
        public TblNewsChannel(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNewsChannel(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNewsChannel = rd.SafeGetInt32("IDNewsChannel");
            _Title = rd.SafeGetString("Title");
            _Description = rd.SafeGetString("Description");
            _Copyright = rd.SafeGetString("Copyright");
            _URL = rd.SafeGetString("URL");
            _Link = rd.SafeGetString("Link");
            _ImageURL = rd.SafeGetString("ImageURL");
            _LastChecked = rd.SafeGetDateTime("LastChecked");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsChannel);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsChannel);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Title", _Title);
            cmd.Parameters.AddWithValue("@Description", _Description);
            cmd.Parameters.AddWithValue("@Copyright", _Copyright);
            cmd.Parameters.AddWithValue("@URL", _URL);
            cmd.Parameters.AddWithValue("@Link", _Link);
            cmd.Parameters.AddWithValue("@ImageURL", _ImageURL);
            cmd.Parameters.AddWithValue("@LastChecked", _LastChecked);
        }

        #endregion
        #region accessors
        public SqlInt32 IDNewsChannel
        {
            get { return _IDNewsChannel; }
            private set
            {
                _IDNewsChannel = value;
                IsDirty = true;
            }
        }
        public SqlString Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }
        public SqlString Copyright
        {
            get { return _Copyright; }
            set
            {
                _Copyright = value;
                IsDirty = true;
            }
        }
        public SqlString URL
        {
            get { return _URL; }
            set
            {
                _URL = value;
                IsDirty = true;
            }
        }
        public SqlString Link
        {
            get { return _Link; }
            set
            {
                _Link = value;
                IsDirty = true;
            }
        }
        public SqlString ImageURL
        {
            get { return _ImageURL; }
            set
            {
                _ImageURL = value;
                IsDirty = true;
            }
        }
        public SqlDateTime LastChecked
        {
            get { return _LastChecked; }
            set
            {
                _LastChecked = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNewsItem : CTable
    {
        private SqlInt32 _IDNewsItem; //IDENTITY 
        private SqlInt32 _IDNewsChannel;
        private SqlString _Title;  //200
        private SqlString _Summary;  //1000
        private SqlString _Link;  //200
        private SqlString _GUID;  //200
        private SqlString _URL;  //500
        private SqlDateTime _Published;
        private SqlString _MediaType;  //50
        private SqlInt32 _MediaLength;
        private SqlBoolean _IsProcessing;
        private SqlBoolean _CanDrop;
        public const int TitleMaxLen = 200;
        public const int SummaryMaxLen = 1000;
        public const int LinkMaxLen = 200;
        public const int GUIDMaxLen = 200;
        public const int URLMaxLen = 500;
        public const int MediaTypeMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNewsItem = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNewsItem; }
        public override void SetValue(SqlString val) { }

        public TblNewsItem() { }
        public TblNewsItem(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNewsItem(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNewsItem = rd.SafeGetInt32("IDNewsItem");
            _IDNewsChannel = rd.SafeGetInt32("IDNewsChannel");
            _Title = rd.SafeGetString("Title");
            _Summary = rd.SafeGetString("Summary");
            _Link = rd.SafeGetString("Link");
            _GUID = rd.SafeGetString("GUID");
            _URL = rd.SafeGetString("URL");
            _Published = rd.SafeGetDateTime("Published");
            _MediaType = rd.SafeGetString("MediaType");
            _MediaLength = rd.SafeGetInt32("MediaLength");
            _IsProcessing = rd.SafeGetBoolean("IsProcessing");
            _CanDrop = rd.SafeGetBoolean("CanDrop");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsItem);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsItem);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNewsItem);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDNewsChannel", _IDNewsChannel);
            cmd.Parameters.AddWithValue("@Title", _Title);
            cmd.Parameters.AddWithValue("@Summary", _Summary);
            cmd.Parameters.AddWithValue("@Link", _Link);
            cmd.Parameters.AddWithValue("@GUID", _GUID);
            cmd.Parameters.AddWithValue("@URL", _URL);
            cmd.Parameters.AddWithValue("@Published", _Published);
            cmd.Parameters.AddWithValue("@MediaType", _MediaType);
            cmd.Parameters.AddWithValue("@MediaLength", _MediaLength);
            cmd.Parameters.AddWithValue("@IsProcessing", _IsProcessing);
            cmd.Parameters.AddWithValue("@CanDrop", _CanDrop);
        }

        #endregion
        #region accessors
        public SqlInt32 IDNewsItem
        {
            get { return _IDNewsItem; }
            private set
            {
                _IDNewsItem = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDNewsChannel
        {
            get { return _IDNewsChannel; }
            set
            {
                _IDNewsChannel = value;
                IsDirty = true;
            }
        }
        public SqlString Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                IsDirty = true;
            }
        }
        public SqlString Summary
        {
            get { return _Summary; }
            set
            {
                _Summary = value;
                IsDirty = true;
            }
        }
        public SqlString Link
        {
            get { return _Link; }
            set
            {
                _Link = value;
                IsDirty = true;
            }
        }
        public SqlString GUID
        {
            get { return _GUID; }
            set
            {
                _GUID = value;
                IsDirty = true;
            }
        }
        public SqlString URL
        {
            get { return _URL; }
            set
            {
                _URL = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Published
        {
            get { return _Published; }
            set
            {
                _Published = value;
                IsDirty = true;
            }
        }
        public SqlString MediaType
        {
            get { return _MediaType; }
            set
            {
                _MediaType = value;
                IsDirty = true;
            }
        }
        public SqlInt32 MediaLength
        {
            get { return _MediaLength; }
            set
            {
                _MediaLength = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsProcessing
        {
            get { return _IsProcessing; }
            set
            {
                _IsProcessing = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanDrop
        {
            get { return _CanDrop; }
            set
            {
                _CanDrop = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNote : CTable
    {
        private SqlInt64 _IDNote; //IDENTITY 
        private SqlString _Note;  //4000
        private SqlInt32 _Length;
        private SqlString _MD5Hash;  //50
        private SqlDateTime _Updated;
        private SqlBoolean _IsManagedByDevice;
        public const int NoteMaxLen = 4000;
        public const int MD5HashMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNote = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNote; }
        public override void SetValue(SqlString val) { _Note = val; }

        public TblNote() { }
        public TblNote(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNote(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNote = rd.SafeGetInt64("IDNote");
            _Note = rd.SafeGetString("Note");
            _Length = rd.SafeGetInt32("Length");
            _MD5Hash = rd.SafeGetString("MD5Hash");
            _Updated = rd.SafeGetDateTime("Updated");
            _IsManagedByDevice = rd.SafeGetBoolean("IsManagedByDevice");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNote);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNote);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNote);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Note", _Note);
            cmd.Parameters.AddWithValue("@Length", _Length);
            cmd.Parameters.AddWithValue("@MD5Hash", _MD5Hash);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
            cmd.Parameters.AddWithValue("@IsManagedByDevice", _IsManagedByDevice);
        }

        #endregion
        #region accessors
        public SqlInt64 IDNote
        {
            get { return _IDNote; }
            private set
            {
                _IDNote = value;
                IsDirty = true;
            }
        }
        public SqlString Note
        {
            get { return _Note; }
            set
            {
                _Note = value;
                IsDirty = true;
            }
        }
        public SqlInt32 Length
        {
            get { return _Length; }
            set
            {
                _Length = value;
                IsDirty = true;
            }
        }
        public SqlString MD5Hash
        {
            get { return _MD5Hash; }
            set
            {
                _MD5Hash = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsManagedByDevice
        {
            get { return _IsManagedByDevice; }
            set
            {
                _IsManagedByDevice = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNotification : CTable
    {
        private SqlInt64 _IDNotification; //IDENTITY 
        private SqlInt32 _TYNotificationChannel;
        private SqlInt32 _TYNotification;
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDGroup;
        private SqlInt64 _IDMedia;
        private SqlString _Title;  //200
        private SqlString _Body;  //4000
        private SqlString _Description;  //500
        private SqlDateTime _Updated;
        public const int TitleMaxLen = 200;
        public const int BodyMaxLen = 4000;
        public const int DescriptionMaxLen = 500;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNotification = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNotification; }
        public override void SetValue(SqlString val) { }

        public TblNotification() { }
        public TblNotification(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNotification(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNotification = rd.SafeGetInt64("IDNotification");
            _TYNotificationChannel = rd.SafeGetInt32("TYNotificationChannel");
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _Title = rd.SafeGetString("Title");
            _Body = rd.SafeGetString("Body");
            _Description = rd.SafeGetString("Description");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotification);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotification);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotification);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYNotificationChannel", _TYNotificationChannel);
            cmd.Parameters.AddWithValue("@TYNotification", _TYNotification);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@Title", _Title);
            cmd.Parameters.AddWithValue("@Body", _Body);
            cmd.Parameters.AddWithValue("@Description", _Description);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt64 IDNotification
        {
            get { return _IDNotification; }
            private set
            {
                _IDNotification = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotificationChannel
        {
            get { return _TYNotificationChannel; }
            set
            {
                _TYNotificationChannel = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlString Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                IsDirty = true;
            }
        }
        public SqlString Body
        {
            get { return _Body; }
            set
            {
                _Body = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNotificationGateway : CTable
    {
        private SqlInt32 _IDNotificationGateway; //IDENTITY 
        private SqlString _NotificationGatewayType;  //20
        private SqlInt64 _IDPhone;
        private SqlInt64 _IDEmail;
        private SqlInt64 _IDRegion;
        private SqlInt32 _TYNotificationVendor;
        private SqlByte _Precedence;
        public const int NotificationGatewayTypeMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNotificationGateway = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNotificationGateway; }
        public override void SetValue(SqlString val) { }

        public TblNotificationGateway() { }
        public TblNotificationGateway(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNotificationGateway(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNotificationGateway = rd.SafeGetInt32("IDNotificationGateway");
            _NotificationGatewayType = rd.SafeGetString("NotificationGatewayType");
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _IDRegion = rd.SafeGetInt64("IDRegion");
            _TYNotificationVendor = rd.SafeGetInt32("TYNotificationVendor");
            _Precedence = rd.SafeGetByte("Precedence");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationGateway);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationGateway);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationGateway);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@NotificationGatewayType", _NotificationGatewayType);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@IDRegion", _IDRegion);
            cmd.Parameters.AddWithValue("@TYNotificationVendor", _TYNotificationVendor);
            cmd.Parameters.AddWithValue("@Precedence", _Precedence);
        }

        #endregion
        #region accessors
        public SqlInt32 IDNotificationGateway
        {
            get { return _IDNotificationGateway; }
            private set
            {
                _IDNotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlString NotificationGatewayType
        {
            get { return _NotificationGatewayType; }
            set
            {
                _NotificationGatewayType = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDRegion
        {
            get { return _IDRegion; }
            set
            {
                _IDRegion = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotificationVendor
        {
            get { return _TYNotificationVendor; }
            set
            {
                _TYNotificationVendor = value;
                IsDirty = true;
            }
        }
        public SqlByte Precedence
        {
            get { return _Precedence; }
            set
            {
                _Precedence = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblNotificationQueue : CTable
    {
        private SqlInt64 _IDNotificationQueue; //IDENTITY 
        private SqlInt32 _TYNotificationChannel;
        private SqlInt32 _TYNotification;
        private SqlInt64 _IDNotification;
        private SqlInt64 _IDProfile_Sender;
        private SqlInt64 _IDProfile_Receiver;
        private SqlInt64 _ReceiverImportCacheID;
        private SqlInt64 _IDGroup;
        private SqlInt64 _IDEmail;
        private SqlInt64 _IDPhone;
        private SqlInt64 _IDEmail_Sender;
        private SqlInt64 _IDPhone_Sender;
        private SqlDateTime _Created;
        private SqlDateTime _Sent;
        private SqlInt32 _RetryCount;
        private SqlInt32 _DeliveryStatus;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDNotificationQueue = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDNotificationQueue; }
        public override void SetValue(SqlString val) { }

        public TblNotificationQueue() { }
        public TblNotificationQueue(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblNotificationQueue(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDNotificationQueue = rd.SafeGetInt64("IDNotificationQueue");
            _TYNotificationChannel = rd.SafeGetInt32("TYNotificationChannel");
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _IDNotification = rd.SafeGetInt64("IDNotification");
            _IDProfile_Sender = rd.SafeGetInt64("IDProfile_Sender");
            _IDProfile_Receiver = rd.SafeGetInt64("IDProfile_Receiver");
            _ReceiverImportCacheID = rd.SafeGetInt64("ReceiverImportCacheID");
            _IDGroup = rd.SafeGetInt64("IDGroup");
            _IDEmail = rd.SafeGetInt64("IDEmail");
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _IDEmail_Sender = rd.SafeGetInt64("IDEmail_Sender");
            _IDPhone_Sender = rd.SafeGetInt64("IDPhone_Sender");
            _Created = rd.SafeGetDateTime("Created");
            _Sent = rd.SafeGetDateTime("Sent");
            _RetryCount = rd.SafeGetInt32("RetryCount");
            _DeliveryStatus = rd.SafeGetInt32("DeliveryStatus");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationQueue);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationQueue);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDNotificationQueue);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYNotificationChannel", _TYNotificationChannel);
            cmd.Parameters.AddWithValue("@TYNotification", _TYNotification);
            cmd.Parameters.AddWithValue("@IDNotification", _IDNotification);
            cmd.Parameters.AddWithValue("@IDProfile_Sender", _IDProfile_Sender);
            cmd.Parameters.AddWithValue("@IDProfile_Receiver", _IDProfile_Receiver);
            cmd.Parameters.AddWithValue("@ReceiverImportCacheID", _ReceiverImportCacheID);
            cmd.Parameters.AddWithValue("@IDGroup", _IDGroup);
            cmd.Parameters.AddWithValue("@IDEmail", _IDEmail);
            cmd.Parameters.AddWithValue("@IDPhone", _IDPhone);
            cmd.Parameters.AddWithValue("@IDEmail_Sender", _IDEmail_Sender);
            cmd.Parameters.AddWithValue("@IDPhone_Sender", _IDPhone_Sender);
            cmd.Parameters.AddWithValue("@Created", _Created);
            cmd.Parameters.AddWithValue("@Sent", _Sent);
            cmd.Parameters.AddWithValue("@RetryCount", _RetryCount);
            cmd.Parameters.AddWithValue("@DeliveryStatus", _DeliveryStatus);
        }

        #endregion
        #region accessors
        public SqlInt64 IDNotificationQueue
        {
            get { return _IDNotificationQueue; }
            private set
            {
                _IDNotificationQueue = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotificationChannel
        {
            get { return _TYNotificationChannel; }
            set
            {
                _TYNotificationChannel = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDNotification
        {
            get { return _IDNotification; }
            set
            {
                _IDNotification = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_Sender
        {
            get { return _IDProfile_Sender; }
            set
            {
                _IDProfile_Sender = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_Receiver
        {
            get { return _IDProfile_Receiver; }
            set
            {
                _IDProfile_Receiver = value;
                IsDirty = true;
            }
        }
        public SqlInt64 ReceiverImportCacheID
        {
            get { return _ReceiverImportCacheID; }
            set
            {
                _ReceiverImportCacheID = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDGroup
        {
            get { return _IDGroup; }
            set
            {
                _IDGroup = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail
        {
            get { return _IDEmail; }
            set
            {
                _IDEmail = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail_Sender
        {
            get { return _IDEmail_Sender; }
            set
            {
                _IDEmail_Sender = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Sender
        {
            get { return _IDPhone_Sender; }
            set
            {
                _IDPhone_Sender = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Created
        {
            get { return _Created; }
            set
            {
                _Created = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Sent
        {
            get { return _Sent; }
            set
            {
                _Sent = value;
                IsDirty = true;
            }
        }
        public SqlInt32 RetryCount
        {
            get { return _RetryCount; }
            set
            {
                _RetryCount = value;
                IsDirty = true;
            }
        }
        public SqlInt32 DeliveryStatus
        {
            get { return _DeliveryStatus; }
            set
            {
                _DeliveryStatus = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblOffer : CTable
    {
        private SqlInt64 _IDOffer; //IDENTITY 
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDMerchant;
        private SqlString _OfferKey;  //50
        private SqlString _Merchant;  //30
        private SqlString _Brief;  //50
        private SqlString _Description;  //300
        private SqlString _Terms;  //120
        private SqlString _LocationDisplay;  //50
        private SqlString _Options;  //150
        private SqlDateTime _RedeemBy;
        private SqlDateTime _Started;
        private SqlDateTime _Ended;
        private SqlMoney _Price;
        private SqlMoney _FaceValue;
        private SqlMoney _TransactionFee;
        private SqlInt32 _MaxAvailable;
        private SqlInt32 _SoldCount;
        private SqlInt32 _MaxPerCustomer;
        private SqlString _MerchantSKU;  //50
        private SqlString _ShippingTerms;  //50
        private SqlBoolean _ChargeTax;
        private SqlBoolean _IsRemoved;
        public const int OfferKeyMaxLen = 50;
        public const int MerchantMaxLen = 30;
        public const int BriefMaxLen = 50;
        public const int DescriptionMaxLen = 300;
        public const int TermsMaxLen = 120;
        public const int LocationDisplayMaxLen = 50;
        public const int OptionsMaxLen = 150;
        public const int MerchantSKUMaxLen = 50;
        public const int ShippingTermsMaxLen = 50;
        private AssocOffer_GroupAssoc _AssocOffer_Group;
        private AssocOffer_Group_InterestAttributeAssoc _AssocOffer_Group_InterestAttribute;
        private AssocOffer_MetroAreaAssoc _AssocOffer_MetroArea;
        private LnkOffer_GroupAssoc _LnkOffer_Group;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDOffer = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDOffer; }
        public override void SetValue(SqlString val) { }

        public TblOffer() { }
        public TblOffer(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblOffer(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocOffer_Group = new AssocOffer_GroupAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocOffer_Group, _AssocOffer_Group);
            _AssocOffer_Group_InterestAttribute = new AssocOffer_Group_InterestAttributeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocOffer_Group_InterestAttribute, _AssocOffer_Group_InterestAttribute);
            _AssocOffer_MetroArea = new AssocOffer_MetroAreaAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocOffer_MetroArea, _AssocOffer_MetroArea);
            _LnkOffer_Group = new LnkOffer_GroupAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkOffer_Group, _LnkOffer_Group);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDMerchant = rd.SafeGetInt64("IDMerchant");
            _OfferKey = rd.SafeGetString("OfferKey");
            _Merchant = rd.SafeGetString("Merchant");
            _Brief = rd.SafeGetString("Brief");
            _Description = rd.SafeGetString("Description");
            _Terms = rd.SafeGetString("Terms");
            _LocationDisplay = rd.SafeGetString("LocationDisplay");
            _Options = rd.SafeGetString("Options");
            _RedeemBy = rd.SafeGetDateTime("RedeemBy");
            _Started = rd.SafeGetDateTime("Started");
            _Ended = rd.SafeGetDateTime("Ended");
            _Price = rd.SafeGetMoney("Price");
            _FaceValue = rd.SafeGetMoney("FaceValue");
            _TransactionFee = rd.SafeGetMoney("TransactionFee");
            _MaxAvailable = rd.SafeGetInt32("MaxAvailable");
            _SoldCount = rd.SafeGetInt32("SoldCount");
            _MaxPerCustomer = rd.SafeGetInt32("MaxPerCustomer");
            _MerchantSKU = rd.SafeGetString("MerchantSKU");
            _ShippingTerms = rd.SafeGetString("ShippingTerms");
            _ChargeTax = rd.SafeGetBoolean("ChargeTax");
            _IsRemoved = rd.SafeGetBoolean("IsRemoved");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDOffer);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDMerchant", _IDMerchant);
            cmd.Parameters.AddWithValue("@OfferKey", _OfferKey);
            cmd.Parameters.AddWithValue("@Merchant", _Merchant);
            cmd.Parameters.AddWithValue("@Brief", _Brief);
            cmd.Parameters.AddWithValue("@Description", _Description);
            cmd.Parameters.AddWithValue("@Terms", _Terms);
            cmd.Parameters.AddWithValue("@LocationDisplay", _LocationDisplay);
            cmd.Parameters.AddWithValue("@Options", _Options);
            cmd.Parameters.AddWithValue("@RedeemBy", _RedeemBy);
            cmd.Parameters.AddWithValue("@Started", _Started);
            cmd.Parameters.AddWithValue("@Ended", _Ended);
            cmd.Parameters.AddWithValue("@Price", _Price);
            cmd.Parameters.AddWithValue("@FaceValue", _FaceValue);
            cmd.Parameters.AddWithValue("@TransactionFee", _TransactionFee);
            cmd.Parameters.AddWithValue("@MaxAvailable", _MaxAvailable);
            cmd.Parameters.AddWithValue("@SoldCount", _SoldCount);
            cmd.Parameters.AddWithValue("@MaxPerCustomer", _MaxPerCustomer);
            cmd.Parameters.AddWithValue("@MerchantSKU", _MerchantSKU);
            cmd.Parameters.AddWithValue("@ShippingTerms", _ShippingTerms);
            cmd.Parameters.AddWithValue("@ChargeTax", _ChargeTax);
            cmd.Parameters.AddWithValue("@IsRemoved", _IsRemoved);
        }

        #endregion
        #region accessors
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            private set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMerchant
        {
            get { return _IDMerchant; }
            set
            {
                _IDMerchant = value;
                IsDirty = true;
            }
        }
        public SqlString OfferKey
        {
            get { return _OfferKey; }
            set
            {
                _OfferKey = value;
                IsDirty = true;
            }
        }
        public SqlString Merchant
        {
            get { return _Merchant; }
            set
            {
                _Merchant = value;
                IsDirty = true;
            }
        }
        public SqlString Brief
        {
            get { return _Brief; }
            set
            {
                _Brief = value;
                IsDirty = true;
            }
        }
        public SqlString Description
        {
            get { return _Description; }
            set
            {
                _Description = value;
                IsDirty = true;
            }
        }
        public SqlString Terms
        {
            get { return _Terms; }
            set
            {
                _Terms = value;
                IsDirty = true;
            }
        }
        public SqlString LocationDisplay
        {
            get { return _LocationDisplay; }
            set
            {
                _LocationDisplay = value;
                IsDirty = true;
            }
        }
        public SqlString Options
        {
            get { return _Options; }
            set
            {
                _Options = value;
                IsDirty = true;
            }
        }
        public SqlDateTime RedeemBy
        {
            get { return _RedeemBy; }
            set
            {
                _RedeemBy = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Started
        {
            get { return _Started; }
            set
            {
                _Started = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Ended
        {
            get { return _Ended; }
            set
            {
                _Ended = value;
                IsDirty = true;
            }
        }
        public SqlMoney Price
        {
            get { return _Price; }
            set
            {
                _Price = value;
                IsDirty = true;
            }
        }
        public SqlMoney FaceValue
        {
            get { return _FaceValue; }
            set
            {
                _FaceValue = value;
                IsDirty = true;
            }
        }
        public SqlMoney TransactionFee
        {
            get { return _TransactionFee; }
            set
            {
                _TransactionFee = value;
                IsDirty = true;
            }
        }
        public SqlInt32 MaxAvailable
        {
            get { return _MaxAvailable; }
            set
            {
                _MaxAvailable = value;
                IsDirty = true;
            }
        }
        public SqlInt32 SoldCount
        {
            get { return _SoldCount; }
            set
            {
                _SoldCount = value;
                IsDirty = true;
            }
        }
        public SqlInt32 MaxPerCustomer
        {
            get { return _MaxPerCustomer; }
            set
            {
                _MaxPerCustomer = value;
                IsDirty = true;
            }
        }
        public SqlString MerchantSKU
        {
            get { return _MerchantSKU; }
            set
            {
                _MerchantSKU = value;
                IsDirty = true;
            }
        }
        public SqlString ShippingTerms
        {
            get { return _ShippingTerms; }
            set
            {
                _ShippingTerms = value;
                IsDirty = true;
            }
        }
        public SqlBoolean ChargeTax
        {
            get { return _ChargeTax; }
            set
            {
                _ChargeTax = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsRemoved
        {
            get { return _IsRemoved; }
            set
            {
                _IsRemoved = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblPerson : CTable
    {
        private SqlInt64 _IDPerson; //IDENTITY 
        private SqlString _Person;  //50
        public const int PersonMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDPerson = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDPerson; }
        public override void SetValue(SqlString val) { _Person = val; }

        public TblPerson() { }
        public TblPerson(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblPerson(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDPerson = rd.SafeGetInt64("IDPerson");
            _Person = rd.SafeGetString("Person");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPerson);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPerson);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPerson);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Person", _Person);
        }

        #endregion
        #region accessors
        public SqlInt64 IDPerson
        {
            get { return _IDPerson; }
            private set
            {
                _IDPerson = value;
                IsDirty = true;
            }
        }
        public SqlString Person
        {
            get { return _Person; }
            set
            {
                _Person = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblPersonKind : CTable
    {
        private SqlInt32 _IDPersonKind; //IDENTITY 
        private SqlString _PersonKind;  //50
        private SqlInt64 _IDPerson;
        public const int PersonKindMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDPersonKind = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDPersonKind; }
        public override void SetValue(SqlString val) { _PersonKind = val; }

        public TblPersonKind() { }
        public TblPersonKind(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblPersonKind(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDPersonKind = rd.SafeGetInt32("IDPersonKind");
            _PersonKind = rd.SafeGetString("PersonKind");
            _IDPerson = rd.SafeGetInt64("IDPerson");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPersonKind);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPersonKind);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPersonKind);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@PersonKind", _PersonKind);
            cmd.Parameters.AddWithValue("@IDPerson", _IDPerson);
        }

        #endregion
        #region accessors
        public SqlInt32 IDPersonKind
        {
            get { return _IDPersonKind; }
            private set
            {
                _IDPersonKind = value;
                IsDirty = true;
            }
        }
        public SqlString PersonKind
        {
            get { return _PersonKind; }
            set
            {
                _PersonKind = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPerson
        {
            get { return _IDPerson; }
            set
            {
                _IDPerson = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblPhone : CTable
    {
        private SqlInt64 _IDPhone; //IDENTITY 
        private SqlString _Phone;  //20
        private SqlString _Country;  //50
        public const int PhoneMaxLen = 20;
        public const int CountryMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDPhone = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDPhone; }
        public override void SetValue(SqlString val) { _Phone = val; }

        public TblPhone() { }
        public TblPhone(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblPhone(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDPhone = rd.SafeGetInt64("IDPhone");
            _Phone = rd.SafeGetString("Phone");
            _Country = rd.SafeGetString("Country");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPhone);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPhone);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDPhone);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Phone", _Phone);
            cmd.Parameters.AddWithValue("@Country", _Country);
        }

        #endregion
        #region accessors
        public SqlInt64 IDPhone
        {
            get { return _IDPhone; }
            private set
            {
                _IDPhone = value;
                IsDirty = true;
            }
        }
        public SqlString Phone
        {
            get { return _Phone; }
            set
            {
                _Phone = value;
                IsDirty = true;
            }
        }
        public SqlString Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblPrioritySearchQueue : CTable
    {
        private SqlInt64 _IDProfile;
        private SqlInt32 _IDDataSource;
        private SqlBoolean _CanSearch;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public TblPrioritySearchQueue() { }
        public TblPrioritySearchQueue(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblPrioritySearchQueue(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDDataSource = rd.SafeGetInt32("IDDataSource");
            _CanSearch = rd.SafeGetBoolean("CanSearch");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDDataSource", _IDDataSource);
            cmd.Parameters.AddWithValue("@CanSearch", _CanSearch);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDDataSource
        {
            get { return _IDDataSource; }
            set
            {
                _IDDataSource = value;
                IsDirty = true;
            }
        }
        public SqlBoolean CanSearch
        {
            get { return _CanSearch; }
            set
            {
                _CanSearch = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblProfile : CTable
    {
        private SqlInt64 _IDProfile; //IDENTITY 
        private SqlInt64 _IDProfile_2;
        private SqlInt64 _IDMedia;
        private SqlInt64 _IDPhone_Primary;
        private SqlInt64 _IDPhone_Preferred;
        private SqlInt64 _IDEmail_Preferred;
        private SqlInt32 _TYDataAccess;
        private SqlString _Password;  //20
        private SqlBoolean _UseCommSMS;
        private SqlBoolean _UseCommEmail;
        private SqlBoolean _UseCommIVR;
        private SqlBoolean _IsConfirmed;
        private SqlByte _AdminNotification;
        private SqlInt64 _DataManagementTypeID;
        private SqlInt64 _ArchiveID;
        private SqlByte _QueueDigest;
        private SqlDateTime _Updated;
        private SqlDateTime _LastNotification;
        private SqlDateTime _LastUpdateSAQ;
        private SqlDecimal _SAQ;
        public const int PasswordMaxLen = 20;
        private AssocProfile_ApplicationAssoc _AssocProfile_Application;
        private AssocProfile_ArchiveRecordAssoc _AssocProfile_ArchiveRecord;
        private AssocProfile_CompanyAssoc _AssocProfile_Company;
        private AssocProfile_ContactAssoc _AssocProfile_Contact;
        private AssocProfile_Contact_InterestAttributesAssoc _AssocProfile_Contact_InterestAttributes;
        private AssocProfile_EmailAssoc _AssocProfile_Email;
        private AssocProfile_GroupAssoc _AssocProfile_Group;
        private AssocProfile_InterestAttributeAssoc _AssocProfile_InterestAttribute;
        private AssocProfile_LocationAssoc _AssocProfile_Location;
        private AssocProfile_MediaAssoc _AssocProfile_Media;
        private AssocProfile_Media_RelationshipAssoc _AssocProfile_Media_Relationship;
        private AssocProfile_MerchantAssoc _AssocProfile_Merchant;
        private AssocProfile_MetroAreaAssoc _AssocProfile_MetroArea;
        private AssocProfile_NoteAssoc _AssocProfile_Note;
        private AssocProfile_PersonAssoc _AssocProfile_Person;
        private AssocProfile_PhoneAssoc _AssocProfile_Phone;
        private AssocProfile_PrefixAssoc _AssocProfile_Prefix;
        private AssocProfile_ProfileAssoc _AssocProfile_Profile;
        private AssocProfile_TitleAssoc _AssocProfile_Title;
        private AssocProfile_WebAddressAssoc _AssocProfile_WebAddress;
        private LnkProfile_ContactAssoc _LnkProfile_Contact;
        private LnkProfile_DataSource_PrioritySearchAssoc _LnkProfile_DataSource_PrioritySearch;
        private LnkProfile_Group_AdminAssoc _LnkProfile_Group_Admin;
        private LnkProfile_Media_HashAssoc _LnkProfile_Media_Hash;
        private LnkProfile_Profile_MergeReviewAssoc _LnkProfile_Profile_MergeReview;
        private OvrProfile_DataAccessAssoc _OvrProfile_DataAccess;
        private OvrProfile_DataManagementAssoc _OvrProfile_DataManagement;
        private OvrProfile_Profile_RelationshipTypeAssoc _OvrProfile_Profile_RelationshipType;
        private ReviewProfile_ApplicationAssoc _ReviewProfile_Application;
        private ReviewProfile_ProfileAssoc _ReviewProfile_Profile;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDProfile = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDProfile; }
        public override void SetValue(SqlString val) { }

        public TblProfile() { }
        public TblProfile(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblProfile(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocProfile_Application = new AssocProfile_ApplicationAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Application, _AssocProfile_Application);
            _AssocProfile_ArchiveRecord = new AssocProfile_ArchiveRecordAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_ArchiveRecord, _AssocProfile_ArchiveRecord);
            _AssocProfile_Company = new AssocProfile_CompanyAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Company, _AssocProfile_Company);
            _AssocProfile_Contact = new AssocProfile_ContactAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Contact, _AssocProfile_Contact);
            _AssocProfile_Contact_InterestAttributes = new AssocProfile_Contact_InterestAttributesAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Contact_InterestAttributes, _AssocProfile_Contact_InterestAttributes);
            _AssocProfile_Email = new AssocProfile_EmailAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Email, _AssocProfile_Email);
            _AssocProfile_Group = new AssocProfile_GroupAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Group, _AssocProfile_Group);
            _AssocProfile_InterestAttribute = new AssocProfile_InterestAttributeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_InterestAttribute, _AssocProfile_InterestAttribute);
            _AssocProfile_Location = new AssocProfile_LocationAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Location, _AssocProfile_Location);
            _AssocProfile_Media = new AssocProfile_MediaAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Media, _AssocProfile_Media);
            _AssocProfile_Media_Relationship = new AssocProfile_Media_RelationshipAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Media_Relationship, _AssocProfile_Media_Relationship);
            _AssocProfile_Merchant = new AssocProfile_MerchantAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Merchant, _AssocProfile_Merchant);
            _AssocProfile_MetroArea = new AssocProfile_MetroAreaAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_MetroArea, _AssocProfile_MetroArea);
            _AssocProfile_Note = new AssocProfile_NoteAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Note, _AssocProfile_Note);
            _AssocProfile_Person = new AssocProfile_PersonAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Person, _AssocProfile_Person);
            _AssocProfile_Phone = new AssocProfile_PhoneAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Phone, _AssocProfile_Phone);
            _AssocProfile_Prefix = new AssocProfile_PrefixAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Prefix, _AssocProfile_Prefix);
            _AssocProfile_Profile = new AssocProfile_ProfileAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Profile, _AssocProfile_Profile);
            _AssocProfile_Title = new AssocProfile_TitleAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_Title, _AssocProfile_Title);
            _AssocProfile_WebAddress = new AssocProfile_WebAddressAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocProfile_WebAddress, _AssocProfile_WebAddress);
            _LnkProfile_Contact = new LnkProfile_ContactAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkProfile_Contact, _LnkProfile_Contact);
            _LnkProfile_DataSource_PrioritySearch = new LnkProfile_DataSource_PrioritySearchAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkProfile_DataSource_PrioritySearch, _LnkProfile_DataSource_PrioritySearch);
            _LnkProfile_Group_Admin = new LnkProfile_Group_AdminAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkProfile_Group_Admin, _LnkProfile_Group_Admin);
            _LnkProfile_Media_Hash = new LnkProfile_Media_HashAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkProfile_Media_Hash, _LnkProfile_Media_Hash);
            _LnkProfile_Profile_MergeReview = new LnkProfile_Profile_MergeReviewAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TLnkProfile_Profile_MergeReview, _LnkProfile_Profile_MergeReview);
            _OvrProfile_DataAccess = new OvrProfile_DataAccessAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TOvrProfile_DataAccess, _OvrProfile_DataAccess);
            _OvrProfile_DataManagement = new OvrProfile_DataManagementAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TOvrProfile_DataManagement, _OvrProfile_DataManagement);
            _OvrProfile_Profile_RelationshipType = new OvrProfile_Profile_RelationshipTypeAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TOvrProfile_Profile_RelationshipType, _OvrProfile_Profile_RelationshipType);
            _ReviewProfile_Application = new ReviewProfile_ApplicationAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TReviewProfile_Application, _ReviewProfile_Application);
            _ReviewProfile_Profile = new ReviewProfile_ProfileAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TReviewProfile_Profile, _ReviewProfile_Profile);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDProfile_2 = rd.SafeGetInt64("IDProfile_2");
            _IDMedia = rd.SafeGetInt64("IDMedia");
            _IDPhone_Primary = rd.SafeGetInt64("IDPhone_Primary");
            _IDPhone_Preferred = rd.SafeGetInt64("IDPhone_Preferred");
            _IDEmail_Preferred = rd.SafeGetInt64("IDEmail_Preferred");
            _TYDataAccess = rd.SafeGetInt32("TYDataAccess");
            _Password = rd.SafeGetString("Password");
            _UseCommSMS = rd.SafeGetBoolean("UseCommSMS");
            _UseCommEmail = rd.SafeGetBoolean("UseCommEmail");
            _UseCommIVR = rd.SafeGetBoolean("UseCommIVR");
            _IsConfirmed = rd.SafeGetBoolean("IsConfirmed");
            _AdminNotification = rd.SafeGetByte("AdminNotification");
            _DataManagementTypeID = rd.SafeGetInt64("DataManagementTypeID");
            _ArchiveID = rd.SafeGetInt64("ArchiveID");
            _QueueDigest = rd.SafeGetByte("QueueDigest");
            _Updated = rd.SafeGetDateTime("Updated");
            _LastNotification = rd.SafeGetDateTime("LastNotification");
            _LastUpdateSAQ = rd.SafeGetDateTime("LastUpdateSAQ");
            _SAQ = rd.SafeGetDecimal("SAQ");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDProfile);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile_2", _IDProfile_2);
            cmd.Parameters.AddWithValue("@IDMedia", _IDMedia);
            cmd.Parameters.AddWithValue("@IDPhone_Primary", _IDPhone_Primary);
            cmd.Parameters.AddWithValue("@IDPhone_Preferred", _IDPhone_Preferred);
            cmd.Parameters.AddWithValue("@IDEmail_Preferred", _IDEmail_Preferred);
            cmd.Parameters.AddWithValue("@TYDataAccess", _TYDataAccess);
            cmd.Parameters.AddWithValue("@Password", _Password);
            cmd.Parameters.AddWithValue("@UseCommSMS", _UseCommSMS);
            cmd.Parameters.AddWithValue("@UseCommEmail", _UseCommEmail);
            cmd.Parameters.AddWithValue("@UseCommIVR", _UseCommIVR);
            cmd.Parameters.AddWithValue("@IsConfirmed", _IsConfirmed);
            cmd.Parameters.AddWithValue("@AdminNotification", _AdminNotification);
            cmd.Parameters.AddWithValue("@DataManagementTypeID", _DataManagementTypeID);
            cmd.Parameters.AddWithValue("@ArchiveID", _ArchiveID);
            cmd.Parameters.AddWithValue("@QueueDigest", _QueueDigest);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
            cmd.Parameters.AddWithValue("@LastNotification", _LastNotification);
            cmd.Parameters.AddWithValue("@LastUpdateSAQ", _LastUpdateSAQ);
            cmd.Parameters.AddWithValue("@SAQ", _SAQ);
        }

        #endregion
        #region accessors
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            private set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile_2
        {
            get { return _IDProfile_2; }
            set
            {
                _IDProfile_2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMedia
        {
            get { return _IDMedia; }
            set
            {
                _IDMedia = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Primary
        {
            get { return _IDPhone_Primary; }
            set
            {
                _IDPhone_Primary = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDPhone_Preferred
        {
            get { return _IDPhone_Preferred; }
            set
            {
                _IDPhone_Preferred = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDEmail_Preferred
        {
            get { return _IDEmail_Preferred; }
            set
            {
                _IDEmail_Preferred = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYDataAccess
        {
            get { return _TYDataAccess; }
            set
            {
                _TYDataAccess = value;
                IsDirty = true;
            }
        }
        public SqlString Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                IsDirty = true;
            }
        }
        public SqlBoolean UseCommSMS
        {
            get { return _UseCommSMS; }
            set
            {
                _UseCommSMS = value;
                IsDirty = true;
            }
        }
        public SqlBoolean UseCommEmail
        {
            get { return _UseCommEmail; }
            set
            {
                _UseCommEmail = value;
                IsDirty = true;
            }
        }
        public SqlBoolean UseCommIVR
        {
            get { return _UseCommIVR; }
            set
            {
                _UseCommIVR = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsConfirmed
        {
            get { return _IsConfirmed; }
            set
            {
                _IsConfirmed = value;
                IsDirty = true;
            }
        }
        public SqlByte AdminNotification
        {
            get { return _AdminNotification; }
            set
            {
                _AdminNotification = value;
                IsDirty = true;
            }
        }
        public SqlInt64 DataManagementTypeID
        {
            get { return _DataManagementTypeID; }
            set
            {
                _DataManagementTypeID = value;
                IsDirty = true;
            }
        }
        public SqlInt64 ArchiveID
        {
            get { return _ArchiveID; }
            set
            {
                _ArchiveID = value;
                IsDirty = true;
            }
        }
        public SqlByte QueueDigest
        {
            get { return _QueueDigest; }
            set
            {
                _QueueDigest = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }
        public SqlDateTime LastNotification
        {
            get { return _LastNotification; }
            set
            {
                _LastNotification = value;
                IsDirty = true;
            }
        }
        public SqlDateTime LastUpdateSAQ
        {
            get { return _LastUpdateSAQ; }
            set
            {
                _LastUpdateSAQ = value;
                IsDirty = true;
            }
        }
        public SqlDecimal SAQ
        {
            get { return _SAQ; }
            set
            {
                _SAQ = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblRegion : CTable
    {
        private SqlInt64 _IDRegion; //IDENTITY 
        private SqlString _Region;  //120
        private SqlInt32 _IDCountry;
        private SqlString _Code;  //8
        private SqlString _ADM1Code;  //16
        public const int RegionMaxLen = 120;
        public const int CodeMaxLen = 8;
        public const int ADM1CodeMaxLen = 16;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDRegion = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDRegion; }
        public override void SetValue(SqlString val) { _Region = val; }

        public TblRegion() { }
        public TblRegion(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblRegion(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDRegion = rd.SafeGetInt64("IDRegion");
            _Region = rd.SafeGetString("Region");
            _IDCountry = rd.SafeGetInt32("IDCountry");
            _Code = rd.SafeGetString("Code");
            _ADM1Code = rd.SafeGetString("ADM1Code");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDRegion);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDRegion);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDRegion);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Region", _Region);
            cmd.Parameters.AddWithValue("@IDCountry", _IDCountry);
            cmd.Parameters.AddWithValue("@Code", _Code);
            cmd.Parameters.AddWithValue("@ADM1Code", _ADM1Code);
        }

        #endregion
        #region accessors
        public SqlInt64 IDRegion
        {
            get { return _IDRegion; }
            private set
            {
                _IDRegion = value;
                IsDirty = true;
            }
        }
        public SqlString Region
        {
            get { return _Region; }
            set
            {
                _Region = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDCountry
        {
            get { return _IDCountry; }
            set
            {
                _IDCountry = value;
                IsDirty = true;
            }
        }
        public SqlString Code
        {
            get { return _Code; }
            set
            {
                _Code = value;
                IsDirty = true;
            }
        }
        public SqlString ADM1Code
        {
            get { return _ADM1Code; }
            set
            {
                _ADM1Code = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblSearchThrottle : CTable
    {
        private SqlInt32 _IDSearchThrottle; //IDENTITY 
        private SqlInt32 _TYDataSource;
        private SqlInt32 _RequestLimit;
        private SqlInt32 _TimeSpan;
        private SqlBoolean _IsAvailable;
        private SqlInt64 _Counter;
        private SqlDateTime _Updated;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDSearchThrottle = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDSearchThrottle; }
        public override void SetValue(SqlString val) { }

        public TblSearchThrottle() { }
        public TblSearchThrottle(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblSearchThrottle(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDSearchThrottle = rd.SafeGetInt32("IDSearchThrottle");
            _TYDataSource = rd.SafeGetInt32("TYDataSource");
            _RequestLimit = rd.SafeGetInt32("RequestLimit");
            _TimeSpan = rd.SafeGetInt32("TimeSpan");
            _IsAvailable = rd.SafeGetBoolean("IsAvailable");
            _Counter = rd.SafeGetInt64("Counter");
            _Updated = rd.SafeGetDateTime("Updated");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSearchThrottle);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSearchThrottle);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSearchThrottle);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TYDataSource", _TYDataSource);
            cmd.Parameters.AddWithValue("@RequestLimit", _RequestLimit);
            cmd.Parameters.AddWithValue("@TimeSpan", _TimeSpan);
            cmd.Parameters.AddWithValue("@IsAvailable", _IsAvailable);
            cmd.Parameters.AddWithValue("@Counter", _Counter);
            cmd.Parameters.AddWithValue("@Updated", _Updated);
        }

        #endregion
        #region accessors
        public SqlInt32 IDSearchThrottle
        {
            get { return _IDSearchThrottle; }
            private set
            {
                _IDSearchThrottle = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYDataSource
        {
            get { return _TYDataSource; }
            set
            {
                _TYDataSource = value;
                IsDirty = true;
            }
        }
        public SqlInt32 RequestLimit
        {
            get { return _RequestLimit; }
            set
            {
                _RequestLimit = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TimeSpan
        {
            get { return _TimeSpan; }
            set
            {
                _TimeSpan = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsAvailable
        {
            get { return _IsAvailable; }
            set
            {
                _IsAvailable = value;
                IsDirty = true;
            }
        }
        public SqlInt64 Counter
        {
            get { return _Counter; }
            set
            {
                _Counter = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Updated
        {
            get { return _Updated; }
            set
            {
                _Updated = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblSmtpAddress : CTable
    {
        private SqlInt32 _IDSmtpAddress; //IDENTITY 
        private SqlString _SmtpAddress;  //50
        private SqlString _Carrier;  //50
        private SqlBoolean _ShouldPoll;
        public const int SmtpAddressMaxLen = 50;
        public const int CarrierMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDSmtpAddress = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDSmtpAddress; }
        public override void SetValue(SqlString val) { _SmtpAddress = val; }

        public TblSmtpAddress() { }
        public TblSmtpAddress(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblSmtpAddress(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDSmtpAddress = rd.SafeGetInt32("IDSmtpAddress");
            _SmtpAddress = rd.SafeGetString("SmtpAddress");
            _Carrier = rd.SafeGetString("Carrier");
            _ShouldPoll = rd.SafeGetBoolean("ShouldPoll");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSmtpAddress);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSmtpAddress);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSmtpAddress);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@SmtpAddress", _SmtpAddress);
            cmd.Parameters.AddWithValue("@Carrier", _Carrier);
            cmd.Parameters.AddWithValue("@ShouldPoll", _ShouldPoll);
        }

        #endregion
        #region accessors
        public SqlInt32 IDSmtpAddress
        {
            get { return _IDSmtpAddress; }
            private set
            {
                _IDSmtpAddress = value;
                IsDirty = true;
            }
        }
        public SqlString SmtpAddress
        {
            get { return _SmtpAddress; }
            set
            {
                _SmtpAddress = value;
                IsDirty = true;
            }
        }
        public SqlString Carrier
        {
            get { return _Carrier; }
            set
            {
                _Carrier = value;
                IsDirty = true;
            }
        }
        public SqlBoolean ShouldPoll
        {
            get { return _ShouldPoll; }
            set
            {
                _ShouldPoll = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblSubscriber : CTable
    {
        private SqlInt64 _IDSubscriber; //IDENTITY 
        private SqlInt64 _IDProfile;
        private SqlInt32 _STSubscriber;
        private SqlString _Password;  //50
        private SqlString _AccessKey;  //50
        public const int PasswordMaxLen = 50;
        public const int AccessKeyMaxLen = 50;
        private AssocSubscriber_DataSourceAssoc _AssocSubscriber_DataSource;
        ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _associations = new ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation>();
        public override ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> GetAssociations() { return _associations; }

        #region functions
        protected override void SetID(SqlInt64 id) { _IDSubscriber = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDSubscriber; }
        public override void SetValue(SqlString val) { }

        public TblSubscriber() { }
        public TblSubscriber(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblSubscriber(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
            _AssocSubscriber_DataSource = new AssocSubscriber_DataSourceAssoc(GetID());
            _associations.TryAdd(DataObjects.TableTypes.TAssocSubscriber_DataSource, _AssocSubscriber_DataSource);
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDSubscriber = rd.SafeGetInt64("IDSubscriber");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _STSubscriber = rd.SafeGetInt32("STSubscriber");
            _Password = rd.SafeGetString("Password");
            _AccessKey = rd.SafeGetString("AccessKey");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSubscriber);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSubscriber);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDSubscriber);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@STSubscriber", _STSubscriber);
            cmd.Parameters.AddWithValue("@Password", _Password);
            cmd.Parameters.AddWithValue("@AccessKey", _AccessKey);
        }

        #endregion
        #region accessors
        public SqlInt64 IDSubscriber
        {
            get { return _IDSubscriber; }
            private set
            {
                _IDSubscriber = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt32 STSubscriber
        {
            get { return _STSubscriber; }
            set
            {
                _STSubscriber = value;
                IsDirty = true;
            }
        }
        public SqlString Password
        {
            get { return _Password; }
            set
            {
                _Password = value;
                IsDirty = true;
            }
        }
        public SqlString AccessKey
        {
            get { return _AccessKey; }
            set
            {
                _AccessKey = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblTimeZone : CTable
    {
        private SqlInt32 _IDTimeZone; //IDENTITY 
        private SqlString _TimeZone;  //50
        private SqlDouble _GMTOffset;
        private SqlString _TimeZoneRegion;  //100
        public const int TimeZoneMaxLen = 50;
        public const int TimeZoneRegionMaxLen = 100;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDTimeZone = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDTimeZone; }
        public override void SetValue(SqlString val) { _TimeZone = val; }

        public TblTimeZone() { }
        public TblTimeZone(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblTimeZone(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDTimeZone = rd.SafeGetInt32("IDTimeZone");
            _TimeZone = rd.SafeGetString("TimeZone");
            _GMTOffset = rd.SafeGetDouble("GMTOffset");
            _TimeZoneRegion = rd.SafeGetString("TimeZoneRegion");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTimeZone);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTimeZone);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTimeZone);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@TimeZone", _TimeZone);
            cmd.Parameters.AddWithValue("@GMTOffset", _GMTOffset);
            cmd.Parameters.AddWithValue("@TimeZoneRegion", _TimeZoneRegion);
        }

        #endregion
        #region accessors
        public SqlInt32 IDTimeZone
        {
            get { return _IDTimeZone; }
            private set
            {
                _IDTimeZone = value;
                IsDirty = true;
            }
        }
        public SqlString TimeZone
        {
            get { return _TimeZone; }
            set
            {
                _TimeZone = value;
                IsDirty = true;
            }
        }
        public SqlDouble GMTOffset
        {
            get { return _GMTOffset; }
            set
            {
                _GMTOffset = value;
                IsDirty = true;
            }
        }
        public SqlString TimeZoneRegion
        {
            get { return _TimeZoneRegion; }
            set
            {
                _TimeZoneRegion = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblTransaction : CTable
    {
        private SqlInt64 _IDTransaction; //IDENTITY 
        private SqlInt64 _IDCreditCard;
        private SqlInt64 _IDProfile;
        private SqlInt64 _IDMerchant;
        private SqlInt64 _MerchantTransactionID;
        private SqlInt64 _IDOffer;
        private SqlInt32 _IDApplication;
        private SqlInt64 _IDLocationStreet;
        private SqlInt64 _IDLocationStreet2;
        private SqlInt64 _IDLocationCity;
        private SqlInt64 _IDLocationState;
        private SqlInt64 _IDLocationCountry;
        private SqlInt64 _IDLocationZipCode;
        private SqlInt32 _TransactionStatusID;
        private SqlInt64 _Quantity;
        private SqlMoney _AmountTotal;
        private SqlMoney _AmountShiping;
        private SqlMoney _AmountTax;
        private SqlString _ItemProperties;  //100
        private SqlString _TransactionKey;  //50
        private SqlBoolean _IsVoid;
        private SqlString _TextVoid;  //50
        private SqlDateTime _Recieved;
        private SqlDateTime _Processed;
        private SqlDateTime _Redeemed;
        private SqlString _ppTransactionID;  //50
        private SqlString _ppAckValue;  //50
        private SqlString _ppErrorCode;  //50
        private SqlString _ppLongMessage;  //200
        private SqlString _ppCvv2Match;  //10
        private SqlString _ppAvsCode;  //10
        private SqlMoney _ppAmount;
        private SqlString _CountryCode;  //5
        private SqlString _CurrencyCode;  //10
        private SqlString _Tax;  //50
        private SqlString _Shipping;  //50
        public const int ItemPropertiesMaxLen = 100;
        public const int TransactionKeyMaxLen = 50;
        public const int TextVoidMaxLen = 50;
        public const int ppTransactionIDMaxLen = 50;
        public const int ppAckValueMaxLen = 50;
        public const int ppErrorCodeMaxLen = 50;
        public const int ppLongMessageMaxLen = 200;
        public const int ppCvv2MatchMaxLen = 10;
        public const int ppAvsCodeMaxLen = 10;
        public const int CountryCodeMaxLen = 5;
        public const int CurrencyCodeMaxLen = 10;
        public const int TaxMaxLen = 50;
        public const int ShippingMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDTransaction = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDTransaction; }
        public override void SetValue(SqlString val) { }

        public TblTransaction() { }
        public TblTransaction(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblTransaction(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDTransaction = rd.SafeGetInt64("IDTransaction");
            _IDCreditCard = rd.SafeGetInt64("IDCreditCard");
            _IDProfile = rd.SafeGetInt64("IDProfile");
            _IDMerchant = rd.SafeGetInt64("IDMerchant");
            _MerchantTransactionID = rd.SafeGetInt64("MerchantTransactionID");
            _IDOffer = rd.SafeGetInt64("IDOffer");
            _IDApplication = rd.SafeGetInt32("IDApplication");
            _IDLocationStreet = rd.SafeGetInt64("IDLocationStreet");
            _IDLocationStreet2 = rd.SafeGetInt64("IDLocationStreet2");
            _IDLocationCity = rd.SafeGetInt64("IDLocationCity");
            _IDLocationState = rd.SafeGetInt64("IDLocationState");
            _IDLocationCountry = rd.SafeGetInt64("IDLocationCountry");
            _IDLocationZipCode = rd.SafeGetInt64("IDLocationZipCode");
            _TransactionStatusID = rd.SafeGetInt32("TransactionStatusID");
            _Quantity = rd.SafeGetInt64("Quantity");
            _AmountTotal = rd.SafeGetMoney("AmountTotal");
            _AmountShiping = rd.SafeGetMoney("AmountShiping");
            _AmountTax = rd.SafeGetMoney("AmountTax");
            _ItemProperties = rd.SafeGetString("ItemProperties");
            _TransactionKey = rd.SafeGetString("TransactionKey");
            _IsVoid = rd.SafeGetBoolean("IsVoid");
            _TextVoid = rd.SafeGetString("TextVoid");
            _Recieved = rd.SafeGetDateTime("Recieved");
            _Processed = rd.SafeGetDateTime("Processed");
            _Redeemed = rd.SafeGetDateTime("Redeemed");
            _ppTransactionID = rd.SafeGetString("ppTransactionID");
            _ppAckValue = rd.SafeGetString("ppAckValue");
            _ppErrorCode = rd.SafeGetString("ppErrorCode");
            _ppLongMessage = rd.SafeGetString("ppLongMessage");
            _ppCvv2Match = rd.SafeGetString("ppCvv2Match");
            _ppAvsCode = rd.SafeGetString("ppAvsCode");
            _ppAmount = rd.SafeGetMoney("ppAmount");
            _CountryCode = rd.SafeGetString("CountryCode");
            _CurrencyCode = rd.SafeGetString("CurrencyCode");
            _Tax = rd.SafeGetString("Tax");
            _Shipping = rd.SafeGetString("Shipping");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTransaction);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTransaction);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDTransaction);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@IDCreditCard", _IDCreditCard);
            cmd.Parameters.AddWithValue("@IDProfile", _IDProfile);
            cmd.Parameters.AddWithValue("@IDMerchant", _IDMerchant);
            cmd.Parameters.AddWithValue("@MerchantTransactionID", _MerchantTransactionID);
            cmd.Parameters.AddWithValue("@IDOffer", _IDOffer);
            cmd.Parameters.AddWithValue("@IDApplication", _IDApplication);
            cmd.Parameters.AddWithValue("@IDLocationStreet", _IDLocationStreet);
            cmd.Parameters.AddWithValue("@IDLocationStreet2", _IDLocationStreet2);
            cmd.Parameters.AddWithValue("@IDLocationCity", _IDLocationCity);
            cmd.Parameters.AddWithValue("@IDLocationState", _IDLocationState);
            cmd.Parameters.AddWithValue("@IDLocationCountry", _IDLocationCountry);
            cmd.Parameters.AddWithValue("@IDLocationZipCode", _IDLocationZipCode);
            cmd.Parameters.AddWithValue("@TransactionStatusID", _TransactionStatusID);
            cmd.Parameters.AddWithValue("@Quantity", _Quantity);
            cmd.Parameters.AddWithValue("@AmountTotal", _AmountTotal);
            cmd.Parameters.AddWithValue("@AmountShiping", _AmountShiping);
            cmd.Parameters.AddWithValue("@AmountTax", _AmountTax);
            cmd.Parameters.AddWithValue("@ItemProperties", _ItemProperties);
            cmd.Parameters.AddWithValue("@TransactionKey", _TransactionKey);
            cmd.Parameters.AddWithValue("@IsVoid", _IsVoid);
            cmd.Parameters.AddWithValue("@TextVoid", _TextVoid);
            cmd.Parameters.AddWithValue("@Recieved", _Recieved);
            cmd.Parameters.AddWithValue("@Processed", _Processed);
            cmd.Parameters.AddWithValue("@Redeemed", _Redeemed);
            cmd.Parameters.AddWithValue("@ppTransactionID", _ppTransactionID);
            cmd.Parameters.AddWithValue("@ppAckValue", _ppAckValue);
            cmd.Parameters.AddWithValue("@ppErrorCode", _ppErrorCode);
            cmd.Parameters.AddWithValue("@ppLongMessage", _ppLongMessage);
            cmd.Parameters.AddWithValue("@ppCvv2Match", _ppCvv2Match);
            cmd.Parameters.AddWithValue("@ppAvsCode", _ppAvsCode);
            cmd.Parameters.AddWithValue("@ppAmount", _ppAmount);
            cmd.Parameters.AddWithValue("@CountryCode", _CountryCode);
            cmd.Parameters.AddWithValue("@CurrencyCode", _CurrencyCode);
            cmd.Parameters.AddWithValue("@Tax", _Tax);
            cmd.Parameters.AddWithValue("@Shipping", _Shipping);
        }

        #endregion
        #region accessors
        public SqlInt64 IDTransaction
        {
            get { return _IDTransaction; }
            private set
            {
                _IDTransaction = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDCreditCard
        {
            get { return _IDCreditCard; }
            set
            {
                _IDCreditCard = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDProfile
        {
            get { return _IDProfile; }
            set
            {
                _IDProfile = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDMerchant
        {
            get { return _IDMerchant; }
            set
            {
                _IDMerchant = value;
                IsDirty = true;
            }
        }
        public SqlInt64 MerchantTransactionID
        {
            get { return _MerchantTransactionID; }
            set
            {
                _MerchantTransactionID = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDOffer
        {
            get { return _IDOffer; }
            set
            {
                _IDOffer = value;
                IsDirty = true;
            }
        }
        public SqlInt32 IDApplication
        {
            get { return _IDApplication; }
            set
            {
                _IDApplication = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet
        {
            get { return _IDLocationStreet; }
            set
            {
                _IDLocationStreet = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationStreet2
        {
            get { return _IDLocationStreet2; }
            set
            {
                _IDLocationStreet2 = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCity
        {
            get { return _IDLocationCity; }
            set
            {
                _IDLocationCity = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationState
        {
            get { return _IDLocationState; }
            set
            {
                _IDLocationState = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationCountry
        {
            get { return _IDLocationCountry; }
            set
            {
                _IDLocationCountry = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDLocationZipCode
        {
            get { return _IDLocationZipCode; }
            set
            {
                _IDLocationZipCode = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TransactionStatusID
        {
            get { return _TransactionStatusID; }
            set
            {
                _TransactionStatusID = value;
                IsDirty = true;
            }
        }
        public SqlInt64 Quantity
        {
            get { return _Quantity; }
            set
            {
                _Quantity = value;
                IsDirty = true;
            }
        }
        public SqlMoney AmountTotal
        {
            get { return _AmountTotal; }
            set
            {
                _AmountTotal = value;
                IsDirty = true;
            }
        }
        public SqlMoney AmountShiping
        {
            get { return _AmountShiping; }
            set
            {
                _AmountShiping = value;
                IsDirty = true;
            }
        }
        public SqlMoney AmountTax
        {
            get { return _AmountTax; }
            set
            {
                _AmountTax = value;
                IsDirty = true;
            }
        }
        public SqlString ItemProperties
        {
            get { return _ItemProperties; }
            set
            {
                _ItemProperties = value;
                IsDirty = true;
            }
        }
        public SqlString TransactionKey
        {
            get { return _TransactionKey; }
            set
            {
                _TransactionKey = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsVoid
        {
            get { return _IsVoid; }
            set
            {
                _IsVoid = value;
                IsDirty = true;
            }
        }
        public SqlString TextVoid
        {
            get { return _TextVoid; }
            set
            {
                _TextVoid = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Recieved
        {
            get { return _Recieved; }
            set
            {
                _Recieved = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Processed
        {
            get { return _Processed; }
            set
            {
                _Processed = value;
                IsDirty = true;
            }
        }
        public SqlDateTime Redeemed
        {
            get { return _Redeemed; }
            set
            {
                _Redeemed = value;
                IsDirty = true;
            }
        }
        public SqlString ppTransactionID
        {
            get { return _ppTransactionID; }
            set
            {
                _ppTransactionID = value;
                IsDirty = true;
            }
        }
        public SqlString ppAckValue
        {
            get { return _ppAckValue; }
            set
            {
                _ppAckValue = value;
                IsDirty = true;
            }
        }
        public SqlString ppErrorCode
        {
            get { return _ppErrorCode; }
            set
            {
                _ppErrorCode = value;
                IsDirty = true;
            }
        }
        public SqlString ppLongMessage
        {
            get { return _ppLongMessage; }
            set
            {
                _ppLongMessage = value;
                IsDirty = true;
            }
        }
        public SqlString ppCvv2Match
        {
            get { return _ppCvv2Match; }
            set
            {
                _ppCvv2Match = value;
                IsDirty = true;
            }
        }
        public SqlString ppAvsCode
        {
            get { return _ppAvsCode; }
            set
            {
                _ppAvsCode = value;
                IsDirty = true;
            }
        }
        public SqlMoney ppAmount
        {
            get { return _ppAmount; }
            set
            {
                _ppAmount = value;
                IsDirty = true;
            }
        }
        public SqlString CountryCode
        {
            get { return _CountryCode; }
            set
            {
                _CountryCode = value;
                IsDirty = true;
            }
        }
        public SqlString CurrencyCode
        {
            get { return _CurrencyCode; }
            set
            {
                _CurrencyCode = value;
                IsDirty = true;
            }
        }
        public SqlString Tax
        {
            get { return _Tax; }
            set
            {
                _Tax = value;
                IsDirty = true;
            }
        }
        public SqlString Shipping
        {
            get { return _Shipping; }
            set
            {
                _Shipping = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblWebAddress : CTable
    {
        private SqlInt64 _IDWebAddress; //IDENTITY 
        private SqlString _WebAddress;  //500
        public const int WebAddressMaxLen = 500;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDWebAddress = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDWebAddress; }
        public override void SetValue(SqlString val) { _WebAddress = val; }

        public TblWebAddress() { }
        public TblWebAddress(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblWebAddress(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDWebAddress = rd.SafeGetInt64("IDWebAddress");
            _WebAddress = rd.SafeGetString("WebAddress");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDWebAddress);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDWebAddress);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@WebAddress", _WebAddress);
        }

        #endregion
        #region accessors
        public SqlInt64 IDWebAddress
        {
            get { return _IDWebAddress; }
            private set
            {
                _IDWebAddress = value;
                IsDirty = true;
            }
        }
        public SqlString WebAddress
        {
            get { return _WebAddress; }
            set
            {
                _WebAddress = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TblZipCode : CTable
    {
        private SqlInt64 _IDZipCode; //IDENTITY 
        private SqlString _ZipCode;  //12
        private SqlInt32 _TYZipCode;
        private SqlInt32 _TYZipCodeLocation;
        private SqlInt64 _IDCity;
        private SqlString _City;  //160
        private SqlString _State;  //8
        private SqlString _Country;  //8
        private SqlDouble _Lat;
        private SqlDouble _Lon;
        private SqlDouble _Xaxis;
        private SqlDouble _Yaxis;
        private SqlDouble _Zaxis;
        private SqlString _LocationText;  //120
        private SqlString _Location;  //120
        private SqlBoolean _Decommisioned;
        public const int ZipCodeMaxLen = 12;
        public const int CityMaxLen = 160;
        public const int StateMaxLen = 8;
        public const int CountryMaxLen = 8;
        public const int LocationTextMaxLen = 120;
        public const int LocationMaxLen = 120;

        #region functions
        protected override void SetID(SqlInt64 id) { _IDZipCode = (SqlInt64)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_IDZipCode; }
        public override void SetValue(SqlString val) { _ZipCode = val; }

        public TblZipCode() { }
        public TblZipCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TblZipCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _IDZipCode = rd.SafeGetInt64("IDZipCode");
            _ZipCode = rd.SafeGetString("ZipCode");
            _TYZipCode = rd.SafeGetInt32("TYZipCode");
            _TYZipCodeLocation = rd.SafeGetInt32("TYZipCodeLocation");
            _IDCity = rd.SafeGetInt64("IDCity");
            _City = rd.SafeGetString("City");
            _State = rd.SafeGetString("State");
            _Country = rd.SafeGetString("Country");
            _Lat = rd.SafeGetDouble("Lat");
            _Lon = rd.SafeGetDouble("Lon");
            _Xaxis = rd.SafeGetDouble("Xaxis");
            _Yaxis = rd.SafeGetDouble("Yaxis");
            _Zaxis = rd.SafeGetDouble("Zaxis");
            _LocationText = rd.SafeGetString("LocationText");
            _Location = rd.SafeGetString("Location");
            _Decommisioned = rd.SafeGetBoolean("Decommisioned");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _IDZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
            cmd.Parameters.AddWithValue("@TYZipCode", _TYZipCode);
            cmd.Parameters.AddWithValue("@TYZipCodeLocation", _TYZipCodeLocation);
            cmd.Parameters.AddWithValue("@IDCity", _IDCity);
            cmd.Parameters.AddWithValue("@City", _City);
            cmd.Parameters.AddWithValue("@State", _State);
            cmd.Parameters.AddWithValue("@Country", _Country);
            cmd.Parameters.AddWithValue("@Lat", _Lat);
            cmd.Parameters.AddWithValue("@Lon", _Lon);
            cmd.Parameters.AddWithValue("@Xaxis", _Xaxis);
            cmd.Parameters.AddWithValue("@Yaxis", _Yaxis);
            cmd.Parameters.AddWithValue("@Zaxis", _Zaxis);
            cmd.Parameters.AddWithValue("@LocationText", _LocationText);
            cmd.Parameters.AddWithValue("@Location", _Location);
            cmd.Parameters.AddWithValue("@Decommisioned", _Decommisioned);
        }

        #endregion
        #region accessors
        public SqlInt64 IDZipCode
        {
            get { return _IDZipCode; }
            private set
            {
                _IDZipCode = value;
                IsDirty = true;
            }
        }
        public SqlString ZipCode
        {
            get { return _ZipCode; }
            set
            {
                _ZipCode = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYZipCode
        {
            get { return _TYZipCode; }
            set
            {
                _TYZipCode = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TYZipCodeLocation
        {
            get { return _TYZipCodeLocation; }
            set
            {
                _TYZipCodeLocation = value;
                IsDirty = true;
            }
        }
        public SqlInt64 IDCity
        {
            get { return _IDCity; }
            set
            {
                _IDCity = value;
                IsDirty = true;
            }
        }
        public SqlString City
        {
            get { return _City; }
            set
            {
                _City = value;
                IsDirty = true;
            }
        }
        public SqlString State
        {
            get { return _State; }
            set
            {
                _State = value;
                IsDirty = true;
            }
        }
        public SqlString Country
        {
            get { return _Country; }
            set
            {
                _Country = value;
                IsDirty = true;
            }
        }
        public SqlDouble Lat
        {
            get { return _Lat; }
            set
            {
                _Lat = value;
                IsDirty = true;
            }
        }
        public SqlDouble Lon
        {
            get { return _Lon; }
            set
            {
                _Lon = value;
                IsDirty = true;
            }
        }
        public SqlDouble Xaxis
        {
            get { return _Xaxis; }
            set
            {
                _Xaxis = value;
                IsDirty = true;
            }
        }
        public SqlDouble Yaxis
        {
            get { return _Yaxis; }
            set
            {
                _Yaxis = value;
                IsDirty = true;
            }
        }
        public SqlDouble Zaxis
        {
            get { return _Zaxis; }
            set
            {
                _Zaxis = value;
                IsDirty = true;
            }
        }
        public SqlString LocationText
        {
            get { return _LocationText; }
            set
            {
                _LocationText = value;
                IsDirty = true;
            }
        }
        public SqlString Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                IsDirty = true;
            }
        }
        public SqlBoolean Decommisioned
        {
            get { return _Decommisioned; }
            set
            {
                _Decommisioned = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeAccount : CTable
    {
        private SqlInt32 _TYAccount; //IDENTITY 
        private SqlString _Account;  //20
        public const int AccountMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYAccount = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYAccount; }
        public override void SetValue(SqlString val) { _Account = val; }

        public TypeAccount() { }
        public TypeAccount(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeAccount(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYAccount = rd.SafeGetInt32("TYAccount");
            _Account = rd.SafeGetString("Account");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYAccount);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYAccount);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYAccount);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Account", _Account);
        }

        #endregion
        #region accessors
        public SqlInt32 TYAccount
        {
            get { return _TYAccount; }
            private set
            {
                _TYAccount = value;
                IsDirty = true;
            }
        }
        public SqlString Account
        {
            get { return _Account; }
            set
            {
                _Account = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeCarrier : CTable
    {
        private SqlInt32 _TYCarrier; //IDENTITY 
        private SqlString _Carrier;  //50
        public const int CarrierMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYCarrier = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYCarrier; }
        public override void SetValue(SqlString val) { _Carrier = val; }

        public TypeCarrier() { }
        public TypeCarrier(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeCarrier(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYCarrier = rd.SafeGetInt32("TYCarrier");
            _Carrier = rd.SafeGetString("Carrier");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCarrier);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCarrier);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCarrier);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Carrier", _Carrier);
        }

        #endregion
        #region accessors
        public SqlInt32 TYCarrier
        {
            get { return _TYCarrier; }
            private set
            {
                _TYCarrier = value;
                IsDirty = true;
            }
        }
        public SqlString Carrier
        {
            get { return _Carrier; }
            set
            {
                _Carrier = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeCommunication : CTable
    {
        private SqlInt32 _TYCommunication; //IDENTITY 
        private SqlString _Communication;  //50
        public const int CommunicationMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYCommunication = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYCommunication; }
        public override void SetValue(SqlString val) { _Communication = val; }

        public TypeCommunication() { }
        public TypeCommunication(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeCommunication(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYCommunication = rd.SafeGetInt32("TYCommunication");
            _Communication = rd.SafeGetString("Communication");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCommunication);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCommunication);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCommunication);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Communication", _Communication);
        }

        #endregion
        #region accessors
        public SqlInt32 TYCommunication
        {
            get { return _TYCommunication; }
            private set
            {
                _TYCommunication = value;
                IsDirty = true;
            }
        }
        public SqlString Communication
        {
            get { return _Communication; }
            set
            {
                _Communication = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeCreditCard : CTable
    {
        private SqlInt32 _TYCreditCard; //IDENTITY 
        private SqlString _CreditCard;  //50
        public const int CreditCardMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYCreditCard = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYCreditCard; }
        public override void SetValue(SqlString val) { _CreditCard = val; }

        public TypeCreditCard() { }
        public TypeCreditCard(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeCreditCard(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYCreditCard = rd.SafeGetInt32("TYCreditCard");
            _CreditCard = rd.SafeGetString("CreditCard");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCreditCard);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCreditCard);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYCreditCard);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@CreditCard", _CreditCard);
        }

        #endregion
        #region accessors
        public SqlInt32 TYCreditCard
        {
            get { return _TYCreditCard; }
            private set
            {
                _TYCreditCard = value;
                IsDirty = true;
            }
        }
        public SqlString CreditCard
        {
            get { return _CreditCard; }
            set
            {
                _CreditCard = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeDataAccess : CTable
    {
        private SqlInt32 _TYDataAccess; //IDENTITY 
        private SqlString _DataAccess;  //20
        public const int DataAccessMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYDataAccess = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYDataAccess; }
        public override void SetValue(SqlString val) { _DataAccess = val; }

        public TypeDataAccess() { }
        public TypeDataAccess(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeDataAccess(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYDataAccess = rd.SafeGetInt32("TYDataAccess");
            _DataAccess = rd.SafeGetString("DataAccess");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataAccess);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataAccess);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataAccess);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DataAccess", _DataAccess);
        }

        #endregion
        #region accessors
        public SqlInt32 TYDataAccess
        {
            get { return _TYDataAccess; }
            private set
            {
                _TYDataAccess = value;
                IsDirty = true;
            }
        }
        public SqlString DataAccess
        {
            get { return _DataAccess; }
            set
            {
                _DataAccess = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeDataInput : CTable
    {
        private SqlInt32 _TYDataInput; //IDENTITY 
        private SqlString _DataInput;  //50
        public const int DataInputMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYDataInput = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYDataInput; }
        public override void SetValue(SqlString val) { _DataInput = val; }

        public TypeDataInput() { }
        public TypeDataInput(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeDataInput(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYDataInput = rd.SafeGetInt32("TYDataInput");
            _DataInput = rd.SafeGetString("DataInput");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataInput);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DataInput", _DataInput);
        }

        #endregion
        #region accessors
        public SqlInt32 TYDataInput
        {
            get { return _TYDataInput; }
            private set
            {
                _TYDataInput = value;
                IsDirty = true;
            }
        }
        public SqlString DataInput
        {
            get { return _DataInput; }
            set
            {
                _DataInput = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeDataManagement : CTable
    {
        private SqlInt32 _TYDataManagement; //IDENTITY 
        private SqlString _DataManagement;  //100
        public const int DataManagementMaxLen = 100;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYDataManagement = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYDataManagement; }
        public override void SetValue(SqlString val) { _DataManagement = val; }

        public TypeDataManagement() { }
        public TypeDataManagement(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeDataManagement(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYDataManagement = rd.SafeGetInt32("TYDataManagement");
            _DataManagement = rd.SafeGetString("DataManagement");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataManagement);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataManagement);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYDataManagement);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@DataManagement", _DataManagement);
        }

        #endregion
        #region accessors
        public SqlInt32 TYDataManagement
        {
            get { return _TYDataManagement; }
            private set
            {
                _TYDataManagement = value;
                IsDirty = true;
            }
        }
        public SqlString DataManagement
        {
            get { return _DataManagement; }
            set
            {
                _DataManagement = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeHarvesterAction : CTable
    {
        private SqlInt32 _TYHarvesterAction; //IDENTITY 
        private SqlString _HarvesterAction;  //50
        public const int HarvesterActionMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYHarvesterAction = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYHarvesterAction; }
        public override void SetValue(SqlString val) { _HarvesterAction = val; }

        public TypeHarvesterAction() { }
        public TypeHarvesterAction(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeHarvesterAction(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYHarvesterAction = rd.SafeGetInt32("TYHarvesterAction");
            _HarvesterAction = rd.SafeGetString("HarvesterAction");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYHarvesterAction);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYHarvesterAction);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYHarvesterAction);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@HarvesterAction", _HarvesterAction);
        }

        #endregion
        #region accessors
        public SqlInt32 TYHarvesterAction
        {
            get { return _TYHarvesterAction; }
            private set
            {
                _TYHarvesterAction = value;
                IsDirty = true;
            }
        }
        public SqlString HarvesterAction
        {
            get { return _HarvesterAction; }
            set
            {
                _HarvesterAction = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeLinkSource : CTable
    {
        private SqlInt32 _TYLinkSource; //IDENTITY 
        private SqlString _LinkSource;  //25
        public const int LinkSourceMaxLen = 25;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYLinkSource = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYLinkSource; }
        public override void SetValue(SqlString val) { _LinkSource = val; }

        public TypeLinkSource() { }
        public TypeLinkSource(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeLinkSource(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYLinkSource = rd.SafeGetInt32("TYLinkSource");
            _LinkSource = rd.SafeGetString("LinkSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLinkSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLinkSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLinkSource);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@LinkSource", _LinkSource);
        }

        #endregion
        #region accessors
        public SqlInt32 TYLinkSource
        {
            get { return _TYLinkSource; }
            private set
            {
                _TYLinkSource = value;
                IsDirty = true;
            }
        }
        public SqlString LinkSource
        {
            get { return _LinkSource; }
            set
            {
                _LinkSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeLocation : CTable
    {
        private SqlInt32 _TYLocation; //IDENTITY 
        private SqlString _Location;  //50
        public const int LocationMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYLocation = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYLocation; }
        public override void SetValue(SqlString val) { _Location = val; }

        public TypeLocation() { }
        public TypeLocation(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeLocation(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYLocation = rd.SafeGetInt32("TYLocation");
            _Location = rd.SafeGetString("Location");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Location", _Location);
        }

        #endregion
        #region accessors
        public SqlInt32 TYLocation
        {
            get { return _TYLocation; }
            private set
            {
                _TYLocation = value;
                IsDirty = true;
            }
        }
        public SqlString Location
        {
            get { return _Location; }
            set
            {
                _Location = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeMediaSource : CTable
    {
        private SqlInt32 _TYMediaSource; //IDENTITY 
        private SqlString _MediaSource;  //50
        public const int MediaSourceMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYMediaSource = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYMediaSource; }
        public override void SetValue(SqlString val) { _MediaSource = val; }

        public TypeMediaSource() { }
        public TypeMediaSource(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeMediaSource(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYMediaSource = rd.SafeGetInt32("TYMediaSource");
            _MediaSource = rd.SafeGetString("MediaSource");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYMediaSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYMediaSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYMediaSource);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@MediaSource", _MediaSource);
        }

        #endregion
        #region accessors
        public SqlInt32 TYMediaSource
        {
            get { return _TYMediaSource; }
            private set
            {
                _TYMediaSource = value;
                IsDirty = true;
            }
        }
        public SqlString MediaSource
        {
            get { return _MediaSource; }
            set
            {
                _MediaSource = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeNotification : CTable
    {
        private SqlInt32 _TYNotification; //IDENTITY 
        private SqlString _Notification;  //50
        public const int NotificationMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYNotification = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYNotification; }
        public override void SetValue(SqlString val) { _Notification = val; }

        public TypeNotification() { }
        public TypeNotification(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeNotification(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYNotification = rd.SafeGetInt32("TYNotification");
            _Notification = rd.SafeGetString("Notification");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotification);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotification);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotification);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Notification", _Notification);
        }

        #endregion
        #region accessors
        public SqlInt32 TYNotification
        {
            get { return _TYNotification; }
            private set
            {
                _TYNotification = value;
                IsDirty = true;
            }
        }
        public SqlString Notification
        {
            get { return _Notification; }
            set
            {
                _Notification = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeNotificationChannel : CTable
    {
        private SqlInt32 _TYNotificationChannel; //IDENTITY 
        private SqlString _NotificationChannel;  //50
        private SqlBoolean _IsEnabled;
        public const int NotificationChannelMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYNotificationChannel = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYNotificationChannel; }
        public override void SetValue(SqlString val) { _NotificationChannel = val; }

        public TypeNotificationChannel() { }
        public TypeNotificationChannel(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeNotificationChannel(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYNotificationChannel = rd.SafeGetInt32("TYNotificationChannel");
            _NotificationChannel = rd.SafeGetString("NotificationChannel");
            _IsEnabled = rd.SafeGetBoolean("IsEnabled");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationChannel);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationChannel);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationChannel);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@NotificationChannel", _NotificationChannel);
            cmd.Parameters.AddWithValue("@IsEnabled", _IsEnabled);
        }

        #endregion
        #region accessors
        public SqlInt32 TYNotificationChannel
        {
            get { return _TYNotificationChannel; }
            private set
            {
                _TYNotificationChannel = value;
                IsDirty = true;
            }
        }
        public SqlString NotificationChannel
        {
            get { return _NotificationChannel; }
            set
            {
                _NotificationChannel = value;
                IsDirty = true;
            }
        }
        public SqlBoolean IsEnabled
        {
            get { return _IsEnabled; }
            set
            {
                _IsEnabled = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeNotificationGateway : CTable
    {
        private SqlInt32 _TYNotificationGateway; //IDENTITY 
        private SqlString _NotificationGateway;  //20
        public const int NotificationGatewayMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYNotificationGateway = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYNotificationGateway; }
        public override void SetValue(SqlString val) { _NotificationGateway = val; }

        public TypeNotificationGateway() { }
        public TypeNotificationGateway(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeNotificationGateway(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYNotificationGateway = rd.SafeGetInt32("TYNotificationGateway");
            _NotificationGateway = rd.SafeGetString("NotificationGateway");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationGateway);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationGateway);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationGateway);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@NotificationGateway", _NotificationGateway);
        }

        #endregion
        #region accessors
        public SqlInt32 TYNotificationGateway
        {
            get { return _TYNotificationGateway; }
            private set
            {
                _TYNotificationGateway = value;
                IsDirty = true;
            }
        }
        public SqlString NotificationGateway
        {
            get { return _NotificationGateway; }
            set
            {
                _NotificationGateway = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeNotificationVendor : CTable
    {
        private SqlInt32 _TYNotificationVendor; //IDENTITY 
        private SqlString _NotificationVendor;  //30
        public const int NotificationVendorMaxLen = 30;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYNotificationVendor = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYNotificationVendor; }
        public override void SetValue(SqlString val) { _NotificationVendor = val; }

        public TypeNotificationVendor() { }
        public TypeNotificationVendor(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeNotificationVendor(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYNotificationVendor = rd.SafeGetInt32("TYNotificationVendor");
            _NotificationVendor = rd.SafeGetString("NotificationVendor");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationVendor);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationVendor);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYNotificationVendor);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@NotificationVendor", _NotificationVendor);
        }

        #endregion
        #region accessors
        public SqlInt32 TYNotificationVendor
        {
            get { return _TYNotificationVendor; }
            private set
            {
                _TYNotificationVendor = value;
                IsDirty = true;
            }
        }
        public SqlString NotificationVendor
        {
            get { return _NotificationVendor; }
            set
            {
                _NotificationVendor = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypePingCycle : CTable
    {
        private SqlInt32 _TYPingCycle; //IDENTITY 
        private SqlString _PingCycle;  //20
        private SqlInt32 _TimeInMinutes;
        public const int PingCycleMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYPingCycle = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYPingCycle; }
        public override void SetValue(SqlString val) { _PingCycle = val; }

        public TypePingCycle() { }
        public TypePingCycle(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypePingCycle(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYPingCycle = rd.SafeGetInt32("TYPingCycle");
            _PingCycle = rd.SafeGetString("PingCycle");
            _TimeInMinutes = rd.SafeGetInt32("TimeInMinutes");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPingCycle);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPingCycle);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPingCycle);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@PingCycle", _PingCycle);
            cmd.Parameters.AddWithValue("@TimeInMinutes", _TimeInMinutes);
        }

        #endregion
        #region accessors
        public SqlInt32 TYPingCycle
        {
            get { return _TYPingCycle; }
            private set
            {
                _TYPingCycle = value;
                IsDirty = true;
            }
        }
        public SqlString PingCycle
        {
            get { return _PingCycle; }
            set
            {
                _PingCycle = value;
                IsDirty = true;
            }
        }
        public SqlInt32 TimeInMinutes
        {
            get { return _TimeInMinutes; }
            set
            {
                _TimeInMinutes = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypePrefix : CTable
    {
        private SqlInt32 _TYPrefix; //IDENTITY 
        private SqlString _Prefix;  //50
        public const int PrefixMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYPrefix = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYPrefix; }
        public override void SetValue(SqlString val) { _Prefix = val; }

        public TypePrefix() { }
        public TypePrefix(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypePrefix(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYPrefix = rd.SafeGetInt32("TYPrefix");
            _Prefix = rd.SafeGetString("Prefix");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPrefix);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYPrefix);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Prefix", _Prefix);
        }

        #endregion
        #region accessors
        public SqlInt32 TYPrefix
        {
            get { return _TYPrefix; }
            private set
            {
                _TYPrefix = value;
                IsDirty = true;
            }
        }
        public SqlString Prefix
        {
            get { return _Prefix; }
            set
            {
                _Prefix = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeRelationship : CTable
    {
        private SqlInt32 _TYRelationship; //IDENTITY 
        private SqlString _Relationship;  //20
        public const int RelationshipMaxLen = 20;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYRelationship = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYRelationship; }
        public override void SetValue(SqlString val) { _Relationship = val; }

        public TypeRelationship() { }
        public TypeRelationship(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeRelationship(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYRelationship = rd.SafeGetInt32("TYRelationship");
            _Relationship = rd.SafeGetString("Relationship");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYRelationship);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYRelationship);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Relationship", _Relationship);
        }

        #endregion
        #region accessors
        public SqlInt32 TYRelationship
        {
            get { return _TYRelationship; }
            private set
            {
                _TYRelationship = value;
                IsDirty = true;
            }
        }
        public SqlString Relationship
        {
            get { return _Relationship; }
            set
            {
                _Relationship = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeServerMessage : CTable
    {
        private SqlInt32 _TYServerMessage; //IDENTITY 
        private SqlString _ServerMessage;  //500
        public const int ServerMessageMaxLen = 500;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYServerMessage = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYServerMessage; }
        public override void SetValue(SqlString val) { _ServerMessage = val; }

        public TypeServerMessage() { }
        public TypeServerMessage(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeServerMessage(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYServerMessage = rd.SafeGetInt32("TYServerMessage");
            _ServerMessage = rd.SafeGetString("ServerMessage");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYServerMessage);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYServerMessage);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYServerMessage);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ServerMessage", _ServerMessage);
        }

        #endregion
        #region accessors
        public SqlInt32 TYServerMessage
        {
            get { return _TYServerMessage; }
            private set
            {
                _TYServerMessage = value;
                IsDirty = true;
            }
        }
        public SqlString ServerMessage
        {
            get { return _ServerMessage; }
            set
            {
                _ServerMessage = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeSource : CTable
    {
        private SqlInt32 _TYSource; //IDENTITY 
        private SqlString _Source;  //15
        public const int SourceMaxLen = 15;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYSource = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYSource; }
        public override void SetValue(SqlString val) { _Source = val; }

        public TypeSource() { }
        public TypeSource(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeSource(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYSource = rd.SafeGetInt32("TYSource");
            _Source = rd.SafeGetString("Source");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYSource);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYSource);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYSource);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Source", _Source);
        }

        #endregion
        #region accessors
        public SqlInt32 TYSource
        {
            get { return _TYSource; }
            private set
            {
                _TYSource = value;
                IsDirty = true;
            }
        }
        public SqlString Source
        {
            get { return _Source; }
            set
            {
                _Source = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeTitle : CTable
    {
        private SqlInt32 _TYTitle; //IDENTITY 
        private SqlString _Title;  //50
        public const int TitleMaxLen = 50;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYTitle = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYTitle; }
        public override void SetValue(SqlString val) { _Title = val; }

        public TypeTitle() { }
        public TypeTitle(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeTitle(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYTitle = rd.SafeGetInt32("TYTitle");
            _Title = rd.SafeGetString("Title");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYTitle);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYTitle);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@Title", _Title);
        }

        #endregion
        #region accessors
        public SqlInt32 TYTitle
        {
            get { return _TYTitle; }
            private set
            {
                _TYTitle = value;
                IsDirty = true;
            }
        }
        public SqlString Title
        {
            get { return _Title; }
            set
            {
                _Title = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeZipCode : CTable
    {
        private SqlInt32 _TYZipCode; //IDENTITY 
        private SqlString _ZipCode;  //10
        public const int ZipCodeMaxLen = 10;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYZipCode = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYZipCode; }
        public override void SetValue(SqlString val) { _ZipCode = val; }

        public TypeZipCode() { }
        public TypeZipCode(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeZipCode(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYZipCode = rd.SafeGetInt32("TYZipCode");
            _ZipCode = rd.SafeGetString("ZipCode");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCode);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCode);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCode);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ZipCode", _ZipCode);
        }

        #endregion
        #region accessors
        public SqlInt32 TYZipCode
        {
            get { return _TYZipCode; }
            private set
            {
                _TYZipCode = value;
                IsDirty = true;
            }
        }
        public SqlString ZipCode
        {
            get { return _ZipCode; }
            set
            {
                _ZipCode = value;
                IsDirty = true;
            }
        }

        #endregion
    }
    public partial class TypeZipCodeLocation : CTable
    {
        private SqlInt32 _TYZipCodeLocation; //IDENTITY 
        private SqlString _ZipCodeLocation;  //15
        public const int ZipCodeLocationMaxLen = 15;

        #region functions
        protected override void SetID(SqlInt64 id) { _TYZipCodeLocation = (SqlInt32)id; }
        public override SqlInt64 GetID() { return (SqlInt64)_TYZipCodeLocation; }
        public override void SetValue(SqlString val) { _ZipCodeLocation = val; }

        public TypeZipCodeLocation() { }
        public TypeZipCodeLocation(SqlInt64 id, SqlConnection cn, SqlCommand cmd, SqlDataReader rd) //initialize from its ID
        {
            SetID(id);
            OnPreRetrieve(cmd);
            Retrieve(cn, cmd, rd);
            Init();
        }
        public TypeZipCodeLocation(SqlDataReader rd)
        {
            InitFromData(rd);
            Init();
        }

        private void Init()
        {
            IsDirty = false;
            IsValid = false;
        }
        protected override Result InitFromData(SqlDataReader rd)
        {
            _TYZipCodeLocation = rd.SafeGetInt32("TYZipCodeLocation");
            _ZipCodeLocation = rd.SafeGetString("ZipCodeLocation");
            IsDirty = false;
            IsValid = true;
            return Result.OK;
        }
        protected override Result OnPreRetrieve(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCodeLocation);
            return Result.OK;
        }
        protected override Result OnPreDelete(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCodeLocation);
            return Result.OK;
        }
        protected override Result OnPreCreate(SqlCommand cmd)
        {
            AddParameters(cmd);
            return Result.OK;
        }
        protected override Result OnPreUpdate(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@RecordID", _TYZipCodeLocation);
            AddParameters(cmd);
            return Result.OK;
        }
        private void AddParameters(SqlCommand cmd)
        {
            cmd.Parameters.AddWithValue("@ZipCodeLocation", _ZipCodeLocation);
        }

        #endregion
        #region accessors
        public SqlInt32 TYZipCodeLocation
        {
            get { return _TYZipCodeLocation; }
            private set
            {
                _TYZipCodeLocation = value;
                IsDirty = true;
            }
        }
        public SqlString ZipCodeLocation
        {
            get { return _ZipCodeLocation; }
            set
            {
                _ZipCodeLocation = value;
                IsDirty = true;
            }
        }

        #endregion
    }
}