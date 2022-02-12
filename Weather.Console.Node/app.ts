import { WeatherForecastApi } from "../Weather.Api.Client.Sdk.Typescript/api/weatherForecastApi";

let weatherForecastApi = new WeatherForecastApi("https://localhost:7036");
let forecastsPromise = weatherForecastApi.getWeatherForecasts();

forecastsPromise.then((forecasts) => {
    forecasts.body.forEach((forecast) => {
        console.log(`Date: ${forecast.date}`);
        console.log(`Summary: ${forecast.summary}`);
        console.log(`Temperature in celsius: ${forecast.temperatureC}`);
    });
});
