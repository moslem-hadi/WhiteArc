using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

public class SystemConfig
{

    private static volatile SystemConfig instance;
    private static object syncRoot = new Object();

    [NonSerialized]
    SqlConnection con;
    [NonSerialized]
    SqlCommand com;
    [NonSerialized]
    SqlDataAdapter da;
    DataTable dtConfig = new DataTable();


    public List<SysData> AppOptions;
    public static SystemConfig Instance
    {
        get
        {
            if (instance == null)
            {
                lock (syncRoot)
                {
                    if (instance == null)
                        instance = new SystemConfig();
                }
            }

            return instance;
        }
    }



    private SystemConfig()
    {
        AppOptions = new List<SysData>();


        con = new SqlConnection(GetConnectionString());
        com = new SqlCommand("", con);
        da = new SqlDataAdapter("", con);

        SetSysData();

    }




    private string GetConnectionString()
    {
        return System.Configuration.ConfigurationManager.ConnectionStrings["CMSConnectionString"].ConnectionString;
    }
    private int executePro(string ProName, ref SqlParameter[] param, ref DataTable dt)
    {
        try
        {
            com.CommandText = ProName;
            com.CommandType = CommandType.Text;
            if (param != null)
                foreach (SqlParameter item in param)
                {
                    com.Parameters.Add(item);
                }
            if (con.State == ConnectionState.Closed)
                con.Open();
            da.SelectCommand = com;
            da.Fill(dt);
            int result = dt.Rows.Count;
            return result;

        }
        catch (Exception ex)
        {

            return -2;
        }
        finally
        {
            com.Parameters.Clear();
            if (con.State == ConnectionState.Open)
                con.Close();
        }

    }

    private void SetSysData()
    {
        if (loadDB(ref dtConfig) == 1)
        {
            for (int i = 0; i < dtConfig.Rows.Count; i++)
            {
                AppOptions.Add(new SysData
                {
                    Name = dtConfig.Rows[i]["Name"].ToString(),
                    Value = dtConfig.Rows[i]["Value"].ToString()
                });
            }
        }
    }

    private int loadDB(ref DataTable dt)
    {
        string Command = "select Name,Value from SettingValue where LoadAtFirst=1";
        SqlParameter[] param = null;
        switch (executePro(Command, ref param, ref dt))
        {
            case -2:
                //catch
                return -2;

            case 0:
                //nothing found
                return 0;

        }
        return 1;
    }

    /// <summary>
    /// گرفتن یک مقدار
    /// </summary>
    /// <param name="ConfigName">نام مقدار</param>
    /// <param name="encrypted">اگر ترو باشد مقدار نام و ولیو توی دیتابیس باید کد شده باشه. و اینجا درست برمیگردن</param>
    /// <returns></returns>
    public string GetConfigVale(string ConfigName,bool encrypted)
    {
        //مثال
        //SystemConfig sysCon = SystemConfig.Instance;
        //string strconf = sysCon.GetConfigVale("AdvancedSearch",true);
        //if (strconf == "OK")
        //
        ConfigName = ConfigName.ToLower();

            string retVal = "";
        if (encrypted)
        {
            string EncriptedName = Encrypt.EncryptString(ConfigName);
            var el = AppOptions.FirstOrDefault(a => a.Name == EncriptedName);
            if (el == null)
                retVal =null;
            else
                retVal = Encrypt.DecryptString(el.Value);

        }
        else
        {
            var el = AppOptions.FirstOrDefault(a => a.Name == ConfigName);
           
                retVal = el?.Value;
        }
        return retVal;
    }

     

    public static void ClearInstance()
    {
        instance = null;
    }
}

public class SysData
{
    public string Name { get; set; }
    public string Value { get; set; }
}


 
