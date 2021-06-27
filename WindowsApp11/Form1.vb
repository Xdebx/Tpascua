
Imports System.Data.OleDb
Public Class Form1
    Dim con As New OleDbConnection
    Dim da As OleDbDataAdapter
    Dim sql As String
    Dim ds As New DataSet

    'Dim con As New OleDb.OleDbConnection

    Dim dbcon As String = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Downloads\Supply_Account_Ns.accdb"

    Private Sub cmdRun_Click(sender As Object, e As EventArgs) Handles cmdRun.Click

        'Dim dbcon As String

        'dbcon = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Downloads\Supply_Account_Ns.accdb"

        '//connect to Database
        con.ConnectionString = dbcon

        con.Open()
        MsgBox("Open Database")

        con.Close()
        MsgBox("Close Database")


    End Sub

    Private Sub cmdAdapter_Click(sender As Object, e As EventArgs) Handles cmdAdapter.Click
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = "Select * From supplier ORDER BY supNum " 'ASC"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier")

        Dgrid.DataSource = ds.Tables("supplier")

        For Each row As DataGridViewRow In Dgrid.Rows
            If Not row.IsNewRow Then

                row.Cells(0).Value.ToString()
                row.Cells(1).Value.ToString()
                row.Cells(2).Value.ToString()
                row.Cells(3).Value.ToString()
                row.Cells(4).Value.ToString()


                'txtsupNum.Text = row.Cells(0).Value.ToString
                'txtComName.Text = row.Cells(1).Value.ToString
                'txtAddr.Text = row.Cells(2).Value.ToString
                'txtConPer.Text = row.Cells(3).Value.ToString
                'txtsupPhone.Text = row.Cells(4).Value.ToString
            End If

        Next
        MsgBox("Do you want to continue?")
        con.Close()

    End Sub

    Private Sub cmdShow_Click(sender As Object, e As EventArgs) Handles cmdShow.Click
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = "Select * From supplier ORDER BY supNum " 'ASC"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier")


        'txtsupNum.Text = ds.Tables("supplier").Rows(0).Item(0)
        'txtComName.Text = ds.Tables("supplier").Rows(0).Item(1)
        'txtAddr.Text = ds.Tables("supplier").Rows(0).Item(2)
        'txtConPer.Text = ds.Tables("supplier").Rows(0).Item(3)
        'txtsupPhone.Text = ds.Tables("supplier").Rows(0).Item(4)


        For i = 0 To ds.Tables("supplier").Rows.Count - 1
            txtsupNum.Text = ds.Tables("supplier").Rows(i).Item(0)
            txtComName.Text = ds.Tables("supplier").Rows(i).Item(1)
            txtAddr.Text = ds.Tables("supplier").Rows(i).Item(2)
            txtConPer.Text = ds.Tables("supplier").Rows(i).Item(3)
            txtsupPhone.Text = ds.Tables("supplier").Rows(i).Item(4)


            MsgBox("Supplier Number: " + txtsupNum.Text + vbNewLine + "Company Name: " + txtComName.Text + vbNewLine + "Comapany Address: " + txtAddr.Text + vbNewLine + "Company Contact Person: " + txtConPer.Text + vbNewLine + "Supplier Phone: " + txtsupPhone.Text)
        Next i

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account"))

        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        con.Close()

    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click


        Dim i As Integer = Dgrid.CurrentRow.Index
        With Dgrid
            txtsupNum.Text = .Item(0, i).Value
            txtComName.Text = .Item(1, i).Value
            txtAddr.Text = .Item(2, i).Value
            txtConPer.Text = .Item(3, i).Value
            txtsupPhone.Text = .Item(4, i).Value
        End With

        MsgBox("Supplier Number: " + txtsupNum.Text + vbNewLine + "Company Name: " + txtComName.Text + vbNewLine + "Comapany Address: " + txtAddr.Text + vbNewLine + "Company Contact Person: " + txtConPer.Text + vbNewLine + "Supplier Phone: " + txtsupPhone.Text)
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        '// Display on the datagrid all supplier with accounts
        ds.Clear()

        con.ConnectionString = dbcon

        con.Open()

        sql = "SELECT supplier.supNum,supComName,supAddr, supConPer,supPhone,account.acctNum,acctDI,acctDue,acctTotal FROM supplier,account WHERE supplier.supNum = account.supNum"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account")

        Dgrid.DataSource = ds.Tables("supplier_account")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account"))

        'MsgBox("Total Columns: " + Dgrid.ColumnCount)

        'MsgBox("Result: " & da.Fill(ds, "supplier" & vbNewLine & da.Fill(ds, "account")))

        con.Close()
        'MessageBox.Show("Result: " & txtAddr.Text)

        'ds.Clear()
        'MsgBox("Result: " & da.Fill(ds, "Supplier_account"))

    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        '//NOT FUNCTION
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = "SELECT supplier.supNum, supComName, supAddr, supConPer, supPhone FROM supplier WHERE supplier.supNum NOT IN (Select supNum FROM account)"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier")

        Dgrid.DataSource = ds.Tables("supplier")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier"))

        con.Close()
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        '// DUE DATE (AND FUNCTION)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = "SELECT supplier.supNum, 
                      supComName, 
                      supAddr, 
                      supConPer, 
                      supPhone, 
                      acctNum, 
                      acctDI, 
                      acctDue, 
                      acctTotal  

               FROM supplier, 
                    account 

               WHERE supplier.supNum = account.supNum AND acctDue >= #06/12/2021# "

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account")

        Dgrid.DataSource = ds.Tables("supplier_account")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account"))

        con.Close()
    End Sub

    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        '// DATE INCCURED 1 (AND FUNCTION)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum, supComName, supAddr, supConPer,supPhone, acctNum, acctDI, acctDue, acctTotal 
               FROM supplier, account 
               WHERE supplier.supNum = account.supNum AND acctDI = #06/10/21# AND acctTotal < 7000 "

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account")

        Dgrid.DataSource = ds.Tables("supplier_account")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Row: " & da.Fill(ds, "supplier_account"))

        con.Close()
    End Sub

    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        '// DATE INCCURED 2 (AND FUNCTION)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum,supComName, supAddr, supConPer, supPhone, acctNum, acctDI, acctDue, acctTotal 
                FROM supplier, account 
                WHERE supplier.supNum = account.supNum AND acctTotal <= 9000 AND acctDI = #06/10/2021#"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account")

        Dgrid.DataSource = ds.Tables("supplier_account")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account"))

        con.Close()
    End Sub

    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        '// 4 tables 
        '// SELECT TableName.AttributeName or TableName.AttributeName(Key Attribute or Primary Key) yung ibang attributes optional na lang yon  pero depende sa pinapahanap
        '// FROM TableName, Tablename, TableName, TableName(4 na table kasi yung pagsasamahin mo)
        '// WHERE condition 1(TableName.ColumnName/Attribute) = (TableName.ColumnName/Attributes) AND  condition 2(TableName.ColumnName/Attributes = TableName.ColumnName/Attributes) 
        'ds.Clear()
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT account.acctNum, 
                       payment.paymentNum, 
                       payment.paymentAmount, 
                       payment.pMod, 
                       paymentMethod.pDesc 

                FROM account, 
                     payment, 
                     pays, 
                     paymentMethod 

                WHERE account.acctNum = pays.acctNum AND 
                      payment.paymentNum = pays.paymentNum AND 
                      paymentMethod.pMod = payment.pMod AND 
                      paymentMethod.pDesc = 'Check'"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "account_payment_pays_paymentMethod")

        Dgrid.DataSource = ds.Tables("account_payment_pays_paymentMethod")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "account_payment_pays_paymentMethod"))

        con.Close()
    End Sub

    Private Sub Button8_Click(sender As Object, e As EventArgs) Handles Button8.Click
        '// 4 Tables (Greater Than Equal)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum, supComName, payment.paymentNum, paymentAmount, paymentDate, pays.acctNum
                FROM payment, pays, account, supplier 
                WHERE payment.paymentNum = pays.paymentNum and pays.acctNum = account.acctNum AND supplier.supNum = account.supNum AND paymentAmount >= 5000"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account_pays_payment")

        Dgrid.DataSource = ds.Tables("supplier_account_pays_payment")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account_pays_payment"))

        con.Close()
    End Sub

    Private Sub Button9_Click(sender As Object, e As EventArgs) Handles Button9.Click
        '// 4 Tables (AND Function/Any Amount)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum, payment.paymentNum, paymentAmount, paymentDate, pays.acctNum, supplier.supComName
                FROM payment, pays, account, supplier
                WHERE payment.paymentNum = pays.paymentNum AND pays.acctNum = account.acctNum AND supplier.supNum = account.supNum"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account_pays_payment")

        Dgrid.DataSource = ds.Tables("supplier_account_pays_payment")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account_pays_payment"))

        con.Close()
    End Sub

    Private Sub Button10_Click(sender As Object, e As EventArgs) Handles Button10.Click
        '// 3 Tables (AND FUNCTION)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT payment.paymentNum, paymentAmount, paymentDate, pays.acctNum 
                FROM payment, pays, account 
                WHERE payment.paymentNum = pays.paymentNum AND pays.acctNum = account.acctNum"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "account_pays_payment")

        Dgrid.DataSource = ds.Tables("account_pays_payment")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "account_pays_payment"))

        con.Close()
    End Sub

    Private Sub Button11_Click(sender As Object, e As EventArgs) Handles Button11.Click
        '// 5 Tables (AND FUNCTION/CREDIT)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum, supplier.supComName, account.acctNum, payment.paymentNum, payment.paymentAmount, payment.pMod, paymentMethod.pDesc
                FROM supplier, account, payment, pays, paymentMethod
                WHERE supplier.supNum = account.supNum and account.acctNum = pays.acctNum AND payment.paymentNum = pays.paymentNum AND paymentMethod.pMod = payment.pMod AND paymentMethod.pDesc = 'Credit'"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account_pays_payment_paymentMethod")

        Dgrid.DataSource = ds.Tables("supplier_account_pays_payment_paymentMethod")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Row: " & da.Fill(ds, "supplier_account_pays_payment_paymentMethod"))

        con.Close()
    End Sub

    Private Sub Button12_Click(sender As Object, e As EventArgs) Handles Button12.Click
        '// 5 Tables (AND FUNCTION/CASH)
        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = " SELECT supplier.supNum, supplier.supComName, account.acctNum, payment.paymentNum, payment.paymentAmount, payment.pMod, paymentMethod.pDesc
                FROM supplier, account, payment, pays, paymentMethod
                WHERE supplier.supNum = account.supNum and account.acctNum = pays.acctNum AND payment.paymentNum = pays.paymentNum AND paymentMethod.pMod = payment.pMod AND paymentMethod.pDesc = 'Cash'"

        da = New OleDbDataAdapter(sql, con)

        da.Fill(ds, "supplier_account_pays_payment_paymentMethod")

        Dgrid.DataSource = ds.Tables("supplier_account_pays_payment_paymentMethod")

        'ds.Clear()
        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        'MsgBox("Result Rows: " & da.Fill(ds, "supplier_account_pays_payment_paymentMethod"))

        con.Close()
    End Sub

    Private Sub Button13_Click(sender As Object, e As EventArgs) Handles Button13.Click



        'Dim connetionString As String
        'Dim connection As OleDbConnection
        'Dim oledbAdapter As New OleDbDataAdapter
        'Dim sql As String
        'connetionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=D:\Downloads\Supply_Account_Ns.accdb"
        'connection = New OleDbConnection(connetionString)
        'sql = "INSERT INTO supplier(supComName, supAddr, supConPer, supPhone) VALUES ('" & txtComName.Text & "', '" & txtAddr.Text & "', '" & txtConPer.Text & "', '" & txtsupPhone.Text & "')"
        'Try
        '    connection.Open()
        '    oledbAdapter.InsertCommand = New OleDbCommand(sql, connection)
        '    oledbAdapter.InsertCommand.ExecuteNonQuery()
        '    MsgBox("Row(s) Inserted !! ")
        'Catch ex As Exception
        '    MsgBox(ex.ToString)
        'End Try


        'Dim connection As OleDbConnection
        'Dim adapt As New OleDbDataAdapter
        'connection = New OleDbConnection(dbcon)

        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()

        sql = "INSERT INTO supplier(supComName, supAddr, supConPer, supPhone) VALUES ('" + txtComName.Text + "','" + txtAddr.Text + "','" + txtConPer.Text + "','" + txtsupPhone.Text + "')"

        da.InsertCommand = New OleDbCommand(sql, con)
        da.InsertCommand.ExecuteNonQuery()

        da.Fill(ds, "supplier")
        Dgrid.DataSource = ds.Tables("supplier")

        MsgBox("New row(s) inserted!")

        con.Close()

    End Sub

    Private Sub Button14_Click(sender As Object, e As EventArgs) Handles Button14.Click

        'Dim connection As OleDbConnection
        'Dim adapt As New OleDbDataAdapter
        'connection = New OleDbConnection(dbcon)

        ds.Clear()
        con.ConnectionString = dbcon


        con.Open()

        sql = "UPDATE supplier set supComName = '" & Convert.ToString(txtComName.Text) & "', supAddr = '" & Convert.ToString(txtAddr.Text) & "' , supConPer = '" & Convert.ToString(txtConPer.Text) &
              "', supPhone = '" & Convert.ToString(txtsupPhone.Text) & "' WHERE supplier.supNum =" & CInt(txtsupNum.Text)

        da.UpdateCommand = New OleDbCommand(sql, con)
        da.UpdateCommand.ExecuteNonQuery()


        da.Fill(ds, "supplier")

        Dgrid.DataSource = ds.Tables("supplier")

        MsgBox("Database is now Updated!")

        con.Close()

    End Sub

    Private Sub Button15_Click(sender As Object, e As EventArgs) Handles Button15.Click

        ds.Clear()
        con.ConnectionString = dbcon

        con.Open()


        sql = "DELETE From supplier WHERE supplier.supNum = " & CInt(txtsupNum.Text) & ""

        da.DeleteCommand = New OleDbCommand(sql, con)
        da.DeleteCommand.ExecuteNonQuery()

        da.Fill(ds, "supplier")
        Dgrid.DataSource = ds.Tables("supplier")

        'MsgBox("Database is now Updated!")
        MsgBox("The supplier number Row: " & txtsupNum.Text & " Is already DELETED!!! ")

        MsgBox("Total Rows: " + Convert.ToString(Dgrid.RowCount - 1) + vbNewLine + "Total Columns: " + Convert.ToString(Dgrid.ColumnCount))

        con.Close()

    End Sub

    Private Sub Button16_Click(sender As Object, e As EventArgs) Handles Button16.Click

        For Each Control In Me.Controls
            If TypeOf Control Is TextBox Then
                Control.text = String.Empty
            End If
        Next

    End Sub
End Class
