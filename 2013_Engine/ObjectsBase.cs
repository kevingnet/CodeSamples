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
        ConcurrentDictionary<DataObjects.TableTypes, CDataMulti> _tablesMulti = new ConcurrentDictionary<DataObjects.TableTypes, CDataMulti>();
        private HistDeviceTokenData _HistDeviceTokenData = new HistDeviceTokenData();
        private HistSearchData _HistSearchData = new HistSearchData();

        public static ColumnTypes GetColumnType(string col)
        {
            ColumnTypes type = ColumnTypes.TCOLUMNINVALID;
            if (Enum.IsDefined(typeof(ColumnTypes), col))
                type = (ColumnTypes)Enum.Parse(typeof(ColumnTypes), col, true);
            return type;
        }

        public CData GetCacheType(TableTypes table)
        {
            return _tables[table];
        }
        public CDataMulti GetMultiCacheType(DataObjects.TableTypes table)
        {
            return _tablesMulti[table];
        }
        public CTable GetMultiObjectFromID(SqlConnection cn, SqlInt64 id, DataObjects.TableTypes table)
        {
            CDataMulti cache = _tablesMulti[table];
            if (cache == null)
                return null;

            bool firstAttempt = true;
        RETRY:
            foreach (CTable item in cache)
            {
                if (id == (SqlInt64)item.GetID())
                    return item;
            }

            //ok, let's get it from the database, but only once
            if (firstAttempt == true)
            {
                cache.LoadObjectFromID(cn, id);
                firstAttempt = false;
                goto RETRY;
            }
            return null;
        }
        public CTable GetObjectFromID(SqlConnection cn, SqlInt64 id, TableTypes table, bool debug = false)
        {
            CData cache = _tables[table];
            if (cache == null)
                return null;

            bool firstAttempt = true;
        RETRY:
            foreach (var pair in cache)
            {
                if (id == (SqlInt64)pair.Key)
                    return pair.Value;
            }

            //id was not found, see if we should get it from the database, that is capacity is not zero, zero means all records are here...
            if (debug == false && cache.GetCapacity() == 0)
                return null;

            //ok, let's get it from the database, but only once
            if (firstAttempt == true)
            {
                cache.LoadObjectFromID(cn, id);
                firstAttempt = false;
                goto RETRY;
            }
            return null;
        }
        //an ID cannot be zero
        public SqlInt64 GetIDFrom(SqlConnection cn, ColumnTypes col, string value, bool create = false)
        {
            //get table name, then
            //ColumnTypes col = GetColumnType(idField);
            TableTypes tableType = m_Fields.GetTableType(col);
            if (tableType == TableTypes.TTABLEINVALID)
                return 0;
            //get id from cache object based on table name
            CData cache = _tables[tableType];
            if (cache == null)
                return 0;

            //field name to search and get its ID
            string field = col.ToString().Substring(3); //remove the ID,TY or ST part from the beginning of the string in field name
            PropertyInfo propertyInfo = null;
            //var pairRefl = cache.ElementAtOrDefault(0);
            //if (pairRefl.Value == null)
            //{
            CTable tmpObj = cache.CreateNewObject();
            propertyInfo = tmpObj.GetType().GetProperty(field);
            //}
            //else
            //{
            //    propertyInfo = tableType.GetProperty(field);
            //propertyInfo = tableType.GetType().GetProperty(field);
            //}
            //PropertyInfo propertyInfo = pairRefl.Value.GetType().GetProperty(field);

            if (propertyInfo == null)
                return 0;
            MethodInfo[] acc = propertyInfo.GetAccessors();
            if (acc == null || acc[0] == null)
                return 0;

            int attempts = 1;
        RETRY:
            foreach (var pair in cache)
            {
                object val = acc[0].Invoke(pair.Value, null);
                if (val.ToString() == value)
                    return (SqlInt64)pair.Key;
            }

            //id was not found, see if we should get it from the database, that is capacity is not zero, zero means all records are here...
            //if (debug == false && cache.GetCapacity() == 0)
            //    return 0;

            //ok, let's get it from the database, but only once
            if (attempts == 1)
            {
                cache.LoadObjectFromName(cn, value);
                attempts++;
                goto RETRY;
            }

            if (create == false)
                return 0;

            //not found, let's add it...
            if (attempts == 2)
            {
                cache.CreateObjectFromName(cn, value);
                attempts++;
                goto RETRY;
            }

            /*
            bool firstAttempt = true;
RETRY:
            foreach (var pair in cache)
            {
                PropertyInfo propertyInfo = pair.Value.GetType().GetProperty(field);
                if (propertyInfo == null)
                    return 0;
                MethodInfo[] acc = propertyInfo.GetAccessors();
                if (acc == null || acc[0] == null)
                    return 0;
                object val = acc[0].Invoke(pair.Value, null);
                if (val.ToString() == value)
                    return (SqlInt64)pair.Key;
            }

            //id was not found, see if we should get it from the database, that is capacity is not zero, zero means all records are here...
            if (debug == false && cache.GetCapacity() == 0)
                return 0;

            //ok, let's get it from the database, but only once
            if (firstAttempt == true)
            {
                cache.LoadObjectFromName(cn, value);
                firstAttempt = false;
                goto RETRY;
            }
            */
            return 0;
        }
        private void LoadData()
        {
            _tablesMulti.TryAdd(TableTypes.THistDeviceToken, _HistDeviceTokenData);
            _tablesMulti.TryAdd(TableTypes.THistSearch, _HistSearchData);

            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cn.Open();
            SqlInt64 id = GetIDFrom(cn, ColumnTypes.TIDDeviceManufacturer, "Applezz", true);
            //SqlInt64 id = GetIDFrom(cn, "Jake Knows, Inc", "IDGroup", true);

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
#if DEBUG
            foreach (var pair in _tables)
            {
                //if (pair.Key == "TblDeviceManufacturer")
                //pair.Value.Populate();
            }
#else
            Parallel.ForEach(_tables, pair =>
            {
                //if (pair.Key == "TblDeviceManufacturer")
                //pair.Value.Populate();
            });
#endif
            stopwatch.Stop();
            log.Info("Cache load -- TIME LAPSE: " + stopwatch.Elapsed);
            cn.Close();
            cn.Dispose();
        }
    }

    public abstract class CData : RingBufferDictionary<CTable>
    {
        public CData(int max, int threshold) : base(max, threshold) { }

        public string GetCommandLoadObjectFromID() { return "cg_usp_" + GetTableName() + "_Retrieve"; }
        public string GetCommandLoadObjectFromName() { return "cg_usp_" + GetTableName() + "_RetrieveFromName"; }
        public string GetCommandLoadObjectAssociations() { return "cg_usp_" + GetTableName() + "_RetrieveAllAssocForID"; }
        public string GetCommandPopulateAssociations() { return "cg_usp_" + GetTableName() + "_Populate"; }
        public string GetTableName()
        {
            string n = GetType().UnderlyingSystemType.Name;
            return Char.ToLowerInvariant(n[0]) + n.Substring(1, n.Length - 5);
        }
        public string GetTypeName()
        {
            string n = GetType().UnderlyingSystemType.Name;
            return "JakeKnowsEngineComponent." + n.Substring(0, n.Length - 4) + ",JakeKnowsEngineComponent";
        }
        private CTable CreateNewObject(SqlDataReader rd)
        {
            Type type = Type.GetType(GetTypeName());
            ConstructorInfo ctor = type.GetConstructor(new[] { typeof(SqlDataReader) });
            object instance = ctor.Invoke(new object[] { rd });
            return (CTable)instance;
        }
        public CTable CreateNewObject()
        {
            Type type = Type.GetType(GetTypeName());
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(Type.EmptyTypes);
            return (CTable)instance;
        }

        protected Result OnPrePopulate(SqlCommand cmd)
        {
            cmd.CommandText = GetCommandPopulateAssociations();
            cmd.Parameters.AddWithValue("@records", GetCapacity());
            return Result.OK;
        }
        public SqlInt64 PostPopulate(SqlDataReader rd)
        {
            return OnPostPopulate(rd);
        }
        protected SqlInt64 OnPostPopulate(SqlDataReader rd)
        {
            SqlInt64 id = 0;
            while (rd.Read())
            {
                CTable tbl = CreateNewObject(rd);
                if (tbl != null)
                {
                    id = tbl.GetID();
                    Add(id, tbl);
                }
            }
            return id;
        }
        protected bool AddAssociation(SqlDataReader rd, DataObjects.TableTypes type)
        {
            CTable tbl;
            SqlInt64 id = rd.SafeGetIDField();
            TryGetValue(id, out tbl);
            if (tbl == null)
                return false;
            tbl.AddAssociation(type, rd);
            return true;
        }

        protected Result OnPreLoadObjectFromID(SqlCommand cmd, SqlInt64 id)
        {
            cmd.CommandText = GetCommandLoadObjectFromID();
            cmd.Parameters.AddWithValue("@RecordID", id);
            return Result.OK;
        }
        public Result CreateObjectFromName(SqlConnection cn, string value)
        {
            try
            {
                Logging.LogDebug("Loading object from name for table: " + GetTableName());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                CTable tmpObj = CreateNewObject();
                tmpObj.SetValue(value);
                tmpObj.Create(cn, cmd);
                TryAdd(tmpObj.GetID(), tmpObj);
                cmd.Dispose();
            }
            catch (Exception e)
            {
            }
            return Result.OK;
        }
        public Result LoadObjectFromID(SqlConnection cn, SqlInt64 id)
        {
            Logging.LogDebug("Loading object from name for table: " + GetTableName());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPreLoadObjectFromID(cmd, id);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            SqlInt64 idReturned = OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            LoadAssociations(cn, id);
            return Result.OK;
        }
        protected Result OnPreLoadObjectFromName(SqlCommand cmd, string value)
        {
            cmd.CommandText = GetCommandLoadObjectFromName();
            cmd.Parameters.AddWithValue("@name", value);
            return Result.OK;
        }
        public Result LoadObjectFromName(SqlConnection cn, string value)
        {
            Logging.LogDebug("Loading object from name for table: " + GetTableName());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPreLoadObjectFromName(cmd, value);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            SqlInt64 id = OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            LoadAssociations(cn, id);
            return Result.OK;
        }
        public Result LoadAssociations(SqlConnection cn, SqlInt64 id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataReader rd = null;
            try
            {
                if (this.Count <= 0)
                    return Result.OK;
                var pairAssoc = this.ElementAt(0);
                if (pairAssoc.Value == null)
                    return Result.OK;
                ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _assocs = pairAssoc.Value.GetAssociations();
                if (_assocs == null || _assocs.Count <= 0)
                    return Result.OK;

                cmd.CommandText = GetCommandLoadObjectAssociations();
                cmd.Parameters.AddWithValue("@IDParent", id);
                rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                if (rd == null)
                    return Result.NODATA;

                while (rd.HasRows)
                {
                    CTableAssociation assoc = null;
                    //get assoc table name
                    string tblName = "";
                    if (rd.Read())
                    {
                        tblName = (string)rd.SafeGetString();
                        DataObjects.TableTypes type = DataObjects.GetTableType(tblName);
                        if (type != DataObjects.TableTypes.TTABLEINVALID)
                        {
                            Logging.LogDebug("LoadAssociations: " + GetTableName() + ":" + tblName);
                            assoc = _assocs[type];
                        }
                    }
                    rd.NextResult();
                    while (rd.Read())
                    {
                        DataObjects.TableTypes type = DataObjects.GetTableType(tblName);
                        if (type != DataObjects.TableTypes.TTABLEINVALID)
                        {
                            AddAssociation(rd, type);
                        }
                    }
                    rd.NextResult();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError("LoadAssociations ERROR: " + ex.Message);
            }
            finally
            {
                if (rd != null)
                    rd.Close();
                cmd.Dispose();
            }
            return Result.OK;
        }

        public Result Populate(string spName, SqlInt64 param)
        {
            Logging.LogDebug("Populate table: " + GetTableName());
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPrePopulate(cmd);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            PopulateAssociations(cn);
            cn.Close();
            cn.Dispose();
            return Result.OK;
        }
        public Result Populate()
        {
            Logging.LogDebug("Populate table: " + GetTableName());
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPrePopulate(cmd);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            PopulateAssociations(cn);
            cn.Close();
            cn.Dispose();
            return Result.OK;
        }
        public Result PopulateAssociations(SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 120;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataReader rd = null;
            try
            {
                if (this.Count <= 0)
                    return Result.OK;
                var pairAssoc = this.ElementAt(0);
                if (pairAssoc.Value == null)
                    return Result.OK;
                ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _assoc = pairAssoc.Value.GetAssociations();
                if (_assoc == null || _assoc.Count <= 0)
                    return Result.OK;

                foreach (var pair in _assoc)
                {
                    Logging.LogDebug("PopulateAssociations: " + GetTableName() + ":" + pair.Value.GetTableName());
                    cmd.CommandText = pair.Value.GetCommandPopulateAssociations();
                    rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    if (rd == null)
                        continue;
                    while (rd.Read())
                    {
                        AddAssociation(rd, pair.Key);
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError("PopulateAssociations ERROR: " + ex.Message);
            }
            finally
            {
                if (rd != null)
                    rd.Close();
                cmd.Dispose();
            }
            return Result.OK;
        }
    }
    public partial class HistDeviceTokenData : CDataMulti
    {
    }
    public partial class HistSearchData : CDataMulti
    {
    }
    public abstract class CDataMulti : ConcurrentBag<CTable>
    {
        public string GetCommandLoadObjectFromID() { return "cg_usp_" + GetTableName() + "_Retrieve"; }
        public string GetCommandLoadObjectFromName() { return "cg_usp_" + GetTableName() + "_RetrieveFromName"; }
        public string GetCommandLoadObjectAssociations() { return "cg_usp_" + GetTableName() + "_RetrieveAllAssocForID"; }
        public string GetCommandPopulateAssociations() { return "cg_usp_" + GetTableName() + "_Populate"; }
        public string GetTableName()
        {
            string n = GetType().UnderlyingSystemType.Name;
            return Char.ToLowerInvariant(n[0]) + n.Substring(1, n.Length - 5);
        }
        public string GetTypeName()
        {
            string n = GetType().UnderlyingSystemType.Name;
            return "JakeKnowsEngineComponent." + n.Substring(0, n.Length - 4) + ",JakeKnowsEngineComponent";
        }

        public CTable CreateNewObject()
        {
            Type type = Type.GetType(GetTypeName());
            ConstructorInfo ctor = type.GetConstructor(Type.EmptyTypes);
            object instance = ctor.Invoke(Type.EmptyTypes);
            return (CTable)instance;
        }
        private CTable CreateNewObject(SqlDataReader rd)
        {
            Type type = Type.GetType(GetTypeName());
            ConstructorInfo ctor = type.GetConstructor(new[] { typeof(SqlDataReader) });
            object instance = ctor.Invoke(new object[] { rd });
            return (CTable)instance;
        }

        protected Result OnPrePopulate(SqlCommand cmd)
        {
            cmd.CommandText = GetCommandPopulateAssociations();
            cmd.Parameters.AddWithValue("@records", 0);
            return Result.OK;
        }
        public SqlInt64 PostPopulate(SqlDataReader rd)
        {
            return OnPostPopulate(rd);
        }
        protected SqlInt64 OnPostPopulate(SqlDataReader rd)
        {
            SqlInt64 id = 0;
            while (rd.Read())
            {
                CTable tbl = CreateNewObject(rd);
                if (tbl != null)
                {
                    Add(tbl);
                }
            }
            return id;
        }
        protected bool AddAssociation(SqlDataReader rd, DataObjects.TableTypes type)
        {
            CTable tbl;
            SqlInt64 id = rd.SafeGetIDField();
            this.TryPeek(out tbl);
            if (tbl == null)
                return false;
            tbl.AddAssociation(type, rd);
            return true;
        }

        protected Result OnPreLoadObjectFromID(SqlCommand cmd, SqlInt64 id)
        {
            cmd.CommandText = GetCommandLoadObjectFromID();
            cmd.Parameters.AddWithValue("@RecordID", id);
            return Result.OK;
        }
        public Result LoadObjectFromID(SqlConnection cn, SqlInt64 id)
        {
            Logging.LogDebug("Loading object from name for table: " + GetTableName());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPreLoadObjectFromID(cmd, id);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            SqlInt64 idReturned = OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            LoadAssociations(cn, id);
            return Result.OK;
        }
        protected Result OnPreLoadObjectFromName(SqlCommand cmd, string value)
        {
            cmd.CommandText = GetCommandLoadObjectFromName();
            cmd.Parameters.AddWithValue("@name", value);
            return Result.OK;
        }
        public Result CreateObjectFromName(SqlConnection cn, string value)
        {
            try
            {
                Logging.LogDebug("Loading object from name for table: " + GetTableName());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = cn;
                CTable tmpObj = CreateNewObject();
                tmpObj.SetValue(value);
                tmpObj.Create(cn, cmd);
                //TryAdd(tmpObj.GetID(), tmpObj);
                Add(tmpObj);
                cmd.Dispose();
            }
            catch (Exception e)
            {
            }
            return Result.OK;
        }
        public Result LoadObjectFromName(SqlConnection cn, string value)
        {
            Logging.LogDebug("Loading object from name for table: " + GetTableName());
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPreLoadObjectFromName(cmd, value);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            SqlInt64 id = OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            LoadAssociations(cn, id);
            return Result.OK;
        }
        public Result LoadAssociations(SqlConnection cn, SqlInt64 id)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataReader rd = null;
            try
            {
                if (this.Count <= 0)
                    return Result.OK;
                CTable elem = this.ElementAt(0);
                if (elem == null)
                    return Result.OK;
                ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _assocs = elem.GetAssociations();
                if (_assocs == null || _assocs.Count <= 0)
                    return Result.OK;

                cmd.CommandText = GetCommandLoadObjectAssociations();
                cmd.Parameters.AddWithValue("@IDParent", id);
                rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                if (rd == null)
                    return Result.NODATA;

                while (rd.HasRows)
                {
                    CTableAssociation assoc = null;
                    //get assoc table name
                    string tblName = "";
                    DataObjects.TableTypes type = DataObjects.GetTableType(tblName);
                    if (rd.Read())
                    {
                        tblName = (string)rd.SafeGetString();
                        if (type != DataObjects.TableTypes.TTABLEINVALID)
                        {
                            Logging.LogDebug("LoadAssociations: " + GetTableName() + ":" + tblName);
                            assoc = _assocs[type];
                        }
                    }
                    rd.NextResult();
                    while (rd.Read())
                    {
                        if (type != DataObjects.TableTypes.TTABLEINVALID)
                        {
                            AddAssociation(rd, type);
                        }
                    }
                    rd.NextResult();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError("LoadAssociations ERROR: " + ex.Message);
            }
            finally
            {
                if (rd != null)
                    rd.Close();
                cmd.Dispose();
            }
            return Result.OK;
        }

        public Result Populate()
        {
            Logging.LogDebug("Populate table: " + GetTableName());
            SqlConnection cn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionString"]);
            cn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataReader rd = null;
            cmd.Connection = cn;
            OnPrePopulate(cmd);
            rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
            OnPostPopulate(rd);
            rd.Close();
            cmd.Dispose();
            PopulateAssociations(cn);
            cn.Close();
            cn.Dispose();
            return Result.OK;
        }
        public Result PopulateAssociations(SqlConnection cn)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandTimeout = 120;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Connection = cn;
            SqlDataReader rd = null;
            try
            {
                if (this.Count <= 0)
                    return Result.OK;
                CTable elem = this.ElementAt(0);
                if (elem == null)
                    return Result.OK;
                ConcurrentDictionary<DataObjects.TableTypes, CTableAssociation> _assoc = elem.GetAssociations();
                if (_assoc == null || _assoc.Count <= 0)
                    return Result.OK;

                foreach (var pair in _assoc)
                {
                    Logging.LogDebug("PopulateAssociations: " + GetTableName() + ":" + pair.Value.GetTableName());
                    cmd.CommandText = pair.Value.GetCommandPopulateAssociations();
                    rd = cmd.ExecuteReader(CommandBehavior.KeyInfo);
                    if (rd == null)
                        continue;
                    while (rd.Read())
                    {
                        AddAssociation(rd, pair.Key);
                    }
                    rd.Close();
                }
            }
            catch (Exception ex)
            {
                Logging.LogError("PopulateAssociations ERROR: " + ex.Message);
            }
            finally
            {
                if (rd != null)
                    rd.Close();
                cmd.Dispose();
            }
            return Result.OK;
        }
    }
}
