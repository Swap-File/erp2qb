
Imports CookComputing.XmlRpc
Imports System.Runtime.Serialization

'[XmlRpcUrl(AuxUrl._url_common)]
Public Interface IOpenErpLogin
    Inherits IXmlRpcProxy
    <XmlRpcMethod("login")> _
    Function login(ByVal dbName As String, ByVal dbUser As String, ByVal dbPwd As String) As Integer
End Interface


'[XmlRpcUrl(AuxUrl._url_object)]
Public Interface IOpenErp
    Inherits IXmlRpcProxy
    <XmlRpcMethod("execute")> _
    Function create(ByVal dbName As String, ByVal userId As Integer, ByVal pwd As String, ByVal model As String, ByVal method As String, ByVal fieldValues As XmlRpcStruct) As Integer

    <XmlRpcMethod("execute")> _
    Function search(ByVal dbName As String, ByVal userId As Integer, ByVal pwd As String, ByVal model As String, ByVal method As String, ByVal filter As Object()) As integer()

    <XmlRpcMethod("execute")> _
    Function write(ByVal dbName As String, ByVal userId As Integer, ByVal pwd As String, ByVal model As String, ByVal method As String, ByVal ids As Integer(), ByVal fieldValues As XmlRpcStruct) As Boolean

    <XmlRpcMethod("execute")> _
    Function unlink(ByVal dbName As String, ByVal userId As Integer, ByVal dbPwd As String, ByVal model As String, ByVal method As String, ByVal ids As Integer()) As Boolean

    <XmlRpcMethod("execute")> _
    Function read(ByVal dbName As String, ByVal userId As Integer, ByVal dbPwd As String, ByVal model As String, ByVal method As String, ByVal ids As Integer(), ByVal fields As Object()) As [Object]()

    <XmlRpcMethod("exec_workflow")> _
    Function exec_workflow(ByVal dbName As String, ByVal userId As Integer, ByVal dbPwd As String, ByVal model As String, ByVal action As String, ByVal ids As Integer) As Boolean



End Interface


Public Class OpenErpObject
    Private server_url As String = "http://localhost:8069"
    Private _suffix_host_url As String = "xmlrpc"

    Private _common_url As String = "common"
    Private _object_url As String = "object"

    Private dbName As String = "db_simple3"
    Private userName As String = "admin"
    Private pwd As String = "admin"
    Private userid As Integer = -1
    Private rpcClient As IOpenErp

    Public Sub New(ByVal host_url As String, ByVal dbname As String, ByVal user As String, ByVal passw As String)
        Me.server_url = host_url
        Me._common_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _common_url
        Me._object_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _object_url

        Me.dbName = dbname
        Me.userName = user
        Me.pwd = passw
    End Sub

    Public Sub New(ByVal data As Object())
        Me.server_url = data(0).ToString()
        Me._common_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _common_url
        Me._object_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _object_url

        Me.dbName = DirectCast(data(1), String)
        Me.userName = data(2).ToString()
        Me.pwd = data(3).ToString()
    End Sub

    Public Sub New()
        Me._common_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _common_url
        Me._object_url = Me.server_url & "/" & Me._suffix_host_url & "/" & _object_url

        Me.login()
    End Sub

    Public Function login() As Boolean

        'Login to openerp
        Dim rpcClientLogin As IOpenErpLogin = DirectCast(XmlRpcProxyGen.Create(GetType(IOpenErpLogin)), IOpenErpLogin)
        rpcClientLogin.Url = Me._common_url
        Try
            Me.userid = rpcClientLogin.login(Me.dbName, Me.userName, Me.pwd)

            'XML RPC Client
            Me.rpcClient = DirectCast(XmlRpcProxyGen.Create(GetType(IOpenErp)), IOpenErp)
            rpcClient.Url = Me._object_url

            Return True
        Catch ex As Exception
            MessageBox.Show("Error: " + ex.Message)
            Return False
        End Try
    End Function

    Public Function create(ByVal model As String, ByVal fieldValues As XmlRpcStruct) As Integer
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.create(Me.dbName, Me.userid, Me.pwd, model, "create", fieldValues)
    End Function

    Public Function search(ByVal model As String, ByVal filter As Object()) As Integer()
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.search(Me.dbName, Me.userid, Me.pwd, model, "search", filter)
    End Function

    Public Function write(ByVal model As String, ByVal ids As Integer(), ByVal fieldValues As XmlRpcStruct) As Boolean
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.write(Me.dbName, Me.userid, Me.pwd, model, "write", ids, fieldValues)
    End Function

    Public Function unlink(ByVal model As String, ByVal ids As Integer()) As Boolean
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.unlink(Me.dbName, Me.userid, Me.pwd, model, "unlink", ids)
    End Function

    Public Function read(ByVal model As String, ByVal ids As Integer(), ByVal fields As Object()) As Object()
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.read(Me.dbName, Me.userid, Me.pwd, model, "read", ids, fields)
    End Function

   

    Public Function exec_workflow(ByVal model As String, ByVal action As String, ByVal id As Integer) As Boolean
        If Me.userid = -1 Then
            Me.login()
        End If
        Return Me.rpcClient.exec_workflow(Me.dbName, Me.userid, Me.pwd, model, action, id)
    End Function

End Class