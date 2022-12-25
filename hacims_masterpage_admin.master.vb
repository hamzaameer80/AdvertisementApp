Imports System.Data
Imports System.Data.SqlClient

Partial Class hacims_masterpage_admin
    Inherits System.Web.UI.MasterPage

    Dim user_authen As New User_page_Authentication
    Dim constr As String = ConfigurationManager.ConnectionStrings("UsersConnectionString").ConnectionString
    Dim ENC As New Encryption


   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Page.Header.DataBind()

        If Request.UserAgent.IndexOf("WebKit") > 0 Then
            Request.Browser.Adapters.Clear()
        End If


        Dim Obj_Menu As New JavaScriptMenu
        Obj_Menu.loginvalidate()

        Session.Add("Page_Name", Request.QueryString("Page_Name"))
        Session.Add("Page", Request.QueryString("Page"))
        Employee_Info()


        If Not Page.IsPostBack Then

            If Session("SubDeptId") = 10 Or Session("SubDeptId") = 39 Then
                ExpItems.Visible = True
            End If

            LogUserPageAccess()
            hlHome.NavigateUrl = Session("MainPage")
        End If

        '''''''''''''''''''
        '''''''''''''''

        'If Session("HospitalID").ToString() = "10" Then

        '  hospital.Text  = 10
        '  ElseIf Session("HospitalID").ToString() = "4" Then
        '           hospital.Text = 11
        ' ElseIf Session("HospitalID").ToString() = "12" Then
        '  hospital.Text = 12
        '       End If
        'response.write(Session("HospitalID").ToString())

        Dim dt As DataTable = Session("Header_DT")
        'hospital.Text = dt.Rows(0)("Hospital_Name").ToString() + "<br>" + dt.Rows(0)("Hospital_Address").ToString() + "<br>" + dt.Rows(0)("TehsilName").ToString() + "<br>" + dt.Rows(0)("Hospital_Phone").ToString() + "    " + dt.Rows(0)("Hospital_FaxNo").ToString()
        'hospital.Text = dt.Rows(0)("Hospital_Name").ToString() + "<br>" + dt.Rows(0)("TehsilName").ToString()
        'Image1.ImageUrl = "~/ShowHeaderImage.ashx?RegNO=" + "1"

    End Sub

    Private Sub LogUserPageAccess()
        Try
            'Dim temp As New HttpSessionState
   
            GetMenuData()
            GetNewMenuData()
            GetListMenuData()

        Catch ex As Exception

        End Try
    End Sub

    Private Function GetSessionToInt(ByVal sessionKey As String) As Integer
        Dim retVal As Integer
        Try
            retVal = Integer.Parse(Session(sessionKey).ToString())
        Catch ex As Exception
            retVal = 0
        End Try
        Return retVal
    End Function


    Private Function GetSessionToLong(ByVal sessionKey As String) As Long
        Dim retVal As Long
        Try
            retVal = Long.Parse(Session(sessionKey).ToString())
        Catch ex As Exception
            retVal = 0
        End Try
        Return retVal
    End Function

    Private Sub GenerateDemoLink()
        Try
            If Session("UserName").ToString().ToUpper().Contains("DEMO") Then

                Dim stringBui As StringBuilder = New System.Text.StringBuilder()

                Using hyperLink = New System.Web.UI.WebControls.HyperLink()
                    hyperLink.Text = "Demo"
                    hyperLink.NavigateUrl = "~/DemoHome.aspx"
                    hyperLink.ToolTip = "Demo"
                    hyperLink.CssClass = "cpass"
                    Using stringWriter = New System.IO.StringWriter(stringBui)
                        Using htmlTextWriter = New System.Web.UI.HtmlTextWriter(stringWriter)
                            hyperLink.RenderControl(htmlTextWriter)
                        End Using
                    End Using

                End Using

                ltrDemo.Text = "<span class=""spltr"">|&nbsp;</span>" + stringBui.ToString()

            Else
                ltrDemo.Text = String.Empty
            End If
        Catch ex As Exception

        End Try
    End Sub

    Sub Employee_Info()
        If Session("UserName") = "admin" Then
            Label_SubDepartment.Text = "Admin"
        Else
            Label_SubDepartment.Text = "Customer"
        End If
        If Session("UserName") = "admin" Then
            Label_Designation.Text = "Administrator"
        Else
            Label_Designation.Text = "User"
        End If
            If String.IsNullOrEmpty(Session("Emp_Log_time")) Then
                LabelDate.Text = FormatDateTime(Date.Now, DateFormat.LongDate) + " " + FormatDateTime(Date.Now, DateFormat.LongTime)
            Else
                LabelDate.Text = FormatDateTime(CDate(Session("Emp_Log_time")), DateFormat.LongDate) + " " + FormatDateTime(CDate(Session("Emp_Log_time")), DateFormat.LongTime)

            End If
    End Sub

    Sub loginvalidate()
        Try
            If ((Session("emp_id") Is Nothing) And (Session("dept_id") Is Nothing)) Then
                Response.Write("~/login.aspx")
            End If
        Catch ex As Exception
        End Try
    End Sub






    Protected Sub Page_PreRender(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreRender
        PopulateMenu()
    End Sub
    Sub PopulateMenu()
        Dim ds As DataSet = GetDataSetForMenu()
        Dim menu As New Menu
        Try


            For Each parentItem As DataRow In ds.Tables("Categories").Rows
                Dim categoryItem As MenuItem = New MenuItem()
                categoryItem.Text = parentItem("Module_Name")
                Dim Str As Int32 = parentItem("Module_ID")
                Dim ds_sub As DataSet = GetDataSetForSubMenu(Str)

                menu.Items.Add(categoryItem)



                Try


                    For Each childItem As DataRow In ds_sub.Tables("child").Rows

                        Dim childrenItem As New MenuItem

                        childrenItem.Text = childItem("Page_Name")
                        childrenItem.NavigateUrl = childItem("Page_URL")
                        categoryItem.ChildItems.Add(childrenItem)

                    Next
                Catch ex As Exception
                    Response.Write(categoryItem.Text)
                End Try
            Next
            menu.Orientation = Orientation.Horizontal

            menu.CssClass = "DynamicMenuStyle"
            menu.DynamicMenuItemStyle.CssClass = "DynamicMenuItemStyle"


            'Panel1.Controls.Add(menu)
            'Panel1.DataBind()
        Catch ex As Exception
            Response.Write("Error main Main Load")
        End Try
    End Sub

    Function GetDataSetForMenu() As DataSet
        Dim myConnection As New SqlConnection(constr)
        'Dim adCat As New SqlDataAdapter("SELECT DISTINCT Admin_User_Module.Module_ID, Admin_User_Module.Module_Name, Admin_User_Module.Priority AS val FROM         Admin_User_Module RIGHT OUTER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " ) AND (Admin_User_Module.For_Main_Page = 0) order by Admin_User_Module.Priority", myConnection)
        'Dim adCat As New SqlDataAdapter("SELECT DISTINCT Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority as val FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =21 ) AND  (Admin_User_Module.type = 1 or Admin_User_Module.Module_Type = 1) order by Admin_User_Module.Priority desc", myConnection)

        Dim ds As New DataSet
        Dim menu As JavaScriptMenu = New JavaScriptMenu()
        ds = menu.GetDataSetForTopMenu(Session("emp_id"))
        Return ds
    End Function

    Function GetDataSetForSubMenu(ByVal str As Int32) As DataSet
        Dim myConnection As New SqlConnection(constr)
        Dim adCat As New SqlDataAdapter("SELECT DISTINCT Admin_User_Module_Pages.Page_Name, Admin_User_Module_Pages.Page_URL,Admin_User_Module_Pages.Priority,Admin_Employee_Module_Pages.Page_ID FROM Admin_Employee_Module_Pages INNER JOIN Admin_User_Module_Pages ON Admin_Employee_Module_Pages.Page_ID = Admin_User_Module_Pages.Page_ID WHERE  (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & ") AND (Admin_User_Module_Pages.Module_ID =" & str & ") ORDER BY Admin_User_Module_Pages.Priority", myConnection)
        Dim ds As New DataSet
        adCat.Fill(ds, "child")
        Return ds
    End Function

    Function GetMenuData()
        Dim table As New DataTable()
        Dim conn As New SqlConnection(constr)
        Dim sql As String = "usp_MainMenu"

        Dim cmd As New SqlCommand(sql, conn)
        cmd.Parameters.AddWithValue("@EmpId", Session("emp_id"))
        cmd.CommandType = CommandType.StoredProcedure

        Dim da As New SqlDataAdapter(cmd)
        da.Fill(table)
        Dim view As New DataView(table)
        'view.RowFilter = "Module_ID is NULL"
        For Each row As DataRowView In view
            Dim menuItem As New MenuItem(row("Module_Name").ToString(), row("Module_ID").ToString())
            'menuItem.NavigateUrl = row("menu_url").ToString()
            menuBarr.Items.Add(menuItem)
            AddChildItems(table, menuItem)

        Next
        'Dim table As New DataTable()
        'Dim conn As New SqlConnection(constr)
        'Dim sql As String = "SELECT DISTINCT top(10) Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " ) AND  (Admin_User_Module.type = 1 or Admin_User_Module.For_Main_Page = 1) order by Admin_User_Module.Priority asc"

        'Dim cmd As New SqlCommand(sql, conn)

        'Dim da As New SqlDataAdapter(cmd)
        'da.Fill(table)
        'Dim view As New DataView(table)
        ''view.RowFilter = "Module_ID is NULL"
        'For Each row As DataRowView In view
        '    Dim menuItem As New MenuItem(row("Module_Name").ToString(), row("Module_ID").ToString())
        '    'menuItem.NavigateUrl = row("menu_url").ToString()
        '    menuBarr.Items.Add(menuItem)
        '    AddChildItems(table, menuItem)

        'Next


    End Function
    Function GetNewMenuData()
        Dim table2 As New DataTable()
        Dim conn2 As New SqlConnection(constr)
        Dim sql2 As String = "usp_SecondMenu"

        'Dim sql2 As String = "SELECT DISTINCT top(10) Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority as val FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " )   AND  (Admin_User_Module.Module_Type = 0  or (Admin_User_Module.For_Main_Page = 1 and Admin_Employee_Module_Pages.Emp_ID =8))    order by Admin_User_Module.Priority desc"
        Dim cmd2 As New SqlCommand(sql2, conn2)
        cmd2.Parameters.AddWithValue("@EmpId", Session("emp_id"))
        cmd2.CommandType = CommandType.StoredProcedure
        Dim da2 As New SqlDataAdapter(cmd2)
        da2.Fill(table2)
        Dim view As New DataView(table2)
        'view.RowFilter = "menu_parent_id is NULL"
        For Each row As DataRowView In view
            Dim menuItem As New MenuItem(row("Module_Name").ToString(), row("Module_ID").ToString())
            'menuItem.NavigateUrl = row("menu_url").ToString()
            menuBarr2.Items.Add(menuItem)

            AddChildItems(table2, menuItem)
        Next
        'Dim table2 As New DataTable()
        'Dim conn2 As New SqlConnection(constr)
        'Dim sql2 As String = "SELECT DISTINCT Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority  FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " ) AND  (Admin_User_Module.type = 1 or Admin_User_Module.For_Main_Page = 1) and Admin_User_Module.Module_ID not in ( SELECT DISTINCT top(10) Admin_User_Module.Module_ID FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " ) AND  (Admin_User_Module.type = 1 or Admin_User_Module.For_Main_Page = 1) order by Admin_User_Module.Priority asc)"

        ''Dim sql2 As String = "SELECT DISTINCT top(10) Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority as val FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " )   AND  (Admin_User_Module.Module_Type = 0  or (Admin_User_Module.For_Main_Page = 1 and Admin_Employee_Module_Pages.Emp_ID =8))    order by Admin_User_Module.Priority desc"
        'Dim cmd2 As New SqlCommand(sql2, conn2)
        'Dim da2 As New SqlDataAdapter(cmd2)
        'da2.Fill(table2)
        'Dim view As New DataView(table2)
        ''view.RowFilter = "menu_parent_id is NULL"
        'For Each row As DataRowView In view
        '    Dim menuItem As New MenuItem(row("Module_Name").ToString(), row("Module_ID").ToString())
        '    'menuItem.NavigateUrl = row("menu_url").ToString()
        '    menuBarr2.Items.Add(menuItem)

        '    AddChildItems(table2, menuItem)
        'Next

    End Function

    Function GetListMenuData()

        Dim table3 As New DataTable()
        Dim conn3 As New SqlConnection(constr)
        Dim sql3 As String = "SELECT DISTINCT Admin_User_Module.Module_ID, Admin_User_Module.Module_Name,Admin_User_Module.Priority  FROM Admin_User_Module INNER JOIN Admin_Employee_Module_Pages ON Admin_User_Module.Module_ID = Admin_Employee_Module_Pages.Module_ID WHERE (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & " )   AND  (Admin_User_Module.Module_Type = 0  or (Admin_User_Module.For_Main_Page = 1 and Admin_Employee_Module_Pages.Emp_ID =8))    order by Admin_User_Module.Priority asc"
        Dim cmd3 As New SqlCommand(sql3, conn3)
        Dim da3 As New SqlDataAdapter(cmd3)
        da3.Fill(table3)
        Dim view As New DataView(table3)
        'view.RowFilter = "menu_parent_id is NULL"
        For Each row As DataRowView In view
            Dim menuItem As New MenuItem(row("Module_Name").ToString(), row("Module_ID").ToString())
            'menuItem.NavigateUrl = row("menu_url").ToString()
            menuBarr3.Items.Add(menuItem)

            AddChildItems(table3, menuItem)
        Next

    End Function

    Function AddChildItems(ByVal table As DataTable, ByVal menuItem As MenuItem)

        Dim str As String = menuItem.Value
        Dim myConnection As New SqlConnection(constr)
        Dim adCat As New SqlDataAdapter("SELECT DISTINCT Admin_User_Module_Pages.Page_Name, Admin_User_Module_Pages.Page_URL,Admin_User_Module_Pages.Priority,Admin_Employee_Module_Pages.Page_ID FROM Admin_Employee_Module_Pages INNER JOIN Admin_User_Module_Pages ON Admin_Employee_Module_Pages.Page_ID = Admin_User_Module_Pages.Page_ID WHERE  (Admin_Employee_Module_Pages.Emp_ID =" & Session("emp_id") & ") AND (Admin_User_Module_Pages.Module_ID =" & str & ") ORDER BY Admin_User_Module_Pages.Priority", myConnection)
        Dim dt As DataTable = New DataTable()
        adCat.Fill(dt)
        Dim viewItem As New DataView(dt)
        For Each childView As DataRowView In viewItem
            Dim childItem As New MenuItem



            childItem.Text = childView("Page_Name").ToString()

            If childItem.Text = "Followup Visit" Then

                childItem.NavigateUrl = childView("Page_URL").ToString() + "?FollowUp=Followup"
                menuItem.ChildItems.Add(childItem)
            ElseIf childItem.Text = "Patient Registration" Then

                childItem.NavigateUrl = childView("Page_URL").ToString() + "?FollowUp=Registration"
                menuItem.ChildItems.Add(childItem)
            Else
                childItem.NavigateUrl = childView("Page_URL").ToString()
                menuItem.ChildItems.Add(childItem)
            End If

            menuBarr.DataBind()
            'Panel1.DataBind()
            'AddChildItems(table, childItem)

        Next
    End Function


    Function Decrypt_String(ByVal Txt As String, ByVal Key As Integer, ByVal Default_Space As Boolean) As String
        Dim First_Space As Boolean = Default_Space
        Dim st As String = ""
        For i As Integer = 0 To Txt.Length - 1
            If Txt(i) = " " Then
                st = st & Convert.ToChar(Asc(Txt(i))).ToString
                First_Space = True
            ElseIf First_Space = False Then
                st = st & Convert.ToChar(Asc(Txt(i))).ToString
            Else
                st = st & Convert.ToChar(Asc(Txt(i)) - Key).ToString
            End If
        Next
        Return st
    End Function
 


End Class

