using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data; 
using System.Web.UI.HtmlControls;   
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using Telerik.Web.UI;
using System.Web.Security;

/// <summary>
/// Summary description for Repository
/// </summary>
/// 
namespace Repository
{   

    public class Struct
    {
        public Struct()
        {
            //
            // TODO: Add constructor logic here
            //
        }


        public struct UserInfo
        {
            public int Id;
            public bool IsExist;
            public string FirstName;
            public string LastName;
            public string Email;
            public string Username;
            public string Password;
            public DateTime BirthDate;
            public int RoleID;
            public bool IsDisable;
            public string CreatedBy;
            public DateTime CreateDate;
            public DateTime LastUpdateDate;

        }
        public struct ActiveSession
        {
            public bool IsActive;
            public string FirstName;
            public string LastName;
            public string Email;
            public string Username;
            public int RoleID;
            public DateTime CreateDate;
            public DateTime LastUpdateDate;
        }
        public struct SpResultset
        {
            public bool Isresult;
            public string Error;
            public string scope;
        }


        public struct CheckMember
        {
            public bool IsActive;
            public string MemberID;
            public string Name;
        	public string Shop;
            public string HiredDate;
        }

        public struct Searchshop
        {
            public int tab;
            public int Column;
            public int Filter;
            public string tag;
            public bool ShowAll;            
            public bool allowpaging;
            public int pageno;
            public int pagerno;
            public string sortorder;

        }

        public struct Searchmember
        {
            public int tab;
            public int Column;
            public int Filter;
            public string tag;
            public bool ShowAll;
            public bool allowpaging;
            public bool IsCalendar;
            public string startdate;
            public string enddate;
            public int pageno;
            public int pagerno;
            public string sortorder;
            public int sortmode;
        }
      

    }
    public class Session
    {
        public Session()
        {

        }


        
        public static void IntializeSession()
        {
            Repository.Struct.ActiveSession active = new Struct.ActiveSession(); 
            active.IsActive = false;
            active.FirstName = "";
            active.LastName = "";
            active.Email = "";
            active.Username = "";
            active.RoleID = 0;
            active.CreateDate = Convert.ToDateTime("2000-01-01 00:00:00");
            active.LastUpdateDate = Convert.ToDateTime("2000-01-01 00:00:00");
            HttpContext.Current.Session["Ufcw_Userinfo"] = active;

        }

        public static void ClearSession()
        {
            HttpContext.Current.Session.Clear();
        }

        private static object GetFromSession(string key)
        {
            if (HttpContext.Current == null || HttpContext.Current.Session == null)
            {
                return null;
            }
            return HttpContext.Current.Session[key];
        }


        public static void UpdateInSession(string key, object value)
        {
            Repository.Struct.UserInfo UserRepository = (Repository.Struct.UserInfo)value;
            Repository.Struct.ActiveSession active = new Repository.Struct.ActiveSession();
            active.IsActive = true;
            active.FirstName = UserRepository.FirstName;
            active.LastName = UserRepository.LastName;
            active.Email = UserRepository.Email;
            active.Username = UserRepository.Username;
            active.RoleID = UserRepository.RoleID;
            active.CreateDate = UserRepository.CreateDate;
            active.LastUpdateDate = UserRepository.LastUpdateDate;
            HttpContext.Current.Session[key] = active;
        }
        public static void CheckSession(string key)
        {
            object UserSession = GetFromSession(key);

            if (UserSession != null)
            {
               Repository.Struct.ActiveSession active = (Repository.Struct.ActiveSession)UserSession;
                if (!active.IsActive)
                {
                    Repository.Session.ClearSession();
                    Repository.Redirector.GoToExpirePage();
                }
            }
            else
            {
                Repository.Session.ClearSession();
                Repository.Redirector.GoToExpirePage();
            }
        }


        public static string GetContentInSessionby(string Getby)
        {
            string content = "";
            object UserSession = GetFromSession("Ufcw_Userinfo");

            if (UserSession != null)
            {
                Repository.Struct.ActiveSession  active = (Repository.Struct.ActiveSession)UserSession;
                if (active.IsActive)
                {
                    switch (Getby)
                    {
                        case "Name":
                            content = active.FirstName + " " + active.LastName;
                            break;
                        case "Email":
                            content = active.Email;
                            break;
                        case "Createdby":
                            content = active.Username;
                            break;
                        case "RoleID":
                            content = active.RoleID.ToString();
                            break;

                    }
                }
            }
            return content;
        }
    }
    public class Connection
    {
        public Connection()
        {
            //
            // TODO: Add constructor logic here
            //
        }

     
        public static string DBConnectionString()
        {
            string strConn = ConfigurationManager.ConnectionStrings["UfcwConnectionString"].ConnectionString;
            return strConn;
        }

        public static DataSet GetDataSet(string strSQL)
        {
            SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
            DataSet ds = new DataSet();
            sdpPrd.Fill(ds);
            return ds;
        }
        public static bool GetDataSet_withoutID(string strSQL)
        {
            bool result;
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            int j = cmd.ExecuteNonQuery();
            conn.Close();
            if (j > 0)
            {
                result = true;
            }
            else
            {
                result = false;
            }

            return result;
        }
        public static int GetDataSet_withID(string strSQL)
        {
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            cmd.ExecuteNonQuery();
            cmd = new SqlCommand("SELECT @@IDENTITY", conn);
            string appId = cmd.ExecuteScalar().ToString();
            int nId = Convert.ToInt32(appId);
            conn.Close();
            return nId;
        }
        public static int GetDataSet_reader(string strSQL)
        {
            int id = 0;
            string query = strSQL;
            SqlConnection conn = new SqlConnection(DBConnectionString());
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd = new SqlCommand(query, conn);
            SqlDataReader rd = cmd.ExecuteReader();
            rd.Read();

            if (rd.HasRows)
            {
                id = Convert.ToInt32(rd["id"].ToString());
            }
            else
            {
                id = 0;
            }
            conn.Close();
            return id;
        }
        public static DataSet GetMultipleDataset(string strSQL, DataSet ds, string tablename)
        {
            SqlDataAdapter sdpPrd = new SqlDataAdapter(strSQL, DBConnectionString());
            sdpPrd.Fill(ds, tablename);
            return ds;
        }

    
    }
    public class En_De_cryptor
    {
        public En_De_cryptor()
        {
            //
            // TODO: Add constructor logic here
            //
        }



        public static string ComputeHash(string plainText, string hashAlgorithm, byte[] saltBytes)
        {
            hashAlgorithm = "SHA512";
            // If salt is not specified, generate it.
            if (saltBytes == null)
            {
                // Define min and max salt sizes.
                int minSaltSize = 4;
                int maxSaltSize = 8;

                // Generate a random number for the size of the salt.
                Random random = new Random();
                int saltSize = random.Next(minSaltSize, maxSaltSize);

                // Allocate a byte array, which will hold the salt.
                saltBytes = new byte[saltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(saltBytes);
            }

            // Convert plain text into a byte array.
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // Allocate array, which will hold plain text and salt.
            byte[] plainTextWithSaltBytes =
            new byte[plainTextBytes.Length + saltBytes.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < plainTextBytes.Length; i++)
                plainTextWithSaltBytes[i] = plainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < saltBytes.Length; i++)
                plainTextWithSaltBytes[plainTextBytes.Length + i] = saltBytes[i];

            HashAlgorithm hash;

            // Make sure hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Initialize appropriate hashing algorithm class.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hash = new SHA384Managed();
                    break;

                case "SHA512":
                    hash = new SHA512Managed();
                    break;

                default:
                    hash = new MD5CryptoServiceProvider();
                    break;
            }

            // Compute hash value of our plain text with appended salt.
            byte[] hashBytes = hash.ComputeHash(plainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] hashWithSaltBytes = new byte[hashBytes.Length +
            saltBytes.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < hashBytes.Length; i++)
                hashWithSaltBytes[i] = hashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < saltBytes.Length; i++)
                hashWithSaltBytes[hashBytes.Length + i] = saltBytes[i];

            // Convert result into a base64-encoded string.
            string hashValue = Convert.ToBase64String(hashWithSaltBytes);

            // Return the result.
            return hashValue;
        }

        public static bool VerifyHash(string plainText, string hashAlgorithm, string hashValue)
        {
            hashAlgorithm = "SHA512";
            // Convert base64-encoded hash value into a byte array.
            byte[] hashWithSaltBytes = Convert.FromBase64String(hashValue);

            // We must know size of hash (without salt).
            int hashSizeInBits, hashSizeInBytes;

            // Make sure that hashing algorithm name is specified.
            if (hashAlgorithm == null)
                hashAlgorithm = "";

            // Size of hash is based on the specified algorithm.
            switch (hashAlgorithm.ToUpper())
            {

                case "SHA384":
                    hashSizeInBits = 384;
                    break;

                case "SHA512":
                    hashSizeInBits = 512;
                    break;

                default: // Must be MD5
                    hashSizeInBits = 128;
                    break;
            }

            // Convert size of hash from bits to bytes.
            hashSizeInBytes = hashSizeInBits / 8;

            // Make sure that the specified hash value is long enough.
            if (hashWithSaltBytes.Length < hashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] saltBytes = new byte[hashWithSaltBytes.Length - hashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < saltBytes.Length; i++)
                saltBytes[i] = hashWithSaltBytes[hashSizeInBytes + i];

            // Compute a new hash string.
            string expectedHashString = ComputeHash(plainText, hashAlgorithm, saltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (hashValue == expectedHashString);
        }


    }
    public class Redirector
    {
        public Redirector()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void GoToHomePage()
        {
            Redirect("~/Default.aspx");
        }

        public static void GoToExpirePage()
        {
            Redirect("~/Expired.aspx");
        }
        public static void GoToPage(int Id)
        {
            switch (Id)
            {
                case 1://admin
                    Redirect("~/Admin/Account/Add.aspx");
                    break;
                case 2://Full control
                  //  Redirect("~/Common/Shops/Browse.aspx");
                    Redirect("~/Common/Dashboard/Home.aspx");
                    break;
                case 3://Read
                    Redirect("~/Common/Shops/Browse.aspx");
                    break;
                case 4://V/E Shops
                    Redirect("~/Common/Shops/Browse.aspx");
                    break;
                case 5://V/E Members
                    Redirect("~/Common/Members/Browse.aspx");
                    break;
                case 6://V/H/T Members
                    Redirect("~/Common/Members/Browse.aspx");
                    break;
            }

        }

        public static void Redirect(String path)
        {
            HttpContext.Current.Response.Redirect(path);
        }
    }

    public class Roles
    {
        public Roles()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        public static DataSet GetRole_ds()
        {
            string query = "SELECT * FROM [Role] order by Name";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);

            return ds;
        }
        public static void GetRole_cb(RadComboBox option)
        {
            DataSet ds = GetRole_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RoleID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }    

    }
    public class Users
    {
        public Users()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public static string Isvalid(string Username, string Password)
        {
            string msg = "";
            Struct.UserInfo UserInfo = new Struct.UserInfo();
            UserInfo = GetUserBy(Username);

            //verify user with  UserRepository
            if (UserInfo.IsExist)
            {
                bool Isvalid = Repository.En_De_cryptor.VerifyHash(Password, "", UserInfo.Password);
                if (Isvalid)
                {
                    UpdateLastLogin(UserInfo.Id);  
                    //clear session
                    Repository.Session.ClearSession();
                    Repository.Session.UpdateInSession("Ufcw_Userinfo", UserInfo);
                    msg = "Successful";
                    FormsAuthentication.RedirectFromLoginPage(UserInfo.Username, true);
                    Redirector.GoToPage(UserInfo.RoleID);

                }
                else { msg = "Login failed!"; }
            }
            else { msg = "Login failed!"; }
            return msg;
        }

        public static void UpdateLastLogin(int Id)
        {
            string query = "UPDATE [Accounts] set LastLogin='" + DateTime.Now + "' where AccountID="+Id;
            Connection.GetDataSet_withoutID(query);
        }   
        
        public static Repository.Struct.UserInfo GetUserBy(string Username)
        {
            Repository.Struct.UserInfo UserInfo = new Repository.Struct.UserInfo();

            string query = "SELECT * FROM [Accounts] where Username='" + Username.Replace("'", "''") + "' AND IsDisable=0";
            DataSet ds = new DataSet();


            ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                UserInfo.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["AccountID"].ToString());
                UserInfo.IsExist = true;
                UserInfo.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                UserInfo.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                UserInfo.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                UserInfo.Username = ds.Tables[0].Rows[0]["Username"].ToString();
                UserInfo.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["BirthDate"].ToString()))
                {
                    UserInfo.BirthDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"].ToString());
                }
                UserInfo.RoleID = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleID"].ToString());
                UserInfo.CreatedBy = ds.Tables[0].Rows[0]["CreatedBy"].ToString();
                UserInfo.CreateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["CreateDate"].ToString());
                UserInfo.LastUpdateDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LastUpdateDate"].ToString());

            }
            else
            { UserInfo.IsExist = false; }

            return UserInfo;
        }

        public static Repository.Struct.SpResultset Register(string @FirstName, string @LastName, string @Email, string @Username, string @Password, string @BirthDate, int @RoleID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Accounts_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = @FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = @LastName;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = @Email;
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 30).Value = @Username;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, -1).Value = @Password;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @BirthDate;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = @RoleID;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

    }
    public class Menu
    {
        public Menu()
        {
        }
        public static int GetPriority(String RoleID)
        {
            int value = 0;
            string query = "SELECT  max(Priority)as Priority  FROM [Menu] WHERE ReferenceID=" + RoleID;
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            if (!string.IsNullOrEmpty(ds.Tables[0].Rows[0]["Priority"].ToString()))
            {
                value = Convert.ToInt32(ds.Tables[0].Rows[0]["Priority"].ToString());
            }
            else { value = 0; }
            value += 1;
            return value;
        }
        public static DataSet GetMenu_sp()
        {
            DataSet ds = new DataSet();
            string role = Repository.Session.GetContentInSessionby("RoleID");
            if (role == "")
            { role = "0"; }
                string query = "SELECT * FROM [Menu] WHERE ReferenceID=" + role +" Order by Priority";               
                ds = Connection.GetDataSet(query);              
            
            return ds;
        }
        public static Repository.Struct.SpResultset Add_MenuItem(string @Name, string @Url, string @Priority, int @RoleID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Menu_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 30).Value = @Name;
            cmd.Parameters.Add("@Url", SqlDbType.NVarChar, -1).Value = @Url;
            cmd.Parameters.Add("@Priority", SqlDbType.NVarChar, 150).Value = @Priority;
            cmd.Parameters.Add("@RoleID", SqlDbType.Int).Value = @RoleID;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static void ActionBar(LinkButton Browse,LinkButton Add,LinkButton Edit,LinkButton View)
        {

            string role = Repository.Session.GetContentInSessionby("RoleID");
            switch (role)
            {
                case "2":
                    break;
                case "3":
                    if (Add != null)
                    {
                        Add.Visible = false;
                    }
                    if (Edit != null)
                    {
                        Edit.Visible = false;
                    }
                    break;
                case "4":
                    if (Add != null)
                    {
                        Add.Visible = false;
                    }
                    break;
                case "5":
                    if (Add != null)
                    {
                        Add.Visible = false;
                    }
                    break;
                case "6":
                    if (Edit != null)
                    {
                        Edit.Visible = false;
                    }
                    break;
                default:
                    break;

            }
        }
        public static void GridBar(HyperLink View, HyperLink Edit, LinkButton Delete)
        {

            string role = Repository.Session.GetContentInSessionby("RoleID");
            switch (role)
            {
                case "2":
                    break;
                case "3":
                    if (Edit != null)
                    {
                        Edit.Visible = false;
                    }
                    if (Delete != null)
                    {
                        Delete.Visible = false;
                    }
                    break;
                case "4":
                    if (Delete != null)
                    {
                        Delete.Visible = false;
                    }
                    break;
                case "5":                  
                    if (Delete != null)
                    {
                        Delete.Visible = false;
                    }
                    break;
                case "6":
                    if (Edit != null)
                    {
                        Edit.Visible = false;
                    }
                    break;
                default:
                    break;

            }
        }
        public static void HideTab(RadTabStrip tab)
        {
            string role = Repository.Session.GetContentInSessionby("RoleID");
            switch (role)
            {
                case "2":
                    break;
                case "3":                   
                    break;
                case "4":
                    if (tab != null)
                    {                        
                        tab.Tabs[1].Visible = false;
                        tab.Tabs[2].Visible = false;
                        tab.Tabs[3].Visible = false; 
                    }
                    break;
                case "5":                   
                    break;
                case "6":                   
                    break;
                default:
                    break;

            }
        }
        public static void disableControls(RadNumericTextBox @ShopID, TextBox @Name, TextBox @Pri_Address, TextBox @Pri_City, TextBox @Pri_State, TextBox @Pri_Zip, TextBox @Pri_Zip_Plus4, TextBox @Sec_Address, TextBox @Sec_City, TextBox @Sec_State, TextBox @Sec_Zip, TextBox @Sec_Zip_Plus4, RadMaskedTextBox @Pri_Phone, RadMaskedTextBox @Pri_Fax, TextBox @Pri_Email, RadMaskedTextBox @Sec_Phone, RadMaskedTextBox @Sec_Fax, TextBox @Sec_Email, RadComboBox @DelegateID, TextBox @OLPD, TextBox @OLPH, RadMonthYearPicker @LPD, RadMonthYearPicker @LPH, RadDatePicker @Contract_Start, RadDatePicker @Contract_End,TextBox @Pri_Extn,TextBox @Sec_Extn)
        {
            string role = Repository.Session.GetContentInSessionby("RoleID");
            switch (role)
            {
                case "2":
                    break;
                case "3":
                    break;
                case "4":
                    @ShopID.Enabled = false;
                    @Name.Enabled = false;
                    @Pri_Address.Enabled = false;
                    @Pri_City.Enabled = false;
                    @Pri_State.Enabled = false;
                    @Pri_Zip.Enabled = false;
                    @Pri_Zip_Plus4.Enabled = false;
                    @Sec_Address.Enabled = false;
                    @Sec_City.Enabled = false;
                    @Sec_State.Enabled = false;
                    @Sec_Zip.Enabled = false;
                    @Sec_Zip_Plus4.Enabled = false;
                    @Pri_Phone.Enabled = false;
                    @Pri_Fax.Enabled = false;
                    @Pri_Email.Enabled = false;
                    @Sec_Phone.Enabled = false;
                    @Sec_Fax.Enabled = false;
                    @Sec_Email.Enabled = false;
                    DelegateID.Enabled = false;
                    @OLPD.Enabled = true;
                    @OLPH.Enabled = true;
                    @LPD.Enabled = true;
                    @LPH.Enabled = true;
                    @Contract_Start.Enabled = false;
                    @Contract_End.Enabled = false;
                    @Pri_Extn.Enabled = false;
                    @Sec_Extn.Enabled = false;
                    break;
                case "5":
                    break;
                case "6":
                    break;
                default:
                    break;
            }

        }
    }
    public class Shops
    {

        public Shops()
        {
            //
            // TODO: Add constructor logic here
            //

        }

         public static void ShopDelete(string ReferenceID)
        {
            int Rec_Result;
                         
                SqlConnection con = new SqlConnection(Connection.DBConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand("Shops_delete_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@ReferenceID", ReferenceID);
              

               Rec_Result = cmd.ExecuteNonQuery();
               if (Rec_Result == 1)
               {
                  
               }
                //Rec_Result = scope.Value.ToString();
               con.Close();

         }
            
            

        #region dropdown
        public static void GetShop_cb(RadComboBox option)
        {
            DataSet ds = GetShop_ds(false);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }
        public static void GetShopReport_cb(RadComboBox option)
        {
            DataSet ds = GetShop_ds(false);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item1 = new RadComboBoxItem("Select", "0");
            RadComboBoxItem item2 = new RadComboBoxItem("All", "");
            option.Items.Add(item1);
            option.Items.Add(item2);
            option.DataBind();

        }

        public static void GetShop_Auto(RadAutoCompleteBox option)
        {
            DataSet ds = GetShop_ds(false);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.Filter=(RadAutoCompleteFilter)Enum.Parse(typeof(RadAutoCompleteFilter), "StartsWith", true);

        }

        public static DataSet GetShop_ds(bool IsDisable)
        {
            string query = "SELECT " +
                "a.[RecordID],a.[ShopID],a.[Name],b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4] " +
                ",b.[Sec_Address],b.[Sec_City],b.[Sec_State],b.[Sec_Zip],b.[Sec_Zip_Plus4] " +
                ",b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email] " +
                ",d.[Name] as Delegate,right(convert(varchar(20),c.[LPD],06), len(convert(varchar(20),c.[LPD],06)) -3) as LPD,ISNULL(right(convert(varchar(20),c.[LPH],06), len(convert(varchar(20),c.[LPH],06))-3),'N/A') as LPH, convert(varchar(12),c.[Contract_Start],101) as Contract_Start, convert(varchar(12),c.[Contract_End],101) as Contract_End,c.[OLPD] +'-'+ c.[OLPH] as OpenMonths " +
                "FROM dbo.Shops as a inner join " +
                        "dbo.Shops_Address as b on b.ReferenceID = a.RecordID inner join " +
                        "dbo.Shops_Delegate as c on c.ReferenceID = a.RecordID left join dbo.Delegate as d on d.RecordID = c.DelegateID ";
            //string query = "SELECT " +
            //     "a.[RecordID],a.[ShopID],a.[Name],b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4] " +
            //     ",b.[Sec_Address],b.[Sec_City],b.[Sec_State],b.[Sec_Zip],b.[Sec_Zip_Plus4] " +
            //     ",b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email] " +
            //     ",d.[Name] as Delegate,convert(varchar(12),c.[OpenMonths],101) as OpenMonths ,convert(varchar(12),c.[OpenMonths],101) as OLPD,convert(varchar(12),c.[OLPH],101) as OLPH, convert(varchar(12),c.[LPD],101) as LPD, convert(varchar(12),c.[LPH],101) as LPH, convert(varchar(12),c.[Contract_Start],101) as Contract_Start, convert(varchar(12),c.[Contract_End],101) as Contract_End " +
            //     "FROM dbo.Shops as a inner join " +
            //             "dbo.Shops_Address as b on b.ReferenceID = a.RecordID inner join " +
            //             "dbo.Shops_Delegate as c on c.ReferenceID = a.RecordID inner join dbo.Delegate as d on d.RecordID = c.DelegateID";
            if (IsDisable)
            {
                query += " WHERE a.IsDisable=1 ";
            }
            else { query += " WHERE a.IsDisable=0 "; }
            query += "order by a.[RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            return ds;


        }  

        public static string Get_ShopIDbyName(string ShopName)
        {
            string ShopID="";
            string query = "select Top 1 RecordID from Shops where Name='" + ShopName + "'";
            DataSet ds = new DataSet();
            ds = Repository.Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                ShopID = ds.Tables[0].Rows[0]["RecordID"].ToString();
            }
            return ShopID;
        }

        #endregion

        #region Information Tab
        public static Repository.Struct.SpResultset Shops_Add(string @ShopID, string @Name, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Sec_Address, string @Sec_City, string @Sec_State, string @Sec_Zip, string @Sec_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email, string @Sec_Phone, string @Sec_Fax, string @Sec_Email, int @DelegateID, string @OLPD, string @OLPH, String @LPD, string @LPH, string @Contract_Start, string @Contract_End,string Pri_Extn,string Sec_Extn)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = @Name;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = @Sec_Address;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = @Sec_City;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = @Sec_State;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = @Sec_Zip;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = @Sec_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = @Sec_Phone;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = @Sec_Fax;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = @Sec_Email;
            cmd.Parameters.Add("@Pri_Extn", SqlDbType.NVarChar, 15).Value = @Pri_Extn;
            cmd.Parameters.Add("@Sec_Extn", SqlDbType.NVarChar, 15).Value = @Sec_Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");
            cmd.Parameters.Add("@DelegateID", SqlDbType.Int).Value = @DelegateID;
            cmd.Parameters.Add("@OLPD", SqlDbType.NVarChar, 500).Value = @OLPD;
            cmd.Parameters.Add("@OLPH", SqlDbType.NVarChar, 500).Value = @OLPH;
            cmd.Parameters.Add("@LPD", SqlDbType.SmallDateTime).Value = @LPD;
            cmd.Parameters.Add("@LPH", SqlDbType.SmallDateTime).Value = @LPH;
            cmd.Parameters.Add("@Contract_Start", SqlDbType.SmallDateTime).Value = @Contract_Start;
            cmd.Parameters.Add("@Contract_End", SqlDbType.SmallDateTime).Value = @Contract_End;


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Shops_Update(string @RecordID, string @ShopID, string @Name, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Sec_Address, string @Sec_City, string @Sec_State, string @Sec_Zip, string @Sec_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email, string @Sec_Phone, string @Sec_Fax, string @Sec_Email, int @DelegateID, string @OLPD, string @OLPH, String @LPD, string @LPH, string @Contract_Start, string @Contract_End,string @Pri_Extn,string @Sec_Extn)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = Isnull(@RecordID);
            cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = Isnull(@ShopID);
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = @Name;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = @Sec_Address;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = @Sec_City;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = @Sec_State;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = @Sec_Zip;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = @Sec_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = @Sec_Phone;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = @Sec_Fax;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = @Sec_Email;
            cmd.Parameters.Add("@Pri_Extn", SqlDbType.NVarChar, 15).Value = @Pri_Extn;
            cmd.Parameters.Add("@Sec_Extn", SqlDbType.NVarChar, 15).Value = @Sec_Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");
            cmd.Parameters.Add("@DelegateID", SqlDbType.Int).Value = @DelegateID;
            cmd.Parameters.Add("@OLPD", SqlDbType.NVarChar, 500).Value = @OLPD;
            cmd.Parameters.Add("@OLPH", SqlDbType.NVarChar, 500).Value = @OLPH;
            cmd.Parameters.Add("@LPD", SqlDbType.SmallDateTime).Value = @LPD;
            cmd.Parameters.Add("@LPH", SqlDbType.SmallDateTime).Value = @LPH;
            cmd.Parameters.Add("@Contract_Start", SqlDbType.SmallDateTime).Value = @Contract_Start;
            cmd.Parameters.Add("@Contract_End", SqlDbType.SmallDateTime).Value = @Contract_End;


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Shops_InActive(string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "InActive";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 500).Value = null;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");
            cmd.Parameters.Add("@DelegateID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@OLPD", SqlDbType.NVarChar, 500).Value = null;
            cmd.Parameters.Add("@OLPH", SqlDbType.NVarChar, 500).Value = null;
            cmd.Parameters.Add("@LPD", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@LPH", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Contract_Start", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Contract_End", SqlDbType.SmallDateTime).Value = null;


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }                   
              
        #endregion

        #region Contacts Tab
        public static void Browse_Contact(RadGrid grid, string RecordID)
        {
            string query = "SELECT a.[RecordID],a.[ReferenceID],a.[Name],a.[Type],a.[Other],a.[Phone],a.[Fax],a.[Mobile],a.[Email],a.[PhoneExtn] as Extn FROM dbo.Shops_Contact as a WHERE a.ReferenceID=" + RecordID + " order by a.[RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;           
        }
        public static Repository.Struct.SpResultset InsertContact(string @ReferenceID, string @Name, string @Type,string @Other, string @Phone, string @Fax, string @Mobile, string @Email,string @Extn)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Contacts_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 70).Value = @Name;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @Type;
            cmd.Parameters.Add("@Other", SqlDbType.NVarChar, 70).Value = @Other; 
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = @Phone;
            cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 15).Value = @Fax;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 15).Value = @Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = @Email;
            cmd.Parameters.Add("@Extn", SqlDbType.NVarChar, 15).Value = @Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset UpdateContact(string @ReferenceID, string @Name, string @Type,string @Other,string @Phone, string @Fax, string @Mobile, string @Email,string @Extn)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Contacts_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 70).Value = @Name;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @Type;
            cmd.Parameters.Add("@Other", SqlDbType.NVarChar, 70).Value = @Other;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = @Phone;
            cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 15).Value = @Fax;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 15).Value = @Mobile;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = @Email;
            cmd.Parameters.Add("@Extn", SqlDbType.NVarChar, 15).Value = @Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset DeleteContact(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Contacts_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 70).Value = null;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Other", SqlDbType.NVarChar, 70).Value = null;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Mobile", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@Extn", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        #endregion

        #region Init & Dues Tab     
        public static void Browse_Fee(RadGrid grid, string RecordID)
        {
            string query = "SELECT [RecordID],[ReferenceID], CONVERT(DECIMAL(19,2),[Init_FullTime]) as Init_FullTime , CONVERT(DECIMAL(19,2),[Init_PartTime]) as Init_PartTime, CONVERT(DECIMAL(19,2),[Due_FullTime]) as Due_FullTime, CONVERT(DECIMAL(19,2),[Due_PartTime]) as Due_PartTime,convert(varchar(12),[Effective_Date],101) as Effective_Date FROM dbo.Shops_FeeStructure WHERE ReferenceID=" + RecordID + " order by [RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
            //grid.DataBind();  

        }
        public static Repository.Struct.SpResultset InsertFee(string @ReferenceID, decimal Init_FullTime, decimal @Init_PartTime, decimal @Due_FullTime, decimal @Due_PartTime, string @Effective_Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_InitFee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Init_FullTime", SqlDbType.Decimal).Value = @Init_FullTime;
            cmd.Parameters.Add("@Init_PartTime", SqlDbType.Decimal).Value = @Init_PartTime;
            cmd.Parameters.Add("@Due_FullTime", SqlDbType.Decimal).Value = @Due_FullTime;
            cmd.Parameters.Add("@Due_PartTime", SqlDbType.Decimal).Value = @Due_PartTime;
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = @Effective_Date;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset UpdateFee(string @ReferenceID, decimal Init_FullTime, decimal @Init_PartTime, decimal @Due_FullTime, decimal @Due_PartTime, string @Effective_Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_InitFee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Init_FullTime", SqlDbType.Decimal).Value = @Init_FullTime;
            cmd.Parameters.Add("@Init_PartTime", SqlDbType.Decimal).Value = @Init_PartTime;
            cmd.Parameters.Add("@Due_FullTime", SqlDbType.Decimal).Value = @Due_FullTime;
            cmd.Parameters.Add("@Due_PartTime", SqlDbType.Decimal).Value = @Due_PartTime;
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = @Effective_Date;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset DeleteFee(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_InitFee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Init_FullTime", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Init_PartTime", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Due_FullTime", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Due_PartTime", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        #endregion

        #region Benefits Tab    
        public static DataSet Select_ShopBenefits(string @ReferenceID)
        {
            string query = "SELECT * FROM [dbo].[Shops_Benefits] WHERE ReferenceID=" + @ReferenceID;
            DataSet ds = Connection.GetDataSet(query);
            return ds;
        }
        public static Repository.Struct.SpResultset InsertBenefit(string @Benefitxml, string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Benefit_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Shops_Benefit_XmlDoc", SqlDbType.NVarChar, -1).Value = @Benefitxml;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }       
        public static Repository.Struct.SpResultset UpdateBenefitType(string @BenefitType, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("RadioSelection_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "BenefitType_Shops";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @BenefitType;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset NoBenefit(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Shops_Benefit_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "NoBenefits";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Shops_Benefit_XmlDoc", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        #endregion

        #region Browse Section
        public static string filter(int type, string column,string tag)
        {
            string query = "";
            switch (type)
            {
              
                case 0://contains
                    query = column + "like" + "'%" + tag + "%'";
                    break;
                case 1://startwith
                    query = column + "like" + "'" + tag + "%'";
                    break;
                case 2://endwith
                    query = column + "like" + "'%" + tag + "'";
                    break;
            }
            return query; 
        }
        public static void Browse_Shop(RadGrid grid, string IsDisable, Repository.Struct.Searchshop criteria)
        {
            
            string query = "SELECT " +
             "a.[RecordID],a.[ShopID],a.[Name],b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4] " +
             ",b.[Sec_Address],b.[Sec_City],b.[Sec_State],b.[Sec_Zip],b.[Sec_Zip_Plus4] " +
             ",b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email] " +
             ",d.[Name] as Delegate,right(convert(varchar(20),c.[LPD],06), len(convert(varchar(20),c.[LPD],06)) -3) as LPD,ISNULL(right(convert(varchar(20),c.[LPH],06), len(convert(varchar(20),c.[LPH],06))-3),'N/A') as LPH, convert(varchar(12),c.[Contract_Start],101) as Contract_Start, convert(varchar(12),c.[Contract_End],101) as Contract_End,c.[OLPD] +'-'+ c.[OLPH] as OpenMonths " +
             "FROM dbo.Shops as a inner join " +
                     "dbo.Shops_Address as b on b.ReferenceID = a.RecordID inner join " +
                     "dbo.Shops_Delegate as c on c.ReferenceID = a.RecordID left join dbo.Delegate as d on d.RecordID = c.DelegateID ";
            switch (IsDisable)
            {
                case "true":
                    query += " WHERE a.IsDisable=1 ";
                    break;
                case "false":
                    query += " WHERE a.IsDisable=0 ";
                    break;
                default:
                    break;

            }
           
            if (!criteria.ShowAll)
            {
                if (criteria.Column != 0)
                {
                    switch (IsDisable)
                    {
                        case "":
                            query += " WHERE  ";
                            break;
                        default:
                            query += " AND ";
                            break;

                    }
                    switch (criteria.Column)
                    {
                        case 0:
                            break;
                        case 1://shopID                 
                            query += filter(criteria.Filter, "a.[ShopID]", criteria.tag);
                            break;
                        case 2://Name
                            query += filter(criteria.Filter, "a.[Name]", criteria.tag);
                            break;
                        case 3://Delegate
                            query += filter(criteria.Filter, "d.[Name]", criteria.tag);
                            break;
                    }
                }
            }

            if (criteria.sortorder == "Desc")
            {
                query += "Order by a.[Name] Desc";
            }
            else
            {
                query += "Order by a.[Name] Asc";
            }
            

           // query += "order by a.[RecordID]";
            DataSet ds = Connection.GetDataSet(query);         
            grid.DataSource = ds;

        }
        public static bool Select_Shop(RadNumericTextBox @ShopID, TextBox @Name, TextBox @Pri_Address, TextBox @Pri_City, TextBox @Pri_State, TextBox @Pri_Zip, TextBox @Pri_Zip_Plus4, TextBox @Sec_Address, TextBox @Sec_City, TextBox @Sec_State, TextBox @Sec_Zip, TextBox @Sec_Zip_Plus4, RadMaskedTextBox @Pri_Phone, RadMaskedTextBox @Pri_Fax, TextBox @Pri_Email, RadMaskedTextBox @Sec_Phone, RadMaskedTextBox @Sec_Fax, TextBox @Sec_Email, RadComboBox @DelegateID, TextBox @OLPD, TextBox @OLPH, RadMonthYearPicker @LPD, RadMonthYearPicker @LPH, RadDatePicker @Contract_Start, RadDatePicker @Contract_End, string @RecordID, RadioButtonList Benefittype, TextBox @Pri_Extn, TextBox @Sec_Extn)
        {
            bool Isvalid = false;
            string query = "SELECT " +
                      "a.[RecordID],a.[BenefitType],a.[ShopID],a.[Name],b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4] " +
                      ",b.[Sec_Address],b.[Sec_City],b.[Sec_State],b.[Sec_Zip],b.[Sec_Zip_Plus4] " +
                      ",b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email],b.[Pri_Phone_Ext],b.[Sec_Phone_Ext] " +
                      ",d.[RecordID] as DelegateID,c.[OLPD],c.[OLPH],c.[LPD],c.[LPH],c.[Contract_Start],c.[Contract_End] " +
                      "FROM dbo.Shops as a inner join " +
                              "dbo.Shops_Address as b on b.ReferenceID = a.RecordID inner join " +
                              "dbo.Shops_Delegate as c on c.ReferenceID = a.RecordID inner join dbo.Delegate as d on d.RecordID = c.DelegateID";

            query += " WHERE a.[RecordID]=" + RecordID;


            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                @ShopID.Text = ds.Tables[0].Rows[0]["ShopID"].ToString();
                @Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                @Pri_Address.Text = ds.Tables[0].Rows[0]["Pri_Address"].ToString();
                @Pri_City.Text = ds.Tables[0].Rows[0]["Pri_City"].ToString();
                @Pri_State.Text = ds.Tables[0].Rows[0]["Pri_State"].ToString();
                @Pri_Zip.Text = ds.Tables[0].Rows[0]["Pri_Zip"].ToString();
                @Pri_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Pri_Zip_Plus4"].ToString();
                @Sec_Address.Text = ds.Tables[0].Rows[0]["Sec_Address"].ToString();
                @Sec_City.Text = ds.Tables[0].Rows[0]["Sec_City"].ToString();
                @Sec_State.Text = ds.Tables[0].Rows[0]["Sec_State"].ToString();
                @Sec_Zip.Text = ds.Tables[0].Rows[0]["Sec_Zip"].ToString();
                @Sec_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Sec_Zip_Plus4"].ToString();
                @Pri_Phone.Text = ds.Tables[0].Rows[0]["Pri_Phone"].ToString();
                @Pri_Fax.Text = ds.Tables[0].Rows[0]["Pri_Fax"].ToString();
                @Pri_Email.Text = ds.Tables[0].Rows[0]["Pri_Email"].ToString();
                @Sec_Phone.Text = ds.Tables[0].Rows[0]["Sec_Phone"].ToString();
                @Sec_Fax.Text = ds.Tables[0].Rows[0]["Sec_Fax"].ToString();
                @Sec_Email.Text = ds.Tables[0].Rows[0]["Sec_Email"].ToString();
                DelegateID.SelectedValue = ds.Tables[0].Rows[0]["DelegateID"].ToString();
                @OLPD.Text =ds.Tables[0].Rows[0]["OLPD"].ToString();
                @OLPH.Text = ds.Tables[0].Rows[0]["OLPH"].ToString();
                @Pri_Extn.Text = ds.Tables[0].Rows[0]["Pri_Phone_Ext"].ToString();
                @Sec_Extn.Text = ds.Tables[0].Rows[0]["Sec_Phone_Ext"].ToString();
                
                if (ds.Tables[0].Rows[0]["LPD"].ToString() != null && ds.Tables[0].Rows[0]["LPD"].ToString() != "")
                {
                    @LPD.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LPD"].ToString());
                }
                if (ds.Tables[0].Rows[0]["LPH"].ToString() != null && ds.Tables[0].Rows[0]["LPH"].ToString() != "")
                {
                    @LPH.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["LPH"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Contract_Start"].ToString() != null && ds.Tables[0].Rows[0]["Contract_Start"].ToString() != "")
                {
                    @Contract_Start.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Contract_Start"].ToString());
                }
                if (ds.Tables[0].Rows[0]["Contract_End"].ToString() != null && ds.Tables[0].Rows[0]["Contract_End"].ToString() != "")
                {
                    @Contract_End.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["Contract_End"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BenefitType"].ToString() != null && ds.Tables[0].Rows[0]["BenefitType"].ToString() != "")
                {
                    Benefittype.SelectedValue = ds.Tables[0].Rows[0]["BenefitType"].ToString();
                }
                Isvalid = true;
            }
            return Isvalid;
        }
      
       
        #endregion

        #region common
        private static string Isnull(string var)
        {
            string result = "";
            if (string.IsNullOrEmpty(var))
            {
                result = null;
            }
            else { result = var; }

            return result;
        }
        #endregion

        #region Notes
        public static Repository.Struct.SpResultset Notes(string @Note,bool @Chkflag,string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Shops";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = @Note;
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = @Chkflag;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static void GetNotes(RadGrid grid, string @RecordID)
        {
            string query = "SELECT RecordID,[Note],[CreatedBy],convert(varchar(12),[CreateDate],101) as CreateDate,Flag FROM Shops_Notes WHERE ReferenceID=" + RecordID + " and Inactive='1' order by [CreateDate]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
        }
        public static bool GetNotes_byShop(string @RecordID)
        {
            bool Result =false;
            string query = "SELECT RecordID,[Note],[CreatedBy],convert(varchar(12),[CreateDate],101) as CreateDate,Flag FROM Shops_Notes WHERE ReferenceID=" + RecordID + " and Inactive='1' and Flag='1' order by [CreateDate]";
            DataSet ds = Connection.GetDataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Result = true;
            }
            return Result;
            
        }
        public static Repository.Struct.SpResultset UpdateNotes(string @DataID,bool @chkFlag)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "ShopNotesUpdate";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = @DataID;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = "";
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = @chkFlag;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        public static Repository.Struct.SpResultset DeleteNotes(string @DataID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "ShopNotesDelete";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = @DataID;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = "";
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
     
        #endregion

        #region Upload
        public static string Shops_Upload(string Filename, string Fileext, string ReferenceID)
        {
            string Rec_Result = "";
            if (ReferenceID != null && ReferenceID != "")
            {
                SqlConnection con = new SqlConnection(Connection.DBConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand("Shops_upload_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
                cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value =Convert.ToInt32(ReferenceID);
                //cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = Convert.ToInt32(ShopID);
                cmd.Parameters.Add("@FileName", SqlDbType.NVarChar,150).Value = Filename;
                cmd.Parameters.Add("@FileExt", SqlDbType.NVarChar, 30).Value = Fileext;
                cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Add";
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


                //Last Inserted or Updated Record
                SqlParameter scope = new SqlParameter("@output", SqlDbType.Int);
                scope.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(scope);

                cmd.ExecuteNonQuery();
                Rec_Result = scope.Value.ToString();
                con.Close();
            }
            return Rec_Result;
        }
        public static void GetShops_Doc(string ReferenceID, RadGrid grid)
        {
            if (ReferenceID != null && ReferenceID != "")
            {
                string query = "SELECT [RecordID],[ReferenceID],[ShopID] ,[Document],[Extension],[CreateDate] FROM [dbo].[Shops_upload] where ReferenceID='" + ReferenceID + "'";
                DataSet ds = Connection.GetDataSet(query);
                grid.DataSource = ds;
            }
        }
        public static void DeleteShops_Doc(string ReferenceID, string DocID)
        {
            string Rec_Result = "";
            if (ReferenceID != null && ReferenceID != "")
            {
                SqlConnection con = new SqlConnection(Connection.DBConnectionString());
                con.Open();
                SqlCommand cmd = new SqlCommand("Shops_upload_sp", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = Convert.ToInt32(DocID);
                cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = Convert.ToInt32(ReferenceID);
                //cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = Convert.ToInt32(ShopID);
                cmd.Parameters.Add("@FileName", SqlDbType.NVarChar, 150).Value = "";
                cmd.Parameters.Add("@FileExt", SqlDbType.NVarChar, 30).Value = "";
                cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
                cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


                //Last Inserted or Updated Record
                SqlParameter scope = new SqlParameter("@output", SqlDbType.Int);
                scope.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(scope);

                cmd.ExecuteNonQuery();
                Rec_Result = scope.Value.ToString();
                con.Close();
            }
        }
        #endregion




    }
    public class Delegate
    {
        public Delegate()
        {
        }
        public static DataSet GetDelegate_ds()
        {
            string query = "SELECT * FROM [Delegate] where IsDisable=0";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static void GetDelegate_cb(RadComboBox option)
        {
            DataSet ds = GetDelegate_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        } 
        public static Repository.Struct.SpResultset Add_Delegate(string @Name)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Delegate_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 60).Value = @Name;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
    }
    public class ContType
    {
        public ContType()
        {
        }
        public static DataSet GetContactType_ds()
        {
            string query = "SELECT * FROM [ContactType] where IsDisable=0";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static void GetContactType_cb(RadComboBox option)
        {
            DataSet ds = GetContactType_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }
        public static Repository.Struct.SpResultset Add_ContType(string @Name)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_ContactType_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 60).Value = @Name;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
    }
    public class Providers
    {
        public Providers()
        {
        }
        public static DataSet GetProviders_ds()
        {
            string query = "SELECT * FROM [Providers] where IsDisable=0";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static void Browse_Providers(RadGrid grid)
        {
            DataSet ds = GetProviders_ds();
            grid.DataSource = ds;
        }
        public static Repository.Struct.SpResultset Remove_Providers(int @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Provider_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "DELETE";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = null;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        public static void GetProviders_cb(RadComboBox option)
        {
            DataSet ds = GetProviders_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }
        public static Repository.Struct.SpResultset Add_Providers(string @Name, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Provider_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = @Name;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");



            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        public static Repository.Struct.SpResultset Update_Providers(int @RecordID,string @Name, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Provider_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "UPDATE";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = @Name;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static bool Select_Provider(int @RecordID, TextBox @Name, TextBox @Address, TextBox @City, TextBox @State, TextBox @ZIp, TextBox @zip4, RadMaskedTextBox @Phone, RadMaskedTextBox @Fax, TextBox @email)
        {
            bool Isvalid = false;
            string query = "SELECT * FROM [Providers] where RecordID="+@RecordID;


            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                @Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                @Address.Text = ds.Tables[0].Rows[0]["Pri_Address"].ToString();
                @City.Text = ds.Tables[0].Rows[0]["Pri_City"].ToString();
                @State.Text = ds.Tables[0].Rows[0]["Pri_State"].ToString();
                @ZIp.Text = ds.Tables[0].Rows[0]["Pri_Zip"].ToString();
                @zip4.Text = ds.Tables[0].Rows[0]["Pri_Zip_Plus4"].ToString();
                @Phone.Text = ds.Tables[0].Rows[0]["Pri_Phone"].ToString();
                @Fax.Text = ds.Tables[0].Rows[0]["Pri_Fax"].ToString();
                @email.Text = ds.Tables[0].Rows[0]["Pri_Email"].ToString();
                
                

                Isvalid = true;
            }
            return Isvalid;
        }
    }
    public class Benefits
    {
        public Benefits()
        {
        }
        public static DataSet GetBenefits_ds()
        {
            string query = "SELECT a.RecordID,a.Name ,a.Description,b.Name as providedby  FROM dbo.Benefits a inner join dbo.Providers b on a.ProviderID = b.RecordID where a.IsDisable=0";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static void GetBenefits_cb(RadComboBox option)
        {
            DataSet ds = GetBenefits_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }

        public static void GetBenefits_chkbox(CheckBoxList option)
        {
            DataSet ds = GetBenefits_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";

            option.AppendDataBoundItems = true;
            option.DataBind();

        }

        public static DataSet GetBenefitsMagnacare_ds()
        {
            string query = "SELECT a.RecordID,a.Name,a.Description,b.Name as providedby  FROM dbo.Benefits a inner join dbo.Providers b on a.ProviderID = b.RecordID where a.IsDisable=0 and a.Name in ('MM - Magnacare','R – Magnacare','R/MC – Magnacare','MC – Magnacare')";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static void GetBenefitsMagnacare_cb(RadComboBox option)
        {
            DataSet ds = GetBenefitsMagnacare_ds();
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("All", "0");
            option.Items.Add(item);
            option.DataBind();

        }  

        public static void Browse_Benefits(RadGrid grid)
        {
            DataSet ds = GetBenefits_ds();
            grid.DataSource = ds;
        }

        public static void BenefitsByShop(RadGrid grid,string MemberID)
        {
            string query = "SELECT a.RecordID,a.Name ,a.Description,b.Name as providedby  FROM dbo.Benefits a inner join dbo.Providers b on a.ProviderID = b.RecordID where a.RecordID in ("+
                            "SELECT BenefitID  FROM dbo.Shops_Benefits where ReferenceID=(select ShopID from dbo.Members_Hired where RecordID ="+MemberID+" ))";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);            
            grid.DataSource = ds;
        }

        public static Repository.Struct.SpResultset Remove_Benefits(int @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Benefits_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "DELETE";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = null;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@ProviderID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Add_Benefits(string @Name, string @Description,int ProviderID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Benefits_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = @Name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = @Description;
            cmd.Parameters.Add("@ProviderID", SqlDbType.Int).Value = @ProviderID;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }


        public static Repository.Struct.SpResultset Update_Benefits(int @RecordID,string @Name, string @Description, int ProviderID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Admin_Benefits_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "UPDATE";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Name", SqlDbType.NVarChar, 400).Value = @Name;
            cmd.Parameters.Add("@Description", SqlDbType.NVarChar, -1).Value = @Description;
            cmd.Parameters.Add("@ProviderID", SqlDbType.Int).Value = @ProviderID;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        
        public static DataSet PartialBenefits()
        {
            string query = "SELECT [BenefitID] from [dbo].[Benefits_Partial]";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            return ds;
        }
        public static bool Select_Benefits(int @RecordID, TextBox @Name, RadComboBox @Provider, TextBox @Description)
        {
            bool Isvalid = false;
            string query = "SELECT a.RecordID,a.Name ,a.Description,b.Name as providedby,a.ProviderID  FROM dbo.Benefits a inner join dbo.Providers b on a.ProviderID = b.RecordID where a.RecordID="+@RecordID;


            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                @Name.Text = ds.Tables[0].Rows[0]["Name"].ToString();
                @Description.Text = ds.Tables[0].Rows[0]["Description"].ToString();
                @Provider.SelectedValue = ds.Tables[0].Rows[0]["ProviderID"].ToString();
                
                Isvalid = true;
            }
            return Isvalid;
        }
    }
    public class Members
    {

        public Members()
        {
            //
            // TODO: Add constructor logic here
            //

        }

        #region Information Tab
        public static string checkExistingMemberbySSN(string @SSN)
        {
            string MemberID = "0";
            string query = "exec dbo.OpenKeys;Select * from [dbo].[Members] where dbo.Decrypt(SSN)= [dbo].[PadLeftOrLimitSSN]('" + @SSN + "','0',9)";
            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemberID = ds.Tables[0].Rows[0]["RecordID"].ToString();
            }          
            return MemberID; 

        }
        public static bool checkExistingMemberbyStatus(string @ReferenceID)
        {
            bool IsActive = false;
            string query = " SELECT * from dbo.Members_Hired where StatusID = (Select RecordID from dbo.Status where Name='Active') "+
                           " AND ReferenceID ="+@ReferenceID;
            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                IsActive = true;
            }
            return IsActive;
        }

        public static Struct.CheckMember Members_Prepopulate(string @Mid, RadNumericTextBox MemberID, TextBox firstname, TextBox lastname, RadMaskedTextBox SSN, RadDatePicker DOB, RadioButtonList Gender, TextBox Pri_Address, TextBox Pri_City, TextBox Pri_State, TextBox Pri_Zip, TextBox Pri_Zip_Plus4, TextBox Sec_Address, TextBox Sec_City, TextBox Sec_State, TextBox Sec_Zip, TextBox Sec_Zip_Plus4, RadMaskedTextBox Pri_Phone, RadMaskedTextBox Pri_Fax, TextBox Pri_Email, RadMaskedTextBox Sec_Phone, RadMaskedTextBox Sec_Fax, TextBox Sec_Email, Label OrigHiredDate)
        {
            Struct.CheckMember memberinfo = new Struct.CheckMember();         

                memberinfo.IsActive  = checkExistingMemberbyStatus(@Mid);
                string query = "exec dbo.OpenKeys;SELECT a.[MemberID],a.[FirstName],a.[LastName],[dbo].[PadLeftOrLimitSSN](dbo.Decrypt(a.[SSN]),'0',9) as SSN,a.[BirthDate],a.[Gender] , a.[Hired_dt] as OriginalHiredDate " +
                ",b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4],b.[Sec_Address],b.[Sec_City],b.[Sec_State]" +
                ",b.[Sec_Zip],b.[Sec_Zip_Plus4],b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email],convert(varchar(12),c.[HiredDate],101) as HiredDate,d.[Name] as shopname " +
                "FROM " +
                "dbo.Members as a inner join  dbo.Members_Address as b on a.RecordID = b.ReferenceID " +
                 "inner join  dbo.Members_Hired  as c on a.RecordID = c.ReferenceID " +
                 "inner join dbo.Shops as d on d.RecordID  = c.ShopID "+
                "where a.RecordID =" + @Mid;
                DataSet ds = Connection.GetDataSet(query);
                if (ds.Tables[0].Rows.Count > 0)
                {
                    memberinfo.MemberID = ds.Tables[0].Rows[0]["MemberID"].ToString();
                    memberinfo.Name = ds.Tables[0].Rows[0]["firstname"].ToString() + " " + ds.Tables[0].Rows[0]["lastname"].ToString();
                    memberinfo.Shop = ds.Tables[0].Rows[0]["shopname"].ToString();
                    memberinfo.HiredDate = ds.Tables[0].Rows[0]["HiredDate"].ToString(); 
                    if (!memberinfo.IsActive)
                    {
                        MemberID.Text = ds.Tables[0].Rows[0]["MemberID"].ToString();
                        firstname.Text = ds.Tables[0].Rows[0]["firstname"].ToString();
                        lastname.Text = ds.Tables[0].Rows[0]["lastname"].ToString();
                        SSN.Text = ds.Tables[0].Rows[0]["SSN"].ToString();
                        Pri_Address.Text = ds.Tables[0].Rows[0]["Pri_Address"].ToString();
                        Pri_City.Text = ds.Tables[0].Rows[0]["Pri_City"].ToString();
                        Pri_State.Text = ds.Tables[0].Rows[0]["Pri_State"].ToString();
                        Pri_Zip.Text = ds.Tables[0].Rows[0]["Pri_Zip"].ToString();
                        Pri_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Pri_Zip_Plus4"].ToString();
                        Sec_Address.Text = ds.Tables[0].Rows[0]["Sec_Address"].ToString();
                        Sec_City.Text = ds.Tables[0].Rows[0]["Sec_City"].ToString();
                        Sec_State.Text = ds.Tables[0].Rows[0]["Sec_State"].ToString();
                        Sec_Zip.Text = ds.Tables[0].Rows[0]["Sec_Zip"].ToString();
                        Sec_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Sec_Zip_Plus4"].ToString();
                        Pri_Phone.Text = ds.Tables[0].Rows[0]["Pri_Phone"].ToString();
                        Pri_Fax.Text = ds.Tables[0].Rows[0]["Pri_Fax"].ToString();
                        Pri_Email.Text = ds.Tables[0].Rows[0]["Pri_Email"].ToString();
                        Sec_Phone.Text = ds.Tables[0].Rows[0]["Sec_Phone"].ToString();
                        Sec_Fax.Text = ds.Tables[0].Rows[0]["Sec_Fax"].ToString();
                        Sec_Email.Text = ds.Tables[0].Rows[0]["Sec_Email"].ToString();
                        string orgHiredDate = ds.Tables[0].Rows[0]["OriginalHiredDate"].ToString();
                        if (orgHiredDate != null && orgHiredDate.Length > 0)
                        {
                            OrigHiredDate.Text = "Original Hired Date : " + orgHiredDate;
                            OrigHiredDate.Visible = true;
                        }
                        else
                        {
                            OrigHiredDate.Visible = false;
                        }
                        if (ds.Tables[0].Rows[0]["Gender"].ToString() != null && ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                        {
                            Gender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                        }
                        if (ds.Tables[0].Rows[0]["BirthDate"].ToString() != null && ds.Tables[0].Rows[0]["BirthDate"].ToString() != "")
                        {
                            DOB.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"].ToString());
                        }
                    }
                }

                return memberinfo; 
        }

        public static Repository.Struct.SpResultset Members_Add(string @MemberID, string @FirstName, string @LastName, string @Initial, string @SSN, string @BirthDate, string @Gender, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Sec_Address, string @Sec_City, string @Sec_State, string @Sec_Zip, string @Sec_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email, string @Sec_Phone, string @Sec_Fax, string @Sec_Email, string @Pri_Extn, string @Sec_Extn, string @HireDate)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = @FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = @LastName;
            cmd.Parameters.Add("@Initial", SqlDbType.NVarChar, 30).Value = @Initial;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = @SSN;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @BirthDate;
            cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = @Gender;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = @Sec_Address;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = @Sec_City;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = @Sec_State;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = @Sec_Zip;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = @Sec_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = @Sec_Phone;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = @Sec_Fax;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = @Sec_Email;
            cmd.Parameters.Add("@Pri_Extn", SqlDbType.NVarChar, 15).Value = @Pri_Extn;
            cmd.Parameters.Add("@Sec_Extn", SqlDbType.NVarChar, 15).Value = @Sec_Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");
            cmd.Parameters.Add("@HiredDate", SqlDbType.DateTime).Value = @HireDate;

            



            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Members_Update(string @RecordID, string @MemberID, string @FirstName, string @LastName, string @Initial, string @SSN, string @BirthDate, string @Gender, string @Pri_Address, string @Pri_City, string @Pri_State, string @Pri_Zip, string @Pri_Zip_Plus4, string @Sec_Address, string @Sec_City, string @Sec_State, string @Sec_Zip, string @Sec_Zip_Plus4, string @Pri_Phone, string @Pri_Fax, string @Pri_Email, string @Sec_Phone, string @Sec_Fax, string @Sec_Email, string @Pri_Extn, string @Sec_Extn, string @HireDate)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = Isnull(@RecordID);
            cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = Isnull(@MemberID);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = @FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = @LastName;
            cmd.Parameters.Add("@Initial", SqlDbType.NVarChar, 30).Value = @Initial;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = @SSN;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @BirthDate;
            cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = @Gender;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = @Pri_Address;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = @Pri_City;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = @Pri_State;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = @Pri_Zip;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = @Pri_Zip_Plus4;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = @Sec_Address;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = @Sec_City;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = @Sec_State;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = @Sec_Zip;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = @Sec_Zip_Plus4;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = @Pri_Phone;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = @Pri_Fax;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = @Pri_Email;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = @Sec_Phone;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = @Sec_Fax;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = @Sec_Email;
            cmd.Parameters.Add("@Pri_Extn", SqlDbType.NVarChar, 150).Value = @Pri_Extn;
            cmd.Parameters.Add("@Sec_Extn", SqlDbType.NVarChar, 150).Value = @Sec_Extn;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");
            cmd.Parameters.Add("@HiredDate", SqlDbType.DateTime).Value = @HireDate;


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Members_InActive(string @RecordID,string @Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "InActive";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @Date;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Members_Terminate(string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Information_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Terminate";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@MemberID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Pri_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Pri_City", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Pri_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Zip_Plus4", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Address", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@Sec_City ", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@Sec_State", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Zip_Plus4 ", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Pri_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@Sec_Phone", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Fax", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Sec_Email", SqlDbType.NVarChar, 150).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        public static bool View_Members(string @Mid, RadNumericTextBox MemberID, TextBox firstname, TextBox lastname, TextBox initial, RadMaskedTextBox SSN, RadDatePicker DOB, RadioButtonList Gender, TextBox Pri_Address, TextBox Pri_City, TextBox Pri_State, TextBox Pri_Zip, TextBox Pri_Zip_Plus4, TextBox Sec_Address, TextBox Sec_City, TextBox Sec_State, TextBox Sec_Zip, TextBox Sec_Zip_Plus4, RadMaskedTextBox Pri_Phone, RadMaskedTextBox Pri_Fax, TextBox Pri_Email, RadMaskedTextBox Sec_Phone, RadMaskedTextBox Sec_Fax, TextBox Sec_Email, Label lblAletter, Label lblBcertificate, Label lblMcertificate, Label lblauthorize, RadComboBox shop, RadDatePicker hireddate, RadioButtonList Benefittype, RadioButtonList Initiationtype, RadioButtonList status, TextBox Pri_Extn, TextBox Sec_Extn, RadDatePicker AffDate, Label OrigHiredDate)
        {
            bool Isvalid = false;
            string query = "exec dbo.OpenKeys;SELECT c.[MemberID],c.[BenefitType],c.[InitiationType],a.[FirstName],a.[LastName],a.[Initial],[dbo].[PadLeftOrLimitSSN](dbo.Decrypt(a.[SSN]),'0',9) as SSN,a.[BirthDate],a.Gender , a.[Hired_dt] as OriginalHiredDate " +
            ",b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4],b.[Sec_Address],b.[Sec_City],b.[Sec_State]" +
            ",b.[Sec_Zip],b.[Sec_Zip_Plus4],b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email],b.[Pri_Phone_Ext],b.[Sec_Phone_Ext] " +
            ",d.[Contribution],d.[AuthorizationLetter],d.[BirthCertificate],d.[MarriageCertificate],c.[HiredDate],c.[AffiliationDate],c.[ShopID],c.[ApplicableTo] " +
            "FROM " +
            "dbo.Members as a inner join  dbo.Members_Address as b on a.RecordID = b.ReferenceID " +
            "inner join  dbo.Members_Hired  as c on a.RecordID = c.ReferenceID " +
            "left join dbo.Members_upload as d on c.RecordID = d.ReferenceID " +
            "where c.RecordID =" + @Mid;



            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemberID.Text = ds.Tables[0].Rows[0]["MemberID"].ToString();
                firstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                lastname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                initial.Text = ds.Tables[0].Rows[0]["Initial"].ToString();
                SSN.Text = ds.Tables[0].Rows[0]["SSN"].ToString();
                Pri_Address.Text = ds.Tables[0].Rows[0]["Pri_Address"].ToString();
                Pri_City.Text = ds.Tables[0].Rows[0]["Pri_City"].ToString();
                Pri_State.Text = ds.Tables[0].Rows[0]["Pri_State"].ToString();
                Pri_Zip.Text = ds.Tables[0].Rows[0]["Pri_Zip"].ToString();
                Pri_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Pri_Zip_Plus4"].ToString();
                Sec_Address.Text = ds.Tables[0].Rows[0]["Sec_Address"].ToString();
                Sec_City.Text = ds.Tables[0].Rows[0]["Sec_City"].ToString();
                Sec_State.Text = ds.Tables[0].Rows[0]["Sec_State"].ToString();
                Sec_Zip.Text = ds.Tables[0].Rows[0]["Sec_Zip"].ToString();
                Sec_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Sec_Zip_Plus4"].ToString();
                Pri_Phone.Text = ds.Tables[0].Rows[0]["Pri_Phone"].ToString();
                Pri_Fax.Text = ds.Tables[0].Rows[0]["Pri_Fax"].ToString();
                Pri_Email.Text = ds.Tables[0].Rows[0]["Pri_Email"].ToString();
                Sec_Phone.Text = ds.Tables[0].Rows[0]["Sec_Phone"].ToString();
                Sec_Fax.Text = ds.Tables[0].Rows[0]["Sec_Fax"].ToString();
                Sec_Email.Text = ds.Tables[0].Rows[0]["Sec_Email"].ToString();
                lblAletter.Text = ds.Tables[0].Rows[0]["AuthorizationLetter"].ToString();
                lblBcertificate.Text = ds.Tables[0].Rows[0]["BirthCertificate"].ToString();
                lblMcertificate.Text = ds.Tables[0].Rows[0]["MarriageCertificate"].ToString();
                lblauthorize.Text = ds.Tables[0].Rows[0]["Contribution"].ToString();
                shop.SelectedValue = ds.Tables[0].Rows[0]["ShopID"].ToString();
                Pri_Extn.Text = ds.Tables[0].Rows[0]["Pri_Phone_Ext"].ToString();
                Sec_Extn.Text = ds.Tables[0].Rows[0]["Sec_Phone_Ext"].ToString();

                string orgHiredDate = ds.Tables[0].Rows[0]["OriginalHiredDate"].ToString();
                if (orgHiredDate != null && orgHiredDate.Length > 0)
                {
                    OrigHiredDate.Text = "Original Hired Date : " + orgHiredDate;
                    OrigHiredDate.Visible = true;
                }
                else
                {
                    OrigHiredDate.Visible = false;
                }
                
                if (ds.Tables[0].Rows[0]["ApplicableTo"].ToString() != null && ds.Tables[0].Rows[0]["ApplicableTo"].ToString() != "")
                {
                    status.SelectedValue = ds.Tables[0].Rows[0]["ApplicableTo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gender"].ToString() != null && ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    Gender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HiredDate"].ToString() != null && ds.Tables[0].Rows[0]["HiredDate"].ToString() != "")
                {
                    hireddate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["HiredDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AffiliationDate"].ToString() != null && ds.Tables[0].Rows[0]["AffiliationDate"].ToString() != "")
                {
                    AffDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AffiliationDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthDate"].ToString() != null && ds.Tables[0].Rows[0]["BirthDate"].ToString() != "")
                {
                    DOB.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BenefitType"].ToString() != null && ds.Tables[0].Rows[0]["BenefitType"].ToString() != "")
                {
                    Benefittype.SelectedValue = ds.Tables[0].Rows[0]["BenefitType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InitiationType"].ToString() != null && ds.Tables[0].Rows[0]["InitiationType"].ToString() != "")
                {
                    Initiationtype.SelectedValue = ds.Tables[0].Rows[0]["InitiationType"].ToString();
                }

                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool Load_Information(string @Mid, RadNumericTextBox MemberID, TextBox firstname, TextBox lastname,TextBox initial, RadMaskedTextBox SSN, RadDatePicker DOB, RadioButtonList Gender, TextBox Pri_Address, TextBox Pri_City, TextBox Pri_State, TextBox Pri_Zip, TextBox Pri_Zip_Plus4, TextBox Sec_Address, TextBox Sec_City, TextBox Sec_State, TextBox Sec_Zip, TextBox Sec_Zip_Plus4, RadMaskedTextBox Pri_Phone, RadMaskedTextBox Pri_Fax, TextBox Pri_Email, RadMaskedTextBox Sec_Phone, RadMaskedTextBox Sec_Fax, TextBox Sec_Email, RadComboBox shop, RadDatePicker hireddate,RadioButtonList status,Label SSNlbl,TextBox Pri_Extn,TextBox Sec_Extn,RadDatePicker AffDate, Label OrigHiredDate)
        {
            bool Isvalid = false;
            string query = "exec dbo.OpenKeys;SELECT c.[MemberID],c.[BenefitType],c.[InitiationType],a.[FirstName],a.[LastName],a.[Initial],[dbo].[PadLeftOrLimitSSN](dbo.Decrypt(a.[SSN]),'0',9) as SSN,a.[BirthDate],a.Gender , a.[Hired_dt] as OriginalHiredDate " +
            ",b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4],b.[Sec_Address],b.[Sec_City],b.[Sec_State]" +
            ",b.[Sec_Zip],b.[Sec_Zip_Plus4],b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email],b.[Pri_Phone_Ext],b.[Sec_Phone_Ext] " +
            ",d.[Contribution],d.[AuthorizationLetter],d.[BirthCertificate],d.[MarriageCertificate],c.[HiredDate],c.[AffiliationDate],c.[ShopID],c.[ApplicableTo] " +
            "FROM " +
            "dbo.Members as a inner join  dbo.Members_Address as b on a.RecordID = b.ReferenceID " +
            "inner join  dbo.Members_Hired  as c on a.RecordID = c.ReferenceID " +
            "left join dbo.Members_upload as d on c.RecordID = d.ReferenceID " +
            "where c.RecordID =" + @Mid;



            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                MemberID.Text = ds.Tables[0].Rows[0]["MemberID"].ToString();
                firstname.Text = ds.Tables[0].Rows[0]["FirstName"].ToString();
                lastname.Text = ds.Tables[0].Rows[0]["LastName"].ToString();
                initial.Text = ds.Tables[0].Rows[0]["Initial"].ToString();
                SSN.Text = ds.Tables[0].Rows[0]["SSN"].ToString();
                SSNlbl.Text = ds.Tables[0].Rows[0]["SSN"].ToString();
                Pri_Address.Text = ds.Tables[0].Rows[0]["Pri_Address"].ToString();
                Pri_City.Text = ds.Tables[0].Rows[0]["Pri_City"].ToString();
                Pri_State.Text = ds.Tables[0].Rows[0]["Pri_State"].ToString();
                Pri_Zip.Text = ds.Tables[0].Rows[0]["Pri_Zip"].ToString();
                Pri_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Pri_Zip_Plus4"].ToString();
                Sec_Address.Text = ds.Tables[0].Rows[0]["Sec_Address"].ToString();
                Sec_City.Text = ds.Tables[0].Rows[0]["Sec_City"].ToString();
                Sec_State.Text = ds.Tables[0].Rows[0]["Sec_State"].ToString();
                Sec_Zip.Text = ds.Tables[0].Rows[0]["Sec_Zip"].ToString();
                Sec_Zip_Plus4.Text = ds.Tables[0].Rows[0]["Sec_Zip_Plus4"].ToString();
                Pri_Phone.Text = ds.Tables[0].Rows[0]["Pri_Phone"].ToString();
                Pri_Fax.Text = ds.Tables[0].Rows[0]["Pri_Fax"].ToString();
                Pri_Email.Text = ds.Tables[0].Rows[0]["Pri_Email"].ToString();
                Sec_Phone.Text = ds.Tables[0].Rows[0]["Sec_Phone"].ToString();
                Sec_Fax.Text = ds.Tables[0].Rows[0]["Sec_Fax"].ToString();
                Sec_Email.Text = ds.Tables[0].Rows[0]["Sec_Email"].ToString();
                shop.SelectedValue = ds.Tables[0].Rows[0]["ShopID"].ToString();
                Pri_Extn.Text = ds.Tables[0].Rows[0]["Pri_Phone_Ext"].ToString();
                Sec_Extn.Text = ds.Tables[0].Rows[0]["Sec_Phone_Ext"].ToString();

                string orgHiredDate = ds.Tables[0].Rows[0]["OriginalHiredDate"].ToString();
                if (orgHiredDate != null && orgHiredDate.Length > 0)
                {
                    OrigHiredDate.Text = "Original Hired Date : " + orgHiredDate;
                    OrigHiredDate.Visible = true;
                }
                else
                {
                    OrigHiredDate.Visible = false;
                }
                if (ds.Tables[0].Rows[0]["ApplicableTo"].ToString() != null && ds.Tables[0].Rows[0]["ApplicableTo"].ToString() != "")
                {
                    status.SelectedValue = ds.Tables[0].Rows[0]["ApplicableTo"].ToString();
                }
                if (ds.Tables[0].Rows[0]["Gender"].ToString() != null && ds.Tables[0].Rows[0]["Gender"].ToString() != "")
                {
                    Gender.SelectedValue = ds.Tables[0].Rows[0]["Gender"].ToString();
                }
                if (ds.Tables[0].Rows[0]["HiredDate"].ToString() != null && ds.Tables[0].Rows[0]["HiredDate"].ToString() != "")
                {
                    hireddate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["HiredDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["AffiliationDate"].ToString() != null && ds.Tables[0].Rows[0]["AffiliationDate"].ToString() != "")
                {
                    AffDate.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["AffiliationDate"].ToString());
                }
                if (ds.Tables[0].Rows[0]["BirthDate"].ToString() != null && ds.Tables[0].Rows[0]["BirthDate"].ToString() != "")
                {
                    DOB.SelectedDate = Convert.ToDateTime(ds.Tables[0].Rows[0]["BirthDate"].ToString());
                }              

                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool Load_Authorization(string @Mid, Label lblAletter, Label lblBcertificate, Label lblMcertificate, Label lblauthorize)
        {
            bool Isvalid = false;
            string query = "SELECT c.[MemberID],c.[BenefitType],c.[InitiationType],a.[FirstName],a.[LastName],a.[SSN],a.[BirthDate],a.Gender" +
            ",b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4],b.[Sec_Address],b.[Sec_City],b.[Sec_State]" +
            ",b.[Sec_Zip],b.[Sec_Zip_Plus4],b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email] " +
            ",d.[Contribution],d.[AuthorizationLetter],d.[BirthCertificate],d.[MarriageCertificate],c.[HiredDate],c.[ShopID] " +
            "FROM " +
            "dbo.Members as a inner join  dbo.Members_Address as b on a.RecordID = b.ReferenceID " +
            "inner join  dbo.Members_Hired  as c on a.RecordID = c.ReferenceID " +
            "left join dbo.Members_upload as d on c.RecordID = d.ReferenceID " +
            "where c.RecordID =" + @Mid;



            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {              
                lblAletter.Text = ds.Tables[0].Rows[0]["AuthorizationLetter"].ToString();
                lblBcertificate.Text = ds.Tables[0].Rows[0]["BirthCertificate"].ToString();
                lblMcertificate.Text = ds.Tables[0].Rows[0]["MarriageCertificate"].ToString();
                lblauthorize.Text = ds.Tables[0].Rows[0]["Contribution"].ToString();            

                Isvalid = true;
            }
            return Isvalid;
        }
        public static bool Load_Type(string @Mid, RadioButtonList Benefittype, RadioButtonList Initiationtype)
        {
            bool Isvalid = false;
            string query = "SELECT c.[MemberID],c.[BenefitType],c.[InitiationType],a.[FirstName],a.[LastName],a.[SSN],a.[BirthDate],a.Gender" +
            ",b.[Pri_Address],b.[Pri_City],b.[Pri_State],b.[Pri_Zip],b.[Pri_Zip_Plus4],b.[Sec_Address],b.[Sec_City],b.[Sec_State]" +
            ",b.[Sec_Zip],b.[Sec_Zip_Plus4],b.[Pri_Phone],b.[Pri_Fax],b.[Pri_Email],b.[Sec_Phone],b.[Sec_Fax],b.[Sec_Email] " +
            ",d.[Contribution],d.[AuthorizationLetter],d.[BirthCertificate],d.[MarriageCertificate],c.[HiredDate],c.[ShopID] " +
            "FROM " +
            "dbo.Members as a inner join  dbo.Members_Address as b on a.RecordID = b.ReferenceID " +
            "inner join  dbo.Members_Hired  as c on a.RecordID = c.ReferenceID " +
            "left join dbo.Members_upload as d on c.RecordID = d.ReferenceID " +
            "where c.RecordID =" + @Mid;



            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {              
                if (ds.Tables[0].Rows[0]["BenefitType"].ToString() != null && ds.Tables[0].Rows[0]["BenefitType"].ToString() != "")
                {
                    Benefittype.SelectedValue = ds.Tables[0].Rows[0]["BenefitType"].ToString();
                }
                if (ds.Tables[0].Rows[0]["InitiationType"].ToString() != null && ds.Tables[0].Rows[0]["InitiationType"].ToString() != "")
                {
                    Initiationtype.SelectedValue = ds.Tables[0].Rows[0]["InitiationType"].ToString();
                }

                Isvalid = true;
            }
            return Isvalid;
        }
     
        #endregion

        #region Shops Tab
        public static DataSet GetHiredMembers_ds(string MemberId)
        {
            string query = "SELECT b.MemberID,b.FirstName,b.LastName,b.SSN,convert(varchar(12),b.BirthDate,101) as BirthDate,convert(varchar(12),b.CreateDate,101) as CreateDate,convert(varchar(12),a.HiredDate,101) as HiredDate,c.Name as Shop,d.Name as Status " +
                            "FROM " +
                            "dbo.Members_Hired as a inner join dbo.Members as b on  a.ReferenceID = b.RecordID inner join " +
                            "dbo.Shops as c on a.ShopID = c.RecordID " +
                            "inner join dbo.Status as d on a.StatusID = d.RecordID "+ 
                            "where a.ReferenceID=" + MemberId;
            DataSet ds = Connection.GetDataSet(query);
            return ds;

        }
        public static void HiredMembersbyID(RadGrid grid, string MemberId)
        {
            DataSet ds = GetHiredMembers_ds(MemberId);
            grid.DataSource = ds;
        }
        public static Repository.Struct.SpResultset Add_Members_shop(string @ReferenceID, string @ShopID, string @HiredDate,string @AffDate)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Shops_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = Isnull(@ShopID);
            cmd.Parameters.Add("@HiredDate", SqlDbType.SmallDateTime).Value = @HiredDate;
            cmd.Parameters.Add("@AffDate", SqlDbType.SmallDateTime).Value = @AffDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset Update_Members_shop(string @RecordID, string @ReferenceID, string @ShopID, string @HiredDate,string @AffDate)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Shops_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@ShopID", SqlDbType.Int).Value = Isnull(@ShopID);
            cmd.Parameters.Add("@HiredDate", SqlDbType.SmallDateTime).Value = @HiredDate;
            cmd.Parameters.Add("@AffDate", SqlDbType.SmallDateTime).Value = @AffDate;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        #endregion      

        #region Benefits Tab
        public static DataSet Select_MemberBenefits(string @ReferenceID)
        {
            string query = "SELECT * FROM [dbo].[Members_Benefits] WHERE ReferenceID=" + @ReferenceID;
            DataSet ds = Connection.GetDataSet(query);
            return ds;
        }       
        public static Repository.Struct.SpResultset InsertBenefit(string @Benefitxml, string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Benefit_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "INSERT";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Members_Benefit_XmlDoc", SqlDbType.NVarChar, -1).Value = @Benefitxml;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset UpdateBenefitType(string @BenefitType, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("RadioSelection_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "BenefitType_Members";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @BenefitType;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset UpdateApplicableTo(string @ApplicableTo, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("RadioSelection_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "ApplicableTo";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @ApplicableTo;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset NoBenefit(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Benefit_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "NoBenefits";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Members_Benefit_XmlDoc", SqlDbType.NVarChar, -1).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        #endregion       

        #region Rate tab
        public static void Browse_Rate(RadGrid grid, string RecordID)
        {
            string query = "SELECT [RecordID],[ReferenceID], CONVERT(DECIMAL(19,2),[Rate]) as Rate , CONVERT(DECIMAL(19,2),[Family]) as Family, CONVERT(DECIMAL(19,2),[Fringe]) as Fringe,convert(varchar(12),[Effective_Date],101) as Effective_Date FROM dbo.Members_Rate WHERE ReferenceID=" + RecordID + " order by [RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
            //grid.DataBind();  

        }
        public static Repository.Struct.SpResultset InsertRate(string @ReferenceID, decimal @Rate, decimal @Family, decimal @Fringe, string @Effective_Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Rate_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = @Rate;
            cmd.Parameters.Add("@Family", SqlDbType.Decimal).Value = @Family;
            cmd.Parameters.Add("@Fringe", SqlDbType.Decimal).Value = @Fringe;            
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = @Effective_Date;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset UpdateRate(string @ReferenceID, decimal @Rate, decimal @Family, decimal @Fringe, string @Effective_Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Rate_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = @Rate;
            cmd.Parameters.Add("@Family", SqlDbType.Decimal).Value = @Family;
            cmd.Parameters.Add("@Fringe", SqlDbType.Decimal).Value = @Fringe;  
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = @Effective_Date;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset DeleteRate(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Rate_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Rate", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Family", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Fringe", SqlDbType.Decimal).Value = null;  
            cmd.Parameters.Add("@Effective_Date", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        #endregion

        #region Dependency Tab
        public static void GetRelationship_cb(RadComboBox option)
        {
            string query = "SELECT * FROM [Relationship] order by Name";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }
        public static void GetDependence_cb(RadComboBox option, string RecordID)
        {
            option.Items.Clear();  
            string query = "SELECT [RecordID], [FirstName]+' '+ [LastName] as Name FROM dbo.Members_Dependence WHERE ReferenceID=" +RecordID+" order by Name";
            DataSet ds = new DataSet();
            ds = Connection.GetDataSet(query);
            option.DataSource = ds;
            option.DataTextField = "Name";
            option.DataValueField = "RecordID";
            option.AppendDataBoundItems = true;
            RadComboBoxItem item = new RadComboBoxItem("Select", "0");
            option.Items.Add(item);
            option.DataBind();

        }
        public static void Browse_Dependency(RadGrid grid, string RecordID)
        {
            string query = "exec dbo.OpenKeys;SELECT a.[RecordID],a.[ReferenceID],a.[FirstName],a.[LastName],convert(varchar(12),a.[BirthDate],101) as BirthDate,[dbo].[PadLeftOrLimitSSN](dbo.Decrypt(a.[SSN]),'0',9) as SSN,a.[Relationship],a.[Gender],a.[Upload],a.[Beneficiary] FROM dbo.Members_Dependence as a WHERE a.ReferenceID=" + RecordID + " order by a.[RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
            //grid.DataBind();
        }
        public static Repository.Struct.SpResultset InsertDependency(string @ReferenceID, string @FirstName, string @LastName, string @BirthDate, string @SSN, string @Relationship,string @Gender,bool @Beneficiary)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Dependency_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Insert";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = @FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = @LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = @Gender;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @BirthDate;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = @SSN;
            cmd.Parameters.Add("@Relationship", SqlDbType.Int).Value = @Relationship;
            cmd.Parameters.Add("@Beneficiary", SqlDbType.Bit).Value = @Beneficiary;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset UpdateDependency(string @ReferenceID, string @FirstName, string @LastName, string @BirthDate, string @SSN, string @Relationship,string @Gender,bool @Beneficiary)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Dependency_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = @FirstName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = @LastName;
            cmd.Parameters.Add("@Gender", SqlDbType.Int).Value = @Gender;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = @BirthDate;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = @SSN;
            cmd.Parameters.Add("@Relationship", SqlDbType.Int).Value = @Relationship;
            cmd.Parameters.Add("@Beneficiary", SqlDbType.Bit).Value = @Beneficiary;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset DeleteDependency(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Dependency_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 30).Value = null;
            cmd.Parameters.Add("@BirthDate", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@SSN", SqlDbType.NVarChar, 15).Value = null;
            cmd.Parameters.Add("@Relationship", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Beneficiary", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset docupload(string @upload, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("UploadSelection_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Dependence";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Type", SqlDbType.Bit).Value = @upload;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        #endregion

        #region Initiation Tab
        public static Repository.Struct.SpResultset UpdateInitiationType(string @Type, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("RadioSelection_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "InitiationFee";
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Type", SqlDbType.Int).Value = @Type;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static bool IsDisable(string RecordID)
        {            
            bool result = false;
            string query = "select (Select Amount from dbo.FeeStructure)as amount ,(select sum(Amount) as partial from dbo.Members_Fee where ReferenceID="+ RecordID +") as partial";
            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                string stramount = ds.Tables[0].Rows[0]["amount"].ToString();
                string strpartial =ds.Tables[0].Rows[0]["partial"].ToString();
                if (stramount != "" && strpartial != "")
                {
                    decimal amount = Convert.ToDecimal(ds.Tables[0].Rows[0]["amount"].ToString());
                    decimal partial = Convert.ToDecimal(ds.Tables[0].Rows[0]["partial"].ToString());
                    decimal balance = amount - partial;
                    if (balance == 0)
                    {
                        result = true;
                    }
                }
                else { result = false; }
                
            }
            return result;
            
        }
        public static decimal GetFee()
        {
            decimal result = 0;
            string query = "SELECT [Amount] FROM [dbo].[FeeStructure]";
            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString());
            }
            return result;
        }
        public static decimal GetPaidAmount(string RecordID, string mode)
        {
            decimal result = 0;
            string query = "SELECT Sum([Amount]) as Amount FROM dbo.Members_Fee WHERE ReferenceID=" + RecordID + " AND Mode='" + mode + "'";
            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["Amount"].ToString() != "")
                {
                    result = Convert.ToDecimal(ds.Tables[0].Rows[0]["Amount"].ToString());
                }
            }
            return result; 
        }
        public static void Browse_Initiation(RadGrid grid, string RecordID,string mode)
        {
            string query = "SELECT [RecordID],[ReferenceID], [Mode], CONVERT(DECIMAL(19,2),[Amount]) as Amount,convert(varchar(12),[Date],101) as Date,[Cost] FROM dbo.Members_Fee WHERE ReferenceID=" + RecordID + " AND Mode='" + mode + "'  order by [RecordID]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
            //grid.DataBind();
        }
        public static Repository.Struct.SpResultset FullFee(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Full";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = GetFee();
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = GetFee();

            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }    
        public static Repository.Struct.SpResultset InsertFullFee(string @ReferenceID, string @Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Full";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = GetFee();     
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = @Date;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = GetFee();
         
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset InsertPartialFee(string @ReferenceID,string @Amount,string @Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "PartialInsert";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = @Amount;           
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = @Date;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = GetFee(); 

            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset UpdatePartialFee(string @ReferenceID, string @Amount, string @Date)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "PartialUpdate";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = @Amount;
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = @Date;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = GetFee();

            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset DeletePartialFee(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Delete";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        public static Repository.Struct.SpResultset NoFee(string @ReferenceID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Fee_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Clear";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Amount", SqlDbType.Decimal).Value = null;
            cmd.Parameters.Add("@Date", SqlDbType.SmallDateTime).Value = null;
            cmd.Parameters.Add("@Cost", SqlDbType.Decimal).Value = null;

            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;

        }
        #endregion


        #region Authorization Tab
        public static Repository.Struct.SpResultset Add_upload(string @ReferenceID,bool @Contribution,bool @AuthorizationLetter,bool @BirthCertificate ,bool @MarriageCertificate )
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Members_Authorization_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Update";
            cmd.Parameters.Add("@ReferenceID", SqlDbType.Int).Value = @ReferenceID;
            cmd.Parameters.Add("@Contribution", SqlDbType.Bit).Value = @Contribution;
            cmd.Parameters.Add("@AuthorizationLetter", SqlDbType.Bit).Value = @AuthorizationLetter;
            cmd.Parameters.Add("@BirthCertificate", SqlDbType.Bit).Value = @BirthCertificate;
            cmd.Parameters.Add("@MarriageCertificate", SqlDbType.Bit).Value = @MarriageCertificate; 
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");           

            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }
            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        #endregion

        #region Browse
        public static string filter(int type, string column, string tag)
        {
            string query = "";
            switch (type)
            {

                case 0://contains
                    query = column + "like" + "'%" + tag + "%'";
                    break;
                case 1://startwith
                    query = column + "like" + "'" + tag + "%'";
                    break;
                case 2://endwith
                    query = column + "like" + "'%" + tag + "'";
                    break;
            }
            return query;
        }
        public static DataSet GetMembers_ds(string status)
        {
            //string column = "";
            //switch (status)
            //{
            //    case "InActive":
            //        column = ",convert(varchar(12),a.TerminatedDate,101) as TerminatedDate";
            //        break;
            //    default:
            //        column = ",convert(varchar(12),a.HiredDate,101) as HiredDate";
            //        break;
            //}
            string query = "exec dbo.OpenKeys; SELECT b.RecordID as Rid,a.RecordID as Mid, a.MemberID,b.FirstName,b.LastName,b.Initial,[dbo].[Decrypt](b.SSN) as SSN,convert(varchar(12),b.BirthDate,101) as BirthDate,convert(varchar(12),b.CreateDate,101) as CreateDate,c.Name as Shop,convert(varchar(12),a.HiredDate,101) as HiredDate ,convert(varchar(12),a.TerminatedDate,101) as TerminatedDate " +
                            " FROM " +
                            "dbo.Members_Hired as a inner join dbo.Members as b on  a.ReferenceID = b.RecordID inner join " +
                            "dbo.Shops as c on a.ShopID = c.RecordID " +
                             "where a.StatusID =( " +
                            "SELECT RecordID FROM dbo.Status where Name='" + status + "')";
            DataSet ds = Connection.GetDataSet(query);
            return ds;


        }
        public static void Browse_MembersbyStatus(RadGrid grid, string Status, Repository.Struct.Searchmember criteria)
        {
            string query = "";
            if (Status != "")
            {
                query = "exec dbo.OpenKeys; SELECT b.RecordID as Rid,a.RecordID as Mid, a.MemberID,b.FirstName,b.LastName,b.Initial,[dbo].[PadLeftOrLimitSSN]([dbo].[Decrypt](b.[SSN]),'0',9) as SSN,convert(varchar(12),b.BirthDate,101) as BirthDate,convert(varchar(12),b.CreateDate,101) as CreateDate,c.Name as Shop,convert(varchar(12),a.HiredDate,101) as HiredDate ,convert(varchar(12),a.TerminatedDate,101) as TerminatedDate " +
                                " FROM " +
                                "dbo.Members_Hired as a inner join dbo.Members as b on  a.ReferenceID = b.RecordID inner join " +
                                "dbo.Shops as c on a.ShopID = c.RecordID " +
                                 "where a.StatusID =( " +
                                "SELECT RecordID FROM dbo.Status where Name='" + Status + "')";
                if (criteria.IsCalendar)
                {
                    query += "  AND (convert(varchar(12),b.CreateDate,101) between '" + criteria.startdate + "' and '" + criteria.enddate + "')";
                }
            }
            else
            {
                query = "exec dbo.OpenKeys; SELECT b.RecordID as Rid,a.RecordID as Mid, a.MemberID,b.FirstName,b.LastName,b.Initial,[dbo].[PadLeftOrLimitSSN]([dbo].[Decrypt](b.[SSN]),'0',9) as SSN,convert(varchar(12),b.BirthDate,101) as BirthDate,convert(varchar(12),b.CreateDate,101) as CreateDate,c.Name as Shop,convert(varchar(12),a.HiredDate,101) as HiredDate ,convert(varchar(12),a.TerminatedDate,101) as TerminatedDate " +
                               " FROM " +
                               "dbo.Members_Hired as a inner join dbo.Members as b on  a.ReferenceID = b.RecordID inner join " +
                               "dbo.Shops as c on a.ShopID = c.RecordID ";

                if (criteria.IsCalendar)
                {
                   // query += " where (convert(varchar(12),b.CreateDate,101) between convert(varchar(12)," + criteria.startdate + ",101) and convert(varchar(12)," + criteria.enddate + ",101))";
                    query += " where (cast(b.CreateDate as datetime) between cast('" + criteria.startdate + "' as datetime) and cast('" + criteria.enddate + "' as datetime))";
                }
                else
                {
                   // query += " where (convert(varchar(12),b.CreateDate,101) not between convert(varchar(12)," + criteria.startdate + ",101) and convert(varchar(12)," + criteria.enddate + ",101))";
                    query += " where (cast(b.CreateDate as datetime) not between cast('" + criteria.startdate + "' as datetime) and cast('" + criteria.enddate + "' as datetime))";
                
                }


            }
          

            if (!criteria.ShowAll)
            {
                if (criteria.Column != 0)
                {
                    query += " AND ";
                    switch (criteria.Column)
                    {
                        case 0:
                            break;
                        case 1://shopID                 
                            query += filter(criteria.Filter, "a.[MemberID]", criteria.tag);
                            break;
                        case 2://First Name
                            query += filter(criteria.Filter, "b.[FirstName]", criteria.tag);
                            break;
                        case 3://Last Name
                            query += filter(criteria.Filter, "b.[LastName]", criteria.tag);
                            break;
                        case 4://Shop
                            query += filter(criteria.Filter, "c.[Name]", criteria.tag);
                            break;
                    }
                }
            }

            if (criteria.sortmode == 1)
            {
                query += " Order by b.FirstName";
            }
            else if (criteria.sortmode == 2)
            {
                query += " Order by b.LastName";
            }
            else if (criteria.sortmode == 3)
            {
                query += " Order by a.HiredDate";
            }
            else if (criteria.sortmode == 4)
            {
                query += " Order by b.CreateDate";
            }
            if (criteria.sortorder == "Desc")
            {
                query += " Desc";
            }
            else
            {
                query += " Asc";
            }
            

            DataSet ds = Connection.GetDataSet(query);
          
            grid.DataSource = ds;

        }
        public static void Browse_Members(RadGrid grid)
        {

            string query = "exec dbo.OpenKeys; Select a.[MemberID],a.[FirstName],a.[LastName],[dbo].[Decrypt](a.[SSN]) as SSN,convert(varchar(12),a.[BirthDate],101) as BirthDate,a.[IsDisable],a.[CreatedBy],convert(varchar(12),a.[CreateDate],101) as CreateDate,a.[UpdatedBy],a.[LastUpdateDate] from dbo.Members as a inner join dbo.Members_Address as b on a.RecordID = b.ReferenceID order by a.RecordID";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;

        }
        #endregion

        #region common
        private static string Isnull(string var)
        {
            string result = "";
            if (string.IsNullOrEmpty(var))
            {
                result = null;
            }
            else { result = var; }

            return result;
        }
        #endregion

        #region Notes
        public static Repository.Struct.SpResultset Notes(string @Note,bool @chkFlag, string @RecordID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "Members";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = @RecordID;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = @Note;
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = @chkFlag;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = true;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static Repository.Struct.SpResultset UpdateNotes(string @DataID, bool @chkFlag)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "MemberNotesUpdate";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = @DataID;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = "";
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = @chkFlag;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }

        public static Repository.Struct.SpResultset DeleteNotes(string @DataID)
        {
            bool sp = false;
            SqlConnection con = new SqlConnection(Connection.DBConnectionString());
            con.Open();
            SqlCommand cmd = new SqlCommand("Notes_Sp", con);
            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.Add("@Mode", SqlDbType.NVarChar, 30).Value = "MemberNotesDelete";
            cmd.Parameters.Add("@DataID", SqlDbType.Int).Value = @DataID;
            cmd.Parameters.Add("@RecordID", SqlDbType.Int).Value = null;
            cmd.Parameters.Add("@Notes", SqlDbType.NVarChar, -1).Value = "";
            cmd.Parameters.Add("@Flag", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@Inactive", SqlDbType.Bit).Value = false;
            cmd.Parameters.Add("@CreatedBy", SqlDbType.NVarChar, 30).Value = Repository.Session.GetContentInSessionby("Createdby");


            //Last Inserted or Updated Record
            SqlParameter scope = new SqlParameter("@scope", SqlDbType.Int);
            scope.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(scope);

            //Additional:action result:'successful','Error'
            SqlParameter result = new SqlParameter("@output", SqlDbType.VarChar, 50);
            result.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(result);

            //Actual error encountered
            SqlParameter errormsg = new SqlParameter("@error_msg", SqlDbType.VarChar, -1);
            errormsg.Direction = ParameterDirection.Output;
            cmd.Parameters.Add(errormsg);

            cmd.ExecuteNonQuery();
            con.Close();
            if (result.Value.ToString() == "successful")
            {
                sp = true;
            }
            else
            {
                sp = false;
            }

            Repository.Struct.SpResultset Resultset = new Struct.SpResultset();
            Resultset.scope = scope.Value.ToString();
            Resultset.Isresult = sp;
            Resultset.Error = errormsg.Value.ToString();

            return Resultset;
        }
        public static bool GetNotes_byMember(string @RecordID)
        {
            bool Result = false;
            string query = "SELECT RecordID,[Note],[CreatedBy],convert(varchar(12),[CreateDate],101) as CreateDate,Flag FROM Members_Notes WHERE ReferenceID=" + RecordID + " and Inactive='1' and Flag='1' order by [CreateDate]";
            DataSet ds = Connection.GetDataSet(query);

            if (ds.Tables[0].Rows.Count > 0)
            {
                Result = true;
            }
            return Result;

        }
        public static void GetNotes(RadGrid grid, string @RecordID)
        {
            string query = "SELECT RecordID,[Note],[CreatedBy],convert(varchar(12),[CreateDate],101) as CreateDate,Flag FROM Members_Notes WHERE ReferenceID=" + RecordID + " and Inactive='1' order by [CreateDate]";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
        }
        public static void GetSSN(RadGrid grid, string @RecordID)
        {
            string query = "SELECT [SSN] as OldSSN,[New_SSN] as NewSSN,[Mod_Date] as Date,[ModUser] FROM [dbo].[Member_SSN] where ReferenceID='" + RecordID + "' order by Date DESC";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
        }
        public static int GetSSNCount(string @RecordID)
        {
            int count = 0;
            string query = "SELECT [SSN] as OldSSN,[New_SSN] as NewSSN,[Mod_Date] as Date,[ModUser] FROM [dbo].[Member_SSN] where ReferenceID='" + RecordID + "' order by Date DESC";
            DataSet ds = Connection.GetDataSet(query);
            count = ds.Tables[0].Rows.Count;
            return count;
        }

        #endregion

    }

    public class dashboard
    {
        public dashboard()
        {
            //
            // TODO: Add constructor logic here
            //

        }




        #region members
        public static void dash_Members(DetailsView Member_statistics, RadChart chart)
        {
            string query = "";
            DataSet ds = new DataSet();

            //Detailsview
            query = "SELECT  sum([Total]) as Total,sum([Active]) as Active,sum([InActive]) as InActive,sum([Last7daysHired]) as Last7daysHired,sum([Last7daysTerminated]) as Last7daysTerminated,sum([TodayHired]) as TodayHired,sum([TodayTerminated]) as TodayTerminated FROM [dbo].[Statistics_Members_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            Member_statistics.DataSource = ds;
            Member_statistics.DataBind();
            //chart
            query = "SELECT  Sum([Total])as Total,Sum([Active])as Active,Sum([InActive]) as InActive FROM [dbo].[Statistics_Members_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            chart.DataSource = ds;
            chart.DataBind(); 

        }

        public static void dash_Shops(DetailsView Shop_statistics, RadChart chart)
        {
            string query = "";
            DataSet ds = new DataSet();
           
            //Detailsview
            query = "SELECT  sum([Total]) as Total,sum([Active]) as Active,sum([InActive]) as InActive,sum([Last7daysHired]) as Last7daysHired,sum([Last7daysTerminated]) as Last7daysTerminated,sum([TodayHired]) as TodayHired,sum([TodayTerminated]) as TodayTerminated FROM [dbo].[Statistics_Shops_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            Shop_statistics.DataSource = ds;
            Shop_statistics.DataBind();
            //chart
            query = "SELECT  Sum([Total])as Total,Sum([Active])as Active,Sum([InActive]) as InActive FROM [dbo].[Statistics_Shops_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            chart.DataSource = ds;
            chart.DataBind(); 

        }
        public static void dash_Providers(DetailsView Provider_statistics, RadChart chart)
        {
            string query = "";
            DataSet ds = new DataSet();

            //Detailsview
            query = "SELECT  sum([Total]) as Total,sum([Active]) as Active,sum([InActive]) as InActive,sum([Last7daysHired]) as Last7daysHired,sum([Last7daysTerminated]) as Last7daysTerminated,sum([TodayHired]) as TodayHired,sum([TodayTerminated]) as TodayTerminated FROM [dbo].[Statistics_Providers_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            Provider_statistics.DataSource = ds;
            Provider_statistics.DataBind();
            //chart
            query = "SELECT  Sum([Total])as Total,Sum([Active])as Active,Sum([InActive]) as InActive FROM [dbo].[Statistics_Providers_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            chart.DataSource = ds;
            chart.DataBind();

        }
        public static void dash_Benefits(DetailsView Benefit_statistics, RadChart chart)
        {
            string query = "";
            DataSet ds = new DataSet();

            //Detailsview
            query = "SELECT  sum([Total]) as Total,sum([Active]) as Active,sum([InActive]) as InActive,sum([Last7daysHired]) as Last7daysHired,sum([Last7daysTerminated]) as Last7daysTerminated,sum([TodayHired]) as TodayHired,sum([TodayTerminated]) as TodayTerminated FROM [dbo].[Statistics_Benefits_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            Benefit_statistics.DataSource = ds;
            Benefit_statistics.DataBind();
            //chart
            query = "SELECT  Sum([Total])as Total,Sum([Active])as Active,Sum([InActive]) as InActive FROM [dbo].[Statistics_Benefits_vw]";
            ds = new DataSet();
            ds = Connection.GetDataSet(query);
            chart.DataSource = ds;
            chart.DataBind();

        }
        public static DataSet dash_Provider1(GridView grid)
        {
            string query = "select b.Name,COUNT(*) as Benefits from dbo.Benefits as a  inner join dbo.Providers as b on a.ProviderID = b.RecordID group by b.Name ";
            DataSet ds = Connection.GetDataSet(query);
            grid.DataSource = ds;
            grid.DataBind();  
            return ds;

        }

        public static string dash_MembersbyDate(string type, int days)
        {
            string result = "0";
            string query = "";

            switch (type)
            {
                case "Hired":
                    query = " select count(*)as Total from [dbo].[Members_Hired] WHERE  (DATEDIFF(day,[HiredDate], GETDATE()) < " + days + ")";
                    break;

                case "Terminated":
                    query = "select count(*)as Total from [dbo].[Members_Hired] WHERE  (DATEDIFF(day, [TerminatedDate], GETDATE()) < " + days + ")";
                    break;
            }


            DataSet ds = Connection.GetDataSet(query);
            if (ds.Tables[0].Rows.Count > 0)
            {
                result = ds.Tables[0].Rows[0]["Total"].ToString();
            }
            return result;
        }
        #endregion


    }

  


      
}
