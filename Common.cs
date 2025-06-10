using System;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Globalization;
using System.Drawing;
using System.Drawing.Imaging;
using narsweb;
using System.Net;
using RestSharp;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.IO;
using System.Net.NetworkInformation;
using System.Management;
using System.Data.OleDb;
using System.Collections;
using narsShop;
using System.Net.Http.Headers;
using System.Net.Http;

namespace narsShop
{
    /// <summary>
    /// Summary description for DataHand.
    /// </summary>
    public class serverparams
    {
        public static string servername;
        public static string dbsname;
    }

    public class SQLH
    {
        public SqlDataAdapter da;
        private DataSet ds;
        private DataView dv;
        private DataRowView dr;
        public string connectionstring;
        public SqlConnection mj;
        private SqlParameter[] sqparm;
        private int maxparm = 0;
        private SqlCommandBuilder cb;
        public string servername;
        public string dbsname;
        //~SQLH()
        //{
        //if (mj.State == ConnectionState.Open)
        //   this.mj.Close();
        //}
        public SQLH()
        {
            if (HttpContext.Current.Session != null)
                this.mj = SqlConnect(HttpContext.Current.Session["servername"].ToString(), HttpContext.Current.Session["dbsname"].ToString());
            else
                this.mj = SqlConnect(serverparams.servername, serverparams.dbsname);
        }
        public SqlParameter[] getparams()
        {
            return sqparm;
        }
        public SQLH(string servername, string dbsname)
        {

            this.mj = SqlConnect(servername, dbsname);
        }
        private SqlConnection SqlConnect(string servername, string dbsname)
        {
            /*   try
               {
                   Global.ctr.IncSQLHConnect_Counter();
               } catch
               {

               }*/
            this.sqparm = new SqlParameter[100];
            this.connectionstring = "user id=talaghes_user;password=Tbox85@5sohrabi;initial catalog=" + dbsname.Trim() + ";data source=" + servername.Trim() + ";Connect Timeout=60";
            return new SqlConnection(this.connectionstring);

        }
        public DataView ParametricSqlExecute(string tcSQL, object attributes, string tcAlias)
        {

            /*   if (this.mj == null)
                   return null;
               try { 
               Global.ctr.IncSQLHSelect_Counter();
               }
               catch
               {

               }*/
            tcSQL = tcSQL.Replace(';', ':');
            SqlCommand oCommand = new SqlCommand(tcSQL, this.mj);

            var props = attributes.GetType().GetProperties();
            //Dictionary<string, object> paramlist = props.ToDictionary(x => x.Name, x => x.GetValue(attributes, null));
            foreach (var x in props)
            {
                oCommand.Parameters.Add(new SqlParameter(x.Name, x.GetValue(attributes, null)));
            }
            //for (int i = 0; i < this.maxparm; ++i) this.sqparm[i] = null;
            //this.maxparm = 0;

            oCommand.CommandTimeout = 100;
            if (mj.State == ConnectionState.Closed)
                mj.Open();
            this.da = new SqlDataAdapter(oCommand);
            this.ds = new DataSet();
            this.da.Fill(this.ds, tcAlias);
            this.dv = this.ds.Tables[0].DefaultView;
            mj.Close();
            return dv;
        }

        public int updatedatasource(string tablename)
        {
            cb = new SqlCommandBuilder(this.da);
            return da.Update(ds, tablename);

        }
        public DataView SqlExecute(string tcSQL, string tcAlias)
        {

            if (this.mj == null)
                return null;
            /* try { 
             Global.ctr.IncSQLHSelect_Counter();
             }
             catch
             {

             }*/
            tcSQL = tcSQL.Replace(';', ':');
            SqlCommand oCommand = new SqlCommand(tcSQL, this.mj);
            foreach (SqlParameter sqp in this.sqparm)
            {
                if (sqp != null) oCommand.Parameters.Add(sqp);
            }
            for (int i = 0; i < this.maxparm; ++i) this.sqparm[i] = null;
            this.maxparm = 0;

            oCommand.CommandTimeout = 100;
            if (mj.State == ConnectionState.Closed)
                this.mj.Open();
            this.da = new SqlDataAdapter(oCommand);
            this.ds = new DataSet();
            this.da.Fill(this.ds, tcAlias);
            this.dv = this.ds.Tables[0].DefaultView;



            int ucc = 0;
            int ssh = -1;
 
            mj.Close();
            return dv;
        }
        public Int64 SqlExecute(string tcSQL)
        {
            /*try { 
            Global.ctr.IncSQLHUpdate_Counter();
            }
            catch
            {

            }*/
            Int64 rcx = 0;
            tcSQL = tcSQL.Replace(';', ':');
            SqlCommand oCommand = new SqlCommand(tcSQL, this.mj);
            SqlCommand oCommand2 = new SqlCommand("select @@IDENTITY", this.mj);
            if (mj.State == ConnectionState.Closed)
                this.mj.Open();
            //oCommand.CreateParameter();
            foreach (SqlParameter sqp in this.sqparm)
            {
                if (sqp != null) oCommand.Parameters.Add(sqp);
            }
            for (int i = 0; i < this.maxparm; ++i) this.sqparm[i] = null;
            this.maxparm = 0;
            //try
            //{
            oCommand.ExecuteNonQuery();
            //}
            //catch
            //{
            //    throw new Exception("Error excuting command : "+tcSQL);
            //}
            object rc = oCommand2.ExecuteScalar();
            if (rc.ToString() != "")
            {
                rcx = Convert.ToInt32(oCommand2.ExecuteScalar());
            }
            //this.ds.Tables[0].Clear();
            //this.da.Fill(this.ds,this.ds.Tables[0].TableName);

            int ucc = 0;
            int ssh = -1;
            //try
            //{
            //    int.TryParse(HttpContext.Current.Session["uc"].ToString(), out ucc);
            //    int.TryParse(HttpContext.Current.Session["sherkat"].ToString(), out ssh);
            //}
            //catch
            //{
            //    ucc = -1;
            //}
            try
            {

                tcSQL = "insert into logs_command (commandstring,barname,tarikh,saat,uc,sherkat,station) values('" + tcSQL.Replace('\'', '"') + "','" + HttpContext.Current.CurrentHandler.ToString() + "','" +
                        persiandate.datef() + "','" + persiandate.DateTimeNow().ToShortTimeString() + "'," + ucc.ToString() + "," + ssh.ToString() + ",'" +
                        HttpContext.Current.Session.SessionID +
                        "')";
                oCommand = new SqlCommand(tcSQL, this.mj);
                oCommand.ExecuteNonQuery();
            }
            catch
            {
            }


            mj.Close();
            return rcx;

        }
        public DataRowView getrow()
        {
            if (this.ds.Tables[0].DefaultView.Count == 0)
            {
                DataRow drr = this.ds.Tables[0].NewRow();
                this.ds.Tables[0].Rows.Add(drr);
            }
            this.dr = this.ds.Tables[0].DefaultView[0];
            return this.dr;
        }
        public void addparm_nvarchar(string pnam, object pval)
        {
            SqlParameter sqan = new SqlParameter(pnam, SqlDbType.NVarChar);
            sqan.Value = pval;
            sqparm[maxparm] = sqan;

            ++maxparm;

        }
        public void addparm(string pname, object pval)
        {

            SqlParameter sqan = new SqlParameter(pname, pval);
            sqparm[maxparm] = sqan;

            ++maxparm;
        }
        public long updatearow(DataRowView arow)
        {
            string cname = "";
            string updstr = "update " + arow.Row.Table.TableName + " set ";

            foreach (System.Data.DataColumn obj in arow.Row.Table.Columns)
            {
                cname = obj.Caption;
                if (cname != "s_id")
                {
                    updstr += cname;
                    updstr += "=@";
                    updstr += cname;
                    updstr += ",";
                }

                this.addparm("@" + cname, arow[cname]);
            }
            updstr = updstr.Remove(updstr.Length - 1);
            updstr += " where s_id=@s_id";
            return this.SqlExecute(updstr);
        }
        public long insertearow(DataRowView arow)
        {
            string cname = "";
            string vname = " values (";
            string updstr = "insert into " + arow.Row.Table.TableName + " (";

            foreach (System.Data.DataColumn obj in arow.Row.Table.Columns)
            {
                cname = obj.Caption;
                if (cname != "s_id")
                {
                    updstr += cname;
                    updstr += ",";

                    vname += "@";
                    vname += cname;
                    vname += ",";
                }

                this.addparm("@" + cname, arow[cname]);
            }
            updstr = updstr.Remove(updstr.Length - 1);
            vname = vname.Remove(vname.Length - 1) + ")";
            updstr += vname;
            return this.SqlExecute(updstr);
        }
        public long deletearow(DataRowView arow)
        {
            string updstr = "delete from " + arow.Row.Table.TableName + " where s_id=@s_id";
            this.addparm("@s_id", arow["s_id"]);
            return this.SqlExecute(updstr);
        }

        public static string ValidateInt(string int_input_text)
        {
            int valu = 0;
            int.TryParse(int_input_text, out valu);
            return valu.ToString();
        }
        public static string Validatedecimal(string decimal_input_text)
        {
            decimal valu = 0;
            decimal.TryParse(decimal_input_text, out valu);
            return valu.ToString();
        }
        public static string ValidateString(string string_input_text)
        {
            string_input_text.Replace('\'', ' ');
            string_input_text.Replace('"', ' ');
            string_input_text.Replace(';', ' ');
            string_input_text.Replace("--", "");

            return string_input_text;
        }

    }
    public class myconvert
    {


        public static Int64 toint(string str)
        {
            try
            {
                return Convert.ToInt64(str.Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static Int64 toint(object obj)
        {
            try
            {
                return Convert.ToInt64(obj.ToString().Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static Int16 toint16(string str)
        {
            try
            {
                return Convert.ToInt16(str.Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static Int16 toint16(object obj)
        {
            try
            {
                return Convert.ToInt16(obj.ToString().Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static double todouble(object obj)
        {
            try
            {
                return Convert.ToDouble(obj.ToString().Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static decimal todecimal(object obj)
        {
            try
            {
                return Convert.ToDecimal(obj.ToString().Replace(",", ""));
            }
            catch
            {
                return 0;
            }
        }
        public static string n2str(string _n)
        {
            string _nn = _n.Trim();
            // _nn = remz(_nn);
            int _ll = _nn.Length;
            if (_ll < 3)
            {
                _nn = _nn.PadLeft(3);
                _ll = 3;
            }

            string _r1 = _nn.Substring(_nn.Length - 3, 3);
            string _s1 = seragam(_r1);
            string sres;

            if (_ll > 3 && _ll < 7)
            {
                sres = seragam(_nn.Substring(0, _ll - 3)) + " هزار ";
                if (_s1 != "") sres += "و";
                sres += _s1;
                _s1 = sres;
            }

            if (_ll > 6 && _ll < 10)
            {
                sres = seragam(_nn.Substring(_nn.Length - 6).Substring(0, 3));
                if (sres.Trim() != "")
                    sres += " هزار ";
                if (_s1 != "" && sres != "")
                    sres += "و";
                _s1 = sres + _s1;

                sres = seragam(_nn.Substring(0, _ll - 6)) + " میلیون ";
                if (_s1 != "")
                    sres += "و";
                _s1 = sres + _s1;
            }

            if (_ll > 9 && _ll < 13)
            {
                string s4 = seragam(_nn.Substring(0, _ll - 9)) + " میلیارد ";
                _nn = _nn.Substring(_ll - 9, 9);
                _ll = 9;

                string s3 = seragam(_nn.Substring(0, _ll - 6));
                if (s3.Trim() != "") s3 += " میلیون ";

                sres = seragam(_nn.Substring(_nn.Length - 6, 6).Substring(0, 3));
                if (sres != "") sres += " هزار ";
                if (_s1 != "" && sres != "") sres += "و";
                _s1 = sres + _s1;

                sres = s3;
                if (_s1 != "" && sres != "")
                    sres += "و";
                sres += _s1;
                _s1 = sres;
                sres = s4;
                if (_s1 != "" && sres != "")
                    sres += "و";
                sres += _s1;
                _s1 = sres;
            }


            return _s1.Trim();
        }
        static string seragam(string st)
        {
            st = st.Trim();
            string fs = "";
            string fs2 = "";
            int mlen = st.Length;
            if (mlen == 1)
            {
                st = "0" + st;
                mlen = mlen + 1;
            }

            switch (mlen)
            {
                case 2:
                    fs = dah(st.Substring(st.Length - 2, 2));
                    fs2 = fs;
                    break;
                case 3:
                    fs = dah(st.Substring(st.Length - 2, 2));
                    fs2 = sad(st.Substring(st.Length - 3, 3).Substring(0, 1));
                    if (fs != "" && sad(st.Substring(st.Length - 3, 3).Substring(0, 1)) != "")
                        fs2 += "و";
                    fs2 += fs;
                    break;
            }
            return fs2;
        }
        static string sad(string i)
        {
            string ps1 = "";
            switch (i)
            {
                case "0":
                    ps1 = ""; break;
                case "1":
                    ps1 = "یکصد "; break;
                case "2":
                    ps1 = "دویست "; break;
                case "3":
                    ps1 = "سیصد "; break;
                case "4":
                    ps1 = "چهارصد "; break;
                case "5":
                    ps1 = "پانصد "; break;
                case "6":
                    ps1 = "ششصد "; break;
                case "7":
                    ps1 = "هفصد "; break;
                case "8":
                    ps1 = "هشصد "; break;
                case "9":
                    ps1 = "نهصد "; break;
            }
            return ps1;
        }
        static string dah(string i)
        {

            string ps2 = "";
            string ps = "";
            if (Convert.ToInt32(i) < 20)
                ps2 = tabist(i);
            else
            {
                switch (i.Substring(0, 1))
                {
                    case "2":
                        ps = "بیست "; break;
                    case "3":
                        ps = "سی "; break;
                    case "4":
                        ps = "چهل "; break;
                    case "5":
                        ps = "پنجاه "; break;
                    case "6":
                        ps = "شصت "; break;
                    case "7":
                        ps = "هفتاد "; break;
                    case "8":
                        ps = "هشتاد "; break;
                    case "9":
                        ps = "نود "; break;
                }
                ps2 = tabist(i.Substring(i.Length - 1));
                if (ps2 != "")
                    ps2 = ps + "و" + ps2;
                else
                    ps2 = ps + ps2;
            }
            return ps2;
        }
        static string tabist(string i)
        {
            string ps3 = "";
            switch (i)
            {
                case "00":
                case "0":
                    ps3 = ""; break;
                case "01":
                case "1":
                    ps3 = "یک"; break;
                case "02":
                case "2":
                    ps3 = "دو"; break;
                case "03":
                case "3":
                    ps3 = "سه"; break;
                case "04":
                case "4":
                    ps3 = "چهار"; break;
                case "05":
                case "5":
                    ps3 = "پنج"; break;
                case "06":
                case "6":
                    ps3 = "شش"; break;
                case "07":
                case "7":
                    ps3 = "هفت"; break;
                case "08":
                case "8":
                    ps3 = "هشت"; break;
                case "09":
                case "9":
                    ps3 = "نه"; break;
                case "10":
                    ps3 = "ده"; break;
                case "11":
                    ps3 = "یازده"; break;
                case "12":
                    ps3 = "دوازده"; break;
                case "13":
                    ps3 = "سیزده"; break;
                case "14":
                    ps3 = "چهارده"; break;
                case "15":
                    ps3 = "پانزده"; break;
                case "16":
                    ps3 = "شانزده"; break;
                case "17":
                    ps3 = "هفده"; break;
                case "18":
                    ps3 = "هجده"; break;
                case "19":
                    ps3 = "نوزده"; break;
            }

            return ps3;
        }
        static string remz(string klm)
        {
            for (int i = 0; i < klm.Length; ++i)
            {
                if (klm.StartsWith("0"))
                    klm = klm.Remove(0);
            }
            return klm;
        }

        public static string nf2ne(string numberstr)
        {
            return numberstr.Replace("۰", "0").Replace("۱", "1").Replace("۲", "2").Replace("۳", "3").Replace("۴", "4").Replace("۵", "5").Replace("۶", "6").Replace("v", "7").Replace("۸", "8").Replace("۹", "9");
        }
    }

    public static class persiandate
    {
        public static string datef()
        {
            return datef(0);
        }
        public static int datediff(string dt1, string dt2)

        {
            return (togeorgian(dt1) - togeorgian(dt2)).Days;
        }

        public static DateTime DateTimeNow()
        {
            DateTime serverTime = DateTime.Now;
            DateTime _localTime = TimeZoneInfo.ConvertTimeBySystemTimeZoneId(serverTime, TimeZoneInfo.Local.Id, "Iran Standard Time");
            return _localTime;
        }
        public static string datef(int _n)
        {


            string _cdatef;
            int _yy = 0;
            int _mm = 0;
            int _dd = 0;
      
                PersianCalendar pcal = new PersianCalendar();
                _yy = pcal.GetYear(DateTimeNow().AddDays(_n));
                _mm = pcal.GetMonth(DateTimeNow().AddDays(_n));
                _dd = pcal.GetDayOfMonth(DateTimeNow().AddDays(_n));
         


            _cdatef = _yy.ToString().Trim().PadLeft(4, '0') + "/" + _mm.ToString().Trim().PadLeft(2, '0') + "/" + _dd.ToString().Trim().PadLeft(2, '0');
            return _cdatef;

        }

        public static string datef(DateTime dt)
        {
            string _cdatef;
            int _yy = 0;
            int _mm = 0;
            int _dd = 0;
            PersianCalendar pcal = new PersianCalendar();
            _yy = pcal.GetYear(dt);
            _mm = pcal.GetMonth(dt);
            _dd = pcal.GetDayOfMonth(dt);



            _cdatef = _yy.ToString().Trim().PadLeft(4, '0') + "/" + _mm.ToString().Trim().PadLeft(2, '0') + "/" + _dd.ToString().Trim().PadLeft(2, '0');
            return _cdatef;
        }
        public static string datename(int _n)
        {

            if (HttpContext.Current.Session["__lang_date"].ToString().Equals("MI"))
            {
                return DateTime.Now.ToShortDateString();
            }

            string _cdatef;



            PersianCalendar pcal = new PersianCalendar();
            _cdatef = (pcal.GetDayOfWeek(DateTimeNow().AddDays(_n))).ToString();
            switch (_cdatef)
            {
                case "Sunday": _cdatef = "یک شنبه"; break;
                case "Monday": _cdatef = "دو شنبه"; break;
                case "Tuesday": _cdatef = "سه شنبه"; break;
                case "Wednesday": _cdatef = "چهار شنبه"; break;
                case "Thursday": _cdatef = "پنج شنبه"; break;
                case "Friday": _cdatef = "جمعه"; break;
                case "Saturday": _cdatef = "شنبه"; break;
            }
            return _cdatef;
        }
        public static string datetimeflag()
        {
            return persiandate.datef() + " " + persiandate.timef();
        }
        public static string add(string _dt1, int p1)
        {

            DateTime dt = togeorgian(_dt1).AddDays(p1);
            return datef(dt);

        }
        public static DateTime togeorgian(string datef)
        {
            PersianCalendar pcal = new PersianCalendar();
            int yy = myconvert.toint16(datef.Substring(0, 4));
            int mm = myconvert.toint16(datef.Substring(5, 2));
            int dd = myconvert.toint16(datef.Substring(8, 2));

   
                return pcal.ToDateTime(yy, mm, dd, 0, 0, 0, 0);
        }
        public static bool iscorrect(string datef)
        {
            bool resp = true;
            /*   int val;
               if (!datef.StartsWith("13") && !datef.StartsWith("14")) resp = false;
               if (!int.TryParse(datef.Substring(0, 4), out val)) resp = false;
               if (!int.TryParse(datef.Substring(5, 2), out val)) resp = false;
               if (val < 1 || val > 12) resp = false;

               if (!int.TryParse(datef.Substring(8, 2), out val)) resp = false;
               if (val < 1 || val > 31) resp = false;

               if (!datef.Substring(4, 1).Equals("/") || !datef.Substring(7, 1).Equals("/")) resp = false;
               return resp;*/

            try
            {
                DateTime dt = togeorgian(datef);

            }
            catch
            {
                resp = false;
            }
            return resp;
        }

        public static string timef(int addhoure, int addmin, int addsec)
        {

            DateTime dt = DateTimeNow().AddHours(addhoure).AddMinutes(addmin).AddSeconds(addsec);
            return dt.Hour.ToString().PadLeft(2, '0') + ":" + dt.Minute.ToString().PadLeft(2, '0') + ":" + dt.Second.ToString().PadLeft(2, '0');
        }
        public static string timef()
        {
            return timef(0, 0, 0);
        }
        public static string timefrommin(int minute)
        {
            return (Convert.ToInt16(minute / 60)).ToString().Trim().PadLeft(2, '0') + ":" + (Convert.ToInt16(minute % 60)).ToString().Trim().PadLeft(2, '0');
        }
        public static int minutefrommidnight()
        {
            return DateTimeNow().Minute + DateTimeNow().Hour * 60;
        }
        public static bool isvalid(string datef)
        {
            return iscorrect(datef);
            /*bool resp = true;
            int val;
            if (!datef.StartsWith("13") && !datef.StartsWith("14")) resp = false;
            if (!int.TryParse(datef.Substring(0, 4), out val)) resp = false;
            if (!int.TryParse(datef.Substring(5, 2), out val)) resp = false;
            if (val < 1 || val > 12) resp = false;

            if (!int.TryParse(datef.Substring(8, 2), out val)) resp = false;
            if (val < 1 || val > 31) resp = false;

            if (!datef.Substring(4, 1).Equals("/") || !datef.Substring(7, 1).Equals("/")) resp = false;
            return resp;*/
        }
        public static bool isvalidtime(string datef)
        {
            bool resp = true;
            int val_s;
            int val_m;
            if (!datef.Contains(":")) return false;

            string[] ts = datef.Split(new char[] { ':' });

            if (!int.TryParse(ts[0], out val_s)) resp = false;
            if (val_s < 0 || val_s > 23) resp = false;
            if (!int.TryParse(ts[1], out val_m)) resp = false;
            if (val_m < 0 || val_m > 59) resp = false;
            return resp;
        }
        public static string correcttimeformat(string datef)
        {
            bool resp = true;
            int val_s;
            int val_m;
            if (!datef.Contains(":")) return null;

            string[] ts = datef.Split(new char[] { ':' });

            int.TryParse(ts[0], out val_s);
            int.TryParse(ts[1], out val_m);
            return val_s.ToString().Trim().PadLeft(2, '0') + ":" + val_m.ToString().Trim().PadLeft(2, '0');
        }
    }


    public static class decode
    {
        public static Dictionary<string, string> customerinfo(string Token,string Session_apiurl)
        {

            Dictionary<string, string> json = null;
            string apiUrl = Session_apiurl+"/api/customer/info/";
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(apiUrl);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var responseTask = client.GetAsync(Token);
            responseTask.Wait();
            var result = responseTask.Result;
            if (result.IsSuccessStatusCode)
            {
                var readTask = result.Content.ReadAsStringAsync();
                readTask.Wait();

                json = JsonConvert.DeserializeObject<Dictionary<string, string>>(readTask.Result); ;
            }

            return json;


        }
        public static string u2name(string __uc)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "fname";
            string __k;
            if (__uc.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm from users where uc='" + __uc + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }

        public static string k2name(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm from kcodes where kcode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }

        public static string category2name(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select categoryname as esm from categories where categoryid='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static string k2vahed(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select vahed from kcodes where kcode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["vahed"].ToString();
                tmpvasc1.Dispose();
            }

            tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm  from vahedha where vhdno='" + __k.Trim() + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }

            return __k;
        }
        public static int k2ctr(string __kcode)
        {
            SQLH sqhand = new SQLH();
            int __k;
            if (__kcode.Trim() == "")
            {
                return -1;
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select ctr as esm from kcodes where kcode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return -1;
            }
            else
            {
                __k = myconvert.toint16(tmpvasc1[0]["esm"].ToString());
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static string m2name(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm from moin where moin='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static string z2name(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm from mhaz where mhaz='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }

        public static string name2z(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select mhaz as esm from mhaz where " + _lanquage + "='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        
        public static DataRowView vasinfo(string vascode)
        {
            SQLH sqhand=new SQLH();

            DataView dv = sqhand.SqlExecute("select * from  vas where vascode='" + vascode + "'", "dv6");
            return dv[0];
        }
        public static string t2name(string __kcode)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select " + _lanquage + " as esm from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static string t2grp(string __kcode)
        {
            SQLH sqhand = new SQLH();

            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select vas_gr as esm from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static string t2at1(string __kcode)
        {
            SQLH sqhand = new SQLH();

            string __k;
            if (__kcode.Trim() == "")
            {
                return "";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select at1 as esm from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0]["esm"].ToString();
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static Int64 t2etb(string __kcode)
        {
            SQLH sqhand = new SQLH();

            Int64 __k;
            if (__kcode.Trim() == "")
            {
                return 0;
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select etebar as esm from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return 0;
            }
            else
            {
                __k = myconvert.toint(tmpvasc1[0]["esm"].ToString());
                tmpvasc1.Dispose();
            }
            return __k;
        }
        public static bool t2blkd(string __kcode)
        {
            SQLH sqhand = new SQLH();

            bool __k = false;
            if (__kcode.Trim() == "")
            {
                return false;
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select mamnoe as esm from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return false;
            }
            else
            {
                if (tmpvasc1[0]["esm"].ToString().Equals("1"))
                    __k = true;
                tmpvasc1.Dispose();
            }
            return __k;
        }

        public static void GridView_RowDataBound_Headertranslate(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                for (int counter = 0; counter < e.Row.Cells.Count; ++counter)
                {
                    if (e.Row.Cells[counter].Text.Equals(""))
                    {

                        var ctrl = (LinkButton)e.Row.Cells[counter].Controls[0];
                        if (ctrl.Text.StartsWith("cts_"))
                            ctrl.Text = decode.getcts(ctrl.Text);
                    }
                    else
                    {
                        if (e.Row.Cells[counter].Text.StartsWith("cts_"))
                            e.Row.Cells[counter].Text = decode.getcts(e.Row.Cells[counter].Text);
                    }
                }
            }
        }
        public static string getcts(string idname)
        {
            SQLH sqhand = new SQLH();


            string respond = "";
            if (idname.Trim() == "")
            {
                return ".";
            }

            string[] listid = idname.Split(' ');

            foreach (string idn in listid)
            {

                System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select * from constant_text where idname='" + idn + "'", "tmpvasc1");

                if (tmpvasc1.Count == 0)
                {
                    respond += idn + " ";
                }
                else
                {
                    respond += tmpvasc1[0][HttpContext.Current.Session["__language"].ToString()].ToString().Trim() + " ";
                    tmpvasc1.Dispose();
                }
            }
            return respond.Trim();
        }
        public static string getvarb(string idname)
        {
            SQLH sqhand = new SQLH();


            string respond = "";
            if (idname.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select * from varbs where varb_name='" + idname + "'", "tmpvasc1");

            if (tmpvasc1.Count == 0)
            {
                respond = " ";
            }
            else
            {
                respond = tmpvasc1[0]["varb_mgd"].ToString().Trim();
            }

            return respond.Trim();
        }
        public static Int64 getmandvas(string mvas, string mcyear, string id_hno)
        {
            Int64 mand1 = 0, mand2 = 0, mand3 = 0, mand4 = 0;
            SQLH sqhand = new SQLH();
            DataView tmp1 = sqhand.SqlExecute("SELECT SUM(bedehkar-bestankar) as mand from forosh_naghd where vas='" + mvas + "' and LEFT(solddate,4)='" + mcyear + "'", "tmp1");
            if (tmp1.Count > 0)
                mand1 = myconvert.toint(tmp1[0]["mand"].ToString());

            DataView tmp2 = sqhand.SqlExecute("SELECT SUM(fi) AS mand from sngchek_list WHERE (dpno=0) AND (moind = '1103101') and cfrom='" + mvas + "'", "tmp2");
            if (tmp2.Count > 0)
                mand2 = myconvert.toint(tmp2[0]["mand"].ToString());

            DataView dvx = sqhand.SqlExecute("SELECT     dbo.fns_getvarb('DaysToVslChck') AS p1", "dvx");
            int daystovslchck = myconvert.toint16(dvx[0][0]);

            DataView tmp3 = sqhand.SqlExecute("SELECT  SUM(fi) AS mand FROM  (select * from sngchek_list WHERE cfrom='" + mvas + "' and (moind = '1103101') AND (dpno > 0)AND (moin2 = '1103102' or moin2='1108101' or moin2='1110110')  AND (vslinbank IS NULL) AND (ttb = ' ') and (barg2=' ') union  SELECT * FROM sngchek_list WHERE cfrom='" + mvas + "' and (moind = '1103101') AND (dpno > 0) AND (moin2 <> '1103102' and moin2<>'1108101' and moin2<>'1110110') AND (tres > '" + persiandate.datef(-1 * daystovslchck) + "')) as drv", "tmp2");
            if (tmp3.Count > 0)
                mand3 = myconvert.toint(tmp3[0]["mand"].ToString());

            DataView tmp4 = sqhand.SqlExecute("select SUM(fi*(case when tedadersal is null then dfax_ersal.tedad else dfax_ersal.tedad-tedadersal end)) as mand from  dfax_ersal where dfax_ersal.s_id<>" + id_hno + " and (dfax_ersal.tedad<>tedadersal or tedadersal is null) and tebtal=' ' and dfax_ersal.vas='" + mvas + "' and dfax_ersal.frshk<>'ثبت نام' and dfax_ersal.frshk<>'اماني پلاک' ", "tmp4");

            if (tmp4.Count > 0)
                mand4 = myconvert.toint(tmp4[0]["mand"].ToString());

            return mand1 + mand2 + mand3 + mand4;
        }
        public static bool isuser(string uc, string post)
        {
            DataView dvtemp = new SQLH().SqlExecute("SELECT * FROM recursive_users_group RIGHT OUTER JOIN users ON recursive_users_group.guc = users.uc WHERE  users.esm='" + post + "' and ((users.uc = '" + uc + "') OR  (recursive_users_group.duc = '" + uc + "'))", "tmpuc");
            if (dvtemp.Count > 0)
                return true;
            else
                return false;
        }

        public static string options2name(int option)
        {
            string opts = "";
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select * from dfax_options order by opt_value desc", "dv");
            foreach (DataRowView dr in dv)
            {
                if (option >= myconvert.toint16(dr["opt_value"].ToString()))
                {
                    opts += dr["persian"].ToString() + " ";
                    option -= myconvert.toint16(dr["opt_value"].ToString());
                }
            }
            return opts;
        }

        internal static string currencytocolor(string currencyname)
        {
            SQLH sqhand = new SQLH();

            string __k;
            if (currencyname.Trim() == "")
            {
                return "white";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select * from currency where currencyname='" + currencyname + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return "white";
            }
            else
            {
                __k = tmpvasc1[0]["backcolor"].ToString().Trim();
                tmpvasc1.Dispose();
            }
            return __k;
        }

        internal static string t2addr(string __kcode, string addr_param)
        {
            SQLH sqhand = new SQLH();
            string _lanquage = "persian";
            string __k;
            if (__kcode.Trim() == "")
            {
                return ".";
            }

            System.Data.DataView tmpvasc1 = sqhand.SqlExecute("select * from vas where vascode='" + __kcode + "'", "tmpvasc1");
            if (tmpvasc1.Count == 0)
            {
                return " ";
            }
            else
            {
                __k = tmpvasc1[0][addr_param].ToString();
                if (addr_param.Contains("mob") || addr_param.Contains("tel"))
                    __k = Crypto.Decrypt(__k);
                tmpvasc1.Dispose();
            }
            return __k;
        }
    }

    public static class anbarlead
    {
        public static decimal[] getmande(string anbar, string kala, string sherkat, string cyear, string date, string ex_pro, string ex_hno)
        {
            SQLH sqhand = new SQLH();
            DataView dv1 = sqhand.SqlExecute("select sum(tedad) as tdd,sum(meghdar) as mgd from anbar_list where az='" + anbar.Trim() + "' and kala='" + kala.Trim() +
                "' and cyear='" + cyear + "' and sherkat='" + sherkat + "' and hdate<='" + date.Trim() + "' and not(pro='" + ex_pro.Trim() + "' and hno='" + ex_hno.Trim() + "')", "dv");
            DataView dv2 = sqhand.SqlExecute("select sum(tedad) as tdd,sum(meghdar) as mgd from anbar_list where be='" + anbar.Trim() + "' and kala='" + kala.Trim() +
                "' and cyear='" + cyear + "' and sherkat='" + sherkat + "' and hdate<='" + date.Trim() + "' and not(pro='" + ex_pro.Trim() + "' and hno='" + ex_hno.Trim() + "')", "dv");
            decimal tsadere, tvarede;
            decimal vsadere, vvarede;
            decimal.TryParse(dv1[0]["tdd"].ToString(), out tsadere);
            decimal.TryParse(dv2[0]["tdd"].ToString(), out tvarede);
            decimal.TryParse(dv1[0]["mgd"].ToString(), out vsadere);
            decimal.TryParse(dv2[0]["mgd"].ToString(), out vvarede);
            decimal[] resp = new decimal[2];
            resp[0] = tvarede - tsadere;
            resp[1] = vvarede - vsadere;
            return resp;
        }
    }
    public static class foroshlead
    {

            public static string UpdateBasePrice()
            {
                SQLH sqhand = new SQLH();
                string retval = "0";
                DataView sqtb = sqhand.SqlExecute("select * from fi_estandard where tebtal>'" + persiandate.datetimeflag() + "' order by teemal", "sqtb");
                if (sqtb.Count > 0)
                {
                    // if (sqtb[0]["teemal"].ToString().CompareTo(persiandate.datef() + " " + persiandate.timef(-8, 0, 0)) > 0)
                    // {
                    retval = sqtb[sqtb.Count - 1]["fi"].ToString().Trim();
                }
                else
                {
                    try
                    {
                        WebClient MyWebClient = new WebClient();
                        MyWebClient.Headers.Add(System.Net.HttpRequestHeader.Cookie, "security=true");
                        Byte[] PageHTMLBytes;
                        PageHTMLBytes = MyWebClient.DownloadData("http://estjt.ir/");
                        UTF8Encoding oUTF8 = new UTF8Encoding();
                        string html = oUTF8.GetString(PageHTMLBytes);
                        html = html.Substring(html.IndexOf("مظنه تهران") + 600, 300);
                        html = html.Substring(html.IndexOf("<span>") + 6, html.IndexOf("</span>") - html.IndexOf("<span>") - 6);
                        html = html.Replace(",", "");
                        retval = html;
                        DataView dv = sqhand.SqlExecute("select * from fi_estandard where tebtal>'" + persiandate.datef() + "' order by teemal", "sqtb");
                        if (!dv[dv.Count - 1]["fi"].ToString().Equals(retval))
                        {
                            sqhand.SqlExecute("update fi_estandard set tebtal='" + persiandate.datetimeflag() + "' where tebtal>'" + persiandate.datetimeflag() + "'");
                            sqhand.SqlExecute("insert into fi_estandard (fi,tebtal,teemal) values (" + retval + ",'1499/99/99 23:59','" + persiandate.datetimeflag() + "')");
                        }
                        else
                        {
                            sqhand.SqlExecute("update fi_estandard set teemal='" + persiandate.datetimeflag() + "' where s_id='" + dv[dv.Count - 1]["s_id"].ToString() + "'");
                        }
                    }
                    catch
                    {

                        if (sqtb.Count > 0)
                        {
                            retval = sqtb[sqtb.Count - 1]["fi"].ToString().Trim();
                        }
                        else
                            retval = "0";
                    }
                }
                return retval;
            }
            public static void updatemandevas(string vascode, string sherkat)
            {
                if (!decode.t2grp(vascode).Contains("مشتر"))
                    return;

                ArrayList retval = getmandevas(vascode, sherkat, 0);
                SQLH sqhand = new SQLH();
                //if (resetdate)
                sqhand.SqlExecute("update vas_ori set tasis='" + retval[2].ToString() + "',mande=" + retval[0].ToString() + ",walet=" + retval[1].ToString() + " where vascode='" + vascode + "'");
                //else
                //    sqhand.SqlExecute("update vas_ori set mande=" + retval[0].ToString() + ",walet=" + retval[1].ToString() + " where vascode='" + vascode + "'");
            }

            public static decimal[] getmandevas(string vascode, string sherkat, long factor = 0, string solddate = "")
            {
                decimal[] retval = new decimal[2];
                ArrayList retval_a = getmandevas(vascode, sherkat, 0, factor, solddate);

                retval[0] = myconvert.todecimal(retval_a[0]);
                retval[1] = myconvert.todecimal(retval_a[1]);

                return retval;
            }
            public static ArrayList getmandevas(string vascode, string sherkat, int stype, long factor = 0, string solddate = "")
            {
                decimal mandeh = 0;
                decimal walet = 0;
                SQLH sqhand = new SQLH();
                string tsql = "";
                string tsql2 = "";
                string lastdate = "";
                string vastype = decode.t2grp(vascode);

                if (factor == 0 && solddate.Equals(""))
                    tsql2 = "";
                if (factor != 0 && solddate.Equals(""))
                {
                    tsql2 = " and a.factor<=" + factor + " ";
                }
                if (factor == 0 && !solddate.Equals(""))
                {
                    tsql2 = " and a.solddate<='" + solddate + "' ";
                }
                if (factor != 0 && !solddate.Equals(""))
                {
                    tsql2 = "and a.factor<=" + factor + " and a.solddate<='" + solddate + "'";
                }

                tsql = @"SELECT a.* , 1000000000000 AS mandeh,dp_main.bedehkar AS bedghest, 1000000000000 AS besghest, 1000000000000 AS walet, 1000 AS radif, '          ' AS noeamal, dpdtl.totgh,'                  ' as karbar,RTRIM(CAST(DATEPART(hour, a.createdate) AS char)) + ':' + CAST(DATEPART(minute, a.createdate) AS char) AS saat ,
                            bank_daftar.cobank5, bank_daftar.vas AS vas2, bank_daftar.m1, bank_daftar.m2  FROM khoroji_kol AS a LEFT OUTER JOIN dp_main ON a.factor = dp_main.factor 
                            LEFT OUTER JOIN (SELECT        factor, vas, SUM(mablagh) AS totgh FROM dp_detail GROUP BY factor, vas) AS dpdtl ON a.factor = dpdtl.factor AND a.vas = dpdtl.vas 
                            LEFT OUTER JOIN   bank_daftar ON a.factor = bank_daftar.factor AND a.sherkat = bank_daftar.sherkat AND (a.sta = 'I' OR a.sta = 'O') where a.sherkat=0 and  (a.vas = '" + vascode + "'  or bank_daftar.vas='" + vascode + "' or bank_daftar.cobank5='" + vascode + "') " + tsql2 + " ORDER BY a.solddate, a.factor ";


                DataView dv = sqhand.SqlExecute(tsql, "dv"); // 5 s

                foreach (DataRowView dr in dv)
                {
                    if (dr["sta"].ToString().Equals("S")) //factor forosh
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        walet -= myconvert.todecimal(dr["kif"]);
                        decimal peyed = myconvert.todecimal(dr["kif"]) +
                            myconvert.todecimal(dr["kart"]) +
                            myconvert.todecimal(dr["naghd"]) +
                            myconvert.todecimal(dr["buyprice"]) +
                            myconvert.todecimal(dr["takhfif"]);

                        mandeh += myconvert.todecimal(dr["ghesti"]);
                        mandeh += myconvert.todecimal(dr["haml"]);
                        mandeh -= peyed;
                    }
                    if (dr["sta"].ToString().Equals("D")) //daryaft ghest
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        walet -= myconvert.todecimal(dr["kif"]);
                        mandeh -= myconvert.todecimal(dr["totalbed"]);
                    }
                    if (dr["sta"].ToString().Equals("W")) //sharj kif
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        walet += myconvert.todecimal(dr["totalbed"]);
                        if (myconvert.todecimal(dr["kif"]) > 0)
                        {
                            mandeh += myconvert.todecimal(dr["kif"]);
                        }
                    }
                    if (dr["sta"].ToString().Equals("P")) //pardakht be moshtari
                    {
                        lastdate = dr["solddate"].ToString().Trim();

                        if ((persiandate.datediff(HttpContext.Current.Session["dp_change_formol_date"].ToString(), dr["solddate"].ToString()) > 0 || !vastype.StartsWith("مشتر")) && dr["shomar2"].ToString().Trim().Equals(""))
                            mandeh += myconvert.todecimal(dr["totalbed"]);
                        else
                            walet -= myconvert.todecimal(dr["totalbed"]);
                    }
                    if (dr["sta"].ToString().Equals("F")) //tasfie
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        walet -= myconvert.todecimal(dr["kif"]);
                        mandeh -= myconvert.todecimal(dr["totalbed"]);
                        mandeh -= myconvert.todecimal(dr["khoshhesabi"]);
                    }
                    if (dr["sta"].ToString().Equals("X")) //انتقال وجه از کیف
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        walet -= myconvert.todecimal(dr["totalbed"]);
                    }
                    if (dr["sta"].ToString().Equals("R")) // taghsit mojadad
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        mandeh += myconvert.todecimal(dr["haml"]);
                        mandeh -= myconvert.todecimal(dr["takhfif"]);
                        mandeh -= myconvert.todecimal(dr["khoshhesabi"]);
                    }
                    if (dr["sta"].ToString().Equals("U")) //marjoe
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        if (!dr["sta2"].ToString().Trim().Equals("4"))
                        {
                            if (persiandate.datediff(HttpContext.Current.Session["dp_change_formol_date"].ToString(), dr["solddate"].ToString()) > 0)
                                walet += myconvert.todecimal(dr["totalbed"]);
                            else
                                mandeh -= myconvert.todecimal(dr["totalbed"]);
                        }
                    }
                    if (dr["sta"].ToString().Equals("I") || dr["sta"].ToString().Equals("O")) //snad motefareghe
                    {
                        lastdate = dr["solddate"].ToString().Trim();
                        if (dr["cobank5"].ToString().Trim().Equals(vascode))
                        {
                            mandeh += myconvert.todecimal(dr["m1"]);
                            mandeh -= myconvert.todecimal(dr["m2"]);
                        }
                        else
                        {
                            mandeh -= myconvert.todecimal(dr["m1"]);
                            mandeh += myconvert.todecimal(dr["m2"]);
                        }
                    }
                }

                ArrayList resp = new ArrayList(3);
                resp.Add(mandeh);
                resp.Add(walet);
                resp.Add(lastdate);
                return resp;
            }
            public static DataView getsoorathesab(string vascode, string sherkat)
            {

                string vastype = decode.t2grp(vascode);

                SQLH sqhand = new SQLH();
                string tsql = @"SELECT a.* , 1000000000000 AS mandeh,dp_main.bedehkar AS bedghest, 1000000000000 AS besghest, 1000000000000 AS walet, 1000 AS radif, '          ' AS noeamal, dpdtl.totgh,'                  ' as karbar,RTRIM(CAST(DATEPART(hour, a.createdate) AS char)) + ':' + CAST(DATEPART(minute, a.createdate) AS char) AS saat ,
                            bank_daftar.cobank5, bank_daftar.vas AS vas2, bank_daftar.m1, bank_daftar.m2  FROM khoroji_kol AS a LEFT OUTER JOIN dp_main ON a.factor = dp_main.factor 
                            LEFT OUTER JOIN (SELECT        factor, vas, SUM(mablagh) AS totgh FROM dp_detail GROUP BY factor, vas) AS dpdtl ON a.factor = dpdtl.factor AND a.vas = dpdtl.vas 
                            LEFT OUTER JOIN   bank_daftar ON a.factor = bank_daftar.factor AND a.sherkat = bank_daftar.sherkat AND (a.sta = 'I' OR a.sta = 'O')  where (a.sherkat=" + sherkat + " or " + sherkat + "=-1)  and  (a.vas = '" + vascode + "'  or bank_daftar.vas='" + vascode + "' or bank_daftar.cobank5='" + vascode + "') ORDER BY a.solddate, a.factor,a.sta";

                DataView dv = sqhand.SqlExecute(tsql, "dv"); // 5 s


                int i = 0;
                decimal mandeh = 0;
                decimal walet = 0;
                foreach (DataRowView dr in dv)
                {
                    i++;
                    dr["radif"] = i;
                    string sharh = "";
                    string sharh2 = "";
                    dr["karbar"] = decode.u2name(dr["uc"].ToString());


                    if (dr["sta"].ToString().Equals("S")) //factor forosh
                    {
                        decimal takhfif = myconvert.todecimal(dr["takhfif"]);
                        decimal peyed = myconvert.todecimal(dr["kif"]) +
                            myconvert.todecimal(dr["kart"]) +
                            myconvert.todecimal(dr["naghd"]) +
                            myconvert.todecimal(dr["buyprice"]);



                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            mandeh += myconvert.todecimal(dr["ghesti"]);
                            mandeh += myconvert.todecimal(dr["haml"]);
                        }
                        dr["besghest"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "فروش";
                        dr["takhfif"] = 0;
                        DataRow drb = dv.Table.NewRow();
                        DataRow drc = dv.Table.NewRow();

                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            mandeh -= peyed;
                            mandeh -= takhfif;
                            walet -= myconvert.todecimal(dr["kif"]);
                        }


                        drb["factor"] = dr["factor"];
                        drb["solddate"] = dr["solddate"];
                        drb["radif"] = dr["radif"];
                        drb["sharh"] = dr["sharh"];
                        drb["totalbed"] = 0;
                        drb["besghest"] = peyed;
                        drb["mandeh"] = mandeh;
                        drb["walet"] = walet;
                        drb["noeamal"] = "پیش قسط";
                        drb["sta"] = "S2";
                        drb["takhfif"] = takhfif;

                        sharh = dr["sharh"].ToString().Trim() + " مبلغ کل " + myconvert.todecimal(dr["naghdi"]).ToString("0,0");

                        sharh += " بابت ";

                        dr["totalbed"] = myconvert.todecimal(dr["ghesti"]) + myconvert.todecimal(dr["haml"]);


                        DataView dvtmp = sqhand.SqlExecute("select *,kcodes.persian as esmkala from khoroji left join kcodes on khoroji.mtcod=kcodes.kcode where factor=" + dr["factor"].ToString(), "dv");
                        foreach (DataRowView drtmp in dvtmp)
                        {
                            sharh += myconvert.todecimal(drtmp["meghdar"]).ToString().Trim() + " گرم " + drtmp["persian"].ToString().Trim() + " با کارمزد " + myconvert.todecimal(drtmp["fi_es"]).ToString() + "%" + "|";
                        }
                        if (myconvert.todecimal(dr["haml"].ToString()) != 0)
                            sharh += " هزینه :" + myconvert.todecimal(dr["haml"]).ToString("0.0") + "|";
                        if (!dr["seller"].ToString().Equals("0"))
                            sharh += "مدیر فروش :" + decode.u2name(dr["seller"].ToString()) + "|";
                        dr["sharh"] = sharh;

                        if (myconvert.toint(dr["takhfif"]) != 0) sharh2 += " تخفیف : " + (myconvert.toint(dr["takhfif"])).ToString("0,0") + "|";
                        if (myconvert.todecimal(dr["kif"]) > 0) sharh2 += " از کیف : " + myconvert.todecimal(dr["kif"]).ToString("0,0") + "|";
                        if (myconvert.todecimal(dr["kart"]) > 0)
                        {
                            sharh2 += " کارت : " + myconvert.todecimal(dr["kart"]).ToString("0,0");
                            sharh2 += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh2 += drbd["tarikhc"].ToString() + " ";
                            sharh2 += ")" + "|";
                        }
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh2 += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی " + "|";
                        if (myconvert.todecimal(dr["buyprice"]) > 0)
                        {
                            sharh2 += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                            DataView dvjtala = sqhand.SqlExecute("select sum(meghdar) from anbar where pro='R' and hno='" + dr["buyhno"] + "' and sherkat=" + sherkat, "dv");
                            sharh2 += dvjtala[0][0].ToString() + "گرم" + "|";
                        }
                        drb["sharh"] = sharh2;
                        drb["sherkat"] = dr["sherkat"];
                        if (peyed > 0 || takhfif > 0)
                        {
                            dv.Table.Rows.Add(drb);
                        }







                    }
                    if (dr["sta"].ToString().Equals("D")) //daryaft ghest
                    {
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            walet -= myconvert.todecimal(dr["kif"]);
                            mandeh -= myconvert.todecimal(dr["totalbed"]);
                        }
                        dr["besghest"] = myconvert.todecimal(dr["totalbed"]);
                        dr["bedghest"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "دریافت قسط";
                        dr["totalbed"] = 0;
                        dr["totalbed"] = 0;
                        dr["sh_rasmi"] = "0";
                        sharh = dr["sharh"].ToString().Trim();
                        if (myconvert.todecimal(dr["kif"]) > 0) sharh += myconvert.todecimal(dr["kif"]).ToString("0,0") + " از کیف |";
                        if (myconvert.todecimal(dr["kart"]) > 0)
                        {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ")|";
                        }
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی |";
                        if (myconvert.todecimal(dr["buyprice"]) > 0)
                        {
                            sharh += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                            DataView dvjtala = sqhand.SqlExecute("select sum(meghdar) from anbar where pro='R' and hno='" + dr["buyhno"] + "' and sherkat=" + sherkat, "dv");
                            sharh += dvjtala[0][0].ToString() + "گرم|";
                        }
                        if (myconvert.todecimal(dr["shomar2"]) != 0) sharh += "مربوط به فاکتور" + dr["shomar2"].ToString() + "|";
                        if (myconvert.todecimal(dr["takhfif"]) > 0) sharh += myconvert.todecimal(dr["takhfif"]).ToString("0,0") + " تخفیف ویژه |";
                        if (myconvert.todecimal(dr["khoshhesabi"]) > 0) sharh += myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0") + " خوشحسابی |";

                        dr["sharh"] = sharh;
                    }
                    if (dr["sta"].ToString().Equals("W")) //sharj kif
                    {
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            walet += myconvert.todecimal(dr["totalbed"]);
                        }
                        if (myconvert.todecimal(dr["kif"]) > 0)
                        {
                            mandeh += myconvert.todecimal(dr["kif"]);
                        }

                        dr["walet"] = walet;
                        dr["noeamal"] = "شارژ کیف";
                        dr["mandeh"] = mandeh;
                        dr["besghest"] = 0;
                        dr["totalbed"] = 0;
                        sharh = dr["sharh"].ToString().Trim();

                        if (myconvert.todecimal(dr["kif"]) > 0) sharh += myconvert.todecimal(dr["kif"]).ToString("0,0") + " از حساب |";
                        if (myconvert.todecimal(dr["kart"]) > 0)
                        {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت ";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ") |";
                        }
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی |";
                        if (myconvert.todecimal(dr["haml"]) > 0) sharh += myconvert.todecimal(dr["haml"]).ToString("0,0") + " هدیه |";
                        if (myconvert.todecimal(dr["buyprice"]) > 0)
                        {
                            sharh += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                            DataView dvjtala = sqhand.SqlExecute("select sum(meghdar) from anbar where pro='R' and hno='" + dr["buyhno"] + "' and sherkat=" + sherkat, "dv");
                            sharh += dvjtala[0][0].ToString() + "گرم |";
                        }
                        if (!dr["jahate"].ToString().Trim().Equals("")) sharh += "انتقالی از" + dr["jahate"].ToString() + " " + decode.t2name(dr["jahate"].ToString()) + " |";
                        if (myconvert.todecimal(dr["shomar2"]) != 0) sharh += " | مربوط به فاکتور" + dr["shomar2"].ToString();

                        dr["sharh"] = sharh;
                        dr["sh_rasmi"] = "0";
                    }
                    if (dr["sta"].ToString().Equals("X")) //انتقال وجه از کیف
                    {
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            walet -= myconvert.todecimal(dr["totalbed"]);
                        }
                        dr["walet"] = walet;
                        dr["noeamal"] = "انتقال کیف";
                        dr["mandeh"] = mandeh;
                        dr["besghest"] = 0;
                        dr["totalbed"] = 0;
                        sharh = dr["sharh"].ToString().Trim();
                        if (myconvert.todecimal(dr["kif"]) > 0) sharh += myconvert.todecimal(dr["kif"]).ToString("0,0") + " از کیف |";
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی |";
                        sharh += "به" + dr["jahate"].ToString() + " " + decode.t2name(dr["jahate"].ToString());
                        dr["sharh"] = sharh;
                        dr["sh_rasmi"] = "0";
                    }
                    if (dr["sta"].ToString().Equals("P")) //pardakht be moshtari
                    {
                        //   walet += myconvert.todecimal(dr["kif"]);
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            if ((persiandate.datediff(HttpContext.Current.Session["dp_change_formol_date"].ToString(), dr["solddate"].ToString()) > 0 || !vastype.StartsWith("مشتر")) && dr["shomar2"].ToString().Trim().Equals(""))
                                mandeh += myconvert.todecimal(dr["totalbed"]);
                            else
                                walet -= myconvert.todecimal(dr["totalbed"]);
                        }
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "پرداخت";
                        dr["besghest"] = 0;
                        sharh = dr["sharh"].ToString().Trim();
                        if (myconvert.todecimal(dr["kif"]) > 0) sharh += "مبلغ" + myconvert.todecimal(dr["kif"]).ToString("0,0") + " از کیف |";
                        if (myconvert.todecimal(dr["kart"]) > 0)
                        {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ")|";
                        }
                        if (myconvert.todecimal(dr["takhfif"]) > 0) sharh += "برگشت از تخفیف" + myconvert.todecimal(dr["takhfif"]).ToString("0,0") + "  |";
                        //sharh += "مبلغ" + myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh += "مبلغ" + myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی |";
                        if (myconvert.todecimal(dr["buyprice"]) > 0)
                        {
                            sharh += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                            DataView dvjtala = sqhand.SqlExecute("select sum(meghdar) from anbar where pro='H' and hno='" + dr["buyhno"] + "' and sherkat=" + sherkat, "dv");
                            sharh += dvjtala[0][0].ToString().Trim() + "گرم|";
                        }
                        dr["sharh"] = sharh;
                        dr["sh_rasmi"] = DBNull.Value;
                    }
                    if (dr["sta"].ToString().Equals("Z")) //zemanat
                    {
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        if (dr["sta2"].ToString().Equals("1"))
                            dr["noeamal"] = "عودت ضمانت";
                        else
                            dr["noeamal"] = "ضمانت";
                        dr["besghest"] = 0;
                        dr["besghest"] = 0;
                        dr["bedghest"] = 0;
                        dr["sh_rasmi"] = "0";
                        sharh = dr["sharh"].ToString().Trim() + " به وزن " + dr["zemanat_vazn"].ToString();
                        dr["sharh"] = sharh;
                    }
                    if (dr["sta"].ToString().Equals("F")) //tasfie
                    {
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            walet -= myconvert.todecimal(dr["kif"]);
                            mandeh -= myconvert.todecimal(dr["totalbed"]);
                            //mandeh -= myconvert.todecimal(dr["takhfif"]);
                            mandeh -= myconvert.todecimal(dr["khoshhesabi"]);
                        }
                        dr["besghest"] = myconvert.todecimal(dr["totalbed"]) + myconvert.todecimal(dr["khoshhesabi"]);
                        dr["totalbed"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "تسویه";

                        sharh = dr["sharh"].ToString().Trim();
                        if (myconvert.todecimal(dr["kif"]) > 0) sharh += myconvert.todecimal(dr["kif"]).ToString("0,0") + " از کیف " + "|";
                        if (myconvert.todecimal(dr["kart"]) > 0)
                        {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت ";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ")" + "|";
                        }
                        if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی " + "|";
                        if (myconvert.todecimal(dr["buyprice"]) > 0)
                        {
                            sharh += " طلای معاوضه " + myconvert.todecimal(dr["buyprice"]).ToString("0,0") + " بابت ";
                            DataView dvjtala = sqhand.SqlExecute("select sum(meghdar) from anbar where pro='R' and hno='" + dr["buyhno"] + "' and sherkat=" + sherkat, "dv");
                            sharh += dvjtala[0][0].ToString() + "گرم" + "|";
                        }
                        if (myconvert.todecimal(dr["takhfif"]) > 0) sharh += myconvert.todecimal(dr["takhfif"]).ToString("0,0") + " تخفیف ویژه  " + "|";
                        if (myconvert.todecimal(dr["khoshhesabi"]) > 0) sharh += myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0") + " خوش حسابی  " + "|";
                        dr["sharh"] = sharh;
                        dr["sh_rasmi"] = "0";
                    }
                    if (dr["sta"].ToString().Equals("R")) // taghsit mojadad
                    {
                        decimal taghiir = 0;
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            taghiir += myconvert.todecimal(dr["haml"]);
                            taghiir -= myconvert.todecimal(dr["takhfif"]);
                            taghiir -= myconvert.todecimal(dr["khoshhesabi"]);
                            mandeh += taghiir;
                        }
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "تقسیط مجدد";

                        if (taghiir > 0)
                        {
                            dr["besghest"] = 0;
                            dr["totalbed"] = Math.Abs(taghiir);
                        }
                        else
                        {
                            dr["besghest"] = Math.Abs(taghiir);
                            dr["totalbed"] = 0;
                        }

                        dr["bedghest"] = 0;
                        dr["sh_rasmi"] = DBNull.Value;
                        sharh = dr["sharh"].ToString().Trim() + "|";
                        if (myconvert.todecimal(dr["khoshhesabi"]) != 0) sharh += "خوش حسابی " + myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0") + "|";
                        if (myconvert.todecimal(dr["haml"]) != 0) sharh += "کارمزد  " + myconvert.todecimal(dr["haml"]).ToString("0,0") + "|";
                        if (myconvert.todecimal(dr["takhfif"]) != 0) sharh += "تخفیف  " + myconvert.todecimal(dr["takhfif"]).ToString("0,0");
                        dr["sharh"] = sharh;
                    }
                    if (dr["sta"].ToString().Equals("U")) //marjoe
                    {
                        if (!dr["sherkat"].ToString().Equals("9"))
                        {
                            if (!dr["sta2"].ToString().Trim().Equals("4"))
                            {
                                if (persiandate.datediff(HttpContext.Current.Session["dp_change_formol_date"].ToString(), dr["solddate"].ToString()) > 0)
                                {
                                    walet += myconvert.todecimal(dr["totalbed"]);
                                    dr["totalbed"] = 0;
                                }
                                else
                                {
                                    mandeh -= myconvert.todecimal(dr["totalbed"]);
                                }
                            }
                            else
                            {
                                dr["totalbed"] = 0;
                            }
                        }
                        dr["besghest"] = 0;
                        dr["bedghest"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "مرجوع ";
                        dr["sh_rasmi"] = "0";
                        sharh = dr["sharh"].ToString().Trim();
                        if (myconvert.todecimal(dr["buyhno"]) > 0)
                        {

                            DataView dvjtala = sqhand.SqlExecute("select * from anbar where pro='R' and hno='" + dr["buyhno"] + "' and sherkat=" + dr["sherkat"].ToString(), "dv");
                            sharh += dvjtala[0]["meghdar"].ToString() + " گرم " + dvjtala[0]["sharh"].ToString().Trim() + " به قیمت " + (myconvert.todecimal(dr["buyprice"]) - myconvert.todecimal(dr["haml"])).ToString("0,0");
                        }
                        if (myconvert.todecimal(dr["takhfif"]) != 0) sharh += " | برگشت از تخفیف" + myconvert.todecimal(dr["takhfif"]).ToString("0,0");
                        if (myconvert.todecimal(dr["khoshhesabi"]) != 0) sharh += " | برگشت سود تقسیط" + myconvert.todecimal(dr["khoshhesabi"]).ToString("0,0");
                        if (myconvert.todecimal(dr["haml"]) != 0) sharh += " | برگشت از هزینه" + myconvert.todecimal(dr["haml"]).ToString("0,0");
                        if (myconvert.todecimal(dr["shomar2"]) != 0) sharh += " | مربوط به فاکتور" + dr["shomar2"].ToString();

                        if (dr["sta2"].ToString().Equals("4")) sharh += " وزن به وزن جدید";
                        if (dr["sta2"].ToString().Equals("2")) sharh += " مرجوع توافقی";
                        if (dr["sta2"].ToString().Equals("1")) sharh += " وزن به وزن قدیمی";
                        if (dr["sta2"].ToString().Equals("0")) sharh += " مرجوع کامل";
                        dr["sharh"] = sharh;
                    }
                    if (dr["sta"].ToString().Equals("T")) //tozihat
                    {
                        dr["besghest"] = 0;
                        dr["bedghest"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "توضیحات";

                        if (dr["sta2"].ToString().Equals("1"))
                            dr["sharh"] = " SMS  " + dr["sharh"].ToString().Trim();
                        if (dr["sta2"].ToString().Equals("2"))
                            dr["sharh"] = " SMS-OK  " + dr["sharh"].ToString().Trim();
                        dr["besghest"] = 0;
                        dr["sh_rasmi"] = DBNull.Value;

                    }
                    if (dr["sta"].ToString().Equals("Q")) //darkhast
                    {
                        dr["besghest"] = 0;
                        dr["bedghest"] = 0;
                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = "درخواست";
                        if (dr["sta2"].ToString().Equals("3") || dr["sta2"].ToString().Equals("4"))
                            dr["sharh"] = " ! " + dr["sharh"].ToString().Trim() + " " + myconvert.todecimal(dr["totalbed"]).ToString("0,0") + "|";
                        else
                            dr["sharh"] = "  " + dr["sharh"].ToString().Trim() + "|";

                        DataView dv2 = sqhand.SqlExecute("select * from dt_mesa where shomare=" + dr["factor"].ToString(), "dv");
                        foreach (DataRowView dr2 in dv2)
                        {
                            dr["sharh"] += decode.u2name(dr2["uc"].ToString()) + " " + dr2["tarikh"].ToString().Trim() + " : " + dr2["sharh"].ToString().Trim() + "|";
                        }

                        dr["totalbed"] = "0";
                        dr["besghest"] = 0;
                        dr["sh_rasmi"] = DBNull.Value;

                    }
                    if (dr["sta"].ToString().Equals("I") || dr["sta"].ToString().Equals("O")) //snad motefareghe
                    {
                        if (dr["cobank5"].ToString().Trim().Equals(vascode))
                        {
                            if (!dr["sherkat"].ToString().Equals("9"))
                            {
                                mandeh += myconvert.todecimal(dr["m1"]);
                                mandeh -= myconvert.todecimal(dr["m2"]);
                            }
                            dr["totalbed"] = myconvert.todecimal(dr["m1"]);
                            dr["besghest"] = myconvert.todecimal(dr["m2"]);

                        }
                        else
                        {
                            if (!dr["sherkat"].ToString().Equals("9"))
                            {
                                mandeh -= myconvert.todecimal(dr["m1"]);
                                mandeh += myconvert.todecimal(dr["m2"]);
                            }
                            dr["totalbed"] = myconvert.todecimal(dr["m2"]);
                            dr["besghest"] = myconvert.todecimal(dr["m1"]);
                        }

                        dr["mandeh"] = mandeh;
                        dr["walet"] = walet;
                        dr["noeamal"] = " سند متفرقه";

                        sharh = dr["sharh"].ToString().Trim();
                        dr["sharh"] = sharh;
                    }

                    if (myconvert.todecimal(dr["takhfif"]) > 0)
                    {
                        if (dr["sta"].ToString().Trim().Equals("P"))
                        {
                            dr["takhfif"] = 0;
                        }
                        else
                        {
                            /*                        if (myconvert.todecimal(dr["totalbed"]) > 0)
                                                        dr["totalbed"] = myconvert.todecimal(dr["totalbed"]) + myconvert.todecimal(dr["takhfif"]);

                                                    if (myconvert.todecimal(dr["besghest"]) > 0 && myconvert.todecimal(dr["besghest"]) >= myconvert.todecimal(dr["takhfif"]))
                                                        dr["besghest"] = myconvert.todecimal(dr["besghest"]) - myconvert.todecimal(dr["takhfif"]);

                                                    if (myconvert.todecimal(dr["besghest"]) > 0 && myconvert.todecimal(dr["besghest"]) < myconvert.todecimal(dr["takhfif"]))
                                                    {

                                                        dr["totalbed"] = myconvert.todecimal(dr["takhfif"]) - myconvert.todecimal(dr["besghest"]);
                                                        dr["besghest"] = 0;
                                                    }
                            */
                        }
                    }
                }
                dv.Sort = "factor,sta";



                return dv;

            }

            public static factorinfo calcfactorinfo(string thisfactor, string vascode)
            {
                SQLH sqhand = new SQLH();
                factorinfo response = new factorinfo();
                if (thisfactor.Trim().Equals("0")) return response;
                string dp_change_formol_date = HttpContext.Current.Session["dp_change_formol_date"].ToString();
                DataView koldaftar = sqhand.SqlExecute("SELECT  s_id,factor, vas, mablagh as ghest, tres,mablagh2 as tasvie FROM   dp_detail where sherkat=0 and  vas='" + vascode + "' and factor=" + thisfactor + " order by tres", "dv");
                DataView factordv = sqhand.SqlExecute("SELECT  * FROM   khoroji_kol where sherkat=0 and  vas='" + vascode + "' and (sta='S' or sta='R') and factor=" + thisfactor, "dv");
                DataView rdv = sqhand.SqlExecute("select * from khoroji_kol where sherkat=0 and vas='" + vascode + "' and sta='R' and shomar2=" + thisfactor, "dv");
                if (rdv.Count > 0) response.Rdetails = rdv[0]; else response.Rdetails = null;

                response.details = factordv[0];
                response.solddate = factordv[0]["solddate"].ToString();
                DataView daryaftiha;
                //            DataView dv = getsoorathesab(vascode, "0");
                //string factortasfie = "0";
                //            string factorsefr = "0";
                //            string firstfactor = "0";
                /*            if (sqhand.SqlExecute("select factor from khoroji_kol where vas='" + vascode + "' and sherkat=0 and sta='F'", "dv").Count > 0)
                            {
                                factortasfie = sqhand.SqlExecute("select max(factor) from khoroji_kol where vas='" + vascode + "' and sherkat=0 and sta='F'", "dv")[0][0].ToString();
                            }
                            foreach (DataRowView dr in dv)
                            {
                                if (myconvert.todecimal(dr["mandeh"]) == 0 && persiandate.datediff(dr["solddate"].ToString(), dp_change_formol_date) <= 0) factorsefr = dr["factor"].ToString();
                            }
                            if (myconvert.toint(factorsefr) > myconvert.toint(factortasfie)) factortasfie = factorsefr;
                */

                if (persiandate.datediff(dp_change_formol_date, response.solddate) > 0)
                {

                    if (koldaftar.Count > 0 && koldaftar[0]["tres"].ToString().Equals("1400/05/28"))
                    {
                        daryaftiha = sqhand.SqlExecute("SELECT  * FROM   khoroji_kol where sherkat=0 and  vas='" + vascode +
                            "' and (" +
                            //"(sta='U' and sta2<>4 and shomar2<='" + thisfactor + "' and factor>" + thisfactor + ") or " +
                            " (sta='D' and shomar2='' )" +
                            " or (sta='P' and shomar2='' and ((factor>" + thisfactor + " or solddate>'" + response.solddate + "') and solddate<'" + dp_change_formol_date + "')) " +
                            " or (sta='F' and shomar2='' and ((factor>" + thisfactor + " or solddate>'" + response.solddate + "') and solddate<'" + dp_change_formol_date + "'))  or (sta='R' and shomar2='" + thisfactor + "')" +
                            " or ((sta='D' or sta='U' or sta='W' or sta='P') and shomar2='" + thisfactor + "')" +
                            ")", "dv");

                    }
                    else
                    {
                        daryaftiha = sqhand.SqlExecute("SELECT  * FROM   khoroji_kol where sherkat=0 and  vas='" + vascode +
                            "' and (" +
                            //" (sta='U' and sta2<>4 and shomar2<='" + thisfactor + "' and factor>" + thisfactor + ") or " +
                            " (sta='D' and shomar2='' and ((factor>" + thisfactor + " or solddate>'" + response.solddate + "') and solddate<'" + dp_change_formol_date + "')) " +
                            " or (sta='P' and shomar2='' and ((factor>" + thisfactor + " or solddate>'" + response.solddate + "') and solddate<'" + dp_change_formol_date + "')) " +
                            " or (sta='F' and shomar2='' and ((factor>" + thisfactor + " or solddate>'" + response.solddate + "') and solddate<'" + dp_change_formol_date + "')) or (sta='R' and shomar2='" + thisfactor + "')" +
                            " or ((sta='D' or sta='U' or sta='W' or sta='P') and shomar2='" + thisfactor + "'))", "dv");
                    }

                }
                else
                {
                    daryaftiha = sqhand.SqlExecute("SELECT  * FROM   khoroji_kol where sherkat=0 and  vas='" + vascode + "' and (sta='D' or sta='U' or sta='W' or sta='P' or sta='R') and shomar2='" + thisfactor + "'", "dv");
                }

                foreach (DataRowView dr in daryaftiha)
                {
                    if (dr["sta"].ToString().Trim().Equals("U") && dr["sta2"].ToString().Trim().Equals("4"))
                        dr["totalbed"] = 0;
                }



                response.koldaftar = koldaftar;
                response.daryaftiha = daryaftiha;
                response.jameaghsat = myconvert.todecimal(koldaftar.Table.Compute("sum(ghest)", ""));
                response.lastghest = koldaftar.Table.Compute("max(tres)", "").ToString();


                foreach (DataRowView dr in daryaftiha)
                {
                    if (dr["sta"].ToString().Equals("W"))
                        response.jamedaryaft -= myconvert.todecimal(dr["totalbed"]);

                    if (dr["sta"].ToString().Equals("D"))
                        response.jamedaryaft += myconvert.todecimal(dr["totalbed"]);
                    if (dr["sta"].ToString().Equals("U"))
                        response.jamedaryaft += myconvert.todecimal(dr["totalbed"]);

                    if (dr["sta"].ToString().Equals("R"))
                        response.jamedaryaft += myconvert.todecimal(dr["totalbed"]);

                    if (dr["sta"].ToString().Equals("P"))
                    {
                        response.jamedaryaft -= myconvert.todecimal(dr["totalbed"]);
                        dr["totalbed"] = -1 * myconvert.todecimal(dr["totalbed"]);
                    }

                    if (dr["sta"].ToString().Equals("D") || dr["sta"].ToString().Equals("U") || dr["sta"].ToString().Equals("R"))
                    {
                        if (myconvert.toint(dr["takhfif"]) > 0)
                        {
                            response.takhfifvije = myconvert.toint(dr["takhfif"]);
                            //  response.jamedaryaft += myconvert.todecimal(dr["takhfif"]);
                        }
                        if (myconvert.toint(dr["khoshhesabi"]) > 0)
                        {
                            response.takhfifsood = myconvert.toint(dr["khoshhesabi"]);
                            //response.jamedaryaft += myconvert.todecimal(dr["khoshhesabi"]);
                        }
                    }
                }
                response.khoshhesabiamalshode = response.takhfifsood;
                response.aslbedehi = response.jameaghsat - response.jamedaryaft;
                if (persiandate.datediff(dp_change_formol_date, response.solddate) <= 0)
                {
                    response.tasvierooz = tasvierooz_new(response.solddate, response.lastghest, response.daryaftiha, response.koldaftar, factordv, response.jamedaryaft) + response.takhfifsood;
                    response.khoshhesabirooz = response.aslbedehi - response.tasvierooz;
                }
                else
                {
                    decimal khoshhesabirooz = khoshhesabirooz_test(response.solddate, response.lastghest, response.daryaftiha, response.koldaftar, response.details, response.khoshhesabiamalshode);
                    response.khoshhesabirooz = khoshhesabirooz;
                    response.tasvierooz = response.jameaghsat - khoshhesabirooz - response.jamedaryaft;

                }

                return response;
            }

            static decimal khoshhesabirooz_old(string startdate, string lastdate, DataView pardakhti, DataView aghsat, DataRowView factor, decimal khoshhesabiemalshode)
            {
                decimal ret = 0;
                if (aghsat.Count == 0) return 0;
                if (khoshhesabiemalshode > 0) return 0;
                if (persiandate.datediff(lastdate, persiandate.datef()) < 1)
                {
                    /*                decimal sumaghsat = myconvert.todecimal(aghsat.Table.Compute("sum(ghest)", ""));
                                    decimal sumpardakht = myconvert.todecimal(pardakhti.Table.Compute("sum(totalbed)", ""));
                                    ret = sumaghsat - sumpardakht;
                    */
                    return 0;
                }
                else
                {
                    /*                decimal sood = myconvert.todecimal(factor["ghesti"]) - myconvert.todecimal(factor["naghdi"]);
                                    int tedadroozha = persiandate.datediff(lastdate, startdate);
                                    int zoodkard = persiandate.datediff(lastdate, persiandate.datef());
                                    ret = Math.Ceiling(sood / tedadroozha * zoodkard);
                    */

                    foreach (DataRowView dr in aghsat)
                    {

                        decimal dtdf = persiandate.datediff(dr["tres"].ToString(), persiandate.datef());
                        if (dtdf > 1)
                        {
                            decimal dtdfinmonth = Math.Round(dtdf / 30);
                            decimal khoshesabi = Math.Round(myconvert.toint(dr["ghest"]) * 5 / 100 * (dtdfinmonth + 1));
                            ret += khoshesabi;
                        }
                    }


                }
                ret = (Math.Ceiling(ret / 10000)) * 10000;

                return ret;
            }

            static decimal khoshhesabirooz_test(string startdate, string lastdate, DataView pardakhti, DataView aghsat, DataRowView factor, decimal khoshhesabiemalshode)
            {
                decimal ret = 0;
                decimal sood = 0;
                if (aghsat.Count == 0) return 0;
                if (khoshhesabiemalshode > 0) return 0;
                if (persiandate.datediff(aghsat[0]["tres"].ToString(), "1400/06/01") <= 0) return 0;
                if (persiandate.datediff(lastdate, persiandate.datef()) < 1) return 0;
                decimal miangin = myconvert.todecimal(aghsat.Table.Compute("sum(ghest)", "")) / aghsat.Count;
                foreach (DataRowView dr in aghsat)
                {
                    if (Math.Abs(myconvert.todecimal(dr["ghest"]) - miangin) > 100000) return 0;
                }

                if (factor["sta"].ToString().Equals("S"))
                    sood = myconvert.todecimal(factor["ghesti"]) - myconvert.todecimal(factor["naghdi"]);
                else
                {
                    decimal sumaghsat = myconvert.todecimal(aghsat.Table.Compute("sum(ghest)", ""));
                    sood = (sumaghsat * (aghsat.Count * 5)) / (100 + aghsat.Count * 5);
                }
                int tedadroozha = persiandate.datediff(lastdate, startdate);
                int zoodkard = persiandate.datediff(lastdate, persiandate.datef());
                ret = Math.Ceiling(sood / tedadroozha * zoodkard);


                ret = (Math.Ceiling(ret / 10000)) * 10000;

                return ret;
            }
            static decimal tasvierooz_new(string startdate, string lastdate, DataView pardakhti, DataView aghsat, DataView factor, decimal jamedaryaft)
            {
                decimal rate = myconvert.todecimal(7.675);
                decimal prepay = myconvert.todecimal(factor[0]["naghd"]) + myconvert.todecimal(factor[0]["kart"]) + myconvert.todecimal(factor[0]["kif"]) + myconvert.todecimal(factor[0]["buyprice"]);
                decimal aghsatcount = aghsat.Count;
                decimal mandeghesti = myconvert.todecimal(aghsat.Table.Compute("sum(ghest)", ""));
                if (aghsatcount == 0)
                    return myconvert.todecimal(factor[0]["totalbed"]) - prepay;
                string dp_change_formol_date = HttpContext.Current.Session["dp_change_formol_date"].ToString();
                decimal mandeghestirooz = 0;
                decimal mandenaghdi = 0;
                if (persiandate.datediff(aghsat.Table.Compute("min(tres)", "").ToString(), "1400/06/01") < 1)
                    return mandeghesti - jamedaryaft;

                if (persiandate.datediff(dp_change_formol_date, startdate) > 0)
                {
                    mandenaghdi = mandeghesti / (100 + aghsatcount * 5) * 100;
                }
                else
                {
                    mandenaghdi = myconvert.todecimal(factor[0]["naghdi"]) - prepay - myconvert.todecimal(factor[0]["takhfif"]);
                }
                decimal netval = mandenaghdi;

                int tedadrooz = 0;
                decimal soodtapardakht = 0;
                decimal aslofare = 0;

                if (persiandate.datediff(persiandate.datef(), startdate) < 10)
                {
                    decimal kolpardakht = myconvert.todecimal(pardakhti.Table.Compute("sum(totalbed)", "sta<>'W'"));
                    kolpardakht -= myconvert.todecimal(pardakhti.Table.Compute("sum(totalbed)", "sta='W'"));
                    netval -= kolpardakht;
                    netval = (Math.Ceiling(netval / 10000)) * 10000;
                    return netval;
                }
                foreach (DataRowView row in pardakhti)
                {
                    if (netval > 0)
                    {
                        tedadrooz = persiandate.datediff(row["solddate"].ToString(), startdate);
                        soodtapardakht = (netval * rate * (tedadrooz + 1)) / (100 * 30);
                        aslofare = netval + soodtapardakht;
                        if (row["sta"].ToString().Equals("W"))
                            netval = aslofare + myconvert.todecimal(row["totalbed"]);
                        else
                            netval = aslofare - myconvert.todecimal(row["totalbed"]);
                    }
                    else
                    {
                        if (row["sta"].ToString().Equals("W"))
                            netval += myconvert.todecimal(row["totalbed"]);
                        else
                            netval -= myconvert.todecimal(row["totalbed"]);
                    }
                    startdate = row["solddate"].ToString();
                }
                if (netval > 0)
                {

                    tedadrooz = persiandate.datediff(persiandate.datef(), startdate);
                    soodtapardakht = (netval * rate * (tedadrooz + 1)) / (100 * 30);
                    aslofare = netval + soodtapardakht;
                    netval = aslofare - 0;
                }

                netval = (Math.Ceiling(netval / 10000)) * 10000;
                string todaymin5 = persiandate.datef(-5);
                aghsat.Sort = "tres";
                decimal comolativeghest = 0;
                foreach (DataRowView drx in aghsat)
                {
                    if (persiandate.datediff(drx["tres"].ToString(), todaymin5) > 0)
                    {
                        mandeghestirooz = myconvert.todecimal(drx["tasvie"]) + comolativeghest;
                        break;
                    }
                    comolativeghest += myconvert.todecimal(drx["ghest"]);
                }

                if (netval > mandeghestirooz) netval = (Math.Ceiling(mandeghestirooz / 10000)) * 10000;

                return netval;
            }
            public class factorinfo
            {
                public DataView koldaftar;
                public DataView daryaftiha;
                public DataRowView details;
                public DataRowView Rdetails;
                public string solddate = "";
                public string lastghest = "";
                public decimal jameaghsat = 0;
                public decimal jamedaryaft = 0;
                public decimal aslbedehi = 0;
                public decimal tasvierooz = 0;
                public decimal khoshhesabirooz = 0;
                public decimal khoshhesabiamalshode = 0;
                public decimal takhfifavalie = 0;
                public decimal takhfifsood = 0;
                public decimal takhfifvije = 0;
            }

        




        public static DataView getzamanat(string vascode)
        {
            SQLH sqhand=   new SQLH();
            return sqhand.SqlExecute("select a.*,anbar.sharh from anbar_mande a left join anbar on a.cert=anbar.cert where a.kala='000000' and a.cert2='" + vascode + "' and anbar.sherkat=0 and anbar.pro='R' ", "dv");
        }
    }
    public static class kharidlead
    {
        public static void updatemandevas(string vascode, string sherkat)
        {
            decimal[] retval = getmandevas(vascode, sherkat);
            SQLH sqhand = new SQLH();
            sqhand.SqlExecute("update vas_ori set tasis='" + persiandate.datetimeflag() + "',mande=" + retval[0].ToString() + ",walet=" + retval[1].ToString() + " where vascode='" + vascode + "'");

        }
        public static decimal[] getmandevas(string vascode, string sherkat, long factor = 0, string solddate = "")
        {
            decimal mandehtala = 0;
            decimal mandehrial = 0;
            SQLH sqhand = new SQLH();
            string tsql = "";
            if (factor == 0 && solddate.Equals(""))
                tsql = "select a.*,10000.000 as bedtala,10000.000 as bestala,10000.000 as mandehtala,1000000000000 as bedrial,1000000000000 as besrial,1000000000000 as mandehrial,1000 as radif,'              ' as noeamal,'          ' as anbarurl from khoroji_kol a where a.sherkat=" + sherkat + " and a.vas='" + vascode + "' order by a.solddate,a.factor"; if (factor != 0 && solddate.Equals(""))

                if (factor != 0 && !solddate.Equals(""))
                    tsql = "select a.*,10000.000 as bedtala,10000.000 as bestala,10000.000 as mandehtala,1000000000000 as bedrial,1000000000000 as besrial,1000000000000 as mandehrial,1000 as radif,'              ' as noeamal,'          ' as anbarurl from khoroji_kol a where a.factor<=" + factor + " and a.sherkat=" + sherkat + " and a.vas='" + vascode + "' order by a.solddate,a.factor";

            if (factor == 0 && !solddate.Equals(""))
                tsql = "select a.*,10000.000 as bedtala,10000.000 as bestala,10000.000 as mandehtala,1000000000000 as bedrial,1000000000000 as besrial,1000000000000 as mandehrial,1000 as radif,'              ' as noeamal,'          ' as anbarurl from khoroji_kol a where a.solddate<='" + solddate + "' and a.sherkat=" + sherkat + " and a.vas='" + vascode + "' order by a.solddate,a.factor";

            if (factor != 0 && !solddate.Equals(""))
                tsql = "select a.*,10000.000 as bedtala,10000.000 as bestala,10000.000 as mandehtala,1000000000000 as bedrial,1000000000000 as besrial,1000000000000 as mandehrial,1000 as radif,'              ' as noeamal,'          ' as anbarurl from khoroji_kol a where a.solddate<='" + solddate + "' and factor<=" + factor + " and a.sherkat=" + sherkat + " and a.vas='" + vascode + "' order by a.solddate,a.factor";


            DataView dv = sqhand.SqlExecute(tsql, "dv"); // 5 s

            foreach (DataRowView dr in dv)
            {
                dr["bedrial"] = 0;
                dr["besrial"] = 0;
                dr["mandehrial"] = 0;
                dr["bedtala"] = 0;
                dr["bestala"] = 0;
                dr["mandehtala"] = 0;

                if (dr["sta"].ToString().Equals("V")) // vorod
                {
                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                    mandehtala -= Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                }
                if (dr["sta"].ToString().Equals("K")) //khoroj 
                {
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                    mandehtala += Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                }

                if (dr["sta"].ToString().Equals("G")) //tabdil tala be rial
                {

                    mandehtala -= Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                }

                if (dr["sta"].ToString().Equals("Y")) //tabdil rial be tala 
                {

                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                    mandehtala += Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;

                }
                if (dr["sta"].ToString().Equals("D")) //daryaft naghdi
                {

                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                }
                if (dr["sta"].ToString().Equals("P")) //pardakht  naghdi
                {
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                }




            }



            decimal[] resp = new decimal[2];
            resp[0] = mandehrial;
            resp[1] = mandehtala;
            return resp;

        }

        public static DataView getsoorathesab(string vascode, string sherkat)
        {
            string tsql = "select a.*,10000.000 as bedtala,10000.000 as bestala,10000.000 as mandehtala,1000000000000 as bedrial,1000000000000 as besrial,1000000000000 as mandehrial,1000 as radif,'              ' as noeamal,'          ' as anbarurl,' ' as tipik from khoroji_kol a where a.sherkat=" + sherkat + " and a.vas='" + vascode + "' order by a.solddate,a.factor";
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute(tsql, "dv"); // 5 s

            int i = 0;
            decimal mandehtala = 0;
            decimal mandehrial = 0;
            string sharh;
            foreach (DataRowView dr in dv)
            {
                dr["bedrial"] = 0;
                dr["besrial"] = 0;
                dr["mandehrial"] = 0;
                dr["bedtala"] = 0;
                dr["bestala"] = 0;
                dr["mandehtala"] = 0;
                sharh = "";
                i++;
                dr["radif"] = i;

                if (dr["sta"].ToString().Equals("V")) // vorod
                {
                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                    mandehtala -= Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                    dr["mandehtala"] = mandehtala;
                    dr["noeamal"] = "ورود از بنکدار";
                    dr["mandehrial"] = mandehrial;
                    dr["bestala"] = dr["totalgold"];
                    dr["besrial"] = dr["totalbed"];
                    dr["sharh"] = dr["sharh"].ToString().Trim() + " وزن ترازو " + dr["buyprice"].ToString().Trim() + " رسید انبار شماره " + dr["buyhno"].ToString().Trim();
                    if (myconvert.todecimal(dr["takhfifvazni"]) != 0)
                        dr["sharh"] += " | تخفیف وزنی:" + myconvert.todecimal(dr["takhfifvazni"]).ToString("0,0");
                    dr["anbarurl"] = "riz_resid_anbar.aspx?hno=" + dr["buyhno"].ToString();
                    dr["tipik"] = "0";
                }
                if (dr["sta"].ToString().Equals("K")) //khoroj 
                {

                    dr["noeamal"] = "خروج به بنکدار";
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                    mandehtala += Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                    dr["mandehtala"] = mandehtala;
                    dr["mandehrial"] = mandehrial;
                    dr["bedtala"] = dr["totalgold"];
                    dr["bedrial"] = dr["totalbed"];
                    dr["sharh"] = dr["sharh"].ToString().Trim() + " وزن ترازو " + dr["buyprice"].ToString().Trim() + " حواله انبار شماره " + dr["buyhno"].ToString().Trim();
                    if (myconvert.todecimal(dr["takhfifvazni"]) != 0)
                        dr["sharh"] += " | تخفیف وزنی:" + myconvert.todecimal(dr["takhfifvazni"]).ToString("0,0");
                    dr["anbarurl"] = "riz_havale_anbar.aspx?hno=" + dr["buyhno"].ToString();
                    dr["tipik"] = "1";
                }

                if (dr["sta"].ToString().Equals("G")) //tabdil rial be tala
                {

                    mandehtala -= Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                    dr["mandehtala"] = mandehtala;
                    dr["noeamal"] = "تبدیل به طلا";
                    dr["mandehrial"] = mandehrial;
                    dr["bestala"] = dr["totalgold"];
                    dr["bedrial"] = dr["totalbed"];
                    dr["sharh"] = dr["sharh"].ToString().Trim() + " نرخ تبدیل " + myconvert.todecimal(dr["baseprice"]).ToString("0,0").Trim();
                }

                if (dr["sta"].ToString().Equals("Y")) //tabdil tala be rial
                {

                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                    mandehtala += Math.Floor(myconvert.todecimal(dr["totalgold"]) * 1000) / 1000;
                    dr["mandehtala"] = mandehtala;
                    dr["noeamal"] = "تبدیل به ریال";
                    dr["mandehrial"] = mandehrial;
                    dr["bedtala"] = dr["totalgold"];
                    dr["besrial"] = dr["totalbed"];
                    dr["sharh"] = dr["sharh"].ToString().Trim() + " نرخ تبدیل " + myconvert.todecimal(dr["baseprice"]).ToString("0,0").Trim();

                }
                if (dr["sta"].ToString().Equals("D")) //daryaft naghdi
                {

                    mandehrial -= myconvert.todecimal(dr["totalbed"]);
                    dr["mandehrial"] = mandehrial;
                    dr["mandehtala"] = mandehtala;
                    dr["bedrial"] = 0;
                    dr["besrial"] = myconvert.todecimal(dr["totalbed"]);
                    dr["noeamal"] = "دریافت نقدی";
                    sharh = dr["sharh"].ToString().Trim();
                    if (myconvert.todecimal(dr["kart"]) > 0)
                    {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ")|";
                    }
                    if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی ";
                    dr["sharh"] = sharh;
                }
                if (dr["sta"].ToString().Equals("P")) //pardakht  naghdi
                {
                    mandehrial += myconvert.todecimal(dr["totalbed"]);
                    dr["mandehrial"] = mandehrial;
                    dr["mandehtala"] = mandehtala;
                    dr["bedrial"] = myconvert.todecimal(dr["totalbed"]);
                    dr["besrial"] = 0;
                    dr["noeamal"] = "پرداخت نقدی";
                    sharh = dr["sharh"].ToString().Trim();
                    if (myconvert.todecimal(dr["kart"]) > 0)
                    {
                            sharh += myconvert.todecimal(dr["kart"]).ToString("0,0") + " با کارت |";
                            sharh += "(";
                            DataView dvbankdaftar = sqhand.SqlExecute("select * from bank_daftar inner join vas_ori on bank_daftar.cobank5=vas_ori.vascode where factor=" + dr["factor"] + " and vas_ori.vas_gr='بانک'", "dv");
                            foreach (DataRowView drbd in dvbankdaftar) sharh += drbd["tarikhc"].ToString() + " ";
                            sharh += ")|";
                    }
                    if (myconvert.todecimal(dr["naghd"]) > 0) sharh += myconvert.todecimal(dr["naghd"]).ToString("0,0") + " نقدی ";
                    dr["sharh"] = sharh;
                }


                if (dr["sta"].ToString().Equals("T")) //tozihat
                {
                    dr["mandehtala"] = mandehtala;
                    dr["noeamal"] = "توضیحات";
                    dr["mandehrial"] = mandehrial;
                }


            }

            return dv;
        }
    }
    public static class getaccessflag
    {
        public static int getflag(string urlstr, string sherkat, string cyear, string uc)
        {
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select menuid from webmenus where menulink='" + urlstr + "'", "dv");
            DataView dv2 = sqhand.SqlExecute("select rwa from uar3 where menu_value='" + dv[0][0].ToString() + "' and sherkat=" + sherkat + " and uc=" + uc + " and cyear='" + cyear + "'", "dv2");
            if (dv2.Count > 0)
                return myconvert.toint16(dv2[0][0]);
            return 0;
        }
        public static int getrwaflag(string urlstr, string sherkat, string cyear, string uc)
        {
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select menuid from webmenus where menulink='" + urlstr + "'", "dv");
            DataView dv2 = sqhand.SqlExecute("select rwa from uar3 where menu_value='" + dv[0][0].ToString() + "' and sherkat=" + sherkat + " and uc=" + uc + " and cyear='" + cyear + "'", "dv2");
            if (dv2.Count > 0)
                return myconvert.toint16(dv2[0][0]);
            return 0;
        }
        public static int getgaflag(string urlstr, string sherkat, string cyear, string uc)
        {
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select menuid from webmenus where menulink='" + urlstr + "'", "dv");
            DataView dv2 = sqhand.SqlExecute("select ga from uar3 where menu_value='" + dv[0][0].ToString() + "' and sherkat=" + sherkat + " and uc=" + uc + " and cyear='" + cyear + "'", "dv2");
            if (dv2.Count > 0)
                return myconvert.toint16(dv2[0][0]);
            return 0;
        }
    }

    public static class shomaremali
    {
        public static bool isvalid(string shmeli)
        {
            return isvalidhaghighi(shmeli);
        }
        private static bool isvalidhaghighi(string ncode)
        {

            if (ncode == null)
                return false;

            ncode = ncode.Trim();

            if (ncode.Length != 10)
                return false;
            Int64 j;
            if (!Int64.TryParse(ncode, out j))
                return false;


            int num1 = myconvert.toint16(ncode.Substring(0, 1)) * 10;
            int num2 = myconvert.toint16(ncode.Substring(1, 1)) * 9;
            int num3 = myconvert.toint16(ncode.Substring(2, 1)) * 8;
            int num4 = myconvert.toint16(ncode.Substring(3, 1)) * 7;
            int num5 = myconvert.toint16(ncode.Substring(4, 1)) * 6;
            int num6 = myconvert.toint16(ncode.Substring(5, 1)) * 5;
            int num7 = myconvert.toint16(ncode.Substring(6, 1)) * 4;
            int num8 = myconvert.toint16(ncode.Substring(7, 1)) * 3;
            int num9 = myconvert.toint16(ncode.Substring(8, 1)) * 2;

            int num_a = myconvert.toint16(ncode.Substring(9, 1));
            int num_b = num1 + num2 + num3 + num4 + num5 + num6 + num7 + num8 + num9;
            int num_c = num_b % 11;

            if ((num_c < 2 && num_a == num_c) || (num_c >= 2 && 11 - num_c == num_a))
                return true;
            else

                return false;
        }
    }
    
    public static class Crypto
    {

        static int inm(int _j, int _x)
        {
            int p = _j % _x;
            //			if(p==0){p = _x;}
            return p;
        }

        public static string incodeoneway(string _s)
        {
            string _k = cheksum(_s);
            string l = incode(_s, _k);
            return l;
        }
        static string incode(string _s, string _k)
        {
            char[] _sc = _s.ToCharArray();
            char[] _kc = _k.ToCharArray();
            string _l = "";
            int _a1 = 0;
            int _a2 = 0;
            int _m = _kc.Length;
            int _n = _sc.Length;
            if (_m > _n)
            {
                _a1 = _n;
                _n = _m;
                _m = _a1;
            }

            for (int i = 0; i < _n; i++)
            {
                _a1 = (int)_sc[inm(i, _sc.Length)];
                _a2 = (int)_kc[inm(i, _kc.Length)];
                _a1 = (Convert.ToInt16((_a1 + _a2) / 2)) & 255;
                _a1 = _a1 | 32;
                _l = _l + (char)_a1;
            }
            return _l;
        }
        static string cheksum(string _s)
        {
            char[] _sc = _s.ToCharArray();
            int _a1 = 0;
            long _a3 = 0;
            int _n = _sc.Length;

            for (int i = 0; i < _n; i++)
            {
                _a1 = (int)_sc[inm(i, _sc.Length)];
                //_a1 = Convert.ToInt16(Convert.ToByte(_sc[inm(i, _sc.Length)]));
                _a3 = _a3 * 2 + _a1;
            }
            return _a3.ToString();
        }


        public static string Crypt(string plainText)
        {
            if (plainText.Trim().Length == 0) return "";

            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }

        public static string Decrypt(string base64EncodedData)
        {
            if (base64EncodedData.Trim().Length == 0) return "";
            try
            {
                var base64EncodedBytes = System.Convert.FromBase64String(base64EncodedData);
                return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
            }
            catch
            { return base64EncodedData; }
        }


        /// <summary>
        /// Generates a 16 byte Unique Identification code of a computer
        /// Example: 4876-8DB5-EE85-69D3-FE52-8CF7-395D-2EA9
        /// </summary>



        public static String GetUniqMachineID()
        {
            string fingerPrint = string.Empty;
            if (string.IsNullOrEmpty(fingerPrint))
            {
                fingerPrint = GetHash("CPU >> " + cpuId() + "\nMac >> " +
            macId() + "\nBASE >> " + baseId()
                                     //+"\nDISK >> "+ diskId() + "\nVIDEO >> " + 
                                     //videoId() +"\nMAC >> "+ macId()
                                     );
            }
            return fingerPrint;
        }
        public static String GetCPUID()
        {
            return cpuId();
        }

        public static String GetBiosID()
        {
            return biosId();
        }


        public static String GetBaseID()
        {
            return baseId();
        }

        public static String GetDiskID()
        {
            return diskId();
        }


        public static String GetMacID()
        {
            return macId();
        }


        public static String GetWMI(string wmiClass, string wmiProperty)
        {
            return identifier(wmiClass, wmiProperty);
        }

        private static string GetHash(string s)
        {
            MD5 sec = new MD5CryptoServiceProvider();
            ASCIIEncoding enc = new ASCIIEncoding();
            byte[] bt = enc.GetBytes(s);
            return GetHexString(sec.ComputeHash(bt));
        }
        private static string GetHexString(byte[] bt)
        {
            string s = string.Empty;
            for (int i = 0; i < bt.Length; i++)
            {
                byte b = bt[i];
                int n, n1, n2;
                n = (int)b;
                n1 = n & 15;
                n2 = (n >> 4) & 15;
                if (n2 > 9)
                    s += ((char)(n2 - 10 + (int)'A')).ToString();
                else
                    s += n2.ToString();
                if (n1 > 9)
                    s += ((char)(n1 - 10 + (int)'A')).ToString();
                else
                    s += n1.ToString();
                if ((i + 1) != bt.Length && (i + 1) % 2 == 0) s += "-";
            }
            return s;
        }
        #region Original Device ID Getting Code
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty, string wmiMustBeTrue)
        {
            string result = "";
            System.Management.ManagementClass mc = new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                if (mo[wmiMustBeTrue].ToString() == "True")
                {
                    //Only get the first one
                    if (result == "")
                    {
                        try
                        {
                            result = mo[wmiProperty].ToString();
                            break;
                        }
                        catch
                        {
                        }
                    }
                }
            }
            return result;
        }
        //Return a hardware identifier
        private static string identifier(string wmiClass, string wmiProperty)
        {
            string result = "";
            System.Management.ManagementClass mc =
        new System.Management.ManagementClass(wmiClass);
            System.Management.ManagementObjectCollection moc = mc.GetInstances();
            foreach (System.Management.ManagementObject mo in moc)
            {
                //Only get the first one
                if (result == "")
                {
                    try
                    {
                        result = mo[wmiProperty].ToString();
                        break;
                    }
                    catch
                    {
                    }
                }
            }
            return result;
        }
        private static string cpuId()
        {
            //Uses first CPU identifier available in order of preference
            //Don't get all identifiers, as it is very time consuming
            string retVal = identifier("Win32_Processor", "UniqueId");
            if (retVal == "") //If no UniqueID, use ProcessorID
            {
                retVal = identifier("Win32_Processor", "ProcessorId");
                if (retVal == "") //If no ProcessorId, use Name
                {
                    retVal = identifier("Win32_Processor", "Name");
                    if (retVal == "") //If no Name, use Manufacturer
                    {
                        retVal = identifier("Win32_Processor", "Manufacturer");
                    }
                    //Add clock speed for extra security
                    retVal += identifier("Win32_Processor", "MaxClockSpeed");
                }
            }
            return retVal;
        }
        //BIOS Identifier
        private static string biosId()
        {
            return identifier("Win32_BIOS", "Manufacturer")
            + identifier("Win32_BIOS", "SMBIOSBIOSVersion")
            + identifier("Win32_BIOS", "IdentificationCode")
            + identifier("Win32_BIOS", "SerialNumber")
            + identifier("Win32_BIOS", "ReleaseDate")
            + identifier("Win32_BIOS", "Version");
        }
        //Main physical hard drive ID
        private static string diskId()
        {
            return identifier("Win32_DiskDrive", "Model")
            + identifier("Win32_DiskDrive", "Manufacturer")
            + identifier("Win32_DiskDrive", "Signature")
            + identifier("Win32_DiskDrive", "TotalHeads");
        }
        //Motherboard ID
        private static string baseId()
        {
            return identifier("Win32_BaseBoard", "Model")
            + identifier("Win32_BaseBoard", "Manufacturer")
            + identifier("Win32_BaseBoard", "Name")
            + identifier("Win32_BaseBoard", "SerialNumber");
        }
        //Primary video controller ID
        private static string videoId()
        {
            return identifier("Win32_VideoController", "DriverVersion")
            + identifier("Win32_VideoController", "Name");
        }
        //First enabled network card ID
        private static string macId()
        {
            return identifier("Win32_NetworkAdapterConfiguration",
    "MACAddress", "IPEnabled");
        }

    }
            #endregion
        






    

    public static class EmailManager
    {

        public static string reciver = "rasmidoc4@gmail.com";
        public static string subject = "Rasmi Doc";
        public static string body = "mail with attachment";
        public static string attachfile = "";
        public static void email_send()
        {

            SendEmail("rasmidoc4@gmail.com", "Rasmidoc4gmail", reciver, subject, body, System.Web.Mail.MailFormat.Text, "");
        }

        public static bool SendEmail(string pGmailEmail, string pGmailPassword, string pTo, string pSubject, string pBody, System.Web.Mail.MailFormat pFormat, string pAttachmentPath)
        {
            try
            {
                System.Web.Mail.MailMessage myMail = new System.Web.Mail.MailMessage();
                myMail.Fields.Add
                    ("http://schemas.microsoft.com/cdo/configuration/smtpserver",
                                  "smtp.gmail.com");
                myMail.Fields.Add
                    ("http://schemas.microsoft.com/cdo/configuration/smtpserverport",
                                  "465");
                myMail.Fields.Add
                    ("http://schemas.microsoft.com/cdo/configuration/sendusing",
                                  "2");
                //sendusing: cdoSendUsingPort, value 2, for sending the message using 
                //the network.

                //smtpauthenticate: Specifies the mechanism used when authenticating 
                //to an SMTP 
                //service over the network. Possible values are:
                //- cdoAnonymous, value 0. Do not authenticate.
                //- cdoBasic, value 1. Use basic clear-text authentication. 
                //When using this option you have to provide the user name and password 
                //through the sendusername and sendpassword fields.
                //- cdoNTLM, value 2. The current process security context is used to 
                // authenticate with the service.
                myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
                //Use 0 for anonymous
                myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/sendusername",
                    pGmailEmail);
                myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/sendpassword",
                     pGmailPassword);
                myMail.Fields.Add
                ("http://schemas.microsoft.com/cdo/configuration/smtpusessl",
                     "true");
                myMail.From = pGmailEmail;
                myMail.To = pTo;
                myMail.Subject = pSubject;
                myMail.BodyFormat = pFormat;
                myMail.Body = pBody;

                if (pAttachmentPath.Trim() != "")
                {
                    System.Web.Mail.MailAttachment MyAttachment = new System.Web.Mail.MailAttachment(pAttachmentPath);
                    myMail.Attachments.Add(MyAttachment);
                    myMail.Priority = System.Web.Mail.MailPriority.High;
                }

                System.Web.Mail.SmtpMail.SmtpServer = "smtp.gmail.com:465";
                System.Web.Mail.SmtpMail.Send(myMail);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }


 





   

    public static class exporttofile
    {
        public static void ExportDatatviewToCsv(string filename, DataView dv, System.Web.HttpResponse response)
        {
            // Open output stream
            //StreamWriter swFile = new StreamWriter(filename);
            StringBuilder swFile = new StringBuilder();

            // Header
            string[] colLbls = new string[dv.Table.Columns.Count];
            for (int i = 0; i < dv.Table.Columns.Count; i++)
            {
                colLbls[i] = dv.Table.Columns[i].ColumnName;
                colLbls[i] = GetWriteableValueForCsv(colLbls[i]);
            }

            // Write labels
            //swFile.WriteLine(string.Join(",", colLbls));
            swFile.AppendLine(string.Join(",", colLbls));
            // Rows of Data
            foreach (DataRowView rowData in dv)
            {
                string[] colData = new string[dv.Table.Columns.Count];
                for (int i = 0; i < dv.Table.Columns.Count; i++)
                {
                    object obj = rowData[i];
                    colData[i] = GetWriteableValueForCsv(obj);
                }

                // Write data in row
                //swFile.WriteLine(string.Join(",", colData));
                swFile.AppendLine(string.Join(",", colData));
            }

            // Close output stream
            // swFile.Close();
            byte[] bytes = Encoding.UTF8.GetBytes(swFile.ToString());
            if (bytes != null)
            {
                response.Clear();
                response.ContentType = "text/csv";
                response.AddHeader("Content-Length", bytes.Length.ToString());
                response.AddHeader("Content-disposition", "attachment; filename=\"" + filename + "" + "\"");
                response.Charset = "windows-1256";
                response.ContentEncoding = Encoding.UTF8;
                response.BinaryWrite(Encoding.UTF8.GetPreamble());
                response.BinaryWrite(bytes);
                response.Flush();
                response.End();
            }
        }
        private static string GetWriteableValueForCsv(object obj)
        {
            // Nullable types to blank
            if (obj == null || obj == Convert.DBNull)
            {
                return "";
            }

            // if string has no ','
            if (obj.ToString().IndexOf(",") == -1)
            {
                return obj.ToString();
            }

            // remove backslahes
            return "\"" + obj.ToString() + "\"";
        }


    }

    public static class importfromfile
    {
        public static  DataTable ReadExcelFile(string sheetName, string path)
        {

            using (OleDbConnection conn = new OleDbConnection())
            {
                DataTable dt = new DataTable();
                string Import_FileName = path;
                string fileExtension = Path.GetExtension(Import_FileName);
                //if (fileExtension == ".xls")
                //    conn.ConnectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 8.0;HDR=YES;'";
                //if (fileExtension == ".xlsx")
                    conn.ConnectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + Import_FileName + ";" + "Extended Properties='Excel 12.0 Xml;HDR=YES;'";
                using (OleDbCommand comm = new OleDbCommand())
                {
                    comm.CommandText = "Select * from [" + sheetName + "$]";
                    comm.Connection = conn;
                    using (OleDbDataAdapter da = new OleDbDataAdapter())
                    {
                        da.SelectCommand = comm;
                        da.Fill(dt);
                        return dt;
                    }
                }
            }
        }
    }
    public class ImpersonationService
    {
        #region Consts

        public const int LOGON32_LOGON_INTERACTIVE = 2;
        public const int LOGON32_PROVIDER_DEFAULT = 0;

        #endregion

        #region External API 
        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern int LogonUser(
            string lpszUsername, string lpszDomain,
            string lpszPassword, int dwLogonType,
            int dwLogonProvider, out IntPtr phToken
            );

        [DllImport("advapi32.dll", SetLastError = true)]
        public static extern bool RevertToSelf();

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int CloseHandle(IntPtr hObject);

        #endregion

        #region Methods 

        public void PerformImpersonatedTask(string username, string domain,
            string password, int logonType, int logonProvider, Action methodToPerform)
        {
            IntPtr token = IntPtr.Zero;
            if (RevertToSelf())
            {
                if (LogonUser(username, domain, password, logonType,
                    logonProvider, out token) != 0)
                {
                    var identity = new WindowsIdentity(token);
                    var impersonationContext = identity.Impersonate();
                    if (impersonationContext != null)
                    {
                        methodToPerform.Invoke();
                        impersonationContext.Undo();
                    }
                }
                else
                {
                    // do logging
                }
            }
            if (token != IntPtr.Zero)
            {
                CloseHandle(token);
            }
        }

        #endregion
    }


   


 




   
    public class User
    {
        public string uc = "";
        public string username = "";
        public string exchange;
        public int sherkat = 0;
        public string pic;
        public bool IsBot;
        public string fname;
        public string email;
        bool isloggedIn = false;
        public string type;
        public string chkpass(string st2, string usertype = "U")
        {
            SQLH sqhand = new SQLH();
            st2 = SQLH.ValidateString(st2).PadRight(10, ' ');
            if (username.Equals("")) return null;
            sqhand.addparm("pesmuser", SQLH.ValidateString(this.username));
            System.Data.DataView users = sqhand.SqlExecute("select * from users where esm=@pesmuser and type='" + usertype + "'", "users");
            if (users.Count > 0 && (Crypto.incodeoneway(st2) == users[0]["cspass"].ToString().Trim() || users[0]["cspass"].ToString().Trim() == ""))
            {
                uc = users[0]["uc"].ToString().Trim();
                email = users[0]["email_id"].ToString().Trim();
                exchange = users[0]["exchange"].ToString().Trim();
                pic = users[0]["pic"].ToString().Trim();
                fname = users[0]["fname"].ToString().Trim();
                sherkat = myconvert.toint16(users[0]["sherkat"].ToString());
                if (users[0]["type"].ToString().Trim().Equals("B")) this.IsBot = true;
                isloggedIn = true;
                type = usertype;

            }
            users.Dispose();
            return this.uc;
        }
        public void load(string uc)
        {

            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select * from users where uc=" + uc, "dv");
            if (dv.Count > 0)
            {
                this.uc = uc;
                username = dv[0]["esm"].ToString().Trim();
                fname = dv[0]["fname"].ToString().Trim();
                pic = dv[0]["pic"].ToString().Trim();
                if (dv[0]["type"].ToString().Trim().Equals("B")) this.IsBot = true;

            }
        }
    }

    public class currentuser : User
    {

        User[] friends;

        public user_notification[] loadnotification()
        {
            SQLH sqhand = new SQLH();
            DataView dv = sqhand.SqlExecute("select * from users_notification where uc=" + this.uc + " and closetime is null", "dv");
            if (dv.Count == 0)
                return Array.Empty<user_notification>();
            int counter = 0;
            user_notification[] respond = new user_notification[dv.Count];
            foreach (DataRowView dr in dv)
            {
                user_notification resp1 = new user_notification(dr["cssclass"].ToString().Trim(),
                                                                dr["icon"].ToString().Trim(),
                                                                dr["contents"].ToString().Trim(),
                                                                dr["time"].ToString().Trim(),
                                                                dr["link"].ToString().Trim(), dr["sender"].ToString().Trim(), myconvert.toint(dr["s_id"].ToString()));
                respond[counter] = resp1;
                ++counter;
            }
            return respond;
        }
        public user_notification[] loadActiveNotification()
        {
            SQLH sqhand = new SQLH();
            DataView dv;
            try
            {
                dv = sqhand.SqlExecute("select * from users_notification where uc=" + this.uc + " and showtime is null", "dv");
            }
            catch
            {
                return Array.Empty<user_notification>();
            }
            if (dv.Count == 0)
                return Array.Empty<user_notification>();

            int counter = 0;
            user_notification[] respond = new user_notification[dv.Count];
            foreach (DataRowView dr in dv)
            {
                user_notification resp1 = new user_notification(dr["cssclass"].ToString().Trim(),
                                                                dr["icon"].ToString().Trim(),
                                                                dr["contents"].ToString().Trim(),
                                                                dr["time"].ToString().Trim(),
                                                                dr["link"].ToString().Trim(),
                                                                dr["sender"].ToString().Trim(), myconvert.toint(dr["s_id"].ToString()));
                respond[counter] = resp1;
                ++counter;
            }

            return respond;
        }
        public user_message[] loadmessages()
        {

            SQLH sqhand = new SQLH();
            DataView dv;
            try
            {
                dv = sqhand.SqlExecute("select * from users_message where sherkat=" + this.sherkat + " and reciver=" + this.uc + " and readtime is null", "dv");
            }
            catch
            {
                return Array.Empty<user_message>();
            }

            int counter = 0;
            user_message[] respond = new user_message[dv.Count];


            foreach (DataRowView dr in dv)
            {
                user_message resp1 = new user_message("avatar-mini2.jpg",
                                                                dr["sender"].ToString().Trim(),
                                                                dr["onvan"].ToString().Trim(),
                                                                dr["sendtime"].ToString().Trim(),
                                                                "/messenger/list_message.aspx?sender=" + dr["sender"].ToString().Trim(),
                                                                dr["sender"].ToString().Trim(), myconvert.toint(dr["s_id"].ToString()));
                respond[counter] = resp1;
                ++counter;
            }

            return respond;
        }
        public user_task[] loadtasks()
        {
            SQLH sqhand = new SQLH();
            DataView dv;
            try
            {
                dv = sqhand.SqlExecute("select * from prj_tasks left join (select taskid, max(progress) as mxprogress from prj_taskaction group by taskid) as drv1 on prj_tasks.s_id = drv1.taskid  where assignedto=" + this.uc + " ", "dv");
            }
            catch
            {
                return Array.Empty<user_task>();
            }
            if (dv.Count == 0)
                return Array.Empty<user_task>();
            int counter = 0;
            user_task[] respond = new user_task[dv.Count];

            foreach (DataRowView dr in dv)
            {
                user_task resp1 = new user_task(dr["title"].ToString().Trim(),
                                                                "progress-bar-warning",
                                                                dr["mxprogress"].ToString().Trim(), "~/mytask/" + dr["s_id"].ToString());
                respond[counter] = resp1;
                ++counter;
            }


            // user_task resp1 = new user_task("Project 1", "progress-bar-warning", "80", "#");
            // user_task resp2 = new user_task("Marketing", "progress-bar-info", "50", "#");
            // user_task resp3 = new user_task("Costing", "progress-bar-danger", "30", "#");
            // user_task resp4 = new user_task("Mobile App", "progress-bar-danger", "10", "#");


            // respond[0] = resp1;
            // respond[1] = resp2;
            // respond[2] = resp3;
            // respond[3] = resp4;

            return respond;
        }
        public string getsr()
        {
            SQLH sqhand = new SQLH();
            string srid = "";
            try
            {
                srid = sqhand.SqlExecute("select sr_id from users_sr where uc=" + this.uc, "dv")[0][0].ToString();
            }
            catch { }
            return srid;
        }
    }
    public class user_task
    {
        public string Name;
        public string Cssclass;
        public string Percent;
        public string Link;
        public user_task(string name, string cssclass, string percent, string link)
        {
            Name = name;
            Cssclass = cssclass;
            Percent = percent;
            Link = link;
        }
    }
    public class user_notification
    {
        public string cssclass;
        public string icon;
        public string content;
        public string time;
        public string link;
        public string sender;
        public long s_id;
        public user_notification(string cssclass, string icon, string content, string time, string link, string sender, long s_id)
        {
            this.cssclass = cssclass;
            this.icon = icon;
            this.content = content;
            this.time = time;
            this.link = link;
            this.sender = sender;
            this.s_id = s_id;
        }
    }


    public class user_message
    {
        public string pic;
        public string name;
        public string content;
        public string time;
        public string link;
        public string sender;
        public long s_id;
        public user_message(string pic, string name, string content, string time, string link, string sender, long s_id)
        {
            this.pic = pic;
            this.name = name;
            this.content = content;
            this.time = time;
            this.link = link;
            this.sender = sender;
            this.s_id = s_id;

        }
    }

    public enum messagetypes { Message, Ack };
    public enum messagecontenttypes { TextMessage, HtmlFormMessage, PhotoMessage };
    public class Message
    {
        public Int64 messageid;
        public Dictionary<string, string> MessageContent;
        public User Sender;
        public User Reciver;
        public bool isSent;
        public bool isRecived;
        public bool isRead;
        public messagecontenttypes messageContentType;
        public messagetypes messageType;
        public Int64? replayto;
    }

    public class Messageserver
    {
        public currentuser user;
        public Message[] read() { return null; }
        public void send(Message message) { }

        public Message[] newmessages() { return null; }

    }

    public static class Icons
    {
        public static string Profile = "icon_profile";
        public static string Pin = "icon_pin";
        public static string Book_alt = "icon_book_alt";
        public static string Like = "icon_like";
    }

    public static class CssClasses
    {
        public static string label_primary = "label-primary";
        public static string label_warning = "label-warningn";
        public static string label_danger = "label-danger";
        public static string label_success = "label-success";
    }

}

namespace ExtensionMethods
{
    public static class StringExtensions
    {
        public static decimal ToDecimal(this String str)
        {
            decimal dec = 0;
            dec = myconvert.todecimal(str);
            return dec;
        }
    }
}


