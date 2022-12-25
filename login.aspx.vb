Imports System.Data
Imports System.Data.SqlClient
Partial Class login
    Inherits System.Web.UI.Page
    Dim departmentid As Integer
    Dim value, value1 As String
    Dim user_authen As New User_page_Authentication
    Dim ENC As New Encryption
    Protected Sub btnLogin_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Try
            If Not Page.IsValid Then
                Return
            End If
            Dim xml As New GenerateXML
            Dim constring As String = ConfigurationManager.ConnectionStrings("Users_ConnectionString").ConnectionString
            Dim con As SqlConnection = New System.Data.SqlClient.SqlConnection(constring)
            Dim empid As Integer
            Dim mainpage As String
            If con.State = ConnectionState.Closed Then
                con.Open()
            End If
            'Enter procedure name
            Dim Procedurename As String = "UserLogin"
            Dim mycommand As New System.Data.SqlClient.SqlCommand(Procedurename, con)
            mycommand.CommandType = Data.CommandType.StoredProcedure
            mycommand.CommandText = Procedurename
            mycommand.Parameters.AddWithValue("@Username", txtusername.Text)
            mycommand.Parameters.AddWithValue("@Password", ENC.Encrypt_Main(txtPassword.Text, True))
            Dim sdr As SqlDataReader = mycommand.ExecuteReader()
            If sdr.HasRows Then
                sdr.Read()
                Session("UserName") = txtusername.Text.Trim()
                Session("HospitalID") = 1
                Session("AttachHospitalID") = 1
                empid = sdr.Item("EmpID")
                Session.Add("emp_id", empid)
                Session.Add("dept_id", 4)
                Session.Add("MainPage", "~/dashboards/DB_Reports.aspx")
                mainpage = "~/dashboards/DB_Reports.aspx"
                'Response.Write("Mainpage.aspx")
                Session.Add("SubDeptID", 4)
                Session.Add("ShiftID", 1)
                Session.Add("dept_Introduction", "Remarks")
                Session.Add("DesignationID", 1)

                Session.Add("Emp_Type", 1)
                Session.Add("UserName", txtusername.Text)
                If Session("UserName") = "admin" Then
                    Session.Add("deptName", "Administrator")
                    Session.Add("SubDeptName", "Administrator")
                Else
                    Session.Add("deptName", "Customer")
                    Session.Add("SubDeptName", "Customer")
                End If
                Session.Add("Branch_Access", 1)
                Session.Add("Company_Branch_Id", 1)
                Session.Add("Financial_Company_Id", 1)
                Session.Timeout = 60
                Session.Add("Emp_Log_time", DateAndTime.Now)
                getHeader()
                If (mainpage Is Nothing) Then
                    lblMessage.Visible = True
                    lblMessage.Text = "Invalid Login Information"
                Else
                    Session("Module_ID") = 485 'user_authen.Tabs_selection1(mainpage, empid)
                    Response.Redirect(mainpage)
                End If
                'EnterUserLoginLog(True)
                sdr.Close()
            Else
                'EnterUserLoginLog(False)
                'SetInvalidLoginAttempCount()
                lblMessage.Visible = True
                lblMessage.Text = "Enter Correct UserName &amp; Password"
                Return
            End If
            con.Close()
        Catch ex As Exception
            lblMessage.Visible = True
            Response.Write(ex.Message)
            lblMessage.Text = ex.Message '"Enter Correct UserName &amp; Password" '& ex.Message
            'Label_message.ForeColor = Drawing.Color.Red : Label_message.Text = "Sorry You Have No Rights To Access, Please Contact To Your Database Administrator"
            'Response.Write(ex.Message)
        End Try
    End Sub

    Private Sub SendUserBlockEmail(ByVal userName As String)
        Dim From As String = ""
        Dim ToEmail As String = "" '""

        Try

            Dim mail As New System.Net.Mail.MailMessage()

            Dim toEmailAddresses As String() = ToEmail.Split(New Char() {";"c}, StringSplitOptions.RemoveEmptyEntries)

            For Each t As String In toEmailAddresses
                mail.[To].Add(t)
            Next

            'mail.To.Add(ToEmail);

            'mail.CC.Add(GetAppSetting("FinanceEmail"))
            mail.From = New Net.Mail.MailAddress(From, "User Account Blocked", System.Text.Encoding.UTF8)
            mail.Subject = "User Account Blocked"
            mail.SubjectEncoding = System.Text.Encoding.UTF8
            mail.Body = "Dear Admin, <br /><br /><br />" & String.Format("User Account for User <b>{0}</b> is blocked due to invalid attempts. <br /><br /> Best Regards, <br /> Technical Support Team.", userName)

            mail.BodyEncoding = System.Text.Encoding.UTF8
            mail.IsBodyHtml = True
            'mail.Priority = MailPriority.High

            Dim client As New Net.Mail.SmtpClient()

            client.Credentials = New System.Net.NetworkCredential(From, "dellsoft")
            client.Port = 587
            client.Host = "mail.tabsole.com.pk"
            client.EnableSsl = False
            Try
                client.Send(mail)
                'WriteEmailLog(From, ToEmail)
            Catch ex As Exception
                If ex.InnerException Is Nothing Then
                    'WriteErrorLog(ex.Message + "::" + ex.StackTrace)
                Else
                    'WriteErrorLog((ex.Message + "::" + ex.StackTrace & "::") + ex.InnerException.Message)
                End If

            End Try
        Catch ex As Exception
            If ex.InnerException Is Nothing Then
                'WriteErrorLog(ex.Message + "::" + ex.StackTrace)
            Else
                'WriteErrorLog((ex.Message + "::" + ex.StackTrace & "::") + ex.InnerException.Message)
            End If
        End Try

    End Sub


    Private Sub EnterUserLoginLog(ByVal success As Boolean)
        Try

            Dim userData As New UserAccessLog.UsageLogDTO
            Dim constring As String = ConfigurationManager.ConnectionStrings("Users_ConnectionString").ConnectionString
            If success Then
                userData.RH_ID = 1
            Else
                userData.RH_ID = 0
            End If
            userData.Useage_DateTime = DateTime.Now
            userData.UserIP = HttpContext.Current.Request.UserHostAddress
            userData.UserName = txtusername.Text.Trim()
            Dim userDataManager As New UserAccessLog.UserAccessLogDBManager(constring)
            userDataManager.LogUserAccessLog(userData)

        Catch ex As Exception

        End Try
    End Sub

    Private Sub SetAccountInActive(ByVal userName As String)
      
    End Sub
    Private Sub SetInvalidLoginAttempCount()
        Try
            If Session("INVALID_LOGIN_ATTEMP") Is Nothing Then
                Session("INVALID_LOGIN_ATTEMP") = 1
            Else
                Dim invalidLoginCount As Integer = 0
                invalidLoginCount = Integer.Parse(Session("INVALID_LOGIN_ATTEMP").ToString())
                invalidLoginCount += 1
                Session("INVALID_LOGIN_ATTEMP") = invalidLoginCount
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Function GetInvalidLoginAttempCount() As Integer
        Try
            Return Integer.Parse(Session("INVALID_LOGIN_ATTEMP").ToString())
        Catch ex As Exception
            Return 0
        End Try
    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LabelDate.Text = FormatDateTime(Date.Now, DateFormat.LongDate) + " " + FormatDateTime(Date.Now, DateFormat.LongTime)
        lblMessage.Visible = False

        If Not Page.IsPostBack Then
            Session.RemoveAll()

        End If

        txtusername.Focus()

        btnLogin.Attributes.Add("OnClick", "return checkValue(this)")
    End Sub
    Private Sub getHeader()

        Try
            Dim dbMgr As DbManager = New DbManager()
            Dim para As SqlParameter() = {New SqlParameter("@HospitalID", Session("HospitalId"))}
            Dim dt As DataTable = dbMgr.ExecuteDataTable("usp_ReportHeaderData_forSession", "Basic_Data_ConnectionString", para)
            Session("DynamicHeader") = dt
        Catch generatedExceptionName As Exception
        Finally

        End Try
    End Sub
 
End Class
