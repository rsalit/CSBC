Imports System
Imports System.Collections.Generic
Imports System.Collections.Specialized
Imports System.ComponentModel
Imports System.Web
Imports System.Web.UI
Imports Encore.PayPal.Nvp
Imports System.Web.Configuration

''' <summary> 
''' Summary description for Globals 
''' </summary> 
<DataObject()> _
Public NotInheritable Class Globals
    Private Sub New()
    End Sub
    Public Shared Function ToString(ByVal input As Object) As String
        Return (IIf(input Is Nothing, String.Empty, input.ToString()))
    End Function

    Public Shared ReadOnly Property UserName() As String
        Get
            If HttpContext.Current.Session("APIUsername") IsNot Nothing Then
                Return HttpContext.Current.Session("APIUsername")
            Else
                Return NvpConfig.Settings.Username
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Password() As String
        Get
            If HttpContext.Current.Session("APIPassword") IsNot Nothing Then
                Return HttpContext.Current.Session("APIPassword")
            Else
                Return NvpConfig.Settings.Password
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Signature() As String
        Get
            If HttpContext.Current.Session("APISignature") IsNot Nothing Then
                Return HttpContext.Current.Session("APISignature")
            Else
                Return NvpConfig.Settings.Signature
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Version() As String
        Get
            If HttpContext.Current.Session("APIVersion") IsNot Nothing Then
                Return HttpContext.Current.Session("APIVersion")
            Else
                Return NvpConfig.Settings.Version
            End If
        End Get
    End Property

    Public Shared ReadOnly Property Environment() As NvpEnvironment
        Get
            If HttpContext.Current.Session("APIEnvironment") IsNot Nothing Then
                Return HttpContext.Current.Session("APIEnvironment")
            Else
                Return NvpConfig.Settings.Environment
            End If
        End Get
    End Property

    Public Shared ReadOnly Property ApiUrl() As String
        Get
            Dim url As String = NvpConfig.Settings.API3tURL
            Select Case Environment
                Case NvpEnvironment.Sandbox
                    url = url.Insert(url.ToLower().IndexOf("paypal"), "sandbox.")
                    Exit Select
                Case NvpEnvironment.BetaSandbox
                    url = url.Insert(url.ToLower().IndexOf("paypal"), "beta-sandbox.")
                    Exit Select
                Case Else
                    Exit Select
            End Select
            Return url
        End Get
    End Property

    Public Shared ReadOnly Property PayPalUrl() As String
        Get
            Dim url As String = NvpConfig.Settings.PayPalURL
            Select Case Environment
                Case NvpEnvironment.Sandbox
                    url = url.Insert(url.ToLower().IndexOf("paypal"), "sandbox.")
                    Exit Select
                Case NvpEnvironment.BetaSandbox
                    url = url.Insert(url.ToLower().IndexOf("paypal"), "beta-sandbox.")
                    Exit Select
                Case Else
                    Exit Select
            End Select
            Return url
        End Get
    End Property

    Public Shared Function GetCredentials() As NvpCredentials
        Dim credentials As New NvpCredentials()
        If HttpContext.Current.Session("APIUsername") IsNot Nothing Then
            credentials.Username = HttpContext.Current.Session("APIUsername").ToString()
        End If
        If HttpContext.Current.Session("APIPassword") IsNot Nothing Then
            credentials.Password = HttpContext.Current.Session("APIPassword").ToString()
        End If
        If HttpContext.Current.Session("APISignature") IsNot Nothing Then
            credentials.Signature = HttpContext.Current.Session("APISignature").ToString()
        End If
        If HttpContext.Current.Session("APISubject") IsNot Nothing Then
            credentials.Subject = HttpContext.Current.Session("APISubject").ToString()
        End If
        If HttpContext.Current.Session("APIUseEncryption") IsNot Nothing Then
            credentials.UseEncryption = CBool(HttpContext.Current.Session("APIUseEncryption"))
        End If
        If HttpContext.Current.Session("APIEncryptionKey") IsNot Nothing Then
            credentials.EncryptionKey = HttpContext.Current.Session("APIEncryptionKey").ToString()
        End If
        If HttpContext.Current.Session("APICertificate") IsNot Nothing Then
            credentials.Certificate = HttpContext.Current.Session("APICertificate").ToString()
        End If
        If HttpContext.Current.Session("APIKeyPassword") IsNot Nothing Then
            credentials.KeyPassword = HttpContext.Current.Session("APIKeyPassword").ToString()
        End If
        If HttpContext.Current.Session("APIUseCertificate") IsNot Nothing Then
            credentials.UseCertificate = CBool(HttpContext.Current.Session("APIUseCertificate"))
        End If
        Return credentials
    End Function

    Public Shared Sub SetCredentials(ByVal apiRequest As NvpApiBase)
        apiRequest.Credentials = GetCredentials()

        If HttpContext.Current.Session("APIVerison") IsNot Nothing Then
            apiRequest.Add(NvpCommonRequest.VERSION, HttpContext.Current.Session("APIVerison").ToString())
        End If
        If HttpContext.Current.Session("APIEnvironment") IsNot Nothing Then
            apiRequest.Environment = DirectCast(HttpContext.Current.Session("APIEnvironment"), NvpEnvironment)
        End If
    End Sub

    Public Shared Sub SetCredentials(ByVal username As String, ByVal password As String, ByVal signature As String, _
        ByVal subject As String, ByVal useEncryption As Boolean, ByVal encryptionKey As String, _
        ByVal certificate As String, ByVal keyPassword As String, ByVal useCertificate As Boolean, _
        ByVal version As String, ByVal environment As NvpEnvironment)
        HttpContext.Current.Session("APIUsername") = IIf(username = String.Empty, Nothing, username)
        HttpContext.Current.Session("APIPassword") = IIf(password = String.Empty, Nothing, password)
        HttpContext.Current.Session("APISignature") = IIf(signature = String.Empty, Nothing, signature)
        HttpContext.Current.Session("APISubject") = IIf(subject = String.Empty, Nothing, subject)
        HttpContext.Current.Session("APIUseEncryption") = useEncryption
        HttpContext.Current.Session("APIEncryptionKey") = IIf(encryptionKey = String.Empty, Nothing, encryptionKey)
        HttpContext.Current.Session("APICertificate") = IIf(certificate = String.Empty, Nothing, certificate)
        HttpContext.Current.Session("APIKeyPassword") = IIf(keyPassword = String.Empty, Nothing, keyPassword)
        HttpContext.Current.Session("APIUseCertificate") = useCertificate
        HttpContext.Current.Session("APIVerison") = IIf(version = String.Empty, Nothing, version)
        HttpContext.Current.Session("APIEnvironment") = environment
    End Sub
End Class