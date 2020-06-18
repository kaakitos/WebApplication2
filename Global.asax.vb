Imports System.Web.SessionState
Imports log4net
Public Class Global_asax
    Inherits System.Web.HttpApplication
    Dim ReadOnly log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)
    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lorsque l'application est démarrée
        log4net.Config.XmlConfigurator.Configure()
        'log.Debug("Application Starting")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lorsque la session est démarrée
        log.Debug("Session started")
        log.DebugFormat("Session ID: {0}", HttpContext.Current.Session.SessionID)
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche au début de chaque demande
    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lors d'une tentative d'authentification de l'utilisation
        log4net.Config.XmlConfigurator.Configure()
        'log.Debug("tentative d'authentification")
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lorsqu'une erreur se produit
        log4net.Config.XmlConfigurator.Configure()
        log.Debug("error")

    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lorsque la session se termine
        log.Debug("Session stopped")
        log.DebugFormat("Session ID: {0}", HttpContext.Current.Session.SessionID)
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Se déclenche lorsque l'application se termine
        log.Debug("Application Ending")
    End Sub

End Class