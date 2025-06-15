Imports System.Net.Http
Imports Newtonsoft.Json.Linq
Imports System.Threading.Tasks
Imports System.Windows.Forms

Public Class Form1
    Inherits Form

    Private ReadOnly httpClient As New HttpClient()
    Private Const API_KEY As String = "c7c3964e9264d60b1b19b7730c23b02b"

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Async Sub SearchButton_Click(sender As Object, e As EventArgs) Handles searchButton.Click
        If String.IsNullOrWhiteSpace(cityTextBox.Text) Then
            MessageBox.Show("Please enter a city name!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        loadingLabel.Visible = True
        resultPanel.Visible = False
        searchButton.Enabled = False

        Try
            Dim weatherData = Await GetWeatherData(cityTextBox.Text)
            If weatherData IsNot Nothing Then
                DisplayWeatherData(weatherData)
                Await FadeInPanel()
            Else
                MessageBox.Show("Failed to retrieve weather data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            loadingLabel.Visible = False
            searchButton.Enabled = True
        End Try
    End Sub

    Private Async Function GetWeatherData(city As String) As Task(Of JObject)
        Dim url As String = $"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={API_KEY}&units=metric"
        Dim response = Await httpClient.GetAsync(url)
        If Not response.IsSuccessStatusCode Then
            MessageBox.Show($"API Error: {response.StatusCode} - {response.ReasonPhrase}")
            Return Nothing
        End If
        Dim content = Await response.Content.ReadAsStringAsync()
        Return JObject.Parse(content)
    End Function

    Private Sub DisplayWeatherData(data As JObject)
        ' Display current weather from API
        Dim cityName As String = data("name").ToString()
        Dim temp As Double = data("main")("temp").Value(Of Double)()
        Dim description As String = data("weather")(0)("description").ToString()
        Dim humidity As Integer = data("main")("humidity").Value(Of Integer)()
        Dim windSpeed As Double = data("wind")("speed").Value(Of Double)()

        Dim textToDisplay = $"City: {cityName}{vbCrLf}" &
                           $"Temperature: {temp:F1}°C{vbCrLf}" &
                           $"Weather: {description}{vbCrLf}" &
                           $"Humidity: {humidity}%{vbCrLf}" &
                           $"Wind Speed: {windSpeed:F1} m/s"
        resultLabel.Text = textToDisplay
        resultPanel.Visible = True

        ' Hardcoded 7-day forecast data (June 15-21, 2025)
        'Dim days() As String = {"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat"}
        Dim tempsMin() As Double = {18.0, 19.0, 20.0, 21.0, 22.0, 23.0, 24.0}
        Dim tempsMax() As Double = {25.0, 26.0, 27.0, 28.0, 29.0, 30.0, 31.0}
        Dim descriptions() As String = {"Sunny", "Cloudy", "Rain", "Thunderstorm", "Snow", "Partly Cloudy", "Clear"}

        For i As Integer = 0 To 6
            Dim pnlDay = Me.Controls("pnlDay" & (i + 1))
            If pnlDay IsNot Nothing AndAlso TypeOf pnlDay Is Panel Then
                Dim lblTemp = DirectCast(pnlDay.Controls("lblTemp" & (i + 1)), Label)
                Dim picIcon = DirectCast(pnlDay.Controls("picIcon" & (i + 1)), PictureBox)
                Dim lblDesc = DirectCast(pnlDay.Controls("lblDesc" & (i + 1)), Label)

                If lblTemp IsNot Nothing AndAlso picIcon IsNot Nothing AndAlso lblDesc IsNot Nothing Then
                    lblTemp.Text = $"{tempsMin(i):F0}° {tempsMax(i):F0}°"
                    lblDesc.Text = descriptions(i)
                    SetWeatherIcon(picIcon, descriptions(i))
                    pnlDay.Visible = True
                    pnlDay.BringToFront()
                Else
                    MessageBox.Show($"Child control not found for day {i + 1} in pnlDay{i + 1}. Please check Designer.", "Debug")
                End If
            Else
                MessageBox.Show($"Panel pnlDay{i + 1} not found. Please check Designer.", "Debug")
            End If
        Next

        resultLabel.Refresh()
    End Sub

    Private Sub SetWeatherIcon(picBox As PictureBox, description As String)
        Try
            Dim iconImage As Image = Nothing
            Select Case description.ToLower()
                Case "sunny", "clear"
                    iconImage = My.Resources.clear.ToBitmap() ' Convert Icon to Bitmap
                Case "cloudy", "partly cloudy"
                    iconImage = My.Resources.clouds.ToBitmap()
                Case "rain"
                    iconImage = My.Resources.rain.ToBitmap()
                Case "thunderstorm"
                    iconImage = My.Resources.thunder.ToBitmap()
                Case "snow"
                    iconImage = My.Resources.snow.ToBitmap()
                Case Else
                    iconImage = My.Resources._default.ToBitmap()
            End Select
            If iconImage IsNot Nothing Then
                picBox.Image = iconImage
                picBox.SizeMode = PictureBoxSizeMode.Zoom
                picBox.BackColor = Color.Transparent
            End If
        Catch ex As Exception
            MessageBox.Show($"Error loading icon: {ex.Message}. Ensure .ico resources are valid.", "Error")
            picBox.Image = Nothing
        End Try
    End Sub

    Private Async Function FadeInPanel() As Task
        resultPanel.Visible = True
        resultPanel.Refresh()
    End Function
End Class