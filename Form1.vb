Public Class frmLoLClientModifier
    Private idiac, idinu As Boolean
    Private idiact, idinue, strfilename As String

    Private Sub PictureBox1_Click(sender As Object, e As EventArgs) Handles PictureBox1.Click

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        idiac = True
        If ListBox1.SelectedItem = "Alemán" Then
            idiact = "de_DE"
        End If
        If ListBox1.SelectedItem = "Checo" Then
            idiact = "cs_CZ"
        End If
        If ListBox1.SelectedItem = "Español (Latinoamerica)" Then
            idiact = "es_MX"
        End If
        If ListBox1.SelectedItem = "Español (España)" Then
            idiact = "es_ES"
        End If
        If ListBox1.SelectedItem = "Ingles (Estados Unidos)" Then
            idiact = "en_US"
        End If
        If ListBox1.SelectedItem = "Ingles (Inglaterra)" Then
            idiact = "en_GB"
        End If
        If ListBox1.SelectedItem = "Ingles (Inglaterra)" Then
            idiact = "en_GB"
        End If
        If ListBox1.SelectedItem = "Ingles (Inglaterra)" Then
            idiact = "en_GB"
        End If
        If ListBox1.SelectedItem = "Frances" Then
            idiact = "fr_FR"
        End If
        If ListBox1.SelectedItem = "Griego" Then
            idiact = "el_GR"
        End If
        If ListBox1.SelectedItem = "Italiano" Then
            idiact = "it_IT"
        End If
        If ListBox1.SelectedItem = "Japonés" Then
            idiact = "ja_JP"
        End If
        If ListBox1.SelectedItem = "Polaco" Then
            idiact = "pl_PL"
        End If
        If ListBox1.SelectedItem = "Portugues" Then
            idiact = "pt_BR"
        End If
        If ListBox1.SelectedItem = "Ruso" Then
            idiact = "ru_RU"
        End If
        If ListBox1.SelectedItem = "Turco" Then
            idiact = "tr_TR"
        End If

    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        If idiac = True And idinu = True Then
            Button2.Enabled = True
            Timer1.Enabled = False
        End If
    End Sub

    Private i As Single
    Private selectedfile As String

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim content As String

        ' lee el contenido actual del texto
        content = System.IO.File.ReadAllText(strfilename)

        If content.Contains(idiact) = False Then
            MsgBox("El idioma actual especificado no es válido o correcto.", MsgBoxStyle.Critical, "Error")
            Exit Sub
        End If
        If idiact = idinue Then
            MsgBox("El idioma seleccionado es el mismo que el actual.", MsgBoxStyle.Critical, "Error")
            Exit sub    
        End If
        ' modifica el idioma
        content = content.Replace(idiact, idinue)

        ' escribe el nuevo contenido
        System.IO.File.WriteAllText(strfilename, content)
        MsgBox("Idioma instalado correctamente. Ejecuta League of Legends para comprobar.", MsgBoxStyle.Information, "Completado")
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        idinu = True
        If ListBox2.SelectedItem = "Alemán" Then
            idinue = "de_DE"
        End If
        If ListBox2.SelectedItem = "Checo" Then
            idinue = "cs_CZ"
        End If
        If ListBox2.SelectedItem = "Español (Latinoamerica)" Then
            idinue = "es_MX"
        End If
        If ListBox2.SelectedItem = "Español (España)" Then
            idinue = "es_ES"
        End If
        If ListBox2.SelectedItem = "Ingles (Estados Unidos)" Then
            idinue = "en_US"
        End If
        If ListBox2.SelectedItem = "Ingles (Inglaterra)" Then
            idinue = "en_GB"
        End If
        If ListBox2.SelectedItem = "Ingles (Inglaterra)" Then
            idinue = "en_GB"
        End If
        If ListBox2.SelectedItem = "Ingles (Inglaterra)" Then
            idinue = "en_GB"
        End If
        If ListBox2.SelectedItem = "Frances" Then
            idinue = "fr_FR"
        End If
        If ListBox2.SelectedItem = "Griego" Then
            idinue = "el_GR"
        End If
        If ListBox2.SelectedItem = "Italiano" Then
            idinue = "it_IT"
        End If
        If ListBox2.SelectedItem = "Japonés" Then
            idinue = "ja_JP"
        End If
        If ListBox2.SelectedItem = "Polaco" Then
            idinue = "pl_PL"
        End If
        If ListBox2.SelectedItem = "Portugues" Then
            idinue = "pt_BR"
        End If
        If ListBox2.SelectedItem = "Ruso" Then
            idinue = "ru_RU"
        End If
        If ListBox2.SelectedItem = "Turco" Then
            idinue = "tr_TR"
        End If
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Dim fd As OpenFileDialog = New OpenFileDialog()

        fd.Title = "Selecciona tu LeagueClientSettings.yaml"
        fd.InitialDirectory = "C:\"
        fd.Filter = "All files (*.*)|*.*|All files (*.*)|*.*"
        fd.FilterIndex = 2
        fd.RestoreDirectory = True

        If fd.ShowDialog() = DialogResult.OK Then
            strfilename = fd.FileName
        End If

        ListBox1.Enabled = True
        ListBox2.Enabled = True
    End Sub
End Class
