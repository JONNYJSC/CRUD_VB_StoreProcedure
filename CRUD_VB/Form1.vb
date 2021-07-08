Imports System.Data.SqlClient

Public Class FormCliente
    Dim connectionstring As String = "server=JONNYJSC-DELL; Initial Catalog=db_Cliente; User Id=sa; Password=admin123;"
    Dim sqlcon As SqlConnection
    Dim sqlcmd As New SqlCommand
    Dim sqlda As SqlDataAdapter
    Dim dt As DataTable

    Private Sub Limpiar()
        txtId.Clear()
        txtNombre.Clear()
        txtCiudad.Clear()
    End Sub
    Public Sub Load_grid()
        Try
            sqlcon = New SqlConnection(connectionstring)
            sqlcmd.Connection = sqlcon
            sqlcmd.CommandText = "spCliente"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("modo", "mostrar")
            sqlcon.Open()
            sqlda = New SqlDataAdapter(sqlcmd)
            dt = New DataTable("tblCliente")
            sqlda.Fill(dt)
            divGridCliente.DataSource = dt
            sqlcmd.Parameters.Clear()
            sqlcon.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
    End Sub
    Private Sub FormCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.Load_grid()
        divGridCliente.Columns(0).Visible = False
        Dim checkBoxColumn As DataGridViewCheckBoxColumn = New DataGridViewCheckBoxColumn()
        checkBoxColumn.HeaderText = "Check"
        checkBoxColumn.Width = 50
        checkBoxColumn.Name = "checkBoxColumn"
        divGridCliente.Columns.Insert(0, checkBoxColumn)
        AddHandler divGridCliente.CellContentClick, AddressOf divGridCliente_CellContentClick
    End Sub

    Private Sub divGridCliente_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles divGridCliente.CellContentClick
        If e.RowIndex >= 0 AndAlso e.ColumnIndex = 0 Then
            For Each row As DataGridViewRow In divGridCliente.Rows
                If row.Index = e.RowIndex Then
                    row.Cells("checkBoxColumn").Value = Not Convert.ToBoolean(row.Cells("checkBoxColumn").EditedFormattedValue)
                    txtId.Text = row.Cells(1).Value.ToString()
                    txtNombre.Text = row.Cells(2).Value.ToString()
                    txtCiudad.Text = row.Cells(3).Value.ToString()
                Else
                    row.Cells("checkBoxColumn").Value = False
                End If
            Next
        End If
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If (txtNombre.Text = String.Empty) Then
            MessageBox.Show("Ingrese el nombre del Cliente")
            Return
        End If

        If (txtCiudad.Text = String.Empty) Then
            MessageBox.Show("Ingrese la ciudad del Cliente")
            Return
        End If

        Try
            Dim client As New Cliente
            client.Nombre = txtNombre.Text.Trim()
            client.Ciudad = txtCiudad.Text.Trim()
            sqlcon = New SqlConnection(connectionstring)
            sqlcmd.Connection = sqlcon
            sqlcmd.CommandText = "spCliente"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("modo", "guardar")
            sqlcmd.Parameters.AddWithValue("Nombre", client.Nombre)
            sqlcmd.Parameters.AddWithValue("Ciudad", client.Ciudad)
            sqlcon.Open()
            sqlcmd.ExecuteNonQuery()
            sqlcmd.Parameters.Clear()
            sqlcon.Close()
            MessageBox.Show("guardado con éxito !!")

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try
        Me.Limpiar()
        Me.Load_grid()
    End Sub

    Private Sub btnActualizar_Click(sender As Object, e As EventArgs) Handles btnActualizar.Click
        'Using con As SqlConnection = New SqlConnection(connectionstring)
        '    Using cmd As SqlCommand = New SqlCommand("UPDATE tblCliente SET Nombre = @Nombre, Ciudad = @Ciudad WHERE ClienteId = @ClienteId", con)
        '        cmd.Parameters.AddWithValue("@ClienteId", txtId.Text)
        '        cmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
        '        cmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text)
        '        con.Open()
        '        cmd.ExecuteNonQuery()
        '        con.Close()
        '        MessageBox.Show("actualizado con éxito !!")
        '    End Using
        'End Using

        Try
            Dim client As New Cliente
            client.Nombre = txtNombre.Text.Trim()
            client.Ciudad = txtCiudad.Text.Trim()
            sqlcon = New SqlConnection(connectionstring)
            sqlcmd.Connection = sqlcon
            sqlcmd.CommandText = "spCliente"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("modo", "actualizar")
            sqlcmd.Parameters.AddWithValue("@ClienteId", txtId.Text)
            sqlcmd.Parameters.AddWithValue("@Nombre", txtNombre.Text)
            sqlcmd.Parameters.AddWithValue("@Ciudad", txtCiudad.Text)
            sqlcon.Open()
            sqlcmd.ExecuteNonQuery()
            sqlcmd.Parameters.Clear()
            sqlcon.Close()
            MessageBox.Show("actualizado con éxito !!")

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

        Me.Load_grid()
        Me.Limpiar()
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        'Using con As SqlConnection = New SqlConnection(connectionstring)
        '    Using cmd As SqlCommand = New SqlCommand("DELETE FROM tblCliente WHERE ClienteId = @ClienteId", con)
        '        cmd.Parameters.AddWithValue("@ClienteId", txtId.Text)
        '        con.Open()
        '        cmd.ExecuteNonQuery()
        '        con.Close()
        '        MessageBox.Show("eliminado con éxito !!")
        '    End Using
        'End Using

        Try
            sqlcon = New SqlConnection(connectionstring)
            sqlcmd.Connection = sqlcon
            sqlcmd.CommandText = "spCliente"
            sqlcmd.CommandType = CommandType.StoredProcedure
            sqlcmd.Parameters.AddWithValue("modo", "eliminar")
            sqlcmd.Parameters.AddWithValue("@ClienteId", txtId.Text)
            sqlcon.Open()
            sqlcmd.ExecuteNonQuery()
            sqlcmd.Parameters.Clear()
            sqlcon.Close()
            MessageBox.Show("eliminado con éxito !!")

        Catch ex As Exception
            MessageBox.Show(ex.Message.ToString())
        End Try

        Me.Load_grid()
        Me.Limpiar()
    End Sub
End Class
