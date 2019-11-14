
Public Class frm_Runes
    Private runesDirectory As String
    Private keyArray, currentRuneArray As List(Of Rune)
    Private runeArray, keystoneArray As List(Of Rune)
    Private pathArray As List(Of SelectedPath)

    Private Sub frm_Runes_Load(sender As Object, e As EventArgs) Handles Me.Load
        runesDirectory = My.Application.Info.DirectoryPath + "\Resources\Runes\"
        HidePanel(pan_fullSelectedPath) 'Hide all panels that the user shouldn't see
        HidePanel(pan_secondaryPath)
        HidePanel(pan_secondaryRunes1)
        HidePanel(pan_secondaryRunes2)
        HidePanel(pan_secondaryRunes3)
        HidePanel(pan_selectedSecRunes)

        pcb_selectedSecPath.Hide()

        Dim currentRune As Rune
        Dim currentPath As SelectedPath
        pathArray = New List(Of SelectedPath)
        runeArray = New List(Of Rune)
        keystoneArray = New List(Of Rune)

        Dim Paths() As String = {"Precision", "Domination", "Resolve", "Sorcery", "Inspiration"} 'Add all rune paths
        For Each path As String In Paths
            currentPath = New SelectedPath()
            currentPath.RunePath = runesDirectory + path + ".png"
            currentPath.RuneName = path
            currentPath.RuneImage = Image.FromFile(currentPath.RunePath)
            pathArray.Add(currentPath)
        Next

        For Each itemPath As SelectedPath In pathArray
            For i = 1 To 4 'Loading all Keystones
                currentRune = New Rune()
                currentRune.RunePath = runesDirectory + itemPath.RuneName + "_Rune" + i.ToString + "_Key.png"
                If System.IO.File.Exists(currentRune.RunePath) Then
                    currentRune.RuneSelectedPath = itemPath
                    currentRune.RuneImage = Image.FromFile(currentRune.RunePath)
                    keystoneArray.Add(currentRune)
                End If
            Next
            For i = 1 To 3
                For j = 1 To 4 'Loading all runes
                    currentRune = New Rune()
                    currentRune.RunePath = runesDirectory + itemPath.RuneName + "_Rune" + j.ToString + "_P" + i.ToString + ".png"
                    If System.IO.File.Exists(currentRune.RunePath) Then
                        currentRune.RuneImage = Image.FromFile(currentRune.RunePath)
                        currentRune.RuneSelectedPath = itemPath
                        runeArray.Add(currentRune)
                    End If
                Next
            Next
        Next
        Dim flag As Boolean = True
    End Sub

    Private Sub ShowMainRunePath(selectedRune As PictureBox) 'Show the runes in the main path based on the chosen rune path
        keyArray = New List(Of Rune)
        currentRuneArray = New List(Of Rune)

        Dim currentPath As SelectedPath = New SelectedPath()
        For Each rune As Rune In keystoneArray
            If rune.RuneSelectedPath.RuneName = selectedRune.Tag Then
                keyArray.Add(rune)
                currentPath = rune.RuneSelectedPath
            End If
        Next

        Me.BackgroundImage = Image.FromFile(My.Application.Info.DirectoryPath + "\Resources\Backgrounds\" + currentPath.RuneName + ".png")

        For Each rune As Rune In runeArray
            If rune.RuneSelectedPath.Equals(currentPath) Then
                currentRuneArray.Add(rune)
            End If
        Next

        Dim currentIndex As Integer
        Dim item As Control
        Dim pictureItem As PictureBox

        For i = 0 To pan_keyRunes.Controls.Count - 1 'Take the panel in reverse and add the items
            currentIndex = (pan_keyRunes.Controls.Count - 1) - i
            item = pan_keyRunes.Controls.Item(currentIndex)
            pictureItem = item
            If keyArray.Count > 0 Then
                pictureItem.Image = keyArray.Item(0).RuneImage
                item.Tag = keyArray.Item(0).RunePath
                keyArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To 3
            currentIndex = (pan_runes1.Controls.Count - 1) - i
            item = pan_runes1.Controls.Item(currentIndex)
            pictureItem = item
            If i < 3 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To 3
            currentIndex = (pan_runes2.Controls.Count - 1) - i
            item = pan_runes2.Controls.Item(currentIndex)
            pictureItem = item
            If i < 3 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To pan_runes3.Controls.Count - 1
            currentIndex = (pan_runes3.Controls.Count - 1) - i
            item = pan_runes3.Controls.Item(currentIndex)
            pictureItem = item
            If currentRuneArray.Count > 0 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        ShowSecondaryPath(selectedRune)

        ShowPanel(pan_fullSelectedPath)
        ShowPanel(pan_keyRunes)
        ShowPanel(pan_runes1)
        ShowPanel(pan_runes2)
        ShowPanel(pan_runes3)

        pcb_selectedSecRune1.Image = Nothing 'Empty the secondary options to prevent them from having 2 times the same runes
        pcb_selectedSecRune1.Tag = Nothing

        pcb_selectedSecRune2.Image = Nothing
        pcb_selectedSecRune2.Tag = Nothing

        pcb_selectedSecPath.Hide()

        For Each image As PictureBox In pan_selectedRunes.Controls 'Empty the selected runes images.
            image.Image = Nothing
            image.Tag = Nothing
        Next
        pan_mainPath.Hide()
    End Sub

    Private Sub HidePanel(panel As Panel)
        panel.Visible = False
    End Sub

    Private Sub ShowPanel(panel As Panel)
        panel.Visible = True
    End Sub

    Private Sub ShowSecondaryPath(selectedRune As PictureBox)
        Dim secondaryPath As List(Of SelectedPath) = New List(Of SelectedPath)
        For Each path As SelectedPath In pathArray
            If path.RuneName <> selectedRune.Tag Then
                secondaryPath.Add(path)
            End If
        Next

        Dim currentIndex As Integer
        Dim item As Control
        Dim pictureItem As PictureBox

        For i = 0 To pan_secondaryPath.Controls.Count - 1 'Take the panel in reverse and add the items
            currentIndex = (pan_secondaryPath.Controls.Count - 1) - i
            item = pan_secondaryPath.Controls.Item(currentIndex)
            pictureItem = item
            If secondaryPath.Count > 0 Then
                pictureItem.Image = secondaryPath.Item(0).RuneImage
                item.Tag = secondaryPath.Item(0).RunePath
                secondaryPath.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        'Since we are showing the possible path options, we don't want to show the runes inside
        ShowPanel(pan_secondaryPath)
        HidePanel(pan_secondaryRunes1)
        HidePanel(pan_secondaryRunes2)
        HidePanel(pan_secondaryRunes3)

    End Sub

    Private Sub ShowSecondaryPathOptions(selectedRune As PictureBox)
        Dim currentPath As SelectedPath = New SelectedPath()

        For Each rune In runeArray
            If rune.RuneSelectedPath.RunePath = selectedRune.Tag Then
                currentRuneArray.Add(rune)
                currentPath = rune.RuneSelectedPath
            End If
        Next

        Dim currentIndex As Integer
        Dim item As Control
        Dim pictureItem As PictureBox

        For i = 0 To 3
            currentIndex = (pan_secondaryRunes1.Controls.Count - 1) - i
            item = pan_secondaryRunes1.Controls.Item(currentIndex)
            pictureItem = item
            If i < 3 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To 3
            currentIndex = (pan_secondaryRunes2.Controls.Count - 1) - i
            item = pan_secondaryRunes2.Controls.Item(currentIndex)
            pictureItem = item
            If i < 3 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        For i = 0 To pan_secondaryRunes3.Controls.Count - 1
            currentIndex = (pan_secondaryRunes3.Controls.Count - 1) - i
            item = pan_secondaryRunes3.Controls.Item(currentIndex)
            pictureItem = item
            If currentRuneArray.Count > 0 Then
                pictureItem.Image = currentRuneArray.Item(0).RuneImage
                item.Tag = currentRuneArray.Item(0).RunePath
                currentRuneArray.RemoveAt(0)
                item.Visible = True
            Else
                item.Visible = False
            End If
        Next

        pcb_selectedSecPath.Image = selectedRune.Image
        pcb_selectedSecPath.Tag = selectedRune.Tag

        pcb_selectedSecRune1.Image = Nothing 'Hide the secondary runes in case you change between secondaries
        pcb_selectedSecRune1.Tag = Nothing

        pcb_selectedSecRune2.Image = Nothing
        pcb_selectedSecRune2.Tag = Nothing

        pcb_selectedSecPath.Show()

        HidePanel(pan_secondaryPath)
        ShowPanel(pan_secondaryRunes1)
        ShowPanel(pan_secondaryRunes2)
        ShowPanel(pan_secondaryRunes3)
    End Sub

    Private Sub ChangeRune(runeBox As PictureBox, selectedRune As PictureBox, panel As Panel)
        runeBox.Image = Image.FromFile(selectedRune.Tag)
        panel.Hide()
    End Sub

    Private Sub ChangeSecondaryRune(selectedRune As PictureBox, secondaryPanel As Panel)
        If IsNothing(pcb_selectedSecRune1.Image) Then
            ChangeRune(pcb_selectedSecRune1, selectedRune, secondaryPanel)
        ElseIf IsNothing(pcb_selectedSecRune2.Image) Then
            ChangeRune(pcb_selectedSecRune2, selectedRune, secondaryPanel)
        Else
            pcb_selectedSecRune1.Image = Nothing
            pcb_selectedSecRune2.Image = Nothing

            ShowPanel(pan_secondaryRunes1)
            ShowPanel(pan_secondaryRunes2)
            ShowPanel(pan_secondaryRunes3)
        End If
        ShowPanel(pan_selectedSecRunes)
    End Sub

    Private Sub ChangeExtraRune(selectedRune As PictureBox, runeBox As PictureBox, extraPanel As Panel)
        runeBox.Image = selectedRune.Image
        runeBox.Tag = selectedRune.Tag
        HidePanel(extraPanel)
    End Sub

    Private Function FindRune(runeList As List(Of Rune), runePath As String) As Rune
        For Each currentRune As Rune In runeList
            If currentRune.RunePath = runePath Then
                Return currentRune
            End If
        Next
        Return Nothing
    End Function

    Private Sub Pcb_selectedKey_Click(sender As Object, e As EventArgs) Handles pcb_selectedKey.Click
        If pan_keyRunes.Visible = True Then
            pan_keyRunes.Hide()
        Else
            pan_keyRunes.Show()
        End If
    End Sub

    Private Sub Pcb_key1_Click(sender As Object, e As EventArgs) Handles pcb_key1.Click
        ChangeRune(pcb_selectedKey, pcb_key1, pan_keyRunes)
    End Sub

    Private Sub Pcb_key2_Click(sender As Object, e As EventArgs) Handles pcb_key2.Click
        ChangeRune(pcb_selectedKey, pcb_key2, pan_keyRunes)
    End Sub

    Private Sub Pcb_key3_Click(sender As Object, e As EventArgs) Handles pcb_key3.Click
        ChangeRune(pcb_selectedKey, pcb_key3, pan_keyRunes)
    End Sub

    Private Sub Pcb_key4_Click(sender As Object, e As EventArgs) Handles pcb_key4.Click
        ChangeRune(pcb_selectedKey, pcb_key4, pan_keyRunes)
    End Sub

    Private Sub Pcb_precisionPath_Click(sender As Object, e As EventArgs) Handles pcb_precisionPath.Click
        ShowMainRunePath(pcb_precisionPath)
        pcb_selectedPath.Image = pcb_precisionPath.Image
        pcb_selectedPath.Tag = pcb_precisionPath.Tag
    End Sub

    Private Sub Pcb_dominationPath_Click(sender As Object, e As EventArgs) Handles pcb_dominationPath.Click
        ShowMainRunePath(pcb_dominationPath)
        pcb_selectedPath.Image = pcb_dominationPath.Image
        pcb_selectedPath.Tag = pcb_dominationPath.Tag
    End Sub

    Private Sub Pcb_rune1P1_Click(sender As Object, e As EventArgs) Handles pcb_rune1P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune1P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune2P1_Click(sender As Object, e As EventArgs) Handles pcb_rune2P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune2P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune3P1_Click(sender As Object, e As EventArgs) Handles pcb_rune3P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune3P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune4P1_Click(sender As Object, e As EventArgs) Handles pcb_rune4P1.Click
        ChangeRune(pcb_selectedRune1, pcb_rune4P1, pan_runes1)
    End Sub

    Private Sub Pcb_rune1P2_Click(sender As Object, e As EventArgs) Handles pcb_rune1P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune1P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune2P2_Click(sender As Object, e As EventArgs) Handles pcb_rune2P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune2P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune3P2_Click(sender As Object, e As EventArgs) Handles pcb_rune3P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune3P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune4P2_Click(sender As Object, e As EventArgs) Handles pcb_rune4P2.Click
        ChangeRune(pcb_selectedRune2, pcb_rune4P2, pan_runes2)
    End Sub

    Private Sub Pcb_rune1P3_Click(sender As Object, e As EventArgs) Handles pcb_rune1P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune1P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune2P3_Click(sender As Object, e As EventArgs) Handles pcb_rune2P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune2P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune3P3_Click(sender As Object, e As EventArgs) Handles pcb_rune3P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune3P3, pan_runes3)
    End Sub

    Private Sub Pcb_rune4P3_Click(sender As Object, e As EventArgs) Handles pcb_rune4P3.Click
        ChangeRune(pcb_selectedRune3, pcb_rune4P3, pan_runes3)
    End Sub

    Private Sub Pcb_selectedPath_Click(sender As Object, e As EventArgs) Handles pcb_selectedPath.Click
        If pan_mainPath.Visible = True Then
            pan_mainPath.Hide()
        Else
            pan_mainPath.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune1_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune1.Click
        If pan_runes1.Visible = True Then
            pan_runes1.Hide()
        Else
            pan_runes1.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune2_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune2.Click
        If pan_runes2.Visible = True Then
            pan_runes2.Hide()
        Else
            pan_runes2.Show()
        End If
    End Sub

    Private Sub Pcb_selectedRune3_Click(sender As Object, e As EventArgs) Handles pcb_selectedRune3.Click
        If pan_runes3.Visible = True Then
            pan_runes3.Hide()
        Else
            pan_runes3.Show()
        End If
    End Sub

    Private Sub Pcb_resolvePath_Click(sender As Object, e As EventArgs) Handles pcb_resolvePath.Click
        ShowMainRunePath(pcb_resolvePath)
        pcb_selectedPath.Image = pcb_resolvePath.Image
        pcb_selectedPath.Tag = pcb_resolvePath.Tag
    End Sub

    Private Sub Pcb_secPathRune1_Click(sender As Object, e As EventArgs) Handles pcb_secPathRune1.Click
        ShowSecondaryPathOptions(pcb_secPathRune1)
    End Sub

    Private Sub Pcb_secPathRune2_Click(sender As Object, e As EventArgs) Handles pcb_secPathRune2.Click
        ShowSecondaryPathOptions(pcb_secPathRune2)
    End Sub

    Private Sub Pcb_secPathRune3_Click(sender As Object, e As EventArgs) Handles pcb_secPathRune3.Click
        ShowSecondaryPathOptions(pcb_secPathRune3)
    End Sub

    Private Sub Pcb_secPathRune4_Click(sender As Object, e As EventArgs) Handles pcb_secPathRune4.Click
        ShowSecondaryPathOptions(pcb_secPathRune4)
    End Sub

    Private Sub Pcb_secRune1P1_Click(sender As Object, e As EventArgs) Handles pcb_secRune1P1.Click
        ChangeSecondaryRune(pcb_secRune1P1, pan_secondaryRunes1)
    End Sub

    Private Sub Pcb_secRune2P1_Click(sender As Object, e As EventArgs) Handles pcb_secRune2P1.Click
        ChangeSecondaryRune(pcb_secRune2P1, pan_secondaryRunes1)
    End Sub

    Private Sub Pcb_secRune3P1_Click(sender As Object, e As EventArgs) Handles pcb_secRune3P1.Click
        ChangeSecondaryRune(pcb_secRune3P1, pan_secondaryRunes1)
    End Sub

    Private Sub Pcb_secRune4P1_Click(sender As Object, e As EventArgs) Handles pcb_secRune4P1.Click
        ChangeSecondaryRune(pcb_secRune4P1, pan_secondaryRunes1)
    End Sub

    Private Sub Pcb_secRune1P2_Click(sender As Object, e As EventArgs) Handles pcb_secRune1P2.Click
        ChangeSecondaryRune(pcb_secRune1P2, pan_secondaryRunes2)
    End Sub

    Private Sub Pcb_secRune2P2_Click(sender As Object, e As EventArgs) Handles pcb_secRune2P2.Click
        ChangeSecondaryRune(pcb_secRune2P2, pan_secondaryRunes2)
    End Sub

    Private Sub Pcb_secRune3P2_Click(sender As Object, e As EventArgs) Handles pcb_secRune3P2.Click
        ChangeSecondaryRune(pcb_secRune3P2, pan_secondaryRunes2)
    End Sub

    Private Sub Pcb_secRune4P2_Click(sender As Object, e As EventArgs) Handles pcb_secRune4P2.Click
        ChangeSecondaryRune(pcb_secRune4P2, pan_secondaryRunes2)
    End Sub

    Private Sub Pcb_secRune1P3_Click(sender As Object, e As EventArgs) Handles pcb_secRune1P3.Click
        ChangeSecondaryRune(pcb_secRune1P3, pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_secRune2P3_Click(sender As Object, e As EventArgs) Handles pcb_secRune2P3.Click
        ChangeSecondaryRune(pcb_secRune2P3, pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_secRune3P3_Click(sender As Object, e As EventArgs) Handles pcb_secRune3P3.Click
        ChangeSecondaryRune(pcb_secRune3P3, pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_secRune4P3_Click(sender As Object, e As EventArgs) Handles pcb_secRune4P3.Click
        ChangeSecondaryRune(pcb_secRune4P3, pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_selectedSecPath_Click(sender As Object, e As EventArgs) Handles pcb_selectedSecPath.Click
        If pan_secondaryPath.Visible = True Then
            HidePanel(pan_secondaryPath)
        Else
            ShowPanel(pan_secondaryPath)
        End If
    End Sub

    Private Sub Pcb_selectedSecRune1_Click(sender As Object, e As EventArgs) Handles pcb_selectedSecRune1.Click
        pcb_selectedSecRune1.Image = Nothing 'Hide the secondary to allow the users to change them
        pcb_selectedSecRune1.Tag = Nothing

        pcb_selectedSecRune2.Image = Nothing
        pcb_selectedSecRune2.Tag = Nothing
        ShowPanel(pan_secondaryRunes1)
        ShowPanel(pan_secondaryRunes2)
        ShowPanel(pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_selectedSecRune2_Click(sender As Object, e As EventArgs) Handles pcb_selectedSecRune2.Click
        pcb_selectedSecRune1.Image = Nothing 'Hide the secondary to allow the users to change them
        pcb_selectedSecRune1.Tag = Nothing

        pcb_selectedSecRune2.Image = Nothing
        pcb_selectedSecRune2.Tag = Nothing
        ShowPanel(pan_secondaryRunes1)
        ShowPanel(pan_secondaryRunes2)
        ShowPanel(pan_secondaryRunes3)
    End Sub

    Private Sub Pcb_extra1P1_Click(sender As Object, e As EventArgs) Handles pcb_extra1P1.Click
        ChangeExtraRune(pcb_extra1P1, pcb_selectedExtra1, pan_extra1)
    End Sub

    Private Sub Pcb_extra2P1_Click(sender As Object, e As EventArgs) Handles pcb_extra2P1.Click
        ChangeExtraRune(pcb_extra2P1, pcb_selectedExtra1, pan_extra1)
    End Sub

    Private Sub Pcb_extra3P1_Click(sender As Object, e As EventArgs) Handles pcb_extra3P1.Click
        ChangeExtraRune(pcb_extra3P1, pcb_selectedExtra1, pan_extra1)
    End Sub

    Private Sub Pcb_extra1P2_Click(sender As Object, e As EventArgs) Handles pcb_extra1P2.Click
        ChangeExtraRune(pcb_extra1P2, pcb_selectedExtra2, pan_extra2)
    End Sub

    Private Sub Pcb_extra2P2_Click(sender As Object, e As EventArgs) Handles pcb_extra2P2.Click
        ChangeExtraRune(pcb_extra2P2, pcb_selectedExtra2, pan_extra2)
    End Sub

    Private Sub Pcb_extra3P2_Click(sender As Object, e As EventArgs) Handles pcb_extra3P2.Click
        ChangeExtraRune(pcb_extra3P2, pcb_selectedExtra2, pan_extra2)
    End Sub

    Private Sub Pcb_extra1P3_Click(sender As Object, e As EventArgs) Handles pcb_extra1P3.Click
        ChangeExtraRune(pcb_extra1P3, pcb_selectedExtra3, pan_extra3)
    End Sub

    Private Sub Pcb_extra2P3_Click(sender As Object, e As EventArgs) Handles pcb_extra2P3.Click
        ChangeExtraRune(pcb_extra2P3, pcb_selectedExtra3, pan_extra3)
    End Sub

    Private Sub Pcb_extra3P3_Click(sender As Object, e As EventArgs) Handles pcb_extra3P3.Click
        ChangeExtraRune(pcb_extra3P3, pcb_selectedExtra3, pan_extra3)
    End Sub

    Private Sub Pcb_selectedExtra1_Click(sender As Object, e As EventArgs) Handles pcb_selectedExtra1.Click
        If pan_extra1.Visible = True Then
            HidePanel(pan_extra1)
        Else
            ShowPanel(pan_extra1)
        End If
    End Sub

    Private Sub Pcb_selectedExtra2_Click(sender As Object, e As EventArgs) Handles pcb_selectedExtra2.Click
        If pan_extra2.Visible = True Then
            HidePanel(pan_extra2)
        Else
            ShowPanel(pan_extra2)
        End If
    End Sub

    Private Sub Pcb_selectedExtra3_Click(sender As Object, e As EventArgs) Handles pcb_selectedExtra3.Click
        If pan_extra3.Visible = True Then
            HidePanel(pan_extra3)
        Else
            ShowPanel(pan_extra3)
        End If
    End Sub

    Private Sub Pcb_sorceryPath_Click(sender As Object, e As EventArgs) Handles pcb_sorceryPath.Click
        ShowMainRunePath(pcb_sorceryPath)
        pcb_selectedPath.Image = pcb_sorceryPath.Image
        pcb_selectedPath.Tag = pcb_sorceryPath.Tag
    End Sub

    Private Sub Pcb_inspirationPath_Click(sender As Object, e As EventArgs) Handles pcb_inspirationPath.Click
        ShowMainRunePath(pcb_inspirationPath)
        pcb_selectedPath.Image = pcb_inspirationPath.Image
        pcb_selectedPath.Tag = pcb_inspirationPath.Tag
    End Sub
End Class

Public Class Rune
    Public RuneImage As Image
    Public RunePath As String
    Public RuneSelectedPath As SelectedPath
End Class

Public Class SelectedPath
    Public RuneImage As Image
    Public RuneName As String
    Public RunePath As String
End Class