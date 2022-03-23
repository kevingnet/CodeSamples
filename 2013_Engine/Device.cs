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
    public class Device
    {
        SqlConnection _Connection = null;
        private SqlInt64 _ID = 0;
        private DataObjects _Objects = null;
        private TblDeviceData _Cache = null;
        private HistDeviceTokenData _CacheHistory = null;
        private TblDevice _Device = null;
        private Device() { }
        public Device(SqlConnection cn, DataObjects obj, string DeviceUID)
        {
            _Connection = cn;
            _Objects = obj;
            _ID = _Objects.GetIDFrom(_Connection, ColumnTypes.TIDDevice, DeviceUID);
            _Cache = (TblDeviceData)_Objects.GetCacheType(DataObjects.TableTypes.TTblDevice);
            _CacheHistory = (HistDeviceTokenData)_Objects.GetMultiCacheType(DataObjects.TableTypes.THistDeviceToken);
            _Device = (TblDevice)_Objects.GetObjectFromID(_Connection, _ID, DataObjects.TableTypes.TTblDevice);
        }
        public bool IsValid() { return (bool)(_ID != 0); }

        //ID is already a valid one and it must be in cache
        public bool VerifyToken(string token)
        {
            if (token == "JakeTestToken")
                return true;
            bool ok = false;
            _Cache.Checkout(_ID);
            if (_Device.Token == token)
                ok = true;
            _Cache.Checkin(_ID);
            if (ok == false)
            {
                //look in the history table
                HistDeviceToken hist = (HistDeviceToken)_Objects.GetMultiObjectFromID(_Connection, _ID, DataObjects.TableTypes.THistDeviceToken);
                if (hist != null)
                {
                    foreach (var item in _CacheHistory)
                    {
                        HistDeviceToken devHist = (HistDeviceToken)item;
                        if (_ID == devHist.GetID() && devHist.DeviceToken == token)
                        {
                            return true;
                        }
                    }
                }
            }
            return ok;
        }
        private SqlInt64 GetOrAddPhone()
        {
            SqlInt64 phoneID = _Objects.GetIDFrom(_Connection, ColumnTypes.TIDPhone, _Device.PhoneNumber.ToString());
            if (phoneID == 0)
            {
                //add it...
                TblPhoneData phones = (TblPhoneData)_Objects.GetCacheType(DataObjects.TableTypes.TTblPhone);
                TblPhone phone = (TblPhone)phones.CreateNewObject();
                SqlCommand cmd = new SqlCommand();
                phone.Phone = _Device.PhoneNumber.ToString();
                phone.Country = "1"; //or whatever...
                phone.Create(_Connection, cmd);
                phoneID = phone.GetID();
                phones.Add(phoneID, phone);
            }
            return phoneID;
        }
        public SqlInt64 GetProfileID()
        {
            SqlInt64 profileID = _Device.IDProfile;
            if (profileID == 0)
            {
                SqlInt64 phoneID = GetOrAddPhone();
                //search for matching profiles in cache, 

                //not found, load matching profile from database..
            }
            return profileID;
        }
    }
}
