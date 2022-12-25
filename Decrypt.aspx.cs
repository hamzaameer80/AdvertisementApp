using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class Decrypt : System.Web.UI.Page
{
    Encryption ENC = new Encryption();
    protected void Page_Load(object sender, EventArgs e)
    {
        //DbManager objDbManager = new DbManager();

        
        //SqlParameter[] sqlParams = { 
                                      
        //                           };

        SqlDataAdapter adt = new SqlDataAdapter();
        DataTable dt = new DataTable();
        String constring = ConfigurationManager.ConnectionStrings["Reg_ConnectionString"].ConnectionString;
        SqlConnection con = new System.Data.SqlClient.SqlConnection(constring);
        System.Data.SqlClient.SqlCommand mycommand = new System.Data.SqlClient.SqlCommand("SELECT [RegNo],PFName,PLName FROM [Patient]", con);
        mycommand.CommandType = CommandType.Text;
        adt.SelectCommand = mycommand;
            mycommand.Connection.Open();

            adt.Fill(dt);


     //  DataTable dt = objDbManager.ExecuteDataTable("selectpatiens", "Reg_Man", sqlParams);

        for(int i=0; i<dt.Rows.Count;i++)
        {
               string regNo = dt.Rows[i]["RegNo"].ToString();
            string PFName = dt.Rows[i]["PFName"].ToString();
            string PLName = dt.Rows[i]["PLName"].ToString();
            

            System.Data.SqlClient.SqlCommand mycommand1 = new System.Data.SqlClient.SqlCommand("insert into patientTemp(regno,regnonew,fname,lname) values('" + regNo + "','" + ENC.Encrypt_Main(regNo, false) + "','" + ENC.Encrypt_Main(PFName, false) + "','" + ENC.Encrypt_Main(PLName, false) + "')", con);
             mycommand1.CommandType = CommandType.Text;
             mycommand1.Parameters.AddWithValue("@regno", regNo);
             mycommand1.Parameters.AddWithValue("@regnonew", ENC.Encrypt_Main(regNo, false));
             mycommand1.Parameters.AddWithValue("@fname", ENC.Encrypt_Main(regNo, false));
             mycommand1.Parameters.AddWithValue("@lname", ENC.Encrypt_Main(regNo, false));


             mycommand1.ExecuteNonQuery();
              //SqlParameter[] sqlParams1 = { 
              //                         new SqlParameter("@regno",regNo) ,
              //                          new SqlParameter("@regnonew",ENC.Encrypt_Main(regNo, false)) ,
              //                           new SqlParameter("@fname",ENC.Encrypt_Main(PFName, false)) ,
              //                            new SqlParameter("@lname",ENC.Encrypt_Main(PLName, false)) 
              //                     };


     // objDbManager.ExecuteNonQuery("addpatiens", "Reg_Man", sqlParams1);
        }


    }


    protected void grd_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            DataRowView drview = e.Row.DataItem as DataRowView;       

            Label lblNewReg = (Label)e.Row.FindControl("lblNewReg");
            Label lblPFName = (Label)e.Row.FindControl("lblPFName");
            Label lblPLName = (Label)e.Row.FindControl("lblPLName");

            string regNo = drview["RegNo"].ToString();
            string PFName = drview["PFName"].ToString();
            string PLName = drview["PLName"].ToString();

            lblNewReg.Text = ENC.Encrypt_Main(regNo, false);
            lblPFName.Text = ENC.Encrypt_Main(PFName, false);
            lblPLName.Text = ENC.Encrypt_Main(PLName, false);

        }
    }
   
}