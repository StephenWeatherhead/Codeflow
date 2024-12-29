Public Class MyTestClass
    Public Shared Function GetWriteLineExpression(employeeName As String, totalPay As Double) As String
        Return String.Format("{0} earned {1:C} in total", employeeName, totalPay)
    End Function
End Class
