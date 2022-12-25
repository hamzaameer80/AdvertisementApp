<%@ WebHandler Language="C#" Class="Hfindaddress" %>

using System;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public class Hfindaddress : IHttpHandler {

    String connection = ConfigurationManager.ConnectionStrings["Basic_Data_ConnectionString"].ToString();
    SqlDataReader reader;
    string querry;
    
    public void ProcessRequest(HttpContext context)
    {
        if (context.Request.QueryString["a"].ToString() == "pp")
        {
            querry = "SELECT ProvinceName,ProvinceCode FROM Province WHERE(CountryCode = " + context.Request.QueryString["b"] + ")";
        }
        else if (context.Request.QueryString["a"].ToString() == "dd")
        {
            querry = "SELECT DistrictName,DistrictCode FROM DISTRICT WHERE(ProvinceCode =" + context.Request.QueryString["b"] + ")ORDER BY DistrictName";
        }
        else if (context.Request.QueryString["a"].ToString() == "tt")
        {
            querry = "SELECT TehsilName,TehsilCode FROM TEHSIL WHERE (districtCode = " + context.Request.QueryString["b"] + ")ORDER BY TehsilName";
        }    
        else
        {
            querry = "SELECT CountryName,CountryCode FROM Country ORDER BY CountryName";
        }
        SqlConnection con = new SqlConnection(connection);
        con.Open();
        SqlCommand cmd = new SqlCommand(querry, con);
        reader = cmd.ExecuteReader();

        const string str = @"<?xml version=""1.0"" encoding=""utf-8"" ?>";
        string str1 = "";
        context.Response.ContentType = "application/xml";
        while (reader.Read())
        {
            str1 += "<collection><name>" + reader.GetString(0) + "</name><code>" + reader.GetInt32(1) + "</code></collection>";
            
        }
        con.Close();
        context.Response.Write(str + "<root>" + str1 + "</root>");
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}