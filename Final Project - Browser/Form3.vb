﻿'Will Eccles
'Date: 12/18/13
'Project: Final Project - Browser
'Language: VB.net

Option Explicit On

Public Class Form3
    'For formatting info, see https://owl.english.purdue.edu/owl/resource/747/08/

    Private Sub Form3_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'On form load, set the date of accessing the site (today's date).
        Lab_Date.Text = "Date accessed: " & DateAndTime.Today()
	End Sub

	Private Sub AutoSiteName_Click(sender As System.Object, e As System.EventArgs) Handles AutoSiteName.Click
        'Set the site name automatically.
        TBox_SiteName.Text = Form1.Browser.DocumentTitle
	End Sub
    'When user changes checked state of box, enable/disable text box accordingly.
    Private Sub EnableName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnableName.CheckedChanged
		If EnableName.Checked = True Then
			TBox_Name.Enabled = True
		ElseIf EnableName.Checked = False Then
			TBox_Name.Enabled = False
		End If
	End Sub
    'When user changes checked state of box, enable/disable text box accordingly.
    Private Sub EnableSiteName_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnableSiteName.CheckedChanged
		If EnableSiteName.Checked = True Then
			TBox_SiteName.Enabled = True
		ElseIf EnableSiteName.Checked = False Then
			TBox_SiteName.Enabled = False
		End If
	End Sub
    'When user changes checked state of box, enable/disable text box accordingly.
    Private Sub EnableVNum_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnableVNum.CheckedChanged
		If EnableVNum.Checked = True Then
			TBox_VNum.Enabled = True
		ElseIf EnableVNum.Checked = False Then
			TBox_VNum.Enabled = False
		End If
	End Sub
    'Enable or disable the text box associated with the check box.
    Private Sub EnableOrganization_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnableOrganization.CheckedChanged
		If EnableOrganization.Checked = True Then
			TBox_Organization.Enabled = True
		Else
			TBox_Organization.Enabled = False
		End If
	End Sub
    'Enable/disable text box for date of creation.
    Private Sub EnableDate_CheckedChanged(sender As System.Object, e As System.EventArgs) Handles EnableDate.CheckedChanged
		If EnableDate.Checked = True Then
			DateCreation.Enabled = True
		Else
			DateCreation.Enabled = False
		End If
	End Sub
    'Generate citation.
    Private Sub But_Gen_Click(sender As System.Object, e As System.EventArgs) Handles But_Gen.Click
		Dim citation As String = ""
        'If set, add name to MLA citation.
        If ((EnableName.Checked = True) And (TBox_Name.Text <> "")) Then
			citation = citation & TBox_Name.Text & "."
		End If
        'Add site name if applicable.
        If ((EnableSiteName.Checked = True) And (TBox_SiteName.Text <> "")) Then
			citation = citation & " " & TBox_SiteName.Text & "."
		End If
        'Version Number.
        If ((EnableVNum.Checked = True) And (TBox_VNum.Text <> "")) Then
			citation = citation & " " & TBox_VNum.Text & "."
		End If
        'Set organization and creation date or just organization.
        If ((EnableOrganization.Checked = True) And (TBox_Organization.Text <> "")) Then
			If ((EnableDate.Checked = True) And (DateCreation.Text <> "")) Then
				citation = citation & " " & TBox_Organization.Text & ", " & DateCreation.Text & "."
			Else
				citation = citation & " " & TBox_Organization.Text & "."
			End If
		End If
        'Add only date of creation if organization is not specified.
        If ((EnableOrganization.Checked = False) And (EnableDate.Checked = True) And (DateCreation.Text <> "")) Then
			citation = citation & " " & DateCreation.Text & "."
		End If
        'Fills in the medium of publication.
        citation = citation & " " & "Web."
        'Date of access. Automagically generated by the computer for you!
        citation = citation & " " & DateAndTime.Today & "."
        'Add URL, also automagic!
        If EnableURL.Checked = True Then
			citation = citation & " " & "<" & Form1.Browser.Url.ToString & ">."
		End If
        'This is the section to save the file.
        '------------------------------------------
        'Dimension variables.
        Dim folderFinder As New FolderBrowserDialog
		Dim browsertitlefriendly As String = Form1.Browser.DocumentTitle.ToString.Replace(" ", "_").Replace(",", "_").Replace("<", "_").Replace(">", "_").Replace(":", "_").Replace(Chr(34), "_").Replace("/", "_").Replace("\", "_").Replace("|", "_").Replace("?", "_").Replace("*", "_").Replace("-", "_")
        'The instructions on the dialog:
        folderFinder.Description = "Choose a folder to save this in. It will show up as 'PAGE_TITLE_mla.txt' in that folder."
        'Show the dialog and get the resulting button press.
        Dim result As DialogResult = folderFinder.ShowDialog()
        'If the button they press is the OK button...
        If result = Windows.Forms.DialogResult.OK Then
            'Get the selected path.
            Dim chosenPath As String = folderFinder.SelectedPath.ToString()
            'Change the path to the one to save it to.
            Dim savetopath As String = (chosenPath & "\" & browsertitlefriendly & "_mla.txt")
            'Actually write to the file:
            My.Computer.FileSystem.WriteAllText(savetopath, ("Remember: you must indent all but the first line when put in documents, and you need to italicize the page name, due to the inability of the program to do so." & vbCrLf & vbCrLf & vbCrLf & citation), False)
            MessageBox.Show("Successfully saved '" & browsertitlefriendly & "_mla.txt' to '" & folderFinder.SelectedPath.ToString & "'!", "Success!")
        End If
    End Sub
End Class