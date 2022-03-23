using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using log4net;

namespace JakeKnowsEngineComponent
{
	public class StrategyUploadProfile : CStrategyOperation //uploadProfile
	{
		public override bool Execute(WSCall ws, DataObjects obj)
		{
			SqlConnection cn = null;
			Logging.LogDebug("Execute");
            try
            {
				int version = ws.GetVersion();
				string Device = ws.GetParameterAs_string("deviceUID"); //Device 
				string Token = ws.GetParameterAs_string("deviceToken"); //Token 
				string PhoneNumber = ws.GetParameterAs_string("devicePhoneNumber"); //PhoneNumber 
				string FirstName = ws.GetParameterAs_string("contactFirstName"); //FirstName 
				string LastName = ws.GetParameterAs_string("contactLastName"); //LastName 
				string Title = ws.GetParameterAs_string("contactTitle"); //Title 
				string Prefix = ws.GetParameterAs_string("contactPrefix"); //Prefix 
				string Email1 = ws.GetParameterAs_string("contactEmail1"); //Email1 
				string Email2 = ws.GetParameterAs_string("contactEmail2"); //Email2 
				string Email3 = ws.GetParameterAs_string("contactEmail3"); //Email3 
				string Email4 = ws.GetParameterAs_string("contactEmail4"); //Email4 
				string Email5 = ws.GetParameterAs_string("contactEmail5"); //Email5 
				string Email6 = ws.GetParameterAs_string("contactEmail6"); //Email6 
				string Company = ws.GetParameterAs_string("contactOrg"); //Company 
				string AddressHome = ws.GetParameterAs_string("homeAddress1"); //AddressHome 
				string AddressHome2 = ws.GetParameterAs_string("homeAddress2"); //AddressHome2 
				string CityHome = ws.GetParameterAs_string("homeCity"); //CityHome 
				string StateHome = ws.GetParameterAs_string("homeState"); //StateHome 
				string ZipCodeHome = ws.GetParameterAs_string("homePostalCode"); //ZipCodeHome 
				string CountryHome = ws.GetParameterAs_string("homeCountry"); //CountryHome 
				string AddressWork = ws.GetParameterAs_string("workAddress1"); //AddressWork 
				string AddressWork2 = ws.GetParameterAs_string("workAddress2"); //AddressWork2 
				string CityWork = ws.GetParameterAs_string("workCity"); //CityWork 
				string StateWork = ws.GetParameterAs_string("workState"); //StateWork 
				string ZipCodeWork = ws.GetParameterAs_string("workPostalCode"); //ZipCodeWork 
				string CountryWork = ws.GetParameterAs_string("workCountry"); //CountryWork 
				string PhoneWork = ws.GetParameterAs_string("workPhoneNumber"); //PhoneWork 
				string PhoneHome = ws.GetParameterAs_string("homePhoneNumber"); //PhoneHome 
				string PhoneWork2 = ws.GetParameterAs_string("work2PhoneNumber"); //PhoneWork2 
				string PhoneHome2 = ws.GetParameterAs_string("home2PhoneNumber"); //PhoneHome2 
				string PhoneFax = ws.GetParameterAs_string("faxPhoneNumber"); //PhoneFax 
				string PhonePager = ws.GetParameterAs_string("pagerPhoneNumber"); //PhonePager 
				string PhoneOther = ws.GetParameterAs_string("otherPhoneNumber"); //PhoneOther 
				string XMLNote = ws.GetParameterAs_string("noteXML"); //XMLNote 
				bool CompressInput = ws.GetParameterAs_bool("compressionFlagInput"); //CompressInput 

				cn = new SqlConnection(JakeKnowsEngine.ConnectionString);
				cn.Open();
				
				SqlInt64 IDDevice = obj.GetIDFrom(cn, ColumnTypes.TIDDevice, Device);
				SqlInt64 IDPerson_FirstName = obj.GetIDFrom(cn, ColumnTypes.TIDPerson, FirstName);
				SqlInt64 IDPerson_LastName = obj.GetIDFrom(cn, ColumnTypes.TIDPerson, LastName);
				SqlInt64 TYTitle = obj.GetIDFrom(cn, ColumnTypes.TTYTitle, Title);
				SqlInt64 TYPrefix = obj.GetIDFrom(cn, ColumnTypes.TTYPrefix, Prefix);
				SqlInt64 IDEmail_Email1 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email1);
				SqlInt64 IDEmail_Email2 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email2);
				SqlInt64 IDEmail_Email3 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email3);
				SqlInt64 IDEmail_Email4 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email4);
				SqlInt64 IDEmail_Email5 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email5);
				SqlInt64 IDEmail_Email6 = obj.GetIDFrom(cn, ColumnTypes.TIDEmail, Email6);
				SqlInt64 IDCompany = obj.GetIDFrom(cn, ColumnTypes.TIDCompany, Company);
				SqlInt64 IDLocationStreet_AddressHome = obj.GetIDFrom(cn, ColumnTypes.TIDLocationStreet, AddressHome);
				SqlInt64 IDLocationStreet2_AddressHome2 = obj.GetIDFrom(cn, ColumnTypes.TIDLocationStreet2, AddressHome2);
				SqlInt64 IDLocationCity_CityHome = obj.GetIDFrom(cn, ColumnTypes.TIDLocationCity, CityHome);
				SqlInt64 IDLocationState_StateHome = obj.GetIDFrom(cn, ColumnTypes.TIDLocationState, StateHome);
				SqlInt64 IDLocationZipCode_ZipCodeHome = obj.GetIDFrom(cn, ColumnTypes.TIDLocationZipCode, ZipCodeHome);
				SqlInt64 IDLocationCountry_CountryHome = obj.GetIDFrom(cn, ColumnTypes.TIDLocationCountry, CountryHome);
				SqlInt64 IDLocationStreet_AddressWork = obj.GetIDFrom(cn, ColumnTypes.TIDLocationStreet, AddressWork);
				SqlInt64 IDLocationStreet2_AddressWork2 = obj.GetIDFrom(cn, ColumnTypes.TIDLocationStreet2, AddressWork2);
				SqlInt64 IDLocationCity_CityWork = obj.GetIDFrom(cn, ColumnTypes.TIDLocationCity, CityWork);
				SqlInt64 IDLocationState_StateWork = obj.GetIDFrom(cn, ColumnTypes.TIDLocationState, StateWork);
				SqlInt64 IDLocationZipCode_ZipCodeWork = obj.GetIDFrom(cn, ColumnTypes.TIDLocationZipCode, ZipCodeWork);
				SqlInt64 IDLocationCountry_CountryWork = obj.GetIDFrom(cn, ColumnTypes.TIDLocationCountry, CountryWork);
				SqlInt64 IDPhone_PhoneWork = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneWork);
				SqlInt64 IDPhone_PhoneHome = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneHome);
				SqlInt64 IDPhone_PhoneWork2 = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneWork2);
				SqlInt64 IDPhone_PhoneHome2 = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneHome2);
				SqlInt64 IDPhone_PhoneFax = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneFax);
				SqlInt64 IDPhone_PhonePager = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhonePager);
				SqlInt64 IDPhone_PhoneOther = obj.GetIDFrom(cn, ColumnTypes.TIDPhone, PhoneOther);

				cn.Close();
				cn.Dispose();
            }
            catch (Exception e)
            {
                log.Error("Exception " + e.Message);
            }
			finally
			{
				if (cn != null)
				{
					if (cn.State != System.Data.ConnectionState.Closed)
						cn.Close();
					cn.Dispose();
				}
			}
            return true;
		}
	}
}
