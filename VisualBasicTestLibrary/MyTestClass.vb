Public Class MyTestClass
    Public Shared Sub PrintMessage(message As String)
        Console.WriteLine("This is method call is coming from Visual Basic. Your message is : " + message)
    End Sub
    Public Shared Function GetTotalPayMessage(employeeName As String, totalPay As Double, randomDictionary As Dictionary(Of String, List(Of String()))) As String
        Return employeeName + " earned £" + totalPay.ToString() + " in total."
    End Function
End Class
