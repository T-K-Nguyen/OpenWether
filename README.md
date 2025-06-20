# Weather Forecast Application

A simple Windows Forms application that displays current weather data using the OpenWeatherMap API and a hardcoded 7-day weather forecast. The current weather is presented in a top-right card format, while the 7-day forecast is displayed in card-like panels with icons.
<p align="center"> 
 <img src="Screenshots/Run.png" width="600"/> 
 <br/> <i>Figure: Application outcome</i> 
</p>


## Features
- Fetch real-time current weather data for any city using the OpenWeatherMap API.
- Display current weather in a table format (City, Temperature, Weather, Humidity, Wind Speed).
- Due to no actual subsription to openWeathermap so this project can't show realtime data for the upcoming days, so it will show a hardcoded 7-day weather forecast (June 15–21, 2025) with temperature ranges and weather conditions.
- Use customizable icons for weather conditions (stored as resources).

## Prerequisites
- **Visual Studio**: 2019 or later (Community, Professional, or Enterprise edition).
- **.NET Framework**: 4.7.2 or higher (adjust based on your project settings).
- **Internet Connection**: Required to fetch current weather data from the OpenWeatherMap API.
- **API Key**: An API key from OpenWeatherMap (sign up at [https://openweathermap.org/](https://openweathermap.org/) to get one).

## Installation

1. **Clone the Repository**:
   - Clone this repository to your local machine:
     ```
     git clone https://github.com/T-K-Nguyen/OpenWether.git
     ```
   - If not using Git, download the ZIP file and extract it.

2. **Open the Project**:
   - Open the solution file (`WeatherForecast.sln`) in Visual Studio.

3. **Configure the API Key**:
   - Open `Form1.vb`.
   - Replace the `API_KEY` constant with your OpenWeatherMap API key:
     ```vbnet
     Private Const API_KEY As String = "your_api_key_here"
     ```

4. **Add png Resources**:
   - Ensure the following `.png` files are added as resources:
     - `clear.png`
     - `clouds.png`
     - `rain.png`
     - `thunder.png`
     - `snow.png`
     - `default.png`
   - To add resources:
     - Go to **Project Properties** > **Resources** tab.
     - Click **Add Existing File** and select each `.png` file, ensuring they are added as **icons**.

5. **Build the Project**:
   - Build the solution (Build > Build Solution) to compile the application.

## Usage

1. **Run the Application**:
   - Press F5 in Visual Studio or run the executable from the `bin\Debug` or `bin\Release` folder.

   <p align="center"> 
      <img src="Screenshots/starting.png" width="600"/> 
      <br/> <i>Figure: Successfully run app</i> 
   </p>

2. **Enter a City**:
   - Type a city name (e.g., "London") in the text box labeled "LONDON".

3. **Search**:
   - Click the "Search" button to fetch and display the current weather data in the table.
   - The 7-day forecast will also appear with hardcoded data and icons.

4. **View Results**:
   - The table on the right shows the current weather (City, Temperature, Weather, Humidity, Wind Speed).
   - The seven panels below show the 7-day forecast with temperatures and weather conditions.
   <p align="center"> 
      <img src="Screenshots/Run.png" width="600"/> 
      <br/> <i>Figure: Application outcome</i> 
   </p>
## Project Structure
- **Form1.vb**: Main form containing the UI and logic.
- **Resources**: Contains `.png` files for weather icons.

- **Form1.Designer.vb**: Main form containing the UI and logic.
   <p align="center"> 
      <img src="Screenshots/design_view.png" width="600"/> 
      <br/> <i>Figure: Desgin View</i> 
   </p>
## Known Issues
- The 7-day forecast is hardcoded and does not reflect real data.
- Requires an internet connection for current weather data.
- icon display depends on valid `.png` files in resources; missing files may cause errors.

## Contributing
Feel free to fork this repository and submit pull requests. Suggestions for improvements (e.g., adding real 7-day forecast data or enhancing the UI) are welcome!

## License
This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments
- Weather data provided by [OpenWeatherMap](https://openweathermap.org/).
- icon resources adapted from various free icons sets.
