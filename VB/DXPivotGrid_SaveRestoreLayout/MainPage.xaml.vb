Imports Microsoft.VisualBasic
Imports System.IO
Imports System.Windows
Imports System.Windows.Controls
Imports System.Xml.Serialization

Namespace DXPivotGrid_SaveRestoreLayout
	Partial Public Class MainPage
		Inherits UserControl
		Private dataFileName As String = "nwind.xml"
		Private stream As MemoryStream
		Public Sub New()
			InitializeComponent()

			' Parses an XML file and creates a collection of data items.
			Dim assembly As System.Reflection.Assembly = _
				System.Reflection.Assembly.GetExecutingAssembly()
			Dim stream As Stream = assembly.GetManifestResourceStream(dataFileName)
			Dim s As New XmlSerializer(GetType(OrderData))
			Dim dataSource As Object = s.Deserialize(stream)

			' Binds a pivot grid to this collection.
			pivotGridControl1.DataSource = dataSource
		End Sub
		Private Sub buttonSave_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			stream = New MemoryStream()
			pivotGridControl1.SaveLayoutToStream(stream)
		End Sub
		Private Sub buttonLoad_Click(ByVal sender As Object, ByVal e As RoutedEventArgs)
			If stream Is Nothing Then
				Return
			End If
			stream.Position = 0
			pivotGridControl1.RestoreLayoutFromStream(stream)
		End Sub
	End Class
End Namespace