Imports System.Web.Services
Imports System.ComponentModel
Imports IBM.Data.DB2.iSeries
Imports DocuWare.Platform.ServerClient
Imports System.Web.Configuration

' Pour autoriser l'appel de ce service Web depuis un script à l'aide d'ASP.NET AJAX, supprimez les marques de commentaire de la ligne suivante.
' <System.Web.Script.Services.ScriptService()> _
<System.Web.Services.WebService(Namespace:="http://tempuri.org/")>
<System.Web.Services.WebServiceBinding(ConformsTo:=WsiProfiles.BasicProfile1_1)>
<ToolboxItem(False)>
Public Class WebService1
    Inherits System.Web.Services.WebService

    Private logger As Object
    Public Function requestDB21(param As String) As String
        'Dim conStr = "DataSource=10.64.20.29;UserID=BJBON;Password=Cambo64250;LibraryList=XDUXFICWEB,Y2SYEXEC;DataCompression=True;providerName=IBM.Data.DB2.iSeries;"
        Dim conStr = WebConfigurationManager.AppSettings("myConnString")
        Dim cnn = New iDB2Connection(conStr)
        cnn.Open()
        Dim cmd = cnn.CreateCommand()
        cmd.CommandText = "BGRKA.TESTCL"
        'TESTCL =  nom de procedure/ BGRKA= le 1er nivo(biblio)
        cmd.CommandType = CommandType.StoredProcedure

        ' create in And out parameters
        Dim parm = cmd.Parameters.Add("@RAISONCODE", iDB2DbType.iDB2Char, 7)
        cmd.Parameters("@RAISONCODE").Direction = ParameterDirection.InputOutput
        cmd.Parameters("@RAISONCODE").Value = ""

        parm = cmd.Parameters.Add("@CODE", iDB2DbType.iDB2Char, 10)
        cmd.Parameters("@CODE").Direction = ParameterDirection.Input
        cmd.Parameters("@CODE").Value = param


        parm = cmd.Parameters.Add("@RESULTAT", iDB2DbType.iDB2Char, 6)
        cmd.Parameters("@RESULTAT").Direction = ParameterDirection.Output


        '// Call the stored procedure
        cmd.ExecuteNonQuery()

        '// Retrieve output parameters
        Dim resultat = cmd.Parameters("@RESULTAT").Value
        cnn.Close()
        Return resultat
    End Function

    <WebMethod()>
    Public Function numChrono(idDoc As Integer) As String
        Dim url = WebConfigurationManager.AppSettings("url")
        Dim Login = WebConfigurationManager.AppSettings("Login")
        Dim Password = WebConfigurationManager.AppSettings("Password")
        Dim Organisation = WebConfigurationManager.AppSettings("Organisation")
        Dim idFileCabinet = WebConfigurationManager.AppSettings("idFileCabinet")

        Dim logger = log4net.LogManager.GetLogger("ErrorLog")
        Dim field = New DocumentIndexField()
        Dim DWDocument As Document
        Dim result As String
        Dim numCrono As String
        Try
            Dim conn = ServiceConnection.Create(New Uri(url), Login, Password, Organisation)
            DWDocument = conn.GetFromDocumentForDocumentAsync(Integer.Parse(idDoc), idFileCabinet).Result
            field = DWDocument.Item("NOM_CLIENT")
            result = field.Item
            conn.Disconnect()
            numCrono = requestDB21(result)
        Catch exc As Exception
            logger.Error(exc.Message)
            Exit Function
        End Try


        Return numCrono

    End Function


End Class