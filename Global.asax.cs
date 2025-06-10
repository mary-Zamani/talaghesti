using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using Newtonsoft.Json;
using System.Data;

namespace narsShop
{
    public class Global : System.Web.HttpApplication
    {
        //public static CounterHelper ctr;
        void Application_Start(object sender, EventArgs e)
        {
            //    Application["OnlineUsers"] = 0;
            // Code that runs on application startup
            //RouteTable.Routes.MapHttpRoute(name: "DefaultApi",routeTemplate: "api/{controller}/{id}", defaults: new { id = System.Web.Http.RouteParameter.Optional });

            //RouteTable.Routes.MapHttpRoute("DefaultApi","api/{controller}/{id}");

           // RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  Code that runs on application shutdown

        }

        void Application_Error(object sender, EventArgs e)
        {
            // Code that runs when an unhandled error occurs

        }

        void Session_Start(object sender, EventArgs e)
        {
            // Code that runs when a new session is started
            Session.Timeout = 1200;
            // add some data to the Session so permanent SessionId is assigned
            // otherwise new SessionId is assigned to the user until some data
            // is actually written to Session
            Session["Start"] = DateTime.Now;
            //string pt = System.Web.Hosting.HostingEnvironment.MapPath("~");

            //DataTable dt = new DataTable();
            //dt.ReadXml(pt + "/configs/narsweb.conf");
            //DataView dv1 = dt.DefaultView;
            //string dbname = dv1[0]["maindb"].ToString();
            //string servername = dv1[0]["dbhost"].ToString();
            Session["servername"] = "185.55.224.39";
            Session["dbsname"]="talaghes_jew";
            Session["apiurl"] = "http://neshat.nars.ir:8000";
            Session["siteurl"] = "http://neshat.nars.ir:8080";
            //Session["apiurl"] = "http://localhost:5122";

            //try
            //{
            //    System.Net.WebClient client = new System.Net.WebClient();
            //    string result = client.DownloadString(Session["apiurl"]+"/api/Test");
            //}
            //catch (System.Net.WebException ex)
            //{
            //    //do something here to make the site unusable, e.g:
            //    Session["apiurl"] = "http://178.131.78.129:8000";

            //}


            TalaModelLibrary.token tk = new TalaModelLibrary.token();
            Session["Token"] = tk;

            // get current context
            //   HttpContext currentContext = HttpContext.Current;

            //    if (currentContext != null)
            //     {
            //       if (!OnlineVisitorsUtility.Visitors.ContainsKey(currentContext.Session.SessionID))
            //           OnlineVisitorsUtility.Visitors.Add(currentContext.Session.SessionID, new WebsiteVisitor(currentContext));
            //   }


            //            Application.Lock();
            //            Application["OnlineUsers"] = (int)Application["OnlineUsers"] + 1;
            //            Application.UnLock();
        }

        void Session_End(object sender, EventArgs e)
        {
            // Code that runs when a session ends. 
            // Note: The Session_End event is raised only when the sessionstate mode
            // is set to InProc in the Web.config file. If session mode is set to StateServer 
            // or SQLServer, the event is not raised.

         //   if (this.Session != null)
         //       OnlineVisitorsUtility.Visitors.Remove(this.Session.SessionID);
//            Application.Lock();
//            Application["OnlineUsers"] = (int)Application["OnlineUsers"] - 1;
//            Application.UnLock();

        }

    /*    static void RegisterRoutes(RouteCollection routes)
        {

            string pt = System.Web.Hosting.HostingEnvironment.MapPath("~");
            DataTable dt = new DataTable();
            dt.ReadXml(pt + "/configs/narsweb.conf");
            DataView dv1 = dt.DefaultView;
            string maindb = dv1[0]["maindb"].ToString();
            string dbhost = dv1[0]["dbhost"].ToString();
            string ctype = "ervername";
            try { ctype= dv1[0]["ctype"].ToString(); } catch { }

            SQLH sqlhand = new SQLH(dbhost, maindb);
            DataView dv = sqlhand.SqlExecute("SELECT tenantid,dbsname," + ctype + " as servername FROM tenants where groupid=1", "dv");

            string servername = "";
            if (ctype.Equals("Remote"))
             servername = dv[0]["remoteserver"].ToString();
            else
                servername = dv[0]["servername"].ToString();
            string dbname = dv[0]["dbsname"].ToString();

            SQLH sqhand = new SQLH(servername, dbname);


            DataView dvb = sqhand.SqlExecute("SELECT * FROM shname where sherkat>=0 and shname>' '", "dv");
            foreach (DataRowView dr in dvb)
            routes.MapPageRoute("sherkat"+dr["sherkat"].ToString(), dr["shname"].ToString().Trim().Replace(' ','_'), "~/gp/memberdetail.aspx?sherkatid=" + dr["sherkat"].ToString());
            //Route Name   : myfriend1   
            //Route URL    : csharpcorner-friend-list  
            //Physical File: FriendList.aspx  
        }*/
    }
}
