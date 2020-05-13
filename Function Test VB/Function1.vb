
Imports System.Linq
Imports System.Net
Imports System.Net.Http
Imports System.Threading.Tasks
Imports Microsoft.Azure.WebJobs
Imports Microsoft.Azure.WebJobs.Extensions.Http
Imports Microsoft.Azure.WebJobs.Host

Public Class Function1
    <FunctionName("Function1")>
    Public Shared Async Function Run(<HttpTrigger(AuthorizationLevel.[Function], "get", "post", Route:=Nothing)> ByVal req As HttpRequestMessage, ByVal log As TraceWriter) As Task(Of HttpResponseMessage)

        ' <HttpTrigger(AuthorizationLevel.[Function], "get", "post", Route:=Nothing)> ByVal req As HttpRequestMessage, ByVal log As TraceWriter) As Task(Of HttpResponseMessage)
        log.Info("VB 64bit HTTP trigger function processed a request.")
        Dim name As String = req.GetQueryNameValuePairs().FirstOrDefault(Function(q) String.Compare(q.Key, "name", True) = 0).Value

        If name Is Nothing Then
            Dim dat = Await req.Content.ReadAsAsync(Of Object)()
            name = dat?.name
        End If

        Return If(name Is Nothing, req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body"), req.CreateResponse(HttpStatusCode.OK, "Hello from " & name))
    End Function

End Class
